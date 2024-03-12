using FootballMatchPredictor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Bet
{
    public record MakeBetViewModel(
        Dictionary<int, string>? PaymentMethods,
        [Required] long CoefficientId,
        [Required(ErrorMessage = "Вы должны выбрать платёжный метод!")] string? SelectedPaymentMethodId,
        decimal MoneyAmount
    );
}
 