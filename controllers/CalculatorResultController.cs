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
        public async Task<IActionResult>  Index(Input input)
        {
            GeneratorHistory generatorHistory = new(input, _BD);
            GeneratorResult result = new();
            generatorHistory.Start();

            if (!input.SaveDB)
            {
                _BD.HistoryInputs.Add(generatorHistory.GetHistoryInput());
                _BD.Historys.Add(generatorHistory.GetHistory());

                await _BD.SaveChangesAsync();
            }

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

        //[NonAction]
        //public virtual async Task OnActionExecutionAsync()
        //{
            
        //    if (_saveDB)
        //    {
        //        _BD.HistoryInputs.Add(_historyInput);
        //        _BD.Historys.Add(_history);

        //        await _BD.SaveChangesAsync();
        //    }
        //    base.OnActionExecutionAsync();
        //}

    }
}
