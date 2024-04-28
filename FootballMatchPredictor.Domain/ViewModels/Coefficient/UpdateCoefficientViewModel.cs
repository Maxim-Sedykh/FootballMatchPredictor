using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Coefficient
{
    public record UpdateCoefficientViewModel(
        long Id,

        [Required(ErrorMessage = "Укажите коэффициент")]
        [Range(0,50,ErrorMessage="Недопустимый коэффициент")]
        float CoefficientValue,

        [Required(ErrorMessage = "Укажите тип ставки")]
        string BetType,


        bool IsActive
    );
}
