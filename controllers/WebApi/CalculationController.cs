using Microsoft.AspNetCore.Mvc;
using printing_calculator.Models.Calculating;
using Microsoft.AspNetCore.Mvc;
using printing_calculator.ViewModels;
using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Calculating;
using printing_calculator.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace printing_calculator.controllers.WebApi
{
	[Route("api/[controller]")]
	[ApiController]
	public class CalculationController : ControllerBase
	{
		private readonly ApplicationContext _applicationContext;
		private readonly ILogger<CalculatorResultController> _logger;
		private readonly ConveyorCalculator _calculator;
		private readonly GeneratorHistory _generatorHistory;
		private readonly Validation _validation;
		public CalculationController(ApplicationContext applicationContext,
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
		//// GET: api/<CalculationController>
		//[HttpGet]
		//public IEnumerable<string> Get()
		//{
		//    return new string[] { "value1", "value2" };
		//}

		//// GET api/<CalculationController>/5
		//[HttpGet("{id}")]
		//public string Get(int id)
		//{
		//    return "value";
		//}

		// POST api/<CalculationController>
		[HttpPost]
		public async Task<int> Post(Input input)
		{
			if(input.LaminationName== string.Empty)
			{
				input.LaminationName = null;
			}
			if (!(await _validation.TryValidateInputAsync(input, new CancellationToken())))
			{
				_logger.LogError("input не прошел валидацию input:{input}", input);
				return -1;
			}

			СalculationHistory? history;

			Result result;

			try
			{
				(history, result, bool tryAnswer) = await _calculator.TryStartCalculation(input, new CancellationToken());

				if (!tryAnswer)
				{
					_logger.LogError("не удался расчет для данных из Input");
					return -1;
				}
			}
			catch (OperationCanceledException)
			{
				return -1;
			}
			catch(Exception ex)
			{
				return -1;
			}

			try
			{
				history.DateTime = DateTime.UtcNow;

				_applicationContext.InputsHistories.Add(history.Input);
				_applicationContext.Histories.Add(history);

				await _applicationContext.SaveChangesAsync(new CancellationToken());
				return history.Id;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "не удалось сохранить просчет");
				return -1;
			}
		}

		// PUT api/<CalculationController>/5
		[HttpPut]
		public async Task<bool> Put(int id, string comment, CancellationToken cancellationToken)
		{
			try
			{
				СalculationHistory? history = await _applicationContext.Histories
					.FirstOrDefaultAsync(history => history.Id == id);

				history.Comment = comment;

				_applicationContext.Update(history);
				await _applicationContext.SaveChangesAsync(cancellationToken);

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
