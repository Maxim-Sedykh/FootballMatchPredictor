using FootballMatchPredictor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Withdrawing
{
    public class WithdrawingViewModel
    {
        public long Id { get; set; }
        public decimal OutputAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
