using Microsoft.AspNetCore.Mvc;
using printing_calculator.DataBase;

namespace printing_calculator.controllers
{
    public class HomesController : Controller
    {
        private readonly ILogger<HomesController> _logger;

        public HomesController(ILogger<HomesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogTrace("Home");
            return View("Page");
        }

        public IActionResult Changelog()
        {
            return View("Chengelog");
        }
    }
}