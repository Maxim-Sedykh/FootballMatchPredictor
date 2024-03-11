using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.ViewModels.Error;
using FootballMatchPredictor.Domain.ViewModels.UserProfile;

namespace FootballMatchPredictor.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        /// <summary>
        /// Получение данных профиля пользователя, переход на профиль пользователя 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserProfile()
        {
            var response = await _userProfileService.GetUserProfile(User.Identity.Name);
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            return View("Error", new ErrorViewModel("Internal server error", 500));
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateUserInfo(UserProfileViewModel viewModel)
        {
            var response = await _userProfileService.UpdateUserInfo(viewModel);
            if (response.IsSuccess)
            {
                return Ok(response.SuccessMessage);
            }
            return BadRequest(response.ErrorMessage);
        }
    }
}
