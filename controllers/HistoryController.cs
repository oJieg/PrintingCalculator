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

        public async Task<IActionResult> Index(int page, CancellationToken cancellationToken, int countPage = 10)
        {
            if (countPage <= 0 && page < 0)
            {
                _logger.LogError("ошибка указания номера страницы или длины страницы page-{page}, countPage-{countPage}",
                    page, countPage);
                return NotFound();
            }
            List<SimpleResult> result = new();

            try
            {
                List<СalculationHistory> histories = await _generatorHistory.GetHistoryListAsync(page, countPage, cancellationToken);

                foreach (СalculationHistory history in histories)
                {
                    result.Add(Converter.HistoryToSimplResult(history));
                }
            }
            catch (OperationCanceledException)
            {
                return new EmptyResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ошибка получения данных для  HistoryController}");
                return NotFound();
            }

            return View("History", result);
        }
    }
}