using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Coefficient;
using FootballMatchPredictor.Domain.ViewModels.Match;
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

        /// <inheritdoc/>
        public async Task<CollectionResult<TeamMatchViewModel>> GetTeamMatches(long id)
        {
            var teamMatches = await _matchRepository.GetAll()
                .Include(x => x.Team1)
                .Include(x => x.Team2)
                .Where(x => x.Team1Id == id || x.Team2Id == id)
                .Select(x => new TeamMatchViewModel(x.Id, x.Team1.Name, x.Team2.Name, x.Team1GoalsCount, x.Team2GoalsCount, x.MatchState.GetDisplayName(), x.MatchDate))
                .ToListAsync();

            return new CollectionResult<TeamMatchViewModel>()
            {
                Data = teamMatches,
            };
        }
    }
}
