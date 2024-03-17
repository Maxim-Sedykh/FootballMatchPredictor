using FootballMatchPredictor.Application.Services;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.ViewModels.Error;
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
    }
}
