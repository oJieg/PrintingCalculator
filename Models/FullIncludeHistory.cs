using printing_calculator.DataBase;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models
{
    public class FullIncludeHistory 
    {
        public List<History> GetList(ApplicationContext DB, int page, int countPage)
        {
            return DB.Historys
                .Include(x => x.Input)
                .Include(x => x.Input.Paper)
                .OrderByDescending(x => x.Id)
                .Take(countPage)
                .ToList();
        }
    }
}