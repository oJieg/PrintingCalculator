using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationCostPriсe : IConveyor
    {
        private readonly Settings.Lamination _lamination1;
        private readonly ApplicationContext _applicationContext;

        public LamonationCostPriсe(Settings.Lamination lamination, ApplicationContext applicationContext)
        {
            _lamination1 = lamination;
            _applicationContext = applicationContext;
        }

        public async Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            if (history.Input.Lamination == null)
            {
                result.LaminationResult.ActualCostPrics = true;
                return (history, result, true);
            }

            try
            {
                int сostPrice = (int)((history.LaminationPrices.Price + _lamination1.Job) * result.PaperResult.Sheets);
                result.LaminationResult.CostPrice = сostPrice;

                result.LaminationResult.ActualCostPrics = await ActualCostPrice(history, cancellationToken);
                return (history, result, true);
            }
            catch
            {
                return (history, result, false);
            }
        }

        private async Task<bool> ActualCostPrice(History history, CancellationToken cancellationToken)
        {
            int PriceId = history.LaminationPrices.Id;
            Lamination lamination = await _applicationContext.Laminations
                .AsNoTracking()
                .Include(laminations => laminations.Price)
                .Where(laminations => laminations.Name == history.Input.Lamination.Name)
                .FirstAsync(cancellationToken);
            List<LaminationPrice> ActualPriceId = lamination.Price;

            if (PriceId == ActualPriceId[^1].Id)
            {
                return true;
            }
            return false;
        }
    }
}