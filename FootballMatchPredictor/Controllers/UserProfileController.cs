using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.ViewModels.Error;
using FootballMatchPredictor.Domain.ViewModels.UserProfile;
using FootballMatchPredictor.Application.Services;
using FootballMatchPredictor.Domain.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace FootballMatchPredictor.Controllers
{
    [Authorize]
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
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().JoinErrors();
                return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage = errorMessage });
            }

            var response = await _userProfileService.UpdateUserInfo(viewModel);

            if (response.IsSuccess)
            {
                return Ok(response.SuccessMessage);
            }
            return BadRequest(new { errorMessage = response.ErrorMessage });
        }

        /// <summary>
        /// Получение статистики пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserStatistics()
        {
            var response = await _userProfileService.GetUserStatistics(User.Identity.Name);
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return BadRequest(response.ErrorMessage);
        }

        [HttpPost]
        public JsonResult GetGenders()
        {
            var types = _userProfileService.GetGenders();
            return Json(types.Data);
        }
    }
}