using Microsoft.AspNetCore.Mvc;
using printing_calculator.ViewModels;
using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Calculating;
using printing_calculator.Models;

namespace printing_calculator.controllers
{
    public class CalculatorResultController : Controller
    {
        private readonly ApplicationContext _BD;
        private readonly ILogger<CalculatorResultController> _logger;
        private readonly ConveyorCalculator _calculator;
        private readonly GeneratorHistory _generatorHistory;
        private readonly Validstion _validstion;

        public CalculatorResultController(ApplicationContext DB,
            ILogger<CalculatorResultController> loggerFactory,
            ConveyorCalculator conveyorCalculator,
            GeneratorHistory generatorHistory,
            Validstion validstion)
        {
            _BD = DB;
            _logger = loggerFactory;
            _calculator = conveyorCalculator;
            _generatorHistory = generatorHistory;
            _validstion = validstion;
        }

        [HttpPost]
        public async Task<IActionResult> Index(Input input)
        {
            if(!(await _validstion.TryValidationInpytAsync(input)))
            {
                _logger.LogError("input не прошел валидацию input:{input}", input);
                return BadRequest();
            }

            ConveyorCalculator conveyor = _calculator;
            History? history = await _generatorHistory.GetFullIncludeHistoryAsync(input);
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
                    _logger.LogError(ex, "не удалось сохранить просчет");
                }
            }

            return View("CalculatorResult", result);
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            ConveyorCalculator conveyor = _calculator;
            History? history = await _generatorHistory.GetFullIncludeHistoryAcync(id);
            if (history == null)
                return NotFound(); //или другой код ошибки

            bool TryCalculatoin = conveyor.TryStartCalculation(ref history, out Result result);
            if (!TryCalculatoin)
            {
                _logger.LogError("не удался расчет на конвеере");
                return NotFound();
            }
            result.HistoryInputId = id;

            return View("CalculatorResult", result);
        }
    }
}