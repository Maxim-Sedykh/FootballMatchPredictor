using FootballMatchPredictor.Application.Services;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Error;
using FootballMatchPredictor.Domain.ViewModels.Match;
using FootballMatchPredictor.Domain.ViewModels.Team;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballMatchPredictor.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<IActionResult> GetMatches()
        {
            return HandleTeamResponse(await _matchService.GetAllMatches());
        }

        public async Task<IActionResult> GetMatchesByAdmin()
        {
            return HandleTeamResponse(await _matchService.GetAllMatches());
        }

        private IActionResult HandleTeamResponse(CollectionResult<MatchViewModel> response)
        {
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            return View("Error", new ErrorViewModel("Internal server error", 500));
        }

        /// <summary>
        /// Получение модального окна с матчами определённых команд
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTeamMatches(long id)
        {
            var response = await _matchService.GetTeamMatches(id);
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return View("Error", new ErrorViewModel("Internal server error", 500));
        }

        [HttpGet]
        public IActionResult CreateMatch() => PartialView();

        [HttpPost]
        public async Task<IActionResult> CreateMatch(CreateMatchViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().JoinErrors();
                return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage = errorMessage });
            }

            var response = await _matchService.CreateMatch(viewModel);

            if (response.IsSuccess)
            {
                return Ok(response.SuccessMessage);
            }
            return BadRequest(new { errorMessage = response.ErrorMessage });
        }

        [HttpPost]
        public JsonResult GetMatchStates()
        {
            var types = _matchService.GetMatchState();
            return Json(types.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetMatch(long id)
        {
            var response = await _matchService.GetMatch(id);
            if (response.IsSuccess)
            {
                return PartialView(response.Data);
            }
            return View("Error", new ErrorViewModel("Internal server error", 500));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTeam(long id)
        {
            var response = await _matchService.DeleteMatch(id);
            if (response.IsSuccess)
            {
                return Ok(response.SuccessMessage);
            }
            return BadRequest(new { errorMessage = response.ErrorMessage });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeam(UpdateMatchViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().JoinErrors();
                return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage = errorMessage });
            }

            var response = await _matchService.UpdateMatch(viewModel);
            if (response.IsSuccess)
            {
                return Ok(response.SuccessMessage);
            }
            return BadRequest(new { errorMessage = response.ErrorMessage });
        }
    }
}
