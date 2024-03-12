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
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FootballMatchPredictor.Application.Services
{
    public class MatchService : IMatchService
    {
        private readonly IBaseRepository<Match> _matchRepository;

        public MatchService(IBaseRepository<Match> matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<BaseResult> CreateMatch(CreateMatchViewModel viewModel)
        {
            var match = viewModel.Adapt<Match>();

            if (match.Team1Id == match.Team2Id)
            {
                return new BaseResult()
                {
                    ErrorCode = (int)StatusCode.TeamsAreEqual,
                    ErrorMessage = ErrorMessage.TeamsAreEqual,
                };
            }

            match.MatchState = MatchState.NotPlayedYet;

            await _matchRepository.CreateAsync(match);

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.MatchCreated,
            };
        }

        public async Task<BaseResult> DeleteMatch(long id)
        {
            var match = await _matchRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

            if (match == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.MatchNotFound,
                    ErrorCode = (int)StatusCode.MatchNotFound
                };
            }

            await _matchRepository.RemoveAsync(match);

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.MatchDeleted,
            };
        }

        public async Task<BaseResult<MatchPageViewModel>> GetAllMatches()
        {
            var matches = await _matchRepository.GetAll()
                .Include(x => x.Team1)
                .Include(x => x.Team2)
                .Include(x => x.Coefficients)
                .ThenInclude(x => x.CoefficientRefer)
                .Select(x => new MatchViewModel(x.Id, x.Team1.Name, x.Team2.Name, x.Team1GoalsCount, x.Team2GoalsCount, x.MatchState.GetDisplayName(), x.MatchDate, 
                x.Coefficients.Select(x => new MatchCoefficientViewModel(x.Id, x.CoefficientValue, x.IsActive, x.CoefficientRefer.Description, x.CreatedAt)).ToList()))
                .ToListAsync();

            var liveMatches = matches.Where(x => x.MatchState == MatchState.InProgress.GetDisplayName()).ToList();
            var notPlayedMatches = matches.Where(x => x.MatchState == MatchState.NotPlayedYet.GetDisplayName()).ToList();
            var playedMatches = matches.Except(notPlayedMatches).Except(liveMatches).ToList();

            return new BaseResult<MatchPageViewModel>()
            {
                Data = new MatchPageViewModel(liveMatches, notPlayedMatches, playedMatches),
            };
        }

        public async Task<BaseResult> UpdateMatch(UpdateMatchViewModel viewModel)
        {
            var match = await _matchRepository.GetAll().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (match == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.MatchNotFound,
                    ErrorCode = (int)StatusCode.MatchNotFound
                };
            }

            match.Team1Id = viewModel.Team1Id;
            match.Team2Id = viewModel.Team2Id;
            match.Team1GoalsCount = viewModel.Team1GoalsCount;
            match.Team2GoalsCount = viewModel.Team2GoalsCount;
            match.MatchState = viewModel.MatchState;
            match.MatchDate = viewModel.MatchDate;

            await _matchRepository.UpdateAsync(match);

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.MatchUpdated,
            };
        }
    }
}
