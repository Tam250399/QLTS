using Autofac;
using GS.Core.Configuration;
using GS.Core.Infrastructure;
using GS.Core.Infrastructure.DependencyManagement;
using GS.NewAPI.Factories;
using GS.Services.DanhMuc;
using GS.Services.DM;
using GS.Services.DMDC;
using GS.Services.HeThong;
using GS.Services.Logging;
using GS.Services.TaiSans;
using GS.Services;
using GS.Services.NghiepVu;
using GS.Services.Common;
using GS.Services.DB;
using GS.Services.BienDongs;
using GS.Services.CCDC;
using GS.Services.ThuocTinhs;
using GS.Services.SHTD;
using GS.Services.BaoCaoDienTus;
using GS.Services.BaoCaoDoiChieus;
using GS.Services.BaoCaos;
using GS.Services.KT;
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
            builder.RegisterType<TaiSanLichSuService>().As<ITaiSanLichSuService>().InstancePerLifetimeScope();

            #endregion
            #region Register Service for NghiepVu
            builder.RegisterType<YeuCauService>().As<IYeuCauService>().InstancePerLifetimeScope();
            builder.RegisterType<YeuCauChiTietService>().As<IYeuCauChiTietService>().InstancePerLifetimeScope();
            builder.RegisterType<YeuCauNhatKyService>().As<IYeuCauNhatKyService>().InstancePerLifetimeScope();
            builder.RegisterType<KiemKeTaiSanServices>().As<IKiemKeTaiSanServices>().InstancePerLifetimeScope();
            #endregion

            #region Register Service for DongBo
            builder.RegisterType<DBTaiSanService>().As<IDBTaiSanService>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanNhatKyService>().As<ITaiSanNhatKyService>().InstancePerLifetimeScope();
            builder.RegisterType<GSAPIService>().As<IGSAPIService>().InstancePerLifetimeScope();
            builder.RegisterType<DB_QueueProcessService>().As<IDB_QueueProcessService>().InstancePerLifetimeScope();
            builder.RegisterType<DB_QueueProcessHistoryService>().As<IDB_QueueProcessHistoryService>().InstancePerLifetimeScope();
            builder.RegisterType<DBTempTaiSanCuService>().As<IDBTempTaiSanCuService>().InstancePerLifetimeScope();
            builder.RegisterType<LogsDongBoCsdlqgService>().As<ILogsDongBoCsdlqgService>().InstancePerLifetimeScope();
            #endregion
            #region Register Service for BienDongs
            builder.RegisterType<BienDongService>().As<IBienDongService>().InstancePerLifetimeScope();
            builder.RegisterType<BienDongChiTietService>().As<IBienDongChiTietService>().InstancePerLifetimeScope();
            #endregion
            #region Register Service for TrungGianBDYC
            builder.RegisterType<TrungGianBDYCService>().As<ITrungGianBDYCService>().InstancePerLifetimeScope();
            //builder.RegisterType<BienDongChiTietService>().As<IBienDongChiTietService>().InstancePerLifetimeScope();
            #endregion
            #region Register Service for CCDC
            builder.RegisterType<CongCuService>().As<ICongCuService>().InstancePerLifetimeScope();
            builder.RegisterType<CongCuDonViService>().As<ICongCuDonViService>().InstancePerLifetimeScope();
            builder.RegisterType<NhapXuatCongCuService>().As<INhapXuatCongCuService>().InstancePerLifetimeScope();
            builder.RegisterType<XuatNhapService>().As<IXuatNhapService>().InstancePerLifetimeScope();
            builder.RegisterType<SuaChuaBaoDuongService>().As<ISuaChuaBaoDuongService>().InstancePerLifetimeScope();
            builder.RegisterType<ChoThueService>().As<IChoThueService>().InstancePerLifetimeScope();
            builder.RegisterType<KiemKeService>().As<IKiemKeService>().InstancePerLifetimeScope();
            builder.RegisterType<KiemKeCongCuService>().As<IKiemKeCongCuService>().InstancePerLifetimeScope();
            builder.RegisterType<KiemKeHoiDongService>().As<IKiemKeHoiDongService>().InstancePerLifetimeScope();
            builder.RegisterType<GiamHongmatService>().As<IGiamHongmatService>().InstancePerLifetimeScope();
            #endregion
            #region Register Service for ThuocTinh
            builder.RegisterType<ThuocTinhService>().As<IThuocTinhService>().InstancePerLifetimeScope();
            builder.RegisterType<ThuocTinhDataService>().As<IThuocTinhDataService>().InstancePerLifetimeScope();
            builder.RegisterType<ThuocTinhTaiSanService>().As<IThuocTinhTaiSanService>().InstancePerLifetimeScope();
            #endregion
            #region Register Service for SHTD
            builder.RegisterType<TaiSanTdService>().As<ITaiSanTdService>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanTdXuLyService>().As<ITaiSanTdXuLyService>().InstancePerLifetimeScope();
            builder.RegisterType<QuyetDinhTichThuService>().As<IQuyetDinhTichThuService>().InstancePerLifetimeScope();
            builder.RegisterType<XuLyService>().As<IXuLyService>().InstancePerLifetimeScope();
            builder.RegisterType<XuLyKetQuaServices>().As<IXuLyKetQuaServices>().InstancePerLifetimeScope();
            builder.RegisterType<KetQuaTaiSanServices>().As<IKetQuaTaiSanServices>().InstancePerLifetimeScope();
            builder.RegisterType<KetQuaService>().As<IKetQuaService>().InstancePerLifetimeScope();
            builder.RegisterType<NhatKyTaiSanToanDanService>().As<INhatKyTaiSanToanDanService>().InstancePerLifetimeScope();
            builder.RegisterType<ThuChiService>().As<IThuChiService>().InstancePerLifetimeScope();
            #endregion
            #region  Register Service Report
            builder.RegisterType<CongCuDungCuService>().As<ICongCuDungCuService>().InstancePerLifetimeScope();
            builder.RegisterType<CheDoKeToanService>().As<ICheDoKeToanService>().InstancePerLifetimeScope();
            builder.RegisterType<BaoCaoChiTietTaiSanService>().As<IBaoCaoChiTietTaiSanService>().InstancePerLifetimeScope();
            builder.RegisterType<CongCuDungCuService>().As<ICongCuDungCuService>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanToanDanService>().As<ITaiSanToanDanService>().InstancePerLifetimeScope();
            builder.RegisterType<QueueProcessService>().As<IQueueProcessService>().InstancePerLifetimeScope();
            builder.RegisterType<BaoCaoTongHopTaiSanService>().As<IBaoCaoTongHopTaiSanService>().InstancePerLifetimeScope();
            builder.RegisterType<BaoCaoCongKhaiService>().As<IBaoCaoCongKhaiService>().InstancePerLifetimeScope();
            builder.RegisterType<BaoCaoTraCuuService>().As<IBaoCaoTraCuuService>().InstancePerLifetimeScope();
            builder.RegisterType<BaoCaoQuocHoiService>().As<IBaoCaoQuocHoiService>().InstancePerLifetimeScope();
            builder.RegisterType<BaoCaoKeKhaiServices>().As<IBaoCaoKeKhaiServices>().InstancePerLifetimeScope();
            builder.RegisterType<BaoCaoDuAnService>().As<IBaoCaoDuAnService>().InstancePerLifetimeScope();
            builder.RegisterType<InTheTaiSanServices>().As<IInTheTaiSanServices>().InstancePerLifetimeScope();
            builder.RegisterType<BaoCaoDoiChieuDuLieuService>().As<IBaoCaoDoiChieuDuLieuService>().InstancePerLifetimeScope();
            builder.RegisterType<BaoCaoDoiChieuService>().As<IBaoCaoDoiChieuService>().InstancePerLifetimeScope();
            builder.RegisterType<BaoCaoDienTuService>().As<IBaoCaoDienTuService>().InstancePerLifetimeScope();
            builder.RegisterType<LogQueueProcessService>().As<ILogQueueProcessService>().InstancePerLifetimeScope();
            #endregion
            #region Register Service KeToan (KT)
            builder.RegisterType<HaoMonTaiSanService>().As<IHaoMonTaiSanService>().InstancePerLifetimeScope();
            builder.RegisterType<HaoMonTaiSanLogService>().As<IHaoMonTaiSanLogService>().InstancePerLifetimeScope();
            builder.RegisterType<KhauHaoTaiSanService>().As<IKhauHaoTaiSanService>().InstancePerLifetimeScope();
            #endregion
            //factories danh muc
            #region factories register
            builder.RegisterType<DanhMucModelFactory>().As<IDanhMucModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanModelFactory>().As<ITaiSanModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<LoaiTaiSanModelFactory>().As<ILoaiTaiSanModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanDatModelFactory>().As<ITaiSanDatModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<BienDongModelFactory>().As<IBienDongModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<BienDongChiTietModelFactory>().As<IBienDongChiTietModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanHienTrangSuDungModelFactory>().As<ITaiSanHienTrangSuDungModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanNguonVonModelFactory>().As<ITaiSanNguonVonModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanLichSuModelFactory>().As<ITaiSanLichSuModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanNhaModelFactory>().As<ITaiSanNhaModelFactory>().InstancePerLifetimeScope();
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