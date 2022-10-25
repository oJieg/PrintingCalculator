using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase;
using printing_calculator.ViewModels;

namespace printing_calculator.controllers
{
    public class SettingController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<SettingController> _logger;

        public SettingController(ApplicationContext applicationContext, ILogger<SettingController> logger)
        {
            _applicationContext = applicationContext;
            _logger = logger;
        }

        public IActionResult Paper()
        {
            PaperAndSize paperAndSize = new();
            
            try
            {
                paperAndSize.PaperCatalog = _applicationContext.PaperCatalogs
                    .Include(paper => paper.Size)
                    .AsNoTracking()
                    .ToList();
                paperAndSize.Size = _applicationContext.SizePapers
                    .AsNoTracking()
                    .ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError("error db", ex);
                return NotFound();
            }

            return View("SettingPaper", paperAndSize);
        }

        public IActionResult Lamination()
        {
            return View();
        }
    }
}
