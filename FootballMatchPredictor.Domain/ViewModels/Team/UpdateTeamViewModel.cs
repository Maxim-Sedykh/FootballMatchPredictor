using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Team
{
    public record UpdateTeamViewModel(
            short Id,
            string Name,
            int MatchesPlayed,
            int MatchesWon,
            string Country,
            DateTime CreatedAt
        );
}
