using FootballMatchPredictor.Application.Resources.Error;
using FootballMatchPredictor.Application.Resources.Success;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Team;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FootballMatchPredictor.Application.Services
{
    public class TeamService : ITeamService
    {
        private readonly IBaseRepository<Team> _teamRepository;

        public TeamService(IBaseRepository<Team> teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<BaseResult> CreateTeam(CreateTeamViewModel viewModel)
        {
            var team = viewModel.Adapt<Team>();

            await _teamRepository.CreateAsync(team);

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.TeamCreated,
            };
        }

        public async Task<BaseResult> DeleteTeam(short id)
        {
            var team = await _teamRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

            if (team == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.TeamNotFound,
                    ErrorCode = (int)StatusCode.TeamNotFound
                };
            }

            await _teamRepository.RemoveAsync(team);

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.TeamDeleted,
            };
        }

        public async Task<CollectionResult<TeamViewModel>> GetAllTeams()
        {
            var teams = await _teamRepository.GetAll()
                .Include(x => x.Country)
                .Select(x => new TeamViewModel(x.Id, x.Name, x.Country.CountryName))
                .ToListAsync();

            if (teams.Count == 0)
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
                Count = teams.Count
            };
        }

        public async Task<BaseResult> UpdateTeam(UpdateTeamViewModel viewModel)
        {
            var team = await _teamRepository.GetAll().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (team == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.TeamNotFound,
                    ErrorCode = (int)StatusCode.TeamNotFound
                };
            }

            team.Name = viewModel.Name;
            team.CountryId = viewModel.CountryId;

            await _teamRepository.UpdateAsync(team);

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.TeamUpdated,
            };
        }
    }
}
