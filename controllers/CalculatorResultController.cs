using Microsoft.AspNetCore.Mvc;
using printing_calculator.ViewModels;
using printing_calculator.Models;
using printing_calculator.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using printing_calculator.Models.markup;

namespace printing_calculator.controllers
{
    public class CalculatorResultController : Controller
    {
        private ApplicationContext _BD;
        private IOptions<Markup> _options;

        public CalculatorResultController(ApplicationContext DB, IOptions<Markup> options)
        {
            _BD = DB;
            _options = options;
        }

        [HttpPost]
        public async Task<IActionResult> Index(Input input)
        {

            GeneratorResult result = new(_options);
            result.Start(input, _BD);

            GeneratorHistory generatorHistory = new(_BD);
            generatorHistory.Start(result.GetResult());

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

            GeneratorResult generatorResult = new(_options);
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
