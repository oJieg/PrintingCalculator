using printing_calculator.ViewModels;
using printing_calculator.DataBase;

namespace printing_calculator.Models
{
    public static class Converter
    {
        public static SimpleResult HistoryToSimplResult(СalculationHistory history)
        {
            if (history.Price == null)
                return new SimpleResult();

            return new()
            {
                HistoryId = history.Id,
                Whidth = history.Input.Whidth,
                Height = history.Input.Height,
                Amount = history.Input.Amount,
                Kinds = history.Input.Kinds,
                PaperName = history.Input.Paper.Name,

                Lamination = history.Input.Lamination != null,
                Creasing = history.CreasingPrice > 0,
                Drilling = history.DrillingPrice > 0,
                Rounding = history.RoundingPrice > 0,

                Price = history.Price.Value
            };
        }
    }
}
