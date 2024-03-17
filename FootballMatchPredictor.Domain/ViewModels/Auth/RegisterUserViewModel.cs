using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Auth
{
    /// <summary>
    /// Модель представления для регистрации пользователя
    /// </summary>
    /// <param name="Username"></param>
    /// <param name="Email"></param>
    /// <param name="Password"></param>
    public record RegisterUserViewModel(

        [Required(ErrorMessage = "Укажите логин")]
        [MaxLength(30, ErrorMessage = "Логин должен иметь длину меньше 30 символов")]
        [MinLength(5, ErrorMessage = "Логин должен иметь длину меньше 5 символов")]
        string Username,

        [Required(ErrorMessage = "Введите почту")]
        [EmailAddress(ErrorMessage = "Некорректный формат почты")]
        [MaxLength(20, ErrorMessage = "Почта должна иметь длину меньше 20 символов")]
        string Email,

        [Required(ErrorMessage = "Укажите Имя")]
        [MaxLength(30, ErrorMessage = "Имя должно иметь длину меньше 30 символов")]
        [MinLength(2, ErrorMessage = "Имя должно иметь длину меньше 2 символов")]
        string FirstName,

        [Required(ErrorMessage = "Укажите Фамилию")]
        [MaxLength(30, ErrorMessage = "Фамилия должна иметь длину меньше 30 символов")]
        [MinLength(2, ErrorMessage = "Фамилия должна иметь длину меньше 2 символов")]
        string SurName,

        [Required(ErrorMessage = "Введите пароль")]
        [MaxLength(30, ErrorMessage = "Пароль должен иметь длину меньше 30 символов")]
        [MinLength(5, ErrorMessage = "Пароль должен иметь длину больше 5 символов")]
        [DataType(DataType.Password)]
        string Password,

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Подтвердите пароль")]
        string PasswordConfirm
    );
}
