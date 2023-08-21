using Microsoft.AspNetCore.Mvc;
using printing_calculator.ViewModels;
using printing_calculator.Models;
using printing_calculator.DataBase;
using printing_calculator.Models.Calculating;
using System.Globalization;

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

        public async Task<IActionResult> Index(CancellationToken cancellationToken, int page = 1, int countPage = 15)
        {
            if (countPage <= 0 && page <= 0)
            {
                _logger.LogError("ошибка указания номера страницы или длины страницы page-{page}, countPage-{countPage}",
                    page, countPage);
                return NotFound();
            }
            HistoryPage historyPage = new();
            historyPage.ThisPage = page;
            List<SimpleResult> result = new();

            try
            {
                int skipHistory = (page - 1) * countPage;
                List<СalculationHistory> histories = await _generatorHistory.GetHistoryListAsync(skipHistory, countPage, cancellationToken);
                historyPage.CurrentPage = await _generatorHistory.GetCountHistoryAsunc();
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

            historyPage.Results = result;
            historyPage.CurrentPage = (int)Math.Ceiling((float)historyPage.CurrentPage / (float)countPage);
            return View("History", historyPage);
        }

        public async Task<IActionResult> Data(CancellationToken cancellationToken, string data, int page = 1, int countPage = 50)
        {
			if (countPage <= 0 && page <= 0)
			{
				_logger.LogError("ошибка указания номера страницы или длины страницы page-{page}, countPage-{countPage}",
					page, countPage);
				return NotFound();
			}
			HistoryPage historyPage = new();
			historyPage.ThisPage = page;
			List<SimpleResult> result = new();

			try
			{
				int skipHistory = (page - 1) * countPage;
				List<СalculationHistory> histories = await _generatorHistory.GetHistoryListForDataAsync(DateTime.Parse(data, CultureInfo.CreateSpecificCulture("en-US"), DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal), skipHistory, countPage, cancellationToken);
				historyPage.CurrentPage = await _generatorHistory.GetCountHistoryAsunc();
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

			historyPage.Results = result;
			historyPage.CurrentPage = (int)Math.Ceiling((float)historyPage.CurrentPage / (float)countPage);
			return View("History", historyPage);
		}

	}
}