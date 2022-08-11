using Microsoft.AspNetCore.Mvc;
using printing_calculator.ViewModels;
using printing_calculator.Models;
using printing_calculator.DataBase;
using printing_calculator.Models.Calculating;

namespace printing_calculator.controllers
{
    public class HistoryController : Controller
    {
        private readonly ILogger<HistoryController> _logger;
        private readonly GeneratorHistory _generatorHistory;

        public HistoryController(ILogger<HistoryController> logger,
            GeneratorHistory generatorHistory)
        {
            _logger = logger;
            _generatorHistory = generatorHistory;
        }

        public async Task<IActionResult> Index(int page, int countPage = 10)
        {
            if (!ValidationPage(page, countPage))
            {
                _logger.LogError("ошибка указания номера страницы или длины страницы page-{page}, countPage-{countPage}",
                    page, countPage);
                return NotFound();
            }
            List<SimplResult> result = new();

            try
            {
                List<History> histories = await _generatorHistory.GetListAsync(page, countPage);

                foreach (History history in histories)
                {
                    result.Add(Converter.HistoryToSimplResult(history));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ошибка получения данных для  HistoryController}");
                return NotFound();
            }

            return View("History", result);
        }

        private static bool ValidationPage(int page, int countPage)
        {
            if (countPage <= 0)
                return false;

            if (page < 0)
                return false;

            return true;
        }
    }
}