using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationCostPrise : IConveyor
    {
        private readonly Settings.Lamination _lamination1;
        private readonly ApplicationContext _DB;
        public LamonationCostPrise(Settings.Lamination lamination, ApplicationContext Db)
        {
            _lamination1 = lamination;
            _DB = Db;
        }
        public bool TryConveyorStart(ref History history, ref Result result)
        {

            if (history.Input.Lamination == null)
            {
                result.LaminationResult.ActualCostPrics = true;
                return true;
            }

            try
            {
                int CostPrice = (int)((history.LaminationPrices.Price + _lamination1.Job) * result.PaperResult.Sheets);
                result.LaminationResult.CostPrice = CostPrice;

                result.LaminationResult.ActualCostPrics = ActualCostPrice(history);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ActualCostPrice(History history)
        {
            int PriceId = history.LaminationPrices.Id;
            List<LaminationPrice> ActualPriceId = _DB.Laminations
                .Include(x => x.Price)
                .Where(x => x.Name == history.Input.Lamination.Name)
                .First()
                .Price;

            if (PriceId == ActualPriceId[^1].Id)
            {
                return true;
            }
            return false;
        }
    }
}