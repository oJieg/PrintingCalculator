using printing_calculator.DataBase;
using printing_calculator.ViewModels;

namespace printing_calculator.Models
{
    public class GeneratorResult
    {
        //private History history;
        private Result _result;
        SplittingPaper splittingPaper = new();


        public Result GetResult()
        {
            return _result;
        }

        public bool Start(History history)
        {
            Test(history);
            return true;
        }
        private void Test(History history)
        {
            _result = new();
            _result.HistoryId = history.Id;
            _result.PaperName = history.PricePaper.Catalog.Name;
            _result.Amount = history.Input.Amount;
            _result.Kinds = 1;
            _result.Height = history.Input.Height;
            _result.Whidth = history.Input.Whidth;
            _result.Duplex = history.Input.Duplex;

            _result.PiecesPerSheet = splittingPaper.Splitting(history.Input.Paper.Size, history.Input.Height, history.Input.Whidth);

            _result.Sheets = ((int)Math.Ceiling(((double)_result.Amount / (double)_result.PiecesPerSheet))) * _result.Kinds;
            CalculatingMarkup markup = new();
            _result.Markup = markup.GetMarkup(history.Markup, _result.Sheets);

            _result.CostPrice =(int)( _result.Sheets * (history.PricePaper.Price + (float)5)); //временное
            _result.Price =(int) (_result.CostPrice + (_result.CostPrice * (float)_result.Markup/100)); //временное
        }
    }
}
