using FootballMatchPredictor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Match
{
    public record UpdateMatchViewModel(
        long Id,

        [Required(ErrorMessage = "Укажите первую команду")]
        string Team1,

        [Required(ErrorMessage = "Укажите вторую команду")]
        string Team2,

        byte Team1GoalsCount,

        byte Team2GoalsCount,

        string MatchState,

        [Required(ErrorMessage = "Укажите дату")]
        DateTime MatchDate
    );
}
