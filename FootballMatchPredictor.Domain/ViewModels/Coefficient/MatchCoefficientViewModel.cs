using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Coefficient
{
    public record MatchCoefficientViewModel(
        long Id,
        float CoefficientValue,
        bool IsActive,
        string CoefficientReferDescription,
        DateTime CreatedAt
    );
}
