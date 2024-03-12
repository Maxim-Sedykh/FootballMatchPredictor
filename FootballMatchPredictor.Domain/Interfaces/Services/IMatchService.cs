using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Match;
using FootballMatchPredictor.Domain.ViewModels.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Interfaces.Services
{
    public interface IMatchService
    {
        Task<BaseResult> CreateMatch(CreateMatchViewModel viewModel);

        Task<BaseResult<MatchPageViewModel>> GetAllMatches();

        Task<BaseResult> UpdateMatch(UpdateMatchViewModel viewModel);

        Task<BaseResult> DeleteMatch(long id);
    }
}
