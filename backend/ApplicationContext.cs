using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase;
using printing_calculator.DataBase.crm;
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
        public DbSet<Markup> Markups { get; set; } = null!;
        public DbSet<MachineSetting> MachineSettings { get; set; } = null!;
        public DbSet<PrintingMachineSetting> PrintingMachinesSettings { get; set; } = null!;
        public DbSet<PosMachinesSetting> PosMachinesSettings { get; set; } = null!;
		public DbSet<SpringBrochureSetting> SpringBrochureSettings { get; set; } = null!;
		public DbSet<CommonToAllMarkup> CommonToAllMarkups { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<Mail> Mails { get; set; } = null!;
        public DbSet<PhoneNumber> PhoneNumbers { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
    : base(options)
        {
			Database.EnsureCreated();
        }
    }
}