using ERP.Application.Extensions;
using ERP.Infrastructure.Extensions;
using ERP.Web.Abstractions;
using ERP.Web.Extensions;
using ERP.Web.Permission;
using ERP.Web.Services;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Hangfire;
using Hangfire.MemoryStorage;
using System;
using ERP.Application.Interfaces.Services.Clients;
using ERP.Application.Interfaces.Services.Commons;
using Microsoft.Extensions.FileProviders;

namespace ERP.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddNotyf(o =>
            {
                o.DurationInSeconds = 10;
                o.IsDismissable = true;
                o.HasRippleEffect = true;
            });
            services.AddApplicationLayer();
            services.AddInfrastructure(_configuration);
            services.AddPersistenceContexts(_configuration);
            services.AddRepositories();
            services.AddSharedInfrastructure(_configuration);
            services.AddMultiLingualSupport();
            services.AddControllersWithViews()
                .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            })
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDistributedMemoryCache();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IViewRenderService, ViewRenderService>();

            #region Hangfire
            services.AddHangfire(config => config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage()
            );
            services.AddHangfireServer();
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseNotyf();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMultiLingualFeature();
            app.UseRouting();
            //Esto expone los archivos locales
            if (System.IO.Directory.Exists(@"\\172.16.50.240\archivos\SIMPLEMAK\OFICINA TECNICA"))
            //if (System.IO.Directory.Exists(@"\\biblioteca\Users\PAOLO\Documents\FACTURACION"))
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(@"\\172.16.50.240\archivos\SIMPLEMAK\OFICINA TECNICA"),
                    RequestPath = "/files"
                });
            }
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Dashboard}/{controller=Home}/{action=Index}/{id?}");
            });

            #region Hangfire
            app.UseHangfireDashboard();

            // Execute every day at 04:00 am (Cron expression = 0 4 * * *)
            recurringJobManager.AddOrUpdate("Run every day from startup, SendEmailReminders.", () => serviceProvider.GetService<IReminderService>().SendEmailReminderAsync(), "0 4 * * *");
            // Execute evert day at 16:00 pm (Cron expression = 0 16 * * *)
            recurringJobManager.AddOrUpdate("Run every day from startup, HFGetAndSaveDollarPrice.", () => serviceProvider.GetService<IDollarExchangeRateService>().HFGetAndSaveDollarPrice(), "0 16 * * *");
            #endregion

            #region Rotativa - PDF Reports
            Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, "../Extensions/Reports");
            #endregion
        }
    }
}