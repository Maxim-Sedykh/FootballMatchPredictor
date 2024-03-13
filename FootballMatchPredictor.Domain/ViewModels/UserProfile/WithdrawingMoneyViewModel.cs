using FootballMatchPredictor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.UserProfile
{
    /// <summary>
    /// Модель представления для вывода денег пользователем
    /// </summary>
    /// <param name="OutputAmount"></param>
    /// <param name="PaymentMethod"></param>
    /// <param name="WinningSum"></param>
    public record WithdrawingMoneyViewModel(decimal OutputAmount, PaymentMethod PaymentMethod, decimal WinningSum);
}
