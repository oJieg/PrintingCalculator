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
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<CalculatorResultController> _logger;
        private readonly ConveyorCalculator _calculator;
        private readonly GeneratorHistory _generatorHistory;
        private readonly Validation _validstion;

        public CalculatorResultController(ApplicationContext applicationContext,
            ILogger<CalculatorResultController> loggerFactory,
            ConveyorCalculator conveyorCalculator,
            GeneratorHistory generatorHistory,
            Validation validstion)
        {
            _applicationContext = applicationContext;
            _logger = loggerFactory;
            _calculator = conveyorCalculator;
            _generatorHistory = generatorHistory;
            _validstion = validstion;
        }

        [HttpPost]
        public async Task<IActionResult> Index(Input input, CancellationToken cancellationToken)
        {
            if (!(await _validstion.TryValidationInpytAsync(input, cancellationToken)))
            {
                _logger.LogError("input не прошел валидацию input:{input}", input);
                return BadRequest();
            }

            ConveyorCalculator conveyor = _calculator;
            History? history = await _generatorHistory.GetFullIncludeHistoryAsync(input, cancellationToken);
            if (history == null)
                return NotFound(); //или другой код ошибки

            Result result = new();
            (History, Result, bool) answer = await conveyor.TryStartCalculation(history, result, cancellationToken); //как то странно выглядит но все же
            result = answer.Item2;
            history = answer.Item1;

            if (!answer.Item3)
            {
                _logger.LogError("не удался расчет для данных из Input");
                return NotFound();
            }

            if (!input.SaveDB) //не получилось вынести сохранение после рендера станицы( 
            {
                try
                {
                    _applicationContext.HistoryInputs.Add(history.Input);
                    _applicationContext.Historys.Add(history);

                    await _applicationContext.SaveChangesAsync(cancellationToken);
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
        public async Task<IActionResult> Index(int id, CancellationToken cancellationToken)
        {
            ConveyorCalculator conveyor = _calculator;
            History? history = await _generatorHistory.GetFullIncludeHistoryAsync(id, cancellationToken);
            if (history == null)
                return NotFound(); //или другой код ошибки

            Result result = new();
            var answer = await conveyor.TryStartCalculation(history, result, cancellationToken);
            result = answer.Item2;
            history = answer.Item1;

            if (!answer.Item3)
            {
                _logger.LogError("не удался расчет на конвейере");
                return NotFound();
            }
            result.HistoryInputId = id;

            return View("CalculatorResult", result);
        }
    }
}