using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Enums
{
    public enum PaymentMethod
    {
        [Display(Name = "Банковская карта")]
        BankCard = 0,

        [Display(Name = "Pay")]
        Pay = 1,

        [Display(Name = "PayPal")]
        PayPal = 2,

        [Display(Name = "Qiwi")]
        Qiwi = 3,

        [Display(Name = "Ваш личный баланс на сайте")]
        UserWinningAmount = 4,
    }
}
