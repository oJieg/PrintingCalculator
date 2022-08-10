using printing_calculator.Models.ConveyorCalculating;
using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using Microsoft.Extensions.Options;

namespace printing_calculator.Models.Calculating
{
    public class ConveyorCalculator
    {
        private readonly Setting _settings;
        private readonly ApplicationContext _DB;
        private readonly ILogger<ConveyorCalculator> _logger;
        public ConveyorCalculator(IOptions<Setting> options, ApplicationContext DB, ILogger<ConveyorCalculator> logger)
        {
            _settings = options.Value;
            _DB = DB;
            _logger = logger;
        }

        public bool TryStartCalculation(ref History history, out Result result)
        {
            return TryRun(ref history, out result, AddConveyor());
        }

        private List<IConveyor> AddConveyor()
        {
            List<IConveyor> conveyors = new()
            {
                new Info(),
                new PaperInfo(),
                new PaperSplitting(_settings.SettingPrinter),
                new ConveyorCalculating.ConsumablePrice(_settings.Consumable, _DB),
                new PaperCostPrice(_DB),
                new PaperMarkup(_settings.MarkupPaper),
                new PaperCutPrise(_settings.CutSetting),
                new PaperPrise(),
                new LamonationInfo(),
                new LamonationMarkup(_settings.Lamination),
                new LamonationCostPrise(_settings.Lamination, _DB),
                new LamonationPrise(_settings.Lamination),
                new PosCreasing(_settings.Pos),
                new PosDrilling(_settings.Pos),
                new PosRounding(_settings.Pos),
                new AllPrice(),
            };
            return conveyors;
        }

        private static bool TryRun(ref History history, out Result result, List<IConveyor> conveyors)
        {
            result = new();
            result.PaperResult = new PaperResult();
            if (history == null)
            {
                return false;
            }

            foreach (var conveyor in conveyors)
            {
                if (!conveyor.TryConveyorStart(ref history, ref result))
                {
                    return false;
                }
            }
            return true;
        }
    }
}