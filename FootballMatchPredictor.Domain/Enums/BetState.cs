using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Enums
{
    public enum BetState
    {
        [Display(Name = "Выигрышный")]
        Winning = 0,

        [Display(Name = "Проигрышный")]
        Losing = 1,

        [Display(Name = "Неизвестно")]
        Unknown = 2,
    }
}
