using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.ViewModels.Coefficient;
using FootballMatchPredictor.Domain.ViewModels.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Team
{
    /// <summary>
    /// Модель представления для получения информации по командам
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="TeamName"></param>
    /// <param name="CountryName"></param>
    /// <param name="MatchesPlayed"></param>
    /// <param name="MatchesWon"></param>
    public record TeamViewModel(
         short Id,
         string TeamName,
         string CountryName,
         int MatchesPlayed,
         int MatchesWon
    );
}
