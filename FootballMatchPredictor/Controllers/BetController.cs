using FootballMatchPredictor.Application.Services;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.ViewModels.Bet;
using FootballMatchPredictor.Domain.ViewModels.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FootballMatchPredictor.Controllers
{
    [Authorize]
    public class BetController: Controller
    {
        private readonly IBetService _betService;

        public BetController(IBetService betService)
        {
            _betService = betService;
        }

        /// <summary>
        /// Модальное окно для того, чтобы сделать ставку
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> MakeBet(long id)
        {
            var response = await _betService.GetCoefficientToMakeBet(id);
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return View("Error", new ErrorViewModel("Internal server error", 500));
        }

        /// <summary>
        /// Модальное окно для того, чтобы сделать ставку
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserBets()
        {
            var response = await _betService.GetUserBets(User.Identity.Name);
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return View("Error", new ErrorViewModel("Internal server error", 500));
        }

        /// <summary>
        /// Сделать ставку
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> MakeBet(MakeBetViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().JoinErrors();
                return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage = errorMessage });
            }

            var response = await _betService.MakeBet(viewModel, User.Identity.Name);

            if (response.IsSuccess)
            {
                return Ok(response.SuccessMessage);
            }
            return BadRequest(new { errorMessage = response.ErrorMessage });
        }

        [HttpPost]
        public JsonResult GetPaymentMethodsToBet()
        {
           var types = _betService.GetPaymentMethodsToBet();
            return Json(types.Data);
        }

        [HttpPost]
        public JsonResult GetBetTypes()
        {
            var betTypes = _betService.GetBetTypes();
            return Json(betTypes.Data);
        }

        /// <summary>
        /// Получение списка всех выводов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllBets()
        {
            var response = await _betService.GetAllBets();
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            return View("Error", new ErrorViewModel("Internal server error", 500));
        }
    }
}
