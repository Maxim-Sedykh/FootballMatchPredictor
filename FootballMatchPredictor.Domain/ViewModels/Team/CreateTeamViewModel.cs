﻿using FootballMatchPredictor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Team
{
    public record CreateTeamViewModel(
        long Id,
        string Username,
        string FirstName,
        string SurName,
        string Email,
        Sex Sex
    );
}
