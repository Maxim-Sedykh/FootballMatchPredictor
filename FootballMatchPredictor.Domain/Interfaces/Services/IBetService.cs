using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Bet;
using FootballMatchPredictor.Domain.ViewModels.Coefficient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Interfaces.Services
{
    public interface IBetService
    {
        Task<BaseResult<MakeBetViewModel>> GetDataToMakeBet(long id);

        Task<BaseResult> MakeBet(MakeBetViewModel viewModel, string userName);
    }
}
