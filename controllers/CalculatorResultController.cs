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
        private readonly Validation _validation;

        public CalculatorResultController(ApplicationContext applicationContext,
            ILogger<CalculatorResultController> loggerFactory,
            ConveyorCalculator conveyorCalculator,
            GeneratorHistory generatorHistory,
            Validation validation)
        {
            _applicationContext = applicationContext;
            _logger = loggerFactory;
            _calculator = conveyorCalculator;
            _generatorHistory = generatorHistory;
            _validation = validation;
        }

        [HttpPost]
        public async Task<IActionResult> Index(Input input, CancellationToken cancellationToken)
        {
            if (!(await _validation.TryValidationInpytAsync(input, cancellationToken)))
            {
                _logger.LogError("input не прошел валидацию input:{input}", input);
                return BadRequest();
            }

            History? history = await _generatorHistory.GetFullIncludeHistoryAsync(input, cancellationToken);
            if (history == null)
                return NotFound(); //или другой код ошибки

            Result result = new();

            try
            {
                ConveyorCalculator conveyor = _calculator;

                (history, result, bool tryAnswer) = await conveyor.TryStartCalculation(history, result, cancellationToken); //как то странно выглядит но все же

                if (!tryAnswer)
                {
                    _logger.LogError("не удался расчет для данных из Input");
                    return NotFound();
                }
            }
            catch (OperationCanceledException)
            {
                return new EmptyResult();
            }

            if (!input.SaveDB)
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
            try
            {
                (history, result, bool tryAnswer) = await conveyor.TryStartCalculation(history, result, cancellationToken);

                if (!tryAnswer)
                {
                    _logger.LogError("не удался расчет на конвейере");
                    return NotFound();
                }
            }
            catch (OperationCanceledException)
            {
                return new EmptyResult();
            }

            result.HistoryInputId = id;

            return View("CalculatorResult", result);
        }
    }
}