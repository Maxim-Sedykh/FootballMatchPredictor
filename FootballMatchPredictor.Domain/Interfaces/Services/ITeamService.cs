using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Team;
using FootballMatchPredictor.Domain.ViewModels.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Interfaces.Services
{
    public interface ITeamService
    {
        Task<BaseResult> CreateTeam(CreateTeamViewModel viewModel);

        Task<CollectionResult<TeamViewModel>> GetAllTeams();

        Task<BaseResult> UpdateTeam(UpdateTeamViewModel viewModel);
    }
}
