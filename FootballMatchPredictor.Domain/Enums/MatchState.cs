using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Enums
{
    public enum MatchState
    {
        [Display(Name = "Ещё не сыигран")]
        NotPlayedYet = 0,

        [Display(Name = "В процессе")]
        InProgress = 1,

        [Display(Name = "Первая команда выиграла")]
        FirstTeamWon = 2,

        [Display(Name = "Вторая команда выиграла")]
        SecondTeamWon = 3,

        [Display(Name = "Ничья")]
        Draw = 4,
    }
}
