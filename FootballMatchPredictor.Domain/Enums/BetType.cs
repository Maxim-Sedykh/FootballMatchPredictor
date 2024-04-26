using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Enums
{
    public enum BetType
    {
        [Display(Name = "Победа первой команды")]
        FirstTeamWon = 0,

        [Display(Name = "Победа второй команды")]
        SecondTeamWon = 1,

        [Display(Name = "Ничья")]
        Draw = 2
    }
}
