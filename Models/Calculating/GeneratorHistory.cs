using printing_calculator.DataBase;
using Microsoft.EntityFrameworkCore;
using printing_calculator.ViewModels;

namespace printing_calculator.Models.Calculating
{
    public class GeneratorHistory
    {
        private readonly ApplicationContext _DB;
        private readonly ILogger<GeneratorHistory> _logger;
        public GeneratorHistory(ApplicationContext DB, ILogger<GeneratorHistory> logger)
        {
            _DB = DB;
            _logger = logger;
        }

        public async Task<History?> GetFullIncludeHistory(int id)
        {
            try
            {
                return await _DB.Historys
                     .Include(x => x.Input)
                     .Include(x => x.PricePaper.Catalog)
                     .Include(x => x.Input.Paper.Size)
                     .Include(x => x.ConsumablePrice)
                     .Include(x => x.Input.Lamination)
                     .Include(x => x.LaminationPrices)
                     .Include(x => x.Input.Lamination.Price)
                     .Where(x => x.Id == id)
                     .FirstAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Произошло обрашение к не верному ID, {ex}", ex);
                return null;
            }
        }

        public async Task<History?> GetFullIncludeHistory(Input input)
        {
            History history = new();
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

                history.Input.Paper = await _DB.PaperCatalogs
                    .Include(x => x.Prices)
                    .Include(x => x.Size)
                    .Where(p => p.Name == input.Paper)
                    .FirstAsync();

                history.ConsumablePrice = await _DB.ConsumablePrices
                    .OrderByDescending(x => x.Id)
                    .FirstAsync();
                history.Input.CreasingAmount = input.Creasing;
                history.Input.DrillingAmount = input.Drilling;
                history.Input.RoundingAmount = input.Rounding;
                history.PricePaper = history.Input.Paper.Prices
                    .OrderByDescending(_ => _.Id)
                    .First();

                if (input.LaminationName != "none")
                {
                    history.Input.Lamination = new Lamination();
                    history.Input.Lamination = await _DB.Laminations
                        .Include(x => x.Price)
                        .Where(x => x.Name == input.LaminationName)
                        .FirstAsync();
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

        public async Task<List<History>> GetList(int page, int countPage)
        {
            return await _DB.Historys
                .Include(x => x.Input)
                .Include(x => x.Input.Paper)
                .Include(x => x.Input.Lamination)
                .OrderByDescending(x => x.Id)
                .Take(countPage)
                .ToListAsync();
        }
    }
}