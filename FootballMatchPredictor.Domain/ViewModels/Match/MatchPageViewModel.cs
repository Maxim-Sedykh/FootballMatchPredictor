using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Match
{
    public record MatchPageViewModel(
        List<MatchViewModel> liveMathes,
        List<MatchViewModel> notPlayedMathes,
        List<MatchViewModel> playedMathes
    );
}
