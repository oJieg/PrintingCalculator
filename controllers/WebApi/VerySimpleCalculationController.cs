using Microsoft.AspNetCore.Mvc;
using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Calculating;
using printing_calculator.Models;
using printing_calculator.ViewModels;


namespace printing_calculator.controllers.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerySimpleCalculationController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<CalculatorResultController> _logger;
        private readonly ConveyorCalculator _calculator;
        private readonly GeneratorHistory _generatorHistory;

        public VerySimpleCalculationController(ApplicationContext applicationContext,
            ILogger<CalculatorResultController> loggerFactory,
            ConveyorCalculator conveyorCalculator,
            GeneratorHistory generatorHistory,
            Validation validation)
        {
            _applicationContext = applicationContext;
            _logger = loggerFactory;
            _calculator = conveyorCalculator;
            _generatorHistory = generatorHistory;
        }

        [HttpGet]
        [Route("{historyId}/{newAmount}")]
        public async Task<ActionResult<int>> Recalculation(int historyId, int newAmount, CancellationToken cancellationToken)
        {
            СalculationHistory history;

            Result result;
            (history, result, bool tryAnswer) = await _calculator.TryStartCalculation(historyId, newAmount, cancellationToken);

            if (!tryAnswer)
            {
                _logger.LogError("не удался расчет для данных из Input");
                return -1;
            }

            try
            {
                _applicationContext.InputsHistories.Add(history.Input);
                _applicationContext.Histories.Add(history);

                await _applicationContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "не удалось сохранить просчет");
            }

            return result.Price;
        }
    }
}
