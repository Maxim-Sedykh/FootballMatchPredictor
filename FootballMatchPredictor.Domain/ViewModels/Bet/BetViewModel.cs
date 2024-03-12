using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Bet
{
    public record BetViewModel(
        long Id,
        string Team1Name,
        string Team2Name,
        float CoefficientValue,
        string CoefficientReferDescription,
        decimal BetAmountMoney,
        decimal WinningAmount,
        string BetTypeName,
        string BetState,
        DateTime CreatedAt
    );
}
