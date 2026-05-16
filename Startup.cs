using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using printing_calculator.Clients;
using printing_calculator.Models;
using printing_calculator.Models.Calculating;
using printing_calculator.Servises;
using printing_calculator.Servises.Interface;
using printing_calculator.Singletones;
using printing_calculator.Singletones.Interfases;
using Refit;
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
            services.Configure<AuthorizationSettings>(_configuration.GetSection("AuthorizationSettings"));
            services.Configure<IntegrationSettings>(_configuration.GetSection("IntegrationSettings"));

            services.AddSingleton<ITokenGenerator, SecurityCryptographyTokenGenerator>();
            services.AddSingleton<ITokenStore, TokensStore>();
            services.AddSingleton<ISettingStore, SettingStore>();

            services.AddTransient<ConveyorCalculator>();
            services.AddTransient<GeneratorHistory>();
            services.AddTransient<Validation>();

            services.AddScoped<IWigetService, WigetService>();

            services.AddMvc();

            services
                .AddRefitClient<IBitrixApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://b24-j3159k.bitrix24.ru/rest/1/b821b0099i4m2kkg"));
            services
                .AddRefitClient<IBitrixWithAauthApi>()
                .ConfigureHttpClient(x => x.BaseAddress = new Uri("https://b24-j3159k.bitrix24.ru/rest"));

            services.AddControllers()
                .AddMvcOptions( option =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                    option.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddJsonOptions(x=>x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            
            services.AddSwaggerGen();

            bool isProd = _configuration.GetValue<bool>("IsProd");

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = "/Account/Login";
                    option.Cookie.HttpOnly = true;
                    option.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    
                    option.Cookie.SameSite = isProd? SameSiteMode.Lax: SameSiteMode.None;
                    option.Events.OnRedirectToLogin = context =>
                    {
                        if (context.Request.Path.StartsWithSegments("/api"))
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return Task.CompletedTask;
                        }
                        context.Response.Redirect(context.RedirectUri);
                        return Task.CompletedTask;
                    };
                    option.Events.OnRedirectToAccessDenied = context =>
                    {
                        if (context.Request.Path.StartsWithSegments("/api"))
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            return Task.CompletedTask;
                        }
                        context.Response.Redirect(context.RedirectUri);
                        return Task.CompletedTask;
                    };
                });

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionHandlingMiddleware>();


            app.UseStaticFiles();
            app.UseRouting(); // используем систему маршрутизации

            app.UseAuthentication();
            app.UseAuthorization();

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