using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Enums
{
    public enum MatchState
    {
        NotPlayedYet = 0,
        InProgress = 1,
        FirstTeamWon = 2,
        SecondTeamWon = 3,
        Draw = 4,
    }
}
