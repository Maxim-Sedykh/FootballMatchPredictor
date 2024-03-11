using FootballMatchPredictor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.UserProfile
{
    public record UserProfileViewModel(
        long Id,
        string Username,
        string FirstName,
        string SurName,
        string Email,
        Sex Sex
    );
}
