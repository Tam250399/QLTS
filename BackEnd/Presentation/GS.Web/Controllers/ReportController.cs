using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Caching;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos;
using GS.Core.Domain.BaoCaos.TS_BCCK;
using GS.Core.Domain.BaoCaos.TS_BCCT;
using GS.Core.Domain.BaoCaos.TS_BCQH;
using GS.Core.Domain.BaoCaos.TS_BCTC;
using GS.Core.Domain.BaoCaos.TS_BCTH;
using GS.Core.Domain.BaoCaos.TS_CDKT;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.SHTD;
using GS.Core.Domain.TaiSans;
using GS.Core.Domain.BaoCaoDoiChieus;
using GS.Services;
using GS.Services.BaoCaos;
using GS.Services.CCDC;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.BaoCaos;
using GS.Web.Factories.CCDC;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.SHTD;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.CCDC;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;
using GS.Web.Models.BaoCaos.TaiSanTongHop;
using GS.Web.Models.BaoCaos.TSTD;
using GS.Web.Models.HeThong;
using GS.Web.Reports.BC_DOI_CHIEU_DATA;
using GS.Web.Reports.CCDC;
using GS.Web.Reports.TaiSanToanDan;
using GS.Web.Reports.Test;
using GS.Web.Reports.TheTaiSan;
using GS.Web.Reports.TS_BCCK;
using GS.Web.Reports.TS_BCCT;
using GS.Web.Reports.TS_BCDA;
using GS.Web.Reports.TS_BCKK;
using GS.Web.Reports.TS_BCQH;
using GS.Web.Reports.TS_BCTC;
using GS.Web.Reports.TS_BCTH;
using GS.Web.Reports.TS_CDKT;
using GS.Web.Reports.TS_PXNTT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GS.Services.BaoCaoDoiChieus;
using GS.Core.Infrastructure;
using GS.Core.Domain.HeThong;
using GS.Core.Data;
using GS.Web.Models.BaoCaos.TS_CDKT;
using GS.Core.Domain.BaoCaos.TheTaiSan;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class ReportController : BaseWorksController
    {
        #region Fields

        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IKiemKeModelFactory _itemModelFactory;
        private readonly IKiemKeService _kiemKeService;
        private readonly IKiemKeHoiDongService _kiemKeHoiDongService;
        private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
        private readonly IKiemKeCongCuService _kiemKeCongCuService;
        private readonly ICongCuDungCuService _congCuDungCuService;
        private readonly INhomCongCuService _nhomCongCuService;
        private readonly INhomCongCuModelFactory _nhomCongCuModelFactory;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly IDonViService _donViService;
        private readonly ICheDoKeToanService _cheDoKeToanService;
        private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
        private readonly ICauHinhService _cauHinhService;
        private readonly IDonViModelFactory _donViModelFactory;
        private readonly ITaiSanToanDanService _taiSanToanDanService;
        private readonly INhatKyService _nhatKyService;
        private readonly IBaoCaoChiTietTaiSanService _taiSanBCCTService;
        private readonly IReportModelFactory _reportModelFactory;
        private readonly IQueueProcessService _queueProcessService;
        private readonly IQueueProcessModelFactory _queueProcessModelFactory;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IBaoCaoTongHopTaiSanService _baoCaoTongHopTaiSanService;
        private readonly IDiaBanService _diaBanService;
        private readonly ILyDoBienDongModelFactory _lyDoBienDongModelFactory;
        private readonly IQuyetDinhTichThuModelFactory _quyetDinhTichThuModelFactory;
        private readonly IBaoCaoCongKhaiService _baoCaoCongKhaiService;
        private readonly INguonGocTaiSanModelFactory _nguonGocTaiSanModelFactory;
        private readonly IBaoCaoTraCuuService _baoCaoTraCuuService;
        private readonly IBaoCaoKeKhaiServices _baoCaoKeKhaiServices;
        private readonly IBaoCaoQuocHoiService _baoCaoQuocHoiService;
        private readonly IBaoCaoDuAnService _baoCaoDuAnService;
        private readonly IInTheTaiSanServices _inTheTaiSanServices;
        private readonly ITaiSanKiemKeService _kiemKeTaiSanService;
        private readonly ITaiSanKiemKeHoiDongService _taiSanKiemKeHoiDongService;
        private readonly IBaoCaoDoiChieuDuLieuService _baoCaoDoiChieuDuLieuService;
        private readonly IBaoCaoDoiChieuService _baoCaoDoiChieuService;
        private readonly IFileCongViecService _fileCongViecService;
        private readonly IGSFileProvider _fileProvider;
        private readonly INguonTaiSanService _nguonTaiSanService;

        #endregion Fields

        #region Ctor

        public ReportController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IKiemKeModelFactory itemModelFactory,
            IKiemKeService kiemKeService,
            IKiemKeHoiDongService kiemKeHoiDongService,
            IDonViBoPhanModelFactory donViBoPhanModelFactory,
            IKiemKeCongCuService kiemKeCongCuService,
            ICongCuDungCuService congCuDungCuService,
            INhomCongCuService nhomCongCuService,
            INhomCongCuModelFactory nhomCongCuModelFactory,
            IDonViBoPhanService donViBoPhanService,
            IDonViService donViService,
            ICheDoKeToanService cheDoKeToanService,
            ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
            ICauHinhService cauHinhService,
            IDonViModelFactory donViModelFactory,
            ITaiSanToanDanService taiSanToanDanService,
            INhatKyService nhatKyService,
            IBaoCaoChiTietTaiSanService taiSanBCCTService,
            IReportModelFactory reportModelFactory,
            IQueueProcessService queueProcessService,
            IQueueProcessModelFactory queueProcessModelFactory,
            IServiceScopeFactory serviceScopeFactory,
            IBaoCaoTongHopTaiSanService baoCaoTongHopTaiSanService,
            IDiaBanService diaBanService,
            ILyDoBienDongModelFactory lyDoBienDongModelFactory,
            IQuyetDinhTichThuModelFactory quyetDinhTichThuModelFactory,
            INguonGocTaiSanModelFactory nguonGocTaiSanModelFactory,
            IBaoCaoCongKhaiService baoCaoCongKhaiService,
            IBaoCaoTraCuuService baoCaoTraCuuService,
            IBaoCaoKeKhaiServices baoCaoKeKhaiServices,
            IBaoCaoQuocHoiService baoCaoQuocHoiService,
            IBaoCaoDuAnService baoCaoDuAnService,
            IInTheTaiSanServices inTheTaiSanServices,
            ITaiSanKiemKeService kiemKeTaiSanService,
            ITaiSanKiemKeHoiDongService taiSanKiemKeHoiDongService,
            IBaoCaoDoiChieuDuLieuService baoCaoDoiChieuDuLieuService,
            IBaoCaoDoiChieuService baoCaoDoiChieuService,
            IFileCongViecService fileCongViecService,
            IGSFileProvider fileProvider,
            INguonTaiSanService nguonTaiSanService
            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._kiemKeService = kiemKeService;
            this._kiemKeHoiDongService = kiemKeHoiDongService;
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
            this._kiemKeCongCuService = kiemKeCongCuService;
            this._congCuDungCuService = congCuDungCuService;
            this._nhomCongCuService = nhomCongCuService;
            this._nhomCongCuModelFactory = nhomCongCuModelFactory;
            this._donViBoPhanService = donViBoPhanService;
            this._donViService = donViService;
            this._cheDoKeToanService = cheDoKeToanService;
            this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
            this._cauHinhService = cauHinhService;
            this._donViModelFactory = donViModelFactory;
            this._taiSanToanDanService = taiSanToanDanService;
            this._nhatKyService = nhatKyService;
            this._taiSanBCCTService = taiSanBCCTService;
            this._reportModelFactory = reportModelFactory;
            this._queueProcessService = queueProcessService;
            this._queueProcessModelFactory = queueProcessModelFactory;
            this._serviceScopeFactory = serviceScopeFactory;
            this._baoCaoTongHopTaiSanService = baoCaoTongHopTaiSanService;
            this._diaBanService = diaBanService;
            this._lyDoBienDongModelFactory = lyDoBienDongModelFactory;
            this._quyetDinhTichThuModelFactory = quyetDinhTichThuModelFactory;
            this._nguonGocTaiSanModelFactory = nguonGocTaiSanModelFactory;
            this._baoCaoCongKhaiService = baoCaoCongKhaiService;
            this._baoCaoTraCuuService = baoCaoTraCuuService;
            this._baoCaoKeKhaiServices = baoCaoKeKhaiServices;
            this._baoCaoQuocHoiService = baoCaoQuocHoiService;
            this._baoCaoDuAnService = baoCaoDuAnService;
            this._inTheTaiSanServices = inTheTaiSanServices;
            this._kiemKeTaiSanService = kiemKeTaiSanService;
            this._taiSanKiemKeHoiDongService = taiSanKiemKeHoiDongService;
            _baoCaoDoiChieuDuLieuService = baoCaoDoiChieuDuLieuService;
            this._baoCaoDoiChieuService = baoCaoDoiChieuService;
            this._fileCongViecService = fileCongViecService;
            this._fileProvider = fileProvider;
            this._nguonTaiSanService = nguonTaiSanService;
        }

        #endregion Ctor

        public virtual IActionResult Index()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCao))
                return AccessDeniedView();
            return View();
        }

        public virtual IActionResult Report_Test()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCao))
                return AccessDeniedView();
            return View();
        }

        public virtual IActionResult Report_Test_()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCao))
                return AccessDeniedView();
            XtraReport model = new TestBolder();
            var obj = _taiSanBCCTService.BaoCaoTSCT_1B(DonViId: (int)_workContext.CurrentDonVi.ID, NgayKetThuc: DateTime.Now, LoaiTaiSan: 0, donViTien: 1000, DonViDienTichDat: 1000, DonViDienTichNha: 1000, BacTaiSan: 5, stringLoaiTaiSan: null);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BC(int LoaiBaoCao = 1)
        {
            var model = new BaoCaoTaiSanModel();
            model.LoaiBaoCao = LoaiBaoCao;
            model.isDonViNhapLieu = _workContext.CurrentDonVi.IS_LA_DON_VI_NHAP_LIEU.GetValueOrDefault(false);
            var bcdc = _baoCaoDoiChieuService.GetBaoCaoDoiChieu1ABySeachModel(donViId: _workContext.CurrentDonVi.ID, heThongId: 0, nam: 0);
            if (bcdc != null)
            {
                model.IsShowBaoCaoDoiChieu = true;
            }
            return View(model);
        }

        public virtual IActionResult TS_BC_OLD(int LoaiBaoCao = 1)
        {
            var model = new BaoCaoTaiSanModel();
            model.LoaiBaoCao = LoaiBaoCao;
            model.isDonViNhapLieu = _workContext.CurrentDonVi.IS_LA_DON_VI_NHAP_LIEU.GetValueOrDefault(false);
            return View(model);
        }

        public virtual IActionResult GetJsonBacBaoCaoByDate(DateTime NgayBaoCao)
        {
            var ddl = _reportModelFactory.GetSelectListsCapBaoCao(NgayBaoCao);
            return Json(ddl);
        }

        #region CCDC

        public virtual IActionResult CCDC_BienBanKiemKe(int KiemKeId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCheDoKeToanSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.CCDC_BienBanKiemKe;
            searchModel.KiemKeId = KiemKeId;
            return PartialView(searchModel);
        }

        public virtual IActionResult CCDC_BienBanKiemKe_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.CCDC_BienBanKiemKe);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            var donvibophan = _donViBoPhanService.GetDonViBoPhanById(searchModel.DonViBoPhan);
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            var bienbankiem = _kiemKeService.GetKiemKeById(searchModel.KiemKeId);
            if (bienbankiem != null) searchModel.NgayBaoCao = bienbankiem.NGAY_KIEM_KE;
            var hoidongkiemke = _kiemKeHoiDongService.GetKiemKeHoiDongs(KiemKeId: searchModel.KiemKeId);
            if (hoidongkiemke.Count() > 0)
            {
                List<string> listHoiDong = new List<string>();
                foreach (var hoidong in hoidongkiemke)
                {
                    listHoiDong.Add("Ông bà: " + hoidong.HO_TEN + "		" + "Chức vụ: " + hoidong.CHUC_VU + "		" + "Đại diện: " + hoidong.DAI_DIEN + "		" + _nhanHienThiService.GetGiaTriEnum<enumViTriKiemKeHoiDong>((enumViTriKiemKeHoiDong)hoidong.VI_TRI_ID));
                }
                searchModel.HoiDongKiemKe = string.Join("\r\n", listHoiDong);
            }
            XtraReport model = new CCDC_BienBanKiemKe(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _congCuDungCuService.BienBanKiemke(searchModel.KiemKeId, DonViTien: searchModel.DonViTien);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult CCDC_KiemKe()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCCDC))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel.NgayBaoCao = DateTime.Now;
            searchModel = _reportModelFactory.PrepareBaoCaoCCDCSearchModel(searchModel);
            return PartialView(searchModel);
        }

        public virtual IActionResult CCDC_KiemKe_Report_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            //var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            //var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            //var cauHinhModel = new CauHinhBaoCaoModel();
            //var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            //if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            //if (cauHinh.BaoCao != null)
            //{
            //	cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == enumMA_BAO_CAO.CCDC_KiemKe).FirstOrDefault();
            //}
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.CCDC_KiemKe);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            var TenBoPhan = _donViBoPhanService.GetDonViBoPhanById(searchModel.DonViBoPhan);
            searchModel.TenBoPhan = TenBoPhan != null ? TenBoPhan.TEN : "";
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new CCDC_KiemKes(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _congCuDungCuService.BaoCaoKiemKeCCDC(NgayBaoCao: (DateTime)searchModel.NgayBaoCao, DonViId: _workContext.CurrentDonVi.ID, NhomCongCuId: searchModel.ListStringCongCu, BoPhanId: searchModel.ListStringDonVi, DonViBoPhan: searchModel.DonViBoPhan, DonViTien: searchModel.DonViTien);
            model.DataSource = obj;

            return ShowViewReport(model);
        }

        public virtual IActionResult CCDC_KiemKeBoPhan()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCCDC))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCCDCSearchModel(searchModel);
            return PartialView(searchModel);
        }

        public virtual IActionResult CCDC_KiemKeBoPhan_Report_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.CCDC_KiemKeBoPhan);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            searchModel.TenBoPhan = _reportModelFactory.PrePareStringDonViBoPhanModel(searchModel.ListDonViBoPhan);
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new CCDC_KiemKeBoPhan(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _congCuDungCuService.BaoCaoKiemKePhongBanCCDC(NgayBaoCao: (DateTime)searchModel.NgayBaoCao, DonViId: _workContext.CurrentDonVi.ID, NhomCongCuId: searchModel.ListStringCongCu, BoPhanId: searchModel.ListStringDonVi, DonViBoPhan: searchModel.DonViBoPhan, DonViTien: searchModel.DonViTien);
            model.DataSource = obj;

            return ShowViewReport(model);
        }

        public virtual IActionResult CCDC_TongHopKiemKe()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCCDC))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCCDCSearchModel(searchModel);
            return PartialView(searchModel);
        }

        public virtual IActionResult CCDC_TongHopKiemKe_Report_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.CCDC_TongHopKiemKe);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            searchModel.TenBoPhan = _reportModelFactory.PrePareStringDonViBoPhanModel(searchModel.ListDonViBoPhan);
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new CCDC_TongHopKiemKe(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _congCuDungCuService.BaoCaoTongHopKiemKe(NgayBaoCao: (DateTime)searchModel.NgayBaoCao, DonViId: _workContext.CurrentDonVi.ID, NhomCongCuId: searchModel.ListStringCongCu, BoPhanId: searchModel.ListStringDonVi, DonViBoPhan: searchModel.DonViBoPhan, DonViTien: searchModel.DonViTien);
            model.DataSource = obj;

            return ShowViewReport(model);
        }

        public virtual IActionResult CCDC_LietKe()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCCDC))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCCDCSearchModel(searchModel);
            return PartialView(searchModel);
        }

        public virtual IActionResult CCDC_LietKe_Report_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            //var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            //var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            //if (cauHinh.BaoCao != null)
            //{
            //	cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == enumMA_BAO_CAO.CCDC_LietKe).FirstOrDefault();
            //}
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.CCDC_LietKe);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            searchModel.TenBoPhan = _reportModelFactory.PrePareStringDonViBoPhanModel(searchModel.ListStringDonVi);
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new CCDC_LietKe(searchModel, cauHinhModel, cauHinhChunghModel);
            //searchModel.ListStringDonVi = searchModel.ListDonViBoPhan != null ? String.Join(",", searchModel.ListDonViBoPhan) : null;
            //searchModel.ListStringCongCu = searchModel.ListCongCu != null ? String.Join(",", searchModel.ListCongCu) : null;
            var obj = _congCuDungCuService.BaoCaoLietKeCCDC(NgayBaoCao: (DateTime)searchModel.NgayBaoCao, DonViId: _workContext.CurrentDonVi.ID, NhomCongCuId: searchModel.ListStringCongCu, BoPhanId: searchModel.ListStringDonVi, DonViTien: searchModel.DonViTien);
            model.DataSource = obj;

            return ShowViewReport(model);
        }

        public virtual IActionResult CCDC_LietKeBoPhan()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCCDC))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCCDCSearchModel(searchModel);
            return PartialView(searchModel);
        }

        public virtual IActionResult CCDC_LietKeBoPhan_Report_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            //var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            //var cauHinhModel = new CauHinhBaoCaoModel();
            //var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            //if (cauHinh.BaoCao != null)
            //{
            //	cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == enumMA_BAO_CAO.CCDC_LietKeBoPhan).FirstOrDefault();
            //}
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.CCDC_LietKeBoPhan);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            searchModel.TenBoPhan = _reportModelFactory.PrePareStringDonViBoPhanModel(searchModel.ListStringDonVi);
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new CCDC_LietKeBoPhans(searchModel, cauHinhModel, cauHinhChunghModel);
            //searchModel.ListStringDonVi = searchModel.ListDonViBoPhan != null ? String.Join(",", searchModel.ListDonViBoPhan) : null;
            //searchModel.ListStringCongCu = searchModel.ListCongCu != null ? String.Join(",", searchModel.ListCongCu) : null;
            var obj = _congCuDungCuService.BaoCaoLietKeCCDC(NgayBaoCao: (DateTime)searchModel.NgayBaoCao, DonViId: _workContext.CurrentDonVi.ID, NhomCongCuId: searchModel.ListStringCongCu, BoPhanId: searchModel.ListStringDonVi, DonViTien: searchModel.DonViTien);
            model.DataSource = obj;

            return ShowViewReport(model);
        }

        public virtual IActionResult CCDC_TangGiamCongCu()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCCDC))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCCDCSearchModel(searchModel);
            return PartialView(searchModel);
        }

        public virtual IActionResult CCDC_TangGiamCongCu_Report_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            //var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            //var cauHinhModel = new CauHinhBaoCaoModel();
            //var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            //if (cauHinh.BaoCao != null)
            //{
            //	cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == enumMA_BAO_CAO.CCDC_TangGiamCongCu).FirstOrDefault();
            //}
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.CCDC_TangGiamCongCu);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            searchModel.TenBoPhan = _reportModelFactory.PrePareStringDonViBoPhanModel(searchModel.ListDonViBoPhan);
            searchModel.StringLyDoTangGiam = _reportModelFactory.PrePareListLyDoTangGiamStringModel(searchModel.ListLyDoIdString);
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            _reportModelFactory.PrePareLyDoTangVaLyDoGiamSearchModel(searchModel);
            XtraReport model = new CCDC_TangAndGiam(searchModel, cauHinhModel, cauHinhChunghModel);
            //searchModel.ListStringDonVi = searchModel.ListDonViBoPhan != null ? String.Join(",", searchModel.ListDonViBoPhan) : null;
            //searchModel.ListStringCongCu = searchModel.ListCongCu != null ? String.Join(",", searchModel.ListCongCu) : null;
            var obj = _congCuDungCuService.BaoCaoTangGiamCongCu(NgayFrom: (DateTime)searchModel.NgayBatDau, NgayTo: (DateTime)searchModel.NgayKetThuc, DonViId: _workContext.CurrentDonVi.ID, BoPhanId: searchModel.ListStringDonVi, NhomCongCuId: searchModel.ListStringCongCu, TangOrGiam: searchModel.LyDoID, DonViTien: searchModel.DonViTien, ListLyDoGiam: searchModel.StringListLyDoGiam, ListLyDoTang: searchModel.StringListLyDoTang);
            model.DataSource = obj;

            return ShowViewReport(model);
        }

        public virtual IActionResult CCDC_TangCongCu()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCCDC))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCCDCSearchModel(searchModel);
            searchModel.LyDoID = 1;
            return PartialView(searchModel);
        }

        public virtual IActionResult CCDC_GiamCongCu()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCCDC))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCCDCSearchModel(searchModel);
            searchModel.LyDoID = 2;
            return PartialView(searchModel);
        }

        public virtual IActionResult CCDC_TangOrGiamCongCu_Report_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var mabaocao = enumMA_BAO_CAO.CCDC_TangGiamCongCu;
            if (searchModel.LyDoID == 1)//tăng
            {
                mabaocao = enumMA_BAO_CAO.CCDC_TangCongCu;
            }
            else if (searchModel.LyDoID == 2)// giảm
            {
                mabaocao = enumMA_BAO_CAO.CCDC_GiamCongCu;
            }
            //var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            //var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            //if (cauHinh.BaoCao != null)
            //{
            //	cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == mabaocao).FirstOrDefault();
            //}
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), mabaocao);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            searchModel.TenBoPhan = _reportModelFactory.PrePareStringDonViBoPhanModel(searchModel.ListDonViBoPhan);
            var ListIdLyDo = searchModel.ListLyDoIdString.ToListInt();
            if (searchModel.LyDoID == 2)
            {
                searchModel.StringLyDoTangGiam = _reportModelFactory.PrePareListLyDoGiamStringModel(ListIdLyDo);
                searchModel.StringListLyDoGiam = string.IsNullOrEmpty(searchModel.ListLyDoIdString) ? null : searchModel.ListLyDoIdString;
            }
            else
            {
                searchModel.StringLyDoTangGiam = _reportModelFactory.PrePareListLyDoTangStringModel(ListIdLyDo);
                searchModel.StringListLyDoTang = string.IsNullOrEmpty(searchModel.ListLyDoIdString) ? null : searchModel.ListLyDoIdString;
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new CCDC_GiamOrTang(searchModel, cauHinhModel, cauHinhChunghModel);
            //searchModel.ListStringDonVi = searchModel.ListDonViBoPhan != null ? String.Join(",", searchModel.ListDonViBoPhan) : null;
            //searchModel.ListStringCongCu = searchModel.ListCongCu != null ? String.Join(",", searchModel.ListCongCu) : null;
            var obj = _congCuDungCuService.BaoCaoTangGiamCongCu(NgayFrom: (DateTime)searchModel.NgayBatDau, NgayTo: (DateTime)searchModel.NgayKetThuc, DonViId: _workContext.CurrentDonVi.ID, BoPhanId: searchModel.ListStringDonVi, NhomCongCuId: searchModel.ListStringCongCu, TangOrGiam: searchModel.LyDoID, DonViTien: searchModel.DonViTien, ListLyDoGiam: searchModel.StringListLyDoGiam, ListLyDoTang: searchModel.StringListLyDoTang);
            model.DataSource = obj;

            return ShowViewReport(model);
        }

        public virtual IActionResult CCDC_SuaChuaCongCu()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCCDC))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCCDCSearchModel(searchModel);
            return PartialView(searchModel);
        }

        public virtual IActionResult CCDC_SuaChuaCongCu_Report_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            //var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            //var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            //if (cauHinh.BaoCao != null)
            //{
            //	cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == enumMA_BAO_CAO.CCDC_SuaChuaCongCu).FirstOrDefault();
            //}
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.CCDC_SuaChuaCongCu);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            searchModel.TenBoPhan = _reportModelFactory.PrePareStringDonViBoPhanModel(searchModel.ListDonViBoPhan);
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new CCDC_SuaChuaBaoDuong(searchModel, cauHinhModel, cauHinhChunghModel);
            //searchModel.ListStringDonVi = searchModel.ListDonViBoPhan != null ? String.Join(",", searchModel.ListDonViBoPhan) : null;
            //searchModel.ListStringCongCu = searchModel.ListCongCu != null ? String.Join(",", searchModel.ListCongCu) : null;
            var obj = _congCuDungCuService.BaoCaoSuaChuaCongCu(NgayFrom: (DateTime)searchModel.NgayBatDau, NgayTo: (DateTime)searchModel.NgayKetThuc, DonViId: _workContext.CurrentDonVi.ID, BoPhanId: searchModel.ListStringDonVi, NhomCongCuId: searchModel.ListStringCongCu, DonViTien: searchModel.DonViTien);
            model.DataSource = obj;

            return ShowViewReport(model);
        }

        public virtual IActionResult CCDC_TongHopTon()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCCDC))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCCDCSearchModel(searchModel);
            return PartialView(searchModel);
        }

        public virtual IActionResult CCDC_TongHopTon_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.CCDC_TongHopTon);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());

            searchModel.TenBoPhan = _reportModelFactory.PrePareStringDonViBoPhanModel(searchModel.ListDonViBoPhan);
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);

            XtraReport model = new CCDC_TongHopTon(searchModel, cauHinhModel, cauHinhChunghModel);

            var obj = _congCuDungCuService.BaoCaoTongHopTon(NgayFrom: (DateTime)searchModel.NgayBatDau, NgayTo: (DateTime)searchModel.NgayKetThuc, DonViId: _workContext.CurrentDonVi.ID, DonViTien: searchModel.DonViTien);
            model.DataSource = obj;

            return ShowViewReport(model);
        }

        public virtual IActionResult CCDC_TongHop(int MauSo = 1)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCCDC))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCCDCSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.CCDC_TongHop;
            searchModel.DDLBacDonVi = new enumCapDonVi().ToSelectList().ToList();
            searchModel.MauSo = MauSo;
            return PartialView(searchModel);
        }

        public virtual IActionResult CCDC_TongHop_Report_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            //var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            //var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            //if (cauHinh.BaoCao != null)
            //{
            //	cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == enumMA_BAO_CAO.CCDC_LietKe).FirstOrDefault();
            //}
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.CCDC_LietKe);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            searchModel.TenBoPhan = _reportModelFactory.PrePareStringDonViBoPhanModel(searchModel.ListStringDonVi);
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new CCDC_TongHop(searchModel, cauHinhModel, cauHinhChunghModel);
            //searchModel.ListStringDonVi = searchModel.ListDonViBoPhan != null ? String.Join(",", searchModel.ListDonViBoPhan) : null;
            //searchModel.ListStringCongCu = searchModel.ListCongCu != null ? String.Join(",", searchModel.ListCongCu) : null;
            var obj = _congCuDungCuService.BaoCaoTongHopCCDC(NgayBaoCao: (DateTime)searchModel.NgayBaoCao, StringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NhomCongCuId: searchModel.ListStringCongCu, BoPhanId: searchModel.ListStringDonVi, DonViTien: searchModel.DonViTien, MauSo: searchModel.MauSo, BacDonVi: searchModel.BacDonVi);
            model.DataSource = obj;

            return ShowViewReport(model);
        }

        public virtual IActionResult CCDC_BaoHongMat()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCCDC))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCCDCSearchModel(searchModel);
            return PartialView(searchModel);
        }

        public virtual IActionResult CCDC_BaoHongMat_Report_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            //var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            //var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            //if (cauHinh.BaoCao != null)
            //{
            //	cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == enumMA_BAO_CAO.CCDC_BaoHongMat).FirstOrDefault();
            //}
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.CCDC_BaoHongMat);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            searchModel.TenBoPhan = _reportModelFactory.PrePareStringDonViBoPhanModel(searchModel.ListDonViBoPhan);
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new CCDC_HongMat(searchModel, cauHinhModel, cauHinhChunghModel);
            //searchModel.ListStringDonVi = searchModel.ListDonViBoPhan != null ? String.Join(",", searchModel.ListDonViBoPhan) : null;
            //searchModel.ListStringCongCu = searchModel.ListCongCu != null ? String.Join(",", searchModel.ListCongCu) : null;
            var obj = _congCuDungCuService.BaoCaoHongMatCongCu(NgayFrom: (DateTime)searchModel.NgayBatDau, NgayTo: (DateTime)searchModel.NgayKetThuc, DonViId: _workContext.CurrentDonVi.ID, BoPhanId: searchModel.ListStringDonVi, NhomCongCuId: searchModel.ListStringCongCu, DonViTien: searchModel.DonViTien);
            model.DataSource = obj;

            return ShowViewReport(model);
        }

        public virtual IActionResult CCDC_PhanBoCongCu()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCCDC))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCCDCSearchModel(searchModel);
            return PartialView(searchModel);
        }

        public virtual IActionResult CCDC_PhanBoCongCu_Report_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            //var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            //var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            //if (cauHinh.BaoCao != null)
            //{
            //	cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == enumMA_BAO_CAO.CCDC_PhanBoCongCu).FirstOrDefault();
            //}
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.CCDC_PhanBoCongCu);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            searchModel.TenBoPhan = _reportModelFactory.PrePareStringDonViBoPhanModel(searchModel.ListDonViBoPhan);
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new CCDC_TongHopPhanBo(searchModel, cauHinhModel, cauHinhChunghModel);
            //searchModel.ListStringDonVi = searchModel.ListDonViBoPhan != null ? String.Join(",", searchModel.ListDonViBoPhan) : null;
            //searchModel.ListStringCongCu = searchModel.ListCongCu != null ? String.Join(",", searchModel.ListCongCu) : null;
            var obj = _congCuDungCuService.BaoCaoPhanBoCongCu(NgayBaoCao: (DateTime)searchModel.NgayBaoCao, DonViId: _workContext.CurrentDonVi.ID, BoPhanId: searchModel.ListStringDonVi, NhomCongCuId: searchModel.ListStringCongCu, DonViTien: searchModel.DonViTien);
            model.DataSource = obj;
            var storage = new MemoryDocumentStorage();
            var cachedReportSource = new CachedReportSource(model, storage);

            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult Print_CCDC_PhanBoCongCu(BaoCaoPhanBoSearchModel searchModel)
        {
            //var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            //var cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == "CCDC_PhanBoCongCu").FirstOrDefault();
            //var report = new CCDC_TongHopPhanBo(_workContext, searchModel.NgayBaoCao, _donViBoPhanService, searchModel.DonViBoPhan, cauHinhModel);
            //var obj = _congCuDungCuService.BaoCaoPhanBoCongCu(NgayBaoCao: searchModel.NgayBaoCao, DonViId: _workContext.CurrentDonVi.ID, BoPhanId: searchModel.DonViBoPhan, NhomCongCuId: searchModel.NhomCongCu);
            //report.DataSource = obj;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    var storage = new MemoryDocumentStorage();
            //    var cachedReportSource = new CachedReportSource(report, storage);
            //    cachedReportSource.CreateDocument();
            //    cachedReportSource.PrintingSystem.ExportToXlsx(ms, new XlsxExportOptions());
            //    return File(ms.ToArray(), MimeTypes.TextXlsx, "CCDC_PhanBoCongCu.xlsx");
            //}
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    report.ExportToXlsx(ms, new XlsxExportOptions());
            //    return File(ms.ToArray(), MimeTypes.TextXlsx, "CCDC_PhanBoCongCu.xlsx");
            //}

            var report = new TestCacheSourchReport();
            report.DataSource = _nhatKyService.GetAllNhatKys().Select(c => c.ToModel<NhatKyModel>());
            using (MemoryStream ms = new MemoryStream())
            {
                var storage = new MemoryDocumentStorage();
                var cachedReportSource = new CachedReportSource(report, storage);
                cachedReportSource.CreateDocument();
                cachedReportSource.PrintingSystem.ExportToXlsx(ms, new XlsxExportOptions());
                return File(ms.ToArray(), MimeTypes.TextXlsx, "test.xlsx");
            }
        }

        [HttpPost]
        public virtual IActionResult Print_CCDC_PhanBoCongCuNoCache(BaoCaoPhanBoSearchModel searchModel)
        {
            var report = new TestCacheSourchReport();
            report.DataSource = _nhatKyService.GetAllNhatKys().Select(c => c.ToModel<NhatKyModel>());
            using (MemoryStream ms = new MemoryStream())
            {
                report.ExportToXlsx(ms, new XlsxExportOptions());
                return File(ms.ToArray(), MimeTypes.TextXlsx, "test.xlsx");
            }
        }

        //[HttpPost]
        //public virtual IActionResult CCDC_BienBanKiemKe(BaoCaoTaiSanChiTietSearchModel searchModel)
        //{
        //	var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.CCDC_BienBanKiemKe);
        //	var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
        //	var donvibophan = _donViBoPhanService.GetDonViBoPhanById(searchModel.DonViBoPhan);
        //	_reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
        //	XtraReport model = new Reports.TS_CDKT.BienBanKiemKe(searchModel, cauHinhModel, cauHinhChunghModel);
        //	var obj = _congCuDungCuService.BienBanKiemke(searchModel.KiemKeId, DonViTien: searchModel.DonViTien);
        //	model.DataSource = obj;
        //	return ShowViewReport(model);
        //}

        #endregion CCDC

        #region Báo cáo theo chế độ kế toán

        public virtual IActionResult CDKT_KiemKeTaiSan()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCheDoKeToan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true, valSelected: 0, valueFirstRow: "0").ToList();
            searchModel.MaBaoCao = enumMA_BAO_CAO.CDKT_KiemKeTaiSan;
            return PartialView(searchModel);
        }

        public virtual IActionResult CDKT_KiemKeTaiSan_Report_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.CDKT_KiemKeTaiSan);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            var donvibophan = _donViBoPhanService.GetDonViBoPhanById(searchModel.DonViBoPhan);
            searchModel.TenBoPhan = donvibophan != null ? donvibophan.TEN : "";
            searchModel.TEN_DON_VI = _workContext.CurrentDonVi.TEN_DON_VI;
            searchModel.MA_DON_VI = _workContext.CurrentDonVi.MA_DON_VI;
            searchModel.MA_QUAN_HE_NGAN_SACH = _donViService.GetDonViById(_workContext.CurrentDonVi.ID)?.MA_DVQHNS;
            XtraReport model = new CDKT_KiemKeTaiSan(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _cheDoKeToanService.BaoCaoKiemKeTaiSan(NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, DonViId: _workContext.CurrentDonVi.ID, BoPhanIds: searchModel.ListDonViBoPhan, MaNhomTaiSan: searchModel.StringLoaiTaiSan, DonViTien: searchModel.DonViTien);
            model.DataSource = obj;

            return ShowViewReport(model);
        }

        public virtual IActionResult CDKT_KiemKeTaiSanPhongBan()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCheDoKeToan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true, valSelected: 0, valueFirstRow: "0").ToList();
            searchModel.MaBaoCao = enumMA_BAO_CAO.CDKT_KiemKeTaiSanPhongBan;
            return PartialView(searchModel);
        }

        public virtual IActionResult CDKT_KiemKeTaiSanPhongBan_Report_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.CDKT_KiemKeTaiSanPhongBan);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            var donvibophan = _donViBoPhanService.GetDonViBoPhanById(searchModel.DonViBoPhan);
            searchModel.TenBoPhan = donvibophan != null ? donvibophan.TEN : "";
            searchModel.TEN_DON_VI = _workContext.CurrentDonVi.TEN_DON_VI;
            searchModel.MA_DON_VI = _workContext.CurrentDonVi.MA_DON_VI;
            searchModel.MA_QUAN_HE_NGAN_SACH = _donViService.GetDonViById(_workContext.CurrentDonVi.ID)?.MA_DVQHNS;
            XtraReport model = new CDKT_KiemKeTaiSanPhongBan(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _cheDoKeToanService.BaoCaoKiemKeTaiSanPhongBan(NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, DonViId: _workContext.CurrentDonVi.ID, ListDonViBoPhan: searchModel.ListDonViBoPhan, MaNhomTaiSan: searchModel.StringLoaiTaiSan, DonViTien: searchModel.DonViTien);
            model.DataSource = obj;

            return ShowViewReport(model);
        }

        public virtual IActionResult CDKT_BS03_MS_B04H_BC_THTANGGIAM_TSCD()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();

            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCheDoKeToanSearchModel(searchModel);
            searchModel.DDLLyDoBienDong = _lyDoBienDongModelFactory.PrepareSelectListLyDoBienDongByBaoCao(loailydoId: 1).ToList();
            searchModel.MaBaoCao = enumMA_BAO_CAO.CDKT_BS03_MS_B04H_BC_THTANGGIAM_TSCD;
            searchModel.DDLLyDoBienDong = new enumLyDoTangGiamForBaoCao().ToSelectList().ToList();
            return PartialView(searchModel);
        }

        public virtual IActionResult CDKT_BS03_MS_B04H_BC_THTANGGIAM_TSCD_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.CDKT_BS03_MS_B04H_BC_THTANGGIAM_TSCD) ?? new CauHinhBaoCaoModel();
            }
            searchModel.MA_QUAN_HE_NGAN_SACH = _donViService.GetDonViById(searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID)?.MA_DVQHNS;
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);

            XtraReport model = new TS_CDKT_BS03_MS_B04H_BC_THTANGGIAM_TSCD(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var obj = _cheDoKeToanService.CDKT_BS03_MS_B04H_BC_THTANGGIAM_TSCD(donViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, Year: (int)searchModel.NamBaoCao, loaiTaiSan: (int)searchModel.LoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, bacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, LyDo: searchModel.LyDoID, ListDonViBoPhan: searchModel.ListDonViBoPhan);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult CDKT_BS03_MS_B04H_BC_THTANGGIAM_TSCD_CHAY_NGAM(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.CDKT_BS03_MS_B04H_BC_THTANGGIAM_TSCD, searchModel, searchModel.FileType, typeof(TS_CDKT_BS03_MS_B04H_BC_THTANGGIAM_TSCD).FullName, typeof(B04HQD19).FullName);
                _cheDoKeToanService.CDKT_BS03_MS_B04H_BC_THTANGGIAM_TSCD(donViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, Year: (int)searchModel.NamBaoCao, loaiTaiSan: (int)searchModel.LoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, bacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, LyDo: searchModel.LyDoID, ListDonViBoPhan: searchModel.ListDonViBoPhan, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoCheDoKeToan });
        }

        public virtual IActionResult CDKT_BS04_MS_C55A_HD_BANG_TINH_HAO_MON()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCheDoKeToanSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.CDKT_BS04_MS_C55A_HD_BANG_TINH_HAO_MON;
            return PartialView(searchModel);
        }

        public virtual IActionResult CDKT_BS04_MS_C55A_HD_BANG_TINH_HAO_MON_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.CDKT_BS04_MS_C55A_HD_BANG_TINH_HAO_MON) ?? new CauHinhBaoCaoModel();
            }
            searchModel.MA_QUAN_HE_NGAN_SACH = _donViService.GetDonViById(searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID)?.MA_DVQHNS;
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new TS_CDKT_BS04_MS_C55A_HD_BANG_TINH_HAO_MON(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var obj = _cheDoKeToanService.CDKT_BS04_MS_C55A_HD_BANG_TINH_HAO_MON(Nam: (int)searchModel.NamBaoCao, DonViId: _workContext.CurrentDonVi.ID, MaNhomTaiSan: searchModel.StringLoaiTaiSan, BoPhanId: searchModel.DonViBoPhan, DonViTien: searchModel.DonViTien);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult CDKT_BS05_BANG_TINH_KHAU_HAO_TSCD()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCheDoKeToanSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.CDKT_BS05_BANG_TINH_KHAU_HAO_TSCD;
            return PartialView(searchModel);
        }

        public virtual IActionResult CDKT_BS05_BANG_TINH_KHAU_HAO_TSCD_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.CDKT_BS05_BANG_TINH_KHAU_HAO_TSCD) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new TS_CDKT_BS05_BANG_TINH_KHAU_HAO_TSCD(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var obj = _cheDoKeToanService.CDKT_BS05_BANG_TINH_KHAU_HAO_TSCD(Nam: (int)searchModel.NamBaoCao, DonViId: _workContext.CurrentDonVi.ID, MaNhomTaiSan: searchModel.StringLoaiTaiSan, BoPhanId: searchModel.DonViBoPhan, DonViTien: searchModel.DonViTien);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult CDKT_BS06_SOGHITANG_TSCD()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCheDoKeToanSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.CDKT_BS06_SOGHITANG_TSCD;
            return PartialView(searchModel);
        }

        public virtual IActionResult CDKT_BS06_SOGHITANG_TSCD_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.CDKT_BS06_SOGHITANG_TSCD) ?? new CauHinhBaoCaoModel();
            }

            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new TS_CDKT_BS06_SOGHITANG_TSCD(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var obj = _cheDoKeToanService.CDKT_BS06_SOGHITANG_TSCD(Nam: (int)searchModel.NamBaoCao, DonViId: _workContext.CurrentDonVi.ID, MaNhomTaiSan: searchModel.StringLoaiTaiSan, BoPhanId: searchModel.DonViBoPhan, DonViTien: searchModel.DonViTien, stringLyDo: searchModel.StringLyDoID);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult CDKT_BS07_SOGHIGIAM_TSCD()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCheDoKeToanSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.CDKT_BS07_SOGHIGIAM_TSCD;
            return PartialView(searchModel);
        }

        public virtual IActionResult CDKT_BS07_SOGHIGIAM_TSCD_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.CDKT_BS07_SOGHIGIAM_TSCD) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new TS_CDKT_BS07_SOGHIGIAM_TSCD(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var obj = _cheDoKeToanService.CDKT_BS07_SOGHIGIAM_TSCD(Nam: (int)searchModel.NamBaoCao, DonViId: _workContext.CurrentDonVi.ID, MaNhomTaiSan: searchModel.StringLoaiTaiSan, BoPhanId: searchModel.DonViBoPhan, DonViTien: searchModel.DonViTien, stringLyDo: searchModel.StringLyDoID);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult CDKT_BS08_MS_S31H_SO_TSCD()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCheDoKeToanSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.CDKT_BS08_MS_S31H_SO_TSCD;
            return PartialView(searchModel);
        }

        public virtual IActionResult CDKT_BS08_MS_S31H_SO_TSCD_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.CDKT_BS08_MS_S31H_SO_TSCD) ?? new CauHinhBaoCaoModel();
            }

            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new TS_CDKT_BS08_MS_S31H_SO_TSCD(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var obj = _cheDoKeToanService.CDKT_BTC_B08_S31H_SO_TS(Nam: (int)searchModel.NamBaoCao, DonViId: _workContext.CurrentDonVi.ID, MaNhomTaiSan: searchModel.StringLoaiTaiSan, ListDonViBoPhan: searchModel.ListDonViBoPhan, DonViTien: searchModel.DonViTien, isInTrongKy: searchModel.isInTrongKy);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult CDKT_B09_S32H_SO_TS()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCheDoKeToanSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.CDKT_B09_S32H_SO_TS;
            return PartialView(searchModel);
        }

        public virtual IActionResult CDKT_B09_S32H_SO_TS_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.CDKT_B09_S32H_SO_TS) ?? new CauHinhBaoCaoModel();
            }

            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new CDKT_B09_S32H_SO_TS(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var obj = _cheDoKeToanService.CDKT_B09_S32H_SO_TS(Nam: (int)searchModel.NamBaoCao, DonViId: _workContext.CurrentDonVi.ID, MaNhomTaiSan: searchModel.StringLoaiTaiSan, BoPhanId: searchModel.DonViBoPhan, DonViTien: searchModel.DonViTien, isInTrongKy: searchModel.isInTrongKy);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult CDKT_B10_S32H_CCDC()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCheDoKeToanSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.CDKT_B10_S32H_CCDC;
            return PartialView(searchModel);
        }

        public virtual IActionResult CDKT_B10_S32H_CCDC_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.CDKT_B10_S32H_CCDC) ?? new CauHinhBaoCaoModel();
            }

            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new CDKT_B10_S32H_CCDC(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var obj = _cheDoKeToanService.CDKT_B10_S32H_CCDC(Nam: (int)searchModel.NamBaoCao, DonViId: _workContext.CurrentDonVi.ID, MaNhomTaiSan: searchModel.StringLoaiTaiSan, BoPhanId: searchModel.DonViBoPhan, DonViTien: searchModel.DonViTien);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult CDKT_B11_S32H_SO_TS_CCDC()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCheDoKeToanSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.CDKT_B11_S32H_SO_TS_CCDC;
            return PartialView(searchModel);
        }

        public virtual IActionResult CDKT_B11_S32H_SO_TS_CCDC_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.CDKT_B11_S32H_SO_TS_CCDC) ?? new CauHinhBaoCaoModel();
            }

            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new CDKT_B11_S32H_SO_TS_CDCC(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var obj = _cheDoKeToanService.CDKT_B11_S32H_SO_TS_CDCC(Nam: (int)searchModel.NamBaoCao, DonViId: _workContext.CurrentDonVi.ID, MaNhomTaiSan: searchModel.StringLoaiTaiSan, BoPhanId: searchModel.DonViBoPhan, DonViTien: searchModel.DonViTien);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult CDKT_B08_S24H_SO_TS()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCheDoKeToanSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.CDKT_B08_S24H_SO_TS;
            return PartialView(searchModel);
        }

        public virtual IActionResult CDKT_B08_S24H_SO_TS_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.CDKT_B08_S24H_SO_TS) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new CDKT_BTC_B08_S24H_SO_TS(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var obj = _cheDoKeToanService.CDKT_BTC_B08_S24H_SO_TS(Nam: (int)searchModel.NamBaoCao, DonViId: _workContext.CurrentDonVi.ID, MaNhomTaiSan: searchModel.StringLoaiTaiSan, ListDonViBoPhan: searchModel.ListDonViBoPhan, DonViTien: searchModel.DonViTien, isInTrongKy: searchModel.isInTrongKy);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult CDKT_B11_S24H_SO_TS_CCDC()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCheDoKeToanSearchModel(searchModel);
            searchModel.DDLNhomCongCu = _nhomCongCuModelFactory.PrepareDDLNhomCongCu(_workContext.CurrentDonVi.ID);
            searchModel.MaBaoCao = enumMA_BAO_CAO.CDKT_B11_S24H_SO_TS_CCDC;
            return PartialView(searchModel);
        }

        public virtual IActionResult CDKT_B11_S24H_SO_TS_CCDC_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.CDKT_B11_S24H_SO_TS_CCDC) ?? new CauHinhBaoCaoModel();
            }

            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new CDKT_BTC_B11_S24H_SO_TS_CDCC(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var obj = _cheDoKeToanService.CDKT_BTC_B11_S24H_SO_TS_CDCC(Nam: (int)searchModel.NamBaoCao, DonViId: _workContext.CurrentDonVi.ID, MaNhomTaiSan: searchModel.StringLoaiTaiSan, BoPhanId: searchModel.ListDonViBoPhan, DonViTien: searchModel.DonViTien, isInTrongKy: searchModel.isInTrongKy);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult CDKT_BienBanKiemKe(int KiemKeId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoCheDoKeToanSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.CDKT_BienBanKiemKe;
            searchModel.KiemKeId = KiemKeId;
            return PartialView(searchModel);
        }

        public virtual IActionResult CDKT_BienBanKiemKe_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.CDKT_BienBanKiemKe);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            var donvibophan = _donViBoPhanService.GetDonViBoPhanById(searchModel.DonViBoPhan);
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            var bienbankiem = _kiemKeTaiSanService.GetTaiSanKiemKeById(searchModel.KiemKeId);
            if (bienbankiem != null) searchModel.NgayBaoCao = bienbankiem.NGAY_KIEM_KE;
            var hoidongkiemke = _taiSanKiemKeHoiDongService.GetTaiSanKiemKeHoiDongs(KiemKeId: searchModel.KiemKeId);
            if (hoidongkiemke.Count() > 0)
            {
                List<string> listHoiDong = new List<string>();
                foreach (var hoidong in hoidongkiemke)
                {
                    listHoiDong.Add("Ông bà: " + hoidong.HO_TEN + "		" + "Chức vụ: " + hoidong.CHUC_VU + "		" + "Đại diện: " + hoidong.DAI_DIEN + "		" + _nhanHienThiService.GetGiaTriEnum<enumViTriKiemKeHoiDong>((enumViTriKiemKeHoiDong)hoidong.VI_TRI_ID));
                }
                searchModel.HoiDongKiemKe = string.Join("\r\n", listHoiDong);
            }
            XtraReport model = new Reports.TS_CDKT.BienBanKiemKe(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _cheDoKeToanService.BienBanKiemke(searchModel.KiemKeId, DonViTien: searchModel.DonViTien);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        #endregion Báo cáo theo chế độ kế toán

        #region TSTD

        //TSTD
        public virtual IActionResult TSTD_ThongTinTaiSan()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTSTD))
                return AccessDeniedView();
            var searchModel = new BaoCaoThongTinTSTDSearchModel();
            //searchModel.DDLDonVi = _donViModelFactory.PrepareSelectListDonVi(isAddFirst: false).ToList();
            searchModel.DonVi = _workContext.CurrentDonVi.ID;
            searchModel.DDLDonViTien = new enumDonViTienTSTD().ToSelectList().ToList();
            searchModel.DDLDonViDienTich = new enumDonViDienTichTSTD().ToSelectList().ToList();
            searchModel.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(cheDoId: 23, isAddFirst: false).ToList();
            return View(searchModel);
        }

        [HttpPost]
        public virtual IActionResult TSTD_ThongTinTaiSan(BaoCaoThongTinTSTDSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TSTD_ThongTinTaiSan);
            }
            XtraReport model = new TSTD_ThonTinTaiSan(_workContext, (DateTime)searchModel.NgayBatDau, (DateTime)searchModel.NgayKetThuc, _donViBoPhanService, searchModel.DonVi, cauHinhModel, searchModel.DonViTien, searchModel.DonViDienTich);
            var obj = _taiSanToanDanService.BaoCaoThongTinTSTDs(NgayBatDau: searchModel.NgayBatDau, DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien.ToIntDonViTien(), DonViDienTich: searchModel.DonViDienTich.ToIntDonViDienTich());
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TSTD_TinhHinhXuLy()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTSTD))
                return AccessDeniedView();
            var searchModel = new BaoCaoHinhThucXuLyTSTDSearchModel();
            // searchModel.DDLDonVi = _donViModelFactory.PrepareSelectListDonVi(isAddFirst: false).ToList();
            searchModel.DonVi = (int)_workContext.CurrentDonVi.ID;
            searchModel.DDLDonViTien = new enumDonViTienTSTD().ToSelectList().ToList();
            searchModel.DDLDonViDienTich = new enumDonViDienTichTSTD().ToSelectList().ToList();
            return View(searchModel);
        }

        [HttpPost]
        public virtual IActionResult TSTD_TinhHinhXuLy(BaoCaoHinhThucXuLyTSTDSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TSTD_TinhHinhXuLy);
            }
            XtraReport model = new TSTD_TinhHinhXuLy(_workContext, (DateTime)searchModel.NgayBatDau, (DateTime)searchModel.NgayKetThuc, _donViBoPhanService, searchModel.DonVi, cauHinhModel, searchModel.DonViTien, searchModel.DonViDienTich);
            var obj = _taiSanToanDanService.BaoCaoTinhHinhXuLyTSTDs(NgayBatDau: searchModel.NgayBatDau, DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, LoaiXuLyId: (int)enumLoaiXuLy.KetQua, DonViTien: searchModel.DonViTien.ToIntDonViTien(), DonViDienTich: searchModel.DonViDienTich.ToIntDonViDienTich());
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TSTD_TongHop()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTSTD))
                return AccessDeniedView();
            var searchModel = new BaoCaoThongTinTSTDSearchModel();
            // searchModel.DDLDonVi = _donViModelFactory.PrepareSelectListDonVi(isAddFirst: false).ToList();
            searchModel.DonVi = (int)_workContext.CurrentDonVi.ID;
            searchModel.DDLDonViTien = new enumDonViTienTSTD().ToSelectList().ToList();
            searchModel.DDLDonViDienTich = new enumDonViDienTichTSTD().ToSelectList().ToList();
            return View(searchModel);
        }

        [HttpPost]
        public virtual IActionResult TSTD_TongHop(BaoCaoThongTinTSTDSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TSTD_TongHop);
            }
            XtraReport model = new TSTD_TongHop(_workContext, (DateTime)searchModel.NgayBatDau, (DateTime)searchModel.NgayKetThuc, _donViBoPhanService, searchModel.DonVi, cauHinhModel, searchModel.DonViTien, searchModel.DonViDienTich);
            var obj = _taiSanToanDanService.BaoCaoTongHopTSTDs(NgayBatDau: searchModel.NgayBatDau, DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, LoaiXuLyId: (int)enumLoaiXuLy.KetQua, DonViTien: searchModel.DonViTien.ToIntDonViTien(), DonViDienTich: searchModel.DonViDienTich.ToIntDonViDienTich());
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TSTD_Giam(int MauGiamTSTD)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTSTD))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTDSearchModel(searchModel, true);
            if (MauGiamTSTD == 1)
            {
                searchModel.MaBaoCao = enumMA_BAO_CAO.TSTD_MAU_04_BC_XLSHTD;
            }
            else
            {
                searchModel.MaBaoCao = enumMA_BAO_CAO.TSTD_MAU_06_BC_XLSHTD;
            }
            searchModel.MauGiamTSTD = MauGiamTSTD;
            return PartialView(searchModel);
        }

        public virtual IActionResult TSTD_Giam_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TSTD_MAU_04_BC_XLSHTD) ?? new CauHinhBaoCaoModel();
            }
            searchModel.enumListLoaiHinhTSTD = new List<enumNHAN_HIEN_THI_LOAI_HINH_TSTD>() { enumNHAN_HIEN_THI_LOAI_HINH_TSTD.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TSTD.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TSTD.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TSTD.TAI_SAN_KHAC };
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            searchModel.TenTaiSan = _nguonGocTaiSanModelFactory.PrepareTenNguonGocByListId(searchModel.StringNguonGocTaiSan);
            XtraReport model = new TSTD_Giam(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _taiSanToanDanService.TSTD_MAU_04_BC(searchModel.NgayBatDau, searchModel.NgayKetThuc, (int)_workContext.CurrentDonVi.ID, searchModel.DonViTien, searchModel.DonViDienTich, searchModel.DonViKhoiLuong, searchModel.StringNguonGocTaiSan, searchModel.BacNguonGocTSTD, searchModel.MauGiamTSTD);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TSTD_Tang()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTSTD))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTDSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TSTD_MAU_03_BC_XLSHTD;
            return PartialView(searchModel);
        }

        public virtual IActionResult TSTD_Tang_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TSTD_MAU_03_BC_XLSHTD) ?? new CauHinhBaoCaoModel();
            }
            searchModel.enumListLoaiHinhTSTD = new List<enumNHAN_HIEN_THI_LOAI_HINH_TSTD>() { enumNHAN_HIEN_THI_LOAI_HINH_TSTD.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TSTD.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TSTD.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TSTD.TAI_SAN_KHAC };
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            searchModel.TenTaiSan = _nguonGocTaiSanModelFactory.PrepareTenNguonGocByListId(searchModel.StringNguonGocTaiSan);
            XtraReport model = new TSTD_Tang(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _taiSanToanDanService.TSTD_MAU_03_BC(searchModel.NgayBatDau, searchModel.NgayKetThuc, (int)_workContext.CurrentDonVi.ID, searchModel.DonViTien, searchModel.DonViDienTich, searchModel.DonViKhoiLuong, searchModel.BacNguonGocTSTD, searchModel.StringNguonGocTaiSan);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TSTD_TongHopSoTienThu()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTSTD))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTDSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TSTD_MAU_05_BC_XLSHTD;
            return PartialView(searchModel);
        }

        public virtual IActionResult TSTD_TongHopSoTienThu_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TSTD_MAU_05_BC_XLSHTD) ?? new CauHinhBaoCaoModel();
            }

            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            searchModel.TenTaiSan = _nguonGocTaiSanModelFactory.PrepareTenNguonGocByListId(searchModel.StringNguonGocTaiSan);
            XtraReport model = new TSTD_TongHopSoTienThu(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _taiSanToanDanService.TSTD_MAU_05_BC(searchModel.NgayBatDau, searchModel.NgayKetThuc, (int)_workContext.CurrentDonVi.ID, searchModel.DonViTien, searchModel.DonViDienTich, searchModel.DonViKhoiLuong, searchModel.StringNguonGocTaiSan, searchModel.LyDoGiamTSTD);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TSTD_TangGiam(int MauSo = 1)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTSTD))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTDSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TSTD_MAU_02_BC_XLSHTD;
            searchModel.MauSo = MauSo;
            return PartialView(searchModel);
        }

        public virtual IActionResult TSTD_TangGiam_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TSTD_MAU_02_BC_XLSHTD) ?? new CauHinhBaoCaoModel();
            }
            searchModel.enumListLoaiHinhTSTD = new List<enumNHAN_HIEN_THI_LOAI_HINH_TSTD>() { enumNHAN_HIEN_THI_LOAI_HINH_TSTD.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TSTD.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TSTD.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TSTD.TAI_SAN_KHAC };
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            searchModel.TenTaiSan = _nguonGocTaiSanModelFactory.PrepareTenNguonGocByListId(searchModel.StringNguonGocTaiSan);
            XtraReport model = new TSTD_TangGiam(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _taiSanToanDanService.TSTD_MAU_02_BC(searchModel.NgayBatDau, searchModel.NgayKetThuc, (int)_workContext.CurrentDonVi.ID, searchModel.DonViTien, searchModel.DonViDienTich, searchModel.DonViKhoiLuong, searchModel.StringNguonGocTaiSan, MauSo: searchModel.MauSo, BacDonVi: searchModel.BacDonVi, BacNguonGoc: searchModel.BacNguonGocTSTD, stringCapHanhChinh: searchModel.StringCapHanhChinh);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TSTD_KeKhaiThongTin()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTSTD))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTDSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TSTD_MAU_01_BC_XLSHTD;
            searchModel.DDLQuyetDinhTichThuTSTD = _quyetDinhTichThuModelFactory.PrepareListModelForBaoCao();
            return PartialView(searchModel);
        }

        public virtual IActionResult TSTD_KeKhaiThongTin_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null)
            { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TSTD_MAU_01_BC_XLSHTD) ?? new CauHinhBaoCaoModel();
            }

            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            searchModel.TenTaiSan = _nguonGocTaiSanModelFactory.PrepareTenNguonGocByListId(searchModel.StringNguonGocTaiSan);

            XtraReport model = new TSTD_KeKhaiThongTin(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _taiSanToanDanService.TSTD_MAU_01_BC(searchModel.QuyetDinhTichThuTSTD, (int)_workContext.CurrentDonVi.ID, searchModel.DonViTien, searchModel.DonViDienTich, searchModel.DonViKhoiLuong, searchModel.StringNguonGocTaiSan);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TSTD_PhuongAnXuLy()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTSTD))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTDSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TSTD_PhuongAnXuLy;
            searchModel.DDLQuyetDinhTichThuTSTD = _quyetDinhTichThuModelFactory.PrepareListModelForBaoCao();
            return PartialView(searchModel);
        }

        [HttpPost]
        public virtual IActionResult TSTD_PhuongAnXuLy(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TSTD_PhuongAnXuLy);
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            searchModel.TenTaiSan = _nguonGocTaiSanModelFactory.PrepareTenNguonGocByListId(searchModel.StringNguonGocTaiSan);
            XtraReport model = new TSTD_HinhThucXuLy(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _taiSanToanDanService.BaoCaoPhuongAnXuLyTSTDs(NgayBatDau: searchModel.NgayBatDau, DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, LoaiXuLyId: (int)enumLoaiXuLy.DeXuat, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien.ToIntDonViTien(), DonViDienTich: searchModel.DonViDienTich.ToIntDonViDienTich(), NguonGoc: searchModel.StringNguonGocTaiSan, HinhThucXuLyId: searchModel.HinhThucXuLyId);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TSTD_KetQuaXuLy()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTSTD))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTDSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TSTD_KetQuaXuLy;
            searchModel.DDLQuyetDinhTichThuTSTD = _quyetDinhTichThuModelFactory.PrepareListModelForBaoCao();
            return PartialView(searchModel);
        }

        [HttpPost]
        public virtual IActionResult TSTD_KetQuaXuLy(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TSTD_KetQuaXuLy);
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new TSTD_KetQuaXuLy(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _taiSanToanDanService.BaoCaoKetQuaXuLyTSTDs(NgayBatDau: searchModel.NgayBatDau, DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, LoaiXuLyId: (int)enumLoaiXuLy.KetQua, DonViTien: searchModel.DonViTien.ToIntDonViTien(), DonViDienTich: searchModel.DonViDienTich.ToIntDonViDienTich());
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        #endregion TSTD

        #region Báo cáo chi tiết tài sản

        public virtual IActionResult TS_BCCT_01B_DK_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            var listEx = (new enumLOAI_HINH_TAI_SAN().ToSelectList(valuesToExclude: new int[] { (int)enumLOAI_HINH_TAI_SAN.DAT, (int)enumLOAI_HINH_TAI_SAN.NHA }).Select(c => c.Value.ToNumberInt32())).ToArray();
            searchModel.LoaiHinhTaiSanAvaliable = new enumLOAI_HINH_TAI_SAN().ToSelectList(valuesToExclude: listEx);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCT_1B_HienTrangSuDungDatNha;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCT_01B_DK_TSNN_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCCT_1B_HienTrangSuDungDatNha) ?? new CauHinhBaoCaoModel();
            }

            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCT_01B_TSC(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _taiSanBCCTService.BaoCaoTSCT_1B(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, donViTien: searchModel.DonViTien, DonViDienTichDat: searchModel.DonViDienTich, DonViDienTichNha: searchModel.DonViDienTich, BacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TSCT_1B_CHAY_NGAM(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCCT_1B_HienTrangSuDungDatNha, searchModel, searchModel.FileType, typeof(rptTS_BCCT_1B).FullName, typeof(TS_BCCT_1B).FullName);
                _taiSanBCCTService.BaoCaoTSCT_1B(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, donViTien: searchModel.DonViTien, DonViDienTichDat: searchModel.DonViDienTich, DonViDienTichNha: searchModel.DonViDienTich, BacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoChiTietTaiSan });
        }

        public virtual IActionResult TS_BCCT_01A_DK_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCT_1A_DK_TSNN_DV_SD;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCT_01A_DK_TSNN_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCCT_1A_DK_TSNN_DV_SD) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCT_1A(searchModel, cauHinhModel, cauHinhChunghModel);
            var data = _taiSanBCCTService.BaoCaoTSCT_1A(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), donViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, BacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TSCT_1A_CHAY_NGAM(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                var cauHinhThreadBaoCao = _cauHinhService.LoadCauHinh<CauHinhThreadBaoCao>();
                var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
                var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
                var cauHinhModel = new CauHinhBaoCaoModel();
                var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
                if (cauHinh.BaoCao != null)
                {
                    cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCCT_1A_DK_TSNN_DV_SD) ?? new CauHinhBaoCaoModel();
                }
                //đẩy tham số
                var queue = new QueueProcess()
                {
                    MA_BAO_CAO = enumMA_BAO_CAO.TS_BCCT_1A_DK_TSNN_DV_SD,
                    STATEMENT = searchModel.toStringJson(),
                    NGUOI_TAO_ID = (int)_workContext.CurrentCustomer.ID,
                    DON_VI_ID = (int)_workContext.CurrentDonVi.ID,
                    FILE_TYPE = searchModel.FileType
                };
                _queueProcessService.InsertQueueProcess(queue);
                var idQueue = queue.ID;
                SuccessNotification("Thêm vào hàng đợi thành công");

                // chạy thread
                Task.Run(() =>
                {
                    Thread.Sleep(20000);
                    decimal mauSo = cauHinhThreadBaoCao.ThreadBaoCao_TimePerCheck == 0 ? 1 : cauHinhThreadBaoCao.ThreadBaoCao_TimePerCheck;
                    decimal loopCount = cauHinhThreadBaoCao.ThreadBaoCao_MaxWaitingTimePerThread / mauSo;
                    for (int i = 1; i <= loopCount; i++)
                    {
                        //kiểm tra xem đủ điều kiện chạy hay không (số lượng task đạng chạy < N)
                        if (_queueProcessService.CountQueueTrangThai(enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU) < cauHinhThreadBaoCao.ThreadBaoCao_MaxThreadRunning)
                            break;

                        //sleep theo milisecond
                        Thread.Sleep((int)cauHinhThreadBaoCao.ThreadBaoCao_TimePerCheck * 60000);
                        if (i == loopCount)
                            return;
                    }
                    _queueProcessService.UpdateStartLayDuLieu(idQueue);
                    //lấy dữ liệu
                    var data = _taiSanBCCTService.BaoCaoTSCT_1A(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, donViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, BacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan);
                    Thread.Sleep(20000);
                    _queueProcessService.UpdateStopLayDuLieu(idQueue);
                    //tạo báo cáo
                    _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
                    XtraReport model = new rptTS_BCCT_1A(searchModel, cauHinhModel, cauHinhChunghModel);

                    model.DataSource = data;

                    _queueProcessModelFactory.SaveFileBaoCao(model, enumMA_BAO_CAO.TS_BCCT_1A_DK_TSNN_DV_SD, idQueue);
                });
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoChiTietTaiSan });
        }

        [HttpPost]
        public virtual IActionResult TSCT_1A_CHAY_NGAM_2(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);

                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(
                    enumMA_BAO_CAO.TS_BCCT_1A_DK_TSNN_DV_SD,
                    searchModel, searchModel.FileType,
                    typeof(rptTS_BCCT_1A).FullName,
                    typeof(TS_BCCT_1A).FullName);

                _taiSanBCCTService.BaoCaoTSCT_1A(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID,
                    NgayKetThuc: searchModel.NgayKetThuc,
                    LoaiTaiSan: (int)searchModel.LoaiTaiSan,
                    donViTien: searchModel.DonViTien,
                    DonViDienTich: searchModel.DonViDienTich,
                    BacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, idQueueBaoCao: queue.ID);

                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoChiTietTaiSan });
        }

        public virtual IActionResult TS_BCCT_01C_DK_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            //searchModel.DDLLyDoBienDong = _lyDoBienDongModelFactory.PrepareSelectListLyDoBienDongByBaoCao(loailydoId: 1).ToList();
            //searchModel.MaBaoCao = enumMA_BAO_CAO.BCCT_01C_DK_TSNN_TangGiamTSNNDonViSuDung;
            //searchModel.DDLLyDoBienDong = new enumLyDoTangGiamForBaoCao().ToSelectList().ToList();
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCT_01C_DK_TSNN_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.BCCT_01C_DK_TSNN_TangGiamTSNNDonViSuDung) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCT_01C_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _taiSanBCCTService.BaoCaoTSCT_01C_DK_TSNN(donViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, ngayBatDau: searchModel.NgayBatDau, ngayKetThuc: searchModel.NgayKetThuc, loaiTaiSan: (int)searchModel.LoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, bacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLyDo: searchModel.StringLyDoID);
            searchModel.DonVi = _workContext.CurrentDonVi.ID;
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TSCT_1C_CHAY_NGAM(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.BCCT_01C_DK_TSNN_TangGiamTSNNDonViSuDung, searchModel, searchModel.FileType, typeof(rptTS_BCCT_01C_TSNN).FullName, typeof(TS_BCCT_01C_DK_TSNN).FullName);
                _taiSanBCCTService.BaoCaoTSCT_01C_DK_TSNN(donViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, ngayBatDau: searchModel.NgayBatDau, ngayKetThuc: searchModel.NgayKetThuc, loaiTaiSan: (int)searchModel.LoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, bacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLyDo: searchModel.StringLyDoID, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoChiTietTaiSan });
        }

        public virtual IActionResult TS_BCCT_01D_DK_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.BCCT_01D_DK_TSNN_BCTangTSNNDonViSuDung;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCT_01D_DK_TSNN_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.BCCT_01D_DK_TSNN_BCTangTSNNDonViSuDung) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCT_01D_DK_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _taiSanBCCTService.BaoCaoTSCT_01D_DK_TSNN(donViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, ngayBatDau: searchModel.NgayBatDau, ngayKetThuc: searchModel.NgayKetThuc, loaiHinhTaiSan: (int)searchModel.LoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, bacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLyDo: searchModel.StringLyDoID);
            searchModel.DonVi = _workContext.CurrentDonVi.ID;
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TSCT_1D_CHAY_NGAM(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.BCCT_01D_DK_TSNN_BCTangTSNNDonViSuDung, searchModel, searchModel.FileType, typeof(rptTS_BCCT_01D_DK_TSNN).FullName, typeof(TS_BCCT_01D_DK_TSNN).FullName);
                _taiSanBCCTService.BaoCaoTSCT_01D_DK_TSNN(donViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, ngayBatDau: searchModel.NgayBatDau, ngayKetThuc: searchModel.NgayKetThuc, loaiHinhTaiSan: (int)searchModel.LoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, bacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLyDo: searchModel.StringLyDoID, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoChiTietTaiSan });
        }

        public virtual IActionResult TS_BCCT_01E_DK_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.BCCT_01E_DK_TSNN_BCGiamTSNNDonViSuDung;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCT_01E_DK_TSNN_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.BCCT_01E_DK_TSNN_BCGiamTSNNDonViSuDung) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCT_01E_DK_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _taiSanBCCTService.BaoCaoTSCT_01E_DK_TSNN(donViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, ngayBatDau: searchModel.NgayBatDau, ngayKetThuc: searchModel.NgayKetThuc, loaiHinhTaiSan: (int)searchModel.LoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, bacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLyDo: searchModel.StringLyDoID);
            searchModel.DonVi = _workContext.CurrentDonVi.ID;
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TSCT_1E_CHAY_NGAM(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.BCCT_01E_DK_TSNN_BCGiamTSNNDonViSuDung, searchModel, searchModel.FileType, typeof(rptTS_BCCT_01E_DK_TSNN).FullName, typeof(TS_BCCT_01E_DK_TSNN).FullName);
                _taiSanBCCTService.BaoCaoTSCT_01E_DK_TSNN(donViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, ngayBatDau: searchModel.NgayBatDau, ngayKetThuc: searchModel.NgayKetThuc, loaiHinhTaiSan: (int)searchModel.LoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, bacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLyDo: searchModel.StringLyDoID, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoChiTietTaiSan });
        }

        public virtual IActionResult TS_BCCT_01F_DK_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.BCCT_01F_DK_TSNN_BCKhauHaoHaoMonDonViSuDung;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCT_01F_DK_TSNN_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.BCCT_01F_DK_TSNN_BCKhauHaoHaoMonDonViSuDung) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCT_01F_DK_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            // code here
            var obj = _taiSanBCCTService.BaoCaoTSCT_01F_DK_TSNN(donViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, namBaoCao: searchModel.NamBaoCao, loaiHinhTaiSan: (int)searchModel.LoaiTaiSan, donViTien: searchModel.DonViTien, stringLoaiTaiSan: searchModel.StringLoaiTaiSan);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TSCT_1F_CHAY_NGAM(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.BCCT_01F_DK_TSNN_BCKhauHaoHaoMonDonViSuDung, searchModel, searchModel.FileType, typeof(rptTS_BCCT_01F_DK_TSNN).FullName, typeof(TS_BCCT_01F_DK_TSNN).FullName);
                _taiSanBCCTService.BaoCaoTSCT_01F_DK_TSNN(donViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, namBaoCao: searchModel.NamBaoCao, loaiHinhTaiSan: (int)searchModel.LoaiTaiSan, donViTien: searchModel.DonViTien, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoChiTietTaiSan });
        }

        public virtual IActionResult TS_BCCT_01G_DK_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.IsChuaCapNhapTien = false;
            searchModel.MaBaoCao = enumMA_BAO_CAO.BCCT_01G_DK_TSNN_BCBanThanhLyTSNN;
            return PartialView(searchModel);
        }

        [HttpGet]
        public virtual IActionResult TS_BCCT_01G_DK_TSNN_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.BCCT_01G_DK_TSNN_BCBanThanhLyTSNN) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCT_01G_DK_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _taiSanBCCTService.BaoCaoTSCT_01G_AND_01H_DK_TSNN(donViId: (searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID), ngayBatDau: searchModel.NgayBatDau, ngayKetThuc: searchModel.NgayKetThuc, loaiHinhTaiSanId: (int)searchModel.LoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, bacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, lyDoBienDongId: searchModel.LyDoBanThanhLyId, isDaThuTien: true);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TSCT_1G_CHAY_NGAM(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.BCCT_01G_DK_TSNN_BCBanThanhLyTSNN, searchModel, searchModel.FileType, typeof(rptTS_BCCT_01G_DK_TSNN).FullName, typeof(TS_BCCT_01Gand01H_DK_TSNN).FullName);
                _taiSanBCCTService.BaoCaoTSCT_01G_AND_01H_DK_TSNN(donViId: (searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID), ngayBatDau: searchModel.NgayBatDau, ngayKetThuc: searchModel.NgayKetThuc, loaiHinhTaiSanId: (int)searchModel.LoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, bacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, lyDoBienDongId: searchModel.LyDoBanThanhLyId, isDaThuTien: true, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoChiTietTaiSan });
        }

        public virtual IActionResult TS_BCCT_01H_DK_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.IsChuaCapNhapTien = false;
            searchModel.MaBaoCao = enumMA_BAO_CAO.BCCT_01H_DK_TSNN_BCBanThanhLyTSNN;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCT_01H_DK_TSNN_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.BCCT_01H_DK_TSNN_BCBanThanhLyTSNN) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCT_01H_DK_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _taiSanBCCTService.BaoCaoTSCT_01G_AND_01H_DK_TSNN(donViId: (searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID), ngayBatDau: searchModel.NgayBatDau, ngayKetThuc: searchModel.NgayKetThuc, loaiHinhTaiSanId: (int)searchModel.LoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, bacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, lyDoBienDongId: searchModel.LyDoBanThanhLyId, isDaThuTien: false);
            searchModel.DonVi = _workContext.CurrentDonVi.ID;
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TSCT_1H_CHAY_NGAM(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.BCCT_01H_DK_TSNN_BCBanThanhLyTSNN, searchModel, searchModel.FileType, typeof(rptTS_BCCT_01H_DK_TSNN).FullName, typeof(TS_BCCT_01Gand01H_DK_TSNN).FullName);
                _taiSanBCCTService.BaoCaoTSCT_01G_AND_01H_DK_TSNN(donViId: (searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID), ngayBatDau: searchModel.NgayBatDau, ngayKetThuc: searchModel.NgayKetThuc, loaiHinhTaiSanId: (int)searchModel.LoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, bacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, lyDoBienDongId: searchModel.LyDoBanThanhLyId, isDaThuTien: false, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoChiTietTaiSan });
        }

        public virtual IActionResult TS_BCCT_BCTDXNTS_DKTS()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCT_BCTDXNTS_DKTS;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCT_BCTDXNTS_DKTS_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCCT_BCTDXNTS_DKTS) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCT_BCTDXNTS_DKTS(searchModel, cauHinhModel, cauHinhChunghModel);
            var data = _taiSanBCCTService.GetTS_BCCT_BCTDXNTS_DKTS(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        #endregion Báo cáo chi tiết tài sản

        #region Báo cáo tổng hợp tài sản

        #region báo cáo tổng hợp tài sản 2A

        public virtual IActionResult TS_BCTH_02A_DK_TSNN(int MauSo = 1)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTongHopTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MauSo = MauSo;
            //searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02A_DK_TSNN;
            switch (MauSo)
            {
                case 1:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02A_DK_TSNN_p1;
                    break;

                case 2:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02A_DK_TSNN_p2;
                    break;

                case 3:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02A_DK_TSNN_p3;
                    break;
            }
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCTH_02A_DK_TSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var maBaoCao = "";
            switch (searchModel.MauSo)
            {
                case 1:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02A_DK_TSNN_p1;
                    break;

                case 2:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02A_DK_TSNN_p2;
                    break;

                case 3:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02A_DK_TSNN_p3;
                    break;
            }
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == maBaoCao) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCTH_02A_DK_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_02A(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayKetThuc: searchModel.NgayKetThuc, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, BacDonVi: searchModel.BacDonVi, isHideDetail: searchModel.IsHideDetail, BacTaiSan: searchModel.BacTaiSan);
            model.DataSource = obj;
            // Xem trực tiếp hoặc xuất luôn excel - HungNT
            if (searchModel.IsExportExcel)
            {
                return Export_Excel(model, searchModel, maBaoCao);

            }
            else
            {
                return ShowViewReport(model);
            }
        }

        [HttpPost]
        public virtual IActionResult TSTH_2A_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                var maBaoCao = "";
                switch (searchModel.MauSo)
                {
                    case 1:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02A_DK_TSNN_p1;
                        break;

                    case 2:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02A_DK_TSNN_p2;
                        break;

                    case 3:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02A_DK_TSNN_p3;
                        break;
                }
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                //insert queue
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(maBaoCao, searchModel, searchModel.FileType, typeof(rptTS_BCTH_02A_DK_TSNN).FullName, typeof(rptTS_BCTH_02A_DK_TSNN).FullName);

                _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_02A(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayKetThuc: searchModel.NgayKetThuc, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, BacDonVi: searchModel.BacDonVi, idQueueBaoCao: queue.ID, BacTaiSan: searchModel.BacTaiSan);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoTongHopTaiSan });
        }

        #endregion báo cáo tổng hợp tài sản 2A

        #region Báo cáo tổng hợp tài sản 2B

        public virtual IActionResult TS_BCTH_02B_DK_TSNN(int MauSo = 1)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTongHopTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            var listEx = (new enumLOAI_HINH_TAI_SAN().ToSelectList(valuesToExclude: new int[] { (int)enumLOAI_HINH_TAI_SAN.DAT, (int)enumLOAI_HINH_TAI_SAN.NHA }).Select(c => c.Value.ToNumberInt32())).ToArray();
            searchModel.LoaiHinhTaiSanAvaliable = new enumLOAI_HINH_TAI_SAN().ToSelectList(valuesToExclude: listEx);
            searchModel.MauSo = MauSo;
            //searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02B_DK_TSNN;
            switch (MauSo)
            {
                case 1:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02B_DK_TSNN_p1;
                    break;

                case 2:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02B_DK_TSNN_p2;
                    break;

                case 3:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02B_DK_TSNN_p3;
                    break;
            }
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCTH_02B_DK_TSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var maBaoCao = "";
            switch (searchModel.MauSo)
            {
                case 1:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02B_DK_TSNN_p1;
                    break;

                case 2:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02B_DK_TSNN_p2;
                    break;

                case 3:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02B_DK_TSNN_p3;
                    break;
            }
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == maBaoCao) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCTH_02B_DK_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_02B(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, BacDonVi: searchModel.BacDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList());
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TSTH_2B_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                var maBaoCao = "";
                switch (searchModel.MauSo)
                {
                    case 1:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02B_DK_TSNN_p1;
                        break;

                    case 2:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02B_DK_TSNN_p2;
                        break;

                    case 3:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02B_DK_TSNN_p3;
                        break;
                }
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                //insert queue
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(maBaoCao, searchModel, searchModel.FileType, typeof(rptTS_BCTH_02B_DK_TSNN).FullName, typeof(TS_BCTH_02B_DK_TSNN).FullName);
                //generate statement
                _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_02B(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, BacDonVi: searchModel.BacDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoTongHopTaiSan });
        }

        #endregion Báo cáo tổng hợp tài sản 2B

        #region Báo cáo tổng hợp tài sản 2C

        public virtual IActionResult TS_BCTH_02C_DK_TSNN(int MauSo = 1)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTongHopTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            //searchModel.DDLLyDoBienDong = new enumLyDoTangGiamForBaoCao().ToSelectList().ToList();
            searchModel.MauSo = MauSo;
            switch (MauSo)
            {
                case 1:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02C_DK_TSNN_p1;
                    break;

                case 2:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02C_DK_TSNN_p2;
                    break;

                case 3:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02C_DK_TSNN_p3;
                    break;
            }
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCTH_02C_DK_TSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var maBaoCao = "";
            switch (searchModel.MauSo)
            {
                case 1:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02C_DK_TSNN_p1;
                    break;

                case 2:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02C_DK_TSNN_p2;
                    break;

                case 3:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02C_DK_TSNN_p3;
                    break;
            }
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == maBaoCao) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCTH_02C_DK_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_02C(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), stringLyDo: searchModel.StringLyDoID, BacDonVi: searchModel.BacDonVi, BacTaiSan: searchModel.BacTaiSan);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TSTH_2C_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                var maBaoCao = "";
                switch (searchModel.MauSo)
                {
                    case 1:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02C_DK_TSNN_p1;
                        break;

                    case 2:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02C_DK_TSNN_p2;
                        break;

                    case 3:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02C_DK_TSNN_p3;
                        break;
                }
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                //insert queue
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(maBaoCao, searchModel, searchModel.FileType, typeof(rptTS_BCTH_02C_DK_TSNN).FullName, typeof(TS_BCTH_02C_DK_TSNN).FullName);

                _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_02C(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), stringLyDo: searchModel.StringLyDoID, BacDonVi: searchModel.BacDonVi, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoTongHopTaiSan });
        }

        #endregion Báo cáo tổng hợp tài sản 2C

        #region Báo cáo tổng hợp tài sản 2D

        public virtual IActionResult TS_BCTH_02D_DK_TSNN(int MauSo = 1)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTongHopTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MauSo = MauSo;
            switch (MauSo)
            {
                case 1:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02D_DK_TSNN_p1;
                    break;

                case 2:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02D_DK_TSNN_p2;
                    break;

                case 3:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02D_DK_TSNN_p3;
                    break;
            }
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCTH_02D_DK_TSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var maBaoCao = "";
            switch (searchModel.MauSo)
            {
                case 1:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02D_DK_TSNN_p1;
                    break;

                case 2:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02D_DK_TSNN_p2;
                    break;

                case 3:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02D_DK_TSNN_p3;
                    break;
            }
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == maBaoCao) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCTH_02D_DK_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_02D(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, stringLyDo: searchModel.StringLyDoID, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, BacDonVi: searchModel.BacDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList());
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TSTH_2D_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                var maBaoCao = "";
                switch (searchModel.MauSo)
                {
                    case 1:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02D_DK_TSNN_p1;
                        break;

                    case 2:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02D_DK_TSNN_p2;
                        break;

                    case 3:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02D_DK_TSNN_p3;
                        break;
                }
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                //insert queue
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(maBaoCao, searchModel, searchModel.FileType, typeof(rptTS_BCTH_02D_DK_TSNN).FullName, typeof(TS_BCTH_02D_DK_TSNN).FullName);

                _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_02D(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, stringLyDo: searchModel.StringLyDoID, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, BacDonVi: searchModel.BacDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoTongHopTaiSan });
        }

        #endregion Báo cáo tổng hợp tài sản 2D

        #region Báo cáo tổng hợp tài sản 2E

        public virtual IActionResult TS_BCTH_02E_DK_TSNN(int MauSo = 1)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTongHopTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MauSo = MauSo;
            switch (MauSo)
            {
                case 1:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02E_DK_TSNN_p1;
                    break;

                case 2:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02E_DK_TSNN_p2;
                    break;

                case 3:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02E_DK_TSNN_p3;
                    break;
            }
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCTH_02E_DK_TSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var maBaoCao = "";
            switch (searchModel.MauSo)
            {
                case 1:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02E_DK_TSNN_p1;
                    break;

                case 2:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02E_DK_TSNN_p2;
                    break;

                case 3:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02E_DK_TSNN_p3;
                    break;
            }
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == maBaoCao) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCTH_02E_DK_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_02E(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, stringLyDo: searchModel.StringLyDoID, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, BacDonVi: searchModel.BacDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList());
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TSTH_2E_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                var maBaoCao = "";
                switch (searchModel.MauSo)
                {
                    case 1:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02E_DK_TSNN_p1;
                        break;

                    case 2:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02E_DK_TSNN_p2;
                        break;

                    case 3:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02E_DK_TSNN_p3;
                        break;
                }
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                //insert queue
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(maBaoCao, searchModel, searchModel.FileType, typeof(rptTS_BCTH_02E_DK_TSNN).FullName, typeof(TS_BCTH_02E_DK_TSNN).FullName);

                _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_02E(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, stringLyDo: searchModel.StringLyDoID, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, BacDonVi: searchModel.BacDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoTongHopTaiSan });
        }

        #endregion Báo cáo tổng hợp tài sản 2E

        #region Báo cáo tổng hợp tài sản 2F

        public virtual IActionResult TS_BCTH_02F_DK_TSNN(int MauSo = 1)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTongHopTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MauSo = MauSo;
            switch (MauSo)
            {
                case 1:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02F_DK_TSNN_p1;
                    break;

                case 2:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02F_DK_TSNN_p2;
                    break;

                case 3:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02F_DK_TSNN_p3;
                    break;
            }
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCTH_02F_DK_TSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var maBaoCao = "";
            switch (searchModel.MauSo)
            {
                case 1:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02F_DK_TSNN_p1;
                    break;

                case 2:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02F_DK_TSNN_p2;
                    break;

                case 3:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02F_DK_TSNN_p3;
                    break;
            }
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == maBaoCao) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCTH_02F_DK_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_02F(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), namBaoCao: searchModel.NamBaoCao, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, MauSo: searchModel.MauSo, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, BacDonVi: searchModel.BacDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList());
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TSTH_2F_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                var maBaoCao = "";
                switch (searchModel.MauSo)
                {
                    case 1:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02F_DK_TSNN_p1;
                        break;

                    case 2:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02F_DK_TSNN_p2;
                        break;

                    case 3:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02F_DK_TSNN_p3;
                        break;
                }
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                //insert queue
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(maBaoCao, searchModel, searchModel.FileType, typeof(rptTS_BCTH_02F_DK_TSNN).FullName, typeof(TS_BCTH_02F_DK_TSNN).FullName);

                _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_02F(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), namBaoCao: searchModel.NamBaoCao, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, MauSo: searchModel.MauSo, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, BacDonVi: searchModel.BacDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoTongHopTaiSan });
        }

        #endregion Báo cáo tổng hợp tài sản 2F

        #region Báo cáo tổng hợp tài sản 2G

        public virtual IActionResult TS_BCTH_02G_DK_TSNN(int MauSo = 1)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTongHopTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MauSo = MauSo;
            switch (MauSo)
            {
                case 1:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02G_DK_TSNN_p1;
                    break;

                case 2:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02G_DK_TSNN_p2;
                    break;

                case 3:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02G_DK_TSNN_p3;
                    break;
            }
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCTH_02G_DK_TSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var maBaoCao = "";
            switch (searchModel.MauSo)
            {
                case 1:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02G_DK_TSNN_p1;
                    break;

                case 2:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02G_DK_TSNN_p2;
                    break;

                case 3:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02G_DK_TSNN_p3;
                    break;
            }
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == maBaoCao) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCTH_02G_DK_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_02G(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, NgayBatDau: searchModel.NgayBatDau, BanOrThanhLy: searchModel.BanOrThanhLy, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, BacDonVi: searchModel.BacDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList());
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TSTH_2G_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                var maBaoCao = "";
                switch (searchModel.MauSo)
                {
                    case 1:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02G_DK_TSNN_p1;
                        break;

                    case 2:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02G_DK_TSNN_p2;
                        break;

                    case 3:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02G_DK_TSNN_p3;
                        break;
                }
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                //insert queue
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(maBaoCao, searchModel, searchModel.FileType, typeof(rptTS_BCTH_02G_DK_TSNN).FullName, typeof(TS_BCTH_02G_DK_TNSS).FullName);

                _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_02G(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, NgayBatDau: searchModel.NgayBatDau, BanOrThanhLy: searchModel.BanOrThanhLy, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, BacDonVi: searchModel.BacDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoTongHopTaiSan });
        }

        #endregion Báo cáo tổng hợp tài sản 2G

        #region Báo cáo tổng hợp tài sản 2H

        public virtual IActionResult TS_BCTH_02H_DK_TSNN(int MauSo = 1)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTongHopTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MauSo = MauSo;
            switch (MauSo)
            {
                case 1:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02H_DK_TSNN_p1;
                    break;

                case 2:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02H_DK_TSNN_p2;
                    break;

                case 3:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02H_DK_TSNN_p3;
                    break;
            }
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCTH_02H_DK_TSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var maBaoCao = "";
            switch (searchModel.MauSo)
            {
                case 1:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02H_DK_TSNN_p1;
                    break;

                case 2:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02H_DK_TSNN_p2;
                    break;

                case 3:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_02H_DK_TSNN_p3;
                    break;
            }
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == maBaoCao) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCTH_02H_DK_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_02H(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, NgayBatDau: searchModel.NgayBatDau, BanOrThanhLy: searchModel.BanOrThanhLy, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, BacDonVi: searchModel.BacDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList());
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TSTH_2H_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                var maBaoCao = "";
                switch (searchModel.MauSo)
                {
                    case 1:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02H_DK_TSNN_p1;
                        break;

                    case 2:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02H_DK_TSNN_p2;
                        break;

                    case 3:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_02H_DK_TSNN_p3;
                        break;
                }
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                //insert queue
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(maBaoCao, searchModel, searchModel.FileType, typeof(rptTS_BCTH_02H_DK_TSNN).FullName, typeof(TS_BCTH_02H_DK_TSNN).FullName);

                _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_02H(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, NgayBatDau: searchModel.NgayBatDau, BanOrThanhLy: searchModel.BanOrThanhLy, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, BacDonVi: searchModel.BacDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoTongHopTaiSan });
        }

        #endregion Báo cáo tổng hợp tài sản 2H

        #region Báo cáo tổng hợp 8A

        public virtual IActionResult TS_BCTH_08A_DK_TSC(int MauSo = 1)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTongHopTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            //var listEx = (new enumLOAI_HINH_TAI_SAN().ToSelectList(valuesToExclude: new int[] { (int)enumLOAI_HINH_TAI_SAN.DAT, (int)enumLOAI_HINH_TAI_SAN.OTO, (int)enumLOAI_HINH_TAI_SAN.DAC_THU }).Select(c => c.Value.ToNumberInt32())).ToArray();
            //searchModel.LoaiHinhTaiSanAvaliable = new enumLOAI_HINH_TAI_SAN().ToSelectList(valuesToExclude: listEx);
            searchModel.MauSo = MauSo;
            //searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02A_DK_TSNN;
            switch (MauSo)
            {
                case 1:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_08A_DK_TSC_P1;
                    break;

                case 2:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_08A_DK_TSC_P2;
                    break;

                case 3:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_08A_DK_TSC_P3;
                    break;
            }
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCTH_08A_DK_TSC_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var maBaoCao = "";
            switch (searchModel.MauSo)
            {
                case 1:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_08A_DK_TSC_P1;
                    break;

                case 2:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_08A_DK_TSC_P2;
                    break;

                case 3:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_08A_DK_TSC_P3;
                    break;
            }
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == maBaoCao) ?? new CauHinhBaoCaoModel();
            }
            searchModel.enumListLoaiHinhTS = new List<enumNHAN_HIEN_THI_LOAI_HINH_TS>() { enumNHAN_HIEN_THI_LOAI_HINH_TS.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TS.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TS.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TS.TAI_SAN_CO_DINH_KHAC };
            _reportModelFactory.PrePareDonViBaoCao(searchModel);



            XtraReport model = new rptTS_BCTH_08A_DK_TSC(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_08A(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), BacDonVi: searchModel.BacDonVi, IsHideDetail: searchModel.IsHideDetail);
            model.DataSource = obj;
            // Xem trực tiếp hoặc xuất luôn excel - HungNT
            if (searchModel.IsExportExcel)
            {
                return Export_Excel(model, searchModel, maBaoCao);

            }
            else
            {
                return ShowViewReport(model);
            }

        }

        [HttpPost]
        public virtual IActionResult TS_BCTH_08A_DK_TSC_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                var maBaoCao = "";
                switch (searchModel.MauSo)
                {
                    case 1:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_08A_DK_TSC_P1;
                        break;

                    case 2:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_08A_DK_TSC_P2;
                        break;

                    case 3:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_08A_DK_TSC_P3;
                        break;
                }
                searchModel.enumListLoaiHinhTS = new List<enumNHAN_HIEN_THI_LOAI_HINH_TS>() { enumNHAN_HIEN_THI_LOAI_HINH_TS.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TS.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TS.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TS.TAI_SAN_CO_DINH_KHAC };
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                //insert queue
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(maBaoCao, searchModel, searchModel.FileType, typeof(rptTS_BCTH_08A_DK_TSC).FullName, typeof(TS_BCTH_08A_DK_TSC).FullName);

                _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_08A(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoTongHopTaiSan });

            //var maBaoCao = "";
            //switch (searchModel.MauSo)
            //{
            //	case 1:
            //		maBaoCao = enumMA_BAO_CAO.TS_BCTH_08A_DK_TSC_P1;
            //		break;

            //	case 2:
            //		maBaoCao = enumMA_BAO_CAO.TS_BCTH_08A_DK_TSC_P2;
            //		break;

            //	case 3:
            //		maBaoCao = enumMA_BAO_CAO.TS_BCTH_08A_DK_TSC_P3;
            //		break;
            //}
            //var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            //var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            //if (cauHinh.BaoCao != null)
            //{
            //	cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == maBaoCao) ?? new CauHinhBaoCaoModel();
            //}
            //searchModel.enumListLoaiHinhTS = new List<enumNHAN_HIEN_THI_LOAI_HINH_TS>() { enumNHAN_HIEN_THI_LOAI_HINH_TS.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TS.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TS.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TS.TAI_SAN_CO_DINH_KHAC };
            //_reportModelFactory.PrePareDonViBaoCao(searchModel);
            //XtraReport model = new rptTS_BCTH_08A_DK_TSC(searchModel, cauHinhModel, cauHinhChunghModel);
            //var obj = _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_08A(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, CapDonVi: searchModel.CapDonVi, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList());
            //model.DataSource = obj;
            //string contentType = "";
            //string fileExtension = "";
            //using (MemoryStream ms = new MemoryStream())
            //{
            //	switch (searchModel.FileType)
            //	{
            //		case (int)enumEXPORT_FILE_TYPE.WORD_DOCX:
            //			model.ExportToDocx(ms);
            //			contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            //			fileExtension = ".docx";
            //			break;

            //		case (int)enumEXPORT_FILE_TYPE.EXCEL_XLSX:
            //			model.ExportToXlsx(ms);
            //			contentType = "application/xlsx";
            //			fileExtension = ".xlsx";
            //			break;

            //		case (int)enumEXPORT_FILE_TYPE.PDF:
            //			model.ExportToPdf(ms);
            //			contentType = "application/pdf";
            //			fileExtension = ".pdf";
            //			break;

            //		default:
            //			model.ExportToPdf(ms);
            //			contentType = "application/pdf";
            //			fileExtension = ".pdf";
            //			break;
            //	}
            //	byte[] fileBinary = ms.ToArray();
            //	return new FileContentResult(fileBinary, contentType)
            //	{
            //		FileDownloadName = maBaoCao + fileExtension
            //	};
            //}
        }
        #endregion Báo cáo tổng hợp 8A

        #region Báo cáo tổng hợp 8B

        public virtual IActionResult TS_BCTH_08B_DK_TSC(int MauSo = 1)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTongHopTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MauSo = MauSo;
            switch (MauSo)
            {
                case 1:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_08B_DK_TSC_P1;
                    break;

                case 2:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_08B_DK_TSC_P2;
                    break;

                case 3:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_08B_DK_TSC_P3;
                    break;
            }
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCTH_08B_DK_TSC_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var maBaoCao = "";
            switch (searchModel.MauSo)
            {
                case 1:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_08B_DK_TSC_P1;
                    break;

                case 2:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_08B_DK_TSC_P2;
                    break;

                case 3:
                    maBaoCao = enumMA_BAO_CAO.TS_BCTH_08B_DK_TSC_P3;
                    break;
            }
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == maBaoCao) ?? new CauHinhBaoCaoModel();
            }
            searchModel.enumListLoaiHinhTS = new List<enumNHAN_HIEN_THI_LOAI_HINH_TS>() { enumNHAN_HIEN_THI_LOAI_HINH_TS.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TS.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TS.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TS.TAI_SAN_CO_DINH_KHAC };
            _reportModelFactory.PrePareDonViBaoCao(searchModel);

            //if (searchModel.CapDonVi < 0)
            //{
            //	searchModel.TenCapHanhChinh = "Tất cả";
            //}
            //else
            //{
            //	if (searchModel.MA_DON_VI == "018999")
            //	{
            //		searchModel.TenCapHanhChinh = _nhanHienThiService.GetGiaTriEnum((enumTinhHuyenXaTrungUong)(searchModel.CapDonVi));

            //	}
            //	else
            //	{
            //		searchModel.TenCapHanhChinh = _nhanHienThiService.GetGiaTriEnum((enumTinhHuyenXa)(searchModel.CapDonVi));
            //	}

            //}
            XtraReport model = new rptTS_BCTH_08B_DK_TSC(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_08B(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), stringLyDo: searchModel.StringLyDoID, BacDonvi: searchModel.BacDonVi, IsHideDetail: searchModel.IsHideDetail);
            model.DataSource = obj;
            // Xem trực tiếp hoặc xuất luôn excel - HungNT
            if (searchModel.IsExportExcel)
            {
                return Export_Excel(model, searchModel, maBaoCao);

            }
            else
            {
                return ShowViewReport(model);
            }

        }

        [HttpPost]
        public virtual IActionResult TS_BCTH_08B_DK_TSC_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            //var maBaoCao = "";
            //switch (searchModel.MauSo)
            //{
            //	case 1:
            //		maBaoCao = enumMA_BAO_CAO.TS_BCTH_08B_DK_TSC_P1;
            //		break;

            //	case 2:
            //		maBaoCao = enumMA_BAO_CAO.TS_BCTH_08B_DK_TSC_P2;
            //		break;

            //	case 3:
            //		maBaoCao = enumMA_BAO_CAO.TS_BCTH_08B_DK_TSC_P3;
            //		break;
            //}
            //var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            //var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            //if (cauHinh.BaoCao != null)
            //{
            //	cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == maBaoCao) ?? new CauHinhBaoCaoModel();
            //}
            //searchModel.enumListLoaiHinhTS = new List<enumNHAN_HIEN_THI_LOAI_HINH_TS>() { enumNHAN_HIEN_THI_LOAI_HINH_TS.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TS.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TS.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TS.TAI_SAN_CO_DINH_KHAC };
            //_reportModelFactory.PrePareDonViBaoCao(searchModel);
            //XtraReport model = new rptTS_BCTH_08B_DK_TSC(searchModel, cauHinhModel, cauHinhChunghModel);
            //var obj = _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_08B(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, CapDonVi: searchModel.CapDonVi, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), stringLyDo: searchModel.StringLyDoID);
            //model.DataSource = obj;
            //string contentType = "";
            //string fileExtension = "";
            //using (MemoryStream ms = new MemoryStream())
            //{
            //	switch (searchModel.FileType)
            //	{
            //		case (int)enumEXPORT_FILE_TYPE.WORD_DOCX:
            //			model.ExportToDocx(ms);
            //			contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            //			fileExtension = ".docx";
            //			break;

            //		case (int)enumEXPORT_FILE_TYPE.EXCEL_XLSX:
            //			model.ExportToXlsx(ms);
            //			contentType = "application/xlsx";
            //			fileExtension = ".xlsx";
            //			break;

            //		case (int)enumEXPORT_FILE_TYPE.PDF:
            //			model.ExportToPdf(ms);
            //			contentType = "application/pdf";
            //			fileExtension = ".pdf";
            //			break;

            //		default:
            //			model.ExportToPdf(ms);
            //			contentType = "application/pdf";
            //			fileExtension = ".pdf";
            //			break;
            //	}
            //	byte[] fileBinary = ms.ToArray();
            //	return new FileContentResult(fileBinary, contentType)
            //	{
            //		FileDownloadName = maBaoCao + fileExtension
            //	};
            //}



            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                var maBaoCao = "";
                switch (searchModel.MauSo)
                {
                    case 1:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_08B_DK_TSC_P1;
                        break;

                    case 2:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_08B_DK_TSC_P2;
                        break;

                    case 3:
                        maBaoCao = enumMA_BAO_CAO.TS_BCTH_08B_DK_TSC_P3;
                        break;
                }
                searchModel.enumListLoaiHinhTS = new List<enumNHAN_HIEN_THI_LOAI_HINH_TS>() { enumNHAN_HIEN_THI_LOAI_HINH_TS.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TS.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TS.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TS.TAI_SAN_CO_DINH_KHAC };
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                //insert queue
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(maBaoCao, searchModel, searchModel.FileType, typeof(rptTS_BCTH_08B_DK_TSC).FullName, typeof(TS_BCTH_08B_DK_TSC).FullName);

                _baoCaoTongHopTaiSanService.BaoCaoTongHopTS_08B(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), stringLyDo: searchModel.StringLyDoID, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoTongHopTaiSan });
        }

        #endregion Báo cáo tổng hợp 8B

        #endregion Báo cáo tổng hợp tài sản

        #region Báo cáo quốc hội

        public virtual IActionResult TS_BCQH_MAU01_THTSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_MAU01_THTSNN;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_MAU01_THTSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_MAU01_THTSNN) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_MAU01_THTSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            model.DataSource = _baoCaoQuocHoiService.BCQH_MAU01_THTSNN(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich);
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCQH_MAU01_THTSNN_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                //đẩy tham số
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCQH_MAU01_THTSNN, searchModel, searchModel.FileType, typeof(rptTS_BCQH_MAU01_THTSNN).FullName, typeof(TS_BCQH_MAU01_THTSNN).FullName);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo file báo cáo", queue, "QueueProcess");
                _baoCaoQuocHoiService.BCQH_MAU01_THTSNN(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoQuocHoiTaiSan });
        }

        public virtual IActionResult TS_BCQH_MAU02_CCTSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_MAU02_CCTSNN;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_MAU02_CCTSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_MAU02_CCTSNN) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_MAU02_CCTSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            model.DataSource = _baoCaoQuocHoiService.BCQH_MAU02_CCTSNN(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich);
            // Xem trực tiếp hoặc xuất luôn excel - HungNT
            if (searchModel.IsExportExcel)
            {
                return Export_Excel(model, searchModel, cauHinhModel.MaBaoCao);

            }
            else
            {
                return ShowViewReport(model);
            }


        }

        [HttpPost]
        public virtual IActionResult TS_BCQH_MAU02_CCTSNN_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCQH_MAU02_CCTSNN, searchModel, searchModel.FileType, typeof(rptTS_BCQH_MAU02_CCTSNN).FullName, typeof(TS_BCQH_MAU02_CCTSNN).FullName);
                _baoCaoQuocHoiService.BCQH_MAU02_CCTSNN(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoQuocHoiTaiSan });
        }

        public virtual IActionResult TS_BCQH_MAU03_QUYDAT_MDSD()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_MAU03_QUYDAT_MDSD;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_MAU03_QUYDAT_MDSD_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_MAU03_QUYDAT_MDSD) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_MAU03_QUYDAT_MDSD(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            model.DataSource = _baoCaoQuocHoiService.BCQH_MAU03_QUYDAT_MDSD(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich);
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TS_BCQH_MAU03_QUYDAT_MDSD_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCQH_MAU03_QUYDAT_MDSD, searchModel, searchModel.FileType, typeof(rptTS_BCQH_MAU03_QUYDAT_MDSD).FullName, typeof(TS_BCQH_MAU03_QUYDAT_MDSD).FullName);
                _baoCaoQuocHoiService.BCQH_MAU03_QUYDAT_MDSD(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoQuocHoiTaiSan });
        }

        public virtual IActionResult TS_BCQH_MAU04_TS_LTS()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_MAU04_TS_LTS;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_MAU04_TS_LTS_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_MAU04_TS_LTS) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_MAU04_TS_LTS(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            model.DataSource = _baoCaoQuocHoiService.BCQH_MAU04_TS_LTS(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich);
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TS_BCQH_MAU04_TS_LTS_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCQH_MAU04_TS_LTS, searchModel, searchModel.FileType, typeof(rptTS_BCQH_MAU04_TS_LTS).FullName, typeof(TS_BCQH_MAU04_TS_LTS).FullName);
                _baoCaoQuocHoiService.BCQH_MAU04_TS_LTS(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoQuocHoiTaiSan });
        }

        public virtual IActionResult TS_BCQH_MAU05_TS_CQ_TC_DV()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_MAU05_TS_CQ_TC_DV;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_MAU05_TS_CQ_TC_DV_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_MAU05_TS_CQ_TC_DV) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_MAU05_TS_CQ_TC_DV(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            model.DataSource = _baoCaoQuocHoiService.BCQH_MAU05_TS_CQ_TC_DV(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien);
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TS_BCQH_MAU05_TS_CQ_TC_DV_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCQH_MAU05_TS_CQ_TC_DV, searchModel, searchModel.FileType, typeof(rptTS_BCQH_MAU05_TS_CQ_TC_DV).FullName, typeof(TS_BCQH_MAU05_TS_CQ_TC_DV).FullName);
                _baoCaoQuocHoiService.BCQH_MAU05_TS_CQ_TC_DV(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoQuocHoiTaiSan });
        }

        public virtual IActionResult TS_BCQH_MAU06_TS_CAPQL()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_MAU06_TS_CAPQL;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_MAU06_TS_CAPQL_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_MAU06_TS_CAPQL) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_MAU06_TS_CAPQL(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            model.DataSource = _baoCaoQuocHoiService.BCQH_MAU06_TS_CAPQL(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich);
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TS_BCQH_MAU06_TS_CAPQL_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCQH_MAU06_TS_CAPQL, searchModel, searchModel.FileType, typeof(rptTS_BCQH_MAU06_TS_CAPQL).FullName, typeof(TS_BCQH_MAU06_TS_CAPQL).FullName);
                _baoCaoQuocHoiService.BCQH_MAU06_TS_CAPQL(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoQuocHoiTaiSan });
        }

        public virtual IActionResult TS_BCQH_MAU07_OTO_SD()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_MAU07_OTO_SD;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_MAU07_OTO_SD_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_MAU07_OTO_SD) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_MAU07_OTO_SD(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            model.DataSource = _baoCaoQuocHoiService.BCQH_MAU07_OTO_SD(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich);
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TS_BCQH_MAU07_OTO_SD_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCQH_MAU07_OTO_SD, searchModel, searchModel.FileType, typeof(rptTS_BCQH_MAU07_OTO_SD).FullName, typeof(TS_BCQH_MAU07_OTO_SD).FullName);
                _baoCaoQuocHoiService.BCQH_MAU07_OTO_SD(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoQuocHoiTaiSan });
        }

        public virtual IActionResult TS_BCQH_MAU08_OTO_VSD()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_MAU08_OTO_VSD;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_MAU08_OTO_VSD_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_MAU08_OTO_VSD) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_MAU08_OTO_VSD(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            model.DataSource = _baoCaoQuocHoiService.BCQH_MAU08_OTO_VSD(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich);
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TS_BCQH_MAU08_OTO_VSD_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCQH_MAU08_OTO_VSD, searchModel, searchModel.FileType, typeof(rptTS_BCQH_MAU08_OTO_VSD).FullName, typeof(TS_BCQH_MAU08_OTO_VSD).FullName);
                _baoCaoQuocHoiService.BCQH_MAU08_OTO_VSD(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoQuocHoiTaiSan });
        }

        public virtual IActionResult TS_BCQH_MAU09_DAT_SD()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_MAU09_DAT_SD;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_MAU09_DAT_SD_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_MAU06_TS_CAPQL) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_MAU09_DAT_SD(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            model.DataSource = _baoCaoQuocHoiService.BCQH_MAU09_DAT_SD(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich);
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCQH_MAU10_TS_TREN500_MDSD()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_MAU10_TS_TREN500_MDSD;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_MAU10_TS_TREN500_MDSD_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_MAU10_TS_TREN500_MDSD) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_MAU10_TS_TREN500_MDSD(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            model.DataSource = _baoCaoQuocHoiService.BCQH_MAU10_TS_TREN500_MDSD(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich);
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TS_BCQH_MAU10_TS_TREN500_MDSD_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCQH_MAU10_TS_TREN500_MDSD, searchModel, searchModel.FileType, typeof(rptTS_BCQH_MAU10_TS_TREN500_MDSD).FullName, typeof(TS_BCQH_MAU10_TS_TREN500_MDSD).FullName);
                _baoCaoQuocHoiService.BCQH_MAU10_TS_TREN500_MDSD(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoQuocHoiTaiSan });
        }

        public virtual IActionResult TS_BCQH_MAU11A_THTSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_MAU11A_THTSNN;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_MAU11A_THTSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_MAU11A_THTSNN) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_MAU11A_THTSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var data = _baoCaoQuocHoiService.BCQH_MAU11A_TS_THTSNN(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), ngayBaoCao: searchModel.NgayKetThuc, donviDienTich: searchModel.DonViDienTich, donViTien: searchModel.DonViTien);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TTS_BCQH_MAU11A_THTSNN_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCQH_MAU11A_THTSNN, searchModel, searchModel.FileType, typeof(rptTS_BCQH_MAU11A_THTSNN).FullName, typeof(TS_BCQH_MAU11A_THTSNN).FullName);
                _baoCaoQuocHoiService.BCQH_MAU11A_TS_THTSNN(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), ngayBaoCao: searchModel.NgayKetThuc, donviDienTich: searchModel.DonViDienTich, donViTien: searchModel.DonViTien, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoQuocHoiTaiSan });
        }

        public virtual IActionResult TS_BCQH_MAU11B_THTSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_MAU11B_THTSNN;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_MAU11B_THTSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_MAU11B_THTSNN) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_MAU11B_THTSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var data = _baoCaoQuocHoiService.BCQH_MAU11B_TS_THTSNN(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), ngayBaoCao: searchModel.NgayKetThuc, donviDienTich: searchModel.DonViDienTich, donViTien: searchModel.DonViTien, is_Huyen: searchModel.IsHuyen, is_Tinh: searchModel.IsTinh, is_Xa: searchModel.IsXa);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TS_BCQH_MAU11B_THTSNN_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCQH_MAU11B_THTSNN, searchModel, searchModel.FileType, typeof(rptTS_BCQH_MAU11B_THTSNN).FullName, typeof(TS_BCQH_MAU11B_THTSNN).FullName);
                _baoCaoQuocHoiService.BCQH_MAU11B_TS_THTSNN(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), ngayBaoCao: searchModel.NgayKetThuc, donviDienTich: searchModel.DonViDienTich, donViTien: searchModel.DonViTien, is_Huyen: searchModel.IsHuyen, is_Tinh: searchModel.IsTinh, is_Xa: searchModel.IsXa, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoQuocHoiTaiSan });
        }

        public virtual IActionResult TS_BCQH_TH_QUAN_LY_SD_KHUONVIEN_DAT_TSC()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_QLSD_DAT_TSC;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_TH_QUAN_LY_SD_KHUONVIEN_DAT_TSC_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_QLSD_DAT_TSC) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);

            XtraReport model = new rptTS_BCQH_TH_QUAN_LY_SD_KHUONVIEN_DAT_TSC(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoQuocHoiService.BCQH_TH_QUAN_LY_SD_KHUONVIEN_DAT_TSC(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, /*StringCapHanhChinh: searchModel.StringCapHanhChinh,*/ stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList());
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCQH_TH_QUAN_LY_SD_NHA_TSC()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_QLSD_NHA_TSC;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_TH_QUAN_LY_SD_NHA_TSC_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_QLSD_NHA_TSC) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_TH_QUAN_LY_SD_NHA_TSC(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoQuocHoiService.BCQH_TH_QUAN_LY_SD_NHA_TSC(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, /*StringCapHanhChinh: searchModel.StringCapHanhChinh,*/ stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList());
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCQH_TH_QUAN_LY_SD_OTO_TS_KHAC_TSC()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_QLSD_OTO_TSC;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_TH_QUAN_LY_SD_OTO_TS_KHAC_TSC_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null)
            {
                cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault();
            }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_QLSD_OTO_TSC) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_TH_QUAN_LY_SD_OTO_TS_KHAC_TSC(searchModel, cauHinhModel, cauHinhChunghModel);
            var data = _baoCaoQuocHoiService.BCQH_TH_QUAN_LY_SD_OTO_TS_KHAC_TSC(ngayBaoCao: searchModel.NgayKetThuc, donViId: _workContext.CurrentDonVi.ID, donViTien: searchModel.DonViTien);
            model.DataSource = data;
            //model.DataSource = null;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCQH_PL02_TANG_GIAM_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL02_TSNN;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_PL02_TANG_GIAM_TSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL02_TSNN) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_PL02_TANG_GIAM_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            //new rptTS_TANG_GIAM_PL02_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var data = _baoCaoQuocHoiService.TS_BCQH_PL02_TANG_GIAM_TSNN(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo,/* CapDonVi: searchModel.CapDonVi,*/ stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), LyDo: searchModel.LyDoID);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCQH_PL03_TANG_GIAM_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL03_TSNN;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_PL03_TANG_GIAM_TSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL03_TSNN) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_PL03_TANG_GIAM_MUC_DICH_SD_QUY_DAT(searchModel, cauHinhModel, cauHinhChunghModel);

            var obj = _baoCaoQuocHoiService.TS_BCQH_PL03_TANG_GIAM_QUY_DAT(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, /*CapDonVi: searchModel.CapDonVi,*/ stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), LyDo: searchModel.LyDoID, stringLyDo: searchModel.StringLyDoID);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCQH_PL05_TANG_GIAM_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL05_TSNN;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_PL05_TANG_GIAM_TSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL05_TSNN) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_PL05_TANG_GIAM_TSNN_NHOM_DV(searchModel, cauHinhModel, cauHinhChunghModel);

            var obj = _baoCaoQuocHoiService.TS_BCQH_PL05_TANG_GIAM_TSNN_NHOM_DV(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, /*CapDonVi: searchModel.CapDonVi,*/ stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), LyDo: searchModel.LyDoID, stringLyDo: searchModel.StringLyDoID);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCQH_PL06_TANG_GIAM_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL06_TSNN;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_PL06_TANG_GIAM_TSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL06_TSNN) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_PL06_TANG_GIAM_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu
            var obj = _baoCaoQuocHoiService.TS_BCQH_PL06_TANG_GIAM_TSNN(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, /*CapDonVi: searchModel.CapDonVi,*/ stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), LyDo: searchModel.LyDoID, stringLyDo: searchModel.StringLyDoID);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCQH_PL07_TANG_GIAM_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.DDLLyDoBienDong = new enumLyDoTangGiamForBaoCao().ToSelectList().ToList();
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL07_TSNN;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_PL07_TANG_GIAM_TSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL07_TSNN) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_PL07_TANG_GIAM_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var obj = _baoCaoQuocHoiService.TS_BCQH_PL07_TANG_GIAM_TSNN(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, /*CapDonVi: searchModel.CapDonVi,*/ stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), LyDo: searchModel.LyDoID, stringLyDo: searchModel.StringLyDoID);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCQH_PL08_TANG_GIAM_OTO_VSD()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL08_SD_OTO;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_PL08_TANG_GIAM_OTO_VSD_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL08_SD_OTO) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_PL08_OTO_VSD(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoQuocHoiService.BCQH_MAU08_OTO_VSD(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), ngayBaoCao: searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, donviDienTich: searchModel.DonViDienTich);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCQH_PL09_TANG_GIAM_TS_TREN500()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL09_NG500;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_PL09_TANG_GIAM_TS_TREN500_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL09_NG500) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_PL09_TANG_GIAM_TS_TREN500(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoQuocHoiService.BCQH_PL09_TANG_GIAM_TS_TREN500(donViId: searchModel.DonVi, donViTien: searchModel.DonViTien, ngayBaoCao: searchModel.NgayKetThuc);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCQH_PL10_TANG_GIAM_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL10_TSNN;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_PL10_TANG_GIAM_TSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL10_TSNN) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_PL10_TANG_GIAM_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoQuocHoiService.TS_BCQH_PL10_TANG_GIAM_TSNN(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, /*CapDonVi: searchModel.CapDonVi,*/ stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), LyDo: searchModel.LyDoID, stringLyDo: searchModel.StringLyDoID);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TS_BCQH_PL10_TANG_GIAM_TSNN_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL10_TSNN, searchModel, searchModel.FileType, typeof(rptTS_BCQH_PL10_TANG_GIAM_TSNN).FullName, typeof(TS_BCQH_PL10_TANG_GIAM_TSNN).FullName);
                _baoCaoQuocHoiService.TS_BCQH_PL10_TANG_GIAM_TSNN(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, /*CapDonVi: searchModel.CapDonVi,*/ stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), LyDo: searchModel.LyDoID, stringLyDo: searchModel.StringLyDoID, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoQuocHoiTaiSan });
        }

        public virtual IActionResult TS_TANG_GIAM_PL10_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL10_TSNN;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_TANG_GIAM_PL10_TSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL10_TSNN) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_PL10_TANG_GIAM_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            //new rptTS_TANG_GIAM_PL10_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            model.DataSource = null;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCQH_PL12_TANG_GIAM_NHOM_DV()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL12_DV;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_PL12_TANG_GIAM_NHOM_DV_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL12_DV) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_PL012_TANG_GIAM_NHOM_DV(searchModel, cauHinhModel, cauHinhChunghModel);
            searchModel.MauSo = 3;
            var obj = _baoCaoQuocHoiService.TS_BCQH_PL12_TANG_GIAM_TSNN_NHOM_DV(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc, LoaiTaiSan: (int)searchModel.LoaiTaiSan, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, /*CapDonVi: searchModel.CapDonVi,*/ stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), LyDo: searchModel.LyDoID, stringLyDo: searchModel.StringLyDoID);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_TANG_GIAM_PL13_HD()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL13_HD;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_TANG_GIAM_PL13_HD_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_TANG_GIAM_PL13_HD) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new XtraReport();

            model.DataSource = null;
            return ShowViewReport(model);
        }

        #region Mẫu QH14 báo cáo ô tô chức danh

        public virtual IActionResult TS_BCQH_QH14A_CT_OTOCD()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_CT_OTOCD;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_QH14A_CT_OTOCD_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_CT_OTOCD) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_QH14_OTO_CHUCDANH(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var data = _baoCaoQuocHoiService.BCQH_QH14_OTO_CHUCDANH(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCQH_QH14B_TH_OTOCD_THC()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_TH_OTOCD_THC;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_QH14B_TH_OTOCD_THC_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_TH_OTOCD_THC) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_QH14_OTO_CHUCDANH(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var data = _baoCaoQuocHoiService.BCQH_QH14_OTO_CHUCDANH(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCQH_QH14C_TH_OTOCD_DV()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCQH_TH_OTOCD_DV;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCQH_QH14C_TH_OTOCD_DV_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCQH_TH_OTOCD_DV) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCQH_QH14C_OTO_CHUCDANH_DV(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //int dv_dpac_id = 99284;
            //var data = _baoCaoQuocHoiService.BCQH_QH14_OTO_CHUCDANH(dv_dpac_id, searchModel.NgayKetThuc);
            var data = _baoCaoQuocHoiService.BCQH_QH14_OTO_CHUCDANH(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TS_BCQH_QH14C_TH_OTOCD_DV_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCQH_TH_OTOCD_DV, searchModel, searchModel.FileType, typeof(rptTS_BCQH_QH14C_OTO_CHUCDANH_DV).FullName, typeof(TS_BCQH_QH14_OTO_CHUCDANH).FullName);
                _baoCaoQuocHoiService.BCQH_QH14_OTO_CHUCDANH(searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), searchModel.NgayKetThuc, donViTien: searchModel.DonViTien, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoQuocHoiTaiSan });
        }

        #endregion Mẫu QH14 báo cáo ô tô chức danh

        #endregion Báo cáo quốc hội

        #region Báo cáo công khai

        public virtual IActionResult TS_BCCK_M01_KHMS_TS()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_M01_KHMS_TS;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_M01_KHMS_TS_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCCK_M01_KHMS_TS) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_M01_KHMS_TS(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            model.DataSource = null;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_M02_KQMS_TS()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_M02_KQMS_TS;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_M02_KQMS_TS_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCCK_M02_KQMS_TS) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_M02_KQMS_TS(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            model.DataSource = null;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_M03_QLNHA_DAT()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_M03_QLNHA_DAT;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_M03_QLNHA_DAT_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCCK_M03_QLNHA_DAT) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_M03_QLNHA_DAT(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            model.DataSource = null;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_M04_QLOTO_KHAC()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_M04_QLOTO_KHAC;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_M04_QLOTO_KHAC_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCCK_M04_QLOTO_KHAC) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_M04_QLOTO_KHAC(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            model.DataSource = null;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_M05_THUE_TS()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_M05_THUE_TS;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_M05_THUE_TS_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCCK_M05_THUE_TS) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_M05_THUE_TS(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            model.DataSource = null;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_M06_CHUYENDOI_SOHUU()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_M06_CHUYENDOI_SOHUU;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_M06_CHUYENDOI_SOHUU_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCCK_M06_CHUYENDOI_SOHUU) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_M06_CHUYENDOI_SOHUU(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            model.DataSource = null;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_M07_KQ_MUASAM()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_M07_KQ_MUASAM;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_M07_KQ_MUASAM_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCCK_M07_KQ_MUASAM) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_M07_KQ_MUASAM(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            model.DataSource = _baoCaoCongKhaiService.BaoCaoCongKhai_07_CK_TSC(donViId: _workContext.CurrentDonVi.ID, namBaoCao: searchModel.NamBaoCao, strLoaiTaiSanId: searchModel.StringLoaiTaiSan, donViTien: searchModel.DonViTien);
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_9a_CK_TSC_TINHHINH_DAUTU()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanCongKhaiSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_9a_CK_TSC_TINHHINH_DAUTU;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_9a_CK_TSC_TINHHINH_DAUTU_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_BCCK_9a_CK_TSC_TINHHINH_DAUTU);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            if (donvi != null)
            {
                if (donvi.PARENT_ID != null)
                {
                    var donvicha = _donViService.GetDonViById((decimal)donvi.PARENT_ID);
                    searchModel.TEN_DON_VI_CAP_TREN = donvicha != null ? donvicha.TEN : _workContext.CurrentDonVi.TEN_DON_VI;
                }
                else
                {
                    searchModel.TEN_DON_VI_CAP_TREN = _workContext.CurrentDonVi.TEN_DON_VI;
                }
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_9a_CK_TSC_TINHHINH_DAUTU(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            var data = _baoCaoCongKhaiService.BaoCaoCongKhai_09A_CK_TSC(donViId: _workContext.CurrentDonVi.ID, namBaoCao: searchModel.NamBaoCao, strLoaiTaiSanId: searchModel.StringLoaiTaiSan, strLyDoId: searchModel.StringLyDoID, donViTien: searchModel.DonViTien);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_9b_CK_TSC_QL_SD_TRU_SO()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanCongKhaiSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_9b_CK_TSC_QL_SD_TRU_SO;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_9b_CK_TSC_QL_SD_TRU_SO_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_BCCK_9b_CK_TSC_QL_SD_TRU_SO);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            if (donvi != null)
            {
                if (donvi.PARENT_ID != null)
                {
                    var donvicha = _donViService.GetDonViById((decimal)donvi.PARENT_ID);
                    searchModel.TEN_DON_VI_CAP_TREN = donvicha != null ? donvicha.TEN : _workContext.CurrentDonVi.TEN_DON_VI;
                }
                else
                {
                    searchModel.TEN_DON_VI_CAP_TREN = _workContext.CurrentDonVi.TEN_DON_VI;
                }
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_9b_CK_TSC_QL_SD_TRU_SO(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var data = _baoCaoCongKhaiService.BaoCaoCongKhai_09B_CK_TSC(donViId: _workContext.CurrentDonVi.ID, namBaoCao: searchModel.NamBaoCao, strLoaiTaiSanId: searchModel.StringLoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_9c_CK_TSC_QL_SD_OTO_KHAC()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanCongKhaiSearchModel(searchModel);
            int[] valueExclude = new int[] { (int)enumLOAI_HINH_TAI_SAN.ALL, (int)enumLOAI_HINH_TAI_SAN.KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.DAT, (int)enumLOAI_HINH_TAI_SAN.NHA };
            searchModel.LoaiHinhTaiSanAvaliable = searchModel.enumLoaiHinhTaiSan.ToSelectList(valuesToExclude: valueExclude);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_9c_CK_TSC_QL_SD_OTO_KHAC;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_9c_CK_TSC_QL_SD_OTO_KHAC_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_BCCK_9c_CK_TSC_QL_SD_OTO_KHAC);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            if (donvi != null)
            {
                if (donvi.PARENT_ID != null)
                {
                    var donvicha = _donViService.GetDonViById((decimal)donvi.PARENT_ID);
                    searchModel.TEN_DON_VI_CAP_TREN = donvicha != null ? donvicha.TEN : _workContext.CurrentDonVi.TEN_DON_VI;
                }
                else
                {
                    searchModel.TEN_DON_VI_CAP_TREN = _workContext.CurrentDonVi.TEN_DON_VI;
                }
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_9c_CK_TSC_QL_SD_OTO_KHAC(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var data = _baoCaoCongKhaiService.BaoCaoCongKhai_09C_CK_TSC(donViId: _workContext.CurrentDonVi.ID, namBaoCao: searchModel.NamBaoCao, strLoaiTaiSanId: searchModel.StringLoaiTaiSan, donViTien: searchModel.DonViTien);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_9d_CK_TSC_XULY_TS()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanCongKhaiSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_9d_CK_TSC_XULY_TS;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_9d_CK_TSC_XULY_TS_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_BCCK_9d_CK_TSC_XULY_TS);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            if (donvi != null)
            {
                if (donvi.PARENT_ID != null)
                {
                    var donvicha = _donViService.GetDonViById((decimal)donvi.PARENT_ID);
                    searchModel.TEN_DON_VI_CAP_TREN = donvicha != null ? donvicha.TEN : _workContext.CurrentDonVi.TEN_DON_VI;
                }
                else
                {
                    searchModel.TEN_DON_VI_CAP_TREN = _workContext.CurrentDonVi.TEN_DON_VI;
                }
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_9d_CK_TSC_XULY_TS(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            var data = _baoCaoCongKhaiService.BaoCaoCongKhai_09D_CK_TSC(donViId: _workContext.CurrentDonVi.ID, namBaoCao: searchModel.NamBaoCao, strLoaiTaiSanId: searchModel.StringLoaiTaiSan, donViTien: searchModel.DonViTien);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_9dd_CK_TSC_KHAITHAC_TC()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanCongKhaiSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_9dd_CK_TSC_KHAITHAC_TC;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_9dd_CK_TSC_KHAITHAC_TC_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_BCCK_9dd_CK_TSC_KHAITHAC_TC);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            if (donvi != null)
            {
                if (donvi.PARENT_ID != null)
                {
                    var donvicha = _donViService.GetDonViById((decimal)donvi.PARENT_ID);
                    searchModel.TEN_DON_VI_CAP_TREN = donvicha != null ? donvicha.TEN : _workContext.CurrentDonVi.TEN_DON_VI;
                }
                else
                {
                    searchModel.TEN_DON_VI_CAP_TREN = _workContext.CurrentDonVi.TEN_DON_VI;
                }
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_9dd_CK_TSC_KHAITHAC_TC(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            var data = _baoCaoCongKhaiService.BaoCaoCongKhai_09DD_CK_TSC(donViId: _workContext.CurrentDonVi.ID, namBaoCao: searchModel.NamBaoCao, strLoaiTaiSanId: searchModel.StringLoaiTaiSan, donViTien: searchModel.DonViTien);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_10a_CK_TSC_DAUTU_XD_THUE()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanCongKhaiSearchModel(searchModel);
            searchModel.NamBaoCao = DateTime.Now.Year;
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_10a_CK_TSC_DAUTU_XD_THUE;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_10a_CK_TSC_DAUTU_XD_THUE_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_BCCK_10a_CK_TSC_DAUTU_XD_THUE);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            if (string.IsNullOrWhiteSpace(searchModel.StringDonVi))
            {
                searchModel.StringDonVi = _workContext.CurrentDonVi.ID.ToString();
            }
            searchModel.enumListLoaiHinhTS = new List<enumNHAN_HIEN_THI_LOAI_HINH_TS>() { enumNHAN_HIEN_THI_LOAI_HINH_TS.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TS.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TS.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TS.TAI_SAN_CO_DINH_KHAC };
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            //_reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_10a_CK_TSC_DAUTU_XD_THUE(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var data = _baoCaoCongKhaiService.BaoCaoCongKhai_10A_CK_TSC(donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, namBaoCao: searchModel.NamBaoCao, stringDonVi: searchModel.StringDonVi, strLoaiTaiSanId: searchModel.StringLoaiTaiSan, bacDonVi: searchModel.BacDonVi);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_10b_CK_TSC_QLSD_TS()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanCongKhaiSearchModel(searchModel);
            searchModel.NamBaoCao = DateTime.Now.Year;
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_10b_CK_TSC_QLSD_TS;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_10b_CK_TSC_QLSD_TS_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_BCCK_10b_CK_TSC_QLSD_TS);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            if (string.IsNullOrWhiteSpace(searchModel.StringDonVi))
            {
                searchModel.StringDonVi = _workContext.CurrentDonVi.ID.ToString();
            }
            searchModel.enumListLoaiHinhTS = new List<enumNHAN_HIEN_THI_LOAI_HINH_TS>() { enumNHAN_HIEN_THI_LOAI_HINH_TS.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TS.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TS.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TS.TAI_SAN_CO_DINH_KHAC };
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_10b_CK_TSC_QLSD_TS(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var data = _baoCaoCongKhaiService.BaoCaoCongKhai_10B_CK_TSC(donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, namBaoCao: searchModel.NamBaoCao, stringDonVi: searchModel.StringDonVi, strLoaiTaiSanId: searchModel.StringLoaiTaiSan, bacDonVi: searchModel.BacDonVi);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TS_BCCK_10b_CK_TSC_QLSD_TS_CHAY_NGAM(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                searchModel.enumListLoaiHinhTS = new List<enumNHAN_HIEN_THI_LOAI_HINH_TS>() { enumNHAN_HIEN_THI_LOAI_HINH_TS.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TS.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TS.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TS.TAI_SAN_CO_DINH_KHAC };
                _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
                //đẩy tham số
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(maBaoCao: enumMA_BAO_CAO.TS_BCCK_10b_CK_TSC_QLSD_TS, searchModel: searchModel, FileType: searchModel.FileType, ClassReportFullName: typeof(rptTS_BCCK_10b_CK_TSC_QLSD_TS).FullName, ModelReturnFullName: typeof(TS_BCCK_10B_CK_TSC).FullName);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo file báo cáo", queue, "QueueProcess");
                _baoCaoCongKhaiService.BaoCaoCongKhai_10B_CK_TSC(donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, namBaoCao: searchModel.NamBaoCao, stringDonVi: searchModel.StringDonVi, strLoaiTaiSanId: searchModel.StringLoaiTaiSan, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoQuocHoiTaiSan });
        }

        public virtual IActionResult TS_BCCK_10c_CK_TSC_XL_TS()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanCongKhaiSearchModel(searchModel);
            searchModel.NamBaoCao = DateTime.Now.Year;
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_10c_CK_TSC_XL_TS;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_10c_CK_TSC_XL_TS_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_BCCK_10c_CK_TSC_XL_TS);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            if (string.IsNullOrWhiteSpace(searchModel.StringDonVi))
            {
                searchModel.StringDonVi = _workContext.CurrentDonVi.ID.ToString();
            }
            searchModel.enumListLoaiHinhTS = new List<enumNHAN_HIEN_THI_LOAI_HINH_TS>() { enumNHAN_HIEN_THI_LOAI_HINH_TS.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TS.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TS.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TS.TAI_SAN_CO_DINH_KHAC };
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            //_reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_10c_CK_TSC_XL_TS(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var data = _baoCaoCongKhaiService.BaoCaoCongKhai_10C_CK_TSC(donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, namBaoCao: searchModel.NamBaoCao, stringDonVi: searchModel.StringDonVi, strLoaiTaiSanId: searchModel.StringLoaiTaiSan, bacDonVi: searchModel.BacDonVi);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_10d_CK_TSC_KTNL_TS()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanCongKhaiSearchModel(searchModel);
            searchModel.NamBaoCao = DateTime.Now.Year;
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_10d_CK_TSC_KTNL_TS;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_10d_CK_TSC_KTNL_TS_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_BCCK_10d_CK_TSC_KTNL_TS);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            if (string.IsNullOrWhiteSpace(searchModel.StringDonVi))
            {
                searchModel.StringDonVi = _workContext.CurrentDonVi.ID.ToString();
            }
            searchModel.enumListLoaiHinhTS = new List<enumNHAN_HIEN_THI_LOAI_HINH_TS>() { enumNHAN_HIEN_THI_LOAI_HINH_TS.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TS.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TS.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TS.TAI_SAN_CO_DINH_KHAC };
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            //lấy dữ liệu báo cáo
            XtraReport model = new rptTS_BCCK_10d_CK_TSC_KTNL_TS(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var data = _baoCaoCongKhaiService.BaoCaoCongKhai_10D_CK_TSC(donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, namBaoCao: searchModel.NamBaoCao, stringDonVi: searchModel.StringDonVi, strLoaiTaiSanId: searchModel.StringLoaiTaiSan, bacDonVi: searchModel.BacDonVi);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_11a_CK_TSC_NGUONLUC_TC_TS()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanCongKhaiSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_11a_CK_TSC_NGUONLUC_TC_TS;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_11a_CK_TSC_NGUONLUC_TC_TS_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_BCCK_11a_CK_TSC_NGUONLUC_TC_TS);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            searchModel.enumListLoaiHinhTS = new List<enumNHAN_HIEN_THI_LOAI_HINH_TS>() { enumNHAN_HIEN_THI_LOAI_HINH_TS.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TS.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TS.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TS.TAI_SAN_CO_DINH_KHAC };
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_11a_CK_TSC_NGUONLUC_TC_TS(searchModel, cauHinhModel, cauHinhChunghModel);
            searchModel.MauSo = 2;
            if (string.IsNullOrWhiteSpace(searchModel.StringDonVi))
            {
                searchModel.StringDonVi = _workContext.CurrentDonVi.ID.ToString();
            }
            //lấy dữ liệu báo cáo
            var data = _baoCaoCongKhaiService.BaoCaoCongKhai_11A_CK_TSC(stringDonVi: searchModel.StringDonVi, namBaoCao: searchModel.NamBaoCao, strLoaiTaiSanId: searchModel.StringLoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, bacDonVi: searchModel.BacDonVi, MauSo: searchModel.MauSo);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TS_BCCK_11a_CK_TSC_NGUONLUC_TC_TS_CHAY_NGAM(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                searchModel.enumListLoaiHinhTS = new List<enumNHAN_HIEN_THI_LOAI_HINH_TS>() { enumNHAN_HIEN_THI_LOAI_HINH_TS.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TS.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TS.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TS.TAI_SAN_CO_DINH_KHAC };
                _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
                if (string.IsNullOrWhiteSpace(searchModel.StringDonVi))
                {
                    searchModel.StringDonVi = _workContext.CurrentDonVi.ID.ToString();
                }
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCCK_11a_CK_TSC_NGUONLUC_TC_TS, searchModel, searchModel.FileType, typeof(rptTS_BCCK_11a_CK_TSC_NGUONLUC_TC_TS).FullName, typeof(TS_BCCK_11A_CK_TSC).FullName);
                _baoCaoCongKhaiService.BaoCaoCongKhai_11A_CK_TSC(stringDonVi: searchModel.StringDonVi, namBaoCao: searchModel.NamBaoCao, strLoaiTaiSanId: searchModel.StringLoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoCongKhaiTaiSan });
        }

        public virtual IActionResult TS_BCCK_11b_CK_TSC_QL_SD_TS()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanCongKhaiSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_11b_CK_TSC_QL_SD_TS;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_11b_CK_TSC_QL_SD_TS_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_BCCK_11b_CK_TSC_QL_SD_TS);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            searchModel.enumListLoaiHinhTS = new List<enumNHAN_HIEN_THI_LOAI_HINH_TS>() { enumNHAN_HIEN_THI_LOAI_HINH_TS.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TS.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TS.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TS.TAI_SAN_CO_DINH_KHAC };
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            //_reportModelFactory.PrepareEnumeBaoCaoChiTiet(searchModel);
            XtraReport model = new rptTS_BCCK_11b_CK_TSC_QL_SD_TS(searchModel, cauHinhModel, cauHinhChunghModel);
            searchModel.MauSo = 2;
            if (string.IsNullOrWhiteSpace(searchModel.StringDonVi))
            {
                searchModel.StringDonVi = _workContext.CurrentDonVi.ID.ToString();
            }
            //lấy dữ liệu báo cáo

            var data = _baoCaoCongKhaiService.BaoCaoCongKhai_11B_CK_TSC(stringDonVi: searchModel.StringDonVi, namBaoCao: searchModel.NamBaoCao, strLoaiTaiSanId: searchModel.StringLoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, bacDonVi: searchModel.BacDonVi, MauSo: searchModel.MauSo);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_11c_CK_TSC_XL_TS()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanCongKhaiSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_11c_CK_TSC_XL_TS;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_11c_CK_TSC_XL_TS_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_BCCK_11c_CK_TSC_XL_TS);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            searchModel.enumListLoaiHinhTS = new List<enumNHAN_HIEN_THI_LOAI_HINH_TS>() { enumNHAN_HIEN_THI_LOAI_HINH_TS.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TS.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TS.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TS.TAI_SAN_CO_DINH_KHAC };
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_11c_CK_TSC_XL_TS(searchModel, cauHinhModel, cauHinhChunghModel);
            searchModel.MauSo = 2;
            if (string.IsNullOrWhiteSpace(searchModel.StringDonVi))
            {
                searchModel.StringDonVi = _workContext.CurrentDonVi.ID.ToString();
            }
            //lấy dữ liệu báo cáo
            var data = _baoCaoCongKhaiService.BaoCaoCongKhai_11C_CK_TSC(stringDonVi: searchModel.StringDonVi, namBaoCao: searchModel.NamBaoCao, strLoaiTaiSanId: searchModel.StringLoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, bacDonVi: searchModel.BacDonVi, MauSo: searchModel.MauSo);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TS_BCCK_11c_CK_TSC_XL_TS_CHAY_NGAM(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                searchModel.enumListLoaiHinhTS = new List<enumNHAN_HIEN_THI_LOAI_HINH_TS>() { enumNHAN_HIEN_THI_LOAI_HINH_TS.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TS.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TS.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TS.TAI_SAN_CO_DINH_KHAC };
                _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
                if (string.IsNullOrWhiteSpace(searchModel.StringDonVi))
                {
                    searchModel.StringDonVi = _workContext.CurrentDonVi.ID.ToString();
                }
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCCK_11c_CK_TSC_XL_TS, searchModel, searchModel.FileType, typeof(rptTS_BCCK_11c_CK_TSC_XL_TS).FullName, typeof(TS_BCCK_11C_CK_TSC).FullName);
                _baoCaoCongKhaiService.BaoCaoCongKhai_11C_CK_TSC(stringDonVi: searchModel.StringDonVi, namBaoCao: searchModel.NamBaoCao, strLoaiTaiSanId: searchModel.StringLoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoCongKhaiTaiSan });
        }

        public virtual IActionResult TS_BCCK_11d_CK_TSC_KTNL_TS()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanCongKhaiSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_11d_CK_TSC_KTNL_TS;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_11d_CK_TSC_KTNL_TS_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_BCCK_11d_CK_TSC_KTNL_TS);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            searchModel.enumListLoaiHinhTS = new List<enumNHAN_HIEN_THI_LOAI_HINH_TS>() { enumNHAN_HIEN_THI_LOAI_HINH_TS.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TS.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TS.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TS.TAI_SAN_CO_DINH_KHAC };
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCK_11d_CK_TSC_KTNL_TS(searchModel, cauHinhModel, cauHinhChunghModel);
            searchModel.MauSo = 2;
            if (string.IsNullOrWhiteSpace(searchModel.StringDonVi))
            {
                searchModel.StringDonVi = _workContext.CurrentDonVi.ID.ToString();
            }
            //lấy dữ liệu báo cáo
            var data = _baoCaoCongKhaiService.BaoCaoCongKhai_11D_CK_TSC(stringDonVi: searchModel.StringDonVi, namBaoCao: searchModel.NamBaoCao, strLoaiTaiSanId: searchModel.StringLoaiTaiSan, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, bacDonVi: searchModel.BacDonVi, MauSo: searchModel.MauSo);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_TDMS()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_TDMS;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_TDMS_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCCK_TDMS) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCCK_TIEN_DO_MUA_SAM(searchModel, cauHinhModel, cauHinhChunghModel);
            model.DataSource = _baoCaoCongKhaiService.BaoCaoCongKhai_TDMS_TSC(donViId: searchModel.DonVi, namBaoCao: searchModel.NamBaoCao, strLoaiTaiSanId: searchModel.StringLoaiTaiSan);
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_THMS()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_TDMS;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_THMS_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCCK_THMS) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCCK_TINH_HINH_MUA_SAM(searchModel, cauHinhModel, cauHinhChunghModel);
            model.DataSource = _baoCaoCongKhaiService.BaoCaoCongKhai_THMS_TSC(donViId: searchModel.DonVi, namBaoCao: searchModel.NamBaoCao, strLoaiTaiSanId: searchModel.StringLoaiTaiSan);
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCCK_DM_TS_DIEU_CHUYEN_BAN_GIAO()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCongKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCK_DM_TS_DIEU_CHUYEN_BAN_GIAO;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCK_DM_TS_DIEU_CHUYEN_BAN_GIAO_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCCK_DM_TS_DIEU_CHUYEN_BAN_GIAO) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCCK_DM_TS_DIEU_CHUYEN_BAN_GIAO(searchModel, cauHinhModel, cauHinhChunghModel);
            model.DataSource = _baoCaoCongKhaiService.BaoCaoCongKhai_DIEU_CHUYEN_BAN_GIAO_TSC(donViId: searchModel.DonVi, donViTien: searchModel.DonViTien, namBaoCao: searchModel.NamBaoCao);
            return ShowViewReport(model);
        }

        #endregion Báo cáo công khai

        #region Báo cáo kê khai

        #region Thông tư 245/2009/TT-BTC ngày 31/12/2009 của Bộ Tài chính

        public virtual IActionResult TS_BCKK_01_DKTSNN_KEKHAITRUSO()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoKeKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCKK_01_DKTSNN_KEKHAITRUSO;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCKK_01_DKTSNN_KEKHAITRUSO_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCKK_01_DKTSNN_KEKHAITRUSO) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCKK_01_DKTSNN_KEKHAITRUSO(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            var data = _baoCaoKeKhaiServices.TS_BCKK_01_DK_TSNN(ngayKetThuc: searchModel.NgayKetThuc, donViId: (int)_workContext.CurrentDonVi.ID);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCKK_02_DKTSNN_KEKHAIOTO()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoKeKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoKeKhaiSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCKK_02_DKTSNN_KEKHAIOTO;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCKK_02_DKTSNN_KEKHAIOTO_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCKK_02_DKTSNN_KEKHAIOTO) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            searchModel.BacTaiSan = 2;
            XtraReport model = new rptTS_BCKK_02_DKTSNN_KEKHAIOTO(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            var data = _baoCaoKeKhaiServices.TS_BCKK_02_DK_TSNN(ngayKetThuc: searchModel.NgayKetThuc, donViId: (int)_workContext.CurrentDonVi.ID);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCKK_03_DKTSNN_KEKHAITS500()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoKeKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoKeKhaiSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCKK_03_DKTSNN_KEKHAITS500;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCKK_03_DKTSNN_KEKHAITS500_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCKK_03_DKTSNN_KEKHAITS500) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            searchModel.BacTaiSan = 5;
            XtraReport model = new rptTS_BCKK_03_DKTSNN_KEKHAITS500(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            var data = _baoCaoKeKhaiServices.TS_BCKK_03_DK_TSNN(ngayKetThuc: searchModel.NgayKetThuc, donViId: (int)_workContext.CurrentDonVi.ID);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCKK_06a_DKTSC_DVTDTTDON_VI()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoKeKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCKK_06a_DKTSC_DVTDTTDON_VI;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCKK_06a_DKTSC_DVTDTTDON_VI_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCKK_06a_DKTSC_DVTDTTDON_VI) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCKK_06a_DKTSC_DVSDTS(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            var data = _baoCaoKeKhaiServices.TS_BCKK_06A_DK_TSC(ngayTu: searchModel.NgayBatDau, ngayDen: searchModel.NgayKetThuc, donViId: searchModel.DonVi == 0 ? (int)_workContext.CurrentDonVi.ID : searchModel.DonVi);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCKK_06b_DKTSC_DVTDTTNHA_DAT()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoKeKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCKK_06b_DKTSC_DVTDTTNHA_DAT;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCKK_06b_DKTSC_DVTDTTNHA_DAT_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCKK_06b_DKTSC_DVTDTTNHA_DAT) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCKK_06b_DKTSC_DVTSTRUSO(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            var data = _baoCaoKeKhaiServices.TS_BCKK_06B_DK_TSC(ngayTu: searchModel.NgayBatDau, ngayDen: searchModel.NgayKetThuc, donViId: searchModel.DonVi == 0 ? (int)_workContext.CurrentDonVi.ID : searchModel.DonVi, donViDienTich: searchModel.DonViDienTich, donViTien: searchModel.DonViTien);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCKK_06c_DKTSC_DVTDTTOTO()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoKeKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCKK_06c_DKTSC_DVTDTTOTO;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCKK_06c_DKTSC_DVTDTTOTO_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCKK_06c_DKTSC_DVTDTTOTO) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCKK_06c_DKTSC_DVTDTTOTO(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            var data = _baoCaoKeKhaiServices.TS_BCKK_06C_DK_TSC(ngayTu: searchModel.NgayBatDau, ngayDen: searchModel.NgayKetThuc, donViId: searchModel.DonVi == 0 ? (int)_workContext.CurrentDonVi.ID : searchModel.DonVi, donViTien: searchModel.DonViTien);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCKK_06d_DKTSC_DVTDTTKHAC()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoKeKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCKK_06d_DKTSC_DVTDTTKHAC;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCKK_06d_DKTSC_DVTDTTKHAC_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null)
            {
                cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault();
            }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCKK_06d_DKTSC_DVTDTTKHAC) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCKK_06d_DKTSC_DVTDTTKHAC(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            var data = _baoCaoKeKhaiServices.TS_BCKK_06D_DK_TSC(ngayTu: searchModel.NgayBatDau, ngayDen: searchModel.NgayKetThuc, donViId: searchModel.DonVi == 0 ? (int)_workContext.CurrentDonVi.ID : searchModel.DonVi, donViTien: searchModel.DonViTien, LoaiHinhTaiSnId: searchModel.StringLoaiTaiSan);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        #endregion Thông tư 245/2009/TT-BTC ngày 31/12/2009 của Bộ Tài chính

        #region Thông tư 144/2017/TT-BTC ngày 29/12/2017 của Bộ Tài chính

        public virtual IActionResult TS_BCKK_04a_DKTSC_KEKHAITRUSO()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoKeKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.NgayBaoCao = DateTime.Now;
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCKK_04a_DKTSC_KEKHAITRUSO;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCKK_04a_DKTSC_KEKHAITRUSO_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
            var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCKK_04a_DKTSC_KEKHAITRUSO) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCKK_04a_DKTSC_KEKHAITRUSO(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var data = _baoCaoKeKhaiServices.TS_BCKK_04A_DK_TSC(ngayBaoCao: searchModel.NgayBaoCao, donViId: searchModel.DonVi, donViDienTich: searchModel.DonViDienTich, donViTien: searchModel.DonViTien);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCKK_04b_DKTSC_KEKHAIOTO()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoKeKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.NgayBaoCao = DateTime.Now;
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCKK_04b_DKTSC_KEKHAIOTO;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCKK_04b_DKTSC_KEKHAIOTO_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCKK_04b_DKTSC_KEKHAIOTO) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCKK_04b_DKTSC_KEKHAIOTO(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            var data = _baoCaoKeKhaiServices.TS_BCKK_04B_DK_TSC(ngayBaoCao: searchModel.NgayBaoCao, donViId: searchModel.DonVi, donViDienTich: searchModel.DonViDienTich, donViTien: searchModel.DonViTien);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCKK_04c_DKTSNN_KEKHAITSKHAC()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoKeKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.NgayBaoCao = DateTime.Now;
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCKK_04c_DKTSNN_KEKHAITSKHAC;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCKK_04c_DKTSNN_KEKHAITSKHAC_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var maBaoCao = enumMA_BAO_CAO.TS_BCKK_04c_DKTSNN_KEKHAITSKHAC;
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCKK_04c_DKTSNN_KEKHAITSKHAC) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCKK_04c_DKTSNN_KEKHAITSKHAC(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var data = _baoCaoKeKhaiServices.TS_BCKK_04C_DK_TSC(ngayBaoCao: searchModel.NgayBaoCao, donViId: searchModel.DonVi, donViDienTich: searchModel.DonViDienTich, donViTien: searchModel.DonViTien, LoaiHinhTaiSnId: searchModel.StringLoaiTaiSan);
            model.DataSource = data;
            if (searchModel.IsExportExcel)
            {
                return Export_Excel_CT(model, searchModel, maBaoCao);

            }
            else
            {
                return ShowViewReport(model);
            }
        }

        public virtual IActionResult TS_BCKK_05a_DKTSDA_KEKHAITRUSO()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoKeKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoKeKhaiSearchModel(searchModel: searchModel, IsMulti: false, isTaiSanDuAn: true);
            searchModel.NgayBaoCao = DateTime.Now;
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCKK_05a_DKTSDA_KEKHAITRUSO;

            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCKK_05a_DKTSDA_KEKHAITRUSO_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCKK_05a_DKTSDA_KEKHAITRUSO) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCKK_05a_DKTSDA_KEKHAITRUSO(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            var data = _baoCaoKeKhaiServices.TS_BCKK_05A_DK_TSDA(ngayBaoCao: searchModel.NgayBaoCao, donViId: (int)_workContext.CurrentDonVi.ID, donViTien: searchModel.DonViTien, donViDienTich: searchModel.DonViDienTich, duAnId: searchModel.DU_AN_ID);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCKK_05b_DKTSDA_KEKHAIOTO()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoKeKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoKeKhaiSearchModel(searchModel: searchModel, IsMulti: false, isTaiSanDuAn: true);
            searchModel.NgayBaoCao = DateTime.Now;
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCKK_05b_DKTSDA_KEKHAIOTO;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCKK_05b_DKTSDA_KEKHAIOTO_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCKK_05a_DKTSDA_KEKHAITRUSO) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCKK_05b_DKTSDA_KEKHAIOTO(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            var data = _baoCaoKeKhaiServices.TS_BCKK_05B_DK_TSDA(ngayBaoCao: searchModel.NgayBaoCao, donViTien: searchModel.DonViTien, donViId: (int)_workContext.CurrentDonVi.ID, duAnId: searchModel.DU_AN_ID);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCKK_05c_DKTSDA_KEKHAIKHAC()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoKeKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoKeKhaiSearchModel(searchModel: searchModel, IsMulti: false, isTaiSanDuAn: true);
            searchModel.NgayBaoCao = DateTime.Now;
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCKK_05c_DKTSDA_KEKHAIKHAC;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCKK_05c_DKTSDA_KEKHAIKHAC_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCKK_05c_DKTSDA_KEKHAIKHAC) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCKK_05c_DKTSDA_KEKHAIKHAC(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            //var data = "";
            var data = _baoCaoKeKhaiServices.TS_BCKK_05C_DK_TSDA(ngayBaoCao: searchModel.NgayBaoCao, donViTien: searchModel.DonViTien, donViId: (int)_workContext.CurrentDonVi.ID, duAnId: searchModel.DU_AN_ID);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCKK_07_DKTSC_XOATS_CSDL()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoKeKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.NgayBaoCao = DateTime.Now;
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCKK_07_DKTSC_XOATS_CSDL;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCKK_07_DKTSC_XOATS_CSDL_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCKK_07_DKTSC_XOATS_CSDL) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCKK_07_DKTSC_XOATS_CSDL(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var data = _baoCaoKeKhaiServices.TS_BCKK_07_DK_TSC(ngayTu: searchModel.NgayBatDau, ngayDen: searchModel.NgayKetThuc, donViId: searchModel.DonVi, LoaiHinhTaiSnId: searchModel.StringLoaiTaiSan);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCKK_01_DMTSNN_GIAM_NHA_DAT()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoKeKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.NgayBaoCao = DateTime.Now;
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCKK_01_DMTSNN_GIAM_NHA_DAT;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCKK_01_DMTSNN_GIAM_NHA_DAT_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCKK_01_DMTSNN_GIAM_NHA_DAT) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCKK_01_DMTSNN_GIAM_NHA_DAT(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var data = _baoCaoKeKhaiServices.TS_BCKK_01_DMTSNN(ngayBaoCao: searchModel.NgayBaoCao, donViId: searchModel.DonVi, donViDienTich: searchModel.DonViDienTich, donViTien: searchModel.DonViTien, stringLyDo: searchModel.StringLyDoID);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCKK_02_DMTSNN_GIAM_OTO()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoKeKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.NgayBaoCao = DateTime.Now;
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCKK_02_DMTSNN_GIAM_OTO;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCKK_02_DMTSNN_GIAM_OTO_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCKK_02_DMTSNN_GIAM_OTO) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCKK_02_DMTSNN_GIAM_OTO(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var data = _baoCaoKeKhaiServices.TS_BCKK_02_DMTSNN(ngayBaoCao: searchModel.NgayBaoCao, donViId: searchModel.DonVi, donViDienTich: searchModel.DonViDienTich, donViTien: searchModel.DonViTien, stringLyDo: searchModel.StringLyDoID);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCKK_03_DMTSNN_GIAM_KHAC()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoKeKhaiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.NgayBaoCao = DateTime.Now;
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCKK_03_DMTSNN_GIAM_KHAC;
            int[] valueExclude = new int[] { (int)enumLOAI_HINH_TAI_SAN.ALL, (int)enumLOAI_HINH_TAI_SAN.DAT, (int)enumLOAI_HINH_TAI_SAN.NHA, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.OTO, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC };
            searchModel.LoaiHinhTaiSanAvaliable = searchModel.enumLoaiHinhTaiSan.ToSelectList(valuesToExclude: valueExclude);
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCKK_03_DMTSNN_GIAM_KHAC_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCKK_03_DMTSNN_GIAM_KHAC) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCKK_03_DMTSNN_GIAM_KHAC(searchModel, cauHinhModel, cauHinhChunghModel);
            //lấy dữ liệu báo cáo
            var data = _baoCaoKeKhaiServices.TS_BCKK_03_DMTSNN(ngayBaoCao: searchModel.NgayBaoCao, donViId: searchModel.DonVi, donViDienTich: searchModel.DonViDienTich, donViTien: searchModel.DonViTien, stringLyDo: searchModel.StringLyDoID, stringLoaiTaiSan: searchModel.StringLoaiTaiSan);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        #endregion Thông tư 144/2017/TT-BTC ngày 29/12/2017 của Bộ Tài chính

        #endregion Báo cáo kê khai

        #region Báo cáo tra cứu

        public virtual IActionResult TS_BCTC_04_DK_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTraCuuSoLieu))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTraCuuSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTC_04_DK_TSNN;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCTC_04_DK_TSNN_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCTC_04_DK_TSNN) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoTraCuu(searchModel);
            XtraReport model = new rptTS_BCTC_04_DK_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoTraCuuService.BaoCaoTraCuuTS_BCTC_04_DK_TSNN(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, BacDonVi: searchModel.BacDonVi, stringCapHanhChinh: searchModel.StringCapHanhChinh);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TS_BCTC_04_DK_TSNN_CHAYNGAM(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCaoTraCuu(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCTC_04_DK_TSNN, searchModel, searchModel.FileType, typeof(rptTS_BCTC_04_DK_TSNN).FullName, typeof(TS_BCTC_04_DK_TSNN).FullName);
                _baoCaoTraCuuService.BaoCaoTraCuuTS_BCTC_04_DK_TSNN(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, stringCapHanhChinh: searchModel.StringCapHanhChinh, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoTraCuuSoLieu });
        }

        public virtual IActionResult TS_BCTC_03_DK_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTraCuuSoLieu))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTraCuuSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTC_03_DK_TSNN;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCTC_03_DK_TSNN_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var maBaoCao = enumMA_BAO_CAO.TS_BCTC_03_DK_TSNN;
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCTC_03_DK_TSNN) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCTC_03_DK_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoTraCuuService.BaoCaoTraCuuTS_BCTC_03_DK_TSNN(
                DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID,
                NgayKetThuc: searchModel.NgayKetThuc,
                ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(),
                donViTien: searchModel.DonViTien,
                DonViDienTich: searchModel.DonViDienTich,
                BacTaiSan: searchModel.BacTaiSan,
                stringLoaiTaiSan: searchModel.StringLoaiTaiSan,
                stringLoaiDonVi: searchModel.StringLoaiDonVi,
                NhanXeId: searchModel.NHAN_XE_ID,
                NamSD_CompareSign: searchModel.NamSD_CompareSign,
                NamSD_Value1: searchModel.NamSD_Value1,
                NamSD_Value2: searchModel.NamSD_Value2,
                NamSx_CompareSign: searchModel.NamSx_CompareSign,
                NamSx_Value1: searchModel.NamSx_Value1,
                NamSx_Value2: searchModel.NamSx_Value2,
                SoTang_CompareSign: searchModel.SoTang_CompareSign,
                SoTang_Value1: searchModel.SoTang_Value1,
                SoTang_Value2: searchModel.SoTang_Value2,
                DienTich_CompareSign: searchModel.DienTich_CompareSign,
                DienTich_Value1: searchModel.DienTich_Value1,
                DienTich_Value2: searchModel.DienTich_Value2,
                SoChoNgoi_CompareSign: searchModel.SoChoNgoi_CompareSign,
                SoChoNgoi_Value1: searchModel.SoChoNgoi_Value1,
                SoChoNgoi_Value2: searchModel.SoChoNgoi_Value2,
                TaiTrong_CompareSign: searchModel.TaiTrong_CompareSign,
                TaiTrong_Value1: searchModel.TaiTrong_Value1,
                TaiTrong_Value2: searchModel.TaiTrong_Value2,
                NguyenGiaNS_CompareSign: searchModel.NguyenGiaNS_CompareSign,
                NguyenGiaNS_Value1: searchModel.NguyenGiaNS_Value1,
                NguyenGiaNS_Value2: searchModel.NguyenGiaNS_Value2,
                NguyenGiaKhac_CompareSign: searchModel.NguyenGiaKhac_CompareSign,
                NguyenGiaKhac_Value1: searchModel.NguyenGiaKhac_Value1,
                NguyenGiaKhac_Value2: searchModel.NguyenGiaKhac_Value2,
                NguyenGiaVienTro_CompareSign: searchModel.NguyenGiaVienTro_CompareSign,
                NguyenGiaVienTro_Value1: searchModel.NguyenGiaVienTro_Value1,
                NguyenGiaVienTro_Value2: searchModel.NguyenGiaVienTro_Value2,
                NguyenGiaODA_CompareSign: searchModel.NguyenGiaODA_CompareSign,
                NguyenGiaODA_Value1: searchModel.NguyenGiaODA_Value1,
                NguyenGiaODA_Value2: searchModel.NguyenGiaODA_Value2,
                TongNguyenGia_CompareSign: searchModel.TongNguyenGia_CompareSign,
                TongNguyenGia_Value1: searchModel.TongNguyenGia_Value1,
                TongNguyenGia_Value2: searchModel.TongNguyenGia_Value2,
                TyLeChatLuong_CompareSign: searchModel.TyLeChatLuong_CompareSign,
                TyLeChatLuong_Value1: searchModel.TyLeChatLuong_Value1,
                TyLeChatLuong_Value2: searchModel.TyLeChatLuong_Value2,
                GTCL_CompareSign: searchModel.GTCL_CompareSign,
                GTCL_Value1: searchModel.GTCL_Value2,
                GTCL_Value2: searchModel.DienTich_Value2,
                ChucDanhId: searchModel.CHUC_DANH_ID,
                SoCau_CompareSign: searchModel.SoCau_CompareSign,
                SoCau_Value1: searchModel.SoCau_Value1,
                SoCau_Value2: searchModel.SoCau_Value2
                );
            model.DataSource = obj;
            if (searchModel.IsExportExcel)
            {
                return Export_Excel_CT(model, searchModel, maBaoCao);

            }
            else
            {
                return ShowViewReport(model);
            }
        }

        [HttpPost]
        public virtual IActionResult TS_BCTC_03_DK_TSNN_CHAYNGAM(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCaoTraCuu(searchModel);
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(enumMA_BAO_CAO.TS_BCTC_03_DK_TSNN, searchModel, searchModel.FileType, typeof(rptTS_BCTC_03_DK_TSNN).FullName, typeof(TS_BCTC_03_DK_TSNN).FullName);
                _baoCaoTraCuuService.BaoCaoTraCuuTS_BCTC_03_DK_TSNN(
                DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID,
                NgayKetThuc: searchModel.NgayKetThuc,
                ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(),
                donViTien: searchModel.DonViTien,
                DonViDienTich: searchModel.DonViDienTich,
                BacTaiSan: searchModel.BacTaiSan,
                stringLoaiTaiSan: searchModel.StringLoaiTaiSan,
                stringLoaiDonVi: searchModel.StringLoaiDonVi,
                NhanXeId: searchModel.NHAN_XE_ID,
                NamSD_CompareSign: searchModel.NamSD_CompareSign,
                NamSD_Value1: searchModel.NamSD_Value1,
                NamSD_Value2: searchModel.NamSD_Value2,
                NamSx_CompareSign: searchModel.NamSx_CompareSign,
                NamSx_Value1: searchModel.NamSx_Value1,
                NamSx_Value2: searchModel.NamSx_Value2,
                SoTang_CompareSign: searchModel.SoTang_CompareSign,
                SoTang_Value1: searchModel.SoTang_Value1,
                SoTang_Value2: searchModel.SoTang_Value2,
                DienTich_CompareSign: searchModel.DienTich_CompareSign,
                DienTich_Value1: searchModel.DienTich_Value1,
                DienTich_Value2: searchModel.DienTich_Value2,
                SoChoNgoi_CompareSign: searchModel.SoChoNgoi_CompareSign,
                SoChoNgoi_Value1: searchModel.SoChoNgoi_Value1,
                SoChoNgoi_Value2: searchModel.SoChoNgoi_Value2,
                TaiTrong_CompareSign: searchModel.TaiTrong_CompareSign,
                TaiTrong_Value1: searchModel.TaiTrong_Value1,
                TaiTrong_Value2: searchModel.TaiTrong_Value2,
                NguyenGiaNS_CompareSign: searchModel.NguyenGiaNS_CompareSign,
                NguyenGiaNS_Value1: searchModel.NguyenGiaNS_Value1,
                NguyenGiaNS_Value2: searchModel.NguyenGiaNS_Value2,
                NguyenGiaKhac_CompareSign: searchModel.NguyenGiaKhac_CompareSign,
                NguyenGiaKhac_Value1: searchModel.NguyenGiaKhac_Value1,
                NguyenGiaKhac_Value2: searchModel.NguyenGiaKhac_Value2,
                NguyenGiaVienTro_CompareSign: searchModel.NguyenGiaVienTro_CompareSign,
                NguyenGiaVienTro_Value1: searchModel.NguyenGiaVienTro_Value1,
                NguyenGiaVienTro_Value2: searchModel.NguyenGiaVienTro_Value2,
                NguyenGiaODA_CompareSign: searchModel.NguyenGiaODA_CompareSign,
                NguyenGiaODA_Value1: searchModel.NguyenGiaODA_Value1,
                NguyenGiaODA_Value2: searchModel.NguyenGiaODA_Value2,
                TongNguyenGia_CompareSign: searchModel.TongNguyenGia_CompareSign,
                TongNguyenGia_Value1: searchModel.TongNguyenGia_Value1,
                TongNguyenGia_Value2: searchModel.TongNguyenGia_Value2,
                TyLeChatLuong_CompareSign: searchModel.TyLeChatLuong_CompareSign,
                TyLeChatLuong_Value1: searchModel.TyLeChatLuong_Value1,
                TyLeChatLuong_Value2: searchModel.TyLeChatLuong_Value2,
                GTCL_CompareSign: searchModel.GTCL_CompareSign,
                GTCL_Value1: searchModel.GTCL_Value2,
                GTCL_Value2: searchModel.DienTich_Value2,
                idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoTraCuuSoLieu });
        }

        #endregion Báo cáo tra cứu

        #region Báo cáo quản lý dự án

        public virtual IActionResult TS_BCDA_01A_DK_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_01A_DK_TSDA;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_01A_DK_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_01A_DK_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_01A_DK_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.GetTS_BCDA_01A_DK_TSDA_KEKHAI_TRU_SO(ngayBaoCao: searchModel.NgayKetThuc, donViId: (int)_workContext.CurrentDonVi.ID, donViDienTich: searchModel.DonViDienTich, donViTien: searchModel.DonViTien);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_01B_DK_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_01B_DK_TSDA;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_01B_DK_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_01B_DK_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_01B_DK_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.GetTS_BCDA_01B_DK_TSDA_KEKHAI_OTO(ngayBaoCao: searchModel.NgayKetThuc, donViId: (int)_workContext.CurrentDonVi.ID, donViDienTich: searchModel.DonViDienTich, donViTien: searchModel.DonViTien);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_01C_DK_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_01C_DK_TSDA;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_01C_DK_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_01C_DK_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_01C_DK_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            //var obj = _baoCaoDuAnService.BCDA_02A_CTDV_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan);
            //model.DataSource = null;
            var data = _baoCaoKeKhaiServices.TS_BCKK_05C_DK_TSDA(ngayBaoCao: searchModel.NgayKetThuc, donViId: (int)_workContext.CurrentDonVi.ID);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_02A_CTDV_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_02A_CTDV_TSDA;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_02A_CTDV_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_02A_CTDV_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_02A_CTDV_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_02A_CTDV_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_02A_THC_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_02A_THC_TSDA;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_02A_THC_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_02A_THC_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_02A_THC_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_02A_THC_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_02B_TS_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(cheDoId: 23, isDisabled: false, listLoaiHinhTaiSanId: new List<decimal>() { (decimal)enumLOAI_HINH_TAI_SAN.DAT, (decimal)enumLOAI_HINH_TAI_SAN.NHA }).ToList();
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_02B_TS_TSDA;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_02B_TS_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_02B_TS_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_02B_TS_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_02B_TS_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_02C_OT_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(cheDoId: 23, isDisabled: false, loaiHinhTaiSanId: (decimal)enumLOAI_HINH_TAI_SAN.OTO).ToList();
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_02C_OT_TSDA;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_02C_OT_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_02C_OT_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_02C_OT_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_02C_OT_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_02D_TSK_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(cheDoId: 23, isDisabled: false, listLoaiHinhTaiSanId: new List<decimal>() { (decimal)enumLOAI_HINH_TAI_SAN.DAC_THU, (decimal)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC, (decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC, (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV, (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI, (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC }).ToList();
            ;

            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_02D_TSK_TSDA;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_02D_TSK_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_02D_TSK_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_02D_TSK_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_02D_TSK_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_02E1_KT_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            //searchModel.DDLLyDoGiam = new enumLyDoGiamBC().ToSelectList(valuesToExclude: new int[] {(int)enumLyDoGiamBC.BiThuHoi}).ToList();
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_02E1_KT_TSDA;
            searchModel.MauSo = 1;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_02E1_KT_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_02E1_KT_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_02E_KT_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_02E_KT_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: searchModel.MauSo, NgayBatDau: searchModel.NgayBatDau, stringLyDo: searchModel.StringLyDoID);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_02E2_KT_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_02E2_KT_TSDA;
            searchModel.MauSo = 2;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_02E2_KT_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_02E2_KT_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_02E_KT_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_02E_KT_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: searchModel.MauSo, NgayBatDau: searchModel.NgayBatDau, stringLyDo: searchModel.StringLyDoID);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_02F3_KT_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_02F3_KT_TSDA;
            searchModel.MauSo = 3;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_02F3_KT_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_02F3_KT_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_02F_KT_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_02F_KT_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: 3, NgayBatDau: searchModel.NgayBatDau);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_02F1_KT_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_02F1_KT_TSDA;
            searchModel.MauSo = 1;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_02F1_KT_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_02F1_KT_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_02F_KT_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_02F_KT_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: 1, NgayBatDau: searchModel.NgayBatDau);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_02F2_KT_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_02F2_KT_TSDA;
            searchModel.MauSo = 2;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_02F2_KT_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_02F2_KT_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_02F_KT_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_02F_KT_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: 2, NgayBatDau: searchModel.NgayBatDau);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_02G_KT_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_02G_KT_TSDA;
            searchModel.MauSo = 2;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_02G_KT_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_02G_KT_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_02G_KT_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_02G_KT_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, NgayBatDau: searchModel.NgayBatDau);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_02I_KT_TSDA(int MauSo)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_02I_KT_TSDA;
            searchModel.MauSo = MauSo;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_02I_KT_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_02I_KT_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_02I_KT_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_02I_KT_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: searchModel.MauSo, NgayBatDau: searchModel.NgayBatDau);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_02K_TS_TSDA(int MauSo)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_02K_TS_TSDA;
            searchModel.MauSo = MauSo;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_02K_TS_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_02K_TS_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new TS_BCDA_02K_TS_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_02K_TS_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: searchModel.MauSo, NgayBatDau: searchModel.NgayBatDau);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_02H_TS_TSDA(int MauSo)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_02H_TS_TSDA;
            searchModel.MauSo = MauSo;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_02H_TS_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_02H_TS_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_02H_TS_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_02H_TS_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: searchModel.MauSo, NgayBatDau: searchModel.NgayBatDau);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_02K1_TS_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_02K1_TS_TSDA;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_02K1_TS_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_02K1_TS_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_02K1_TS_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_02K_TS_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: 3, NgayBatDau: searchModel.NgayBatDau);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_02H1_TS_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_02H1_TS_TSDA;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_02H1_TS_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_02H1_TS_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_02H1_TS_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_02H_TS_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: 3, NgayBatDau: searchModel.NgayBatDau);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_04_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_04_TSDA;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_04_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_04_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_04_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            //var obj = _baoCaoDuAnService.BCDA_02H_TS_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: 3, NgayBatDau: searchModel.NgayBatDau);
            model.DataSource = null;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_04_TSDA_PHULUC01_DINHKEM_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_04_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_04_TSDA_PHU_LUC_01_DINHKEM(searchModel, cauHinhModel, cauHinhChunghModel);
            //var obj = _baoCaoDuAnService.BCDA_02H_TS_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: 3, NgayBatDau: searchModel.NgayBatDau);
            model.DataSource = null;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_04_TSDA_PHULUC02_DINHKEM_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_04_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_04_TSDA_PHU_LUC_02_DINHKEM(searchModel, cauHinhModel, cauHinhChunghModel);
            //var obj = _baoCaoDuAnService.BCDA_02H_TS_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: 3, NgayBatDau: searchModel.NgayBatDau);
            model.DataSource = null;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_05_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_05_TSDA;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_05_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_05_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_05_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            //var obj = _baoCaoDuAnService.BCDA_02H_TS_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: 3, NgayBatDau: searchModel.NgayBatDau);
            model.DataSource = null;
            return ShowViewReport(model);
        }

        #endregion Báo cáo quản lý dự án

        #region Báo cáo cung cấp thông tin tài chính B03

        #region Tổng hợp

        #region hữu hình

        public virtual IActionResult TS_BCTH_B03_CCTT_HUU_HINH()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCungCapThongTinTaiChinh))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.NamBaoCao = DateTime.Now.Year;
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_B03_CCTT_HUUHINH;
            searchModel.DDLBacDonVi = enumCapDonVi.Cap_1.ToSelectList(valuesToExclude: new int[] { (int)enumCapDonVi.Cap_3, (int)enumCapDonVi.Cap_4, (int)enumCapDonVi.Cap_5 }).ToList();
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCTH_B03_CCTT_HUU_HINH_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCTH_B03_CCTT_HUUHINH) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoTongHopTaiSanService.BC_CCTT_B03_HUU_HINH(namBaoCao: searchModel.NamBaoCao, donViId: searchModel.DonVi, donViTien: searchModel.DonViTien, /*CapDonVi: searchModel.CapDonVi,*/BacDonVi: searchModel.BacDonVi);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        #endregion hữu hình

        #region vô hình

        public virtual IActionResult TS_BCTH_TT_CUNGCAP_TAICHINH()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCungCapThongTinTaiChinh))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.NamBaoCao = DateTime.Now.Year;
            searchModel.DDLBacDonVi = enumCapDonVi.Cap_1.ToSelectList(valuesToExclude: new int[] { (int)enumCapDonVi.Cap_3, (int)enumCapDonVi.Cap_4, (int)enumCapDonVi.Cap_5 }).ToList();
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCTH_TT_CUNGCAP_TAICHINH_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCTH_B03_CCTT_VOHINH) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCTH_TT_CUNGCAP_TAICHINH(searchModel, cauHinhModel, cauHinhChunghModel);
            model.DataSource = _baoCaoTongHopTaiSanService.BC_CCTT_B03_VO_HINH(namBaoCao: searchModel.NamBaoCao, donViId: searchModel.DonVi, donViTien: searchModel.DonViTien/*, CapDonVi: searchModel.CapDonVi*/);
            return ShowViewReport(model);
        }

        #endregion vô hình

        #endregion Tổng hợp

        #region chi tiết

        public virtual IActionResult TS_BCCT_B03_CCTT_HUU_HINH()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCungCapThongTinTaiChinh))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.IsChuaCapNhapTien = false;
            searchModel.MaBaoCao = enumMA_BAO_CAO.BCCT_B03_CCTT_HUUHINH;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCT_B03_CCTT_HUU_HINH_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.BCCT_B03_CCTT_HUUHINH) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoTongHopTaiSanService.BC_CCTT_B03_HUU_HINH(namBaoCao: searchModel.NamBaoCao, donViId: _workContext.CurrentDonVi.ID, donViTien: searchModel.DonViTien);
            searchModel.DonVi = _workContext.CurrentDonVi.ID;
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        [HttpPost]
        public virtual IActionResult TSCT_B03_CCTT_HUU_HINH_CHAY_NGAM(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (_queueProcessService.CheckCanCreateThread(_workContext.CurrentCustomer.ID))
            {
                _reportModelFactory.PrePareDonViBaoCao(searchModel);
                //đẩy tham số
                var queue = _queueProcessModelFactory.InsertQueueForSpecificReport(maBaoCao: enumMA_BAO_CAO.BCCT_B03_CCTT_HUUHINH, searchModel: searchModel, FileType: searchModel.FileType, ClassReportFullName: typeof(rptTS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH).FullName, ModelReturnFullName: typeof(TS_BCTH_TT_CUNGCAP_TAICHINH_HUU_HINH).FullName);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo file báo cáo", queue, "QueueProcess");
                _baoCaoTongHopTaiSanService.BC_CCTT_B03_HUU_HINH(namBaoCao: searchModel.NamBaoCao, donViId: _workContext.CurrentDonVi.ID, donViTien: searchModel.DonViTien, idQueueBaoCao: queue.ID);
                SuccessNotification("Thêm vào hàng đợi thành công");
            }
            else
            {
                ErrorNotification("Hệ thống quá tải, xin thử lại sau.");
            }
            return RedirectToAction("TS_BC", "Report", new { LoaiBaoCao = (int)enumLoaiBaoCao.BaoCaoTaiChinhNhaNuoc });
        }

        public virtual IActionResult TS_BCCT_B03_CCTT_VO_HINH()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoCungCapThongTinTaiChinh))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.IsChuaCapNhapTien = false;
            searchModel.MaBaoCao = enumMA_BAO_CAO.BCCT_B03_CCTT_VOHINH;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCCT_B03_CCTT_VO_HINH_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.BCCT_B03_CCTT_VOHINH) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCTH_TT_CUNGCAP_TAICHINH(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoTongHopTaiSanService.BC_CCTT_B03_VO_HINH(namBaoCao: searchModel.NamBaoCao, donViId: _workContext.CurrentDonVi.ID, donViTien: searchModel.DonViTien);
            searchModel.DonVi = _workContext.CurrentDonVi.ID;
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        #endregion chi tiết

        #endregion Báo cáo cung cấp thông tin tài chính B03

        #region In thẻ tài sản

        #region Thẻ TSCĐ
        public virtual IActionResult TS_IN_THE_TSCD_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_IN_THE_TSCD_S25H_TT107;
            searchModel.NgayBaoCao = DateTime.Now;
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_IN_THE_TSCD_S25H_TT107);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            if (donvi != null)
            {
                if (donvi.PARENT_ID != null)
                {
                    var donvicha = _donViService.GetDonViById((decimal)donvi.PARENT_ID);
                    searchModel.TEN_DON_VI_CAP_TREN = donvicha != null ? donvicha.TEN : _workContext.CurrentDonVi.TEN_DON_VI;
                }
                else
                {
                    searchModel.TEN_DON_VI_CAP_TREN = _workContext.CurrentDonVi.TEN_DON_VI;
                }
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTHE_TAI_SAN_CO_DINH_TT107(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _inTheTaiSanServices.TS_THE_TAI_SAN_CO_DINH(ngayBaoCao: DateTime.Now, taiSanId: searchModel.TaiSanId);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_IN_LIST_THE_TSCD_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_IN_THE_TSCD_S25H_TT107;
            searchModel.NgayBaoCao = DateTime.Now;
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_IN_THE_TSCD_S25H_TT107);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            if (donvi != null)
            {
                if (donvi.PARENT_ID != null)
                {
                    var donvicha = _donViService.GetDonViById((decimal)donvi.PARENT_ID);
                    searchModel.TEN_DON_VI_CAP_TREN = donvicha != null ? donvicha.TEN : _workContext.CurrentDonVi.TEN_DON_VI;
                }
                else
                {
                    searchModel.TEN_DON_VI_CAP_TREN = _workContext.CurrentDonVi.TEN_DON_VI;
                }
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptLIST_THE_TAI_SAN_CO_DINH_TT107(searchModel, cauHinhModel, cauHinhChunghModel);



            var listTTS = new List<TS_LIST_THE_TAI_SAN_CO_DINH>();
            var listObj = _inTheTaiSanServices.TS_LIST_THE_TAI_SAN_CO_DINH(ngayBaoCao: DateTime.Now, listTaiSanId: searchModel.ListTaiSanId);
            var listId = listObj.Select(c => c.TAI_SAN_ID).Distinct();
            foreach (var taiSanId in listId)
            {
                var subReport = new rptTHE_TAI_SAN_CO_DINH_TT107(searchModel, cauHinhModel, cauHinhChunghModel);
                var obj = listObj.Where(c => c.TAI_SAN_ID == taiSanId).ToList();
                subReport.DataSource = obj;
                var report = new TS_LIST_THE_TAI_SAN_CO_DINH();
                report.TAI_SAN_ID = taiSanId ?? 0;
                report.THE_TAI_SAN_CO_DINH_TT107 = subReport;
                listTTS.Add(report);
            }

            model.DataSource = listTTS;
            return ShowViewReport(model);

        }
        #endregion

        #region Thẻ TSCĐ TT133
        public virtual IActionResult TS_IN_LIST_THE_TSCD_TT133_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_IN_THE_TSCD_S11_DNN_TT133;
            searchModel.NgayBaoCao = DateTime.Now;
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_IN_THE_TSCD_S11_DNN_TT133);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            if (donvi != null)
            {
                if (donvi.PARENT_ID != null)
                {
                    var donvicha = _donViService.GetDonViById((decimal)donvi.PARENT_ID);
                    searchModel.TEN_DON_VI_CAP_TREN = donvicha != null ? donvicha.TEN : _workContext.CurrentDonVi.TEN_DON_VI;
                }
                else
                {
                    searchModel.TEN_DON_VI_CAP_TREN = _workContext.CurrentDonVi.TEN_DON_VI;
                }
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptLIST_THE_TAI_SAN_CO_DINH_TT133();
            var listTTS = new List<TS_LIST_THE_TAI_SAN_CO_DINH_TT133>();
            var listObj = _inTheTaiSanServices.TS_LIST_THE_TAI_SAN_CO_DINH_TT133(ngayBaoCao: DateTime.Now, listTaiSanId: searchModel.ListTaiSanId);
            var listId = listObj.Select(c => c.TAI_SAN_ID).Distinct();
            foreach (var taiSanId in listId)
            {
                var subReport = new rptTHE_TAI_SAN_CO_DINH_TT133(searchModel, cauHinhModel, cauHinhChunghModel);
                var obj = listObj.Where(c => c.TAI_SAN_ID == taiSanId).ToList();
                subReport.DataSource = obj;
                var report = new TS_LIST_THE_TAI_SAN_CO_DINH_TT133();
                report.TAI_SAN_ID = taiSanId ?? 0;
                report.TS_THE_TAI_SAN_CO_DINH_TT133 = subReport;
                listTTS.Add(report);
            }

            model.DataSource = listTTS;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_IN_THE_TSCD_TT133_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_IN_THE_TSCD_S11_DNN_TT133;
            searchModel.NgayBaoCao = DateTime.Now;
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_IN_THE_TSCD_S11_DNN_TT133);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            if (donvi != null)
            {
                if (donvi.PARENT_ID != null)
                {
                    var donvicha = _donViService.GetDonViById((decimal)donvi.PARENT_ID);
                    searchModel.TEN_DON_VI_CAP_TREN = donvicha != null ? donvicha.TEN : _workContext.CurrentDonVi.TEN_DON_VI;
                }
                else
                {
                    searchModel.TEN_DON_VI_CAP_TREN = _workContext.CurrentDonVi.TEN_DON_VI;
                }
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTHE_TAI_SAN_CO_DINH_TT133(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _inTheTaiSanServices.TS_THE_TAI_SAN_CO_DINH_TT133(ngayBaoCao: DateTime.Now, taiSanId: searchModel.TaiSanId);
            model.DataSource = obj;
            return ShowViewReport(model);

        }
        #endregion

        #region Thẻ KK tài sản

        public virtual IActionResult TS_IN_THE_KIEM_KE_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_IN_THE_KIEM_KE_TAI_SAN;
            searchModel.NgayBaoCao = DateTime.Now;
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_IN_THE_KIEM_KE_TAI_SAN);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            if (donvi != null)
            {
                if (donvi.PARENT_ID != null)
                {
                    var donvicha = _donViService.GetDonViById((decimal)donvi.PARENT_ID);
                    searchModel.TEN_DON_VI_CAP_TREN = donvicha != null ? donvicha.TEN : _workContext.CurrentDonVi.TEN_DON_VI;
                }
                else
                {
                    searchModel.TEN_DON_VI_CAP_TREN = _workContext.CurrentDonVi.TEN_DON_VI;
                }
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTHE_KIEM_KE_TSCD(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _inTheTaiSanServices.TS_THE_KIEM_KE(taiSanId: searchModel.TaiSanId);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_IN_LIST_THE_KIEM_KE_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_IN_THE_KIEM_KE_TAI_SAN;
            searchModel.NgayBaoCao = DateTime.Now;
            var cauHinhModel = _reportModelFactory.PrePareCauHinhBaoCaoModel(new CauHinhBaoCaoModel(), enumMA_BAO_CAO.TS_IN_THE_KIEM_KE_TAI_SAN);
            var cauHinhChunghModel = _reportModelFactory.PrePareCauHinhBaoCaoChungModel(new CauHinhBaoCaoChungModel());
            var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            if (donvi != null)
            {
                if (donvi.PARENT_ID != null)
                {
                    var donvicha = _donViService.GetDonViById((decimal)donvi.PARENT_ID);
                    searchModel.TEN_DON_VI_CAP_TREN = donvicha != null ? donvicha.TEN : _workContext.CurrentDonVi.TEN_DON_VI;
                }
                else
                {
                    searchModel.TEN_DON_VI_CAP_TREN = _workContext.CurrentDonVi.TEN_DON_VI;
                }
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptLIST_THE_KIEM_KE_TSCD();
            var listTTS = new List<TS_LIST_THE_KIEM_KE_TSCD>();
            var listObj = _inTheTaiSanServices.TS_LIST_THE_KIEM_KE(listTaiSanId: searchModel.ListTaiSanId);
            var listId = listObj.Select(c => c.TAI_SAN_ID).Distinct();
            foreach (var taiSanId in listId)
            {
                var subReport = new rptTHE_KIEM_KE_TSCD(searchModel, cauHinhModel, cauHinhChunghModel);
                var obj = listObj.Where(c => c.TAI_SAN_ID == taiSanId).ToList();
                subReport.DataSource = obj;
                var report = new TS_LIST_THE_KIEM_KE_TSCD();
                report.TAI_SAN_ID = taiSanId ?? 0;
                report.THE_KIEM_KE_TSCD = subReport;
                listTTS.Add(report);
            }

            model.DataSource = listTTS;
            return ShowViewReport(model);
        }

        #endregion





        #endregion In thẻ tài sản

        #region Phiếu xác nhận thông tin tài sản

        public virtual IActionResult TS_PXNTT_MAU_01()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_PXNTT_MAU_01;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_PXNTT_MAU_01_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_PXNTT_MAU_01) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_PXNTT_MAU_01(searchModel, cauHinhModel, cauHinhChunghModel);
            model.DataSource = _taiSanBCCTService.PXNTT_MAU_01(DonViId: searchModel.DonVi, NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc);
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_PXNTT_MAU_02()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_PXNTT_MAU_02;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_PXNTT_MAU_02_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_PXNTT_MAU_02) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_PXNTT_MAU_02();
            model.DataSource = null;/*_taiSanBCCTService.PXNTT_MAU_01(DonViId: searchModel.DonVi, NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc);*/
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_PXNTT_MAU_03()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoChiTietTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_PXNTT_MAU_03;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_PXNTT_MAU_03_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_PXNTT_MAU_03) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_PXNTT_MAU_03();
            //model.DataSource = _taiSanBCCTService.PXNTT_MAU_01(DonViId: searchModel.DonVi, NgayBatDau: searchModel.NgayBatDau, NgayKetThuc: searchModel.NgayKetThuc);
            return ShowViewReport(model);
        }

        #endregion Phiếu xác nhận thông tin tài sản

        #region Ban quản lý dự án

        public virtual IActionResult TS_BCDA_01BC_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanBanQLDuAnSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_01BC_TSDA;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_01BC_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_01BC_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_01BC_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_01BC_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: 3, NgayBatDau: searchModel.NgayBatDau, DuAnId: (int)searchModel.DuAnId);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_02BC_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanBanQLDuAnSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_02BC_TSDA;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_02BC_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_02BC_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_02BC_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_02BC_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: 3, NgayBatDau: searchModel.NgayBatDau, DuAnId: (int)searchModel.DuAnId);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_03BC_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanBanQLDuAnSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_03BC_TSDA;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_03BC_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_03BC_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_03BC_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_03BC_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: 3, NgayBatDau: searchModel.NgayBatDau, DuAnId: (int)searchModel.DuAnId);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_04BC_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanBanQLDuAnSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_04BC_HTSD_KHAC;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_04BC_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_04BC_HTSD_KHAC) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_04BC_HTSD_KHAC(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_04BC_HTSD_KHAC(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: 3, NgayBatDau: searchModel.NgayBatDau, DuAnId: (int)searchModel.DuAnId);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_05BC_TSDA()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDuAn))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanBanQLDuAnSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_05BC_TANG_GIAM_TSDA;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_05BC_TSDA_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_05BC_TANG_GIAM_TSDA) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_05BC_TANG_GIAM_TSDA(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDuAnService.BCDA_05BC_TANG_GIAM_TSDA(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, MauSo: 3, NgayBatDau: searchModel.NgayBatDau, DuAnId: (int)searchModel.DuAnId);
            model.DataSource = obj;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_04A_TRUSODENGHIXULY()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanBanQLDuAnSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_04A_TRUSODENGHIXULY;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_04A_TRUSODENGHIXULY_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_04A_TRUSODENGHIXULY) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_04A_TRUSODENGHIXULY(searchModel, cauHinhModel, cauHinhChunghModel);
            ////lấy dữ liệu báo cáo(searchModel, cauHinhModel, cauHinhChunghModel);
            var data = _baoCaoDuAnService.GetTS_BCDA_04A_TRUSODENGHIXULY(ngayBaoCao: searchModel.NgayKetThuc, donViId: (int)_workContext.CurrentDonVi.ID, donViDienTich: searchModel.DonViDienTich, donViTien: searchModel.DonViTien, DuAnId: searchModel.DuAnId);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_04B_OTODENGHIXULY()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanBanQLDuAnSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_04B_OTODENGHIXULY;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_04B_OTODENGHIXULY_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_04B_OTODENGHIXULY) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_04B_OTODENGHIXULY(searchModel, cauHinhModel, cauHinhChunghModel);
            ////lấy dữ liệu báo cáo
            var data = _baoCaoDuAnService.GetTS_BCDA_04B_OTODENGHIXULY(ngayBaoCao: searchModel.NgayKetThuc, donViId: (int)_workContext.CurrentDonVi.ID, donViDienTich: searchModel.DonViDienTich, donViTien: searchModel.DonViTien, DuAnId: searchModel.DuAnId);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult TS_BCDA_04C_KHACDENGHIXULY()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoQuocHoiTaiSan))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanBanQLDuAnSearchModel(searchModel);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCDA_04C_KHACDENGHIXULY;
            return PartialView(searchModel);
        }

        public virtual IActionResult TS_BCDA_04C_KHACDENGHIXULY_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCDA_04C_KHACDENGHIXULY) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_BCDA_04C_KHACDENGHIXULY(searchModel, cauHinhModel, cauHinhChunghModel);
            ////lấy dữ liệu báo cáo
            var data = _baoCaoDuAnService.GetTS_BCDA_04C_KHACDENGHIXULY(ngayBaoCao: searchModel.NgayKetThuc, donViId: (int)_workContext.CurrentDonVi.ID, donViDienTich: searchModel.DonViDienTich, donViTien: searchModel.DonViTien, DuAnId: searchModel.DuAnId);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        #endregion Ban quản lý dự án

        #region Đối chiếu chuyển đổi dữ liệu

        public virtual IActionResult BC_DOICHIEU_DATA_TS_BCCT_01A_DK_TSNN()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDoiChieuDuLieu))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCCT_1A_DK_TSNN_DV_SD;
            //var listNguonExclude = new[]
            //{
            //    (int)enumNguonTaiSan.KHAC, (int)enumNguonTaiSan.QLTSC,(int)enumNguonTaiSan.TAT_CA
            //};
            //searchModel.ddlNguonTaiSan = (new enumNguonTaiSan()).ToSelectList(valuesToExclude: listNguonExclude).ToList();
            var listNguonExclude = new List<decimal>() { (decimal)enumNguonTaiSan.KHAC, (decimal)enumNguonTaiSan.QLTSC, (decimal)enumNguonTaiSan.TAT_CA };
            List<SelectListItem> lstnguonTaiSan = _nguonTaiSanService.GetAllNguonTaiSanActive().Where(x => !listNguonExclude.Contains(x.ID)).Select(x => new SelectListItem()
            {
                Value = x.ID.ToString(),
                Text = x.TEN
            }).ToList();
            searchModel.ddlNguonTaiSan = lstnguonTaiSan;
            return PartialView(searchModel);
        }

        public virtual IActionResult BC_DOICHIEU_DATA_TS_BCCT_01A_DK_TSNN_(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel();
            var cauHinhChunghModel = new CauHinhBaoCaoChungModel();
            if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == enumMA_BAO_CAO.TS_BCCT_1A_DK_TSNN_DV_SD) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCaoCT(searchModel);
            XtraReport model = new rptTS_BCCT_1A(searchModel, cauHinhModel, cauHinhChunghModel);
            var data = _baoCaoDoiChieuDuLieuService.BaoCaoTSCT_1A(DonViId: searchModel.DonVi > 0 ? (int)searchModel.DonVi : (int)_workContext.CurrentDonVi.ID, NgayKetThuc: searchModel.NgayKetThuc, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), donViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, BacTaiSan: searchModel.BacTaiSan, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, NguonTaiSanId: searchModel.NguonTaiSanId);
            model.DataSource = data;
            return ShowViewReport(model);
        }

        public virtual IActionResult BC_DOICHIEU_DATA_02A_DK_TSNN(int MauSo = 1)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDoiChieuDuLieu))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MauSo = MauSo;
            //searchModel.DDLCapDonVi = new enumTinhHuyenXaTrungUong().ToSelectList().Select(c => new SelectListItem { Value = c.Value, Text = c.Text, Selected = false }).ToList();
            //searchModel.DDLCapDonVi.Insert(0, new SelectListItem { Value = "-1", Text = "Tất cả", Selected = true });
            //searchModel.MaBaoCao = enumMA_BAO_CAO.TS_BCTH_02A_DK_TSNN;
            switch (MauSo)
            {
                case 1:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_DOICHIEU_DATA_02A_DK_TSNN_p1;
                    break;

                case 2:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_DOICHIEU_DATA_02A_DK_TSNN_p2;
                    break;

                case 3:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_DOICHIEU_DATA_02A_DK_TSNN_p3;
                    break;
            }
            //var listNguonExclude = new[]
            //{
            //	(int)enumNguonTaiSan.KHAC, (int)enumNguonTaiSan.QLTSC
            //};
            //searchModel.ddlNguonTaiSan = (new enumNguonTaiSan()).ToSelectList(valuesToExclude: listNguonExclude).ToList();

            var listNguonExclude = new List<decimal>() { (decimal)enumNguonTaiSan.KHAC, (decimal)enumNguonTaiSan.QLTSC, (decimal)enumNguonTaiSan.TAT_CA };
            List<SelectListItem> lstnguonTaiSan = _nguonTaiSanService.GetAllNguonTaiSanActive().Where(x => !listNguonExclude.Contains(x.ID)).Select(x => new SelectListItem()
            {
                Value = x.ID.ToString(),
                Text = x.TEN
            }).ToList();
            searchModel.ddlNguonTaiSan = lstnguonTaiSan;

            return PartialView(searchModel);
        }

        public virtual IActionResult BC_DOICHIEU_DATA_02A_DK_TSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var maBaoCao = "";
            switch (searchModel.MauSo)
            {
                case 1:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_DOICHIEU_DATA_02A_DK_TSNN_p1;
                    break;

                case 2:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_DOICHIEU_DATA_02A_DK_TSNN_p2;
                    break;

                case 3:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_DOICHIEU_DATA_02A_DK_TSNN_p3;
                    break;
            }
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == maBaoCao) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_DOICHIEU_DATA_02A_DK_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDoiChieuDuLieuService.BaoCaoDoiChieuData_02A(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayKetThuc: searchModel.NgayKetThuc, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, NguonTaiSanId: searchModel.NguonTaiSanId, BacDonVi: searchModel.BacDonVi, isHideDetail: searchModel.IsHideDetail);
            model.DataSource = obj;

            if (searchModel.IsExportExcel)
            {
                return Export_Excel(model, searchModel, maBaoCao);

            }
            else
            {
                return ShowViewReport(model);
            }

        }

        /// <summary>
        /// HungNT Hàm xuất excel
        /// </summary>
        /// <param name="report">Báo cáo</param>
        /// <param name="maBaoCao">Mã báo cáo để đặt tên</param>
        /// <returns></returns>
        public virtual IActionResult Export_Excel(XtraReport report, BaoCaoTaiSanTongHopSearchModel searchModel, string maBaoCao)
        {
            var strTitok = DateTime.Now.ToString("yyyyMMdd");
            var nameFile = $"{maBaoCao}_{searchModel.NamBaoCao}_{strTitok}";
            var fileExport = _queueProcessModelFactory.ExportFileBaoCao(report, maBaoCao, null, enumEXPORT_FILE_TYPE.EXCEL_XLSX);
            return new FileContentResult(fileExport, MimeTypes.TextXls)
            {
                FileDownloadName = nameFile + ".xlsx"
            };
        }

        public virtual IActionResult Export_Excel_CT(XtraReport reportct, BaoCaoTaiSanChiTietSearchModel searchModel, string maBaoCao)
        {
            var strTitok = DateTime.Now.ToString("yyyyMMdd");
            var nameFile = $"{maBaoCao}_{searchModel.NamBaoCao}_{strTitok}";
            var fileExport = _queueProcessModelFactory.ExportFileBaoCao(reportct, maBaoCao, null, enumEXPORT_FILE_TYPE.EXCEL_XLSX);
            return new FileContentResult(fileExport, MimeTypes.TextXls)
            {
                FileDownloadName = nameFile + ".xlsx"
            };
        }
        // get file đối chiếu có sẵn
        public virtual IActionResult GetFileBaoCaoDoiChieu(int MauSo = 1)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDoiChieuDuLieu))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            searchModel.MauSo = MauSo;
            searchModel.NamBaoCao = 2020;
            //var listNguonExclude = new[]
            //{
            //    (int)enumHeThong.CTNS,(int)enumHeThong.HTDB,(int)enumHeThong.KHO
            //};
            //searchModel.ddlHeThong = (new enumHeThong()).ToSelectList(valuesToExclude: listNguonExclude).ToList();
            var listNguonExclude = new List<decimal>() { (decimal)enumNguonTaiSan.KHAC, (decimal)enumNguonTaiSan.QLTSC, (decimal)enumNguonTaiSan.TAT_CA };
            List<SelectListItem> lstnguonTaiSan = _nguonTaiSanService.GetAllNguonTaiSanActive().Where(x => !listNguonExclude.Contains(x.ID)).Select(x => new SelectListItem()
            {
                Value = x.ID.ToString(),
                Text = x.TEN
            }).ToList();
            searchModel.ddlNguonTaiSan = lstnguonTaiSan;
            searchModel.ddlHeThong = lstnguonTaiSan;
            return PartialView(searchModel);
        }
        [HttpPost]
        public virtual IActionResult GetFileBaoCaoDoiChieu(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            if (searchModel.MauSo == 1)
            {
                searchModel.BAO_CAO_ID = 0;
            }
            else if (searchModel.MauSo == 2)
            {
                searchModel.BAO_CAO_ID = 1;
            }
            else searchModel.BAO_CAO_ID = 2;
            //if (searchModel.NguonTaiSanId == 0)
            //{
            //	searchModel.HE_THONG_ID = 0;
            //}
            //else searchModel.HE_THONG_ID = 1;
            //if (searchModel.NAM_BAO_CAO_DC == 0)
            //{
            //    searchModel.NamBaoCao = 2018;
            //}
            //else if (searchModel.NAM_BAO_CAO_DC == 1)
            //{
            //    searchModel.NamBaoCao = 2019;
            //}
            //else 
            //    searchModel.NamBaoCao = 2020;
            searchModel.NamBaoCao = searchModel.NAM_BAO_CAO_DC.GetValueOrDefault(0);
            var item = _baoCaoDoiChieuService.GetBaoCaoDoiChieuBySeachModel(searchModel.DonVi, searchModel.BAO_CAO_ID, searchModel.HE_THONG_ID, searchModel.NamBaoCao);
            if (item == null)
            {
                return Content("Không có dữ liệu");
            }
            else
            {
                var fileDoiChieu = _fileCongViecService.GetFileCongViecById(item.FILE_ID);
                var fileExport = DownloadFile(fileDoiChieu.GUID);
                return fileExport;
            }
        }
        public virtual IActionResult GetFileBaoCaoDoiChieu1A()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDoiChieuDuLieu))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanChiTietSearchModel(searchModel);
            searchModel.NamBaoCao = 2020;
            //var listNguonExclude = new[]
            //{
            //    (int)enumHeThong.CTNS,(int)enumHeThong.HTDB
            //};
            //searchModel.DDLHeThong = (new enumHeThong()).ToSelectList(valuesToExclude: listNguonExclude).ToList();
            var listNguonExclude = new List<decimal>() { (decimal)enumNguonTaiSan.KHAC, (decimal)enumNguonTaiSan.QLTSC, (decimal)enumNguonTaiSan.TAT_CA };
            List<SelectListItem> lstnguonTaiSan = _nguonTaiSanService.GetAllNguonTaiSanActive().Where(x => !listNguonExclude.Contains(x.ID)).Select(x => new SelectListItem()
            {
                Value = x.ID.ToString(),
                Text = x.TEN
            }).ToList();
            searchModel.ddlNguonTaiSan = lstnguonTaiSan;
            searchModel.DDLHeThong = lstnguonTaiSan;
            return PartialView(searchModel);
        }
        [HttpPost]
        public virtual IActionResult GetFileBaoCaoDoiChieu1A(BaoCaoTaiSanChiTietSearchModel searchModel)
        {
            //if (searchModel.NAM_BAO_CAO_DC == 0)
            //{
            //    searchModel.NamBaoCao = 2018;
            //}
            //else if (searchModel.NAM_BAO_CAO_DC == 1)
            //{
            //    searchModel.NamBaoCao = 2019;
            //}
            //else searchModel.NamBaoCao = 2020;
            searchModel.NamBaoCao = searchModel.NAM_BAO_CAO_DC;
            var item = _baoCaoDoiChieuService.GetBaoCaoDoiChieu1ABySeachModel(searchModel.DonVi, searchModel.HE_THONG_ID, searchModel.NamBaoCao);
            if (item == null)
            {
                return Content("Không có dữ liệu");
            }
            else
            {
                var fileDoiChieu = _fileCongViecService.GetFileCongViecById(item.FILE_ID);
                var fileExport = DownloadFile(fileDoiChieu.GUID);
                return fileExport;
            }
        }
        byte[] GetWorkFileContentOnDisk(FileCongViec item)
        {
            var _fileStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), item.NGAY_TAO.ToPathFolderStore(), item.GUID.ToString() + item.DUOI_FILE);
            return _fileProvider.ReadAllBytes(_fileStore);
        }
        public virtual IActionResult DownloadFile(Guid downloadGuid)
        {
            var download = _fileCongViecService.GetFileByGuid(downloadGuid);
            if (download == null)
                return Content("No download record found with the specified id");
            if (download.NOI_DUNG_FILE == null)
                download.NOI_DUNG_FILE = GetWorkFileContentOnDisk(download);
            //use stored data
            if (download.NOI_DUNG_FILE == null || download.NOI_DUNG_FILE.Length < 2)
                return Content($"Download data is not available any more. Download GD={download.ID}");

            var fileName = !string.IsNullOrWhiteSpace(download.TEN_FILE) ? download.TEN_FILE : download.ID.ToString();
            var contentType = !string.IsNullOrWhiteSpace(download.LOAI_FILE)
                ? download.LOAI_FILE
                : MimeTypes.ApplicationOctetStream;
            return new FileContentResult(download.NOI_DUNG_FILE, contentType)
            {
                FileDownloadName = fileName
            };
        }
        public virtual IActionResult BC_DOICHIEU_DATA_02B_DK_TSNN(int MauSo = 1)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoDoiChieuDuLieu))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanTongHopSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTongHopSearchModel(searchModel);
            var listEx = (new enumLOAI_HINH_TAI_SAN().ToSelectList(valuesToExclude: new int[] { (int)enumLOAI_HINH_TAI_SAN.DAT, (int)enumLOAI_HINH_TAI_SAN.NHA }).Select(c => c.Value.ToNumberInt32())).ToArray();
            searchModel.LoaiHinhTaiSanAvaliable = new enumLOAI_HINH_TAI_SAN().ToSelectList(valuesToExclude: listEx);
            searchModel.MauSo = MauSo;
            switch (MauSo)
            {
                case 1:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_DOICHIEU_DATA_02B_DK_TSNN_p1;
                    break;

                case 2:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_DOICHIEU_DATA_02B_DK_TSNN_p2;
                    break;

                case 3:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_DOICHIEU_DATA_02B_DK_TSNN_p3;
                    break;
            }
            //var listNguonExclude = new[]
            //{
            //    (int)enumNguonTaiSan.KHAC, (int)enumNguonTaiSan.QLTSC,(int)enumNguonTaiSan.TAT_CA
            //};
            //searchModel.ddlNguonTaiSan = (new enumNguonTaiSan()).ToSelectList(valuesToExclude: listNguonExclude).ToList();
            var listNguonExclude = new List<decimal>() { (decimal)enumNguonTaiSan.KHAC, (decimal)enumNguonTaiSan.QLTSC, (decimal)enumNguonTaiSan.TAT_CA };
            List<SelectListItem> lstnguonTaiSan = _nguonTaiSanService.GetAllNguonTaiSanActive().Where(x => !listNguonExclude.Contains(x.ID)).Select(x => new SelectListItem()
            {
                Value = x.ID.ToString(),
                Text = x.TEN
            }).ToList();
            searchModel.ddlNguonTaiSan = lstnguonTaiSan;
            return PartialView(searchModel);
        }

        public virtual IActionResult BC_DOICHIEU_DATA_02B_DK_TSNN_(BaoCaoTaiSanTongHopSearchModel searchModel)
        {
            var maBaoCao = "";
            switch (searchModel.MauSo)
            {
                case 1:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_DOICHIEU_DATA_02B_DK_TSNN_p1;
                    break;

                case 2:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_DOICHIEU_DATA_02B_DK_TSNN_p2;
                    break;

                case 3:
                    searchModel.MaBaoCao = enumMA_BAO_CAO.TS_DOICHIEU_DATA_02B_DK_TSNN_p3;
                    break;
            }
            var cauHinhChung = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0); var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
            var cauHinhModel = new CauHinhBaoCaoModel(); var cauHinhChunghModel = new CauHinhBaoCaoChungModel(); if (cauHinhChung.BaoCao != null) { cauHinhChunghModel = cauHinhChung.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault(); }
            if (cauHinh.BaoCao != null)
            {
                cauHinhModel = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().FirstOrDefault(c => c.MaBaoCao == maBaoCao) ?? new CauHinhBaoCaoModel();
            }
            _reportModelFactory.PrePareDonViBaoCao(searchModel);
            XtraReport model = new rptTS_DOICHIEU_DATA_02B_DK_TSNN(searchModel, cauHinhModel, cauHinhChunghModel);
            var obj = _baoCaoDoiChieuDuLieuService.BaoCaoDoiChieuData_02B(stringDonVi: searchModel.StringDonVi != null ? searchModel.StringDonVi : _workContext.CurrentDonVi.ID.ToString(), NgayKetThuc: searchModel.NgayKetThuc, ListLoaiTaiSanId: searchModel.ListLoaiTaiSanId.ToList(), DonViTien: searchModel.DonViTien, DonViDienTich: searchModel.DonViDienTich, MauSo: searchModel.MauSo, StringCapHanhChinh: searchModel.StringCapHanhChinh, stringLoaiTaiSan: searchModel.StringLoaiTaiSan, stringLoaiDonVi: searchModel.StringLoaiDonVi, BacDonVi: searchModel.BacDonVi, NguonTaiSanId: searchModel.NguonTaiSanId);
            model.DataSource = obj;
            return ShowViewReport(model);
        }
        #endregion Đối chiếu chuyển đổi dữ liệu

        #region Công Cụ Kiểm Tra

        public virtual IActionResult CCKTTaiSanSoLieuDaNhap()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBaoCaoTraCuuSoLieu))
                return AccessDeniedView();
            var searchModel = new BaoCaoTaiSanChiTietSearchModel();
            searchModel = _reportModelFactory.PrepareBaoCaoTaiSanTraCuuSearchModel(searchModel, true);
            searchModel.MaBaoCao = enumMA_BAO_CAO.CCKTTaiSanSoLieuDaNhap_PL02;
            return PartialView(searchModel);
        }

        #endregion Công Cụ Kiểm Tra
    }
}