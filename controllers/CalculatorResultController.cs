using Microsoft.AspNetCore.Mvc;
using printing_calculator.ViewModels;
using printing_calculator.Models;
using printing_calculator.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using printing_calculator.Models.Settings;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Calculating;


namespace printing_calculator.controllers
{
    public class CalculatorResultController : Controller
    {
        private ApplicationContext _BD;
        private Setting _options;

        public CalculatorResultController(ApplicationContext DB, IOptions<Setting> options)
        {
            _BD = DB;
            _options = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Index(Input input)
        {
            FillingHistoryImpyt fillingHistory = new(_BD);
            History history =  fillingHistory.GeneratorHistory(input);

            Result result = new();
          //  result.ResultPos = new ResultPos();
            ConveyorCalculator conveyor = new(_options);
            conveyor.TryStartCalculation(ref history, out result);

            if (!input.SaveDB)
            {
                _BD.HistoryInputs.Add(history.Input);
                _BD.Historys.Add(history);

                await _BD.SaveChangesAsync();
                
            }

            return View("CalculatorResult", result);
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            Result result = new();
            FullIncludeHistory fullIncludeHistory = new FullIncludeHistory();
            History histories = fullIncludeHistory.Get(_BD, id);

            ConveyorCalculator conveyor = new(_options);
            conveyor.TryStartCalculation(ref histories, out result);
            result.HistoryInputId = id;
            return View("CalculatorResult", result);
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
