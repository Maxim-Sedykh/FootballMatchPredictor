using FootballMatchPredictor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Match
{
    public record UpdateMatchViewModel(
        long Id,
        string Team1,
        string Team2,
        byte Team1GoalsCount,
        byte Team2GoalsCount,
        string MatchState,
        DateTime MatchDate
    );
}
