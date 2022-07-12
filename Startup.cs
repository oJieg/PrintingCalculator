using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using printing_calculator.DataBase;
using printing_calculator.Models;


namespace printing_calculator
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private IConfigurationBuilder _bulder;
        public Startup(IConfiguration config)
        {
            _configuration = config;
            _bulder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

        }
        public void ConfigureServices(IServiceCollection services)
        {
           // services.Configure<Markup>(_bulder.Build().GetSection("Markup"));
            services.Configure<Setting>(_bulder.Build().GetSection("Settings"));
            //services.AddControllersWithViews();

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
                    name : "default",
                    pattern : "{controller=Calculator}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=CalculatorResult}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=History}/{action=Index}/{id?}");
            });


            //TestPage testPage = new();
            //app.UseMvc();
            //app.Run(testPage.Test);


            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}