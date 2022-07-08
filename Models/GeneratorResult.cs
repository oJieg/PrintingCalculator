using printing_calculator.DataBase;
using printing_calculator.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using printing_calculator.Models.markup;

namespace printing_calculator.Models
{
    public class GeneratorResult
    {
        //private History history;
        private Result _result;
        SplittingPaper splittingPaper = new();
        private IOptions<Markup> _options;

        public GeneratorResult(IOptions<Markup> list)
        {
            _options = list;
        }

        public Result GetResult()
        {
            return _result;
        }

        public bool Start(History history)
        {
            Test(history);
            return true;
        }

        public bool Start(Input input, ApplicationContext DB)
        {
            History history = new History();
            history.Input = HistoryInput(input, DB);
            history.Id = -1;
            List<PricePaper> prise = DB.PaperCatalogs
                .Include(x => x.Prices)
                .Where(p => p.Name == input.Paper)
                .First().Prices.ToList();
            history.PricePaper = prise[prise.Count - 1];
            //history.Input.Paper = _BD.pa
            // из бд взять бумагу.
            Test(history);
            return true;
        }
        private void Test(History history)
        {
            // _options.Value.MarkupMuch;
            _result = new();
            _result.HistoryId = history.Id;
            //_result.PaperName = history.PricePaper.Catalog.Name;
            _result.PaperName = history.Input.Paper.Name;
            _result.Amount = history.Input.Amount;
            _result.Kinds = history.Input.Kinds;
            _result.Height = history.Input.Height;
            _result.Whidth = history.Input.Whidth;
            _result.Duplex = history.Input.Duplex;

            _result.PiecesPerSheet = splittingPaper.Splitting(history.Input.Paper.Size, history.Input.Height, history.Input.Whidth);

            _result.Sheets = ((int)Math.Ceiling(((double)_result.Amount / (double)_result.PiecesPerSheet))) * _result.Kinds;
            CalculatingMarkup markup = new(_options.Value.MarkupPaper.MarkupList);
            _result.Markup = markup.GetMarkup(_result.Sheets);

            _result.CostPrice = (int)(_result.Sheets * (history.PricePaper.Price + (float)5)); //временное
            _result.Price = (int)(_result.CostPrice + (_result.CostPrice * (float)_result.Markup / 100)); //временное
        }

        private HistoryInput HistoryInput(Input _input, ApplicationContext _BD) // повторяется в генераторе истории.........
        {
            HistoryInput _historyInput = new();
            _historyInput.Whidth = _input.Whidth;
            _historyInput.Height = _input.Height;
            _historyInput.Amount = _input.Amount;
            _historyInput.Duplex = _input.Duplex;
            _historyInput.Paper = _BD.PaperCatalogs
                .Include(x => x.Size)
                .Include(y => y.Prices)
                .Where(p => p.Name == _input.Paper)
                .FirstOrDefault();
            _historyInput.Kinds = _input.Kinds;
            return _historyInput;
        }
    }
}