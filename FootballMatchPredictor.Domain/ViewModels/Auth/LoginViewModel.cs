using System;
using System.Collections.Generic;
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
        string Email,
        string Password
    );
}
