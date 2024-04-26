using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Coefficient
{
    public record UpdateCoefficientViewModel(
        long Id,
        string Team1Name,
        string Team2Name,
        float CoefficientValue,
        string BetType,
        bool IsActive
    );
}
