using Microsoft.AspNetCore.Mvc;

namespace FootballMatchPredictor.Controllers
{
    public class BetController: Controller
    {
        public IActionResult MakeBet() => PartialView();
    }
}
