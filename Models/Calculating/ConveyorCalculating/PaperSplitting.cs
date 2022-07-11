using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models;
using Microsoft.Extensions.Options;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperSplitting : IConveyor
    {
        private IOptions<Settings> _settings;
        public PaperSplitting(IOptions<Settings> options)
        {
            _settings = options;
        }

        public bool TryConveyorStart(ref History history, ref Result result)
        {
            SplittingPaper splittingPaper = new SplittingPaper();
            result.ResultPaper.PiecesPerSheet = splittingPaper.PiecesPerSheet(history.Input.Paper.Size, result.Height, result.Whidth);
            result.ResultPaper.Sheets = ((int)
                Math.Ceiling(((double)result.Amount / (double)result.ResultPaper.PiecesPerSheet)))
                * result.Kinds;

            return true;
        }
    }
}
