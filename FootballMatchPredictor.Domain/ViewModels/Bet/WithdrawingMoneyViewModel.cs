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
    /// Модель представления для вывода денег пользователем
    /// </summary>
    /// <param name="OutputAmount"></param>
    /// <param name="PaymentMethod"></param>
    /// <param name="WinningSum"></param>
    public record WithdrawingMoneyViewModel(
        [Required(ErrorMessage = "Вы должны указать количество денежных средств")]
        [Range(50, 1000000, ErrorMessage = "Значение должно быть от 50 до 1 000 000")]
        decimal OutputAmount,

        [Required(ErrorMessage = "Выберите тип платежа")]
        string? PaymentMethod,

        decimal WinningSum
    );
}
