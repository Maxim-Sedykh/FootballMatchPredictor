﻿using FootballMatchPredictor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Team
{
    public record TeamViewModel(
        short Id,
        string Name,
        string Country
    );
}
