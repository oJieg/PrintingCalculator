using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase;

namespace printing_calculator
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ConsumablePrice> ConsumablePrices { get; set; } = null!;
        //  public DbSet<Markup> Markups { get; set; } = null!;
        public DbSet<LaminationPrice> LaminationPrices { get; set; } = null!;
        public DbSet<Lamination> Laminations { get; set; } = null!;
        public DbSet<PricePaper> PricePapers { get; set; } = null!;
        public DbSet<SizePaper> SizePapers { get; set; } = null!;
        public DbSet<History> Historys { get; set; } = null!;
        public DbSet<HistoryInput> HistoryInputs { get; set; } = null!;
        public DbSet<PaperCatalog> PaperCatalogs { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            // Database.EnsureDeleted();
            //  Database.EnsureCreated();
        }
    }
}