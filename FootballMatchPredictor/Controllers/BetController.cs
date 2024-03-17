using FootballMatchPredictor.Application.Services;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.ViewModels.Bet;
using FootballMatchPredictor.Domain.ViewModels.Error;
using Microsoft.AspNetCore.Mvc;

namespace FootballMatchPredictor.Controllers
{
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
            if (ModelState.IsValid)
            {
                var response = await _betService.MakeBet(viewModel, User.Identity.Name);
                if (response.IsSuccess)
                {
                    return Ok(response.SuccessMessage);
                }
                return BadRequest(new { errorMessage = response.ErrorMessage });
            }
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().JoinErrors();
            return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
        }

        /// <summary>
        /// Получение модального окна для вывода денег
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> WithdrawingMoney()
        {
            var response = await _betService.GetUserWinningSum(User.Identity.Name);
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
            if (ModelState.IsValid)
            {
                var response = await _betService.WithdrawingMoney(viewModel, User.Identity.Name);
                if (response.IsSuccess)
                {
                    return Ok(response.SuccessMessage);
                }
                return BadRequest(new { errorMessage = response.ErrorMessage });
            }
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().JoinErrors();
            return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
        }

        [HttpPost]
        public JsonResult GetPaymentMethods()
        {
            var types = _betService.GetPaymentMethods();
            return Json(types.Data);
        }
    }
}
