using FootballMatchPredictor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.User
{
    public record UserViewModel(
            long Id,
            string Username,
            string FirstName,
            string SurName,
            string Gender,
            string Email,
            string Role,
            decimal WinningSum,
            DateTime CreatedAt
        );
}
