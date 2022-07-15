using printing_calculator.DataBase;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models
{
    public class FullIncludeHistory
    {
        public History? Get(ApplicationContext DB, int id)
        {
            return DB.Historys
                .Include(x => x.Input)
                .Include(x => x.PricePaper.Catalog)
                .Include(x => x.Input.Paper.Size)
                .Include(x => x.ConsumablePrice)
                .Include(x => x.Input.Lamination)
                .Include(x => x.Input.Lamination.Price)
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }
        public List<History> GetList(ApplicationContext DB, int page, int countPage)
        {
            return DB.Historys
                .Include(x => x.Input)
                .Include(x => x.PricePaper.Catalog)
                .Include(x => x.Input.Lamination)
                .OrderByDescending(x => x.Id)
                .Take(countPage)
                .ToList();
        }
    }
}
