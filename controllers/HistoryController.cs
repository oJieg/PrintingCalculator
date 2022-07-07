using Microsoft.AspNetCore.Mvc;
using printing_calculator.ViewModels;
using printing_calculator.Models;
using printing_calculator.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace printing_calculator.controllers
{
    public class HistoryController : Controller
    {
        private ApplicationContext _BD;
        public HistoryController(ApplicationContext context)
        {
            _BD = context;
        }

        public IActionResult Index(int page, int countPage = 10)
        {
            List<Result> result = new();
            FullIncludeHistory fullIncludeHistory = new();
            List<History> histories = fullIncludeHistory.GetList(_BD, page, countPage);

            GeneratorResult generatorResult = new();
            foreach (History history in histories)
            {
                generatorResult.Start(history);
                result.Add(generatorResult.GetResult());
            }

            return View("History", result);
        }
    }
}
