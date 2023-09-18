using Microsoft.AspNetCore.Mvc;
using printing_calculator.Models.Calculating;
using printing_calculator.ViewModels;
using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace printing_calculator.controllers.WebApi
{
	[ApiController]
	public class CalculationController : ControllerBase
	{
		private readonly ApplicationContext _applicationContext;
		private readonly ILogger<CalculatorResultController> _logger;
		private readonly ConveyorCalculator _calculator;
		private readonly Validation _validation;

		public CalculationController(ApplicationContext applicationContext,
			ILogger<CalculatorResultController> loggerFactory,
			ConveyorCalculator conveyorCalculator,
			Validation validation)
		{
			_applicationContext = applicationContext;
			_logger = loggerFactory;
			_calculator = conveyorCalculator;
			_validation = validation;
		}

		[HttpPost("api/simpl-calculation")]
		public async Task<ApiSimplCalculationAnswer> SimplCalculation(Input input)
		{
			if (input.LaminationName == string.Empty)
			{
				input.LaminationName = null;
			}
			if (!(await _validation.TryValidateInputAsync(input, new CancellationToken())))
			{
				_logger.LogError("input не прошел валидацию input:{input}", input);
				return new ApiSimplCalculationAnswer() { Status = new StatusCalculation() { Status = StatusAnswer.Other, ErrorMassage = "Введенные данные не прошли валидацию" } };
			}

			СalculationHistory? history;

			Result result;

			try
			{
				(history, result, StatusCalculation tryAnswer) = await _calculator.TryStartCalculation(input, new CancellationToken());

				if (tryAnswer.Status != StatusAnswer.Ok)
				{
					_logger.LogError("не удался расчет для данных из Input");
					return new ApiSimplCalculationAnswer() { Status = tryAnswer };
				}
			}
			catch (OperationCanceledException)
			{
				return new ApiSimplCalculationAnswer() { Status = new StatusCalculation() { Status = StatusAnswer.Cancellation } };
			}
			catch (Exception ex)
			{
				return new ApiSimplCalculationAnswer() { Status = new StatusCalculation() { Status = StatusAnswer.Other, ErrorMassage = ex.ToString() } };
			}

			try
			{
				history.DateTime = DateTime.UtcNow;

				_applicationContext.InputsHistories.Add(history.Input);
				_applicationContext.Histories.Add(history);

				await _applicationContext.SaveChangesAsync(new CancellationToken());
				return new ApiSimplCalculationAnswer()
				{
					Status = new StatusCalculation() { Status = StatusAnswer.Ok },
					IdHistory = history.Id,
					Price = history.Price
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "не удалось сохранить просчет");
				return new ApiSimplCalculationAnswer() { Status = new StatusCalculation() { Status = StatusAnswer.Other, ErrorMassage = ex.ToString() } };
			}
		}

		// PUT api/<CalculationController>/5
		[HttpPut("api/add-comment")]
		public async Task<bool> Put(AddComment addComment)
		{
			try
			{
				СalculationHistory? history = await _applicationContext.Histories
					.FirstOrDefaultAsync(history => history.Id == addComment.Id);

				history.Comment = addComment.Comment;

				_applicationContext.Update(history);
				await _applicationContext.SaveChangesAsync(new CancellationToken());

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

        [HttpPost("api/calculation")]
        public async Task<ApiResultAnswer> Calculation(Input input)
		{
            if (input.LaminationName == string.Empty)
            {
                input.LaminationName = null;
            }
            if (!(await _validation.TryValidateInputAsync(input, new CancellationToken())))
            {
                _logger.LogError("input не прошел валидацию input:{input}", input);
                return new ApiResultAnswer() { Status = new StatusCalculation() { Status = StatusAnswer.Other, ErrorMassage = "Введенные данные не прошли валидацию" } };
            }

            СalculationHistory? history;

            Result result;

            try
            {
                (history, result, StatusCalculation tryAnswer) = await _calculator.TryStartCalculation(input, new CancellationToken());

                if (tryAnswer.Status != StatusAnswer.Ok)
                {
                    _logger.LogError("не удался расчет для данных из Input");
                    return new ApiResultAnswer() { Status = tryAnswer };
                }
            }
            catch (OperationCanceledException)
            {
                return new ApiResultAnswer() { Status = new StatusCalculation() { Status = StatusAnswer.Cancellation } };
            }
            catch (Exception ex)
            {
                return new ApiResultAnswer() { Status = new StatusCalculation() { Status = StatusAnswer.Other, ErrorMassage = ex.ToString() } };
            }

            try
            {
                history.DateTime = DateTime.UtcNow;

                _applicationContext.InputsHistories.Add(history.Input);
                _applicationContext.Histories.Add(history);

                await _applicationContext.SaveChangesAsync(new CancellationToken());
                return new ApiResultAnswer()
                {
                    Status = new StatusCalculation() { Status = StatusAnswer.Ok },
                    IdHistory = history.Id,
                    Result = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "не удалось сохранить просчет");
                return new ApiResultAnswer() { Status = new StatusCalculation() { Status = StatusAnswer.Other, ErrorMassage = ex.ToString() } };
            }
        }


	}
	public class AddComment
	{
		public int Id { get; set; }
		public string Comment { get; set; }
	}
}
