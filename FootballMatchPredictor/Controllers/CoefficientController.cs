using FootballMatchPredictor.Application.Services;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.ViewModels.Coefficient;
using FootballMatchPredictor.Domain.ViewModels.Error;
using FootballMatchPredictor.Domain.ViewModels.Team;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballMatchPredictor.Controllers
{
    public class CoefficientController: Controller
    {
        private readonly ICoefficientService _coefficientService;

        public CoefficientController(ICoefficientService coefficientService)
        {
            _coefficientService = coefficientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMatchCoefficients(long id)
        {
            var response = await _coefficientService.GetMatchCoefficients(id);
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            return View("Error", new ErrorViewModel("Internal server error", 500));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoefficients()
        {
            var response = await _coefficientService.GetAllCoefficients();
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            return View("Error", new ErrorViewModel("Internal server error", 500));
        }

        [HttpGet]
        public async Task<IActionResult> GetCoefficient(long id)
        {
            var response = await _coefficientService.GetCoefficientById(id);
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return View("Error", new ErrorViewModel("Internal server error", 500));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCoefficient(long id)
        {
            var response = await _coefficientService.DeleteCoefficientById(id);
            if (response.IsSuccess)
            {
                return Ok(response.SuccessMessage);
            }
            return BadRequest(new { errorMessage = response.ErrorMessage });
        }

        /// <summary>
        /// Получение модального окна для вывода денег
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateCoefficient(UpdateCoefficientViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().JoinErrors();
                return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage = errorMessage });
            }

            var response = await _coefficientService.UpdateCoefficient(viewModel);
            if (response.IsSuccess)
            {
                return Ok(response.SuccessMessage);
            }
            return BadRequest(new { errorMessage = response.ErrorMessage });
        }
    }
}
