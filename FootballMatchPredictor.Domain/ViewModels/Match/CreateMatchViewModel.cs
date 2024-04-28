using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Match
{
    public record CreateMatchViewModel(
        [Required(ErrorMessage = "Укажите первую команду")]
        short Team1Id,

        [Required(ErrorMessage = "Укажите вторую команду")]
        short Team2Id,

        [Required(ErrorMessage = "Укажите дату матча")]
        DateTime MatchDate
    );
}
