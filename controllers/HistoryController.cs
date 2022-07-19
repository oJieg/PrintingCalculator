using Microsoft.AspNetCore.Mvc;
using printing_calculator.ViewModels;
using printing_calculator.Models;
using printing_calculator.DataBase;
using Microsoft.Extensions.Options;

namespace printing_calculator.controllers
{
    public class HistoryController : Controller
    {
        private readonly ApplicationContext _BD;
        private readonly ILogger<HistoryController> _logger;
        public HistoryController(ApplicationContext context, ILogger<HistoryController> logger)
        {
            _BD = context;
            _logger = logger;
        }

        public IActionResult Index(int page, int countPage = 10)
        {
            if (!ValidationPage(page, countPage))
            {
                _logger.LogError("ошибка указания номера страницы или длины страницы page-{page}, countPage-{countPage}",
                    page, countPage);
                return NotFound();
            }
            List<SimplResult> result = new();
            FullIncludeHistory fullIncludeHistory = new();

            try
            {
                List<History> histories = fullIncludeHistory.GetList(_BD, page, countPage);

                foreach (History history in histories)
                {
                    result.Add(HistoryToSimplResult(history));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ошибка получения данных для  HistoryController, {ex}", ex);
                return NotFound();
            }

            return View("History", result);
        }

        private static bool ValidationPage(int page, int countPage)
        {
            if (countPage <= 0)
                return false;

            if (page <= 0)
                return false;

            return true;
        }

        private static SimplResult HistoryToSimplResult(History history)
        {
            SimplResult result = new();
            result.HistoryId = history.Id;
            result.Whidth = history.Input.Whidth;
            result.Height = history.Input.Height;
            result.Amount = history.Input.Amount;
            result.Kinds = history.Input.Kinds;
            result.PaperName = history.Input.Paper.Name;
            if (history.Input.Lamination == null)
            {
                result.Lamination = false;
            }
            else
            {
                result.Lamination = true;
            }

            if (history.CreasingPrice > 0)
            {
                result.Creasing = true;
            }
            else
            {
                result.Creasing = false;
            }

            if (history.DrillingPrice > 0)
            {
                result.Drilling = true;
            }
            else
            {
                result.Drilling = false;
            }

            if (history.RoundingPrice > 0)
            {
                result.Drilling = true;
            }
            else
            {
                result.Drilling = false;
            }

            result.Price = (int)history.Price;
            return result;
        }
    }
}
