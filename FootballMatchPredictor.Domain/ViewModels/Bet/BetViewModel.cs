using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Bet
{
    /// <summary>
    /// Модель представления для просмотра данных о ставках
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Team1Name"></param>
    /// <param name="Team2Name"></param>
    /// <param name="CoefficientValue"></param>
    /// <param name="CoefficientReferDescription"></param>
    /// <param name="BetAmountMoney"></param>
    /// <param name="WinningAmount"></param>
    /// <param name="BetTypeName"></param>
    /// <param name="BetState"></param>
    /// <param name="CreatedAt"></param>
    public record BetViewModel(
        long Id,
        string Team1Name,
        string Team2Name,
        float CoefficientValue,
        decimal BetAmountMoney,
        decimal WinningAmount,
        string BetType,
        string BetState,
        DateTime CreatedAt
    );
}
