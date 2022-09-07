using printing_calculator.Models.ConveyorCalculating;
using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using Microsoft.Extensions.Options;

namespace printing_calculator.Models.Calculating
{
    public class ConveyorCalculator
    {
        private readonly Setting _settings;
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<ConveyorCalculator> _logger;

        public ConveyorCalculator(IOptions<Setting> options, ApplicationContext applicationContext, ILogger<ConveyorCalculator> logger)
        {
            _settings = options.Value;
            _applicationContext = applicationContext;
            _logger = logger;
        }

        public async Task<(History, Result, bool)> TryStartCalculation(History history, Result result, CancellationToken cancellationToken)
        {
            return await TryRun(history, result, AddConveyor(), cancellationToken);
        }

        private List<IConveyor> AddConveyor()
        {
            List<IConveyor> conveyors = new()
            {
                new Info(),
                new PaperInfo(),
                new PaperSplitting(_settings.SettingPrinter),
                new ConveyorCalculating.ConsumablePrice(_settings.Consumable, _applicationContext),
                new PaperCostPrice(_applicationContext),
                new PaperMarkup(_settings),
                new PaperCutPriсe(_settings.CutSetting),
                new PaperPriсe(),
                new LamonationInfo(),
                new LamonationMarkup(_settings.Lamination),
                new LamonationCostPriсe(_settings.Lamination, _applicationContext),
                new LamonationPriсe(_settings.Lamination),
                new PosCreasing(_settings.Pos),
                new PosDrilling(_settings.Pos),
                new PosRounding(_settings.Pos),
                new AllPrice(),
            };
            return conveyors;
        }

        private async Task<(History, Result, bool)> TryRun(History history, Result result, List<IConveyor> conveyors, CancellationToken cancellationToken)
        {
            result = new();
            if (history == null)
            {
                return (new History(), result, false);
            }

            foreach (var conveyor in conveyors)
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
    }
}