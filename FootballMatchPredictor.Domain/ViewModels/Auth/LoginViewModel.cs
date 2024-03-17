using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Auth
{
    /// <summary>
    /// Модель представления для входа пользователя в аккаунт
    /// </summary>
    /// <param name="Email"></param>
    /// <param name="Password"></param>
    public record LoginViewModel(
        [Required(ErrorMessage = "Введите почту")]
        [EmailAddress(ErrorMessage = "Некорректный формат почты")]
        [MaxLength(20, ErrorMessage = "Почта должна иметь длину меньше 20 символов")]
        string Email,

        [Required(ErrorMessage = "Введите пароль")]
        [MaxLength(30, ErrorMessage = "Пароль должен иметь длину меньше 30 символов")]
        [MinLength(5, ErrorMessage = "Пароль должен иметь длину больше 5 символов")]
        [DataType(DataType.Password)]
        string Password
    );
}
