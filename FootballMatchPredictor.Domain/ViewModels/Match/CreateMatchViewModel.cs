﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Match
{
    public record CreateMatchViewModel(
        short Team1Id,
        short Team2Id,
        DateTime MatchDate
    );
}
