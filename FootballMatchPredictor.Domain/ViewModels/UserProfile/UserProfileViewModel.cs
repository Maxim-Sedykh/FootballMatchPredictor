using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.ViewModels.Bet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// <param name="Gender"></param>
    public record UserProfileViewModel(
        long Id,

        [Required(ErrorMessage = "Укажите логин")]
        [MaxLength(30, ErrorMessage = "Логин должен иметь длину меньше 30 символов")]
        [MinLength(5, ErrorMessage = "Логин должен иметь длину меньше 5 символов")]
        string Username,

        [Required(ErrorMessage = "Укажите Имя")]
        [MaxLength(30, ErrorMessage = "Имя должно иметь длину меньше 30 символов")]
        [MinLength(5, ErrorMessage = "Имя должно иметь длину меньше 5 символов")]
        string FirstName,

        [Required(ErrorMessage = "Укажите Фамилию")]
        [MaxLength(30, ErrorMessage = "Фамилия должна иметь длину меньше 30 символов")]
        [MinLength(5, ErrorMessage = "Фамилия должна иметь длину меньше 5 символов")]
        string SurName,

        [Required(ErrorMessage = "Введите почту")]
        [EmailAddress(ErrorMessage = "Некорректный формат почты")]
        [MaxLength(20, ErrorMessage = "Почта должна иметь длину меньше 20 символов")]
        string Email,

        [Required(ErrorMessage = "Укажите пол")]
        string Gender
    );
}
