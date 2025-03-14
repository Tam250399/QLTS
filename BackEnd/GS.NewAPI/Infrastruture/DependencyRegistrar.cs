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

namespace GS.WebApi.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, GSConfig config)
        {
            //factories danh muc
            #region factories danh muc
            builder.RegisterType<DanhMucModelFactory>().As<IDanhMucModelFactory>().InstancePerLifetimeScope();
            #endregion
        }
        public int Order => 3;
    }
}