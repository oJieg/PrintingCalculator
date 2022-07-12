using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;
using printing_calculator.Models;
using Microsoft.Extensions.Options;


namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperSplitting : IConveyor
    {
        SettingPrinter _settings;
        public PaperSplitting(SettingPrinter options)
        {
            _settings = options;
        }

        public bool TryConveyorStart(ref History history, ref Result result)
        {

            result.ResultPaper.PiecesPerSheet = PiecesPerSheet(history.Input.Paper.Size, result.Height, result.Whidth);
            result.ResultPaper.Sheets = ((int)
                Math.Ceiling(((double)result.Amount / (double)result.ResultPaper.PiecesPerSheet)))
                * result.Kinds;

            return true;
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
            sizeProdyctionHeight = sizeProdyctionHeight + _settings.Bleed;
            sizeProdyctionWidth = sizeProdyctionWidth +_settings.Bleed;

            sizePaperHorizontal = sizePaperHorizontal - _settings.FieldForLabels;
            sizePaperHVertical = sizePaperHVertical - _settings.FieldForLabels;

            int horizontal = (int)((float)sizePaperHorizontal / sizeProdyctionHeight);
            int vertical = (int)((float)sizePaperHVertical / sizeProdyctionWidth);

            return horizontal * vertical;
        }
    }
}
