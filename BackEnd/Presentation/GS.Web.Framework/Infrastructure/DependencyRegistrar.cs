using Autofac;
using Autofac.Builder;
using Autofac.Core;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Configuration;
using GS.Core.Data;
using GS.Core.Infrastructure;
using GS.Core.Infrastructure.DependencyManagement;
using GS.Data;
using GS.Services.Authentication;
using GS.Services.Common;
using GS.Services.Helpers;
using GS.Services.HeThong;
using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.Tasks;
using GS.Web.Framework.Mvc.Routing;
using GS.Web.Framework.UI;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GS.Web.Framework.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, GSConfig config)
        {
            //file provider
            builder.RegisterType<GSFileProvider>().As<IGSFileProvider>().InstancePerLifetimeScope();

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

            //user agent helper
            builder.RegisterType<UserAgentHelper>().As<IUserAgentHelper>().InstancePerLifetimeScope();

            //data layer
            builder.RegisterType<EfDataProviderManager>().As<IDataProviderManager>().InstancePerDependency();
            builder.Register(context => context.Resolve<IDataProviderManager>().DataProvider).As<IDataProvider>().InstancePerDependency();

            builder.Register(context => new GSObjectContext(context.Resolve<DbContextOptions<GSObjectContext>>()))
                .As<IDbContext>().InstancePerLifetimeScope();

            //repositories
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //plugins


            //cache manager
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().InstancePerLifetimeScope();

            //static cache manager
            if (config.RedisCachingEnabled)
            {
                builder.RegisterType<RedisConnectionWrapper>()
                    .As<ILocker>()
                    .As<IRedisConnectionWrapper>()
                    .SingleInstance();
                builder.RegisterType<RedisCacheManager>().As<IStaticCacheManager>().InstancePerLifetimeScope();
            }
            else
            {
                builder.RegisterType<MemoryCacheManager>()
                    .As<ILocker>()
                    .As<IStaticCacheManager>()
                    .SingleInstance();
            }

            //work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

            //store context
            builder.RegisterType<DefaultLogger>().As<ILogger>().InstancePerLifetimeScope();
            //services

            builder.RegisterType<MaintenanceService>().As<IMaintenanceService>().InstancePerLifetimeScope();

            

            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerLifetimeScope();

            builder.RegisterType<PageHeadBuilder>().As<IPageHeadBuilder>().InstancePerLifetimeScope();

            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();
           

            builder.RegisterType<ActionContextAccessor>().As<IActionContextAccessor>().InstancePerLifetimeScope();
            builder.RegisterType<DateTimeHelper>().As<IDateTimeHelper>().InstancePerLifetimeScope();

            #region Common Application
            builder.RegisterType<CookieAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<NhanHienThiService>().As<INhanHienThiService>().InstancePerLifetimeScope();
            builder.RegisterType<CauHinhService>().As<ICauHinhService>().InstancePerLifetimeScope();
            builder.RegisterType<NhatKyService>().As<INhatKyService>().InstancePerLifetimeScope();
            #endregion
            //register all settings
            builder.RegisterSource(new SettingsSource());

          


        }

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 0; }
        }
    }


    /// <summary>
    /// Setting source
    /// </summary>
    public class SettingsSource : IRegistrationSource
    {
        static readonly MethodInfo BuildMethod = typeof(SettingsSource).GetMethod(
            "BuildRegistration",
            BindingFlags.Static | BindingFlags.NonPublic);

        /// <summary>
        /// Registrations for
        /// </summary>
        /// <param name="service">Service</param>
        /// <param name="registrations">Registrations</param>
        /// <returns>Registrations</returns>
        public IEnumerable<IComponentRegistration> RegistrationsFor(
            Service service,
            Func<Service, IEnumerable<IComponentRegistration>> registrations)
        {
            var ts = service as TypedService;
            if (ts != null && typeof(ICauHinh).IsAssignableFrom(ts.ServiceType))
            {
                var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
                yield return (IComponentRegistration)buildMethod.Invoke(null, null);
            }
        }

        static IComponentRegistration BuildRegistration<TSettings>() where TSettings : ICauHinh, new()
        {
            return RegistrationBuilder
                .ForDelegate((c, p) =>
                {
                    ///loat toan bo du lieu cau hinh len cache
                    return c.Resolve<ICauHinhService>().LoadCauHinh<TSettings>();
                })
                .InstancePerLifetimeScope()
                .CreateRegistration();
        }

        /// <summary>
        /// Is adapter for individual components
        /// </summary>
        public bool IsAdapterForIndividualComponents { get { return false; } }
    }

}
