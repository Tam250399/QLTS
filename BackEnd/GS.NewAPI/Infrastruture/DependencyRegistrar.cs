using Autofac;
using GS.Core;
using GS.Core.Configuration;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Security;
using GS.Core.Infrastructure;
using GS.Data;
using GS.NewAPI.Factories;
using GS.Services.Authentication;
using GS.Services.Helpers;
using GS.Web.Framework;

namespace GS.NewAPI.Infrastructure
{
    public static class DependencyRegistrar
    {
        public static void Register(ContainerBuilder builder)
        {
            //factories danh muc
            #region factories register
            builder.RegisterType<DanhMucModelFactory>().As<IDanhMucModelFactory>().InstancePerLifetimeScope();
            #endregion
            #region application common
            builder.RegisterType<CauHinhNguoiDung>().SingleInstance();
            builder.RegisterType<CauHinhChung>().SingleInstance();
            builder.RegisterType<SecuritySettings>().SingleInstance();
            builder.RegisterType<GSConfig>().SingleInstance();
            builder.RegisterType<GSObjectContext>().As<IDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<GSFileProvider>().As<IGSFileProvider>().InstancePerLifetimeScope();
            builder.RegisterType<UserAgentHelper>().As<IUserAgentHelper>().InstancePerLifetimeScope();
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
            builder.RegisterType<CookieAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            
            #endregion
            //repositories
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }
    }
}