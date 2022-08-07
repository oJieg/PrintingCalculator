using printing_calculator.ViewModels;
using printing_calculator.DataBase;

namespace printing_calculator.Models
{
    public static class Converter
    {
        public static SimplResult HistoryToSimplResult(History history)
        {
            SimplResult result = new()
            {
                HistoryId = history.Id,
                Whidth = history.Input.Whidth,
                Height = history.Input.Height,
                Amount = history.Input.Amount,
                Kinds = history.Input.Kinds,
                PaperName = history.Input.Paper.Name,

                Price = (int)history.Price
            };

            result.Lamination = history.Input.Lamination != null;
            result.Creasing = history.CreasingPrice > 0;
            result.Drilling = history.DrillingPrice > 0;
            result.Rounding = history.RoundingPrice > 0;

            return result;
        }

    }
}
