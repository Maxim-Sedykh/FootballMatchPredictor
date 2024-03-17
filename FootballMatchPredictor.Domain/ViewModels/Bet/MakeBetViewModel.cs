using FootballMatchPredictor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Bet
{
    /// <summary>
    /// Модель представления для модальной страницы для того, чтобы сделать ставку
    /// </summary>
    /// <param name="CoefficientId"></param>
    /// <param name="PaymentMethod"></param>
    /// <param name="MoneyAmount"></param>
    public record MakeBetViewModel(
        long CoefficientId,

        [Required(ErrorMessage = "Вы должны выбрать платёжный метод!")] 
        string? PaymentMethod,

        [Required(ErrorMessage = "Вы должны указать количество денежных средств")]
        [Range(1000, 1000000, ErrorMessage = "Значение должно быть от 1000 до 1 000 000")]
        decimal MoneyAmount
    );
}
 