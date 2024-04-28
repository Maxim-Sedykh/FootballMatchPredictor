using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.ViewModels.Error;
using FootballMatchPredictor.Domain.ViewModels.Team;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Application.Services;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Result;

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

        public async Task<IActionResult> GetAllTeams()
        {
            return HandleTeamResponse(await _teamService.GetAllTeams());
        }

        public async Task<IActionResult> GetAllTeamsByAdmin()
        {
            return HandleTeamResponse(await _teamService.GetAllTeams());
        }

        private IActionResult HandleTeamResponse(CollectionResult<TeamViewModel> response)
        {
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            return View("Error", new ErrorViewModel("Internal server error", 500));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateTeam() => PartialView();

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTeam(short id)
        {
            var response = await _teamService.GetTeamData(id);
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return View("Error", new ErrorViewModel("Internal server error", 500));
        }

        /// <summary>
        /// Получение модального окна для вывода денег
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteTeam(short id)
        {
            var response = await _teamService.DeleteTeam(id);
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
        public async Task<IActionResult> UpdateTeam(TeamViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().JoinErrors();
                return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage = errorMessage });
            }

            var response = await _teamService.UpdateTeam(viewModel);
            if (response.IsSuccess)
            {
                return Ok(response.SuccessMessage);
            }
            return BadRequest(new { errorMessage = response.ErrorMessage });
        }

        [HttpPost]
        public JsonResult GetTeamDictionary()
        {
            var types = _teamService.GetTeamsDictionary();
            return Json(types.Data);
        }
    }
}
