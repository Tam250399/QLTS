using Autofac;
using GS.Core.Configuration;
using GS.Core.Domain.DB;
using GS.Core.Infrastructure;
using GS.Core.Infrastructure.DependencyManagement;
using GS.Services.Authentication;
using GS.Services.BaoCaos;
using GS.Services.BienDongs;
using GS.Services.CCDC;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Services.NghiepVu;
using GS.Services.SHTD;
using GS.Services.TaiSans;
using GS.Services.ThuocTinhs;

namespace GS.Web.Infrastructure
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


            #region app work service            

            builder.RegisterType<FileCongViecService>().As<IFileCongViecService>().InstancePerLifetimeScope();            
            builder.RegisterType<NguoiDungService>().As<INguoiDungService>().InstancePerLifetimeScope();
            builder.RegisterType<VaiTroService>().As<IVaiTroService>().InstancePerLifetimeScope();
            builder.RegisterType<VaiTroNguoiDungService>().As<IVaiTroNguoiDungService>().InstancePerLifetimeScope();
            builder.RegisterType<HoatDongServices>().As<IHoatDongService>().InstancePerLifetimeScope();
            builder.RegisterType<QuyenService>().As<IQuyenService>().InstancePerLifetimeScope();
            builder.RegisterType<QuyenVaiTroService>().As<IQuyenVaiTroService>().InstancePerLifetimeScope();
            builder.RegisterType<NguoiDungDonViService>().As<INguoiDungDonViService>().InstancePerLifetimeScope();
            #endregion
            #region app work factory HeThong
            #endregion

            builder.RegisterType<CheDoHaoMonService>().As<ICheDoHaoMonService>().InstancePerLifetimeScope();
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
            builder.RegisterType<LoaiDonViService>().As<ILoaiDonViService>().InstancePerLifetimeScope();
            builder.RegisterType<LoaiTaiSanService>().As<ILoaiTaiSanService>().InstancePerLifetimeScope();
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
            #endregion
            #region Register Factory for DanhMuc
            #endregion

        }

		/// <summary>
		/// Gets order of this dependency registrar implementation
		/// </summary>
		public int Order
        {
            get { return 2; }
        }
    }


}
