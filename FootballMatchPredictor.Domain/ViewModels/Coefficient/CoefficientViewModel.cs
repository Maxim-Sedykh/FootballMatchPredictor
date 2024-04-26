using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Coefficient
{
    /// <summary>
    /// Модель представления для просмотра коэффициента
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Team1Name"></param>
    /// <param name="Team2Name"></param>
    /// <param name="CoefficientValue"></param>
    /// <param name="CoefficientReferDescription"></param>
    /// <param name="CreatedAt"></param>
    public record CoefficientViewModel(
        long Id,
        string Team1Name,
        string Team2Name,
        float CoefficientValue,
        string BetType,
        bool IsActive,
        DateTime MatchDate,
        DateTime CreatedAt
    );
}
