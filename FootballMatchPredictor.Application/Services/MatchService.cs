using FootballMatchPredictor.Application.Resources.Error;
using FootballMatchPredictor.Application.Resources.Success;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Match;
using FootballMatchPredictor.Domain.ViewModels.UserProfile;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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

        public async Task<CollectionResult<MatchViewModel>> GetAllMatches()
        {
            var matches = await _matchRepository.GetAll()
                .Include(x => x.Team1)
                .Include(x => x.Team2)
                .Select(x => new MatchViewModel(x.Id, x.Team1.Name, x.Team2.Name, x.Team1GoalsCount, x.Team2GoalsCount, x.MatchState, x.MatchDate))
                .OrderBy(x => x.MatchDate)
                .ToListAsync();

            if (matches.Count == 0)
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
                Count = matches.Count()
            };
        }

        public Task<BaseResult> UpdateMatch(UpdateMatchViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResult<MatchViewModel>> GetMatchById(long id)
        {
            var matches = await _matchRepository.GetAll()
                .Include(x => x.Team1)
                .Include(x => x.Team2)
                .Select(x => new MatchViewModel(x.Id, x.Team1.Name, x.Team2.Name, x.Team1GoalsCount, x.Team2GoalsCount, x.MatchState, x.MatchDate))
                .FirstOrDefaultAsync(x => x.Id == id);

            if (matches == null)
            {
                return new BaseResult<MatchViewModel>()
                {
                    ErrorMessage = ErrorMessage.MatchNotFound,
                    ErrorCode = (int)StatusCode.MatchNotFound
                };
            }

            return new CollectionResult<MatchViewModel>()
            {
                Data = matches,
                Count = matches.Count()
            };
        }

        public Task<BaseResult> UpdateMatch(UpdateMatchViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
