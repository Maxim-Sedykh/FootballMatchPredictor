using FootballMatchPredictor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Match
{
    public record CreateMatchViewModel(
        string Team1Id,
        string Team2Id,
        DateTime MatchDate
    );
}
