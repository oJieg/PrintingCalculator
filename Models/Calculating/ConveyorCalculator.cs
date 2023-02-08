using printing_calculator.Models.ConveyorCalculating;
using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using Microsoft.Extensions.Options;
using printing_calculator.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models.Calculating
{
    public class ConveyorCalculator
    {
        private DataBase.setting.Setting? _settings;
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<ConveyorCalculator> _logger;
        private readonly GeneratorHistory _generatorHistory;

        public ConveyorCalculator(IOptions<Setting> options, 
            ApplicationContext applicationContext, 
            ILogger<ConveyorCalculator> logger,
            GeneratorHistory generatorHistory)
        {
           // _settings = options.Value;
            _applicationContext = applicationContext;
            _logger = logger;
            _generatorHistory = generatorHistory;
        }

        
        public async Task<(СalculationHistory, Result, bool)> TryStartCalculation(int id, CancellationToken cancellationToken)
        {
            СalculationHistory? history = await _generatorHistory.GetFullIncludeHistoryAsync(id, cancellationToken);

            return await StartConveyor(history, cancellationToken);
        }

        public async Task<(СalculationHistory, Result, bool)> TryStartCalculation(Input input, CancellationToken cancellationToken)
        {
            СalculationHistory? history = await _generatorHistory.GetFullIncludeHistoryAsync(input, cancellationToken);

            return await StartConveyor(history, cancellationToken);
        }

        public async Task<(СalculationHistory, Result, bool)> TryStartCalculation(int historyId, int newAmount, CancellationToken cancellationToken)
        {
            Input input = await _generatorHistory.GetInputFromHistoryId(historyId, newAmount);
            СalculationHistory? history = await _generatorHistory.GetFullIncludeHistoryAsync(input, cancellationToken);

            return await StartConveyor(history, cancellationToken);
        }

        private async Task<(СalculationHistory, Result, bool)> StartConveyor(СalculationHistory history, CancellationToken cancellationToken)
        {
            Result result = new();
            if (history == null)
            {
                return (new СalculationHistory(), result, false);
            }
            _settings =   _applicationContext.Settings.Where(x=>x.Id==1)
                .Include(x => x.PosMachines)
                    .ThenInclude(x => x.Markup)
                .Include(x => x.PrintingsMachines)
                    .ThenInclude(x => x.Markup)
                .Include(x => x.Machines)
                    .ThenInclude(x => x.Markup)
                .FirstOrDefault();
            if(_settings == null)
            {
                return (new СalculationHistory(), result, false);
            }

            foreach (var conveyor in AddConveyor())
            {
                (history, result, bool tryAnswer) = await conveyor.TryConveyorStartAsync(history, result, cancellationToken);
                if (!tryAnswer)
                {
                    _logger.LogError("ошибка подсчета в методе {conveyor}", conveyor);
                    return (history, result, false);
                }
            }
            return (history, result, true);
        }

        private List<IConveyor> AddConveyor()
        {
            List<IConveyor> conveyors = new()
            {
                new Info(),
                new PaperInfo(),
                new PaperSplitting(_settings),
                new ConveyorCalculating.ConsumablePrice(_settings, _applicationContext),
                new PaperCostPrice(_applicationContext),
                new PaperMarkup(_settings),
                new PaperCutPriсe(_settings),
                new PaperPriсe(),
                new LamonationInfo(),
                new LamonationMarkup(_settings),
                new LamonationCostPriсe(_settings, _applicationContext),
                new LamonationPriсe(_settings),
                new PosCreasing(_settings),
                new PosDrilling(_settings),
                new PosRounding(_settings),
                new AllPrice(),
            };
            return conveyors;
        }
    }
}