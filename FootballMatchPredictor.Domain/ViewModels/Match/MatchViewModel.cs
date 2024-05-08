using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.ViewModels.Coefficient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Match
{
    /// <summary>
    /// Модель представления для предоставления информации по матчам
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Team1Name"></param>
    /// <param name="Team2Name"></param>
    /// <param name="Team1GoalsCount"></param>
    /// <param name="Team2GoalsCount"></param>
    /// <param name="MatchState"></param>
    /// <param name="MatchDate"></param>
    public record MatchViewModel(
        long Id,
        string Team1Name,
        string Team2Name,
        byte Team1GoalsCount,
        byte Team2GoalsCount,
        float Team1WinRate,
        float Team2WinRate,
        float DrawProbability,
        string MatchState,
        DateTime MatchDate
    );
}
