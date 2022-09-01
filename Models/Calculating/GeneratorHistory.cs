using printing_calculator.DataBase;
using Microsoft.EntityFrameworkCore;
using printing_calculator.ViewModels;

namespace printing_calculator.Models.Calculating
{
    public class GeneratorHistory
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<GeneratorHistory> _logger;

        public GeneratorHistory(ApplicationContext applicationContext, ILogger<GeneratorHistory> logger)
        {
            _applicationContext = applicationContext;
            _logger = logger;
        }

        public async Task<History?> GetFullIncludeHistoryAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationContext.Historys
                     .AsNoTracking()
                     .Include(historys => historys.Input)
                        .ThenInclude(input => input.Paper.Size)
                     .Include(historys => historys.Input)
                        .ThenInclude(input => input.Lamination!)
                        .ThenInclude(lamination => lamination.Price)
                     .Include(historys => historys.PricePaper.Catalog)
                     .Include(historys => historys.ConsumablePrice)
                     .Include(historys => historys.LaminationPrices)
                     .Where(historys => historys.Id == id)
                     .FirstOrDefaultAsync(cancellationToken);
            }
            catch (OperationCanceledException)
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

        public async Task<History> GetFullIncludeHistoryAsync(Input input, CancellationToken cancellationToken)
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

                history.Input.CreasingAmount = input.Creasing;
                history.Input.DrillingAmount = input.Drilling;
                history.Input.RoundingAmount = input.Rounding;

                history.Input.CreasingAmount = input.Creasing;
                history.Input.DrillingAmount = input.Drilling;
                history.Input.RoundingAmount = input.Rounding;
                if (input.SaveDB)
                {
                    await GetHistoryWithAsNoTracking(history, input, cancellationToken);
                }
                else
                {
                    await GetHistoryWithoutAsNoTracking(history, input, cancellationToken);
                }

                history.PricePaper = history.Input.Paper.Prices
                    .OrderByDescending(prices => prices.Id)
                    .First();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Неудалось преобразовать input в history");
            }
            return history;
        }

        private async Task<History> GetHistoryWithAsNoTracking(History history, Input input, CancellationToken cancellationToken)
        {
            history.Input.Paper = await _applicationContext.PaperCatalogs
                   .AsNoTracking()
                   .Include(paperCatalogs => paperCatalogs.Prices)
                   .Include(paperCatalogs => paperCatalogs.Size)
                   .Where(paperCatalogs => paperCatalogs.Name == input.Paper)
                   .FirstAsync(cancellationToken);

            history.ConsumablePrice = await _applicationContext.ConsumablePrices
                    .AsNoTracking()
                    .OrderByDescending(consumablePrices => consumablePrices.Id)
                    .FirstAsync(cancellationToken);
            if (input.LaminationName != Constants.ReturnEmptyOutputHttp)
            {
                history.Input.Lamination = await _applicationContext.Laminations
                    .AsNoTracking()
                    .Include(laminations => laminations.Price)
                    .Where(laminations => laminations.Name == input.LaminationName)
                    .FirstAsync(cancellationToken);
                history.LaminationPrices = history.Input.Lamination.Price
                    .OrderByDescending(price => price.Id)
                    .First();
            }
            return history;
        }

        private async Task<History> GetHistoryWithoutAsNoTracking(History history, Input input, CancellationToken cancellationToken)
        {
            history.Input.Paper = await _applicationContext.PaperCatalogs
                   .Include(paperCatalogs => paperCatalogs.Prices)
                   .Include(paperCatalogs => paperCatalogs.Size)
                   .Where(paperCatalogs => paperCatalogs.Name == input.Paper)
                   .FirstAsync(cancellationToken);

            history.ConsumablePrice = await _applicationContext.ConsumablePrices
                    .OrderByDescending(consumablePrices => consumablePrices.Id)
                    .FirstAsync(cancellationToken);

            if (input.LaminationName != Constants.ReturnEmptyOutputHttp)
            {
                history.Input.Lamination = await _applicationContext.Laminations
                    .Include(laminations => laminations.Price)
                    .Where(laminations => laminations.Name == input.LaminationName)
                    .FirstAsync(cancellationToken);
                history.LaminationPrices = history.Input.Lamination.Price
                    .OrderByDescending(price => price.Id)
                    .First();
            }
            return history;
        }

        public async Task<List<History>> GetListAsync(int page, int countPage, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationContext.Historys
                    .AsNoTracking()
                    .Include(historys => historys.Input)
                        .ThenInclude(Input => Input.Paper)
                    .Include(historys => historys.Input)
                        .ThenInclude(Input => Input.Lamination)
                    .OrderByDescending(historys => historys.Id)
                    .Take(countPage)
                    .ToListAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                return new List<History>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Не вышло получить список истории");
                return new List<History>();
            }
        }
    }
}