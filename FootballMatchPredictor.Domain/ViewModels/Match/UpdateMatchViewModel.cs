using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Match
{
    public record UpdateMatchViewModel(
        long Id,
        byte Team1GoalsCount,
        byte Team2GoalsCount,
        string MatchState,
        DateTime MatchDate
    );
}
