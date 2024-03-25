using FootballMatchPredictor.Domain.ViewModels.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FootballMatchPredictor.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// ������� ��������
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        /// <summary>
        /// �������� ��� �������������� ���������� �� ������
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}
