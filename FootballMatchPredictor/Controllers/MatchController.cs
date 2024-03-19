using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.ViewModels.Error;
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

        /// <summary>
        /// Получение всех матчей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMatches()
        {
            var response = await _matchService.GetAllMatches();
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

    }
}
