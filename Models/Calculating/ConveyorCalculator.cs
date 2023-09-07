using printing_calculator.Models.ConveyorCalculating;
using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.ViewModels;
using Microsoft.EntityFrameworkCore;
using printing_calculator.Models.Calculating.ConveyorCalculating;

namespace printing_calculator.Models.Calculating
{
	public class ConveyorCalculator
	{
		private DataBase.setting.Setting? _settings;
		private readonly ApplicationContext _applicationContext;
		private readonly ILogger<ConveyorCalculator> _logger;
		private readonly GeneratorHistory _generatorHistory;

		public ConveyorCalculator(ApplicationContext applicationContext,
			ILogger<ConveyorCalculator> logger,
			GeneratorHistory generatorHistory)
		{
			_applicationContext = applicationContext;
			_logger = logger;
			_generatorHistory = generatorHistory;
		}


		public async Task<(СalculationHistory, Result, StatusCalculation)> TryStartCalculation(int id, CancellationToken cancellationToken)
		{
			СalculationHistory? history = await _generatorHistory.GetFullIncludeHistoryAsync(id, cancellationToken);
			if(history == null)
			{
                return (new СalculationHistory(),new Result(), new StatusCalculation()
                {
                    Status = StatusType.Other,
                    ErrorMassage = "Данное Id не найдено"
                });
            }

			return await StartConveyor(history, cancellationToken);
		}

		public async Task<(СalculationHistory, Result, StatusCalculation)> TryStartCalculation(Input input, CancellationToken cancellationToken)
		{
			СalculationHistory? history = await _generatorHistory.GetFullIncludeHistoryAsync(input, cancellationToken);

			return await StartConveyor(history, cancellationToken);
		}

		public async Task<(СalculationHistory, Result, StatusCalculation)> TryStartCalculation(int historyId, int newAmount, CancellationToken cancellationToken)
		{
			Input input = await _generatorHistory.GetInputFromHistoryId(historyId, newAmount);
			СalculationHistory? history = await _generatorHistory.GetFullIncludeHistoryAsync(input, cancellationToken);

			return await StartConveyor(history, cancellationToken);
		}

		private async Task<(СalculationHistory, Result, StatusCalculation)> StartConveyor(СalculationHistory history, CancellationToken cancellationToken)
		{
			Result result = new();
			if (history == null)
			{
				return (new СalculationHistory(), result, new StatusCalculation() { Status = StatusType.Other,
				ErrorMassage = "произошла ошибка при заполении history"
				});
			}
			_settings = _applicationContext.Settings.Where(x => x.Id == 1)
				.Include(x => x.PosMachines)
					.ThenInclude(x => x.Markups)
				.Include(x => x.PrintingsMachines)
					.ThenInclude(x => x.Markups)
				.Include(x => x.Machines)
					.ThenInclude(x => x.Markups)
				.Include(x => x.CommonToAllMarkups)
				.FirstOrDefault();
			if (_settings == null)
			{
				return (new СalculationHistory(), result, new StatusCalculation() {
					Status = StatusType.Other, 
					ErrorMassage = "Ошибка при загрузке Settings из базы"
				});
			}
			StatusCalculation tryAnswer = new();
			foreach (var conveyor in AddConveyor())
			{
				(history, result, tryAnswer) = await conveyor.TryConveyorStartAsync(history, result, cancellationToken);
				if (tryAnswer.Status != StatusType.Ok)
				{
					_logger.LogError("ошибка подсчета в методе {conveyor}", conveyor);
					return (history, result, tryAnswer);
				}
			}
			return (history, result, tryAnswer);
		}

		private List<IConveyor> AddConveyor()
		{
			List<IConveyor> conveyors = new()
			{
				new Info(),
				new PaperInfo(),
				new PaperSplitting(_settings),
				new printing_calculator.Models.ConveyorCalculating.ConsumablePrice(_settings, _applicationContext),
				new PaperCostPrice(_applicationContext),
				new PaperMarkup(_settings),
				new PaperCutPriсe(_settings),
				new PaperPriсe(_settings),
				new LamonationInfo(),
				new LamonationMarkup(_settings),
				new LamonationCostPriсe(_settings, _applicationContext),
				new LamonationPriсe(_settings),
				new PosCreasing(_settings),
				new PosDrilling(_settings),
				new PosRounding(_settings),
				new PosSpringBrochure(_settings, _applicationContext),
				new PosStapleBrochure(_settings),
				new AllPrice(_settings),
			};
			return conveyors;
		}
	}
}