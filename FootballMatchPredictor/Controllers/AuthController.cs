﻿using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FootballMatchPredictor.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Переход на модальное окно логина пользователя (GET)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login() => PartialView();

        /// <summary>
        /// Логин пользователя (POST)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var response = await _authService.Login(model);
            if (response.IsSuccess)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(response.Data));

                return RedirectToAction("Index", "Home");
            }
            return View("Error", $"{response.ErrorMessage}");
        }

        /// <summary>
        /// Переход на страницу для регистрации (GET)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register() => PartialView();

        /// <summary>
        /// Регистрация пользователя (POST)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            var response = await _authService.Register(model);
            if (response.IsSuccess)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(response.Data));

                return RedirectToAction("Index", "Home");
            }
            return View("Error", $"{response.ErrorMessage}");
        }

        /// <summary>
        /// Выход пользователя из аккаунта
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
