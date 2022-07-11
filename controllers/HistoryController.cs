using Microsoft.AspNetCore.Mvc;
using printing_calculator.ViewModels;
using printing_calculator.Models;
using printing_calculator.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Options;
using printing_calculator.Models.Calculating;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.controllers
{
    public class HistoryController : Controller
    {
        private ApplicationContext _BD;
        private IOptions<Settings> _options;
        public HistoryController(ApplicationContext context, IOptions<Settings> options)
        {
            _BD = context;
            _options = options;
        }

        public IActionResult Index(int page, int countPage = 10)
        {
            List<Result> result = new();
            FullIncludeHistory fullIncludeHistory = new();
            List<History> histories = fullIncludeHistory.GetList(_BD, page, countPage);

            ConveyorCalculator conveyor = new(_options);
            foreach (History history in histories)  //наименование поменяй на человеческие!!!!
            {
                History history1 = history;
                Result result1 = new();
                conveyor.TryStartCalculation(ref history1, out result1);
                
                result.Add(result1);
            }

            return View("History", result);
        }
    }
}
