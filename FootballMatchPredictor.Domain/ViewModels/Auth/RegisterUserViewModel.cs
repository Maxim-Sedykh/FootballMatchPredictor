using System;
using System.Collections.Generic;
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
        string Username,
        string Email,
        string FirstName,
        string SurName,
        string Password,
        string PasswordConfirm
    );
}
