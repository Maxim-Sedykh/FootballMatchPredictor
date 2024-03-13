using FootballMatchPredictor.Application.Resources.Error;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Team;
using Microsoft.EntityFrameworkCore;

namespace FootballMatchPredictor.Application.Services
{
    /// <inheritdoc/>
    public class TeamService : ITeamService
    {
        private readonly IBaseRepository<Team> _teamRepository;

        public TeamService(IBaseRepository<Team> teamRepository)
        {
            _teamRepository = teamRepository;
        }

        /// <inheritdoc/>
        public async Task<CollectionResult<TeamViewModel>> GetAllTeams()
        {
            var teams = await _teamRepository.GetAll()
                .Include(x => x.Country)
                .Select(x => new TeamViewModel(x.Id, x.Name, x.Country.CountryName, x.MatchesPlayed, x.MatchesWon)).ToArrayAsync();

            if (teams.Length == 0)
            {
                return new CollectionResult<TeamViewModel>()
                {
                    ErrorMessage = ErrorMessage.TeamsNotFound,
                    ErrorCode = (int)StatusCode.TeamsNotFound
                };
            }

            return new CollectionResult<TeamViewModel>()
            {
                Data = teams,
                Count = teams.Length
            };
        }
    }
}
