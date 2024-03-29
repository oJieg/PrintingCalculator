﻿using Microsoft.EntityFrameworkCore;
using printing_calculator.Models;
using printing_calculator.Models.Calculating;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            services.AddTransient<Validation>();

            services.AddMvc();

            string ConectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(ConectionString));



            services.AddControllers().AddJsonOptions(x=>x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseRouting(); // используем систему маршрутизации

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = "api";
            });

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
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                   name: "Setting",
                   pattern: "{controller=Setting}/{action=Paper}/{id?}");
                endpoints.MapControllers();

				endpoints.MapControllerRoute(
				   name: "Setting",
				   pattern: "{controller=SettingMashines}/{action=Index}/{id?}");
				endpoints.MapControllers();

				endpoints.MapControllerRoute(
                   name: "MailTransfer",
                   pattern: "{controller=MailTransfer}/{action=Client}/{id?}");
                endpoints.MapControllers();

            });
        }
    }
}