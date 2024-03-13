using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Match
{
    /// <summary>
    /// Модель представления для предоставления разных матчей пользователю на странице
    /// </summary>
    /// <param name="liveMathes"></param>
    /// <param name="notPlayedMathes"></param>
    /// <param name="playedMathes"></param>
    public record MatchPageViewModel(
        List<MatchViewModel> liveMathes,
        List<MatchViewModel> notPlayedMathes,
        List<MatchViewModel> playedMathes
    );
}
