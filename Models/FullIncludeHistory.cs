using printing_calculator.DataBase;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models
{
    public class FullIncludeHistory
    {
        public async Task<List<History>> GetList(ApplicationContext DB, int page, int countPage)
        {
            return await DB.Historys
                .Include(x => x.Input)
                .Include(x => x.Input.Paper)
                .Include(x => x.Input.Lamination)
                .OrderByDescending(x => x.Id)
                .Take(countPage)
                .ToListAsync();
        }
    }
}