using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.ViewModels.Error;
using FootballMatchPredictor.Domain.ViewModels.Team;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FootballMatchPredictor.Domain.Extensions;

namespace FootballMatchPredictor.Controllers
{
    [AllowAnonymous]
    public class TeamController: Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        /// <summary>
        /// Страница с информацией по командами
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAllTeams()
        {
            var response = await _teamService.GetAllTeams();
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            return View("Error", new ErrorViewModel("Internal server error", 500));
        }

        [HttpGet]
        public IActionResult CreateTeam() => PartialView();

        [HttpPost]
        public async Task<IActionResult> CreateTeam(CreateTeamViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().JoinErrors();
                return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage = errorMessage });
            }

            var response = await _teamService.CreateTeam(viewModel);

            if (response.IsSuccess)
            {
                return Ok(response.SuccessMessage);
            }
            return BadRequest(new { errorMessage = response.ErrorMessage });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> GetAllCountries()
        {
            var response = await _teamService.GetAllCountries();
            if (response.IsSuccess)
            {
                return Json(response.Data);
            }
            return Json("Error");
        }
    }
}
