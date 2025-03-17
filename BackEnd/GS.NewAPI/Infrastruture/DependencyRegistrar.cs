using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using GS.Core.Configuration;
using GS.Core.Infrastructure;
using GS.Core.Infrastructure.DependencyManagement;
using GS.Services.Authentication;
using GS.Services.BaoCaos;
using GS.Services.BienDongs;
using GS.Services.CCDC;
using GS.Services.Common;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.DM;
using GS.Services.DMDC;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Services.Logging;
using GS.Services.NghiepVu;
using GS.Services.SHTD;
using GS.Services.TaiSans;
using GS.Services.ThuocTinhs;
using GS.NewAPI.Factories;
using GS.Data;
using GS.Core.Domain.Security;
using GS.Core;
using GS.Services.Helpers;
using GS.Web.Framework;

namespace GS.NewAPI.Infrastructure
{
    public static class DependencyRegistrar
    {
        public static void Register(ContainerBuilder builder)
        {
            //factories danh muc
            #region factories danh muc
            builder.RegisterType<DanhMucModelFactory>().As<IDanhMucModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<GSObjectContext>().As<IDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<SecuritySettings>().AsSelf().As<SecuritySettings>();
            // Register IGSFileProvider
            builder.RegisterType<GSFileProvider>().As<IGSFileProvider>().InstancePerLifetimeScope();

            // Register GSConfig (assuming GSConfig is a configuration or settings class)
            builder.RegisterType<GSConfig>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserAgentHelper>().As<IUserAgentHelper>().InstancePerLifetimeScope();
            //work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
            #endregion
        }
    }
}