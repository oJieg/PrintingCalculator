﻿using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperSplitting : IConveyor
    {
        private readonly DataBase.setting.Setting _settings;
        public PaperSplitting(DataBase.setting.Setting settings)
        {
            _settings = settings;
        }

        public Task<(СalculationHistory, Result, bool)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult((history, result, false));
            }
            result.PaperResult.PiecesPerSheet = PiecePerSheet(history.Input.Paper.Size, result.Height, result.Whidth);
            result.PaperResult.Sheets = ((int)
                Math.Ceiling(((double)result.Amount / (double)result.PaperResult.PiecesPerSheet)))
                * result.Kinds;

            return Task.FromResult((history, result, true));
        }

        private int PiecePerSheet(SizePaper sizePaper, float SizeProdyctionHeight, float SizeProdyctionWidth)
        {
            int SizePaperHeight = sizePaper.Height - (int)_settings.PrintingsMachines[0].WhiteFieldHeight;
                //.WhiteFieldHeight;
            int SizePaperWidth = sizePaper.Width - (int)_settings.PrintingsMachines[0].WhiteFieldWidth;
            //.WhiteFieldWidth;

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
            sizeProdyctionHeight += _settings.PrintingsMachines[0].Bleed;
            sizeProdyctionWidth += _settings.PrintingsMachines[0].Bleed;

            sizePaperHorizontal -= _settings.PrintingsMachines[0].FieldForLabels;
            sizePaperHVertical -= _settings.PrintingsMachines[0].FieldForLabels;

            int horizontal = (int)((float)sizePaperHorizontal / sizeProdyctionHeight);
            int vertical = (int)((float)sizePaperHVertical / sizeProdyctionWidth);

            return horizontal * vertical;
        }
    }
}