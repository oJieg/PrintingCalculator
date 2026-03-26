using Microsoft.AspNetCore.Mvc;
using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Calculating;

namespace printing_calculator.controllers
{
	public class CalculatorResultController : Controller
	{
		private readonly ILogger<CalculatorResultController> _logger;
		private readonly ConveyorCalculator _calculator;

		public CalculatorResultController(ILogger<CalculatorResultController> loggerFactory,
			ConveyorCalculator conveyorCalculator)
		{
			_logger = loggerFactory;
			_calculator = conveyorCalculator;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int id, CancellationToken cancellationToken)
		{

			СalculationHistory? history;
			Result result;
			try
			{
				(history, result, StatusCalculation tryAnswer) = await _calculator.TryStartCalculation(id, cancellationToken);

				if (tryAnswer.Status != StatusAnswer.Ok)
				{
					_logger.LogError("не удался расчет на конвейере");
					return View("SettingMashines/Error", result);
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