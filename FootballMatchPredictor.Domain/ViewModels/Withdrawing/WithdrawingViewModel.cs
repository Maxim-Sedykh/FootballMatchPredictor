using FootballMatchPredictor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Withdrawing
{
    public record WithdrawingViewModel(
            long Id,
            decimal OutputAmount,
            string PaymentMethod,
            string UserName,
            DateTime CreatedAt,
            DateTime UpdatedAt
        );
}
