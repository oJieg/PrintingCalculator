using Microsoft.AspNetCore.Mvc;
using printing_calculator.DataBase;
using printing_calculator.DataBase.setting;

namespace printing_calculator.controllers
{
    public class HomesController : Controller
    {
        private readonly ILogger<HomesController> _logger;

        public HomesController(ILogger<HomesController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index", "Calculator");

        }

        public IActionResult Changelog()
        {
            return View("Chengelog");
        }
    }
}