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
        private readonly ConveyorCalculator _calculator;
        private readonly GeneratorHistory _generatorHistory;

        public CalculatorResultController(ApplicationContext DB,
            IOptions<Setting> options,
            ILogger<CalculatorResultController> loggerFactory,
            ConveyorCalculator conveyorCalculator,
            GeneratorHistory generatorHistory)
        {
            _BD = DB;
            _options = options.Value;
            _logger = loggerFactory;
            _calculator = conveyorCalculator;
            _generatorHistory = generatorHistory;
        }

        [HttpPost]
        public async Task<IActionResult> Index(Input input)
        {
            //валидация input
            ConveyorCalculator conveyor = _calculator;
            History? history = await _generatorHistory.GetFullIncludeHistory(input);
            if (history == null)
                return NotFound(); //или другой код ошибки

            bool tryCalculation = conveyor.TryStartCalculation(ref history, out Result result);
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
        public async Task<IActionResult> Index(int id)
        {
            ConveyorCalculator conveyor = _calculator;
            History? history = await _generatorHistory.GetFullIncludeHistory(id);
            if (history == null)
                return NotFound(); //или другой код ошибки

            bool TryCalculatoin = conveyor.TryStartCalculation(ref history, out Result result);
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