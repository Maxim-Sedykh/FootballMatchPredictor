using FootballMatchPredictor.Application.Resources.Error;
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
    /// <inheritdoc/>
    public class MatchService : IMatchService
    {
        private readonly IBaseRepository<Match> _matchRepository;

        public MatchService(IBaseRepository<Match> matchRepository)
        {
            _matchRepository = matchRepository;
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
    }
}
