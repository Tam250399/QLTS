using Autofac;
using GS.Core;
using GS.Core.Configuration;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Security;
using GS.Core.Infrastructure;
using GS.Core.Infrastructure.DependencyManagement;
using GS.Data;
using GS.NewAPI.Factories;
using GS.Services.Authentication;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.DM;
using GS.Services.DMDC;
using GS.Services.Helpers;
using GS.Services.HeThong;
using GS.Services.Logging;
using GS.Services.TaiSans;
using GS.Web.Framework;

namespace GS.NewAPI.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, GSConfig config)
        {
            #region app work service           
            builder.RegisterType<NLogger>().As<IApiLog>().InstancePerLifetimeScope();
            builder.RegisterType<NguoiDungService>().As<INguoiDungService>().InstancePerLifetimeScope();
            builder.RegisterType<VaiTroService>().As<IVaiTroService>().InstancePerLifetimeScope();
            builder.RegisterType<VaiTroNguoiDungService>().As<IVaiTroNguoiDungService>().InstancePerLifetimeScope();
            builder.RegisterType<HoatDongServices>().As<IHoatDongService>().InstancePerLifetimeScope();
            builder.RegisterType<QuyenService>().As<IQuyenService>().InstancePerLifetimeScope();
            builder.RegisterType<QuyenVaiTroService>().As<IQuyenVaiTroService>().InstancePerLifetimeScope();
            builder.RegisterType<NguoiDungDonViService>().As<INguoiDungDonViService>().InstancePerLifetimeScope();
            #endregion
            #region Register Service for DanhMuc
            builder.RegisterType<CheDoHaoMonService>().As<ICheDoHaoMonService>().InstancePerLifetimeScope();
            builder.RegisterType<ChucDanhService>().As<IChucDanhService>().InstancePerLifetimeScope();
            builder.RegisterType<DiaBanService>().As<IDiaBanService>().InstancePerLifetimeScope();
            builder.RegisterType<DiaBanTestService>().As<IDiaBanTestService>().InstancePerLifetimeScope();
            builder.RegisterType<DoiTacService>().As<IDoiTacService>().InstancePerLifetimeScope();
            builder.RegisterType<DongXeService>().As<IDongXeService>().InstancePerLifetimeScope();
            builder.RegisterType<DonViService>().As<IDonViService>().InstancePerLifetimeScope();
            builder.RegisterType<DonViChuyenDoiService>().As<IDonViChuyenDoiService>().InstancePerLifetimeScope();
            builder.RegisterType<DonViBoPhanService>().As<IDonViBoPhanService>().InstancePerLifetimeScope();
            builder.RegisterType<DuAnService>().As<IDuAnService>().InstancePerLifetimeScope();
            builder.RegisterType<HienTrangService>().As<IHienTrangService>().InstancePerLifetimeScope();
            builder.RegisterType<HinhThucMuaSamService>().As<IHinhThucMuaSamService>().InstancePerLifetimeScope();
            builder.RegisterType<LoaiBienDongService>().As<ILoaiBienDongService>().InstancePerLifetimeScope();
            builder.RegisterType<LoaiLyDoBienDongService>().As<ILoaiLyDoBienDongService>().InstancePerLifetimeScope();
            builder.RegisterType<LoaiDonViService>().As<ILoaiDonViService>().InstancePerLifetimeScope();
            builder.RegisterType<LoaiTaiSanService>().As<ILoaiTaiSanService>().InstancePerLifetimeScope();
            builder.RegisterType<LoaiTaiSanKhauHaoService>().As<ILoaiTaiSanKhauHaoService>().InstancePerLifetimeScope();
            builder.RegisterType<LoaiTaiSanDonViServices>().As<ILoaiTaiSanDonViServices>().InstancePerLifetimeScope();
            builder.RegisterType<LyDoBienDongService>().As<ILyDoBienDongService>().InstancePerLifetimeScope();
            builder.RegisterType<MucDichSuDungService>().As<IMucDichSuDungService>().InstancePerLifetimeScope();
            builder.RegisterType<NguonVonService>().As<INguonVonService>().InstancePerLifetimeScope();
            builder.RegisterType<NhanXeService>().As<INhanXeService>().InstancePerLifetimeScope();
            builder.RegisterType<QuocGiaService>().As<IQuocGiaService>().InstancePerLifetimeScope();
            builder.RegisterType<NhomCongCuService>().As<INhomCongCuService>().InstancePerLifetimeScope();
            builder.RegisterType<BaoCaoService>().As<IBaoCaoService>().InstancePerLifetimeScope();
            builder.RegisterType<PhuongAnXuLyService>().As<IPhuongAnXuLyService>().InstancePerLifetimeScope();
            builder.RegisterType<HinhThucXuLyService>().As<IHinhThucXuLyService>().InstancePerLifetimeScope();
            builder.RegisterType<NguonGocTaiSanService>().As<INguonGocTaiSanService>().InstancePerLifetimeScope();
            builder.RegisterType<MappingLoaiTaiSanService>().As<IMappingLoaiTaiSanService>().InstancePerLifetimeScope();
            builder.RegisterType<DMDC_DuAnService>().As<IDMDC_DuAnService>().InstancePerLifetimeScope();
            builder.RegisterType<DMDC_DiaBanService>().As<IDMDC_DiaBanService>().InstancePerLifetimeScope();
            builder.RegisterType<DMDC_DonViDuAnService>().As<IDMDC_DonViDuAnService>().InstancePerLifetimeScope();
            builder.RegisterType<DMDC_DonViNganSachService>().As<IDMDC_DonViNganSachService>().InstancePerLifetimeScope();
            builder.RegisterType<DMDC_QuocGiaService>().As<IDMDC_QuocGiaService>().InstancePerLifetimeScope();
            builder.RegisterType<DMDC_ToChucNganSachService>().As<IDMDC_ToChucNganSachService>().InstancePerLifetimeScope();
            #endregion
            #region Register Service for TaiSan
            builder.RegisterType<TaiSanService>().As<ITaiSanService>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanClnService>().As<ITaiSanClnService>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanDatService>().As<ITaiSanDatService>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanMayMocService>().As<ITaiSanMayMocService>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanNguonVonService>().As<ITaiSanNguonVonService>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanNhaService>().As<ITaiSanNhaService>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanOtoService>().As<ITaiSanOtoService>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanVktService>().As<ITaiSanVktService>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanKiemKeService>().As<ITaiSanKiemKeService>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanKiemKeHoiDongService>().As<ITaiSanKiemKeHoiDongService>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanKiemKeTaiSanService>().As<ITaiSanKiemKeTaiSanService>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanChoThueService>().As<ITaiSanChoThueService>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanVoHinhService>().As<ITaiSanVoHinhService>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanHienTrangSuDungService>().As<ITaiSanHienTrangSuDungService>().InstancePerLifetimeScope();
            builder.RegisterType<KhaiThacService>().As<IKhaiThacService>().InstancePerLifetimeScope();
            builder.RegisterType<KhaiThacTaiSanService>().As<IKhaiThacTaiSanService>().InstancePerLifetimeScope();
            #endregion
            //factories danh muc
            #region factories register
            builder.RegisterType<DanhMucModelFactory>().As<IDanhMucModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanModelFactory>().As<ITaiSanModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<LoaiTaiSanModelFactory>().As<ILoaiTaiSanModelFactory>().InstancePerLifetimeScope();
            #endregion
            #region application common
            //builder.RegisterType<CauHinhNguoiDung>().SingleInstance();
            //builder.RegisterType<CauHinhChung>().SingleInstance();
            //builder.RegisterType<SecuritySettings>().SingleInstance();
            //builder.RegisterType<GSConfig>().SingleInstance();
            //builder.RegisterType<HostingConfig>().SingleInstance();
            //builder.RegisterType<GSObjectContext>().As<IDbContext>().InstancePerLifetimeScope();
            //builder.RegisterType<GSFileProvider>().As<IGSFileProvider>().InstancePerLifetimeScope();
            //builder.RegisterType<UserAgentHelper>().As<IUserAgentHelper>().InstancePerLifetimeScope();
            //builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
            //builder.RegisterType<CookieAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
          

            #endregion
            //repositories
            //builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order => 3;
    }
}