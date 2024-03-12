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

        [HttpGet]
        public async Task<IActionResult> MakeBet(long id)
        {
            var response = await _betService.GetDataToMakeBet(id);
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return View("Error", new ErrorViewModel("Internal server error", 500));
        }

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
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToArray();
            string errorMessage = string.Join(" ", errorMessages);
            return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage = errorMessage });
        }
    }
}
