using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Coefficient
{
    /// <summary>
    /// Модель представления для просмотра информации по коэффициентам на матч
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="CoefficientValue"></param>
    /// <param name="IsActive"></param>
    /// <param name="CoefficientReferDescription"></param>
    /// <param name="CreatedAt"></param>
    public record MatchCoefficientViewModel(
        long Id,
        float CoefficientValue,
        bool IsActive,
        string CoefficientReferDescription,
        DateTime CreatedAt
    );
}
