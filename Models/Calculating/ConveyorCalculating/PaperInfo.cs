﻿using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperInfo : IConveyor
    {
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            try
            {
                result.PaperResult.NamePaper = history.Input.Paper.Name;
                result.PaperResult.Duplex = history.Input.Duplex;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}