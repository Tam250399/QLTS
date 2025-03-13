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
using GS.WebApi.Factories;
using GS.WebApi.Factories.BaoCaoSvc;
using GS.WebApi.Factories.Common;
using GS.WebApi.Factories.Common.ConsumingApi;
using GS.WebApi.Factories.ConsumingApi;
using GS.WebApi.SoapServices;
using SoapCore.Extensibility;

namespace GS.WebApi.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, GSConfig config)
        {
            #region app work service           
            builder.RegisterType<NLogger>().As<IApiLog>().InstancePerLifetimeScope();
            builder.RegisterType<FileCongViecService>().As<IFileCongViecService>().InstancePerLifetimeScope();
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
            builder.RegisterType<LogsDongBoCsdlqgService>().As<ILogsDongBoCsdlqgService>().InstancePerLifetimeScope();
            #endregion
            //factories danh muc
            #region factories danh muc
            //builder.RegisterType<DanhMucFactory>().As<IDanhMucFactory>().InstancePerLifetimeScope();
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
            builder.RegisterType<Kho_TaiSanToanDanFactory>().As<IKho_TaiSanToanDanFactory>().InstancePerLifetimeScope();
            #endregion
            #region Soap service 
            builder.RegisterType<DanhMucSvc>().As<IDanhMucSvc>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanSvc>().As<ITaiSanSvc>().InstancePerLifetimeScope();
            builder.RegisterType<DanhMucSvcModelFactory>().As<IDanhMucSvcModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanSvcFactory>().As<ITaiSanSvcFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanDBSvcFactory>().As<ITaiSanDBSvcFactory>().InstancePerLifetimeScope();
            builder.RegisterType<GSAPIService>().As<IGSAPIService>().InstancePerLifetimeScope();

            builder.RegisterType<DMDC_DanhMucSvc>().As<IDMDC_DanhMucSvc>().InstancePerLifetimeScope();
            builder.RegisterType<DMDC_DanhMucSvcModelFactory>().As<IDMDC_DanhMucSvcModelFactory>().InstancePerLifetimeScope();
            
            #endregion
            #region DB service 
            builder.RegisterType<DBTaiSanService>().As<IDBTaiSanService>().InstancePerLifetimeScope();
            builder.RegisterType<CommonFactory>().As<ICommonFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiSanNhatKyService>().As<ITaiSanNhatKyService>().InstancePerLifetimeScope();
            builder.RegisterType<Kho_DanhMucFactory>().As<IKho_DanhMucFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Kho_TaiSanFactory>().As<IKho_TaiSanFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ValidateFactory>().As<IValidateFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TempDongBoTaiSanCuService>().As<ITempDongBoTaiSanCuService>().InstancePerLifetimeScope();
            builder.RegisterType<GSAPIService>().As<IGSAPIService>().InstancePerLifetimeScope();
            builder.RegisterType<DB_QueueProcessService>().As<IDB_QueueProcessService>().InstancePerLifetimeScope();
            builder.RegisterType<DB_QueueProcessHistoryService>().As<IDB_QueueProcessHistoryService>().InstancePerLifetimeScope();
            builder.RegisterType<DBTempTaiSanCuService>().As<IDBTempTaiSanCuService>().InstancePerLifetimeScope();
            builder.RegisterType<LogsDongBoCsdlqgService>().As<ILogsDongBoCsdlqgService>().InstancePerLifetimeScope();
            #endregion
            #region Register Service for NghiepVu
            builder.RegisterType<YeuCauService>().As<IYeuCauService>().InstancePerLifetimeScope();
            builder.RegisterType<YeuCauChiTietService>().As<IYeuCauChiTietService>().InstancePerLifetimeScope();
            builder.RegisterType<YeuCauNhatKyService>().As<IYeuCauNhatKyService>().InstancePerLifetimeScope();
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
            builder.RegisterType<DongBoServices>().As<IDongBoServices>().InstancePerLifetimeScope();
            builder.RegisterType<CauHinhService>().As<ICauHinhService>().InstancePerLifetimeScope();
            builder.RegisterType<YeuCauService>().As<IYeuCauService>().InstancePerLifetimeScope();
            builder.RegisterType<YeuCauService>().As<IYeuCauService>().InstancePerLifetimeScope();
            builder.RegisterType<QueueProcessService>().As<IQueueProcessService>().InstancePerLifetimeScope();
            builder.RegisterType<BaoCaoSvcModelFactory>().As<IBaoCaoSvcModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DB_QueueProcessService>().As<IDB_QueueProcessService>().InstancePerLifetimeScope();
            builder.RegisterType<DB_QueueProcessHistoryService>().As<IDB_QueueProcessHistoryService>().InstancePerLifetimeScope();
            #region Register Service KeToan (KT)
            builder.RegisterType<HaoMonTaiSanService>().As<IHaoMonTaiSanService>().InstancePerLifetimeScope();
            builder.RegisterType<HaoMonTaiSanLogService>().As<IHaoMonTaiSanLogService>().InstancePerLifetimeScope();
            builder.RegisterType<KhauHaoTaiSanService>().As<IKhauHaoTaiSanService>().InstancePerLifetimeScope();
            #endregion

            builder.RegisterType<CustomFaultExceptionTransformer>().As<IFaultExceptionTransformer>().InstancePerLifetimeScope();
        }
        public int Order => 3;
    }
}