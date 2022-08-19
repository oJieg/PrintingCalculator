using printing_calculator.DataBase;
using Microsoft.EntityFrameworkCore;
using printing_calculator.ViewModels;

namespace printing_calculator.Models.Calculating
{
    public class GeneratorHistory
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<GeneratorHistory> _logger;

        public GeneratorHistory(ApplicationContext DB, ILogger<GeneratorHistory> logger)
        {
            _applicationContext = DB;
            _logger = logger;
        }

        public async Task<History?> GetFullIncludeHistoryAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationContext.Historys
                     .AsNoTracking()
                     .Include(x => x.Input)
                     .Include(x => x.PricePaper.Catalog)
                     .Include(x => x.Input.Paper.Size)
                     .Include(x => x.ConsumablePrice)
                     .Include(x => x.Input.Lamination)
                     .Include(x => x.LaminationPrices)
                     .Include(x => x.Input.Lamination.Price)
                     .Where(x => x.Id == id)
                     .FirstOrDefaultAsync(cancellationToken);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogInformation("пользователь отменил запрос");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Неудалось получить из базы history по id");
                return null;
            }

        }

        public async Task<History?> GetFullIncludeHistoryAsync(Input input, CancellationToken cancellationToken)
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

                history.Input.Paper = await _applicationContext.PaperCatalogs
                    .AsNoTracking()
                    .Include(x => x.Prices)
                    .Include(x => x.Size)
                    .Where(p => p.Name == input.Paper)
                    .FirstAsync(cancellationToken);

                history.ConsumablePrice = await _applicationContext.ConsumablePrices
                    .AsNoTracking()
                    .OrderByDescending(x => x.Id)
                    .FirstAsync(cancellationToken);
                history.Input.CreasingAmount = input.Creasing;
                history.Input.DrillingAmount = input.Drilling;
                history.Input.RoundingAmount = input.Rounding;
                history.PricePaper = history.Input.Paper.Prices
                    .OrderByDescending(x => x.Id)
                    .First();

                if (input.LaminationName != "none")
                {
                    history.Input.Lamination = new Lamination();
                    history.Input.Lamination = await _applicationContext.Laminations
                        .AsNoTracking()
                        .Include(x => x.Price)
                        .Where(x => x.Name == input.LaminationName)
                        .FirstAsync(cancellationToken);
                    history.LaminationPrices = history.Input.Lamination.Price
                        .OrderByDescending(x => x.Id)
                        .First();
                }
                history.Input.CreasingAmount = input.Creasing;
                history.Input.DrillingAmount = input.Drilling;
                history.Input.RoundingAmount = input.Rounding;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Неудалось преобразовать input в history");
            }
            return history;
        }

        public async Task<List<History>> GetListAsync(int page, int countPage)
        {
            try
            {
                return await _applicationContext.Historys
                    .AsNoTracking()
                    .Include(x => x.Input)
                    .Include(x => x.Input.Paper)
                    .Include(x => x.Input.Lamination)
                    .OrderByDescending(x => x.Id)
                    .Take(countPage)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Не вышло получить список истории");
                return new List<History>();
            }
        }
    }
}