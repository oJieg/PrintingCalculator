using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperSplitting : IConveyor
    {
        private readonly SettingPrinter _settings;
        public PaperSplitting(SettingPrinter options)
        {
            _settings = options;
        }

        public async Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            if(cancellationToken.IsCancellationRequested)
            {
                return (history, result, false);
            }
            result.PaperResult.PiecesPerSheet = PiecesPerSheet(history.Input.Paper.Size, result.Height, result.Whidth);
            result.PaperResult.Sheets = ((int)
                Math.Ceiling(((double)result.Amount / (double)result.PaperResult.PiecesPerSheet)))
                * result.Kinds;

            return (history, result, true);
        }

        private int PiecesPerSheet(SizePaper sizePaper, float SizeProdyctionHeight, float SizeProdyctionWidth)
        {
            int SizePaperHeight = sizePaper.SizePaperHeight - _settings.WhiteFieldHeight;
            int SizePaperWidth = sizePaper.SizePaperWidth - _settings.WhiteFieldWidth;

            int HorizontOrientation = Splitting(SizePaperHeight, SizePaperWidth, SizeProdyctionHeight, SizeProdyctionWidth);
            int VerticalOrientation = Splitting(SizePaperWidth, SizePaperHeight, SizeProdyctionHeight, SizeProdyctionWidth);

            if (HorizontOrientation > VerticalOrientation)
            {
                return HorizontOrientation;
            }
            else
            {
                return VerticalOrientation;
            }
        }

        private int Splitting(int sizePaperHorizontal, int sizePaperHVertical, float sizeProdyctionHeight, float sizeProdyctionWidth)
        {
            sizeProdyctionHeight += _settings.Bleed;
            sizeProdyctionWidth += _settings.Bleed;

            sizePaperHorizontal -= _settings.FieldForLabels;
            sizePaperHVertical -= _settings.FieldForLabels;

            int horizontal = (int)((float)sizePaperHorizontal / sizeProdyctionHeight);
            int vertical = (int)((float)sizePaperHVertical / sizeProdyctionWidth);

            return horizontal * vertical;
        }
    }
}