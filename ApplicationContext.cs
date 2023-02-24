using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase;
using printing_calculator.DataBase.setting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Emit;

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
        public DbSet<Markup> Markups { get; set; } = null!;
        public DbSet<MachineSetting> MachineSettings { get; set; } = null!;
        public DbSet<PrintingMachineSetting> PrintingMachinesSettings { get; set; } = null!;
        public DbSet<PosMachinesSetting> PosMachinesSettings { get; set; } = null!;
		public DbSet<CommonToAllMarkup> CommonToAllMarkups { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
    : base(options)
        {
            Database.EnsureCreated();
        }

  //      protected override void OnModelCreating(ModelBuilder modelBuilder)
  //      {
		//	Markup printMarkups0 = new()
		//	{
		//		Id = 1,
		//		Page = 0,
		//		MarkupForThisPage = 350
		//	};
		//	Markup printMarkups10 = new()
		//	{
		//		Id = 2,
		//		Page = 10,
		//		MarkupForThisPage = 300
		//	};
		//	Markup printMarkups50 = new()
		//	{
		//		Id = 3,
		//		Page = 50,
		//		MarkupForThisPage = 200
		//	};
		//	Markup printMarkups100 = new()
		//	{
		//		Id = 4,
		//		Page = 100,
		//		MarkupForThisPage = 175
		//	};
		//	Markup printMarkups250 = new()
		//	{
		//		Id = 5,
		//		Page = 250,
		//		MarkupForThisPage = 90
		//	};

		//	Markup laminationMarkups0 = new()
		//	{
		//		Id = 6,
		//		Page = 0,
		//		MarkupForThisPage = 120
		//	};
		//	Markup laminationMarkups30 = new()
		//	{
		//		Id = 7,
		//		Page = 30,
		//		MarkupForThisPage = 100
		//	};
		//	Markup laminationMarkups150 = new()
		//	{
		//		Id = 8,
		//		Page = 150,
		//		MarkupForThisPage = 10
		//	};

		//	Markup markupZiro1 = new()
		//	{
		//		Id = 9,
		//		Page = 0,
		//		MarkupForThisPage = 0
		//	};
		//	Markup markupZiro2 = new()
		//	{
		//		Id = 10,
		//		Page = 0,
		//		MarkupForThisPage = 0
		//	};
		//	Markup markupZiro3 = new()
		//	{
		//		Id = 11,
		//		Page = 0,
		//		MarkupForThisPage = 0
		//	};
		//	Markup markupZiro4 = new()
		//	{
		//		Id = 12,
		//		Page = 0,
		//		MarkupForThisPage = 0
		//	};

		//	List<Markup> printMarkups = new() { printMarkups0, printMarkups10, printMarkups50, printMarkups100, printMarkups250 };
		//	List<Markup> laminationMarkup = new() { laminationMarkups0, laminationMarkups30, laminationMarkups150 };

		//	PrintingMachineSetting printingMachine = new()
		//	{
		//		Id = 1,
		//		NameMachine = "180",
		//		Markups = new(printMarkups),
		//		ConsumableOther = 3,
		//		AdjustmenPrice = 0,

		//		WhiteFieldHeight = 6,
		//		WhiteFieldWidth = 5,

		//		FieldForLabels = 4,
		//		Bleed = 3,

		//		MaximumSizeLength = 2000,
		//		MaximumSizeWidth = 320,

		//		ConsumableDye = 9100,
		//		MainConsumableForDrawing = 42900
		//	};
		//	MachineSetting Laminator = new()
		//	{
		//		Id = 2,
		//		NameMachine = "laminator",
		//		Markups = new(laminationMarkup),
		//		ConsumableOther = 30,
		//		AdjustmenPrice = 50
		//	};
		//	PosMachinesSetting cut = new()
		//	{
		//		Id = 3,
		//		NameMachine = "cuting",
		//		Markups = new(1) { markupZiro1 },
		//		ConsumableOther = 0,
		//		AdjustmenPrice = 0,
		//		CountOfPapersInOneAdjustmentCut = 100,
		//		AddMoreHit = 0
		//	};
		//	PosMachinesSetting Creasing = new()
		//	{
		//		Id = 4,
		//		NameMachine = "creasing",
		//		Markups = new(1) { markupZiro2 },
		//		ConsumableOther = 1,
		//		AdjustmenPrice = 150,
		//		CountOfPapersInOneAdjustmentCut = 1,
		//		AddMoreHit = 1
		//	};
		//	PosMachinesSetting Drilling = new()
		//	{
		//		Id = 5,
		//		NameMachine = "drilling",
		//		Markups = new(1) { markupZiro3 },
		//		ConsumableOther = 0.6f,
		//		AdjustmenPrice = 150,
		//		CountOfPapersInOneAdjustmentCut = 1,
		//		AddMoreHit = 0.6f
		//	};
		//	PosMachinesSetting Rounding = new()
		//	{
		//		Id = 6,
		//		NameMachine = "rounding",
		//		Markups = new(1) { markupZiro4 },
		//		ConsumableOther = 0.5f,
		//		AdjustmenPrice = 150,
		//		CountOfPapersInOneAdjustmentCut = 1,
		//		AddMoreHit = 0
		//	};
		//	Setting setting = new()
		//	{
		//		Id = 1,
		//		PrintingsMachines = new() { printingMachine },
		//		Machines = new() { Laminator },
		//		PosMachines = new() { cut, Creasing, Drilling, Rounding }
		//	};

		//	modelBuilder.Entity<Markup>().HasData(printMarkups0, printMarkups10, printMarkups50, printMarkups100, printMarkups250);
		//	modelBuilder.Entity<Markup>().HasData(laminationMarkups0, laminationMarkups30, laminationMarkups150);
		//	modelBuilder.Entity<Markup>().HasData(markupZiro1, markupZiro2, markupZiro3, markupZiro4);

		//	modelBuilder.Entity<MachineSetting>().HasData(Laminator);
		//	modelBuilder.Entity<PrintingMachineSetting>().HasData(printingMachine);
		//	modelBuilder.Entity<PosMachinesSetting>().HasData(cut);
		//	modelBuilder.Entity<PosMachinesSetting>().HasData(Creasing);
		//	modelBuilder.Entity<PosMachinesSetting>().HasData(Drilling);
		//	modelBuilder.Entity<PosMachinesSetting>().HasData(Rounding);
		//}
	}
}