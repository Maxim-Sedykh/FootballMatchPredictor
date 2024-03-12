﻿using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.ViewModels.Coefficient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Match
{
    public record MatchViewModel(
            long Id,
            string Team1Name,
            string Team2Name,
            byte Team1GoalsCount,
            byte Team2GoalsCount,
            string MatchState,
            DateTime MatchDate,
            List<MatchCoefficientViewModel> Coefficients
        );
}
