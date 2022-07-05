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
            FullIncludeHistory fullIncludeHistory = new ();
            List<History> histories = fullIncludeHistory.GetList(_BD, page, countPage);

            GeneratorResult generatorResult = new();
            for (int i = 0; i < countPage; i++)
            {
                generatorResult.Start(histories[i]);
                result.Add(generatorResult.GetResult());
            }
            
            return View("History", result);
        }
    }
}
