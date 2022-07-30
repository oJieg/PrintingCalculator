using Microsoft.EntityFrameworkCore;
using printing_calculator.Models;

namespace printing_calculator
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationBuilder _bulder;
        public Startup(IConfiguration config)
        {
            _configuration = config;
            _bulder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Setting>(_bulder.Build().GetSection("Settings"));

            services.AddMvc();
            string ConectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(ConectionString));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting(); // используем систему маршрутизации

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Homes}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=ValuesProcessing}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Calculator}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=CalculatorResult}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=History}/{action=Index}/{id?}");
            });
        }
    }
}