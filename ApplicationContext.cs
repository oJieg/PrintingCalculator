using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase;
using printing_calculator.DataBase.setting;

namespace printing_calculator
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ConsumablePrice> ConsumablePrices { get; set; } = null!;
        public DbSet<Lamination> Laminations { get; set; } = null!;
        public DbSet<SizePaper> SizePapers { get; set; } = null!;
        public DbSet<СalculationHistory> Histories { get; set; } = null!;
        public DbSet<InputHistory> InputsHistories { get; set; } = null!;
        public DbSet<PaperCatalog> PaperCatalogs { get; set; } = null!;
        public DbSet<Setting> Settings { get; set; } = null!;
        public DbSet<Markups> Markups { get; set; } = null!;
        public DbSet<MachineSetting> MachineSettings { get; set; } = null!;
        public DbSet<PrintingMachineSetting> PrintingMachinesSettings { get; set; } = null!;
        public DbSet<PosMachinesSetting> PosMachinesSettings { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
    : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<PosMachinesSetting>().ToTable("PosMachinesSetting");
//            modelBuilder.Entity<PrintingMachineSetting>().ToTable("PrintingMachineSetting");


        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    Markups markups0 = new()
        //    {
        //        Id = 0,
        //        Page = 0,
        //        MarkupForThisPage = 350
        //    };

        //    Markups markups50 = new()
        //    {
        //        Id = 1,
        //        Page = 50,
        //        MarkupForThisPage = 200
        //    };
        //    Markups markupZiro = new()
        //    {
        //        Id = 2,
        //        Page = 0,
        //        MarkupForThisPage = 0
        //    };

        //    List<Markups> markups = new() { markups0, markups50 };
        //    List<Markups> markupsZiro = new() { markupZiro };

        //    PrintingMachineSetting printingMachine = new()
        //    {
        //        Id = 0,
        //        NameMAchine = "180",
        //        Markup = new(markups),
        //        ConsumableOther = 3,
        //        AdjustmenPrice = 0,

        //        WhiteFieldHeight = 6,
        //        WhiteFieldWidth = 5,

        //        FieldForLabels = 4,
        //        Bleed = 3,

        //        MaximumSizeLength = 2000,
        //        MaximumSizeWidth = 320,

        //        ConsumableDye = 9100,
        //        MainConsumableForDrawing = 42900
        //    };
        //    MachineSetting Laminator = new()
        //    {
        //        Id = 1,
        //        NameMAchine = "laminator",
        //        Markup = new(markups),
        //        ConsumableOther = 30,
        //        AdjustmenPrice = 50
        //    };
        //    PosMachinesSetting cut = new()
        //    {
        //        Id = 2,
        //        NameMAchine = "cuting",
        //        Markup = new(markupsZiro),
        //        ConsumableOther = 0,
        //        AdjustmenPrice = 0,
        //        CountOfPapersInOneAdjustmentCut = 100,
        //        AddMoreHit = 0
        //    };
        //    PosMachinesSetting Creasing = new()
        //    {
        //        Id = 3,
        //        NameMAchine = "creasing",
        //        Markup = new(markupsZiro),
        //        ConsumableOther = 1,
        //        AdjustmenPrice = 150,
        //        CountOfPapersInOneAdjustmentCut = 1,
        //        AddMoreHit = 1
        //    };
        //    PosMachinesSetting Drilling = new()
        //    {
        //        Id = 4,
        //        NameMAchine = "drilling",
        //        Markup = new(markupsZiro),
        //        ConsumableOther = 0.6f,
        //        AdjustmenPrice = 150,
        //        CountOfPapersInOneAdjustmentCut = 1,
        //        AddMoreHit = 0.6f
        //    };
        //    PosMachinesSetting Rounding = new()
        //    {
        //        Id = 5,
        //        NameMAchine = "rounding",
        //        Markup = new(markupsZiro),
        //        ConsumableOther = 0.5f,
        //        AdjustmenPrice = 150,
        //        CountOfPapersInOneAdjustmentCut = 1,
        //        AddMoreHit = 0
        //    };
        //    Setting setting = new()
        //    {
        //        Id = 0,
        //        PrintingsMachines = new() { printingMachine },
        //        Machines = new() { Laminator },
        //        PosMachines = new() { cut, Creasing, Drilling, Rounding }
        //    };
        //    modelBuilder.Entity<Markups>().HasData(markups0, markups50, markupZiro);
        //    modelBuilder.Entity<MachineSetting>().HasData(Laminator);
        //    modelBuilder.Entity<PrintingMachineSetting>().HasData(printingMachine);
        //    modelBuilder.Entity<PosMachinesSetting>().HasData(cut, Creasing, Drilling, Rounding);
        //}
    }
}