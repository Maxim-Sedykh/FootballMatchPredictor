using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.ViewModels.Bet;
using FootballMatchPredictor.Domain.ViewModels.Error;
using Microsoft.AspNetCore.Mvc;

namespace FootballMatchPredictor.Controllers
{
    public class WithdrawingController : Controller
    {
        private readonly IWithdrawingService _withdrawingService;

        public WithdrawingController(IWithdrawingService withdrawingService)
        {
            _withdrawingService = withdrawingService;
        }

        /// <summary>
        /// Получение списка всех выводов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllWithdrawings()
        {
            var response = await _withdrawingService.GetAllWithdrawings();
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            return View("Error", new ErrorViewModel("Internal server error", 500));
        }

        [HttpPost]
        public JsonResult GetPaymentMethodsToWithDraw()
        {
            var types = _withdrawingService.GetPaymentMethodsToWithdraw();
            return Json(types.Data);
        }

        /// <summary>
        /// Получение модального окна для вывода денег
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> WithdrawingMoney()
        {
            var response = await _withdrawingService.GetUserWinningSum(User.Identity.Name);
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return View("Error", new ErrorViewModel("Internal server error", 500));
        }

        /// <summary>
        /// Вывод денег пользователя
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> WithdrawingMoney(WithdrawingMoneyViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().JoinErrors();
                return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage = errorMessage });
            }

            var response = await _withdrawingService.WithdrawingMoney(viewModel, User.Identity.Name);

            if (response.IsSuccess)
            {
                return Ok(response.SuccessMessage);
            }
            return BadRequest(new { errorMessage = response.ErrorMessage });
        }
    }
}
