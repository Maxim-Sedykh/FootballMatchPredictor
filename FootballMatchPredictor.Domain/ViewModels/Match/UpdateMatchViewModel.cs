using FootballMatchPredictor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Match
{
    public record UpdateMatchViewModel(
            string Team1Id,
            string Team2Id,
            byte Team1GoalsCount,
            byte Team2GoalsCount,
            MatchState MatchState,
            DateTime MatchDate
        );
}
