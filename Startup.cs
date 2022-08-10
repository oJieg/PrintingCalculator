using Microsoft.EntityFrameworkCore;
using printing_calculator.Models;
using printing_calculator.Models.Calculating;

namespace printing_calculator
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration config)
        {
            _configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ConveyorCalculator>();
            services.AddTransient<GeneratorHistory>();
            services.Configure<Setting>(_configuration.GetSection("Settings"));

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
                    name: "test",
                    pattern: "{controller=ValuesProcessing}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Calculator",
                    pattern: "{controller=Calculator}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "CalculatorResult",
                    pattern: "{controller=CalculatorResult}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                   name: "History",
                   pattern: "{controller=History}/{action=Index}/{id?}");
            });
        }
    }
}