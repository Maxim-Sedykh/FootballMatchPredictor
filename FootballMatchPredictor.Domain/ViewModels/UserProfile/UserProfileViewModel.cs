using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.ViewModels.Bet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.UserProfile
{
    /// <summary>
    /// Модель представления для получения информации для страницы профиля пользователя
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Username"></param>
    /// <param name="FirstName"></param>
    /// <param name="SurName"></param>
    /// <param name="Email"></param>
    /// <param name="Sex"></param>
    /// <param name="UserBets"></param>
    public record UserProfileViewModel(
        long Id,
        string Username,
        string FirstName,
        string SurName,
        string Email,
        Sex Sex,
        List<BetViewModel>? UserBets
    );
}
