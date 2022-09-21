using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class AllPrice : IConveyor
    {
        public Task<(СalculationHistory, Result, bool)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult((history, result, false));
            }

            try
            {
                result.Price = Convert.ToInt32(Math.Round((double)result.Price / 100, 1) * 100); //округление

                if (history.Price == null)
                {
                    result.TryPrice = true;
                    history.Price = result.Price;
                }

                result.TryPrice = result.IsActualPaperPrice() &&
                    (result.Price == history.Price);

                if (result.Price != history.Price)
                {
                    result.Price = history.Price.Value;
                    result.TryPrice = false;
                }

                return Task.FromResult((history, result, true));
            }
            catch (OverflowException)
            {
                return Task.FromResult((history, result, false));
            }
        }
    }
}