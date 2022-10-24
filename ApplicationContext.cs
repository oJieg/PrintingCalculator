using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase;

namespace printing_calculator
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ConsumablePrice> ConsumablePrices { get; set; } = null!;
        public DbSet<LaminationPrice> LaminationPrices { get; set; } = null!;
        public DbSet<Lamination> Laminations { get; set; } = null!;
        public DbSet<PaperPrice> PaperPrices { get; set; } = null!;
        public DbSet<SizePaper> SizePapers { get; set; } = null!;
        public DbSet<СalculationHistory> Histories { get; set; } = null!;
        public DbSet<InputHistory> InputsHistories { get; set; } = null!;
        public DbSet<PaperCatalog> PaperCatalogs { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
    : base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsumablePrice>().HasData(new ConsumablePrice[]
            {
              new  ConsumablePrice()
                {
                    TonerPrice = 45100,
                    DrumPrice1 = 28100,
                    DrumPrice2 = 28100,
                    DrumPrice3 = 28100,
                    DrumPrice4 = 28100
                }
            });
            modelBuilder.Entity<SizePaper>().HasData(new SizePaper[]
            {
                new SizePaper()
                {
                     Name = "SRA3",
                    Height = 320,
                    Width = 450
                }
            });

            modelBuilder.Entity<PaperCatalog>().HasData(new PaperCatalog[]
             {
                new PaperCatalog()
                {
                    Name = "Нулевая бумага",
                    Prices = new List<PaperPrice>() { new PaperPrice{Price=0f} },
                    Size = new SizePaper()
                {
                     Name = "SRA3",
                    Height = 320,
                    Width = 450
                }

                }
             });

        }
    }
}