using printing_calculator.Models.ConveyorCalculating;
using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models.Calculating
{
    public class ConveyorCalculator
    {
        private readonly Setting _settings;
        private readonly ApplicationContext _DB;
        private readonly ILogger<ConveyorCalculator> _logger;
        public ConveyorCalculator(Setting options, ApplicationContext DB, ILogger<ConveyorCalculator> logger)
        {
            _settings = options;
            _DB = DB;
            _logger = logger;
        }

        public bool TryStartCalculation(Input input, out History history, out Result result)
        {
            history = GeneratorHistory(input);
            return TryRun(ref history, out result, AddConveyor());
        }

        public bool TryStartCalculation(int historyId, out Result result)
        {
            History? history = GetFullIncludeHistory(historyId);
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
                new PaperCostPrice( _DB), 
                new PaperMarkup(_settings.MarkupPaper), 
                new PaperCupPrise(_settings.CutSetting), 
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

        //изначально были отдельно, но перенес сюда, что бы в одном месте сразу все добавлять.
        private History? GetFullIncludeHistory(int id)
        {
            try
            {

               return _DB.Historys
                    .Include(x => x.Input)
                    .Include(x => x.PricePaper.Catalog)
                    .Include(x => x.Input.Paper.Size)
                    .Include(x => x.ConsumablePrice)
                    .Include(x => x.Input.Lamination)
                    .Include(x => x.LaminationPrices)
                    .Include(x => x.Input.Lamination.Price)
                    .Where(x => x.Id == id)
                    .First();

            }
            catch (Exception ex)
            {
                _logger.LogError("Произошло обрашение к не верному ID, {ex}", ex);
                return null;
            }
        }

        //изначально были отдельно, но перенес сюда, что бы в одном месте сразу все добавлять.
        private History GeneratorHistory(Input input)
        {
            History history = new ();
            history.Input = new HistoryInput();
            try
            {
                history.Input.Height = input.Height;
                history.Input.Whidth = input.Whidth;
                history.Input.Amount = input.Amount;
                history.Input.Kinds = input.Kinds;
                history.Input.Duplex = input.Duplex;
                history.Input.CreasingAmount = input.Creasing;
                history.Input.DrillingAmount = input.Drilling;
                history.Input.RoundingAmount = input.Rounding;

                history.Input.Paper = _DB.PaperCatalogs
                    .Include(x => x.Prices)
                    .Include(x => x.Size)
                    .Where(p => p.Name == input.Paper)
                    .First();

                history.ConsumablePrice = _DB.ConsumablePrices
                    .OrderByDescending(x => x.Id)
                    .First();
                history.Input.CreasingAmount = input.Creasing;
                history.Input.DrillingAmount = input.Drilling;
                history.Input.RoundingAmount = input.Rounding;
                history.PricePaper = history.Input.Paper.Prices
                    .OrderByDescending(_ => _.Id)
                    .First();

                if (input.LaminationName != "none")
                {
                    history.Input.Lamination = new Lamination();
                    history.Input.Lamination = _DB.Laminations
                        .Include(x => x.Price)
                        .Where(x => x.Name == input.LaminationName)
                        .First();
                    history.LaminationPrices = history.Input.Lamination.Price
                        .OrderByDescending(_ => _.Id)
                        .First();
                }
                history.Input.CreasingAmount = input.Creasing;
                history.Input.DrillingAmount = input.Drilling;
                history.Input.RoundingAmount = input.Rounding;
            }
            catch (Exception ex)
            {
                _logger.LogError("Пришел не верный Imput, {ex}", ex);
            }

            return history;
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