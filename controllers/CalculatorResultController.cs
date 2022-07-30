using Microsoft.AspNetCore.Mvc;
using printing_calculator.ViewModels;
using printing_calculator.Models;
using printing_calculator.DataBase;
using Microsoft.Extensions.Options;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Calculating;

namespace printing_calculator.controllers
{
    public class CalculatorResultController : Controller
    {
        private readonly ApplicationContext _BD;
        private readonly Setting _options;
        private readonly ILogger<CalculatorResultController> _logger;
        private readonly ILogger<ConveyorCalculator> _loggerConveyor;
        public CalculatorResultController(ApplicationContext DB,
            IOptions<Setting> options, ILoggerFactory loggerFactory)
        {
            _BD = DB;
            _options = options.Value;
            _logger = loggerFactory.CreateLogger<CalculatorResultController>();
            _loggerConveyor = loggerFactory.CreateLogger<ConveyorCalculator>();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Input input)
        {
            //валидация input

            ConveyorCalculator conveyor = new(_options, _BD, _loggerConveyor);

            bool tryCalculation = conveyor.TryStartCalculation(input, out History history, out Result result);
            if (!tryCalculation)
            {
                _logger.LogError("не удался расчет для данных из Input");
                return NotFound();
            }

            if (!input.SaveDB) //не получилось вынести сохранение после рендера станицы( 
            {
                try
                {
                    _BD.HistoryInputs.Add(history.Input);
                    _BD.Historys.Add(history);

                    await _BD.SaveChangesAsync();
                    result.HistoryInputId = history.Id;
                }
                catch (Exception ex)
                {
                    _logger.LogError("не удалось сохранить просчет {ex}", ex);
                }
            }

            return View("CalculatorResult", result);
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            ConveyorCalculator conveyor = new(_options, _BD, _loggerConveyor);

            bool TryCalculatoin = conveyor.TryStartCalculation(id, out Result result);
            if (!TryCalculatoin)
            {
                _logger.LogError("не удался расчет, возможно не верный id");
                return NotFound();
            }
            result.HistoryInputId = id;

            return View("CalculatorResult", result);
        }
    }
}
