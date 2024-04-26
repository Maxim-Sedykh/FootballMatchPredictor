using FootballMatchPredictor.Application.Resources.Error;
using FootballMatchPredictor.Application.Resources.Success;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Coefficient;
using FootballMatchPredictor.Domain.ViewModels.Match;
using FootballMatchPredictor.Domain.ViewModels.Team;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FootballMatchPredictor.Application.Services
{
    /// <inheritdoc/>
    public class MatchService : IMatchService
    {
        private readonly IBaseRepository<Match> _matchRepository;

        public MatchService(IBaseRepository<Match> matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<BaseResult> CreateMatch(CreateMatchViewModel viewModel)
        {
            var match = await _matchRepository.GetAll()
                .FirstOrDefaultAsync(x => (x.Team1Id == viewModel.Team1Id || x.Team1Id == viewModel.Team2Id)
                                        && (x.Team2Id == viewModel.Team1Id || x.Team2Id == viewModel.Team2Id)
                                        && x.MatchDate == viewModel.MatchDate);

            if (match != null)
            {
                return new BaseResult()
                {
                    ErrorCode = (int)StatusCode.MatchAlreadyExist,
                    ErrorMessage = ErrorMessage.MatchAlreadyExist
                };
            }

            match = viewModel.Adapt<Match>();

            await _matchRepository.CreateAsync(match);

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
                .Select(x => x.Adapt<MatchViewModel>())
                .ToListAsync();

            if (matches == null)
            {
                return new CollectionResult<MatchViewModel>()
                {
                    ErrorMessage = ErrorMessage.MatchesNotFound,
                    ErrorCode = (int)StatusCode.MatchesNotFound
                };
            }

            return new CollectionResult<MatchViewModel>()
            {
                Data = matches,
                Count = matches.Count   
            };
        }

        public async Task<BaseResult<MatchViewModel>> GetMatch(long id)
        {
            var match = await _matchRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

            if (match == null)
            {
                return new BaseResult<MatchViewModel>()
                {
                    ErrorMessage = ErrorMessage.MatchNotFound,
                    ErrorCode = (int)StatusCode.MatchNotFound
                };
            }

            return new BaseResult<MatchViewModel>()
            {
                Data = match.Adapt<MatchViewModel>()
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
            var match = await _matchRepository.GetAll().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (match == null)
            {
                return new BaseResult<MatchViewModel>()
                {
                    ErrorMessage = ErrorMessage.MatchNotFound,
                    ErrorCode = (int)StatusCode.MatchNotFound
                };
            }

            match = viewModel.Adapt<Match>();

            return new BaseResult<MatchViewModel>()
            {
                SuccessMessage = SuccessMessage.MatchUpdated
            };
        }
    }
}
