using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Team
{
    public record CreateTeamViewModel(

        [Required(ErrorMessage = "Укажите имя команды")]
        string TeamName,

        [Required(ErrorMessage = "Укажите страну")]
        int CountryIdentifier
    );
}
