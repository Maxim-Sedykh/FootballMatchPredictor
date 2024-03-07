using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Interfaces.Services
{
    /// <summary>
    /// Сервис отвечающий за работу с авторизацией и аутентификацией c помощью Cookie
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<BaseResult<ClaimsIdentity>> Register(RegisterUserViewModel viewModel);

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<BaseResult<ClaimsIdentity>> Login(LoginViewModel viewModel);
    }
}
