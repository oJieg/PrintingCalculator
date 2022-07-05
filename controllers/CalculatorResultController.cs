using Microsoft.AspNetCore.Mvc;
using printing_calculator.ViewModels;
using printing_calculator.Models;
using printing_calculator.DataBase;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.controllers
{
    public class CalculatorResultController : Controller
    {
        private ApplicationContext _BD;
        public CalculatorResultController(ApplicationContext DB)
        {
            _BD = DB;
        }

        [HttpPost]
        public async Task<IActionResult> Index(Input input)
        {
            GeneratorHistory generatorHistory = new(input, _BD);
            generatorHistory.Start();
            _BD.HistoryInputs.Add(generatorHistory.GetHistoryInput());
            _BD.Historys.Add(generatorHistory.GetHistory());

            await _BD.SaveChangesAsync();
            GeneratorResult result = new();
            result.Start(generatorHistory.GetHistory());

            return View("CalculatorResult", result.GetResult());
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            Result result = new();
            FullIncludeHistory fullIncludeHistory = new FullIncludeHistory();
            History histories = fullIncludeHistory.Get(_BD, id);

            GeneratorResult generatorResult = new();
            generatorResult.Start(histories);

            return View("CalculatorResult", generatorResult.GetResult());
        }

    }
}
