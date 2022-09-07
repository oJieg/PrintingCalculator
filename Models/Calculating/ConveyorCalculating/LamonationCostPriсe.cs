using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationCostPriсe : IConveyor
    {
        private readonly Settings.Lamination _lamination;
        private readonly ApplicationContext _applicationContext;

        public LamonationCostPriсe(Settings.Lamination lamination, ApplicationContext applicationContext)
        {
            _lamination = lamination;
            _applicationContext = applicationContext;
        }

        public async Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            if (history.Input.Lamination == null || history.LaminationPrices == null)
            {
                result.LaminationResult.ActualCostPrics = true;
                return (history, result, true);
            }

            try
            {
                int сostPrice = Convert.ToInt32((history.LaminationPrices.Price + _lamination.Job) * result.PaperResult.Sheets);
                result.LaminationResult.CostPrice = сostPrice;

                result.LaminationResult.ActualCostPrics =
                    await ActualCostPrice(history.LaminationPrices.Id, history.Input.Lamination.Name, cancellationToken);
                return (history, result, true);
            }
            catch (OverflowException)
            {
                return (history, result, false);
            }
        }

        private async Task<bool> ActualCostPrice(int priceId, string laminationName, CancellationToken cancellationToken)
        {
            Lamination lamination = await _applicationContext.Laminations
                .AsNoTracking()
                .Include(laminations => laminations.Price)
                .Where(laminations => laminations.Name == laminationName)
                .FirstAsync(cancellationToken);
            List<LaminationPrice> actualPriceId = lamination.Price;

            return priceId == actualPriceId[^1].Id;
        }
    }
}