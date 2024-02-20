using printing_calculator.Models.Calculating;
using printing_calculator.ViewModels;
using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models;
using Microsoft.AspNetCore.Mvc;

namespace printing_calculator.controllers.WebApi
{
    [ApiController]
    public class GetResultController
    {
        private readonly ILogger<CalculatorResultController> _logger;
        private readonly ConveyorCalculator _calculator;

        public GetResultController(ILogger<CalculatorResultController> loggerFactory,
            ConveyorCalculator conveyorCalculator)
        {
            _logger = loggerFactory;
            _calculator = conveyorCalculator;
        }

        [HttpGet("api/get-result{id}")]
        public async Task<ApiResultAnswer> GetResult(int id)
        {
            СalculationHistory? history;
            Result result;
            try
            {
                (history, result, StatusCalculation tryAnswer) = await _calculator.TryStartCalculation(id, new CancellationToken());

                if (tryAnswer.Status != StatusAnswer.Ok)
                {
                    _logger.LogError("не удался расчет на конвейере");
                    return new ApiResultAnswer() { Status = tryAnswer };
                }
            }
            catch (OperationCanceledException)
            {
                return new ApiResultAnswer() { Status = new StatusCalculation() { Status = StatusAnswer.Cancellation } };
            }

            result.HistoryInputId = id;

            return new ApiResultAnswer()
            {
                Status = new StatusCalculation() { Status = StatusAnswer.Ok },
                Result = result,
                IdHistory = id
            };
        }

        [HttpGet("api/get-simpl-result{id}")]
        public async Task<ApiSimplResultAnswer> GetSimplResult(int id)
        {
            СalculationHistory? history;
            Result result;
            try
            {
                (history, result, StatusCalculation tryAnswer) = await _calculator.TryStartCalculation(id, new CancellationToken());

                if (tryAnswer.Status != StatusAnswer.Ok)
                {
                    _logger.LogError("не удался расчет на конвейере");
                    return new ApiSimplResultAnswer() { Status = tryAnswer };
                }
            }
            catch (OperationCanceledException)
            {
                return new ApiSimplResultAnswer() { Status = new StatusCalculation() { Status = StatusAnswer.Cancellation } };
            }

            result.HistoryInputId = id;

            return new ApiSimplResultAnswer()
            {
                Status = new StatusCalculation() { Status = StatusAnswer.Ok },
                SimplResult = Converter.HistoryToSimplResult(history),
                IdHistory = id
            };
        }
    }
}
