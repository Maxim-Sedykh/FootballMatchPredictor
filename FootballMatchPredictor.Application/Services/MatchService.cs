using FootballMatchPredictor.Application.Helpers.Elo;
using FootballMatchPredictor.Application.Helpers.EloClasses;
using FootballMatchPredictor.Application.Resources.Error;
using FootballMatchPredictor.Application.Resources.Success;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.Interfaces.Database;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Coefficient;
using FootballMatchPredictor.Domain.ViewModels.Match;
using FootballMatchPredictor.Domain.ViewModels.Team;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Text.RegularExpressions;
using Match = FootballMatchPredictor.Domain.Entities.Match;

namespace FootballMatchPredictor.Application.Services
{
    /// <inheritdoc/>
    public class MatchService : IMatchService
    {
        private readonly IBaseRepository<Match> _matchRepository;
        private readonly IBaseRepository<Team> _teamRepository;
        private readonly IBaseRepository<Coefficient> _coefficientRepository;
        private readonly IBaseRepository<Bet> _betRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MatchService(IBaseRepository<Match> matchRepository, IBaseRepository<Team> teamRepository,
            IBaseRepository<Coefficient> coefficientRepository, IUnitOfWork unitOfWork, IBaseRepository<Bet> betRepository,
            IBaseRepository<User> userRepository)
        {
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _coefficientRepository = coefficientRepository;
            _unitOfWork = unitOfWork;
            _betRepository = betRepository;
            _userRepository = userRepository;
        }

        public async Task<BaseResult> CreateMatch(CreateMatchViewModel viewModel)
        {
            var team1 = await _teamRepository.GetAll().FirstOrDefaultAsync(x => x.Id == viewModel.Team1Id);
            var team2 = await _teamRepository.GetAll().FirstOrDefaultAsync(x => x.Id == viewModel.Team2Id);

            var match = await _matchRepository.GetAll()
                .FirstOrDefaultAsync(x => (x.Team1Id == viewModel.Team1Id || x.Team1Id == viewModel.Team2Id)
                                        && (x.Team2Id == viewModel.Team1Id || x.Team2Id == viewModel.Team2Id)
                                        && x.MatchDate == DateTime.SpecifyKind(viewModel.MatchDate, DateTimeKind.Utc));

            if (team1 == null || team2 == null)
            {
                return new BaseResult()
                {
                    ErrorCode = (int)StatusCode.TeamNotFound,
                    ErrorMessage = ErrorMessage.TeamNotFound
                };
            }

            if (match != null)
            {
                return new BaseResult()
                {
                    ErrorCode = (int)StatusCode.MatchAlreadyExist,
                    ErrorMessage = ErrorMessage.MatchAlreadyExist
                };
            }

            if (viewModel.Team1Id == viewModel.Team2Id)
            {
                return new BaseResult()
                {
                    ErrorCode = (int)StatusCode.MatchTeamsAreEqual,
                    ErrorMessage = ErrorMessage.MatchTeamsAreEqual
                };
            }

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    match = viewModel.Adapt<Match>();

                    match.MatchDate = DateTime.SpecifyKind(viewModel.MatchDate, DateTimeKind.Utc);
                    match.Team1Id = viewModel.Team1Id;
                    match.Team2Id = viewModel.Team2Id;

                    var matchWinrates = EloCalculator.CalculateProbabilities(team1.Rating, team2.Rating);

                    match.Team1WinRate = matchWinrates.FirstTeamValue;
                    match.Team2WinRate = matchWinrates.SecondTeamValue;
                    match.DrawProbability = matchWinrates.DrawValue;

                    await _matchRepository.CreateAsync(match);

                    var matchCoefficients = EloCalculator.CalculateBettingCoefficients(team1.Rating, team2.Rating);

                    var newCoefficients = GetNewMatchCoefficients(match.Id, matchCoefficients);

                    await _coefficientRepository.AddRangeAsync(newCoefficients);

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                }
            }

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.MatchCreated
            };
        }

        public CollectionResult<KeyValuePair<int, string>> GetMatchState()
        {
            var genders = Enum.GetValues(typeof(MatchState))
                    .Cast<MatchState>()
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

            return new CollectionResult<KeyValuePair<int, string>>()
            {
                Data = genders,
                Count = genders.Count
            };
        }

        public async Task<BaseResult> DeleteMatch(long id)
        {
            var match = await _matchRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

            if (match == null)
            {
                return new CollectionResult<MatchViewModel>()
                {
                    ErrorMessage = ErrorMessage.MatchNotFound,
                    ErrorCode = (int)StatusCode.MatchNotFound
                };
            }

            await _matchRepository.RemoveAsync(match);

            return new BaseResult() 
            {
                SuccessMessage = SuccessMessage.MatchDeleted
            };
        }

        /// <inheritdoc/>
        public async Task<CollectionResult<MatchViewModel>> GetAllMatches()
        {
            var matches = await _matchRepository.GetAll()
                .Include(x => x.Team1)
                .Include(x => x.Team2)
                .ToListAsync();

            var matchViewModels = matches.Select(x => x.Adapt<MatchViewModel>()).OrderBy(x => x.Id).ToList();

            if (!matches.Any())
            {
                return new CollectionResult<MatchViewModel>()
                {
                    ErrorMessage = ErrorMessage.MatchesNotFound,
                    ErrorCode = (int)StatusCode.MatchesNotFound
                };
            }

            return new CollectionResult<MatchViewModel>()
            {
                Data = matchViewModels,
                Count = matchViewModels.Count,
            };
        }

        private List<Coefficient> GetNewMatchCoefficients(long matchId, MatchValue matchCoefficients)
        {
            return new List<Coefficient>()
            {
                new Coefficient()
                {
                    MatchId = matchId,
                    CoefficientValue = matchCoefficients.FirstTeamValue,
                    IsActive = true,
                    BetType = BetType.FirstTeamWon,
                },
                new Coefficient()
                {
                    MatchId = matchId,
                    CoefficientValue = matchCoefficients.SecondTeamValue,
                    IsActive = true,
                    BetType = BetType.SecondTeamWon,
                },
                new Coefficient()
                {
                    MatchId = matchId,
                    CoefficientValue = matchCoefficients.DrawValue,
                    IsActive = true,
                    BetType = BetType.Draw,
                }
            };
        }

        public async Task<BaseResult<UpdateMatchViewModel>> GetMatchToUpdate(long id)
        {
            var match = await _matchRepository.GetAll()
                .Include(x => x.Team1)
                .Include(x => x.Team2)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (match == null)
            {
                return new BaseResult<UpdateMatchViewModel>()
                {
                    ErrorMessage = ErrorMessage.MatchNotFound,
                    ErrorCode = (int)StatusCode.MatchNotFound
                };
            }

            var result = match.Adapt<UpdateMatchViewModel>();
            
            return new BaseResult<UpdateMatchViewModel>()
            {
                Data = match.Adapt<UpdateMatchViewModel>()
            };
        }

        /// <inheritdoc/>
        public async Task<CollectionResult<MatchViewModel>> GetTeamMatches(long id)
        {
            var teamMatches = await _matchRepository.GetAll()
                .Include(x => x.Team1)
                .Include(x => x.Team2)
                .Where(x => x.Team1Id == id || x.Team2Id == id)
                .Select(x => x.Adapt<MatchViewModel>())
                .ToListAsync();

            return new CollectionResult<MatchViewModel>()
            {
                Data = teamMatches,
                Count = teamMatches.Count
            };
        }

        public async Task<BaseResult<MatchViewModel>> UpdateMatch(UpdateMatchViewModel viewModel)
        {
            if (viewModel.Team1 == viewModel.Team2)
            {
                return new BaseResult<MatchViewModel>()
                {
                    ErrorCode = (int)StatusCode.MatchTeamsAreEqual,
                    ErrorMessage = ErrorMessage.MatchTeamsAreEqual
                };
            }

            var match = await _matchRepository.GetAll()
                .Include(x => x.Team1)
                .Include(x => x.Team2)
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (match == null)
            {
                return new BaseResult<MatchViewModel>()
                {
                    ErrorMessage = ErrorMessage.MatchNotFound,
                    ErrorCode = (int)StatusCode.MatchNotFound
                };
            }

            var team1Id = Convert.ToInt16(viewModel.Team1);
            var team2Id = Convert.ToInt16(viewModel.Team2);

            if (match.Team1Id != team1Id || match.Team2Id != team2Id)
            {
                return new BaseResult<MatchViewModel>()
                {
                    ErrorMessage = ErrorMessage.TeamNotFound,
                    ErrorCode = (int)StatusCode.TeamNotFound
                };
            }

            using(var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var newMatchState = EnumExtension.GetEnumFromDisplay<MatchState>(viewModel.MatchState);

                    match.MatchState = newMatchState;
                    match.Team1Id = team1Id;
                    match.Team2Id = team2Id;
                    match.Team1GoalsCount = viewModel.Team1GoalsCount;
                    match.Team2GoalsCount = viewModel.Team2GoalsCount;
                    match.MatchDate = viewModel.MatchDate;

                    float K = 32; // Коэффициент для метода Эло
                    float Team1Rating = match.Team1.Rating;
                    float Team2Rating = match.Team2.Rating;

                    float ExpectedWinValue = (float)(1 / (1 + Math.Pow(10, (Team2Rating - Team1Rating) / 400)));
                    float WinTeamRatingChange = K * (1 - ExpectedWinValue);
                    float LoseTeamRatingChange = K * (0 - (1 - ExpectedWinValue));

                    var bets = _betRepository.GetAll()
                        .Include(x => x.Coefficient)
                        .Include(x => x.User)
                        .Where(x => x.MatchId == match.Id)
                        .ToList();


                    if (newMatchState != MatchState.NotPlayedYet && newMatchState != MatchState.InProgress)
                    {
                        match.IsEnded = true;

                        if (newMatchState == MatchState.FirstTeamWon)
                        {
                            (match.Team1.Rating, match.Team2.Rating) = (Team1Rating + WinTeamRatingChange, Team2Rating + LoseTeamRatingChange);
                            SetMatchBetsRightState(bets, BetType.FirstTeamWon);
                        }
                        else if (newMatchState == MatchState.SecondTeamWon)
                        {
                            (match.Team1.Rating, match.Team2.Rating) = (Team1Rating + LoseTeamRatingChange, Team2Rating + WinTeamRatingChange);
                            SetMatchBetsRightState(bets, BetType.SecondTeamWon);
                        }
                        else
                        {
                            SetMatchBetsRightState(bets, BetType.Draw);
                        }
                    }

                    await _betRepository.UpdateRangeAsync(bets);
                    await _matchRepository.UpdateAsync(match);

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                }
            }

            return new BaseResult<MatchViewModel>()
            {
                SuccessMessage = SuccessMessage.MatchUpdated
            };
        }

        private void SetMatchBetsRightState(List<Bet> bets, BetType betType)
        {
            foreach (var bet in bets)
            {
                if (bet.Coefficient.BetType == betType)
                {
                    bet.BetState = BetState.Winning;
                    bet.User.WinningSum += bet.WinningAmount;
                }
                else
                {
                    bet.BetState = BetState.Losing;
                }
            }
        }
    }
}
