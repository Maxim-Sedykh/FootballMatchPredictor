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

    public class TeamViewModel
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string? CountryName { get; set; }

        public double Rating { get; set; }
        public int MatchesPlayed { get; set; }
        public int MatchesWon { get; set; }
    }
}
