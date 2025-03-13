using DevExpress.AspNetCore;
using GS.Services.Tasks;
using GS.Web.Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
namespace GS.Web
{
    /// <summary>
    /// Represents startup class of application
    /// </summary>
    public class Startup
    {
        #region Properties

        /// <summary>
        /// Get Configuration of the application
        /// </summary>
        public IConfiguration Configuration { get; }

        #endregion

        #region Ctor

        public Startup(IConfiguration configuration)
        {
            //set configuration
            Configuration = configuration;
        }

        #endregion

        /// <summary>
        /// Add services to the application and configure service provider
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //services.AddSignalR();
            services.AddDevExpressControls();
            services.AddMvc();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2);
                options.CookieHttpOnly = true;
            });
            return services.ConfigureApplicationServices(Configuration);
        }

        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {

            //application.UseSignalR(routes =>
            //{
            //    routes.MapHub<Components.NotificationHub>("/notificationHub");
            //});
            application.UseDevExpressControls();
            application.ConfigureRequestPipeline();
            application.UseSession();
            //new NotificationComponent().RegisterNotification();
        }
    }
}
