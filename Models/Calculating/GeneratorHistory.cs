﻿using printing_calculator.DataBase;
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

                await GetHistory(history, input, cancellationToken);
                if (input.LaminationName != Constants.ReturnEmptyOutputHttp && history.Input.Lamination != null)
                {
                    await GetHistoryLamination(history, input, cancellationToken);
                    history.LaminationPrices = history.Input.Lamination.Price
                        .OrderByDescending(price => price.Id)
                        .First();
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

        private async Task<History> GetHistory(History history, Input input, CancellationToken cancellationToken)
        {
            IQueryable<PaperCatalog> historyInputPaper = _applicationContext.PaperCatalogs
                    .Include(paperCatalogs => paperCatalogs.Prices)
                    .Include(paperCatalogs => paperCatalogs.Size)
                    .Where(paperCatalogs => paperCatalogs.Name == input.Paper);

            IQueryable<ConsumablePrice> historyConsumablePrice = _applicationContext.ConsumablePrices
                    .OrderByDescending(consumablePrices => consumablePrices.Id);

            if (input.SaveDB)
            {
                historyInputPaper = historyInputPaper.AsNoTracking();
                historyConsumablePrice = historyConsumablePrice.AsNoTracking();
            }

            history.Input.Paper = await historyInputPaper.FirstAsync(cancellationToken);
            history.ConsumablePrice = await historyConsumablePrice.FirstAsync(cancellationToken);
            return history;
        }

        private async Task<History> GetHistoryLamination(History history, Input input, CancellationToken cancellationToken)
        {
            IQueryable<Lamination> historyInputLamination = _applicationContext.Laminations
                .Include(laminations => laminations.Price)
                .Where(laminations => laminations.Name == input.LaminationName);


            if (input.SaveDB)
            {
                historyInputLamination.AsNoTracking();
            }
            history.Input.Lamination = await historyInputLamination.FirstAsync(cancellationToken);
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