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
using GS.Web.Factories.BaoCaos;
using GS.Web.Factories.BienDongs;
using GS.Web.Factories.KeToan;
using GS.Web.Factories.TaiSans;
using GS.Web.Factories.HeThong;
using GS.Web.Factories.QL;
using GS.Web.Factories.DongBo;
using GS.Services.Common;
using GS.Web.Factories.DB;
using GS.Services.DM;
using GS.Web.Factories.DM;
using GS.Services.ExportImport;
using GS.Services.BaoCaoDoiChieus;
using GS.Web.Factories.BaoCao;
using GS.Web.Factories.BaoCaoDienTus;
using GS.Services.BaoCaoDienTus;
using GS.Services.DMDC;

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
            builder.RegisterType<WidgetService>().As<IWidgetService>().InstancePerLifetimeScope();
            builder.RegisterType<VaiTroWidgetService>().As<IVaiTroWidgetService>().InstancePerLifetimeScope();
            builder.RegisterType<ScheduleTaskService>().As<IScheduleTaskService>().InstancePerLifetimeScope();
            #endregion
            #region app work factory HeThong
            builder.RegisterType<Factories.HeThong.FileCongViecModelFactory>().As<Factories.HeThong.IFileCongViecModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.HeThong.CauHinhModelFactory>().As<Factories.HeThong.ICauHinhModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.HeThong.NguoiDungModelFactory>().As<Factories.HeThong.INguoiDungModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.HeThong.NhanHienThiModelFactory>().As<Factories.HeThong.INhanHienThiModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.HeThong.QuyenModelFactory>().As<Factories.HeThong.IQuyenModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.HeThong.VaiTroModelFactory>().As<Factories.HeThong.IVaiTroModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.HeThong.HoatDongModelFactory>().As<Factories.HeThong.IHoatDongModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.HeThong.LoaiHoatDongModelFactory>().As<Factories.HeThong.ILoaiHoatDongModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.HeThong.NhatKyModelFactory>().As<Factories.HeThong.INhatKyModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.BaoCaos.ReportModelFactory>().As<Factories.BaoCaos.IReportModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<WidgetModelFactory>().As<IWidgetModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<VaiTroWidgetModelFactory>().As<IVaiTroWidgetModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.HeThong.DinhMucModelFactory>().As<Factories.HeThong.IDinhMucModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.HeThong.DinhMucChiTietModelFactory>().As<Factories.HeThong.IDinhMucChiTietModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ScheduleTaskModelFactory>().As<IScheduleTaskModelFactory>().InstancePerLifetimeScope();
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
            #region Register Factory for DongBo
            builder.RegisterType<DongBoFactory>().As<IDongBoFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanNhatKyModelFactory>().As<ITaiSanNhatKyModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DBTaiSanModelFactory>().As<IDBTaiSanModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DB_QueueProcessModelFactory>().As<IDB_QueueProcessModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DB_QueueProcessHistoryModelFactory>().As<IDB_QueueProcessHistoryModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DBTempTaiSanCuModelFactory>().As<IDBTempTaiSanCuModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<LogsDongBoCsdlqgModelFactory>().As<ILogsDongBoCsdlqgModelFactory>().InstancePerLifetimeScope();

            #endregion

            builder.RegisterType<CheDoHaoMonService>().As<ICheDoHaoMonService>().InstancePerLifetimeScope();
            builder.RegisterType<ExportManager>().As<IExportManager>().InstancePerLifetimeScope();
            builder.RegisterType<DongBoServices>().As<IDongBoServices>().InstancePerLifetimeScope();
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
            builder.RegisterType<LoaiTaiSanKhauHaoService>().As<ILoaiTaiSanKhauHaoService>().InstancePerLifetimeScope();
            builder.RegisterType<MappingLoaiTaiSanService>().As<IMappingLoaiTaiSanService>().InstancePerLifetimeScope();
            builder.RegisterType<DonViLichSuService>().As<IDonViLichSuService>().InstancePerLifetimeScope();
            builder.RegisterType<DinhMucService>().As<IDinhMucService>().InstancePerLifetimeScope();
            builder.RegisterType<DinhMucChiTietService>().As<IDinhMucChiTietService>().InstancePerLifetimeScope();
            builder.RegisterType<DMDC_DonViNganSachService>().As<IDMDC_DonViNganSachService>().InstancePerLifetimeScope();
            builder.RegisterType<NguonTaiSanService>().As<INguonTaiSanService>().InstancePerLifetimeScope();
            #endregion
            #region Register Factory for DanhMuc
            builder.RegisterType<Factories.DanhMuc.CheDoHaoMonModelFactory>().As<Factories.DanhMuc.ICheDoHaoMonModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.ChucDanhModelFactory>().As<Factories.DanhMuc.IChucDanhModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.DiaBanModelFactory>().As<Factories.DanhMuc.IDiaBanModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.DiaBanTestModelFactory>().As<Factories.DanhMuc.IDiaBanTestModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.DoiTacModelFactory>().As<Factories.DanhMuc.IDoiTacModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.DongXeModelFactory>().As<Factories.DanhMuc.IDongXeModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.DonViModelFactory>().As<Factories.DanhMuc.IDonViModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.DonViBoPhanModelFactory>().As<Factories.DanhMuc.IDonViBoPhanModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.DonViChuyenDoiModelFactory>().As<Factories.DanhMuc.IDonViChuyenDoiModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.DuAnModelFactory>().As<Factories.DanhMuc.IDuAnModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.HienTrangModelFactory>().As<Factories.DanhMuc.IHienTrangModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.HinhThucMuaSamModelFactory>().As<Factories.DanhMuc.IHinhThucMuaSamModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.LoaiBienDongModelFactory>().As<Factories.DanhMuc.ILoaiBienDongModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<LoaiLyDoBienDongModelFactory>().As<ILoaiLyDoBienDongModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.LoaiDonViModelFactory>().As<Factories.DanhMuc.ILoaiDonViModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.LoaiTaiSanModelFactory>().As<Factories.DanhMuc.ILoaiTaiSanModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.LoaiTaiSanVoHinhModelFactory>().As<Factories.DanhMuc.ILoaiTaiSanVoHinhModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.LyDoBienDongModelFactory>().As<Factories.DanhMuc.ILyDoBienDongModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.MucDichSuDungModelFactory>().As<Factories.DanhMuc.IMucDichSuDungModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.NguonVonModelFactory>().As<Factories.DanhMuc.INguonVonModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.NhanXeModelFactory>().As<Factories.DanhMuc.INhanXeModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.QuocGiaModelFactory>().As<Factories.DanhMuc.IQuocGiaModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.NhomCongCuModelFactory>().As<Factories.DanhMuc.INhomCongCuModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.BaoCaoModelFactory>().As<Factories.DanhMuc.IBaoCaoModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.PhuongAnXuLyModelFactory>().As<Factories.DanhMuc.IPhuongAnXuLyModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.HinhThucXuLyModelFactory>().As<Factories.DanhMuc.IHinhThucXuLyModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.NguonGocTaiSanModelFactory>().As<Factories.DanhMuc.INguonGocTaiSanModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.LoaiTaiSanKhauHaoModelFactory>().As<Factories.DanhMuc.ILoaiTaiSanKhauHaoModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.DanhMuc.NguonTaiSanModelFactory>().As<Factories.DanhMuc.INguonTaiSanModelFactory>().InstancePerLifetimeScope();
            #endregion

            #region Register Service for NghiepVu
            builder.RegisterType<YeuCauService>().As<IYeuCauService>().InstancePerLifetimeScope();
            builder.RegisterType<YeuCauChiTietService>().As<IYeuCauChiTietService>().InstancePerLifetimeScope();
            builder.RegisterType<YeuCauNhatKyService>().As<IYeuCauNhatKyService>().InstancePerLifetimeScope();
            builder.RegisterType<KiemKeTaiSanServices>().As<IKiemKeTaiSanServices>().InstancePerLifetimeScope();
            #endregion
            #region Register Factory for NghiepVu
            builder.RegisterType<Factories.NghiepVu.YeuCauModelFactory>().As<Factories.NghiepVu.IYeuCauModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.NghiepVu.YeuCauChiTietModelFactory>().As<Factories.NghiepVu.IYeuCauChiTietModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.NghiepVu.YeuCauNhatKyModelFactory>().As<Factories.NghiepVu.IYeuCauNhatKyModelFactory>().InstancePerLifetimeScope();
            #endregion

            #region Register Service for TaiSans
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
            builder.RegisterType<MuaSamService>().As<IMuaSamService>().InstancePerLifetimeScope();
            builder.RegisterType<MuaSamChiTietService>().As<IMuaSamChiTietService>().InstancePerLifetimeScope();
            builder.RegisterType<DeNghiXuLyService>().As<IDeNghiXuLyService>().InstancePerLifetimeScope();
            builder.RegisterType<DeNghiXuLyTaiSanService>().As<IDeNghiXuLyTaiSanService>().InstancePerLifetimeScope();
            builder.RegisterType<KhaiThacChiTietServices>().As<IKhaiThacChiTietServices>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanKhauHaoService>().As<ITaiSanKhauHaoService>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanImportService>().As<ITaiSanImportService>().InstancePerLifetimeScope();
            #endregion
            #region Register Factory for TaiSans
            builder.RegisterType<Factories.TaiSans.TaiSanModelFactory>().As<Factories.TaiSans.ITaiSanModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.TaiSans.TaiSanClnModelFactory>().As<Factories.TaiSans.ITaiSanClnModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.TaiSans.TaiSanDatModelFactory>().As<Factories.TaiSans.ITaiSanDatModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.TaiSans.TaiSanMayMocModelFactory>().As<Factories.TaiSans.ITaiSanMayMocModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.TaiSans.TaiSanNguonVonModelFactory>().As<Factories.TaiSans.ITaiSanNguonVonModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.TaiSans.TaiSanNhaModelFactory>().As<Factories.TaiSans.ITaiSanNhaModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.TaiSans.TaiSanOtoModelFactory>().As<Factories.TaiSans.ITaiSanOtoModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.TaiSans.TaiSanVktModelFactory>().As<Factories.TaiSans.ITaiSanVktModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanKiemKeModelFactory>().As<ITaiSanKiemKeModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanKiemKeHoiDongModelFactory>().As<ITaiSanKiemKeHoiDongModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanKiemKeTaiSanModelFactory>().As<ITaiSanKiemKeTaiSanModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanChoThueModelFactory>().As<ITaiSanChoThueModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanVoHinhModelFactory>().As<ITaiSanVoHinhModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanHienTrangSuDungModelFactory>().As<ITaiSanHienTrangSuDungModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<KhaiThacModelFactory>().As<IKhaiThacModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<KhaiThacChiTietModelFactory>().As<IKhaiThacChiTietModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<KhaiThacTaiSanModelFactory>().As<IKhaiThacTaiSanModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<MuaSamModelFactory>().As<IMuaSamModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<MuaSamChiTietModelFactory>().As<IMuaSamChiTietModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<CapNhatTaiSanModelFactory>().As<ICapNhatTaiSanModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DeNghiXuLyModelFactory>().As<IDeNghiXuLyModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DeNghiXuLyTaiSanModelFactory>().As<IDeNghiXuLyTaiSanModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanKhauHaoModelFactory>().As<ITaiSanKhauHaoModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanImportModelFactory>().As<ITaiSanImportModelFactory>().InstancePerLifetimeScope();
            #endregion
            #region Register Service for BienDongs
            builder.RegisterType<BienDongService>().As<IBienDongService>().InstancePerLifetimeScope();
            builder.RegisterType<BienDongChiTietService>().As<IBienDongChiTietService>().InstancePerLifetimeScope();
            #endregion
            #region Register Factory for BienDongs
            builder.RegisterType<Factories.BienDongs.BienDongModelFactory>().As<Factories.BienDongs.IBienDongModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.BienDongs.BienDongChiTietModelFactory>().As<Factories.BienDongs.IBienDongChiTietModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.BienDongs.ThaoTacBienDongModelFactory>().As<Factories.BienDongs.IThaoTacBienDongModelFactory>().InstancePerLifetimeScope();
            #endregion
            #region Register Service for TrungGianBDYC
            builder.RegisterType<TrungGianBDYCService>().As<ITrungGianBDYCService>().InstancePerLifetimeScope();
            //builder.RegisterType<BienDongChiTietService>().As<IBienDongChiTietService>().InstancePerLifetimeScope();
            #endregion
            #region Register Factory for TrungGianBDYC
            builder.RegisterType<TrungGianBDYCModelFactory>().As<ITrungGianBDYCModelFactory>().InstancePerLifetimeScope();
            //builder.RegisterType<Factories.BienDongs.BienDongChiTietModelFactory>().As<Factories.BienDongs.IBienDongChiTietModelFactory>().InstancePerLifetimeScope();
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
            #region Register Factory for CCDC
            builder.RegisterType<Factories.CCDC.CongCuModelFactory>().As<Factories.CCDC.ICongCuModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.CCDC.CongCuDonViModelFactory>().As<Factories.CCDC.ICongCuDonViModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.CCDC.NhapXuatCongCuModelFactory>().As<Factories.CCDC.INhapXuatCongCuModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.CCDC.XuatNhapModelFactory>().As<Factories.CCDC.IXuatNhapModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.CCDC.SuaChuaBaoDuongModelFactory>().As<Factories.CCDC.ISuaChuaBaoDuongModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.CCDC.ChoThueModelFactory>().As<Factories.CCDC.IChoThueModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.CCDC.KiemKeModelFactory>().As<Factories.CCDC.IKiemKeModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.CCDC.KiemKeCongCuModelFactory>().As<Factories.CCDC.IKiemKeCongCuModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.CCDC.KiemKeHoiDongModelFactory>().As<Factories.CCDC.IKiemKeHoiDongModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.CCDC.GiamHongmatModelFactory>().As<Factories.CCDC.IGiamHongmatModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.CCDC.GiamDieuChuyenModelFactory>().As<Factories.CCDC.IGiamDieuChuyenModelFactory>().InstancePerLifetimeScope();
            #endregion
            #region Register Service for ThuocTinh
            builder.RegisterType<ThuocTinhService>().As<IThuocTinhService>().InstancePerLifetimeScope();
            builder.RegisterType<ThuocTinhDataService>().As<IThuocTinhDataService>().InstancePerLifetimeScope();
            builder.RegisterType<ThuocTinhTaiSanService>().As<IThuocTinhTaiSanService>().InstancePerLifetimeScope();
            #endregion
            #region Register Factory for ThuocTinh
            builder.RegisterType<Factories.ThuocTinhs.ThuocTinhModelFactory>().As<Factories.ThuocTinhs.IThuocTinhModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.ThuocTinhs.ThuocTinhDataModelFactory>().As<Factories.ThuocTinhs.IThuocTinhDataModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.ThuocTinhs.ThuocTinhTaiSanModelFactory>().As<Factories.ThuocTinhs.IThuocTinhTaiSanModelFactory>().InstancePerLifetimeScope();

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
            #region Register Factory for SHTD
            builder.RegisterType<Factories.SHTD.TaiSanTdModelFactory>().As<Factories.SHTD.ITaiSanTdModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.SHTD.TaiSanTdXuLyModelFactory>().As<Factories.SHTD.ITaiSanTdXuLyModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.SHTD.QuyetDinhTichThuModelFactory>().As<Factories.SHTD.IQuyetDinhTichThuModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.SHTD.XuLyModelFactory>().As<Factories.SHTD.IXuLyModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.SHTD.XuLyKetQuaModelFactory>().As<Factories.SHTD.IXuLyKetQuaModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.SHTD.KetQuaTaiSanModelFactory>().As<Factories.SHTD.IKetQuaTaiSanModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.SHTD.KetQuaModelFactory>().As<Factories.SHTD.IKetQuaModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.SHTD.NhatKyTaiSanToanDanModelFactory>().As<Factories.SHTD.INhatKyTaiSanToanDanModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.SHTD.ThuChiModelFactory>().As<Factories.SHTD.IThuChiModelFactory>().InstancePerLifetimeScope();

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
            #region  Register Factory Report

            builder.RegisterType<QueueProcessModelFactory>().As<IQueueProcessModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<LogQueueProcessModelFactory>().As<ILogQueueProcessModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<BaoCaoDoiChieuModelFactory>().As<IBaoCaoDoiChieuModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<BaoCaoDienTuModelFactory>().As<IBaoCaoDienTuModelFactory>().InstancePerLifetimeScope();
            #endregion

            #region Register Service KeToan (KT)
            builder.RegisterType<HaoMonTaiSanService>().As<IHaoMonTaiSanService>().InstancePerLifetimeScope();
            builder.RegisterType<HaoMonTaiSanLogService>().As<IHaoMonTaiSanLogService>().InstancePerLifetimeScope();
            builder.RegisterType<KhauHaoTaiSanService>().As<IKhauHaoTaiSanService>().InstancePerLifetimeScope();
            #endregion
            #region Register Factory KeToan(KT)
            builder.RegisterType<Factories.KeToan.HaoMonTaiSanModelFactory>().As<Factories.KeToan.IHaoMonTaiSanModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.KeToan.HaoMonTaiSanLogModelFactory>().As<Factories.KeToan.IHaoMonTaiSanLogModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.KeToan.KhauHaoTaiSanModelFactory>().As<Factories.KeToan.IKhauHaoTaiSanModelFactory>().InstancePerLifetimeScope();
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
