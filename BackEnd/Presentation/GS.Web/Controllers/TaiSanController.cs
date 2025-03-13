//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Data;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.KT;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Core.Infrastructure;
using GS.Services;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.HeThong;
using GS.Services.KT;
using GS.Services.NghiepVu;
using GS.Services.Security;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.BaoCaos;
using GS.Web.Factories.BienDongs;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.DB;
using GS.Web.Factories.HeThong;
using GS.Web.Factories.KeToan;
using GS.Web.Factories.NghiepVu;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Extensions;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.BienDongs;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.HeThong;
using GS.Web.Models.ImportTaiSan;
using GS.Web.Models.KeToan;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class TaiSanController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly ITaiSanModelFactory _itemModelFactory;
        private readonly ITaiSanService _itemService;
        private readonly ITaiSanDatModelFactory _taisandatModelFactory;
        private readonly ITaiSanDatService _taisandatService;
        private readonly IDiaBanModelFactory _diabanModelFactory;
        private readonly ILoaiTaiSanModelFactory _loaitaisanModelFactory;
        private readonly ILoaiTaiSanService _loaitaisanService;
        private readonly ICheDoHaoMonService _chedohaomonService;
        private readonly ILyDoBienDongService _lydobiendongService;
        private readonly ILyDoBienDongModelFactory _lydobiendongModelFactory;
        private readonly ITaiSanNguonVonService _taisannguonvonService;
        private readonly INguonVonService _nguonvonService;
        private readonly INguonVonModelFactory _nguonvonModelFactory;
        private readonly IHienTrangModelFactory _hienTrangModelFactory;
        private readonly ITaiSanMayMocModelFactory _taiSanMayMocModelFactory;
        private readonly ITaiSanNhaModelFactory _taiSanNhaModelFactory;
        private readonly ITaiSanOtoModelFactory _taiSanOtoModelFactory;
        private readonly IYeuCauChiTietModelFactory _yeuCauChiTietModelFactory;
        private readonly IYeuCauModelFactory _yeucauModelFactory;
        private readonly IYeuCauService _yeucauService;
        private readonly ITaiSanVktModelFactory _taiSanVktModelFactory;
        private readonly ITaiSanClnModelFactory _taiSanClnModelFactory;
        private readonly ITaiSanNhaService _taisannhaService;
        private readonly ITaiSanOtoService _taisanOtoService;
        private readonly ITaiSanClnService _taisanClnService;
        private readonly ITaiSanMayMocService _taisanmaymocService;
        private readonly ITaiSanVktService _taisanVKTService;
        private readonly ITaiSanVoHinhService _taiSanVoHinhService;
        private readonly IYeuCauChiTietService _yeucauchitietService;
        private readonly IHienTrangService _hientrangService;
        private readonly IBienDongService _biendongService;
        private readonly IBienDongChiTietService _biendongchitietService;
        private readonly IDonViService _donviService;
        private readonly IYeuCauNhatKyModelFactory _yeucaunhatkyModelFactory;
        private readonly IYeuCauNhatKyService _yeucaunhatkyService;
        private readonly IBienDongModelFactory _bienDongModelFactory;
        private readonly IBienDongChiTietModelFactory _bienDongChiTietModelFactory;
        private readonly ITaiSanVoHinhModelFactory _taiSanVoHinhModelFactory;
        private readonly ILoaiTaiSanVoHinhModelFactory _loaiTaiSanVoHinhModelFactory;
        private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViServices;
        private readonly ITrungGianBDYCService _trungGianBDYCService;
        private readonly ITrungGianBDYCModelFactory _trungGianBDYCModelFactory;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IThaoTacBienDongModelFactory _thaoTacBienDongModelFactory;
        private readonly ICauHinhService _cauHinhService;
        private readonly ITaiSanNhatKyService _taiSanNhatKyService;
        private readonly ILoaiTaiSanKhauHaoService _loaiTaiSanKhauHaoService;
        private readonly IChucDanhService _chucDanhService;
        private readonly IDonViModelFactory _donViModelFactory;
        private readonly ICapNhatTaiSanModelFactory _capNhatTaiSanModelFactory;
        private readonly IReportModelFactory _reportModelFactory;
        private readonly ILoaiDonViService _loaiDonViService;
        private readonly IGSFileProvider _fileProvider;
        private readonly ITaiSanImportService _taiSanImportService;
        private readonly ITaiSanImportModelFactory _taiSanImportModelFactory;
        private readonly ITaiSanHienTrangSuDungModelFactory _taiSanHienTrangSuDungModelFactory;
        private readonly IHinhThucMuaSamService _hinhThucMuaSamService;
        private readonly IQuocGiaService _quocGiaService;
        private readonly ITaiSanKhauHaoService _taiSanKhauHaoService;
        #region KT
        private readonly IHaoMonTaiSanModelFactory _haoMonTaiSanModelFactory;
        private readonly IHaoMonTaiSanService _haoMonTaiSanService;
        private readonly IKhauHaoTaiSanModelFactory _khauHaoTaiSanModelFactory;

        private readonly IDB_QueueProcessModelFactory _dB_QueueProcessModelFactory;

        private readonly IChucDanhModelFactory _chucDanhModelFactory;
        private readonly IDinhMucModelFactory _dinhMucModelFactory;
        #endregion KT
        #endregion Fields

        #region Ctor

        public TaiSanController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ITaiSanModelFactory itemModelFactory,
            ITaiSanService itemService,
            ITaiSanDatModelFactory taisandatModelFactory,
            ITaiSanDatService taisandatService,
            IDiaBanModelFactory diabanModelFactory,
            ILoaiTaiSanModelFactory loaitaisanModelFactory,
            ICheDoHaoMonService chedohaomonService,
            ILyDoBienDongService lydobiendongService,
            ILoaiTaiSanService loaitaisanService,
            ILyDoBienDongModelFactory lydobiendongModelFactory,
            ITaiSanNguonVonService taisannguonvonService,
            INguonVonService nguonvonService,
            INguonVonModelFactory nguonvonModelFactory,
            IHienTrangModelFactory hienTrangModelFactory,
            ITaiSanMayMocModelFactory taiSanMayMocModelFactory,
            ITaiSanOtoModelFactory taiSanOtoModelFactory,
            IYeuCauChiTietModelFactory yeuCauChiTietModelFactory,
            ITaiSanNhaModelFactory taiSanNhaModelFactory,
            IYeuCauModelFactory yeucauModelFactory,
            IYeuCauService yeucauService,
            ITaiSanNhaService taisannhaService,
            ITaiSanOtoService taisanOtoService,
            ITaiSanClnService taisanClnService,
            ITaiSanMayMocService taisanmaymocService,
            ITaiSanVktService taisanVKTService,
            IYeuCauChiTietService yeucauchitietService,
            ITaiSanClnModelFactory taiSanClnModelFactory,
            ITaiSanVktModelFactory taiSanVktModelFactory,
            IHienTrangService hientrangService,
            IBienDongService biendongService,
            IBienDongChiTietService biendongchitietService,
            IDonViService donviService,
            IYeuCauNhatKyModelFactory yeucaunhatkyModelFactory,
            IYeuCauNhatKyService yeucaunhatkyService,
            IBienDongModelFactory bienDongModelFactory,
            IBienDongChiTietModelFactory bienDongChiTietModelFactory,
            ITaiSanVoHinhService taiSanVoHinhService,
            ITaiSanVoHinhModelFactory taiSanVoHinhModelFactory,
            ILoaiTaiSanVoHinhModelFactory loaiTaiSanVoHinhModelFactory,
            ILoaiTaiSanDonViServices loaiTaiSanVoHinhService,
            ITrungGianBDYCService trungGianBDYCService,
            ITrungGianBDYCModelFactory trungGianBDYCModelFactory,
            INguoiDungService nguoiDungService,
            IThaoTacBienDongModelFactory thaoTacBienDongModelFactory,
            ICauHinhService cauHinhService,
            ITaiSanNhatKyService taiSanNhatKyService,
            ILoaiTaiSanKhauHaoService loaiTaiSanKhauHaoService,
            IHaoMonTaiSanModelFactory haoMonTaiSanModelFactory,
            IHaoMonTaiSanService haoMonTaiSanService,
            IChucDanhService chucDanhService,
            IDonViModelFactory donViModelFactory,
            ICapNhatTaiSanModelFactory capNhatTaiSanModelFactory,
            IChucDanhModelFactory chucDanhModelFactory,
            IDinhMucModelFactory dinhMucModelFactory,
            IDB_QueueProcessModelFactory dB_QueueProcessModelFactory,
            ILoaiDonViService loaiDonViService,
            IReportModelFactory reportModelFactory,
            IGSFileProvider gSFileProvider,
            ITaiSanImportService taiSanImportService,
            ITaiSanImportModelFactory taiSanImportModelFactory,
            ITaiSanHienTrangSuDungModelFactory taiSanHienTrangSuDungModelFactory,
            IHinhThucMuaSamService hinhThucMuaSamService,
            IQuocGiaService quocGiaService,
            ITaiSanKhauHaoService taiSanKhauHaoService,
            IKhauHaoTaiSanModelFactory khauHaoTaiSanModelFactory
            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._taisandatModelFactory = taisandatModelFactory;
            this._taisandatService = taisandatService;
            this._diabanModelFactory = diabanModelFactory;
            this._loaitaisanModelFactory = loaitaisanModelFactory;
            this._loaitaisanService = loaitaisanService;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._chedohaomonService = chedohaomonService;
            this._lydobiendongService = lydobiendongService;
            this._lydobiendongModelFactory = lydobiendongModelFactory;
            this._taisannguonvonService = taisannguonvonService;
            this._nguonvonService = nguonvonService;
            this._nguonvonModelFactory = nguonvonModelFactory;
            this._hienTrangModelFactory = hienTrangModelFactory;
            this._taiSanMayMocModelFactory = taiSanMayMocModelFactory;
            this._taiSanOtoModelFactory = taiSanOtoModelFactory;
            this._yeuCauChiTietModelFactory = yeuCauChiTietModelFactory;
            this._taiSanNhaModelFactory = taiSanNhaModelFactory;
            this._yeucauModelFactory = yeucauModelFactory;
            this._yeucauService = yeucauService;
            this._taiSanClnModelFactory = taiSanClnModelFactory;
            this._taisannhaService = taisannhaService;
            this._taisanOtoService = taisanOtoService;
            this._taisanClnService = taisanClnService;
            this._taisanmaymocService = taisanmaymocService;
            this._taisanVKTService = taisanVKTService;
            this._yeucauchitietService = yeucauchitietService;
            this._taiSanVktModelFactory = taiSanVktModelFactory;
            this._hientrangService = hientrangService;
            this._biendongService = biendongService;
            this._biendongchitietService = biendongchitietService;
            this._donviService = donviService;
            this._yeucaunhatkyModelFactory = yeucaunhatkyModelFactory;
            this._yeucaunhatkyService = yeucaunhatkyService;
            this._bienDongModelFactory = bienDongModelFactory;
            this._bienDongChiTietModelFactory = bienDongChiTietModelFactory;
            this._taiSanVoHinhService = taiSanVoHinhService;
            this._taiSanVoHinhModelFactory = taiSanVoHinhModelFactory;
            this._loaiTaiSanVoHinhModelFactory = loaiTaiSanVoHinhModelFactory;
            this._loaiTaiSanDonViServices = loaiTaiSanVoHinhService;
            this._trungGianBDYCService = trungGianBDYCService;
            this._trungGianBDYCModelFactory = trungGianBDYCModelFactory;
            this._nguoiDungService = nguoiDungService;
            this._thaoTacBienDongModelFactory = thaoTacBienDongModelFactory;
            this._cauHinhService = cauHinhService;
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._loaiTaiSanKhauHaoService = loaiTaiSanKhauHaoService;
            this._haoMonTaiSanModelFactory = haoMonTaiSanModelFactory;
            this._haoMonTaiSanService = haoMonTaiSanService;
            this._chucDanhService = chucDanhService;
            this._donViModelFactory = donViModelFactory;
            this._capNhatTaiSanModelFactory = capNhatTaiSanModelFactory;
            this._dB_QueueProcessModelFactory = dB_QueueProcessModelFactory;
            this._loaiDonViService = loaiDonViService;
            this._chucDanhModelFactory = chucDanhModelFactory;
            this._dinhMucModelFactory = dinhMucModelFactory;
            this._reportModelFactory = reportModelFactory;
            this._fileProvider = gSFileProvider;
            this._taiSanImportService = taiSanImportService;
            this._taiSanImportModelFactory = taiSanImportModelFactory;
            this._taiSanHienTrangSuDungModelFactory = taiSanHienTrangSuDungModelFactory;
            this._hinhThucMuaSamService = hinhThucMuaSamService;
            this._quocGiaService = quocGiaService;
            this._khauHaoTaiSanModelFactory = khauHaoTaiSanModelFactory;
            this._taiSanKhauHaoService = taiSanKhauHaoService;
        }

        #endregion Ctor

        #region Methods

        public virtual IActionResult EntryTaiSan()
        {
            if (_quyenService.Authorize(StandardPermissionProvider.USERQLTraCuuTaiSan))
                return RedirectToAction("TraCuuTaiSan");
            else if (_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return RedirectToAction("List");
            else
                return AccessDeniedView();
        }

        public virtual IActionResult List(bool? isDanhSachTSDA = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu) && !_quyenService.Authorize(StandardPermissionProvider.USERQLTraCuuTaiSan))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanSearchModel();
            var hasDonViQLDA = _donviService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
            if (hasDonViQLDA)
            {
                var donvi = _donViModelFactory.GetDonViWithConditions(donViId: _workContext.CurrentDonVi.ID, isBanQuanLyDuAn: hasDonViQLDA, isCreateTSDA: isDanhSachTSDA);
                searchmodel.donviId = donvi.ID;
            }
            else
                searchmodel.donviId = _workContext.CurrentDonVi.ID;
            searchmodel.IsDanhSachTaiSanDuAn = isDanhSachTSDA;
            var model = new TaiSanSearchModel();
            // nếu đơn vị không quy định tài sản đặc thù
            if (!_loaiTaiSanDonViServices.CheckLoaiTaiSanDacThu(_workContext.CurrentDonVi?.ID))
            {
                model = _itemModelFactory.PrepareTaiSanSearchModel(searchmodel, isExcutedTSDT: true);
            }
            else
            {
                model = _itemModelFactory.PrepareTaiSanSearchModel(searchmodel);
            }

            model.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET;
            var _searchModel = HttpContext.Session.GetObject<TaiSanSearchModel>(enumTENCAUHINH.KeySearch + typeof(TaiSanSearchModel).Name);
            if (_searchModel != null && _searchModel.IsLoadSession)
            {
                model.pageIndex = _searchModel.pageIndex;
                model.Page = _searchModel.Page;
                model.PageSize = _searchModel.PageSize;
                model.KeySearch = _searchModel.KeySearch;
                model.donviId = _searchModel.donviId;
                model.TRANG_THAI_ID = _searchModel.TRANG_THAI_ID;
                model.DON_VI_BO_PHAN_ID = _searchModel.DON_VI_BO_PHAN_ID;
                model.Fromdate = _searchModel.Fromdate;
                model.Todate = _searchModel.Todate;
                model.pageIndex = _searchModel.pageIndex;
                model.isduoi500 = _searchModel.isduoi500;
                model.istren500 = _searchModel.istren500;
                model.LoaiHinhTaiSanSelected = _searchModel.strLoaiHinhTSIds != null ? _searchModel.strLoaiHinhTSIds.Split(',').ToList().Select(m => int.Parse(m)).ToList() : null;
                UpdateSessionSearchModel<TaiSanSearchModel>(false);
            }

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(TaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedView();
            //prepare model
            if (searchModel.PageSize == 0) searchModel.PageSize = 10;
            //searchModel.NguoiTaoId = _workContext.CurrentCustomer.ID;
            searchModel.NguoiTaoId = 0;
            searchModel.isDuyet = true; // check hiển thị tài sản đã giảm toàn bộ hay không
            var model = _itemModelFactory.PrepareDanhSachTaiSan(searchModel);
            HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch + searchModel.GetType().Name, searchModel);
            return Json(model);
        }

        public virtual IActionResult _ListTaiSanForDeNghiXuLy(int? pageIndex = 0, bool? isDanhSachTSDA = false, String KeySearch = "", decimal DE_NGHI_XU_LY_ID = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu) && !_quyenService.Authorize(StandardPermissionProvider.USERQLTraCuuTaiSan))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanSearchModel();
            searchmodel.KeySearch = KeySearch;
            var hasDonViQLDA = _donviService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
            if (hasDonViQLDA)
            {
                var donvi = _donViModelFactory.GetDonViWithConditions(donViId: _workContext.CurrentDonVi.ID, isBanQuanLyDuAn: hasDonViQLDA, isCreateTSDA: isDanhSachTSDA);
                searchmodel.donviId = donvi.ID;
            }
            else
            {
                searchmodel.donviId = _workContext.CurrentDonVi.ID;
            }
            searchmodel.IsDanhSachTaiSanDuAn = isDanhSachTSDA;
            var model = _itemModelFactory.PrepareTaiSanSearchModel(searchmodel);
            model.DE_NGHI_XU_LY_ID = DE_NGHI_XU_LY_ID;
            if (pageIndex > 0)
            {
                model.Page = (int)pageIndex;
            }
            return PartialView(model);
        }

        //[HttpPost]
        //public virtual IActionResult ListTaiSanGiamNhieu(TaiSanSearchModel searchModel)
        //{
        //	if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan))
        //		return AccessDeniedView();
        //	if (searchModel.PageSize == 0) searchModel.PageSize = 10;
        //}
        public virtual IActionResult TaiSanChuaTinhHaoMon(decimal LoaiHinhTaiSanId = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLCongCuTaiSanChuaTinhHaoMon))
                return AccessDeniedView();
            var searchmodel = new TaiSanSearchModel();
            var model = _itemModelFactory.PrepareTaiSanSearchModel(searchmodel, true);
            if (LoaiHinhTaiSanId > 0)
            {
                foreach (var lhtschecK in model.LoaiHinhTaiSanAvailable)
                {
                    if (lhtschecK.Value.ToNumber() == (decimal)enumLOAI_HINH_TAI_SAN.ALL)
                    {
                        lhtschecK.Selected = false;
                    }
                    if (LoaiHinhTaiSanId == lhtschecK.Value.ToNumber())
                    {
                        lhtschecK.Selected = true;
                    }
                }
            }
            model.ddlNguonTaiSan = (new enumNguonTaiSan()).ToSelectList().ToList();
            model.isHaoMon = false;
            return View(model);
        }

        public virtual IActionResult TraCuuTaiSan(decimal LoaiHinhTaiSanId = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTraCuuTaiSan))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanSearchModel();
            var model = _itemModelFactory.PrepareTaiSanSearchModel(searchmodel, true);
            if (LoaiHinhTaiSanId > 0)
            {
                foreach (var lhtschecK in model.LoaiHinhTaiSanAvailable)
                {
                    if (lhtschecK.Value.ToNumber() == (decimal)enumLOAI_HINH_TAI_SAN.ALL)
                    {
                        lhtschecK.Selected = false;
                    }
                    if (LoaiHinhTaiSanId == lhtschecK.Value.ToNumber())
                    {
                        lhtschecK.Selected = true;
                    }
                }
            }
            model.ddlNguonTaiSan = (new enumNguonTaiSan()).ToSelectList().ToList();
            model.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET;
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult TraCuuTaiSan(TaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTraCuuTaiSan))
                return AccessDeniedView();
            //prepare model
            searchModel.isDuyet = true;
            searchModel.NguoiTaoId = 0;
            var model = _itemModelFactory.PrepareDanhSachTaiSan(searchModel);

            return Json(model);
        }

        [HttpPost]
        public virtual IActionResult ChotHaoMon(string maTS)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTraCuuTaiSan))
                return AccessDeniedView();
            if (string.IsNullOrEmpty(maTS))
            {
                return JsonErrorMessage("Phải chọn loại tài sản");
            }
            var ts = _itemService.GetTaiSanByMa(maTS);
            if (ts != null)
            {
                var result = _itemService.ChotToanBoHaoMonTS(ts.ID);
                if (result)
                {
                    return JsonSuccessMessage("Chốt thành công");
                }
                else
                {
                    return JsonErrorMessage("Có lỗi xảy ra");
                }
            }
            else
            {
                return JsonErrorMessage("Có lỗi xảy ra");
            }
        }

        public virtual IActionResult ExportTaiSan(string KeySearch, DateTime? Fromdate, DateTime? Todate, decimal? donviId, int? NguonTaiSanId, decimal TRANG_THAI_ID, string strLoaiHinhTSIds, bool isduoi500, bool istren500, bool isToanQuoc)
        {
            TaiSanSearchModel searchModel = new TaiSanSearchModel()
            {
                KeySearch = KeySearch,
                Fromdate = Fromdate,
                Todate = Todate,
                donviId = donviId,
                NguonTaiSanId = NguonTaiSanId,
                TRANG_THAI_ID = TRANG_THAI_ID,
                strLoaiHinhTSIds = strLoaiHinhTSIds,
                isduoi500 = isduoi500,
                istren500 = istren500,
                isToanQuoc = isToanQuoc
            };
            searchModel.isDuyet = true;
            searchModel.NguoiTaoId = 0;
            var model = _itemModelFactory.PrepareExportTaiSan(searchModel);
            int total = model != null ? model.Count() : 0;
            bool addSTT = true;
            string fName = $"TaiSan_{total}({DateTime.Now.ToString("dd-MM-yyyy hh24-mm-ss")})";
            MemoryStream stream = new MemoryStream();
            stream = _reportModelFactory.prepareExcelEntity<TaiSanExport>(stream, model, "DonVi", addSTT);
            var bytes = stream.ToArray();
            return new FileContentResult(bytes, MimeTypes.TextXlsx)
            {
                FileDownloadName = fName + ".xlsx"
            };
        }

        public virtual IActionResult ExportTaiSanKiemTra(TaiSanSearchModel searchModel)
        {
            var model = _itemModelFactory.PrepareExportTaiSanKiemTra(searchModel);
            int total = model != null ? model.Count() : 0;
            bool addSTT = true;
            string fName = $"TaiSan_{total}({DateTime.Now.ToString("dd-MM-yyyy hh24-mm-ss")})";
            MemoryStream stream = new MemoryStream();
            stream = _reportModelFactory.prepareExcelEntity<TaiSanExport>(stream, model, "DonVi", addSTT);
            var bytes = stream.ToArray();
            return new FileContentResult(bytes, MimeTypes.TextXlsx)
            {
                FileDownloadName = fName + ".xlsx"
            };
        }

        public virtual IActionResult NhapLieu()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedView();
            NhapLieuModel model = new NhapLieuModel();
            model.IS_LA_DON_VI_NHAP_LIEU = _workContext.CurrentDonVi.IS_LA_DON_VI_NHAP_LIEU ?? false;
            return View(model);
        }

        public virtual IActionResult ChonLoaiTaiSan(bool isTangMoi, bool? isCreateTSDA = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedView();
            //prepare model
            var model = _loaitaisanModelFactory.PrepareListLoaiHinhTaiSanModelForSelect();
            if (!_loaiTaiSanDonViServices.CheckLoaiTaiSanDacThu(_workContext.CurrentDonVi?.ID))
            {
                model = model.Where(c => c.Id != (int)enumLOAI_HINH_TAI_SAN.DAC_THU).ToList();
            }
            // là nhập số dư hay tăng mới
            model[0].isTangMoi = isTangMoi;
            model[0].isCreateTSDA = isCreateTSDA ?? false;
            //check dơn vị có ts ql như tscđ
            var donvi = _donviService.GetDonViLonNhat(_workContext.CurrentDonVi.ID);
            var Dpac = _donviService.GetDonViById((int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG);
            if (donvi != null)
            {
                model[0].isTSQL = donvi.IS_TSQUAN_LY_NHU_TSCD ?? false;
            }
            if (Dpac.IS_TSQUAN_LY_NHU_TSCD ?? false)
            {
                model[0].isTSQL = true;
            }
            return View(model);
        }

        public virtual IActionResult ChonLoaiTaiSanDuoi500Tr(bool isTangMoi, bool? isCreateTSDA = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedView();
            //prepare model
            var model = _loaitaisanModelFactory.PrepareListLoaiHinhTaiSanModelForSelect();
            if (!_loaiTaiSanDonViServices.CheckLoaiTaiSanDacThu(_workContext.CurrentDonVi?.ID))
            {
                model = model.Where(c => c.Id != (int)enumLOAI_HINH_TAI_SAN.DAC_THU).ToList();
            }

            // Yêu cầu chỉ lấy ra 5 nhóm tài sản --hungnt
            //model = model.Where(c => c.Id == (int)enumLOAI_HINH_TAI_SAN.DAT
            //                     || c.Id == (int)enumLOAI_HINH_TAI_SAN.NHA
            //                     || c.Id == (int)enumLOAI_HINH_TAI_SAN.OTO
            //                     || c.Id == (int)enumLOAI_HINH_TAI_SAN.TSCD_KHAC
            //                     || c.Id == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_QUAN_LY_NHU_TSCD
            //                     ).ToList();
            ///add box empty
            //var emtyLoaiHinhTaiSan = new LoaiHinhTaiSanModel();
            //model.Insert(model.Count() - 2, emtyLoaiHinhTaiSan);
            //model.Insert(model.Count(), emtyLoaiHinhTaiSan);
            // là nhập số dư hay tăng mới
            model[0].isTangMoi = isTangMoi;
            model[0].isCreateTSDA = isCreateTSDA ?? false;
            //check dơn vị có ts ql như tscđ
            var donvi = _donviService.GetDonViLonNhat(_workContext.CurrentDonVi.ID);
            if (donvi != null)
            {
                model[0].isTSQL = donvi.IS_TSQUAN_LY_NHU_TSCD.GetValueOrDefault();
            }
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult _ChonLoaiTaiSanCoDinhKhac(bool isTangMoi, bool? isCreateTSDA = false, int nhomTaiSanId = (int)enumLOAI_HINH_TAI_SAN.TSCD_KHAC)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedView();
            //prepare model
            var model = _loaitaisanModelFactory.PrepareListLoaiHinhTaiSanModel();
            if (nhomTaiSanId == (int)enumLOAI_HINH_TAI_SAN.TSCD_KHAC)
            {
                // Yêu cầu chỉ lấy ra 5 nhóm tài sản --hungnt
                model = model.Where(c => c.Id == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC
                                     || c.Id == (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC
                                     || c.Id == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI
                                     || c.Id == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV
                                     || c.Id == (int)enumLOAI_HINH_TAI_SAN.VO_HINH
                                     || c.Id == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC
                                     || c.Id == (int)enumLOAI_HINH_TAI_SAN.DAC_THU
                                     ).ToList();
                var emtyLoaiHinhTaiSan = new LoaiHinhTaiSanModel();
                model.Insert(model.Count() - 1, emtyLoaiHinhTaiSan);
                model[0].isTSQL = false;
            }
            else if (nhomTaiSanId == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_QUAN_LY_NHU_TSCD)
            {
                model = model.Where(c => c.Id == (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC
                                   || c.Id == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI
                                   || c.Id == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV
                                   || c.Id == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC
                                   ).ToList();
                var emtyLoaiHinhTaiSan = new LoaiHinhTaiSanModel();
                model.Insert(model.Count() - 1, emtyLoaiHinhTaiSan);
                model[0].isTSQL = true; // check xem nhóm nào là tsql
            }
            else
            {
                model = model.Where(c => c.Id == (int)enumLOAI_HINH_TAI_SAN.DAT
                                  || c.Id == (int)enumLOAI_HINH_TAI_SAN.NHA
                                  || c.Id == (int)enumLOAI_HINH_TAI_SAN.OTO
                                  || c.Id == (int)enumLOAI_HINH_TAI_SAN.TSCD_KHAC
                                  || c.Id == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_QUAN_LY_NHU_TSCD
                                  ).ToList();
                model[0].isTSQL = false;
            }
            ////add box empty
            //var emtyLoaiHinhTaiSan = new LoaiHinhTaiSanModel();
            //model.Insert(model.Count() - 2, emtyLoaiHinhTaiSan);
            //model.Insert(model.Count(), emtyLoaiHinhTaiSan);
            // là nhập số dư hay tăng mới
            model[0].isTangMoi = isTangMoi;
            model[0].isCreateTSDA = isCreateTSDA ?? false;
            return PartialView(model);
        }

        public virtual IActionResult _ChonLoaiTaiSan(/*int loaiBienDongId = (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO,*/ bool isTangMoi = true, bool? isCreateTSDA = false, int nhomTaiSanId = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedView();
            //prepare model
            var model = _loaitaisanModelFactory.PrepareListLoaiHinhTaiSanModelForSelect();
            if (!_loaiTaiSanDonViServices.CheckLoaiTaiSanDacThu(_workContext.CurrentDonVi?.ID))
            {
                model = model.Where(c => c.Id != (int)enumLOAI_HINH_TAI_SAN.DAC_THU).ToList();
            }
            //add box empty
            //var emtyLoaiHinhTaiSan = new LoaiHinhTaiSanModel();
            //model.Insert(model.Count() - 2, emtyLoaiHinhTaiSan);
            //model.Insert(model.Count(), emtyLoaiHinhTaiSan);
            model[0].isTangMoi = isTangMoi;
            model[0].isCreateTSDA = isCreateTSDA ?? false;
            //model[0].LoaiBienDongId = loaiBienDongId;
            var donvi = _donviService.GetDonViLonNhat(_workContext.CurrentDonVi.ID);
            var Dpac = _donviService.GetDonViById((int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG);
            if (donvi != null)
            {
                model[0].isTSQL = donvi.IS_TSQUAN_LY_NHU_TSCD.GetValueOrDefault();
            }
            if (Dpac.IS_TSQUAN_LY_NHU_TSCD ?? false)
            {
                model[0].isTSQL = true;
            }
            return PartialView(model);
        }

        public virtual IActionResult TaiSanOto(decimal? loailydobiendongId = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedView();
            var item = new TaiSan();
            var model = new TaiSanModel();
            model.LoaiLyDoBienDongId = (int)enumLOAI_HINH_TAI_SAN.OTO;
            item.LOAI_HINH_TAI_SAN_ID = (int)enumLOAI_HINH_TAI_SAN.OTO;
            item.TEN = " ";
            item.DON_VI_ID = _workContext.CurrentDonVi.ID;
            //item.NGAY_NHAP = DateTime.Now;
            item.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.NHAP;
            _itemService.InsertTaiSan(item);
            //insert NV_YEU_CAU

            var yeucau = new YeuCau();
            yeucau.TAI_SAN_ID = item.ID;
            yeucau.LOAI_HINH_TAI_SAN_ID = item.LOAI_HINH_TAI_SAN_ID;
            yeucau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.NHAP;
            yeucau.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            _yeucauService.InsertYeuCau(yeucau);

            // insert NV_YeuCauChiTiet
            var yeucauchitiet = new YeuCauChiTiet();
            yeucauchitiet.YEU_CAU_ID = yeucau.ID;
            var lstHienTrang = _hientrangService.GetHienTrangs(LoaiHinhTsId: yeucau.LOAI_HINH_TAI_SAN_ID, isTSDA: _donviService.isDonViBanQuanLyDuAn(model.DON_VI_ID.GetValueOrDefault()));
            var lstObjHienTrang = new List<ObjHienTrang>();
            foreach (var ht in lstHienTrang)
            {
                var obj = new ObjHienTrang();
                obj.HienTrangId = ht.ID;
                obj.TenHienTrang = ht.TEN_HIEN_TRANG;
                obj.KieuDuLieuId = ht.KIEU_DU_LIEU_ID;
                obj.NhomHienTrangId = ht.NHOM_HIEN_TRANG_ID;
                obj.GiaTriCheckbox = false;
                lstObjHienTrang.Add(obj);
            }
            var hientrangList = new HienTrangList()
            {
                TaiSanId = item.ID,
                lstObjHienTrang = lstObjHienTrang
            };
            yeucauchitiet.HTSD_JSON = hientrangList.toStringJson();
            yeucauchitiet.DATA_JSON = yeucau.ToModel<YeuCauModel>().toStringJson();
            _yeucauchitietService.InsertYeuCauChiTiet(yeucauchitiet);

            model = _itemModelFactory.PrepareTaiSanModel(model, item);
            model.LyDoTangAvailable = _lydobiendongModelFactory.PrepareSelectListLyDoBienDong(LoaiHinhTaiSanId: model.LOAI_HINH_TAI_SAN_ID, loailydoId: loailydobiendongId);
            model.LyDoTangAvailable.AddFirstRow("-- Chọn lý do tăng --");
            model.LOAI_HINH_TAI_SAN_ID = (int)enumLOAI_HINH_TAI_SAN.OTO;

            var TsOto = new TaiSanOto();
            TsOto.TAI_SAN_ID = item.ID;
            TsOto.BIEN_KIEM_SOAT = " ";
            _taisanOtoService.InsertTaiSanOto(TsOto);
            var itemOto = _taisanOtoService.GetTaiSanOtoByTaiSanId(item.ID);
            model.taisanOtoModel = _taiSanOtoModelFactory.PrepareTaiSanOtoModel(new TaiSanOtoModel(), itemOto);
            //model.taisanOtoModel = TsOto.ToModel<TaiSanOtoModel>();

            model.YeuCauId = yeucau.ID;
            model.SoLuongNhanBan = 1;
            return View(model);
        }

        public virtual IActionResult Create(decimal LoaiHinhTSId, decimal? loailydobiendongId = 0, bool? isTangMoi = null, bool? isCreateTSDA = false, bool isTSQL = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu) || !_workContext.CurrentDonVi.IS_LA_DON_VI_NHAP_LIEU.GetValueOrDefault(false))
                return AccessDeniedView();
            var item = new TaiSan();
            var model = new TaiSanModel();
            model.LoaiLyDoBienDongId = loailydobiendongId;
            item.LOAI_HINH_TAI_SAN_ID = Convert.ToDecimal(LoaiHinhTSId);
            item.TEN = " ";
            var hasChildDonViQLDA = _donviService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
            var donvi = _donViModelFactory.GetDonViWithConditions(donViId: _workContext.CurrentDonVi.ID, isBanQuanLyDuAn: hasChildDonViQLDA, isCreateTSDA: isCreateTSDA);
            if (donvi != null)
            {
                item.DON_VI_ID = donvi.ID;
            }
            else
            {
                //trường hợp là đơn vị ban quản lý dự án nhưng không có đơn vị dự án và văn phòng thì tạo 2 đơn vị con quản lý
                DonVi donViTSC = _donViModelFactory.PrepareDonViConChoBQLDA(parentId: _workContext.CurrentDonVi.ID, pLA_BAN_QL_DU_AN: false);
                _donviService.InsertDonVi(donViTSC);
                DonVi donViTSDA = _donViModelFactory.PrepareDonViConChoBQLDA(parentId: _workContext.CurrentDonVi.ID, pLA_BAN_QL_DU_AN: true);
                _donviService.InsertDonVi(donViTSDA);
                if (isCreateTSDA.Value)
                {
                    item.DON_VI_ID = donViTSDA.ID;
                }
                else
                {
                    item.DON_VI_ID = donViTSC.ID;
                }
            }
            //item.NGAY_NHAP = DateTime.Now;
            item.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.NHAP;
            _itemService.InsertTaiSan(item);
            //insert NV_YEU_CAU

            var yeucau = new YeuCau();
            yeucau.TAI_SAN_ID = item.ID;
            yeucau.LOAI_HINH_TAI_SAN_ID = item.LOAI_HINH_TAI_SAN_ID;
            yeucau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.NHAP;
            yeucau.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            _yeucauService.InsertYeuCau(yeucau);

            // insert NV_YeuCauChiTiet
            var yeucauchitiet = new YeuCauChiTiet();
            yeucauchitiet.YEU_CAU_ID = yeucau.ID;
            var lstHienTrang = _hientrangService.GetHienTrangs(LoaiHinhTsId: yeucau.LOAI_HINH_TAI_SAN_ID, isTSDA: isCreateTSDA);
            var lstObjHienTrang = new List<ObjHienTrang>();
            foreach (var ht in lstHienTrang)
            {
                if (ht.HIEN_THI_ID == (int)enumHienTrang_HIEN_THI_ID.KHONG_HIEN_THI)
                    continue;
                var obj = new ObjHienTrang();
                obj.HienTrangId = ht.ID;
                obj.TenHienTrang = ht.TEN_HIEN_TRANG;
                obj.KieuDuLieuId = ht.KIEU_DU_LIEU_ID;
                obj.NhomHienTrangId = ht.NHOM_HIEN_TRANG_ID;
                obj.GiaTriCheckbox = false;
                lstObjHienTrang.Add(obj);
            }
            var hientrangList = new HienTrangList()
            {
                TaiSanId = item.ID,
                lstObjHienTrang = lstObjHienTrang
            };
            yeucauchitiet.HTSD_JSON = hientrangList.toStringJson();
            yeucauchitiet.DATA_JSON = yeucau.ToModel<YeuCauModel>().toStringJson();
            _yeucauchitietService.InsertYeuCauChiTiet(yeucauchitiet);

            model = _itemModelFactory.PrepareTaiSanModel(model: model, item: item, isCreateTaiSan: true, tsQLNTSCD: isTSQL);
            model.isTangMoi = isTangMoi;
            model.isCreateTSDA = isCreateTSDA;
            model.LyDoTangAvailable = _lydobiendongModelFactory.PrepareSelectListLyDoBienDong(LoaiHinhTaiSanId: model.LOAI_HINH_TAI_SAN_ID, loailydoId: loailydobiendongId, isTangMoi: isTangMoi);
            if (isTangMoi != null && isTangMoi == true)
            {
                model.LyDoTangAvailable.AddFirstRow("-- Chọn lý do tăng --");
            }
            model.LOAI_HINH_TAI_SAN_ID = LoaiHinhTSId;
            //insert chi tiet tung loai tai san(TS_TAI_SAN_DAT, TS_TAI_SAN_NHA....)
            switch (item.LOAI_HINH_TAI_SAN_ID)
            {
                case (int)enumLOAI_HINH_TAI_SAN.DAT:
                    var TsDat = new TaiSanDat();
                    TsDat.DIEN_TICH = 0;
                    TsDat.TAI_SAN_ID = item.ID;
                    _taisandatService.InsertTaiSanDat(TsDat);
                    var itemTSDat = _taisandatService.GetTaiSanDatByTaiSanId(item.ID);
                    model.taisandatModel = _taisandatModelFactory.PrepareTaiSanDatModel(TsDat.ToModel<TaiSanDatModel>(), itemTSDat);
                    model.cohoso = true;
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.NHA:
                    var TsNha = new TaiSanNha();
                    TsNha.TAI_SAN_ID = item.ID;
                    TsNha.TAI_SAN_DAT_ID = 0;
                    TsNha.DIEN_TICH_SAN_XAY_DUNG = 0;
                    TsNha.DIEN_TICH_XAY_DUNG = item.NAM_SAN_XUAT;
                    TsNha.NGAY_SU_DUNG = null;
                    _taisannhaService.InsertTaiSanNha(TsNha);
                    model.LOAI_HINH_TAI_SAN_ID = item.LOAI_HINH_TAI_SAN_ID;
                    var itemTsNha = _taisannhaService.GetTaiSanNhaByTaiSanId(item.ID);
                    model.taisannhaModel = _taiSanNhaModelFactory.PrepareTaiSanNhaModel(TsNha.ToModel<TaiSanNhaModel>(), itemTsNha);
                    model.taisannhaModel.isQuanLyDat = true;
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                case (int)enumLOAI_HINH_TAI_SAN.OTO:
                    var TsOto = new TaiSanOto();
                    TsOto.TAI_SAN_ID = item.ID;
                    TsOto.BIEN_KIEM_SOAT = "-";
                    _taisanOtoService.InsertTaiSanOto(TsOto);
                    model.taisanOtoModel = TsOto.ToModel<TaiSanOtoModel>();
                    var itemOto = _taisanOtoService.GetTaiSanOtoByTaiSanId(item.ID);
                    model.taisanOtoModel = _taiSanOtoModelFactory.PrepareTaiSanOtoModel(model.taisanOtoModel, itemOto);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                    var tsCayLauNam = new TaiSanCln();
                    tsCayLauNam.TAI_SAN_ID = item.ID;
                    _taisanClnService.InsertTaiSanCln(tsCayLauNam);
                    model.taisanClnModel = tsCayLauNam.ToModel<TaiSanClnModel>();
                    model.taisanClnModel = _taiSanClnModelFactory.PrepareTaiSanClnModel(model.taisanClnModel, tsCayLauNam);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                    var tsMayMoc = new TaiSanMayMoc();
                    tsMayMoc.TAI_SAN_ID = item.ID;
                    _taisanmaymocService.InsertTaiSanMayMoc(tsMayMoc);
                    model.taisanmaymocModel = _taiSanMayMocModelFactory.PrepareTaiSanMayMocModel(model.taisanmaymocModel, tsMayMoc);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    var tsVatKienTruc = new TaiSanVkt();
                    tsVatKienTruc.TAI_SAN_ID = item.ID;
                    _taisanVKTService.InsertTaiSanVkt(tsVatKienTruc);
                    model.taisanVktModel = _taiSanVktModelFactory.PrepareTaiSanVktModel(model.taisanVktModel, tsVatKienTruc);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                    var taiSanVoHinh = new TaiSanVoHinh();
                    taiSanVoHinh.TAI_SAN_ID = item.ID;
                    _taiSanVoHinhService.InsertTaiSanVoHinh(taiSanVoHinh);
                    model.taisanvohinhModel = _taiSanVoHinhModelFactory.PrepareTaiSanVoHinhModel(model.taisanvohinhModel, taiSanVoHinh);
                    break;
            }
            model.YeuCauId = yeucau.ID;
            model.SoLuongNhanBan = 1;
            if (isTangMoi == false)
            {
                model.NGAY_NHAP = new DateTime(2017, 12, 31);
            }
            // check tsql như tscđ khác
            model.isTSQL = isTSQL;
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Create(string LOAI_HINH_TAI_SAN_ID, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu)
                || !_workContext.CurrentDonVi.IS_LA_DON_VI_NHAP_LIEU.GetValueOrDefault(false))
                return AccessDeniedView();
            var sessionId = _workContext.CurrentCustomer.GUID;
            var item = new TaiSan();

            //_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<TaiSanModel>(), "TaiSan");
            return RedirectToAction("Edit", new { id = item.ID });
        }

        public virtual IActionResult CreateTest(decimal LoaiHinhTSId, decimal? loailydobiendongId = 0, bool? isTangMoi = null, bool? isCreateTSDA = false, bool isTSQL = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedView();
            var item = new TaiSan();
            var model = new TaiSanModel();
            model.LoaiLyDoBienDongId = loailydobiendongId;
            item.LOAI_HINH_TAI_SAN_ID = Convert.ToDecimal(LoaiHinhTSId);
            item.TEN = " ";
            var hasChildDonViQLDA = _donviService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
            var donvi = _donViModelFactory.GetDonViWithConditions(donViId: _workContext.CurrentDonVi.ID, isBanQuanLyDuAn: hasChildDonViQLDA, isCreateTSDA: isCreateTSDA);
            if (donvi != null)
            {
                item.DON_VI_ID = donvi.ID;
            }
            else
            {
                //trường hợp là đơn vị ban quản lý dự án nhưng không có đơn vị dự án và văn phòng thì tạo 2 đơn vị con quản lý
                DonVi donViTSC = _donViModelFactory.PrepareDonViConChoBQLDA(parentId: _workContext.CurrentDonVi.ID, pLA_BAN_QL_DU_AN: false);
                _donviService.InsertDonVi(donViTSC);
                DonVi donViTSDA = _donViModelFactory.PrepareDonViConChoBQLDA(parentId: _workContext.CurrentDonVi.ID, pLA_BAN_QL_DU_AN: true);
                _donviService.InsertDonVi(donViTSDA);
                if (isCreateTSDA.Value)
                {
                    item.DON_VI_ID = donViTSDA.ID;
                }
                else
                {
                    item.DON_VI_ID = donViTSC.ID;
                }
            }
            //item.NGAY_NHAP = DateTime.Now;
            item.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.NHAP;
            _itemService.InsertTaiSan(item);
            //insert NV_YEU_CAU

            var yeucau = new YeuCau();
            yeucau.TAI_SAN_ID = item.ID;
            yeucau.LOAI_HINH_TAI_SAN_ID = item.LOAI_HINH_TAI_SAN_ID;
            yeucau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.NHAP;
            yeucau.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            _yeucauService.InsertYeuCau(yeucau);

            // insert NV_YeuCauChiTiet
            var yeucauchitiet = new YeuCauChiTiet();
            yeucauchitiet.YEU_CAU_ID = yeucau.ID;
            var lstHienTrang = _hientrangService.GetHienTrangs(LoaiHinhTsId: yeucau.LOAI_HINH_TAI_SAN_ID, isTSDA: isCreateTSDA);
            var lstObjHienTrang = new List<ObjHienTrang>();
            foreach (var ht in lstHienTrang)
            {
                if (ht.HIEN_THI_ID == (int)enumHienTrang_HIEN_THI_ID.KHONG_HIEN_THI)
                    continue;
                var obj = new ObjHienTrang();
                obj.HienTrangId = ht.ID;
                obj.TenHienTrang = ht.TEN_HIEN_TRANG;
                obj.KieuDuLieuId = ht.KIEU_DU_LIEU_ID;
                obj.NhomHienTrangId = ht.NHOM_HIEN_TRANG_ID;
                obj.GiaTriCheckbox = false;
                lstObjHienTrang.Add(obj);
            }
            var hientrangList = new HienTrangList()
            {
                TaiSanId = item.ID,
                lstObjHienTrang = lstObjHienTrang
            };
            yeucauchitiet.HTSD_JSON = hientrangList.toStringJson();
            yeucauchitiet.DATA_JSON = yeucau.ToModel<YeuCauModel>().toStringJson();
            _yeucauchitietService.InsertYeuCauChiTiet(yeucauchitiet);

            model = _itemModelFactory.PrepareTaiSanModel(model: model, item: item, isCreateTaiSan: true, tsQLNTSCD: isTSQL);
            model.isTangMoi = isTangMoi;
            model.isCreateTSDA = isCreateTSDA;
            model.LyDoTangAvailable = _lydobiendongModelFactory.PrepareSelectListLyDoBienDong(LoaiHinhTaiSanId: model.LOAI_HINH_TAI_SAN_ID, loailydoId: loailydobiendongId, isTangMoi: isTangMoi);
            if (isTangMoi != null && isTangMoi == true)
            {
                model.LyDoTangAvailable.AddFirstRow("-- Chọn lý do tăng --");
            }
            model.LOAI_HINH_TAI_SAN_ID = LoaiHinhTSId;
            //insert chi tiet tung loai tai san(TS_TAI_SAN_DAT, TS_TAI_SAN_NHA....)
            switch (item.LOAI_HINH_TAI_SAN_ID)
            {
                case (int)enumLOAI_HINH_TAI_SAN.DAT:
                    var TsDat = new TaiSanDat();
                    TsDat.DIEN_TICH = 0;
                    TsDat.TAI_SAN_ID = item.ID;
                    _taisandatService.InsertTaiSanDat(TsDat);
                    var itemTSDat = _taisandatService.GetTaiSanDatByTaiSanId(item.ID);
                    model.taisandatModel = _taisandatModelFactory.PrepareTaiSanDatModel(TsDat.ToModel<TaiSanDatModel>(), itemTSDat);
                    model.cohoso = true;
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.NHA:
                    var TsNha = new TaiSanNha();
                    TsNha.TAI_SAN_ID = item.ID;
                    TsNha.TAI_SAN_DAT_ID = 0;
                    TsNha.DIEN_TICH_SAN_XAY_DUNG = 0;
                    TsNha.DIEN_TICH_XAY_DUNG = item.NAM_SAN_XUAT;
                    TsNha.NGAY_SU_DUNG = null;
                    _taisannhaService.InsertTaiSanNha(TsNha);
                    model.LOAI_HINH_TAI_SAN_ID = item.LOAI_HINH_TAI_SAN_ID;
                    var itemTsNha = _taisannhaService.GetTaiSanNhaByTaiSanId(item.ID);
                    model.taisannhaModel = _taiSanNhaModelFactory.PrepareTaiSanNhaModel(TsNha.ToModel<TaiSanNhaModel>(), itemTsNha);
                    model.taisannhaModel.isQuanLyDat = true;
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                case (int)enumLOAI_HINH_TAI_SAN.OTO:
                    var TsOto = new TaiSanOto();
                    TsOto.TAI_SAN_ID = item.ID;
                    TsOto.BIEN_KIEM_SOAT = "-";
                    _taisanOtoService.InsertTaiSanOto(TsOto);
                    model.taisanOtoModel = TsOto.ToModel<TaiSanOtoModel>();
                    var itemOto = _taisanOtoService.GetTaiSanOtoByTaiSanId(item.ID);
                    model.taisanOtoModel = _taiSanOtoModelFactory.PrepareTaiSanOtoModel(model.taisanOtoModel, itemOto);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                    var tsCayLauNam = new TaiSanCln();
                    tsCayLauNam.TAI_SAN_ID = item.ID;
                    _taisanClnService.InsertTaiSanCln(tsCayLauNam);
                    model.taisanClnModel = tsCayLauNam.ToModel<TaiSanClnModel>();
                    model.taisanClnModel = _taiSanClnModelFactory.PrepareTaiSanClnModel(model.taisanClnModel, tsCayLauNam);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                    var tsMayMoc = new TaiSanMayMoc();
                    tsMayMoc.TAI_SAN_ID = item.ID;
                    _taisanmaymocService.InsertTaiSanMayMoc(tsMayMoc);
                    model.taisanmaymocModel = _taiSanMayMocModelFactory.PrepareTaiSanMayMocModel(model.taisanmaymocModel, tsMayMoc);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    var tsVatKienTruc = new TaiSanVkt();
                    tsVatKienTruc.TAI_SAN_ID = item.ID;
                    _taisanVKTService.InsertTaiSanVkt(tsVatKienTruc);
                    model.taisanVktModel = _taiSanVktModelFactory.PrepareTaiSanVktModel(model.taisanVktModel, tsVatKienTruc);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                    var taiSanVoHinh = new TaiSanVoHinh();
                    taiSanVoHinh.TAI_SAN_ID = item.ID;
                    _taiSanVoHinhService.InsertTaiSanVoHinh(taiSanVoHinh);
                    model.taisanvohinhModel = _taiSanVoHinhModelFactory.PrepareTaiSanVoHinhModel(model.taisanvohinhModel, taiSanVoHinh);
                    break;
            }
            model.YeuCauId = yeucau.ID;
            model.SoLuongNhanBan = 1;
            if (isTangMoi == false)
            {
                model.NGAY_NHAP = new DateTime(2017, 12, 31);
            }
            // check tsql như tscđ khác
            model.isTSQL = isTSQL;
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult CreateTest(string LOAI_HINH_TAI_SAN_ID, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedView();

            var item = new TaiSan();

            //_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<TaiSanModel>(), "TaiSan");
            return RedirectToAction("Edit", new { id = item.ID });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="yeucauId"></param>
        /// <param name="continueAddNha">check taisandat btn lưu và thêm nhà</param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public virtual IActionResult Edit(Guid guid, decimal? yeucauId = 0, bool continueAddNha = false, int? pageIndex = 1)

        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu) || !_workContext.CurrentDonVi.IS_LA_DON_VI_NHAP_LIEU.GetValueOrDefault(false))
                return AccessDeniedView();
            var item = _itemService.GetTaiSanByGuId(guid);

            if (item.PHAN_LOAI_TAI_SAN == 1)
            {
                var bd = _biendongService.GetBienDongCuoiByTaiSanId(item.ID);
                if (bd != null)
                {
                    string Note = "";
                    _thaoTacBienDongModelFactory.HuyDuyetBienDongFunc(bd.ID, Note);
                }
            }

            var model = new TaiSanModel();
            model.YeuCauId = yeucauId;
            model = _itemModelFactory.PrepareTaiSanModel(model: model, item: item, isCreateTaiSan: true);
            switch (model.LOAI_HINH_TAI_SAN_ID)
            {
                case (int)enumLOAI_HINH_TAI_SAN.DAT:
                    model.taisandatModel = _taisandatModelFactory.PrepareTaiSanDatModel(model: model.taisandatModel, item: _taisandatService.GetTaiSanDatByTaiSanId(model.ID));
                    model.cohoso = model.taisandatModel.cohoso;
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.NHA:
                    model.taisannhaModel = _taiSanNhaModelFactory.PrepareTaiSanNhaModel(model: model.taisannhaModel, item: _taisannhaService.GetTaiSanNhaByTaiSanId(model.ID));
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    model.taisanVktModel = _taiSanVktModelFactory.PrepareTaiSanVktModel(model: model.taisanVktModel, item: _taisanVKTService.GetTaiSanVktByTaiSanId(model.ID));
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.OTO:
                case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                    model.taisanOtoModel = _taiSanOtoModelFactory.PrepareTaiSanOtoModel(model: model.taisanOtoModel, item: _taisanOtoService.GetTaiSanOtoByTaiSanId(model.ID));
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                    model.taisanClnModel = _taiSanClnModelFactory.PrepareTaiSanClnModel(model: model.taisanClnModel, item: _taisanClnService.GetTaiSanClnByTaiSanId(model.ID));
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                    model.taisanmaymocModel = _taiSanMayMocModelFactory.PrepareTaiSanMayMocModel(model: model.taisanmaymocModel, item: _taisanmaymocService.GetTaiSanMaymocByTaiSanId(model.ID));
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                    model.taisanvohinhModel = _taiSanVoHinhModelFactory.PrepareTaiSanVoHinhModel(model: model.taisanvohinhModel, item: _taiSanVoHinhService.GetTaiSanVoHinhByTaiSanId(model.ID));
                    break;

                default:
                    break;
            }
            model.pageIndex = pageIndex ?? 1;
            if (model.YeuCauId > 0)
                model.YeuCauStatusId = _yeucauService.GetYeuCauById(model.YeuCauId ?? 0).TRANG_THAI_ID;
            model.ContinueAndAddNha = continueAddNha;
            if (model.NGAY_NHAP.HasValue && model.NGAY_NHAP < new DateTime(2018, 01, 01))
                model.isTangMoi = false;
            else
                model.isTangMoi = true;
            DonVi tsDonViId = _donviService.GetDonViById(model.DON_VI_ID.Value);
            if (model.DU_AN_ID > 0 || (tsDonViId != null && tsDonViId.LA_BAN_QL_DU_AN != null && tsDonViId.LA_BAN_QL_DU_AN.Value))
            {
                model.isCreateTSDA = true;
            }
            else
            {
                model.isCreateTSDA = false;
            }
            if (model.CHE_DO_HACH_TOAN_ID != (int)GS.Core.Domain.DanhMuc.enumCHE_DO_HACH_TOAN.HAO_MON)
            {
                model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI = (model.nvYeuCauChiTietModel.HM_GIA_TRI_CON_LAI ?? 0) + (model.nvYeuCauChiTietModel.KH_CON_LAI ?? 0);
            }
            // do không lưu điều kiện là ts quản lý như tscđ trong db nên phải check nguyên giá ts có >5tr và <10tr không
            //nếu thỏa mã thì coi đó là ts quản lý như tscđ rồi bắt validate
            if (model.nvYeuCauChiTietModel.NGUYEN_GIA >= 5000000 && model.nvYeuCauChiTietModel.NGUYEN_GIA < 10000000)
            {
                var listTSQL = new List<decimal>() { (decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC, (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI,
                                                    (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV, (decimal)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC,
                                                    (decimal)enumLOAI_HINH_TAI_SAN.VO_HINH };
                if (listTSQL.Contains(model.LOAI_HINH_TAI_SAN_ID ?? 0))
                {
                    model.isTSQL = true;
                }
            }
            //Lấy thông tin khấu hao hiện tại của tài sản
            #region Lấy thông tin khấu hao hiện tại của tài sản
            //Nếu xóa tất cả bản ghi cũ thì dùng
            //TaiSanKhauHao taiSanKhauHao = _taiSanKhauHaoService.GetTaiSanKhauHaoMoiNhatByTaiSanId(model.ID);
            //nếu không
            TaiSanKhauHao taiSanKhauHao = _taiSanKhauHaoService.GetTaiSanKhauHaoIdMaxByTaiSanId(model.ID);
            if (taiSanKhauHao != null)
            {
                model.LoaiTaiSanModel = new LoaiTaiSanModel();
                model.LoaiTaiSanModel.KH_TY_LE = taiSanKhauHao.TY_LE_KHAU_HAO ?? 0;
                model.LoaiTaiSanModel.KH_THOI_HAN_SU_DUNG = taiSanKhauHao.SO_THANG_KHAU_HAO ?? 0;
            }
            #endregion Lấy thông tin khấu hao hiện tại của tài sản
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Edit(TaiSanModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu) || !_workContext.CurrentDonVi.IS_LA_DON_VI_NHAP_LIEU.GetValueOrDefault(false))
                return AccessDeniedView();

            //try to get a store with the specified id
            ValidateListHienTrang(model.nvYeuCauChiTietModel);

            var item = _itemService.GetTaiSanById(model.ID);
            model.NGUOI_TAO_ID = item.NGUOI_TAO_ID;
            if (item == null)
                return RedirectToAction("List");
            if (item.TrangThaiTaiSan == enumTRANG_THAI_TAI_SAN.DA_DUYET || item.TrangThaiTaiSan == enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO)
                return JsonErrorMessage("Tài sản đã được duyệt");

            if (ModelState.IsValid)
            {
                if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO)
                {
                    model.TEN = _taiSanOtoModelFactory.genTenTaiSanOto(model.taisanOtoModel.NHAN_XE_ID, model.taisanOtoModel.DONG_XE_ID, model.taisanOtoModel.BIEN_KIEM_SOAT);
                }
                if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
                    model.MA = _itemModelFactory.LoadMaTaiSan(_workContext.CurrentDonVi.ID, model.ID, model.LOAI_TAI_SAN_DON_VI_ID, model.LOAI_HINH_TAI_SAN_ID);
                else
                    model.MA = _itemModelFactory.LoadMaTaiSan(_workContext.CurrentDonVi.ID, model.ID, model.LOAI_TAI_SAN_ID, model.LOAI_HINH_TAI_SAN_ID);
                _itemModelFactory.PrepareTaiSan(model, item);
                item.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.CHO_DUYET;
                _itemService.UpdateTaiSan(item);
                //edit chi tiet tung loai tai san(TS_TAI_SAN_DAT, TS_TAI_SAN_NHA....)
                //update
                var loaiLyDo = _lydobiendongService.GetLyDoBienDongById(model.LY_DO_BIEN_DONG_ID.Value);
                var yeuCau = new YeuCau();
                if (model.YeuCauId > 0)
                    yeuCau = _yeucauService.GetYeuCauById(model.YeuCauId.Value);
                else
                    yeuCau = _yeucauService.GetYeuCauCuNhatByTSId(model.ID);
                yeuCau.LY_DO_BIEN_DONG_ID = model.LY_DO_BIEN_DONG_ID;
                yeuCau.NGUON_VON_JSON = model.lstNguonVonModel.toStringJson();
                yeuCau = _yeucauModelFactory.PrepareYeuCau(yeuCau, item);
                yeuCau.NGUYEN_GIA = model.nvYeuCauChiTietModel.NGUYEN_GIA;
                var yeuCauChiTiet = _yeucauchitietService.GetYeuCauChiTietByYeuCauId(yeuCau.ID);
                yeuCauChiTiet = _yeuCauChiTietModelFactory.PrepareYeuCauChiTiet(yeuCauChiTiet, model);
                _itemModelFactory.PrepareSaveHMKHTaiSan(yeuCauChiTiet, model);
                switch (item.LOAI_HINH_TAI_SAN_ID)
                {
                    case (int)enumLOAI_HINH_TAI_SAN.DAT:
                        var TsDat = _taisandatService.GetTaiSanDatByTaiSanId(model.ID);
                        _taisandatModelFactory.PrepareTaiSanDat(model.taisandatModel, TsDat);
                        _taisandatService.UpdateTaiSanDat(TsDat);
                        var listNha = _taisannhaService.GetTaiSanNhaByDatId(TsDat.TAI_SAN_ID);
                        if (listNha != null)
                        {
                            //update lại địa chỉ của tài sản nhà được gắn trên đất
                            foreach (var itemNha in listNha)
                            {
                                //itemNha.DIA_CHI = TsDat.DIA_CHI;
                                itemNha.DIA_CHI = model.TEN;
                                _taisannhaService.UpdateTaiSanNha(itemNha);
                            }
                        }
                        //yeuCauChiTiet.DIA_CHI = model.TEN; //địa chỉ đẩy đủ cả tỉnh, huyện, xã
                        yeuCauChiTiet.DIA_CHI = TsDat.DIA_CHI;//địa chỉ nguyên bản chưa xử lý
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.NHA:
                        var TsNha = _taisannhaService.GetTaiSanNhaByTaiSanId(model.ID);
                        _taiSanNhaModelFactory.PrepareTaiSanNha(model.taisannhaModel, TsNha);
                        TsNha.NGAY_SU_DUNG = model.NGAY_SU_DUNG;
                        _taisannhaService.UpdateTaiSanNha(TsNha);
                        yeuCauChiTiet.DIA_CHI = TsNha.DIA_CHI;
                        if ((model.taisannhaModel.TAI_SAN_DAT_ID ?? 0) <= 0)
                        {
                            // lưu địa chỉ đầy đủ của nhà không đất trên ycct.Dia_CHI
                            // địa chỉ nguyên bản lưu trên taisannha, ycct.NHA_DIA_CHI
                            yeuCauChiTiet.DIA_CHI = _taiSanNhaModelFactory.PrepareDiaChiNhaByDiaBan(TsNha.DIA_CHI.Trim(), model.taisannhaModel.DIA_BAN_ID);
                            yeuCauChiTiet.NHA_DIA_CHI = TsNha.DIA_CHI;
                        }
                        // thêm lưu địa chỉ nhà
                        yeuCauChiTiet.DIA_BAN_ID = model.taisannhaModel.DIA_BAN_ID;

                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                    case (int)enumLOAI_HINH_TAI_SAN.OTO:
                        var TsOto = _taisanOtoService.GetTaiSanOtoById(model.ID);
                        _taiSanOtoModelFactory.PrepareTaiSanOto(model.taisanOtoModel, TsOto);
                        _taisanOtoService.UpdateTaiSanOto(TsOto);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                        model.taisanClnModel = new TaiSanClnModel();
                        model.taisanClnModel.TAI_SAN_ID = model.ID;
                        model.taisanClnModel.NAM_SINH = model.NAM_SAN_XUAT;
                        var TsCayLauNam = _taisanClnService.GetTaiSanClnByTaiSanId(model.ID);
                        _taiSanClnModelFactory.PrepareTaiSanCln(model.taisanClnModel, TsCayLauNam);
                        _taisanClnService.UpdateTaiSanCln(TsCayLauNam);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                    case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                        model.taisanmaymocModel.TAI_SAN_ID = model.ID;
                        model.taisanmaymocModel.PHU_KIEN_JSON = model.taisanmaymocModel.ListPhuKienHuuHinh.toStringJson();
                        var TsMayMoc = _taisanmaymocService.GetTaiSanMaymocByTaiSanId(model.ID);
                        _taiSanMayMocModelFactory.PrepareTaiSanMayMoc(model.taisanmaymocModel, TsMayMoc);
                        _taisanmaymocService.UpdateTaiSanMayMoc(TsMayMoc);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                        model.taisanVktModel.TAI_SAN_ID = model.ID;
                        var TsVatKienTruc = _taisanVKTService.GetTaiSanVktByTaiSanId(model.ID);
                        _taiSanVktModelFactory.PrepareTaiSanVkt(model.taisanVktModel, TsVatKienTruc);
                        _taisanVKTService.UpdateTaiSanVkt(TsVatKienTruc);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                        model.taisanvohinhModel.TAI_SAN_ID = model.ID;
                        var taisanvohinh = _taiSanVoHinhService.GetTaiSanVoHinhByTaiSanId(model.ID);
                        _taiSanVoHinhModelFactory.PrepareTaiSanVoHinh(model.taisanvohinhModel, taisanvohinh);
                        _taiSanVoHinhService.UpdateTaiSanVoHinh(taisanvohinh);
                        break;
                }
                //update
                yeuCau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
                yeuCau.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO;
                //hoa hồng
                yeuCau.HOA_HONG_NOP_NSNN = model.HOA_HONG_NOP_NSNN;
                yeuCau.HOA_HONG_DE_LAI_DON_VI = model.HOA_HONG_DE_LAI_DON_VI;
                yeuCau.GHI_CHU = model.GHI_CHU;
                _yeucauService.UpdateYeuCau(yeuCau);

                _yeucauchitietService.UpdateYeuCauChiTiet(yeuCauChiTiet);
                //update tỷ lệ khấu hao tài sản
                //var taisankhauhao = _loaiTaiSanKhauHaoService.GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(model.LOAI_TAI_SAN_ID, model.DON_VI_ID);
                //var ltskh = taisankhauhao.ToModel<LoaiTaiSanKhauHaoModel>();
                //taisankhauhao = _loaiTaiSanKhauHaoModelFactory.PrepareLoaiTaiSanKhauHaoFromTS(ltskh, model);
                //_loaiTaiSanKhauHaoService.UpdateLoaiTaiSanKhauHao(taisankhauhao);
                //Ghi log
                _yeucaunhatkyModelFactory.InsertYeuCauNhatKy(yeuCau.ToModel<YeuCauModel>(), enumLOAI_NHATKY_YEUCAU.SUA);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<TaiSanModel>(), "TaiSan");

                //tự động duyệt tài sản  < 500tr (trừ đất, nhà, ô tô)
                if (_itemModelFactory.CheckAutoDuyet(item.LOAI_HINH_TAI_SAN_ID, item.NGUYEN_GIA_BAN_DAU))
                {
                    _thaoTacBienDongModelFactory.DuyetYeuCauFunc(yeuCau.ID, yeuCau);
                }

                if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT && model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC && model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.OTO && model.SoLuongNhanBan > 1 && model.SoLuongNhanBan < 51)
                {
                    NhanBanTaiSan(item.ID, model.SoLuongNhanBan - 1);
                    return JsonSuccessMessage("Đã tạo " + model.SoLuongNhanBan + " bản ghi thành công!", item.ToModel<TaiSanModel>());
                }
                //Ghi bảng chốt giá trị tăng mới tài sản
                if (model.DON_VI_CHE_DO_HACH_TOAN_ID != (int)enumCHE_DO_HACH_TOAN.KHAU_HAO)
                {
                    _haoMonTaiSanModelFactory.InsertUpdateHaoMonTaiSanModelFromTangMoiTS(yeuCau: yeuCau, yeuCauChiTiet: yeuCauChiTiet, haoMonTaiSanModel: new HaoMonTaiSanModel());
                }
                if (model.DON_VI_CHE_DO_HACH_TOAN_ID != (int)enumCHE_DO_HACH_TOAN.HAO_MON)
                {
                    _khauHaoTaiSanModelFactory.InsertUpdateKhauHaoTaiSanModelFromTangMoiTS(yeuCau: yeuCau, yeuCauChiTiet: yeuCauChiTiet, khauHaoTaiSanModel: new KhauHaoTaiSanModel());
                }
                //update session
                UpdateSessionSearchModel<TaiSanSearchModel>(true);

                return JsonSuccessMessage("Cập nhật dữ liệu thành công !", item.ToModel<TaiSanModel>());
            }
            //prepare model
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return JsonErrorMessage("Error", list);
        }

        public virtual IActionResult EditTaiSanDaDuyetDUoi500tr(decimal TaiSanId, bool continueAddNha = false, int? pageIndex = 1)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu) || !_workContext.CurrentDonVi.IS_LA_DON_VI_NHAP_LIEU.GetValueOrDefault(false))
                return AccessDeniedView();
            var item = _itemService.GetTaiSanById(TaiSanId);

            var model = item.ToModel<TaiSanModel>();
            model = _itemModelFactory.PrepareTaiSanModel(model: model, item: item, isCreateTaiSan: true);
            switch (model.LOAI_HINH_TAI_SAN_ID)
            {
                case (int)enumLOAI_HINH_TAI_SAN.DAT:
                    model.taisandatModel = _taisandatModelFactory.PrepareTaiSanDatModel(model: model.taisandatModel, item: _taisandatService.GetTaiSanDatByTaiSanId(model.ID));
                    model.cohoso = model.taisandatModel.cohoso;
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.NHA:
                    model.taisannhaModel = _taiSanNhaModelFactory.PrepareTaiSanNhaModel(model: model.taisannhaModel, item: _taisannhaService.GetTaiSanNhaByTaiSanId(model.ID));
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    model.taisanVktModel = _taiSanVktModelFactory.PrepareTaiSanVktModel(model: model.taisanVktModel, item: _taisanVKTService.GetTaiSanVktByTaiSanId(model.ID));
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.OTO:
                case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                    model.taisanOtoModel = _taiSanOtoModelFactory.PrepareTaiSanOtoModel(model: model.taisanOtoModel, item: _taisanOtoService.GetTaiSanOtoByTaiSanId(model.ID));
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                    model.taisanClnModel = _taiSanClnModelFactory.PrepareTaiSanClnModel(model: model.taisanClnModel, item: _taisanClnService.GetTaiSanClnByTaiSanId(model.ID));
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                    model.taisanmaymocModel = _taiSanMayMocModelFactory.PrepareTaiSanMayMocModel(model: model.taisanmaymocModel, item: _taisanmaymocService.GetTaiSanMaymocByTaiSanId(model.ID));
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                    model.taisanvohinhModel = _taiSanVoHinhModelFactory.PrepareTaiSanVoHinhModel(model: model.taisanvohinhModel, item: _taiSanVoHinhService.GetTaiSanVoHinhByTaiSanId(model.ID));
                    break;

                default:
                    break;
            }
            model.pageIndex = pageIndex ?? 1;
            if (model.YeuCauId > 0)
                model.YeuCauStatusId = _yeucauService.GetYeuCauById(model.YeuCauId ?? 0).TRANG_THAI_ID;
            model.ContinueAndAddNha = continueAddNha;
            DonVi tsDonViId = _donviService.GetDonViById(model.DON_VI_ID.Value);
            if (model.DU_AN_ID > 0 || (tsDonViId.LA_BAN_QL_DU_AN != null && tsDonViId.LA_BAN_QL_DU_AN.Value))
            {
                model.isCreateTSDA = true;
            }
            else
            {
                model.isCreateTSDA = false;
            }
            if (model.CHE_DO_HACH_TOAN_ID != (int)GS.Core.Domain.DanhMuc.enumCHE_DO_HACH_TOAN.HAO_MON)
            {
                model.nvYeuCauChiTietModel.HMKH_GIA_TRI_CON_LAI = (model.nvYeuCauChiTietModel.HM_GIA_TRI_CON_LAI == null ? 0 : model.nvYeuCauChiTietModel.HM_GIA_TRI_CON_LAI) + (model.nvYeuCauChiTietModel.KH_CON_LAI == null ? 0 : model.nvYeuCauChiTietModel.KH_CON_LAI);
            }
            // do không lưu điều kiện là ts quản lý như tscđ trong db nên phải check nguyên giá ts có >5tr và <10tr không
            //nếu thỏa mã thì coi đó là ts quản lý như tscđ rồi bắt validate
            if (model.nvYeuCauChiTietModel.NGUYEN_GIA >= 5000000 && model.nvYeuCauChiTietModel.NGUYEN_GIA < 10000000)
            {
                var listTSQL = new List<decimal>() { (decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC, (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI,
                                                    (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV, (decimal)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC,
                                                    (decimal)enumLOAI_HINH_TAI_SAN.VO_HINH };
                if (listTSQL.Contains(model.LOAI_HINH_TAI_SAN_ID ?? 0))
                {
                    model.isTSQL = true;
                }
            }
            //Lấy thông tin khấu hao hiện tại của tài sản
            #region Lấy thông tin khấu hao hiện tại của tài sản
            //Nếu xóa tất cả bản ghi cũ thì dùng
            //TaiSanKhauHao taiSanKhauHao = _taiSanKhauHaoService.GetTaiSanKhauHaoMoiNhatByTaiSanId(model.ID);
            //nếu không
            TaiSanKhauHao taiSanKhauHao = _taiSanKhauHaoService.GetTaiSanKhauHaoIdMaxByTaiSanId(model.ID);
            if (taiSanKhauHao != null)
            {
                model.LoaiTaiSanModel = new LoaiTaiSanModel();
                model.LoaiTaiSanModel.KH_TY_LE = taiSanKhauHao.TY_LE_KHAU_HAO;
                model.LoaiTaiSanModel.KH_THOI_HAN_SU_DUNG = taiSanKhauHao.SO_THANG_KHAU_HAO;
            }
            #endregion Lấy thông tin khấu hao hiện tại của tài sản
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult EditTaiSanDaDuyetDuoi500tr(TaiSanModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu) || !_workContext.CurrentDonVi.IS_LA_DON_VI_NHAP_LIEU.GetValueOrDefault(false))
                return AccessDeniedView();

            //try to get a store with the specified id
            ValidateListHienTrang(model.nvYeuCauChiTietModel);

            var item = _itemService.GetTaiSanById(model.ID);
            model.NGUOI_TAO_ID = item.NGUOI_TAO_ID;
            if (item == null)
                return RedirectToAction("List");
            if (item.TrangThaiTaiSan == enumTRANG_THAI_TAI_SAN.DA_DUYET || item.TrangThaiTaiSan == enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO)
                return JsonErrorMessage("Tài sản đã được duyệt");

            if (ModelState.IsValid)
            {
                if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO)
                {
                    model.TEN = _taiSanOtoModelFactory.genTenTaiSanOto(model.taisanOtoModel.NHAN_XE_ID, model.taisanOtoModel.DONG_XE_ID, model.taisanOtoModel.BIEN_KIEM_SOAT);
                }
                if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
                    model.MA = _itemModelFactory.LoadMaTaiSan(_workContext.CurrentDonVi.ID, model.ID, model.LOAI_TAI_SAN_DON_VI_ID, model.LOAI_HINH_TAI_SAN_ID);
                else
                    model.MA = _itemModelFactory.LoadMaTaiSan(_workContext.CurrentDonVi.ID, model.ID, model.LOAI_TAI_SAN_ID, model.LOAI_HINH_TAI_SAN_ID);
                _itemModelFactory.PrepareTaiSan(model, item);
                item.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.CHO_DUYET;
                _itemService.UpdateTaiSan(item);
                //edit chi tiet tung loai tai san(TS_TAI_SAN_DAT, TS_TAI_SAN_NHA....)
                //update
                var loaiLyDo = _lydobiendongService.GetLyDoBienDongById(model.LY_DO_BIEN_DONG_ID.Value);
                var yeuCau = new YeuCau();
                if (model.YeuCauId > 0)
                    yeuCau = _yeucauService.GetYeuCauById(model.YeuCauId.Value);
                else
                    yeuCau = _yeucauService.GetYeuCauCuNhatByTSId(model.ID);
                yeuCau.LY_DO_BIEN_DONG_ID = model.LY_DO_BIEN_DONG_ID;
                yeuCau.NGUON_VON_JSON = model.lstNguonVonModel.toStringJson();
                yeuCau = _yeucauModelFactory.PrepareYeuCau(yeuCau, item);
                yeuCau.NGUYEN_GIA = model.nvYeuCauChiTietModel.NGUYEN_GIA;
                var yeuCauChiTiet = _yeucauchitietService.GetYeuCauChiTietByYeuCauId(yeuCau.ID);
                yeuCauChiTiet = _yeuCauChiTietModelFactory.PrepareYeuCauChiTiet(yeuCauChiTiet, model);
                _itemModelFactory.PrepareSaveHMKHTaiSan(yeuCauChiTiet, model);
                switch (item.LOAI_HINH_TAI_SAN_ID)
                {
                    case (int)enumLOAI_HINH_TAI_SAN.DAT:
                        var TsDat = _taisandatService.GetTaiSanDatByTaiSanId(model.ID);
                        _taisandatModelFactory.PrepareTaiSanDat(model.taisandatModel, TsDat);
                        _taisandatService.UpdateTaiSanDat(TsDat);
                        var listNha = _taisannhaService.GetTaiSanNhaByDatId(TsDat.TAI_SAN_ID);
                        if (listNha != null)
                        {
                            //update lại địa chỉ của tài sản nhà được gắn trên đất
                            foreach (var itemNha in listNha)
                            {
                                //itemNha.DIA_CHI = TsDat.DIA_CHI;
                                itemNha.DIA_CHI = model.TEN;
                                _taisannhaService.UpdateTaiSanNha(itemNha);
                            }
                        }
                        //yeuCauChiTiet.DIA_CHI = model.TEN; //địa chỉ đẩy đủ cả tỉnh, huyện, xã
                        yeuCauChiTiet.DIA_CHI = TsDat.DIA_CHI;//địa chỉ nguyên bản chưa xử lý
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.NHA:
                        var TsNha = _taisannhaService.GetTaiSanNhaByTaiSanId(model.ID);
                        _taiSanNhaModelFactory.PrepareTaiSanNha(model.taisannhaModel, TsNha);
                        TsNha.NGAY_SU_DUNG = model.NGAY_SU_DUNG;
                        _taisannhaService.UpdateTaiSanNha(TsNha);
                        yeuCauChiTiet.DIA_CHI = TsNha.DIA_CHI;
                        if ((model.taisannhaModel.TAI_SAN_DAT_ID ?? 0) <= 0)
                        {
                            // lưu địa chỉ đầy đủ của nhà không đất trên ycct.Dia_CHI
                            // địa chỉ nguyên bản lưu trên taisannha, ycct.NHA_DIA_CHI
                            yeuCauChiTiet.DIA_CHI = _taiSanNhaModelFactory.PrepareDiaChiNhaByDiaBan(TsNha.DIA_CHI.Trim(), model.taisannhaModel.DIA_BAN_ID);
                            yeuCauChiTiet.NHA_DIA_CHI = TsNha.DIA_CHI;
                        }
                        // thêm lưu địa chỉ nhà
                        yeuCauChiTiet.DIA_BAN_ID = model.taisannhaModel.DIA_BAN_ID;

                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                    case (int)enumLOAI_HINH_TAI_SAN.OTO:
                        var TsOto = _taisanOtoService.GetTaiSanOtoById(model.ID);
                        _taiSanOtoModelFactory.PrepareTaiSanOto(model.taisanOtoModel, TsOto);
                        _taisanOtoService.UpdateTaiSanOto(TsOto);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                        model.taisanClnModel = new TaiSanClnModel();
                        model.taisanClnModel.TAI_SAN_ID = model.ID;
                        model.taisanClnModel.NAM_SINH = model.NAM_SAN_XUAT;
                        var TsCayLauNam = _taisanClnService.GetTaiSanClnByTaiSanId(model.ID);
                        _taiSanClnModelFactory.PrepareTaiSanCln(model.taisanClnModel, TsCayLauNam);
                        _taisanClnService.UpdateTaiSanCln(TsCayLauNam);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                    case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                        model.taisanmaymocModel.TAI_SAN_ID = model.ID;
                        model.taisanmaymocModel.PHU_KIEN_JSON = model.taisanmaymocModel.ListPhuKienHuuHinh.toStringJson();
                        var TsMayMoc = _taisanmaymocService.GetTaiSanMaymocByTaiSanId(model.ID);
                        _taiSanMayMocModelFactory.PrepareTaiSanMayMoc(model.taisanmaymocModel, TsMayMoc);
                        _taisanmaymocService.UpdateTaiSanMayMoc(TsMayMoc);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                        model.taisanVktModel.TAI_SAN_ID = model.ID;
                        var TsVatKienTruc = _taisanVKTService.GetTaiSanVktByTaiSanId(model.ID);
                        _taiSanVktModelFactory.PrepareTaiSanVkt(model.taisanVktModel, TsVatKienTruc);
                        _taisanVKTService.UpdateTaiSanVkt(TsVatKienTruc);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                        model.taisanvohinhModel.TAI_SAN_ID = model.ID;
                        var taisanvohinh = _taiSanVoHinhService.GetTaiSanVoHinhByTaiSanId(model.ID);
                        _taiSanVoHinhModelFactory.PrepareTaiSanVoHinh(model.taisanvohinhModel, taisanvohinh);
                        _taiSanVoHinhService.UpdateTaiSanVoHinh(taisanvohinh);
                        break;
                }
                //update
                yeuCau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
                yeuCau.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO;
                //hoa hồng
                yeuCau.HOA_HONG_NOP_NSNN = model.HOA_HONG_NOP_NSNN;
                yeuCau.HOA_HONG_DE_LAI_DON_VI = model.HOA_HONG_DE_LAI_DON_VI;
                yeuCau.GHI_CHU = model.GHI_CHU;
                _yeucauService.UpdateYeuCau(yeuCau);

                _yeucauchitietService.UpdateYeuCauChiTiet(yeuCauChiTiet);
                //update tỷ lệ khấu hao tài sản
                //var taisankhauhao = _loaiTaiSanKhauHaoService.GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(model.LOAI_TAI_SAN_ID, model.DON_VI_ID);
                //var ltskh = taisankhauhao.ToModel<LoaiTaiSanKhauHaoModel>();
                //taisankhauhao = _loaiTaiSanKhauHaoModelFactory.PrepareLoaiTaiSanKhauHaoFromTS(ltskh, model);
                //_loaiTaiSanKhauHaoService.UpdateLoaiTaiSanKhauHao(taisankhauhao);
                //Ghi log
                _yeucaunhatkyModelFactory.InsertYeuCauNhatKy(yeuCau.ToModel<YeuCauModel>(), enumLOAI_NHATKY_YEUCAU.SUA);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<TaiSanModel>(), "TaiSan");

                //tự động duyệt tài sản  < 500tr (trừ đất, nhà, ô tô)
                if (_itemModelFactory.CheckAutoDuyet(item.LOAI_HINH_TAI_SAN_ID, item.NGUYEN_GIA_BAN_DAU))
                {
                    _thaoTacBienDongModelFactory.DuyetYeuCauFunc(yeuCau.ID, yeuCau);
                }

                if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT && model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC && model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.OTO && model.SoLuongNhanBan > 1 && model.SoLuongNhanBan < 51)
                {
                    NhanBanTaiSan(item.ID, model.SoLuongNhanBan - 1);
                    return JsonSuccessMessage("Đã tạo " + model.SoLuongNhanBan + " bản ghi thành công!", item.ToModel<TaiSanModel>());
                }
                //Ghi bảng chốt giá trị tăng mới tài sản
                if (model.DON_VI_CHE_DO_HACH_TOAN_ID != (int)enumCHE_DO_HACH_TOAN.KHAU_HAO)
                {
                    _haoMonTaiSanModelFactory.InsertUpdateHaoMonTaiSanModelFromTangMoiTS(yeuCau: yeuCau, yeuCauChiTiet: yeuCauChiTiet, haoMonTaiSanModel: new HaoMonTaiSanModel());
                }
                if (model.DON_VI_CHE_DO_HACH_TOAN_ID != (int)enumCHE_DO_HACH_TOAN.HAO_MON)
                {
                    _khauHaoTaiSanModelFactory.InsertUpdateKhauHaoTaiSanModelFromTangMoiTS(yeuCau: yeuCau, yeuCauChiTiet: yeuCauChiTiet, khauHaoTaiSanModel: new KhauHaoTaiSanModel());
                }
                //update session
                UpdateSessionSearchModel<TaiSanSearchModel>(true);

                return JsonSuccessMessage("Cập nhật dữ liệu thành công !", item.ToModel<TaiSanModel>());
            }
            //prepare model
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return JsonErrorMessage("Error", list);
        }

        private void ValidateListHienTrang(YeuCauChiTietModel model)
        {
            if (model.lstHienTrang != null && model.lstHienTrang.Count() > 0)
            {
                foreach (var hienTrang in model.lstHienTrang)
                {
                    if (hienTrang != null)
                    {
                        // nếu mà hiện trạng không đúng với loại hình đơn vị nhưng có giá trị thì báo validate
                        if (_hienTrangModelFactory.CheckIsHienTrangNhapSai(_workContext.CurrentDonVi.ID, hienTrang))
                        {
                            ModelState.AddModelError($"HienTrang_{hienTrang.HienTrangId}", "Hiện trạng không đúng với loại hình đơn vị");
                        }
                    }
                }
            }
        }

        [HttpPost]
        public virtual IActionResult LuuVaDuyet(TaiSanModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu) || !_workContext.CurrentDonVi.IS_LA_DON_VI_NHAP_LIEU.GetValueOrDefault(false))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanById(model.ID);
            model.NGUOI_TAO_ID = item.NGUOI_TAO_ID;
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                model.MA = _itemModelFactory.LoadMaTaiSan(_workContext.CurrentDonVi.ID, model.ID, model.LOAI_TAI_SAN_ID, model.LOAI_HINH_TAI_SAN_ID);
                _itemModelFactory.PrepareTaiSan(model, item);
                item.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.CHO_DUYET;
                _itemService.UpdateTaiSan(item);
                //edit chi tiet tung loai tai san(TS_TAI_SAN_DAT, TS_TAI_SAN_NHA....)
                //update
                var loaiLyDo = _lydobiendongService.GetLyDoBienDongById(model.LY_DO_BIEN_DONG_ID.Value);
                var yeuCau = new YeuCau();
                if (model.YeuCauId > 0)
                    yeuCau = _yeucauService.GetYeuCauById(model.YeuCauId.Value);
                else
                    yeuCau = _yeucauService.GetYeuCauCuNhatByTSId(model.ID);
                yeuCau.LY_DO_BIEN_DONG_ID = model.LY_DO_BIEN_DONG_ID;
                yeuCau.NGUON_VON_JSON = model.lstNguonVonModel.toStringJson();
                yeuCau = _yeucauModelFactory.PrepareYeuCau(yeuCau, item);
                yeuCau.NGUYEN_GIA = model.nvYeuCauChiTietModel.NGUYEN_GIA;

                var yeuCauChiTiet = _yeucauchitietService.GetYeuCauChiTietByYeuCauId(yeuCau.ID);
                yeuCauChiTiet = _yeuCauChiTietModelFactory.PrepareYeuCauChiTiet(yeuCauChiTiet, model);
                switch (item.LOAI_HINH_TAI_SAN_ID)
                {
                    case (int)enumLOAI_HINH_TAI_SAN.DAT:
                        var TsDat = _taisandatService.GetTaiSanDatByTaiSanId(model.ID);
                        _taisandatModelFactory.PrepareTaiSanDat(model.taisandatModel, TsDat);
                        _taisandatService.UpdateTaiSanDat(TsDat);
                        var listNha = _taisannhaService.GetTaiSanNhaByDatId(TsDat.TAI_SAN_ID);
                        if (listNha != null)
                        {
                            //update lại địa chỉ của tài sản nhà được gắn trên đất
                            foreach (var itemNha in listNha)
                            {
                                itemNha.DIA_CHI = TsDat.DIA_CHI;
                                _taisannhaService.UpdateTaiSanNha(itemNha);
                            }
                        }
                        yeuCauChiTiet.DIA_CHI = TsDat.DIA_CHI;
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.NHA:
                        var TsNha = _taisannhaService.GetTaiSanNhaByTaiSanId(model.ID);
                        _taiSanNhaModelFactory.PrepareTaiSanNha(model.taisannhaModel, TsNha);
                        TsNha.NGAY_SU_DUNG = model.NGAY_SU_DUNG;
                        _taisannhaService.UpdateTaiSanNha(TsNha);
                        yeuCauChiTiet.DIA_CHI = TsNha.DIA_CHI;
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                    case (int)enumLOAI_HINH_TAI_SAN.OTO:
                        var TsOto = _taisanOtoService.GetTaiSanOtoById(model.ID);
                        _taiSanOtoModelFactory.PrepareTaiSanOto(model.taisanOtoModel, TsOto);
                        _taisanOtoService.UpdateTaiSanOto(TsOto);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                        model.taisanClnModel = new TaiSanClnModel();
                        model.taisanClnModel.TAI_SAN_ID = model.ID;
                        model.taisanClnModel.NAM_SINH = model.NAM_SAN_XUAT;
                        var TsCayLauNam = _taisanClnService.GetTaiSanClnByTaiSanId(model.ID);
                        _taiSanClnModelFactory.PrepareTaiSanCln(model.taisanClnModel, TsCayLauNam);
                        _taisanClnService.UpdateTaiSanCln(TsCayLauNam);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                    case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                        model.taisanmaymocModel.TAI_SAN_ID = model.ID;
                        model.taisanmaymocModel.PHU_KIEN_JSON = model.taisanmaymocModel.ListPhuKienHuuHinh.toStringJson();
                        var TsMayMoc = _taisanmaymocService.GetTaiSanMaymocByTaiSanId(model.ID);
                        _taiSanMayMocModelFactory.PrepareTaiSanMayMoc(model.taisanmaymocModel, TsMayMoc);
                        _taisanmaymocService.UpdateTaiSanMayMoc(TsMayMoc);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                        model.taisanVktModel.TAI_SAN_ID = model.ID;
                        var TsVatKienTruc = _taisanVKTService.GetTaiSanVktByTaiSanId(model.ID);
                        _taiSanVktModelFactory.PrepareTaiSanVkt(model.taisanVktModel, TsVatKienTruc);
                        _taisanVKTService.UpdateTaiSanVkt(TsVatKienTruc);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                        model.taisanvohinhModel.TAI_SAN_ID = model.ID;
                        var taisanvohinh = _taiSanVoHinhService.GetTaiSanVoHinhByTaiSanId(model.ID);
                        _taiSanVoHinhModelFactory.PrepareTaiSanVoHinh(model.taisanvohinhModel, taisanvohinh);
                        _taiSanVoHinhService.UpdateTaiSanVoHinh(taisanvohinh);
                        break;
                }
                //update
                yeuCau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
                yeuCau.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO;
                //hoa hồng
                yeuCau.HOA_HONG_NOP_NSNN = model.HOA_HONG_NOP_NSNN;
                yeuCau.HOA_HONG_DE_LAI_DON_VI = model.HOA_HONG_DE_LAI_DON_VI;
                yeuCau.GHI_CHU = model.GHI_CHU;
                _yeucauService.UpdateYeuCau(yeuCau);

                _yeucauchitietService.UpdateYeuCauChiTiet(yeuCauChiTiet);
                //Ghi log
                _yeucaunhatkyModelFactory.InsertYeuCauNhatKy(yeuCau.ToModel<YeuCauModel>(), enumLOAI_NHATKY_YEUCAU.SUA);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<TaiSanModel>(), "TaiSan");
                //tự động duyệt
                if (_itemModelFactory.CheckAutoDuyet(item.LOAI_HINH_TAI_SAN_ID, item.NGUYEN_GIA_BAN_DAU))
                    _thaoTacBienDongModelFactory.DuyetYeuCauFunc(yeuCau.ID, yeuCau);

                if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT && model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC && model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.OTO && model.SoLuongNhanBan > 1 && model.SoLuongNhanBan < 51)
                {
                    NhanBanTaiSan(item.ID, model.SoLuongNhanBan - 1);
                    return JsonSuccessMessage("Đã tạo " + model.SoLuongNhanBan + " bản ghi thành công!", item.ToModel<TaiSanModel>());
                }
                //Ghi bảng chốt giá trị tăng mới tài sản
                _haoMonTaiSanModelFactory.InsertUpdateHaoMonTaiSanModelFromTangMoiTS(yeuCau: yeuCau, yeuCauChiTiet: yeuCauChiTiet, haoMonTaiSanModel: new HaoMonTaiSanModel());
                _thaoTacBienDongModelFactory.DuyetYeuCauFunc(yeuCau.ID);

                return JsonSuccessMessage("Cập nhật dữ liệu thành công !", item.ToModel<TaiSanModel>());
            }
            //prepare model
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return JsonErrorMessage("Error", list);
        }

        [HttpGet]
        public virtual IActionResult GetThongTinLoaiTaiSan(decimal loaiTaiSanId, decimal? loaiHinhTaiSanId = 0)
        {
            var loaiTaiSanModel = new LoaiTaiSanModel();
            if (loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.VO_HINH || loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
            {
                var lts = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(loaiTaiSanId);
                if (lts != null)
                    loaiTaiSanModel = lts.ToModel<LoaiTaiSanModel>();
                loaiTaiSanModel.KH_TyLe = (loaiTaiSanModel.KH_TY_LE == null ? 0 : loaiTaiSanModel.KH_TY_LE).ToString() + "%";
            }
            else
            {
                var lts = _loaitaisanService.GetLoaiTaiSanById(loaiTaiSanId);
                if (lts != null)
                    loaiTaiSanModel = lts.ToModel<LoaiTaiSanModel>();
                var taiSanKhauHao = _loaiTaiSanKhauHaoService.GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(loaiTaiSanId: loaiTaiSanId, donViId: _workContext.CurrentDonVi.ID);
                if (taiSanKhauHao != null)
                {
                    loaiTaiSanModel.KH_TyLe = (taiSanKhauHao.TY_LE_KHAU_HAO == null ? 0 : taiSanKhauHao.TY_LE_KHAU_HAO).ToString() + "%";
                    loaiTaiSanModel.KH_TY_LE = taiSanKhauHao.TY_LE_KHAU_HAO ?? 0;
                    loaiTaiSanModel.KH_THOI_HAN_SU_DUNG = taiSanKhauHao.THOI_HAN_SU_DUNG ?? 0;
                }
            }
            loaiTaiSanModel.HM_TyLe = (loaiTaiSanModel.HM_TY_LE == null ? 0 : loaiTaiSanModel.HM_TY_LE).ToString() + "%";
            if (loaiTaiSanModel.KH_TY_LE == null)
                loaiTaiSanModel.KH_TY_LE = 0;
            return JsonSuccessMessage("success", loaiTaiSanModel);
        }

        [HttpGet]
        public virtual IActionResult DeleteTaiSan(decimal id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedView();
            ////try to get a store with the specified guid
            var item = _itemService.GetTaiSanById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                if (item.TRANG_THAI_ID != (int)enumTRANG_THAI_TAI_SAN.DA_DUYET || item.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO)
                {
                    var listYeuCau = _yeucauService.GetYeuCaus(item.ID);
                    bool checkQuyenXoa = true;
                    foreach (var itemYC in listYeuCau)
                    {
                        if (itemYC.TRANG_THAI_ID == (int)enumTRANG_THAI_YEU_CAU.DA_DUYET)
                        {
                            checkQuyenXoa = false;
                        }
                    }
                    if (item.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                    {
                        var listTaiSanNha = _taisannhaService.GetTaiSanNhaByDatId(item.ID);
                        if (listTaiSanNha != null && listTaiSanNha.Count > 0)
                        {
                            checkQuyenXoa = false;
                            return JsonErrorMessage("Có tài sản nhà trên tài sản này.");
                        }
                    }
                    if (checkQuyenXoa)
                    {
                        item.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.XOA;
                        item.NGAY_CAP_NHAT = DateTime.Now;
                        _itemService.UpdateTaiSan(item);
                        foreach (var itemYC in listYeuCau)
                        {
                            itemYC.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.XOA;
                            _yeucauService.UpdateYeuCau(itemYC);
                        }
                        // update session
                        var SearchModel = HttpContext.Session.GetObject<TaiSanSearchModel>(enumTENCAUHINH.KeySearch + typeof(TaiSanSearchModel).Name);
                        SearchModel.IsLoadSession = true;
                        HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch + typeof(TaiSanSearchModel).Name, SearchModel);
                        return JsonSuccessMessage("Đã xóa tài sản");
                    }
                    else
                        return JsonErrorMessage("Tài sản này không được xóa");
                }
                else
                    return JsonErrorMessage("Tài sản này không được xóa");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Error", "Có lỗi xảy ra trong quá trình xóa.");
            }
        }

        [HttpPost]
        public virtual IActionResult LoadMaTaiSan(decimal? DonViId = 0, decimal? TaiSanId = 0, decimal? LoaiTaiSanId = 0, decimal? loaiHinhTaiSanId = 0)
        {
            if (LoaiTaiSanId == 0 || LoaiTaiSanId == null)
                return Json("");
            var donVi = _donviService.GetDonViById(DonViId ?? 0);
            var loaiTS = new LoaiTaiSanModel();
            if (loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
            {
                // get tài sản vô hình  gốc
                decimal? parentId = LoaiTaiSanId;
                decimal? tree_level = 0;
                LoaiTaiSanDonVi taiSanVoHinh = new LoaiTaiSanDonVi();
                do
                {
                    if (parentId == null)
                        break;
                    taiSanVoHinh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(parentId.Value);
                    tree_level = taiSanVoHinh.TREE_LEVEL;
                    parentId = taiSanVoHinh.PARENT_ID;
                } while (tree_level > 2);
                //var LoaiTaiSanVoHinhCha = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMa()
                loaiTS = taiSanVoHinh.ToModel<LoaiTaiSanModel>();
            }
            else
                loaiTS = _loaitaisanService.GetLoaiTaiSanById(LoaiTaiSanId ?? 0).ToModel<LoaiTaiSanModel>();
            var MaTs = "";

            if (donVi != null && loaiTS != null)
            {
                MaTs = CommonHelper.GenMaTaiSan(donVi.MA, loaiTS.MA, TaiSanId ?? 0);
            }
            return Json(MaTs);
        }

        [HttpPost]
        public virtual IActionResult GetLoaiTaiSans(DateTime NgayNhap, decimal loaiHinhTSId)
        {
            if (loaiHinhTSId == (int)enumLOAI_HINH_TAI_SAN.VO_HINH || loaiHinhTSId == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
            {
                var donViId = _workContext.CurrentDonVi.ID;
                var selectList = _loaiTaiSanVoHinhModelFactory.PrepareSelectListLoaiTaiSanDonVi(isAddFirst: true, loaiHinhTaiSanId: loaiHinhTSId, donViId: donViId);
                return Json(selectList);
            }
            else
            {
                var selectList = _loaitaisanModelFactory.PrepareSelectListLoaiTaiSan(isAddFirst: true, loaiHinhTaiSanId: loaiHinhTSId);
                return Json(selectList);
            }
        }

        [HttpPost]
        public virtual IActionResult GetLyDoBienDongs(decimal IdLoaiTs)
        {
            var LoaiTS = _loaitaisanService.GetLoaiTaiSanById(IdLoaiTs);
            var selectList = _lydobiendongModelFactory.PrepareSelectListLyDoBienDong(LoaiHinhTaiSanId: LoaiTS.LOAI_HINH_TAI_SAN_ID, isAddFirst: true, strFirstRow: "-- Chọn lý do tăng --");
            return Json(selectList);
        }

        [HttpPost]
        public virtual IActionResult GetLoaiHinhTaiSanId(decimal IdLoaiTs)
        {
            var LoaiTS = _loaitaisanService.GetLoaiTaiSanById(IdLoaiTs);
            return JsonSuccessMessage("success", LoaiTS.LOAI_HINH_TAI_SAN_ID);
        }

        public virtual IActionResult _NguonVon(decimal NguonVonId, decimal? TaiSanId = 0)
        {
            var NguonVon = _nguonvonService.GetNguonVonById(NguonVonId);
            var model = NguonVon.ToModel<NguonVonModel>();
            if (TaiSanId > 0)
            {
                var taisan = _itemService.GetTaiSanById((decimal)TaiSanId);
                model.LoaiHinhTaiSanId = taisan.LOAI_HINH_TAI_SAN_ID;
                var TaiSanNguonVon = _taisannguonvonService.GetTaiSanNguonVonByTaiSanIdFirst(TaiSanId: (decimal)TaiSanId, NguonVonId: NguonVonId);
                model.GiaTri = TaiSanNguonVon.GIA_TRI;
            }
            return PartialView(model);
        }

        /// <summary>
        /// Description: View thong tin tai san
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public virtual IActionResult _ViewThongTinTaiSan(Guid guid, bool? isTraCuu = false, decimal? id = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTaiSan))
                return AccessDeniedView();
            TaiSan item = new TaiSan();
            if (id > 0)
                item = _itemService.GetTaiSanById(id.Value);
            else
                item = _itemService.GetTaiSanByGuId(guid);
            var model = _itemModelFactory.PrepareTaiSanModelView(new TaiSanModel(), item);
            if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
            {
                var loaiTSVH = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(item.LOAI_TAI_SAN_DON_VI_ID ?? 0);
                if (loaiTSVH != null)
                {
                    var loaiTSCha = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(loaiTSVH.PARENT_ID ?? 0);
                    model.TenLoaiTaiSanCha = loaiTSCha != null ? loaiTSCha.TEN : null;
                }
            }
            else if (item.loaitaisan != null)
            {
                var loaiTSCha = _loaitaisanService.GetLoaiTaiSanById(item.loaitaisan.PARENT_ID ?? 0);
                model.TenLoaiTaiSanCha = loaiTSCha != null ? loaiTSCha.TEN : null;
            }
            model.isTraCuu = isTraCuu;
            return PartialView(model);
        }

        #endregion Methods

        #region TaiSanDat

        public virtual IActionResult _TaiSanDat(decimal taiSanId, bool isEdit = false)
        {
            var item = _taisandatService.GetTaiSanDatByTaiSanId(taiSanId);
            var model = _taisandatModelFactory.PrepareTaiSanDatModel(new TaiSanDatModel(), item);
            return PartialView(model);
        }

        //Get DataSource Tinh, Huyen, Xa
        [HttpPost]
        public virtual IActionResult GetDataSource(decimal QuocGiaId, decimal? ParentId = 0, decimal? CapDiaBan = 0, string str = null)
        {
            var selectList = _diabanModelFactory.PrepareDiaBanAvailabele(QuocGiaId: QuocGiaId, ParentId: ParentId, CapDiaBan: CapDiaBan, IsListChaCon: false, IsAddFirst: true, strFirstRow: str);
            return Json(selectList);
        }

        #endregion TaiSanDat

        #region TaiSanOto

        [HttpPost]
        public virtual IActionResult CheckDinhMucOto(KiemTraDinhMucModel request)
        {
            KetQuaDinhMuc ketQua = _dinhMucModelFactory.CheckDinhMucOto(ChucDanhId: request.ChucDanhId, LoaiTaiSanId: request.LoaiTaiSanId, DonViId: _workContext.CurrentDonVi.ID, NgayGhiTang: request.NgayGhiTang, NguyenGia: request.NguyenGia);
            return JsonSuccessMessage(objdata: ketQua);
            //var count = _taisanOtoService.CountOtoByChucDanh(chucDanhId, _workContext.CurrentDonVi.ID);
            //if (count > 0)
            //    return JsonErrorMessage("Cảnh báo: Chức danh đã có ô tô");
            //return JsonSuccessMessage();
        }

        public virtual IActionResult CheckChucDanhOto(decimal chucDanhId)
        {
            //KetQuaDinhMuc ketQua = _dinhMucModelFactory.CheckDinhMucOto(ChucDanhId: request.ChucDanhId, LoaiTaiSanId: request.LoaiTaiSanId, DonViId: _workContext.CurrentDonVi.ID, NgayGhiTang: request.NgayGhiTang, NguyenGia: request.NguyenGia);
            //return JsonSuccessMessage(objdata: ketQua);
            var count = _taisanOtoService.CountOtoByChucDanh(chucDanhId, _workContext.CurrentDonVi.ID);
            if (count > 0)
                return JsonErrorMessage("Cảnh báo: Chức danh đã có ô tô");
            return JsonSuccessMessage();
        }

        public virtual IActionResult CheckListChucDanhOto(int[] chucDanhIds)
        {
            decimal count = 0;
            string message = "";

            if (chucDanhIds != null && chucDanhIds.Count() > 0)
            {
                foreach (var chucDanhId in chucDanhIds)
                {
                    var countOto = _taisanOtoService.CountOtoByChucDanh(chucDanhId, _workContext.CurrentDonVi.ID);
                    if (countOto > 0)
                    {
                        var TenChucDanh = _chucDanhService.GetChucDanhById(chucDanhId)?.TEN_CHUC_DANH;
                        count = count + 1;
                        message = message + TenChucDanh + ",";
                    }
                }
            }
            if (count > 0)
            {
                message = message.Remove(message.Length - 1);
                return JsonErrorMessage($"Cảnh báo: Chức danh {message} đã có ô tô");
            }

            return JsonSuccessMessage();
        }

        public virtual IActionResult GetListChucDanhKhac(decimal? chucDanhId)
        {
            try
            {
                var listChucDanh = _chucDanhModelFactory.PrepareSelectListChucDanh(isAddFirst: true, valueExcuted: chucDanhId, isMultiple: true, IsPhanCap: false);
                return JsonSuccessMessage(objdata: listChucDanh);
            }
            catch (Exception e)
            {
                return JsonErrorMessage(e.Message);
            }
        }

        #endregion TaiSanOto
        #region _TaiSanMayMocThietBi

        public virtual IActionResult _AddPhuKienTSHuuHinh()
        {
            var model = new TaiSanPhuKienHuuHinh();
            var _newMa = Guid.NewGuid();
            model.PHU_KIEN_MA = _newMa.ToString();
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult _AddPhuKienTSHuuHinh(TaiSanPhuKienHuuHinh model)
        {
            return PartialView(model);
        }

        #endregion _TaiSanMayMocThietBi

        #region Tài sản Vật kiến trúc

        #endregion Tài sản Vật kiến trúc

        #region Tài sản cây lâu năm

        #endregion Tài sản cây lâu năm

        #region TaiSanNha

        /// <summary>
        /// Description: Popup tạo nhà ở trên view đất
        /// </summary>
        /// <param name="taiSanDatID"></param>
        /// <param name="taiSanNhaId"></param>
        /// <param name="diaChiDat"></param>
        /// <param name="loailydobiendongId"></param>
        /// <returns></returns>
        public virtual IActionResult _TaiSanNhaTheoDat(decimal taiSanDatID, decimal? taiSanNhaId = 0, string diaChiDat = "", decimal? loailydobiendongId = 0, bool? isTangMoi = null, bool? isCreateTSDA = false)
        {
            var model = new TaiSanModel();
            model.isTangMoi = isTangMoi ?? true;

            if (taiSanNhaId > 0)
            {
                var TsNha = _taisannhaService.GetTaiSanNhaByTaiSanId(taiSanNhaId.Value);
                var Ts = _itemService.GetTaiSanById(TsNha.TAI_SAN_ID);
                model = Ts.ToModel<TaiSanModel>();
                model = _itemModelFactory.PrepareTaiSanModel(model, Ts);
                //model.taisannhaModel = TsNha.ToModel<TaiSanNhaModel>();
                model.taisannhaModel = _taiSanNhaModelFactory.PrepareTaiSanNhaModel(model.taisannhaModel, TsNha);
                model.cohoso = model.taisannhaModel.TaiSanModel.cohoso;
                if (model.NGAY_NHAP.HasValue && model.NGAY_NHAP < new DateTime(2018, 01, 01))
                    model.isTangMoi = false;
                else
                    model.isTangMoi = true;
            }
            else
            {
                TaiSan taiSanDat = _itemService.GetTaiSanById(taiSanDatID);
                if (taiSanDat != null && taiSanDat.DU_AN_ID > 0)
                {
                    model.isCreateTSDA = true;
                    isCreateTSDA = true;
                }
                else
                {
                    model.isCreateTSDA = false;
                    isCreateTSDA = false;
                }
                model.LOAI_HINH_TAI_SAN_ID = (int)enumLOAI_HINH_TAI_SAN.NHA;
                model = _itemModelFactory.PrepareTaiSanModel(model, null);
                //model.NGAY_NHAP = DateTime.Now;
                model.taisannhaModel = _taiSanNhaModelFactory.PrepareTaiSanNhaModel(model: new TaiSanNhaModel(), item: null, isCreateTSDA: isCreateTSDA ?? false);
                model.nvYeuCauChiTietModel = new YeuCauChiTietModel();
                model.nvYeuCauChiTietModel.PhuongPhapTinhKhauHaoAvailable = model.nvYeuCauChiTietModel.enumPhuongPhapTinhKhauHao.ToSelectList();
                model.taisannhaModel.NHA_DIA_CHI = diaChiDat;
                model.taisannhaModel.TAI_SAN_DAT_ID = taiSanDatID;
                model.DON_VI_ID = taiSanDat.DON_VI_ID;
                model.DU_AN_ID = taiSanDat.DU_AN_ID;
                model.LY_DO_BIEN_DONG_ID = taiSanDat?.LY_DO_BIEN_DONG_ID;
                model.NGAY_NHAP = taiSanDat?.NGAY_NHAP;
                //if (model.NGAY_NHAP.HasValue)
                //{
                //    var CheDoId = _chedohaomonService.GetCheDoHaoMonByNgayNhap(model.NGAY_NHAP.GetValueOrDefault()).ID;
                //    model.LoaiTaiSanAvailable = _loaitaisanModelFactory.PrepareSelectListLoaiTaiSan(isAddFirst: true, loaiHinhTaiSanId: model.LOAI_HINH_TAI_SAN_ID, cheDoId: CheDoId);
                //}
                model.LyDoTangAvailable = _lydobiendongModelFactory.PrepareSelectListLyDoBienDong(LoaiHinhTaiSanId: (int)enumLOAI_HINH_TAI_SAN.NHA, loailydoId: (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO, isAddFirst: false, isTangMoi: isTangMoi);
                if (isTangMoi == true)
                {
                    model.LyDoTangAvailable.AddFirstRow("-- Chọn lý do tăng --");
                }
                if (isTangMoi == false)
                {
                    model.NGAY_NHAP = new DateTime(2017, 12, 31);
                }
            }
            return PartialView(model);
        }

        public virtual IActionResult _NguonVonNha(decimal NguonVonId, decimal? TaiSanId = 0)
        {
            var NguonVon = _nguonvonService.GetNguonVonById(NguonVonId);
            var model = NguonVon.ToModel<NguonVonModel>();
            if (TaiSanId > 0)
            {
                var taisan = _itemService.GetTaiSanById((decimal)TaiSanId);
                model.LoaiHinhTaiSanId = taisan.LOAI_HINH_TAI_SAN_ID;
                var TaiSanNguonVon = _taisannguonvonService.GetTaiSanNguonVonByTaiSanIdFirst(TaiSanId: (decimal)TaiSanId, NguonVonId: NguonVonId);
                model.GiaTri = TaiSanNguonVon.GIA_TRI;
            }
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult CreateOrUpdateNha(TaiSanModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedView();
            model.taisannhaModel.DIA_CHI = model.taisannhaModel.NHA_DIA_CHI;
            if (model.HM_SO_NAM_CON_LAI > 0)
            {
                model.HM_TY_LE = Math.Round((1 / model.HM_SO_NAM_CON_LAI ?? 1), 2) * 100;
            }
            if (ModelState.IsValid)
            {
                var taisan = new TaiSan();
                model.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.CHO_DUYET;

                if (model.ID > 0)
                {
                    taisan = _itemService.GetTaiSanById(model.ID);
                    var maTSNha = taisan.MA;
                    _itemModelFactory.PrepareTaiSan(model, taisan);
                    taisan.MA = maTSNha;
                    _itemService.UpdateTaiSan(taisan);

                    var TsNha = _taisannhaService.GetTaiSanNhaByTaiSanId(taisan.ID);
                    _taiSanNhaModelFactory.PrepareTaiSanNha(model.taisannhaModel, TsNha);
                    _taisannhaService.UpdateTaiSanNha(TsNha);

                    //update NV_YEU_CAU
                    var yeucaunha = _yeucauService.GetYeuCauMoiNhatByTaiSanId(taisan.ID);
                    yeucaunha = _yeucauModelFactory.PrepareYeuCau(yeucaunha, taisan);
                    yeucaunha.LY_DO_BIEN_DONG_ID = model.LY_DO_BIEN_DONG_ID;
                    yeucaunha.NGUYEN_GIA = model.nvYeuCauChiTietModel.NGUYEN_GIA;
                    yeucaunha.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
                    yeucaunha.NGUON_VON_JSON = model.lstNguonVonModel.toStringJson();
                    _yeucauService.UpdateYeuCau(yeucaunha);

                    // update NV_YeuCauChiTiet
                    var yeucauchitietnha = _yeucauchitietService.GetYeuCauChiTietByYeuCauId(yeucaunha.ID);
                    yeucauchitietnha = _yeuCauChiTietModelFactory.PrepareYeuCauChiTiet(yeucauchitietnha, model);
                    yeucauchitietnha.DATA_JSON = yeucaunha.ToModel<YeuCauModel>().toStringJson();
                    yeucauchitietnha.DIA_CHI = model.taisannhaModel.DIA_CHI;
                    _yeucauchitietService.UpdateYeuCauChiTiet(yeucauchitietnha);
                    return JsonSuccessMessage("Cập nhật thành công", TsNha.ToModel<TaiSanNhaModel>());
                }
                else
                {
                    // insert tai san
                    _itemModelFactory.PrepareTaiSan(model, taisan);
                    taisan.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
                    taisan.DON_VI_ID = _workContext.CurrentDonVi.ID;
                    taisan.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                    _itemService.InsertTaiSan(taisan);
                    //update lai ma tai san nha
                    var donVi = _donviService.GetDonViById(taisan.DON_VI_ID ?? 0);
                    var loaiTs = _loaitaisanService.GetLoaiTaiSanById(taisan.LOAI_TAI_SAN_ID ?? 0);
                    taisan.MA = CommonHelper.GenMaTaiSan(donVi.MA, loaiTs.MA, taisan.ID);
                    _itemService.UpdateTaiSan(taisan);
                    //insert NV_YEU_CAU

                    var yeucau = _yeucauModelFactory.PrepareYeuCau(new YeuCau(), taisan);
                    yeucau.TAI_SAN_ID = taisan.ID;
                    yeucau.LY_DO_BIEN_DONG_ID = model.LY_DO_BIEN_DONG_ID;
                    yeucau.LOAI_HINH_TAI_SAN_ID = taisan.LOAI_HINH_TAI_SAN_ID;
                    yeucau.NGUYEN_GIA = model.nvYeuCauChiTietModel.NGUYEN_GIA;
                    yeucau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
                    yeucau.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO;
                    yeucau.NGUON_VON_JSON = model.lstNguonVonModel.toStringJson();
                    yeucau.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                    _yeucauService.InsertYeuCau(yeucau);

                    // insert NV_YeuCauChiTiet
                    var yeucauchitiet = new YeuCauChiTiet();
                    if (model.LOAI_TAI_SAN_ID > 0)
                    {
                        var loaiTS = _loaitaisanService.GetLoaiTaiSanById(model.LOAI_TAI_SAN_ID.Value);
                        yeucauchitiet.HM_TY_LE_HAO_MON = loaiTS.HM_TY_LE;
                    }
                    yeucauchitiet.YEU_CAU_ID = yeucau.ID;
                    _yeuCauChiTietModelFactory.PrepareYeuCauChiTiet(yeucauchitiet, model);
                    yeucauchitiet.DATA_JSON = yeucau.ToModel<YeuCauModel>().toStringJson();
                    yeucauchitiet.DIA_CHI = model.taisannhaModel.DIA_CHI;
                    _yeucauchitietService.InsertYeuCauChiTiet(yeucauchitiet);

                    //var lstHienTrang = _hientrangService.GetHienTrangs(LoaiHinhTsId: yeucau.LOAI_HINH_TAI_SAN_ID);
                    //var lstObjHienTrang = new List<ObjHienTrang>();
                    //foreach (var ht in lstHienTrang)
                    //{
                    //    var obj = new ObjHienTrang();
                    //    obj.HienTrangId = ht.ID;
                    //    obj.TenHienTrang = ht.TEN_HIEN_TRANG;
                    //    lstObjHienTrang.Add(obj);
                    //}
                    //var hientrangList = new HienTrangList()
                    //{
                    //    TaiSanId = yeucau.TAI_SAN_ID,
                    //    lstObjHienTrang = lstObjHienTrang
                    //};
                    //yeucauchitiet.HTSD_JSON = hientrangList.toStringJson();
                    //yeucauchitiet.DATA_JSON = yeucauchitiet.toStringJson();

                    //insert nguon von
                    //if (model.SelectedNguonVonIds.Count() > 0)
                    //{
                    //    _taisannguonvonService.DeleteTaiSanNguonVonByTaiSanId(taisan.ID);
                    //    foreach (var nv in model.lstNguonVonModel)
                    //    {
                    //        var tsnv = new TaiSanNguonVon();
                    //        tsnv.TAI_SAN_ID = taisan.ID;
                    //        tsnv.NGUON_VON_ID = nv.ID;
                    //        tsnv.GIA_TRI = nv.GiaTri ?? 0;
                    //        _taisannguonvonService.InsertTaiSanNguonVon(tsnv);
                    //    }
                    //}
                    // Insert Nha
                    var TsNha = new TaiSanNha();
                    _taiSanNhaModelFactory.PrepareTaiSanNha(model.taisannhaModel, TsNha);
                    TsNha.TAI_SAN_ID = taisan.ID;
                    _taisannhaService.InsertTaiSanNha(TsNha);
                    return JsonSuccessMessage("Thêm nhà thành công", TsNha.ToModel<TaiSanNhaModel>());
                }
            }
            //prepare model
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return JsonErrorMessage("Error", list);
        }

        [HttpGet]
        public virtual IActionResult _ListTaiSanNhaByDatId(decimal taiSanDatId)
        {
            var searchModel = new TaiSanNhaSearchModel();
            searchModel.TAI_SAN_DAT_ID = taiSanDatId;
            return PartialView(searchModel);
        }

        [HttpPost]
        public virtual IActionResult ListTaiSanNhaByDatId(TaiSanNhaSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedView();
            //prepare model
            TaiSanNhaListModel model = new TaiSanNhaListModel();
            if (searchModel.TAI_SAN_DAT_ID > 0)
            {
                if (searchModel.PageSize == 0) searchModel.PageSize = 10;
                model = _taiSanNhaModelFactory.PrepareTaiSanNhaListModel(searchModel);
            }
            return Json(model);
        }

        #endregion TaiSanNha

        #region Tài sản vô hình

        public virtual IActionResult _TaiSanVoHinh(decimal taiSanId)
        {
            var item = _taiSanVoHinhService.GetTaiSanVoHinhById(taiSanId);
            var model = _taiSanVoHinhModelFactory.PrepareTaiSanVoHinhModel(new TaiSanVoHinhModel(), item);
            return PartialView(model);
        }

        #endregion Tài sản vô hình
        #region Tinh hao mon, trich khau hao

        /// <summary>
        /// Description: Tinh toan gia tri tai san khi nhap so du hoặc tăng mới tài sản
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult TinhToanGiaTriTS(TinhGiaTriTSModel model)
        {
            var donvi = _donviService.GetDonViById(_workContext.CurrentDonVi.ID);
            model.DON_VI_CHE_DO_HACH_TOAN_ID = donvi.CHE_DO_HACH_TOAN_ID ?? (int)enumCHE_DO_HACH_TOAN.HAO_MON;
            if (model.TS_NguyenGia == null || (model.LoaiTaiSanId == null && model.LoaiTaiSanDonViId == null))
                return JsonSuccessMessage("success", model);
            if (model.LoaiTaiSanId > 0 || model.LoaiTaiSanDonViId > 0)
            {
                //code phiên bản nhập giá trị nguyên giá trích khấu hao thay cho tỷ lệ NG trích
                //Nên cần tính tỷ lệ trích KH từ NG trích KH

                //if (model.HM_LuyKe != null && model.HM_GiaTriConLai != null) { }
                //if (model.TS_NguyenGiaTinhKH == null)
                //{
                //	model.TS_NguyenGiaTinhKH = 0;
                //}
                //model.TS_GiaTriHienTai = model.TS_NguyenGia;
                //if (model.TS_NguyenGia == 0)
                //{
                //	model.TS_TyLeNguyenGiaTrichKH = 0;
                //}
                //else
                //{
                //	model.TS_TyLeNguyenGiaTrichKH = model.TS_NguyenGiaTinhKH / model.TS_NguyenGia;
                //}
                if (model.TS_GiaTriHienTai == null)
                {
                    model.TS_GiaTriHienTai = model.TS_NguyenGia;
                }
                model.TS_TyLeNguyenGiaTinhHM = 100 - model.TS_TyLeNguyenGiaTrichKH;
                model = _itemModelFactory.TinhToanGiaTriTS(model);
                return JsonSuccessMessage("success", model);
            }
            else
                return JsonErrorMessage("Error", "Bạn chưa chọn loại tài sản.");
        }

        #endregion Tinh hao mon, trich khau hao
        #region Duyet Tai San

        public virtual IActionResult ListYeuCauDuyetTaiSan(int HanhDong)
        {
            if ((enumHanhDongListDuyetTaiSan)HanhDong == enumHanhDongListDuyetTaiSan.DUYET)
            {
                if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDuyetDangKy))
                    return AccessDeniedView();
            }
            else if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBoDuyetDangKy))
                return AccessDeniedView();

            //prepare model
            //var searchmodel = new DuyetDangKyTaiSanSearchModel();
            var searchModel = new TaiSanSearchModel();
            var model = _itemModelFactory.PrepareTaiSanSearchModel(searchModel: searchModel, isDuyetBienDong: true);
            model.enumHanhDongListDuyetTaiSan = (enumHanhDongListDuyetTaiSan)HanhDong;
            //bỏ duyệt: danh sách chỉ có đã duyệt
            if (model.enumHanhDongListDuyetTaiSan == enumHanhDongListDuyetTaiSan.BO_DUYET)
                model.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.DA_DUYET;
            //còn lại mặc định danh sách là chờ duyệt
            else
                model.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET;
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult ListYeuCauDuyetTaiSan(TaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDuyetDangKy))
                return AccessDeniedView();
            //prepare model
            searchModel.isDuyet = true;
            searchModel.IsChonTaiSan = true;
            var model = _itemModelFactory.PrepareTaiSanListModel(searchModel);

            return Json(model);
        }

        [HttpPost]
        public virtual IActionResult ListYeuCau(TrungGianBDYCSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTaiSan))
                return AccessDeniedView();
            //prepare model
            var model = _trungGianBDYCModelFactory.PrepareTrungGianBDYCListModel(searchModel);

            return Json(model);
        }

        [HttpPost]
        public virtual IActionResult TrinhDuyet(string strTaiSanIds, string strYeuCauIds)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDuyetDangKy))
                return AccessDeniedView();
            if (!String.IsNullOrEmpty(strTaiSanIds))
            {
                var lstTaiSanIds = strTaiSanIds.Split("-");
                foreach (var TsId in lstTaiSanIds)
                {
                    var taisan = _itemService.GetTaiSanById(Convert.ToDecimal(TsId));
                    taisan.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.CHO_DUYET;
                    _itemService.UpdateTaiSan(taisan);
                    var yeucau = _yeucauService.GetYeuCauMoiNhatByTaiSanId(taisan.ID);
                    yeucau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
                    _yeucauService.UpdateYeuCau(yeucau);
                    _yeucaunhatkyModelFactory.InsertYeuCauNhatKy(yeucau.ToModel<YeuCauModel>(), enumLOAI_NHATKY_YEUCAU.TRINH_DUYET);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TrinhDuyet, "Trình duyệt tài sản", taisan.ToModel<TaiSanModel>(), "TaiSan");
                    _hoatdongService.InsertHoatDong(enumHoatDong.TrinhDuyet, "Trình duyệt yêu cầu", yeucau.ToModel<YeuCauModel>(), "YeuCau");
                }
            }
            if (!String.IsNullOrEmpty(strYeuCauIds))
            {
                var lstYeuCauIds = strYeuCauIds.Split("-");
                foreach (var YeucauId in lstYeuCauIds)
                {
                    var yeucau = _yeucauService.GetYeuCauById(Convert.ToDecimal(YeucauId));
                    yeucau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
                    _yeucauService.UpdateYeuCau(yeucau);
                    _yeucaunhatkyModelFactory.InsertYeuCauNhatKy(yeucau.ToModel<YeuCauModel>(), enumLOAI_NHATKY_YEUCAU.TRINH_DUYET);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TrinhDuyet, "Trình duyệt yêu cầu", yeucau.ToModel<YeuCauModel>(), "YeuCau");
                }
                return JsonSuccessMessage("Trình duyệt thành công");
            }
            return JsonSuccessMessage("Trình duyệt thành công");
        }

        public virtual IActionResult _LyDoHuyBDorYC(int ID, int BDorYC)
        {
            var item = _trungGianBDYCService.GetYCBDorYCById(ID, BDorYC);
            var model = new TrungGianBDYCModel();
            if (item != null)
            {
                model = item.ToModel<TrungGianBDYCModel>();
                if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
                {
                    model.TenLoaiTaiSan = _loaitaisanService.GetLoaiTaiSanById(item.LOAI_TAI_SAN_ID ?? 0).TEN;
                }
                else
                {
                    //Code here
                    var lts_vh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(item.LOAI_TAI_SAN_DON_VI_ID ?? 0);
                    if (lts_vh != null)
                    {
                        model.TenLoaiTaiSan = lts_vh.TEN;
                    }
                }
                var lyDoBienDong = _lydobiendongService.GetLyDoBienDongById(model.LY_DO_BIEN_DONG_ID ?? 0);
                model.TenLyDoBienDong = lyDoBienDong.TEN;
                if (model.NGUOI_TAO_ID > 0)
                {
                    var nguoidung = _nguoiDungService.GetNguoiDungById(model.NGUOI_TAO_ID);
                    model.TenNguoiTao = nguoidung.TEN_DANG_NHAP;
                }
                var donVi = _donviService.GetDonViById(item.DON_VI_ID ?? 0);
                model.TenDonVi = donVi.TEN;
                model.TenTrangThai = _nhanHienThiService.GetGiaTriEnum(item.TrangThaiYeuCau);
            }
            return PartialView(model);
        }

        #endregion Duyet Tai San

        #region Các popup tài sản

        public virtual IActionResult _ChonTaiSanDat(bool? isDanhSachTaiSanDuAn = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDTangGiamHangNam)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu)
                )
                return AccessDeniedView();
            //prepare model
            var searchModel = new TaiSanSearchModel();
            searchModel.LOAI_HINH_TAI_SAN_ID = (int)enumLOAI_HINH_TAI_SAN.DAT;
            var hasDonViQLDA = _donviService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
            if (hasDonViQLDA)
            {
                var donvi = _donViModelFactory.GetDonViWithConditions(donViId: _workContext.CurrentDonVi.ID, isBanQuanLyDuAn: hasDonViQLDA, isCreateTSDA: isDanhSachTaiSanDuAn);
                searchModel.donviId = donvi.ID;
            }
            else
            {
                searchModel.donviId = _workContext.CurrentDonVi.ID;
            }
            searchModel.IsDanhSachTaiSanDuAn = isDanhSachTaiSanDuAn;
            var model = _itemModelFactory.PrepareTaiSanSearchModel(searchModel);
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult _ChonTaiSanDat(TaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDTangGiamHangNam)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu)
                )
                return AccessDeniedKendoGridJson();
            //prepare model
            if (searchModel.PageSize == 0) searchModel.PageSize = 10;
            searchModel.LOAI_HINH_TAI_SAN_ID = (int)enumLOAI_HINH_TAI_SAN.DAT;

            var model = _itemModelFactory.PrepareDanhSachTaiSan(searchModel);
            return Json(model);
        }

        public virtual IActionResult GetTaiSanDatById(Decimal id)
        {
            if (id > 0)
            {
                var taiSan = _itemService.GetTaiSanById(id);
                var taiSanDat = _taisandatService.GetTaiSanDatByTaiSanId(id);
                var model = new TaiSanDatModel();
                model.TAI_SAN_ID = taiSanDat.TAI_SAN_ID;
                model.DIA_CHI = taiSanDat.DIA_CHI ?? taiSan.TEN;
                model.TenDat = taiSan.TEN;
                model.TaiSanMa = taiSan.MA;
                return JsonSuccessMessage("Success", model);
            }
            else
                return JsonErrorMessage("Tài sản không tồn tại");
        }

        /// <summary>
        /// Description: Chọn tài sản thực hiện biến động
        /// </summary>
        /// <param name="loaiLyDoBienDong"></param>
        /// <param name="isDanhSachTaiSanDuAn"></param>
        /// <returns></returns>
        public virtual IActionResult _ChonTaiSan(decimal? loaiLyDoBienDong = 0, bool? isDanhSachTaiSanDuAn = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDTangGiamHangNam)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu)
                )
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanSearchModel();
            if (loaiLyDoBienDong.HasValue && loaiLyDoBienDong > 0)
            {
                searchmodel.Loai_Ly_Do_ID = loaiLyDoBienDong.Value;
            }
            //searchmodel.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.DA_DUYET;
            var hasDonViQLDA = _donviService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
            if (hasDonViQLDA)
            {
                var donvi = _donViModelFactory.GetDonViWithConditions(donViId: _workContext.CurrentDonVi.ID, isBanQuanLyDuAn: hasDonViQLDA, isCreateTSDA: isDanhSachTaiSanDuAn);
                searchmodel.donviId = donvi.ID;
            }
            else
            {
                searchmodel.donviId = _workContext.CurrentDonVi.ID;
            }
            searchmodel.IsDanhSachTaiSanDuAn = isDanhSachTaiSanDuAn;

            var model = _itemModelFactory.PrepareTaiSanSearchModel(searchmodel);
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult _ChonTaiSan(TaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDTangGiamHangNam)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu)
                )
                return AccessDeniedKendoGridJson();
            //prepare model
            if (searchModel.PageSize == 0) searchModel.PageSize = 10;
            searchModel.NguoiTaoId = 0;
            searchModel.IsChonTaiSan = true;
            var model = _itemModelFactory.PrepareDanhSachTaiSan(searchModel);
            return Json(model);
        }

        #endregion Các popup tài sản
        #region Module nhân bản, sao chép

        public virtual IActionResult SaoChepTaiSan(decimal idTSCP)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedView();

            //Lấy thông tin chung tài sản được sao chép
            var itemCP = _itemService.GetTaiSanById(idTSCP);
            if (itemCP == null)
                return RedirectToAction("List");
            //Tạo bản sao thông tin tài sản
            var _newTS = new TaiSan(itemCP);
            _newTS.ID = 0;
            _newTS.MA = null;
            _newTS.MA_DB = null;
            _newTS.MA_QLDKTS40 = null;
            _newTS.GUID = Guid.NewGuid();
            _newTS.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.NHAP;
            _itemService.InsertTaiSan(_newTS);
            //update mã tài sản
            var donVi = _workContext.CurrentDonVi;
            if (itemCP.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
            {
                var loaiTs = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(itemCP.LOAI_TAI_SAN_DON_VI_ID ?? 0);
                _newTS.MA = CommonHelper.GenMaTaiSan(donVi.MA_DON_VI, loaiTs.MA, _newTS.ID);
                _itemService.UpdateTaiSan(_newTS);
            }
            else
            {
                var loaiTs = _loaitaisanService.GetLoaiTaiSanById(itemCP.LOAI_TAI_SAN_ID ?? 0);
                _newTS.MA = CommonHelper.GenMaTaiSan(donVi.MA_DON_VI, loaiTs.MA, _newTS.ID);
                _itemService.UpdateTaiSan(_newTS);
            }
            //sao chép yêu cầu hoặc biến động
            var _newYeuCau = new YeuCau();
            var _newYeuCauCT = new YeuCauChiTiet();
            if (itemCP.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET || itemCP.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO)
            {
                //Tài sản đã duyệt lấy thông tin từ biến động
                var _bienDongCP = _biendongService.GetBienDongCuNhatByTaiSanId(itemCP.ID);
                var _bienDongCTCP = _biendongchitietService.GetBienDongChiTietByBDId(_bienDongCP.ID);
                _newYeuCau = _yeucauModelFactory.PrepareYeuCauFromBienDong(_bienDongCP, new YeuCau(), true);
                _newYeuCauCT = _yeuCauChiTietModelFactory.PrepareYCCTFromBDCT(_bienDongCTCP, new YeuCauChiTiet(), true);
            }
            else
            {
                //Tài sản chưa duyệt lấy thông tin từ yêu cầu
                var _yeuCauCP = _yeucauService.GetYeuCauCuNhatByTSId(itemCP.ID);
                var _yeuCauCTCP = _yeucauchitietService.GetYeuCauChiTietByYeuCauId(_yeuCauCP.ID);
                _newYeuCau = new YeuCau(_yeuCauCP);
                _newYeuCauCT = new YeuCauChiTiet(_yeuCauCTCP);
            }
            _newYeuCau.ID = 0;
            _newYeuCau.GUID = new Guid();
            _newYeuCau.TAI_SAN_ID = _newTS.ID;
            _newYeuCau.TAI_SAN_MA = _newTS.MA;
            _newYeuCau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
            _newYeuCau.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            _yeucauService.InsertYeuCau(_newYeuCau);
            _newYeuCauCT.YEU_CAU_ID = _newYeuCau.ID;
            _yeucauchitietService.InsertYeuCauChiTiet(_newYeuCauCT);
            //Ghi log nhật ký yêu cầu
            _yeucaunhatkyModelFactory.InsertYeuCauNhatKy(_newYeuCau.ToModel<YeuCauModel>(), enumLOAI_NHATKY_YEUCAU.THEM_MOI);
            //Nhân bản tài sản chi tiết theo từng loại hình
            switch (itemCP.LOAI_HINH_TAI_SAN_ID)
            {
                case (int)enumLOAI_HINH_TAI_SAN.DAT:
                    var tsDat = _taisandatService.GetTaiSanDatByTaiSanId(idTSCP);
                    var _itemDatClone = new TaiSanDat(tsDat);
                    _itemDatClone.TAI_SAN_ID = _newTS.ID;
                    _taisandatService.InsertTaiSanDat(_itemDatClone);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.NHA:
                    var tsNha = _taisannhaService.GetTaiSanNhaByTaiSanId(idTSCP);
                    var _itemNhaClone = new TaiSanNha(tsNha);
                    _itemNhaClone.TAI_SAN_ID = _newTS.ID;
                    _taisannhaService.InsertTaiSanNha(_itemNhaClone);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                case (int)enumLOAI_HINH_TAI_SAN.OTO:
                    var tsOto = _taisanOtoService.GetTaiSanOtoByTaiSanId(idTSCP);
                    var _itemOtoClone = new TaiSanOto(tsOto);
                    _itemOtoClone.TAI_SAN_ID = _newTS.ID;
                    _itemOtoClone.BIEN_KIEM_SOAT = " ";
                    _taisanOtoService.InsertTaiSanOto(_itemOtoClone);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                    var tsCLN = _taisanClnService.GetTaiSanClnByTaiSanId(idTSCP);
                    var _itemCLNClone = new TaiSanCln(tsCLN);
                    _itemCLNClone.TAI_SAN_ID = _newTS.ID;
                    _taisanClnService.InsertTaiSanCln(_itemCLNClone);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                    var tsMMTB = _taisanmaymocService.GetTaiSanMaymocByTaiSanId(idTSCP);
                    var _itemMMTBClone = new TaiSanMayMoc(tsMMTB);
                    _itemMMTBClone.TAI_SAN_ID = _newTS.ID;
                    _taisanmaymocService.InsertTaiSanMayMoc(_itemMMTBClone);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    var itemVKTCP = _taisanVKTService.GetTaiSanVktByTaiSanId(idTSCP);
                    var _itemVKTClone = new TaiSanVkt(itemVKTCP);
                    _itemVKTClone.TAI_SAN_ID = _newTS.ID;
                    _taisanVKTService.InsertTaiSanVkt(_itemVKTClone);
                    break;

                case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                    var itemVH = _taiSanVoHinhService.GetTaiSanVoHinhByTaiSanId(idTSCP);
                    var _itemVHClone = new TaiSanVoHinh(itemVH);
                    _itemVHClone.TAI_SAN_ID = _newTS.ID;
                    _taiSanVoHinhService.InsertTaiSanVoHinh(_itemVHClone);
                    break;

                default:
                    break;
            }
            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Sao chép thông tin", _newTS.ToModel<TaiSanModel>(), "TaiSan");
            return RedirectToAction("Edit", new { guid = _newTS.GUID });
            //return Json(_newTS.GUID);
        }

        /// <summary>
        /// Description: Function thực hiện chức năng nhân bản tài sản
        /// </summary>
        /// <param name="_idClone">id của tài sản được nhân bản</param>
        /// <param name="_soLuong">số lượng nhân bản</param>
        public void NhanBanTaiSan(decimal _idClone, decimal _soLuong)
        {
            if (_soLuong < 1)
                return;
            //lay thông tin nhân bản
            var tsClone = _itemService.GetTaiSanById(_idClone);
            string maLoaiTs = "";
            if (tsClone.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
            {
                var loaiTSVoHinh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(tsClone.LOAI_TAI_SAN_DON_VI_ID ?? 0);
                maLoaiTs = loaiTSVoHinh.MA;
            }
            else
            {
                var loaiTs = _loaitaisanService.GetLoaiTaiSanById(tsClone.LOAI_TAI_SAN_ID ?? 0);
                maLoaiTs = loaiTs.MA;
            }
            var donVi = _donviService.GetDonViById(tsClone.DON_VI_ID ?? 0);
            var _listTSIdClone = new List<TaiSan>();
            for (int i = 0; i < _soLuong; i++)
            {
                var _newTS = new TaiSan(tsClone);
                _newTS.ID = 0;
                _newTS.MA = null;
                _newTS.MA_DB = null;
                _newTS.MA_QLDKTS40 = null;
                _newTS.GUID = Guid.NewGuid();
                _itemService.InsertTaiSan(_newTS);
                //update Mã tài sản
                _newTS.MA = CommonHelper.GenMaTaiSan(donVi.MA, maLoaiTs, _newTS.ID);
                _itemService.UpdateTaiSan(_newTS);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Nhân bản thông tin ", _newTS.ToModel<TaiSanModel>(), "TaiSan");
                _listTSIdClone.Add(_newTS);
            }

            //Lấy thông tin yêu cầu tài sản
            //Lấy thông tin yêu cầu tài sản
            //sao chép yêu cầu hoặc biến động
            var yeuCau = new YeuCau();
            var yeuCauCT = new YeuCauChiTiet();
            if (tsClone.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET || tsClone.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO)
            {
                //Tài sản đã duyệt lấy thông tin từ biến động
                var _bienDongCP = _biendongService.GetBienDongCuNhatByTaiSanId(tsClone.ID);
                var _bienDongCTCP = _biendongchitietService.GetBienDongChiTietByBDId(_bienDongCP.ID);
                yeuCau = _yeucauModelFactory.PrepareYeuCauFromBienDong(_bienDongCP, new YeuCau(), true);
                yeuCauCT = _yeuCauChiTietModelFactory.PrepareYCCTFromBDCT(_bienDongCTCP, new YeuCauChiTiet(), true);
            }
            else
            {
                //Tài sản chưa duyệt lấy thông tin từ yêu cầu
                yeuCau = _yeucauService.GetYeuCauCuNhatByTSId(tsClone.ID);
                yeuCauCT = _yeucauchitietService.GetYeuCauChiTietByYeuCauId(yeuCau.ID);
            }
            foreach (var _newTS in _listTSIdClone)
            {
                //nhân bản yêu cầu
                var _newYeuCau = new YeuCau(yeuCau);
                _newYeuCau.ID = 0;
                _newYeuCau.GUID = new Guid();
                _newYeuCau.TAI_SAN_ID = _newTS.ID;
                _newYeuCau.TAI_SAN_MA = _newTS.MA;
                _newYeuCau.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;

                _yeucauService.InsertYeuCau(_newYeuCau);
                var _newYeuCauChiTiet = new YeuCauChiTiet(yeuCauCT);
                _newYeuCauChiTiet.YEU_CAU_ID = _newYeuCau.ID;
                _yeucauchitietService.InsertYeuCauChiTiet(_newYeuCauChiTiet);
                //Ghi log nhật ký yêu cầu
                _yeucaunhatkyModelFactory.InsertYeuCauNhatKy(_newYeuCau.ToModel<YeuCauModel>(), enumLOAI_NHATKY_YEUCAU.THEM_MOI);
                //Nhân bản tài sản chi tiết theo từng loại hình
                switch (tsClone.LOAI_HINH_TAI_SAN_ID)
                {
                    case (int)enumLOAI_HINH_TAI_SAN.DAT:
                        var tsDat = _taisandatService.GetTaiSanDatByTaiSanId(_idClone);
                        var _itemDatClone = new TaiSanDat(tsDat);
                        _itemDatClone.TAI_SAN_ID = _newTS.ID;
                        _taisandatService.InsertTaiSanDat(_itemDatClone);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.NHA:
                        var tsNha = _taisannhaService.GetTaiSanNhaByTaiSanId(_idClone);
                        var _itemNhaClone = new TaiSanNha(tsNha);
                        _itemNhaClone.TAI_SAN_ID = _newTS.ID;
                        _taisannhaService.InsertTaiSanNha(_itemNhaClone);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                    case (int)enumLOAI_HINH_TAI_SAN.OTO:
                        var tsOto = _taisanOtoService.GetTaiSanOtoByTaiSanId(_idClone);
                        var _itemOtoClone = new TaiSanOto(tsOto);
                        _itemOtoClone.TAI_SAN_ID = _newTS.ID;
                        _itemOtoClone.BIEN_KIEM_SOAT = " ";
                        _taisanOtoService.InsertTaiSanOto(_itemOtoClone);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                        var tsCLN = _taisanClnService.GetTaiSanClnByTaiSanId(_idClone);
                        var _itemCLNClone = new TaiSanCln(tsCLN);
                        _itemCLNClone.TAI_SAN_ID = _newTS.ID;
                        _taisanClnService.InsertTaiSanCln(_itemCLNClone);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                    case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                        var tsMMTB = _taisanmaymocService.GetTaiSanMaymocByTaiSanId(_idClone);
                        var _itemMMTBClone = new TaiSanMayMoc(tsMMTB);
                        _itemMMTBClone.TAI_SAN_ID = _newTS.ID;
                        _taisanmaymocService.InsertTaiSanMayMoc(_itemMMTBClone);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                        var itemVKTCP = _taisanVKTService.GetTaiSanVktByTaiSanId(_idClone);
                        var _itemVKTClone = new TaiSanVkt(itemVKTCP);
                        _itemVKTClone.TAI_SAN_ID = _newTS.ID;
                        _taisanVKTService.InsertTaiSanVkt(_itemVKTClone);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                        var itemVH = _taiSanVoHinhService.GetTaiSanVoHinhByTaiSanId(_idClone);
                        var _itemVHClone = new TaiSanVoHinh(itemVH);
                        _itemVHClone.TAI_SAN_ID = _newTS.ID;
                        _taiSanVoHinhService.InsertTaiSanVoHinh(_itemVHClone);
                        break;

                    default:
                        break;
                }
                //tự động duyệt
                if (_itemModelFactory.CheckAutoDuyet(_newYeuCau.LOAI_HINH_TAI_SAN_ID, _newTS.NGUYEN_GIA_BAN_DAU))
                    _thaoTacBienDongModelFactory.DuyetYeuCauFunc(yeuCau.ID, _newYeuCau);
            }
        }

        #endregion Module nhân bản, sao chép

        #region Giá mua tiếp nhận

        [HttpPost]
        public virtual IActionResult CheckGiaMuaTiepNhan(int loaitaisan, int? chucdanh, int giamua, DateTime ngaysudung)
        {
            var cauhinhdinhmucchucdanh = _cauHinhService.LoadCauHinh<CauHinhDinhMucChucDanh>(_workContext.CurrentDonVi.ID);
            var listCauHinh = cauhinhdinhmucchucdanh.dmcd.toEntities<DinhMucChucDanhModel>();
            var listChiTiet = new List<ChiTietDinhMucChucDanhModel>();
            var dinhmuc = new DinhMucChucDanhModel();
            var chitietDM = new ChiTietDinhMucChucDanhModel();

            if (loaitaisan != 0)
            {
                if (chucdanh != null)
                {
                    foreach (var cauhinh in listCauHinh)
                    {
                        foreach (var chitiet in cauhinh.ChiTietDinhMuc)
                        {
                            if (chitiet.NHOM_TAI_SAN_ID == loaitaisan && chitiet.CHUC_DANH_ID == chucdanh)
                            {
                                //listChiTiet.Add(chitiet);
                                chitietDM = chitiet;
                            }
                        }
                    }
                }
                else
                {
                    foreach (var cauhinh in listCauHinh)
                    {
                        foreach (var chitiet in cauhinh.ChiTietDinhMuc)
                        {
                            if (chitiet.NHOM_TAI_SAN_ID == loaitaisan)
                            {
                                dinhmuc = cauhinh;
                                chitietDM = chitiet;
                            }
                        }
                    }
                }
            }
            if (chitietDM.DINH_MUC < giamua && DateTime.Compare(ngaysudung, dinhmuc.DEN_NGAY.Value) < 0 && DateTime.Compare(ngaysudung, dinhmuc.TU_NGAY.Value) >= 0)
            {
                return JsonSuccessMessage("Giá mua đang nhập quá định mức");
            }
            return JsonSuccessMessage("");
        }

        #endregion Giá mua tiếp nhận

        #region Xóa toàn bộ tài sản

        public IActionResult DeleteAllTaiSan()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLXoaTaiSan))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanSearchModel();
            var model = _itemModelFactory.PrepareTaiSanSearchModel(searchmodel, true);
            model.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.ALL;
            model.loaihinhtaisancheck = true;
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult DeleteAllTaiSan(TaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLXoaTaiSan))
                return AccessDeniedView();
            //prepare model
            searchModel.isDuyet = true;
            searchModel.NguoiTaoId = 0;
            var model = _itemModelFactory.PrepareDanhSachTaiSan(searchModel);

            return Json(model);
        }

        [HttpPost]
        public virtual IActionResult ActionDeleteAllTaiSan(TaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLXoaTaiSan))
                return AccessDeniedView();
            if (string.IsNullOrEmpty(searchModel.strLoaiHinhTSIds))
                return JsonErrorMessage("Phải chọn loại tài sản");
            var res = _itemService.DeleteTaiSansLogic(p_DonViId: searchModel.donviId.Value, strLoaiHinhTaiSan: searchModel.strLoaiHinhTSIds, IsDeleteDVCon: searchModel.IsDeleteDVCon, nguon_tai_san_id: searchModel.NguonTaiSanId);
            if (res)
                return JsonSuccessMessage("Xóa thành công");
            else
                return JsonErrorMessage("Có lỗi xảy ra");
        }

        [HttpPost]
        public virtual IActionResult ActionDeleteLogicTaiSan(Guid guid)
        {
            if (guid == Guid.Empty)
                return JsonErrorMessage("Có lỗi xảy ra");
            var ts = _itemService.GetTaiSanByGuId(guid);
            if (ts != null)
            {
                ts.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.XOA;
                ts.NGAY_CAP_NHAT = DateTime.Now;
                _itemService.UpdateTaiSan(ts);
                //_taiSanNhatKyService.UpdateTrangThaiTaiSanNhatKy(ts.ID);
                //cập nhật bđ
                var list_BD = _biendongService.GetBienDongsByTaiSanId(ts.ID);
                if (list_BD != null && list_BD.Count > 0)
                {
                    foreach (var bd in list_BD)
                    {
                        bd.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.XOA;
                        _biendongService.UpdateBienDong(bd);
                    }
                }
                //cập nhật yc
                var list_YC = _yeucauService.GetYeuCaus(ts.ID);
                if (list_YC != null && list_YC.Count > 0)
                {
                    foreach (var yc in list_YC)
                    {
                        yc.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.XOA;
                        _yeucauService.UpdateYeuCau(yc);
                    }
                }
                return JsonSuccessMessage("Xóa thành công");
            }
            return JsonErrorMessage("Có lỗi xảy ra");
        }

        #endregion Xóa toàn bộ tài sản
        #region Chuyển nguồn tài sản

        public IActionResult ChuyenNguonTaiSan()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTaiSan))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanSearchModel();
            var model = _itemModelFactory.PrepareTaiSanSearchModel(searchmodel, true);
            model.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.ALL;
            model.loaihinhtaisancheck = true;
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult ChuyenNguonTaiSan(TaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTaiSan))
                return AccessDeniedView();
            //prepare model
            searchModel.isDuyet = true;
            searchModel.NguoiTaoId = 0;
            var model = _itemModelFactory.PrepareDanhSachTaiSan(searchModel);
            return Json(model);
        }

        [HttpPost]
        public virtual IActionResult ActionChuyenAllTaiSan(TaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTaiSan))
                return AccessDeniedView();
            if (string.IsNullOrEmpty(searchModel.strLoaiHinhTSIds))
                return JsonErrorMessage("Phải chọn loại tài sản");
            var res = _itemService.ChuyenNguonTaiSan(p_DonViId: searchModel.donviId.Value, strLoaiHinhTaiSan: searchModel.strLoaiHinhTSIds, IsDVCon: searchModel.IsDeleteDVCon, nguon_tai_san_id_from: searchModel.NguonTaiSanId, nguon_tai_san_id_to: searchModel.NguonTaiSanIdTo);
            if (res)
                return JsonSuccessMessage("Chuyển thành công");
            else
                return JsonErrorMessage("Có lỗi xảy ra");
        }

        #endregion Chuyển nguồn tài sản
        #region Hàm tiện ích

        public virtual IActionResult KiemTraTaiSanTruocKhiTaoBienDong(decimal taiSanId)
        {
            var ts = _itemService.GetTaiSanById(taiSanId);
            if (ts != null)
            {
                if (ts.TrangThaiTaiSan == enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO)
                    return JsonErrorMessage($"Tài sản {ts.MA} đã duyệt giảm toàn bộ");
                else if (ts.TrangThaiTaiSan == enumTRANG_THAI_TAI_SAN.TRA_LAI)
                    return JsonErrorMessage($"Tài sản {ts.MA} đang được trả lại");
                //kiểm tra có yêu cầu giảm
                var res = _itemService.IsNotHasBDGiamTaiSan(ts.GUID);
                if (!res)
                    return JsonErrorMessage($"Tài sản {ts.MA} có biến động giảm");

                return JsonSuccessMessage();
            }
            return JsonErrorMessage($"Tài sản {ts.MA} không tồn tại");
        }

        #endregion Hàm tiện ích

        public virtual IActionResult KiemTraTaiSanDatNha(int? pageIndex = 0)
        {
            var searchmodel = new TaiSanSearchModel();
            var model = _itemModelFactory.PrepareKiemTraTaiSanSearchModel(searchmodel);
            if (pageIndex > 0)
                searchmodel.Page = (int)pageIndex;
            searchmodel.donviId = _workContext.CurrentDonVi.ID;
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult KiemTraTaiSanDatNha(TaiSanSearchModel searchModel)
        {
            searchModel.donviId = _workContext.CurrentDonVi.ID;
            var model = _itemModelFactory.PrepareKiemTraTaiSanListModel(searchModel);
            return Json(model);
        }

        #region Cập nhập tài sản nhận về

        public virtual IActionResult CapNhapTaiSanChuyenDung()
        {
            //if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMuc))
            //    return AccessDeniedView();
            //prepare model
            var model = new TaiSanSearchModel();
            model = _capNhatTaiSanModelFactory.PrepareCapNhatTaiSanSearchModel(model);
            model.PageSize = 20;
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult CapNhapTaiSanChuyenDung(TaiSanSearchModel searchModel)
        {
            //if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMuc))
            //    return AccessDeniedKendoGridJson();
            //prepare model
            //if (searchModel.PageSize == 0)
            //searchModel.PageSize = 20;
            searchModel.MA_CAU_HINH_CANH_BAO_TS = "CAPNHAT_LOAITS";
            var model = _capNhatTaiSanModelFactory.PrepareCapNhatTaiSanListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult ExportTaiSanOto(string KeySearch)
        {
            TaiSanSearchModel searchModel = new TaiSanSearchModel()
            {
                KeySearch = KeySearch
            };
            searchModel.MA_CAU_HINH_CANH_BAO_TS = "CAPNHAT_LOAITS";
            var model = _capNhatTaiSanModelFactory.PrepareExportTaiSanOto(searchModel);
            int total = model != null ? model.Count() : 0;
            bool addSTT = true;
            string fName = $"TaiSan_{total}({DateTime.Now.ToString("dd-MM-yyyy hh24-mm-ss")})";
            MemoryStream stream = new MemoryStream();
            stream = _reportModelFactory.prepareExcelEntity<TaiSanExportOto>(stream, model, "DonVi", addSTT);
            var bytes = stream.ToArray();
            return new FileContentResult(bytes, MimeTypes.TextXlsx)
            {
                FileDownloadName = fName + ".xlsx"
            };
        }

        [HttpPost]
        public virtual IActionResult EditLoaiTaiSanXeChuyenDung(string strTaiSanId, decimal idLTS)
        {
            if (!string.IsNullOrEmpty(strTaiSanId))
            {
                var list_idTS = strTaiSanId.Split(",").Select(p => int.Parse(p));
                if (list_idTS == null || list_idTS.Count() == 0)
                    return JsonErrorMessage();
                foreach (var item in list_idTS)
                {
                    var ts = _itemService.GetTaiSanById(item);
                    if (ts != null)
                    {
                        var yeucaus = _yeucauService.GetYeuCaus(TaiSanId: item);
                        if (yeucaus != null)
                        {
                            foreach (var yc in yeucaus)
                            {
                                yc.LOAI_TAI_SAN_ID = idLTS;
                                _yeucauService.UpdateYeuCau(yc);
                            }
                        }
                        var bienDongs = _biendongService.GetBienDongsByTaiSanId(taiSanId: item);
                        if (bienDongs != null)
                        {
                            foreach (var bd in bienDongs)
                            {
                                bd.LOAI_TAI_SAN_ID = idLTS;
                                _biendongService.UpdateBienDong(bd);
                            }
                        }
                        ts.LOAI_TAI_SAN_ID = idLTS;
                        _itemService.UpdateTaiSan(ts);
                    }
                }
                return this.JsonSuccessMessage();
            }
            return JsonErrorMessage();
        }

        public virtual async Task<IActionResult> CanhBao()
        {
            var cauHinhCanhBaoTaiSans = await _itemService.CountTaiSanCanhBao(_workContext.CurrentDonVi.ID);
            return View(cauHinhCanhBaoTaiSans);
        }

        public virtual async Task<IActionResult> CanhBaoDuLieuTaiSan()
        {
            var isChuyenMa = _donviService.CheckTaiKhoanDuocCapNhatMaT(_workContext.CurrentDonVi.ID,
                                                                       _workContext.CurrentCustomer.ID,
                                                                       _workContext.CurrentCustomer.IS_QUAN_TRI);
            var model = new DonViModel() { };
            model.IsThongBaoChuyenMa = isChuyenMa;
            return View(model);
        }

        public virtual async Task<IActionResult> CanhBaoChung()
        {
            // Cảnh báo không lưu trong Database
            var cauHinhCanhBaoTaiSans = (await _itemService.CountTaiSanCanhBaoChung(_workContext.CurrentDonVi.ID)).ToList();
            // Cảnh báo đơn vị cấu hình trong database
            var cauHinhFromDonVi = await _itemService.CountTaiSanCanhBaoFromCauHinhDonVi(_workContext.CurrentDonVi.ID);
            // add vào
            if (cauHinhFromDonVi != null)
            {
                cauHinhCanhBaoTaiSans.AddRange(cauHinhFromDonVi);
            }
            return View(cauHinhCanhBaoTaiSans);
        }

        public virtual async Task<IActionResult> IsHasCanhBao()
        {
            var _res = await _itemService.IsHasCanhBao(_workContext.CurrentDonVi.ID);
            if (_res)
                return JsonSuccessMessage();
            else
                return JsonErrorMessage();
        }

        #endregion Cập nhập tài sản nhận về

        #region Danh sách tài sản thay đổi địa bàn

        public virtual IActionResult ListTaiSanThayDoiDiaBan(decimal? loaiLyDoBienDong = 0, bool? isDanhSachTaiSanDuAn = false)
        {
            //if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDTangGiamHangNam)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu)
            //    )
            //    return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanSearchModel();
            if (loaiLyDoBienDong.HasValue && loaiLyDoBienDong > 0)
            {
                searchmodel.Loai_Ly_Do_ID = loaiLyDoBienDong.Value;
            }
            //searchmodel.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.DA_DUYET;
            var hasDonViQLDA = _donviService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
            if (hasDonViQLDA)
            {
                var donvi = _donViModelFactory.GetDonViWithConditions(donViId: _workContext.CurrentDonVi.ID, isBanQuanLyDuAn: hasDonViQLDA, isCreateTSDA: isDanhSachTaiSanDuAn);
                searchmodel.donviId = donvi.ID;
            }
            else
            {
                searchmodel.donviId = _workContext.CurrentDonVi.ID;
            }
            searchmodel.IsDanhSachTaiSanDuAn = isDanhSachTaiSanDuAn;

            var model = _itemModelFactory.PrepareTaiSanSearchModel(searchmodel);
            var listLoaiHinhTaiSanId = new List<int>() { (int)enumLOAI_HINH_TAI_SAN.DAT, (int)enumLOAI_HINH_TAI_SAN.NHA, (int)enumLOAI_HINH_TAI_SAN.ALL };
            model.ddlLoaiHinhTaiSanDatNha = model.LoaiHinhTaiSanAvailable.Where(c => listLoaiHinhTaiSanId.Contains(int.Parse(c.Value))).ToList();
            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> ListTaiSanThayDoiDiaBan(TaiSanSearchModel searchModel)
        {
            //if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDTangGiamHangNam)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu)
            //    )
            //    return AccessDeniedKendoGridJson();
            ////prepare model
            if (searchModel.PageSize == 0) searchModel.PageSize = 10;
            searchModel.NguoiTaoId = _workContext.CurrentCustomer.ID;
            var model = await _itemModelFactory.PrepareDanhSachTaiSanThayDoiDiaBan(searchModel);

            return Json(model);
        }

        #endregion Danh sách tài sản thay đổi địa bàn

        #region Danh sách tài sản sai hiện trạng

        public virtual async Task<IActionResult> ListTaiSanSaiHienTrang(decimal? loaiLyDoBienDong = 0, bool? isDanhSachTaiSanDuAn = false)
        {
            //if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDTangGiamHangNam)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu)
            //    )
            //    return AccessDeniedView();
            ////prepare model
            var searchmodel = new TaiSanSearchModel();
            if (loaiLyDoBienDong.HasValue && loaiLyDoBienDong > 0)
            {
                searchmodel.Loai_Ly_Do_ID = loaiLyDoBienDong.Value;
            }
            //searchmodel.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.DA_DUYET;
            var hasDonViQLDA = _donviService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
            if (hasDonViQLDA)
            {
                var donvi = _donViModelFactory.GetDonViWithConditions(donViId: _workContext.CurrentDonVi.ID, isBanQuanLyDuAn: hasDonViQLDA, isCreateTSDA: isDanhSachTaiSanDuAn);
                searchmodel.donviId = donvi.ID;
            }
            else
            {
                searchmodel.donviId = _workContext.CurrentDonVi.ID;
            }
            searchmodel.IsDanhSachTaiSanDuAn = isDanhSachTaiSanDuAn;
            var model = _itemModelFactory.PrepareTaiSanSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> ListTaiSanSaiHienTrang(TaiSanSearchModel searchModel)
        {
            //if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDTangGiamHangNam)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu)
            //    )
            //    return AccessDeniedKendoGridJson();
            //prepare model
            if (searchModel.PageSize == 0) searchModel.PageSize = 10;
            searchModel.NguoiTaoId = _workContext.CurrentCustomer.ID;
            var model = await _itemModelFactory.PrepareDanhSachTaiSanSaiHienTrang(searchModel);

            return Json(model);
        }

        #endregion Danh sách tài sản sai hiện trạng

        #region Danh sách tài sản ô tô sai số chỗ ngồi

        public virtual async Task<IActionResult> ListTaiSanOtoSaiSoChoNgoi(decimal? loaiLyDoBienDong = 0, bool? isDanhSachTaiSanDuAn = false)
        {
            ////prepare model
            var searchmodel = new TaiSanSearchModel();
            if (loaiLyDoBienDong.HasValue && loaiLyDoBienDong > 0)
            {
                searchmodel.Loai_Ly_Do_ID = loaiLyDoBienDong.Value;
            }
            //searchmodel.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.DA_DUYET;
            searchmodel.LOAI_HINH_TAI_SAN_ID = (int)enumLOAI_HINH_TAI_SAN.OTO;
            var hasDonViQLDA = _donviService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
            if (hasDonViQLDA)
            {
                var donvi = _donViModelFactory.GetDonViWithConditions(donViId: _workContext.CurrentDonVi.ID, isBanQuanLyDuAn: hasDonViQLDA, isCreateTSDA: isDanhSachTaiSanDuAn);
                searchmodel.donviId = donvi.ID;
            }
            else
            {
                searchmodel.donviId = _workContext.CurrentDonVi.ID;
            }
            searchmodel.IsDanhSachTaiSanDuAn = isDanhSachTaiSanDuAn;
            var model = _itemModelFactory.PrepareTaiSanSearchModel(searchmodel);
            var listLoaiHinhTaiSanId = new List<int>() { (int)enumLOAI_HINH_TAI_SAN.OTO };
            model.ddlLoaiHinhTaiSanOto = model.LoaiHinhTaiSanAvailable.Where(c => listLoaiHinhTaiSanId.Contains(int.Parse(c.Value))).ToList();
            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> ListTaiSanOtoSaiSoChoNgoi(TaiSanSearchModel searchModel)
        {
            if (searchModel.PageSize == 0) searchModel.PageSize = 10;
            searchModel.NguoiTaoId = _workContext.CurrentCustomer.ID;
            var model = await _itemModelFactory.PrepareDanhSachOtoSaiChoNgoi(searchModel);
            return Json(model);
        }

        #endregion Danh sách tài sản ô tô sai số chỗ ngồi
        #region

        public virtual async Task<IActionResult> ListTaiSanChuaTinhHaoMon(decimal? loaiLyDoBienDong = 0, bool? isDanhSachTaiSanDuAn = false)
        {
            ////prepare model
            var searchmodel = new TaiSanSearchModel();
            if (loaiLyDoBienDong.HasValue && loaiLyDoBienDong > 0)
            {
                searchmodel.Loai_Ly_Do_ID = loaiLyDoBienDong.Value;
            }
            //searchmodel.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.DA_DUYET;
            //searchmodel.LOAI_HINH_TAI_SAN_ID = (int)enumLOAI_HINH_TAI_SAN.OTO;
            var hasDonViQLDA = _donviService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
            if (hasDonViQLDA)
            {
                var donvi = _donViModelFactory.GetDonViWithConditions(donViId: _workContext.CurrentDonVi.ID, isBanQuanLyDuAn: hasDonViQLDA, isCreateTSDA: isDanhSachTaiSanDuAn);
                searchmodel.donviId = donvi.ID;
            }
            else
            {
                searchmodel.donviId = _workContext.CurrentDonVi.ID;
            }
            searchmodel.IsDanhSachTaiSanDuAn = isDanhSachTaiSanDuAn;
            var model = _itemModelFactory.PrepareTaiSanSearchModel(searchmodel);
            //var listLoaiHinhTaiSanId = new List<int>() { (int)enumLOAI_HINH_TAI_SAN.OTO };
            //model.ddlLoaiHinhTaiSanOto = model.LoaiHinhTaiSanAvailable.Where(c => listLoaiHinhTaiSanId.Contains(int.Parse(c.Value))).ToList();
            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> ListTaiSanChuaTinhHaoMon(TaiSanSearchModel searchModel)
        {
            if (searchModel.PageSize == 0) searchModel.PageSize = 10;
            searchModel.NguoiTaoId = _workContext.CurrentCustomer.ID;
            var model = await _itemModelFactory.PrepareDanhSachTaiSanChuaTinhHaoMon(searchModel);
            return Json(model);
        }

        #endregion
        #region Danh sách tài sản nguyên giá dưới 10tr

        /// <summary>
        /// Nếu đơn vị có quản lý ts quản lý như tscđ (check trong cấu hình đơn vị) thì sẽ lọc ra tài sản nguyên giá < 5.000.000
        /// Nếu không thì sẽ lọc nguyên giá < 10.000.000
        /// </summary>
        /// <param name="loaiLyDoBienDong"></param>
        /// <param name="isDanhSachTaiSanDuAn"></param>
        /// <returns></returns>
        public virtual IActionResult ListTaiSanNguyenGiaDuoi10Trieu(decimal? loaiLyDoBienDong = 0, bool? isDanhSachTaiSanDuAn = false)
        {
            //if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDTangGiamHangNam)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu)
            //    )
            //    return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanSearchModel();
            if (loaiLyDoBienDong.HasValue && loaiLyDoBienDong > 0)
            {
                searchmodel.Loai_Ly_Do_ID = loaiLyDoBienDong.Value;
            }
            //searchmodel.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.DA_DUYET;
            var hasDonViQLDA = _donviService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
            if (hasDonViQLDA)
            {
                var donvi = _donViModelFactory.GetDonViWithConditions(donViId: _workContext.CurrentDonVi.ID, isBanQuanLyDuAn: hasDonViQLDA, isCreateTSDA: isDanhSachTaiSanDuAn);
                searchmodel.donviId = donvi.ID;
            }
            else
            {
                searchmodel.donviId = _workContext.CurrentDonVi.ID;
            }
            searchmodel.IsDanhSachTaiSanDuAn = isDanhSachTaiSanDuAn;

            var model = _itemModelFactory.PrepareTaiSanSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> ListTaiSanNguyenGiaDuoi10Trieu(TaiSanSearchModel searchModel)
        {
            //if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDTangGiamHangNam)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLTaiSan)
            //    && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu)
            //    )
            //    return AccessDeniedKendoGridJson();
            //prepare model
            if (searchModel.PageSize == 0) searchModel.PageSize = 10;
            searchModel.NguoiTaoId = _workContext.CurrentCustomer.ID;
            var model = await _itemModelFactory.PrepareDanhSachTaiSanSaiDuoi10Trieu(searchModel);

            return Json(model);
        }

        #endregion

        #region Danh sách tài sản nhận điều chuyển

        public virtual async Task<IActionResult> ListTaiSanNhanDieuChuyen(decimal? loaiLyDoBienDong = 0, bool? isDanhSachTaiSanDuAn = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDTangGiamHangNam)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu)
                )
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanSearchModel();
            if (loaiLyDoBienDong.HasValue && loaiLyDoBienDong > 0)
            {
                searchmodel.Loai_Ly_Do_ID = loaiLyDoBienDong.Value;
            }
            //searchmodel.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.DA_DUYET;
            var hasDonViQLDA = _donviService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
            if (hasDonViQLDA)
            {
                var donvi = _donViModelFactory.GetDonViWithConditions(donViId: _workContext.CurrentDonVi.ID, isBanQuanLyDuAn: hasDonViQLDA, isCreateTSDA: isDanhSachTaiSanDuAn);
                searchmodel.donviId = donvi.ID;
            }
            else
            {
                searchmodel.donviId = _workContext.CurrentDonVi.ID;
            }
            searchmodel.IsDanhSachTaiSanDuAn = isDanhSachTaiSanDuAn;

            var model = _itemModelFactory.PrepareTaiSanSearchModel(searchmodel);
            var dv = _donviService.GetDonViById(_workContext.CurrentDonVi.ID);

            model.isNhapLieu = dv.LA_DON_VI_NHAP_LIEU ?? false;
            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> ListTaiSanNhanDieuChuyen(TaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDThayDoiThongTin)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDTangGiamHangNam)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDGiamTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDChoThueTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLTaiSan)
                && !_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu)
                )
                return AccessDeniedKendoGridJson();
            //prepare model
            if (searchModel.PageSize == 0) searchModel.PageSize = 10;
            searchModel.NguoiTaoId = _workContext.CurrentCustomer.ID;
            var model = await _itemModelFactory.PrepareDanhSachTaiSanNhanDieuChuyen(searchModel);

            return Json(model);
        }

        #endregion

        #region Chuyển quyền xử lý tài sản

        public virtual IActionResult ListQuyenXuLyTaiSan()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLCongCuChuyenQuyenXuLyTaiSan))
                return AccessDeniedView();
            var model = _itemModelFactory.PrepareQuyenXuLyTaiSanSearchModel(new TaiSanSearchModel());
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult ListQuyenXuLyTaiSan(TaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLCongCuChuyenQuyenXuLyTaiSan))
                return AccessDeniedKendoGridJson();
            //prepare model
            if (searchModel.PageSize == 0) searchModel.PageSize = 10;
            searchModel.isDuyet = true; // check hiển thị tài sản đã giảm toàn bộ hay không
            var model = _itemModelFactory.PrepareDanhSachTaiSan(searchModel);
            HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch + searchModel.GetType().Name, searchModel);
            return Json(model);
        }

        public virtual IActionResult ChuyenTaiSanTheoUser(decimal fromUser, decimal toUser, decimal donViId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLCongCuChuyenQuyenXuLyTaiSan))
                return JsonErrorMessage("Không có quyền truy cập");
            var taiSans = _itemService.GetTaiSanByNguoiTao(fromUser, donViId);
            if (taiSans != null && taiSans.Count > 0)
                foreach (var taiSan in taiSans)
                    _thaoTacBienDongModelFactory.CapNhatQuyenXuLyTaiSan(taiSan, toUser);
            return JsonSuccessMessage();
        }

        public virtual IActionResult ChuyenQuyenXuLyTaiSanTheoId(string Ids, decimal toUser)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLCongCuChuyenQuyenXuLyTaiSan))
                return JsonErrorMessage("Không có quyền truy cập");
            var taiSanIds = Ids.Split('-').Select(p => decimal.Parse(p)).ToArray();
            var taiSans = _itemService.GetTaiSanByIds(taiSanIds);
            if (taiSans != null && taiSans.Count > 0)
                foreach (var taiSan in taiSans)
                    _thaoTacBienDongModelFactory.CapNhatQuyenXuLyTaiSan(taiSan, toUser);
            return JsonSuccessMessage();
        }

        #endregion

        #region Khai thác tài sản

        public virtual IActionResult ListTaiSanKhaiThac()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedView();
            var searchmodel = new TaiSanSearchModel();
            var model = _itemModelFactory.PrepareTaiSanSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult ListTaiKhaiThac(TaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedView();

            var model = _itemModelFactory.PrepareTaiSanKhaiThacListModel(searchModel);
            return View(model);
        }

        #endregion

        #region tool

        public virtual IActionResult Tool_NhanBan()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMuc))
                return AccessDeniedView();
            //prepare model
            var model = new TaiSanModel();
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Tool_NhanBan(Decimal taiSanId, Decimal soLuongNhanBan)
        {
            if (soLuongNhanBan > 0)
            {
                var ts = _itemService.GetTaiSanById(taiSanId);
                if (ts != null)
                {
                    var guid = Guid.NewGuid();
                    var logComment = guid + " Tool NBTS bắt đầu: " + DateTime.Now;
                    _hoatdongService.InsertHoatDong(systemKeyword: "NhanBanTool", comment: logComment, entity: ts.ToModel<TaiSanModel>(), DoiTuong: "TaiSan");
                    Tool_NhanBanTaiSan(_idClone: taiSanId, _soLuong: soLuongNhanBan, donViId: _workContext.CurrentDonVi.ID);
                    logComment = guid + " Tool NBTS kết thúc: " + DateTime.Now;
                    _hoatdongService.InsertHoatDong(systemKeyword: "NhanBan", comment: logComment, entity: ts.ToModel<TaiSanModel>(), DoiTuong: "TaiSan");
                    return JsonSuccessMessage("Đã tạo " + soLuongNhanBan + " bản ghi thành công!");
                }
                else
                {
                    return JsonErrorMessage("Error");
                }
            }
            return JsonErrorMessage("Error");
        }

        /// <summary>
        /// Description: Function thực hiện chức năng nhân bản tài sản
        /// </summary>
        /// <param name="_idClone">id của tài sản được nhân bản</param>
        /// <param name="_soLuong">số lượng nhân bản</param>
        public void Tool_NhanBanTaiSan(decimal _idClone, decimal _soLuong, decimal donViId)
        {
            if (_soLuong < 1)
                return;
            //lay thông tin nhân bản

            var tsClone = _itemService.GetTaiSanById(_idClone);
            var _listTSIdClone = new List<TaiSan>();
            for (int i = 0; i < _soLuong; i++)
            {
                DonVi donVi = new DonVi();
                if (donViId > 0)
                {
                    var donViList = _donviService.GetListDonViChild(parentId: donViId).Where(c => c.LA_DON_VI_NHAP_LIEU == true).ToList();
                    var rand = new Random();
                    donVi = donViList[rand.Next(donViList.Count)];
                }
                else
                {
                    donVi = _donviService.GetDonViById(tsClone.DON_VI_ID ?? 0);
                }
                tsClone.DON_VI_ID = donVi.ID;

                string maLoaiTs = "";
                if (tsClone.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
                {
                    var loaiTSVoHinh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(tsClone.LOAI_TAI_SAN_DON_VI_ID ?? 0);
                    maLoaiTs = loaiTSVoHinh.MA;
                }
                else
                {
                    var loaiTs = _loaitaisanService.GetLoaiTaiSanById(tsClone.LOAI_TAI_SAN_ID ?? 0);
                    maLoaiTs = loaiTs.MA;
                }
                var _newTS = new TaiSan(tsClone);
                _newTS.ID = 0;
                _newTS.MA = null;
                _newTS.MA_DB = null;
                _newTS.MA_QLDKTS40 = null;
                _newTS.GUID = Guid.NewGuid();
                _newTS.TEN = _newTS.TEN + " cpy" + (i + 1);
                _itemService.InsertTaiSan(_newTS);
                //update Mã tài sản
                _newTS.MA = CommonHelper.GenMaTaiSan(donVi.MA, maLoaiTs, _newTS.ID);
                _itemService.UpdateTaiSan(_newTS);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Nhân bản thông tin ", _newTS.ToModel<TaiSanModel>(), "TaiSan");
                _listTSIdClone.Add(_newTS);
            }

            //Lấy thông tin yêu cầu tài sản
            //Lấy thông tin yêu cầu tài sản
            //sao chép yêu cầu hoặc biến động
            var yeuCau = new YeuCau();
            var yeuCauCT = new YeuCauChiTiet();
            if (tsClone.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET || tsClone.TRANG_THAI_ID == (int)enumTRANG_THAI_TAI_SAN.DA_DUYET_GIAM_TOAN_BO)
            {
                //Tài sản đã duyệt lấy thông tin từ biến động
                var _bienDongCP = _biendongService.GetBienDongCuNhatByTaiSanId(tsClone.ID);
                var _bienDongCTCP = _biendongchitietService.GetBienDongChiTietByBDId(_bienDongCP.ID);
                yeuCau = _yeucauModelFactory.PrepareYeuCauFromBienDong(_bienDongCP, new YeuCau(), true);
                yeuCauCT = _yeuCauChiTietModelFactory.PrepareYCCTFromBDCT(_bienDongCTCP, new YeuCauChiTiet(), true);
            }
            else
            {
                //Tài sản chưa duyệt lấy thông tin từ yêu cầu
                yeuCau = _yeucauService.GetYeuCauCuNhatByTSId(tsClone.ID);
                yeuCauCT = _yeucauchitietService.GetYeuCauChiTietByYeuCauId(yeuCau.ID);
            }
            foreach (var _newTS in _listTSIdClone)
            {
                //nhân bản yêu cầu
                var _newYeuCau = new YeuCau(yeuCau);
                _newYeuCau.ID = 0;
                _newYeuCau.GUID = new Guid();
                _newYeuCau.TAI_SAN_ID = _newTS.ID;
                _newYeuCau.TAI_SAN_MA = _newTS.MA;
                _newYeuCau.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                _newYeuCau.DON_VI_ID = _newTS.DON_VI_ID;
                //Gán lại đơn vị id ngẫu nhiên

                _yeucauService.InsertYeuCau(_newYeuCau);
                var _newYeuCauChiTiet = new YeuCauChiTiet(yeuCauCT);
                _newYeuCauChiTiet.YEU_CAU_ID = _newYeuCau.ID;
                _yeucauchitietService.InsertYeuCauChiTiet(_newYeuCauChiTiet);
                //Ghi log nhật ký yêu cầu
                _yeucaunhatkyModelFactory.InsertYeuCauNhatKy(_newYeuCau.ToModel<YeuCauModel>(), enumLOAI_NHATKY_YEUCAU.THEM_MOI);
                //Nhân bản tài sản chi tiết theo từng loại hình
                switch (tsClone.LOAI_HINH_TAI_SAN_ID)
                {
                    case (int)enumLOAI_HINH_TAI_SAN.DAT:
                        var tsDat = _taisandatService.GetTaiSanDatByTaiSanId(_idClone);
                        var _itemDatClone = new TaiSanDat(tsDat);
                        _itemDatClone.TAI_SAN_ID = _newTS.ID;
                        _taisandatService.InsertTaiSanDat(_itemDatClone);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.NHA:
                        var tsNha = _taisannhaService.GetTaiSanNhaByTaiSanId(_idClone);
                        var _itemNhaClone = new TaiSanNha(tsNha);
                        _itemNhaClone.TAI_SAN_ID = _newTS.ID;
                        _taisannhaService.InsertTaiSanNha(_itemNhaClone);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                    case (int)enumLOAI_HINH_TAI_SAN.OTO:
                        var tsOto = _taisanOtoService.GetTaiSanOtoByTaiSanId(_idClone);
                        var _itemOtoClone = new TaiSanOto(tsOto);
                        _itemOtoClone.TAI_SAN_ID = _newTS.ID;
                        _itemOtoClone.BIEN_KIEM_SOAT = " ";
                        _taisanOtoService.InsertTaiSanOto(_itemOtoClone);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                        var tsCLN = _taisanClnService.GetTaiSanClnByTaiSanId(_idClone);
                        var _itemCLNClone = new TaiSanCln(tsCLN);
                        _itemCLNClone.TAI_SAN_ID = _newTS.ID;
                        _taisanClnService.InsertTaiSanCln(_itemCLNClone);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                    case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                        var tsMMTB = _taisanmaymocService.GetTaiSanMaymocByTaiSanId(_idClone);
                        var _itemMMTBClone = new TaiSanMayMoc(tsMMTB);
                        _itemMMTBClone.TAI_SAN_ID = _newTS.ID;
                        _taisanmaymocService.InsertTaiSanMayMoc(_itemMMTBClone);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                        var itemVKTCP = _taisanVKTService.GetTaiSanVktByTaiSanId(_idClone);
                        var _itemVKTClone = new TaiSanVkt(itemVKTCP);
                        _itemVKTClone.TAI_SAN_ID = _newTS.ID;
                        _taisanVKTService.InsertTaiSanVkt(_itemVKTClone);
                        break;

                    case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                        var itemVH = _taiSanVoHinhService.GetTaiSanVoHinhByTaiSanId(_idClone);
                        var _itemVHClone = new TaiSanVoHinh(itemVH);
                        _itemVHClone.TAI_SAN_ID = _newTS.ID;
                        _taiSanVoHinhService.InsertTaiSanVoHinh(_itemVHClone);
                        break;

                    default:
                        break;
                }
                //tự động duyệt
                if (_itemModelFactory.CheckAutoDuyet(_newYeuCau.LOAI_HINH_TAI_SAN_ID, _newTS.NGUYEN_GIA_BAN_DAU))
                    _thaoTacBienDongModelFactory.DuyetYeuCauFunc(yeuCau.ID, _newYeuCau);
            }
        }

        // tool xóa tài sản chạy procedure
        public IActionResult Tool_XoaTaiSan()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLXoaTaiSan))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanSearchModel();
            var model = _itemModelFactory.PrepareTaiSanSearchModel(searchmodel, true);
            return View(model);
        }

        [HttpPost]
        public IActionResult Tool_XoaTaiSan(ToolXoaTaiSanModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLXoaTaiSan))
                return AccessDeniedView();
            var donvi = _donviService.GetDonViById((model.DON_VI_ID != null ? (decimal)model.DON_VI_ID : 0));
            if (donvi != null)
            {
                var toolmodel = new ToolXoaTaiSanModel() { DON_VI_ID = donvi.ID, DON_VI_MA = donvi.MA, NGUOI_DUNG_ID = _workContext.CurrentCustomer.ID, NGAY_XOA = DateTime.Now, IS_XOA_CON = model.IS_XOA_CON, NGAY_XOA_XONG = null };
                var hd = _hoatdongService.InsertHoatDong(enumHoatDong.XoaTaiSanTheoDonVi, "Xóa tài sản theo đơn vị", toolmodel, "ToolXoaTaiSanModel");
                //call store để xóa tài sản
                var result = _itemService.XoaTaiSanByDonViId(DonViMa: toolmodel.DON_VI_MA, IsDeleteDVCon: toolmodel.IS_XOA_CON, NgayTao: toolmodel.NGAY_XOA);
                //update hoạt động khi xóa xong
                if (result)
                {
                    toolmodel.NGAY_XOA_XONG = DateTime.Now;
                    hd.DU_LIEU = toolmodel.toStringJson();
                    _hoatdongService.UpdateHoatDong(hd);
                    return JsonSuccessMessage("Cập nhật thành công");
                }
                else
                {
                    return JsonErrorMessage("Cập nhật không thành công");
                }
            }
            return JsonErrorMessage("Cập nhật không thành công");
        }

        public void XoaTaiSanChayNgam(ToolXoaTaiSanModel toolmodel, decimal ID_HOAT_DONG)
        {
            //call store để xóa tài sản
            var result = _itemService.XoaTaiSanByDonViId(DonViMa: toolmodel.DON_VI_MA, IsDeleteDVCon: toolmodel.IS_XOA_CON, NgayTao: toolmodel.NGAY_XOA);
            //update hoạt động khi xóa xong
            if (result)
            {
                var hd = _hoatdongService.GetHoatDongById(ID_HOAT_DONG);
                toolmodel.NGAY_XOA_XONG = DateTime.Now;
                hd.DU_LIEU = toolmodel.toStringJson();
                _hoatdongService.UpdateHoatDong(hd);
            }
        }

        #endregion tool

        #region Import Excel

        private List<string> ValidateListImportTaiSan(ImportExcelTaiSanModel model)
        {
            string Error = string.Empty;
            if (string.IsNullOrEmpty(model.TEN)) Error = String.Concat(Error, $" - Tên không được để trống");
            if (string.IsNullOrEmpty(model.DON_VI_MA)) Error = String.Concat(Error, $" - Mã đơn vị không được để trống");
            if (model.NGUYEN_GIA == null) Error = String.Concat(Error, $" - Nguyên giá không được để trống");
            if (model.NGAY_SU_DUNG == null) Error = String.Concat(Error, $" - Ngày sử dụng không được để trống");
            if (model.NGAY_TANG == null) Error = String.Concat(Error, $" - Ngày tăng không được để trống");
            if (string.IsNullOrEmpty(model.LY_DO_TANG_MA)) Error = String.Concat(Error, $" - Lý do tăng không được để trống");
            if (string.IsNullOrEmpty(model.LOAI_TAI_SAN_MA))
            {
                Error = String.Concat(Error, $" - Mã loại không được để trống");
            }
            else
            {
                var loaihinhtaisanid = Convert.ToDecimal(model.LOAI_TAI_SAN_MA.Substring(0, 1));
                if (loaihinhtaisanid == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
                {
                    /*if (string.IsNullOrEmpty(model.TAI_SAN_DAT_MA))
                    {
                        Error = String.Concat(Error, $" - Mã tài sản đất không được để trống");
                        countError++;
                        //ModelState.AddModelError($"Row_{model.Row}", "Mã tài sản đất không được để trống");
                    }*/
                    if (model.DIEN_TICH_DATNHA == null) Error = String.Concat(Error, $" - Diện tích đất không được để trống");
                    if (string.IsNullOrEmpty(model.DIA_CHI)) Error = String.Concat(Error, $" - Địa chỉ không được để trống");
                }
                else if (loaihinhtaisanid == (decimal)enumLOAI_HINH_TAI_SAN.NHA)
                {
                    if (model.DIEN_TICH_DATNHA == null) Error = String.Concat(Error, $" - Diện tích nhà không được để trống");
                    if (model.SO_TANG == null) Error = String.Concat(Error, $" - Số tầng không được để trống");
                    if (string.IsNullOrEmpty(model.DIA_CHI)) Error = String.Concat(Error, $" - Địa chỉ không được để trống");
                    if (model.TONG_DT_SAN_XD == null) Error = String.Concat(Error, $" - Diện tích sàn xây dựng không được để trống");
                }
                else if (loaihinhtaisanid == (decimal)enumLOAI_HINH_TAI_SAN.OTO)
                {
                    if (string.IsNullOrEmpty(model.BIEN_KIEM_SOAT)) Error = String.Concat(Error, $" - Biển kiểm soát không được để trống");
                    if (string.IsNullOrEmpty(model.NHAN_XE)) Error = String.Concat(Error, $" - Nhãn xe không được để trống");
                    if (model.SO_CHO_NGOI == null)  Error = String.Concat(Error, $" - Số chỗ ngồi không được để trống");
                    if (model.SO_CAU_XE == null)
                        Error = String.Concat(Error, $" - Số cầu xe không được để trống");
                }
            }
            return string.IsNullOrEmpty(Error) ? new List<string>(): new List<string>() { $"Hàng {model.Row}:" + Error };
        }

        private List<string> ValidateListImportMaTaiSanCu (ImportExcelMaTaiSanCuModel model)
        {
            List<string> ListError = new List<string>();
            string Error = $"Hàng {model.Row}:";
            int countError = 0;
            if (string.IsNullOrEmpty(model.DON_VI_MA))
            {
                Error = String.Concat(Error, $" - Mã đơn vị không được để trống");
                countError++;
            }
            if (string.IsNullOrEmpty(model.TAI_SAN_MA_TSC))
            {
                Error = String.Concat(Error, $" - Mã TSC không được để trống");
                countError++;
            }
            if (string.IsNullOrEmpty(model.TAI_SAN_MA_CU))
            {
                Error = String.Concat(Error, $" - Mã cũ không được để trống");
                countError++;
            }
            if (countError > 0)
            {
                ListError.Add(Error);
            }
            return ListError;
        }

        private List<string> ListErrorTaiSans(ImportExcelTaiSan item)
        {
            List<string> ListError = new List<string>();
            //var loaitaisan = _loaitaisanService.GetLoaiTaiSanByMa(item.LOAI_TAI_SAN_MA);
            string Error = $"{item.TEN} :";
            int countError = 0;

            var loaihinhtaisanid = Convert.ToDecimal(item.LOAI_TAI_SAN_MA.Substring(0, 1));
            if (loaihinhtaisanid != (int)enumLOAI_HINH_TAI_SAN.VO_HINH && loaihinhtaisanid != (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
            {
                var loaitaisan = _loaitaisanService.GetLoaiTaiSanByMa(item.LOAI_TAI_SAN_MA);
                if (loaitaisan == null)
                {
                    Error = String.Concat(Error, $" - LOAI_TAI_SAN_MA không tồn tại");
                    countError++;
                }
            }
            else
            {
                if (item.LOAI_TAI_SAN_MA != "9" && item.LOAI_TAI_SAN_MA != "10" && item.LOAI_TAI_SAN_MA != "902" && item.LOAI_TAI_SAN_MA != "903" && item.LOAI_TAI_SAN_MA != "904" && item.LOAI_TAI_SAN_MA != "905" && item.LOAI_TAI_SAN_MA != "906")
                {
                    var donViLonNhat = _donviService.GetDonViLonNhatByMa(item.DON_VI_MA);
                    var loaitaisandv = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMaAndDonVi(item.LOAI_TAI_SAN_MA, donViLonNhat?.ID ?? 0);
                    if (loaitaisandv == null)
                    {
                        Error = String.Concat(Error, $" - LOAI_TAI_SAN_MA không tồn tại");
                        countError++;
                    }
                }
            }
            var donvi = _donviService.GetDonViByMa(item.DON_VI_MA);
            if (donvi == null)
            {
                Error = String.Concat(Error, $" - DON_VI_MA không tồn tại");
                countError++;
            }
            var lydobiendong = _lydobiendongService.GetLyDoBienDongByMa(item.LY_DO_TANG_MA);
            if (lydobiendong == null)
            {
                Error = String.Concat(Error, $" - LY_DO_TANG_MA không tồn tại");
                countError++;
            }
            if (item.HINH_THUC_MUA_SAM != null)
            {
                var hinhThucMuaSam = _hinhThucMuaSamService.GetHinhThucMuaSamByMa(item.HINH_THUC_MUA_SAM);
                if (hinhThucMuaSam == null)
                {
                    Error = String.Concat(Error, $" - HINH_THUC_MUA_SAM không tồn tại");
                    countError++;
                }
            }
            if (item.DON_VI_MUA_SAM != null)
            {
                var donvimuasam = _donviService.GetDonViByMa(item.DON_VI_MUA_SAM);
                if (donvimuasam == null)
                {
                    Error = String.Concat(Error, $" - DON_VI_MUA_SAM không tồn tại");
                    countError++;
                }
            }
            if (item.NUOC_SX != null)
            {
                var quocGia = _quocGiaService.GetQuocGiaById(Convert.ToInt32(item.NUOC_SX));
                if (quocGia == null)
                {
                    Error = String.Concat(Error, $" - NUOC_SX không tồn tại");
                    countError++;
                }
            }
            if (countError > 0)
            {
                ListError.Add(Error);
            }
            return ListError;
        }

        public virtual IActionResult ImportTaiSanExcel()
        {
            return View();
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public virtual IActionResult ImportTaiSanExcel(IFormFile file)
        {
            if (file == null)
            {
                ErrorNotification("Bạn chưa Nhập file Import");
                // return RedirectToAction("DongBoTaiSan");
            }
            List<int> listRowIdentical = new List<int>();
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(file.OpenReadStream());
            //  lưu file
            string _fileStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), DateTime.Now.ToPathFolderStore(), Guid.NewGuid().ToString() + ".xlsx");
            workbook.SaveToFile(_fileStore, ExcelVersion.Version2013);
            var fileInfo = new FileInfo(_fileStore);
            var package = new ExcelPackage(fileInfo);
            ExcelWorksheets workSheets = package.Workbook.Worksheets;
            List<MessageReturn> ListResult = new List<MessageReturn>();
            if (workSheets.Count < 1)
            {
                //ErrorMessageJson("File không chuẩn dữ liệu");
                return null;
            }
            ExcelWorksheet taiSan = workSheets.First();
            List<ExcelModel> lstSettings = new List<ExcelModel>();
            string strSettings = _fileProvider.ReadAllText(_fileProvider.MapPath("~/App_Data/jsonSetting_ImportExcel_TaiSan.json"), Encoding.UTF8);
            lstSettings = strSettings.toEntities<ExcelModel>();
            int maxCol = lstSettings != null ? lstSettings.Max(c => c.COL) : 0;
            if (maxCol < taiSan.Dimension.End.Column || taiSan.Dimension.End.Column < maxCol)
                ErrorNotification("Thông tin tài sản không đúng định dạng");
            List<ImportExcelTaiSanModel> lstImport = new List<ImportExcelTaiSanModel>();
            List<string> ListErr = new List<string>();
            List<string> ListExists = new List<string>();
            List<string> unionList = new List<string>();
            for (int rowNumber = 3; rowNumber <= taiSan.Dimension.End.Row; rowNumber++)
            {
                ExcelRange row = taiSan.Cells[rowNumber, 1, rowNumber, taiSan.Dimension.End.Column];
                ImportExcelTaiSanModel item = new ImportExcelTaiSanModel();
                item = row.toEntity<ImportExcelTaiSanModel>(item, rowNumber, lstSettings);
                var rows = rowNumber;
                item.Row = rows;
                /*ValidateListImportTaiSan(item);
                if(ModelState.IsValid)
                {
                    var checkTen = lstImport.Where(c => c.DON_VI_MA == item.DON_VI_MA && c.TEN == item.TEN).ToList();
                    if (checkTen.Count() == 0)
                    {
                        lstImport.Add(item);
                    }
                    else
                    {
                        listRowIdentical.Add(rows);
                    }
                }*/
                var rs = ValidateListImportTaiSan(item);
                if (rs.Count > 0)
                {
                    unionList = unionList.Union(rs).ToList();
                    continue;
                }
                else
                {
                    /*var checkTen = lstImport.Where(c => c.DON_VI_MA == item.DON_VI_MA && c.TEN == item.TEN).ToList();
                    if (checkTen.Count() == 0)
                    {*/
                    lstImport.Add(item);
                    /*}
                    else
                    {
                        listRowIdentical.Add(rows);
                    }*/
                }
            }
            if (unionList.Count > 0)
            {
                var Error = string.Join("; ", unionList);
                return JsonErrorMessage("Thông tin tài sản trong excel chưa đúng!", Error);
            }
            int soTsImport = 0;
            foreach (var item in lstImport)
            {
                try
                {
                    TaiSanModel model = new TaiSanModel();
                    ImportExcelTaiSan ts = new ImportExcelTaiSan();
                    BienDongModel bd = new BienDongModel();
                    BienDongChiTietModel bdct = new BienDongChiTietModel();
                    YeuCau yeuCau = new YeuCau();
                    ts = item.ToEntity<ImportExcelTaiSan>();
                    ts.LOAI_TAI_SAN_MA = item.LOAI_TAI_SAN_MA.Split(new string[] { "-" }, StringSplitOptions.None)[0];
                    ts.LY_DO_TANG_MA = item.LY_DO_TANG_MA.Split(new string[] { "-" }, StringSplitOptions.None)[0];
                    if (item.PHUONG_THUC_MUA_SAM != null)
                    {
                        ts.PHUONG_THUC_MUA_SAM = item.PHUONG_THUC_MUA_SAM.Split(new string[] { "-" }, StringSplitOptions.None)[0];
                    }
                    if (item.HINH_THUC_MUA_SAM != null)
                    {
                        ts.HINH_THUC_MUA_SAM = item.HINH_THUC_MUA_SAM.Split(new string[] { "-" }, StringSplitOptions.None)[0];
                    }
                    if (item.DON_VI_MUA_SAM != null)
                    {
                        ts.DON_VI_MUA_SAM = item.DON_VI_MUA_SAM.Split(new string[] { "-" }, StringSplitOptions.None)[0];
                    }
                    if (item.MA_HUYEN != null)
                    {
                        ts.MA_HUYEN = item.MA_HUYEN.Split(new string[] { "-" }, StringSplitOptions.None)[0];
                    }
                    if (item.MA_XA != null)
                    {
                        ts.MA_XA = item.MA_XA.Split(new string[] { "-" }, StringSplitOptions.None)[0];
                    }
                    if (item.HTSD_TAI_SAN_KHAC != null)
                    {
                        ts.HTSD_TAI_SAN_KHAC = item.HTSD_TAI_SAN_KHAC.Split(new string[] { "-" }, StringSplitOptions.None)[0];
                    }
                    if (item.CHUC_DANH_SU_DUNG != null)
                    {
                        ts.CHUC_DANH_SU_DUNG = item.CHUC_DANH_SU_DUNG.Split(new string[] { "-" }, StringSplitOptions.None)[0];
                    }
                    if (item.DONG_XE != null)
                    {
                        ts.DONG_XE = item.DONG_XE.Split(new string[] { "-" }, StringSplitOptions.None)[0];
                    }
                    if (item.NHAN_XE != null)
                    {
                        ts.NHAN_XE = item.NHAN_XE.Split(new string[] { "-" }, StringSplitOptions.None)[0];
                    }
                    if (item.NUOC_SX != null)
                    {
                        ts.NUOC_SX = item.NUOC_SX.Split(new string[] { "-" }, StringSplitOptions.None)[0];
                    }
                    /*var checkts = _taiSanImportService.GetTaiSanByDonViMa(ts.DON_VI_MA).Where(c => c.TEN == item.TEN && c.TRANG_THAI_ID ==1);
                    if (checkts.Count() == 0)
                    {*/
                    var rs = ListErrorTaiSans(ts);
                    if (rs.Count > 0)
                    {
                        unionList = unionList.Union(rs).ToList();
                        continue;
                    }
                    _taiSanImportService.InsertTaiSan(ts);
                    if (ts.SO_LUONG > 1)
                    {
                        ts.NGUYEN_GIA = ts.NGUYEN_GIA / ts.SO_LUONG;
                        ts.NV_NGAN_SACH = ts.NV_NGAN_SACH / ts.SO_LUONG;
                        ts.NV_QUY_HDSN = ts.NV_QUY_HDSN / ts.SO_LUONG;
                        ts.NV_VIEN_TRO = ts.NV_VIEN_TRO / ts.SO_LUONG;
                        ts.NV_KHAC = ts.NV_KHAC / ts.SO_LUONG;
                        ts.HAO_MON_LUY_KE = ts.HAO_MON_LUY_KE / ts.SO_LUONG;
                        ts.GIA_TRI_CON_LAI = ts.GIA_TRI_CON_LAI / ts.SO_LUONG;
                        //var Ten = ts.TEN;
                        for (int sl = 1; sl <= ts.SO_LUONG; sl++)
                        {
                            var result = _taiSanImportModelFactory.InsertToTaiSan(ts, model);
                            switch (result.LOAI_HINH_TAI_SAN_ID)
                            {
                                case (int)enumLOAI_HINH_TAI_SAN.DAT:
                                    var TsDat = new TaiSanDat();
                                    TsDat.DIEN_TICH = 0;
                                    TsDat.TAI_SAN_ID = result.ID;
                                    _taisandatService.InsertTaiSanDat(TsDat);
                                    //var itemTSDat = _taisandatService.GetTaiSanDatByTaiSanId(result.ID);
                                    //model.taisandatModel = _taisandatModelFactory.PrepareTaiSanDatModel(TsDat.ToModel<TaiSanDatModel>(), itemTSDat);
                                    //model.cohoso = true;
                                    break;

                                case (int)enumLOAI_HINH_TAI_SAN.NHA:
                                    var TsNha = new TaiSanNha();
                                    TsNha.TAI_SAN_ID = result.ID;
                                    TsNha.TAI_SAN_DAT_ID = 0;
                                    TsNha.DIEN_TICH_SAN_XAY_DUNG = 0;
                                    TsNha.DIEN_TICH_XAY_DUNG = 0;
                                    TsNha.NAM_XAY_DUNG = result.NAM_SAN_XUAT;
                                    TsNha.NGAY_SU_DUNG = null;
                                    _taisannhaService.InsertTaiSanNha(TsNha);
                                    //model.LOAI_HINH_TAI_SAN_ID = result.LOAI_HINH_TAI_SAN_ID;
                                    //var itemTsNha = _taisannhaService.GetTaiSanNhaByTaiSanId(result.ID);
                                    //model.taisannhaModel = _taiSanNhaModelFactory.PrepareTaiSanNhaModel(TsNha.ToModel<TaiSanNhaModel>(), itemTsNha);
                                    //model.taisannhaModel.isQuanLyDat = true;
                                    break;

                                case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                                case (int)enumLOAI_HINH_TAI_SAN.OTO:
                                    var TsOto = new TaiSanOto();
                                    TsOto.TAI_SAN_ID = result.ID;
                                    TsOto.BIEN_KIEM_SOAT = "-";
                                    _taisanOtoService.InsertTaiSanOto(TsOto);
                                    //model.taisanOtoModel = TsOto.ToModel<TaiSanOtoModel>();
                                    //var itemOto = _taisanOtoService.GetTaiSanOtoByTaiSanId(result.ID);
                                    //model.taisanOtoModel = _taiSanOtoModelFactory.PrepareTaiSanOtoModel(model.taisanOtoModel, itemOto);
                                    break;

                                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                                    var tsCayLauNam = new TaiSanCln();
                                    tsCayLauNam.TAI_SAN_ID = result.ID;
                                    _taisanClnService.InsertTaiSanCln(tsCayLauNam);
                                    //model.taisanClnModel = tsCayLauNam.ToModel<TaiSanClnModel>();
                                    //model.taisanClnModel = _taiSanClnModelFactory.PrepareTaiSanClnModel(model.taisanClnModel, tsCayLauNam);
                                    break;

                                case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                                case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                                    var tsMayMoc = new TaiSanMayMoc();
                                    tsMayMoc.TAI_SAN_ID = result.ID;
                                    _taisanmaymocService.InsertTaiSanMayMoc(tsMayMoc);
                                    //model.taisanmaymocModel = _taiSanMayMocModelFactory.PrepareTaiSanMayMocModel(model.taisanmaymocModel, tsMayMoc);
                                    break;

                                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                                    var tsVatKienTruc = new TaiSanVkt();
                                    tsVatKienTruc.TAI_SAN_ID = result.ID;
                                    _taisanVKTService.InsertTaiSanVkt(tsVatKienTruc);
                                    //model.taisanVktModel = _taiSanVktModelFactory.PrepareTaiSanVktModel(model.taisanVktModel, tsVatKienTruc);
                                    break;

                                case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                                    var taiSanVoHinh = new TaiSanVoHinh();
                                    taiSanVoHinh.TAI_SAN_ID = result.ID;
                                    _taiSanVoHinhService.InsertTaiSanVoHinh(taiSanVoHinh);
                                    //model.taisanvohinhModel = _taiSanVoHinhModelFactory.PrepareTaiSanVoHinhModel(model.taisanvohinhModel, taiSanVoHinh);
                                    break;
                            }
                            var tsimport = _itemService.GetTaiSanByMa(result.MA).ToModel<TaiSanModel>();
                            _taiSanImportModelFactory.InsertToBienDong(tsimport, bd);
                            var biendong = _biendongService.GetBienDongCuoiByTaiSanId(tsimport.ID).ToModel<BienDongModel>();
                            var biendongchitiet = _taiSanImportModelFactory.InsertToBienDongChiTiet(ts.ToModel<ImportExcelTaiSanModel>(), bdct, biendong);
                            _taiSanImportModelFactory.InsertTaiSanNguonVonFromBienDong(ts.ToModel<ImportExcelTaiSanModel>(), tsimport, biendong);
                            _taiSanHienTrangSuDungModelFactory.InsertHienTrangSuDungForBienDong(biendong.ID, biendong.TAI_SAN_ID, biendongchitiet.HTSD_JSON);
                            _taiSanImportModelFactory.InsertHaoMonFromTsImport(biendong.ToEntity<BienDong>(), biendongchitiet.ToEntity<BienDongChiTiet>(), haoMonTaiSanModel: new HaoMonTaiSanModel());
                            _taiSanImportModelFactory.InsertKhauHaoFromTsImport(ts.ToModel<ImportExcelTaiSanModel>(), biendong, biendongchitiet, taiSanKhauHaoModel: new TaiSanKhauHaoModel());
                            var tsmoi = _itemService.GetTaiSanById(tsimport.ID);
                            tsmoi.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET;
                            _itemService.UpdateTaiSan(tsmoi);
                            var bdmoi = _biendongService.GetBienDongById(biendong.ID);
                            bdmoi.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.DA_DUYET;
                            bdmoi.NGAY_DUYET = DateTime.Now;
                            _biendongService.UpdateBienDong(bdmoi);
                        }
                        var GetTsImport = _taiSanImportService.GetTaiSanById(ts.ID);
                        GetTsImport.TRANG_THAI_ID = 1;
                        _taiSanImportService.UpdateTaiSan(GetTsImport);
                        soTsImport++;
                    }
                    else
                    {
                        var result = _taiSanImportModelFactory.InsertToTaiSan(ts, model);
                        switch (result.LOAI_HINH_TAI_SAN_ID)
                        {
                            case (int)enumLOAI_HINH_TAI_SAN.DAT:
                                var TsDat = new TaiSanDat();
                                TsDat.DIEN_TICH = 0;
                                TsDat.TAI_SAN_ID = result.ID;
                                _taisandatService.InsertTaiSanDat(TsDat);
                                //var itemTSDat = _taisandatService.GetTaiSanDatByTaiSanId(result.ID);
                                //model.taisandatModel = _taisandatModelFactory.PrepareTaiSanDatModel(TsDat.ToModel<TaiSanDatModel>(), itemTSDat);
                                //model.cohoso = true;
                                break;

                            case (int)enumLOAI_HINH_TAI_SAN.NHA:
                                var TsNha = new TaiSanNha();
                                TsNha.TAI_SAN_ID = result.ID;
                                TsNha.TAI_SAN_DAT_ID = 0;
                                TsNha.DIEN_TICH_SAN_XAY_DUNG = 0;
                                TsNha.DIEN_TICH_XAY_DUNG = 0;
                                TsNha.NAM_XAY_DUNG = result.NAM_SAN_XUAT;
                                TsNha.NGAY_SU_DUNG = null;
                                _taisannhaService.InsertTaiSanNha(TsNha);
                                //model.LOAI_HINH_TAI_SAN_ID = result.LOAI_HINH_TAI_SAN_ID;
                                //var itemTsNha = _taisannhaService.GetTaiSanNhaByTaiSanId(result.ID);
                                //model.taisannhaModel = _taiSanNhaModelFactory.PrepareTaiSanNhaModel(TsNha.ToModel<TaiSanNhaModel>(), itemTsNha);
                                //model.taisannhaModel.isQuanLyDat = true;
                                break;

                            case (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                            case (int)enumLOAI_HINH_TAI_SAN.OTO:
                                var TsOto = new TaiSanOto();
                                TsOto.TAI_SAN_ID = result.ID;
                                TsOto.BIEN_KIEM_SOAT = "-";
                                _taisanOtoService.InsertTaiSanOto(TsOto);
                                //model.taisanOtoModel = TsOto.ToModel<TaiSanOtoModel>();
                                //var itemOto = _taisanOtoService.GetTaiSanOtoByTaiSanId(result.ID);
                                //model.taisanOtoModel = _taiSanOtoModelFactory.PrepareTaiSanOtoModel(model.taisanOtoModel, itemOto);
                                break;

                            case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV:
                                var tsCayLauNam = new TaiSanCln();
                                tsCayLauNam.TAI_SAN_ID = result.ID;
                                _taisanClnService.InsertTaiSanCln(tsCayLauNam);
                                //model.taisanClnModel = tsCayLauNam.ToModel<TaiSanClnModel>();
                                //model.taisanClnModel = _taiSanClnModelFactory.PrepareTaiSanClnModel(model.taisanClnModel, tsCayLauNam);
                                break;

                            case (int)enumLOAI_HINH_TAI_SAN.DAC_THU:
                            case (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                            case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                                var tsMayMoc = new TaiSanMayMoc();
                                tsMayMoc.TAI_SAN_ID = result.ID;
                                _taisanmaymocService.InsertTaiSanMayMoc(tsMayMoc);
                                //model.taisanmaymocModel = _taiSanMayMocModelFactory.PrepareTaiSanMayMocModel(model.taisanmaymocModel, tsMayMoc);
                                break;

                            case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                                var tsVatKienTruc = new TaiSanVkt();
                                tsVatKienTruc.TAI_SAN_ID = result.ID;
                                _taisanVKTService.InsertTaiSanVkt(tsVatKienTruc);
                                //model.taisanVktModel = _taiSanVktModelFactory.PrepareTaiSanVktModel(model.taisanVktModel, tsVatKienTruc);
                                break;

                            case (int)enumLOAI_HINH_TAI_SAN.VO_HINH:
                                var taiSanVoHinh = new TaiSanVoHinh();
                                taiSanVoHinh.TAI_SAN_ID = result.ID;
                                _taiSanVoHinhService.InsertTaiSanVoHinh(taiSanVoHinh);
                                //model.taisanvohinhModel = _taiSanVoHinhModelFactory.PrepareTaiSanVoHinhModel(model.taisanvohinhModel, taiSanVoHinh);
                                break;
                        }
                        var tsimport = _itemService.GetTaiSanByMa(result.MA).ToModel<TaiSanModel>();
                        _taiSanImportModelFactory.InsertToBienDong(tsimport, bd);
                        var biendong = _biendongService.GetBienDongCuoiByTaiSanId(tsimport.ID).ToModel<BienDongModel>();
                        var biendongchitiet = _taiSanImportModelFactory.InsertToBienDongChiTiet(ts.ToModel<ImportExcelTaiSanModel>(), bdct, biendong);
                        _taiSanImportModelFactory.InsertTaiSanNguonVonFromBienDong(ts.ToModel<ImportExcelTaiSanModel>(), tsimport, biendong);
                        _taiSanHienTrangSuDungModelFactory.InsertHienTrangSuDungForBienDong(biendong.ID, biendong.TAI_SAN_ID, biendongchitiet.HTSD_JSON);
                        _taiSanImportModelFactory.InsertHaoMonFromTsImport(biendong.ToEntity<BienDong>(), biendongchitiet.ToEntity<BienDongChiTiet>(), haoMonTaiSanModel: new HaoMonTaiSanModel());
                        _taiSanImportModelFactory.InsertKhauHaoFromTsImport(ts.ToModel<ImportExcelTaiSanModel>(), biendong, biendongchitiet, taiSanKhauHaoModel: new TaiSanKhauHaoModel());
                        var tsmoi = _itemService.GetTaiSanById(tsimport.ID);
                        tsmoi.TRANG_THAI_ID = (decimal)enumTRANG_THAI_TAI_SAN.DA_DUYET;
                        _itemService.UpdateTaiSan(tsmoi);
                        var bdmoi = _biendongService.GetBienDongById(biendong.ID);
                        bdmoi.TRANG_THAI_ID = (decimal)enumTRANG_THAI_YEU_CAU.DA_DUYET;
                        bdmoi.NGAY_DUYET = DateTime.Now;
                        _biendongService.UpdateBienDong(bdmoi);
                        var GetTsImport = _taiSanImportService.GetTaiSanById(ts.ID);
                        GetTsImport.TRANG_THAI_ID = 1;
                        _taiSanImportService.UpdateTaiSan(GetTsImport);
                        soTsImport++;
                    }
                    /*}
                    else
                    {
                        ListExists.Add(item.TEN);
                    }*/
                }
                catch (Exception ex)
                {
                    ListErr.Add(item.TEN);
                }
            }
            if (soTsImport > 0)
            {
                if (ListErr.Count == 0 && unionList.Count == 0)
                {
                    return JsonSuccessMessage("Import tài sản thành công " + soTsImport + " tài sản!");/*, "Hàng tài sản bị trùng trong file excel: " + string.Join("; ", listRowIdentical)*/
                }
                string Error = "";
                if (unionList.Count > 0)
                {
                    Error = String.Concat(Error, $"{string.Join("; ", unionList)}; ");
                }
                if (ListErr.Count > 0)
                {
                    Error = String.Concat(Error, $"{ListErr.Count} tài sản import lỗi: {string.Join("; ", ListErr)}");
                }
                return JsonSuccessMessage("Import tài sản thành công " + soTsImport + " tài sản!", Error);
            }
            else
            {
                string Error = "";
                if (unionList.Count > 0)
                {
                    Error = String.Concat(Error, $"{string.Join("; ", unionList)}; ");
                }
                if (ListErr.Count > 0)
                {
                    Error = String.Concat(Error, $"{ListErr.Count} tài sản import lỗi: {string.Join("; ", ListErr)}");
                }
                return JsonErrorMessage("Import tài sản thất bại", Error);
            }
            //return JsonErrorMessage("Thông tin tài sản chưa đúng!");
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public virtual IActionResult ImportMaTaiSanCuExcel(IFormFile file)
        {
            if (file == null)
            {
                ErrorNotification("Bạn chưa Nhập file Import");
                // return RedirectToAction("DongBoTaiSan");
            }
            List<int> listRowIdentical = new List<int>();
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(file.OpenReadStream());
            //  lưu file
            string _fileStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), DateTime.Now.ToPathFolderStore(), Guid.NewGuid().ToString() + ".xlsx");
            workbook.SaveToFile(_fileStore, ExcelVersion.Version2013);
            var fileInfo = new FileInfo(_fileStore);
            var package = new ExcelPackage(fileInfo);
            ExcelWorksheets workSheets = package.Workbook.Worksheets;
            List<MessageReturn> ListResult = new List<MessageReturn>();
            if (workSheets.Count < 1)
            {
                //ErrorMessageJson("File không chuẩn dữ liệu");
                return null;
            }
            ExcelWorksheet taiSan = workSheets.First();
            List<ExcelModel> lstSettings = new List<ExcelModel>();
            string strSettings = _fileProvider.ReadAllText(_fileProvider.MapPath("~/App_Data/jsonSetting_ImportExcelMaTaiSanCu.json"), Encoding.UTF8);
            lstSettings = strSettings.toEntities<ExcelModel>();
            int maxCol = lstSettings != null ? lstSettings.Max(c => c.COL) : 0;
            if (maxCol < taiSan.Dimension.End.Column || taiSan.Dimension.End.Column < maxCol)
                ErrorNotification("Thông tin tài sản không đúng định dạng");
            List<ImportExcelMaTaiSanCuModel> lstImport = new List<ImportExcelMaTaiSanCuModel>();
            List<string> ListErr = new List<string>();
            List<string> ListExists = new List<string>();
            List<string> unionList = new List<string>();
            for (int rowNumber = 3; rowNumber <= taiSan.Dimension.End.Row; rowNumber++)
            {
                ExcelRange row = taiSan.Cells[rowNumber, 1, rowNumber, taiSan.Dimension.End.Column];
                ImportExcelMaTaiSanCuModel item = new ImportExcelMaTaiSanCuModel();
                item = row.toEntity<ImportExcelMaTaiSanCuModel>(item, rowNumber, lstSettings);
                var rows = rowNumber;
                item.Row = rows;
                var rs = ValidateListImportMaTaiSanCu(item);
                if (rs.Count > 0)
                {
                    unionList = unionList.Union(rs).ToList();
                    continue;
                }
                else
                {
                    lstImport.Add(item);
                }
            }
            if (unionList.Count > 0)
            {
                var Error = string.Join("; ", unionList);
                return JsonErrorMessage("Thông tin tài sản trong excel chưa đúng!", Error);
            }
            int soTsImport = 0;
            foreach (var item in lstImport)
            {
                try
                {
                    ImportExcelMaTaiSanCuModel TaiSanCu = item;
                    TaiSan taiSanDB = _itemService.GetTaiSanByMa(TaiSanCu.TAI_SAN_MA_TSC);
                    taiSanDB.MA_DB = TaiSanCu.TAI_SAN_MA_CU;
                    //var taiSanCapNhat = _itemModelFactory.UpdateMaTaiSanCu(TaiSanCu, taiSanDB);
                    soTsImport++;

                    _itemService.UpdateTaiSan(taiSanDB);
                   
                    /*}
                    else
                    {
                        ListExists.Add(item.TEN);
                    }*/
                }
                catch (Exception ex)
                {
                    ListErr.Add(item.TAI_SAN_MA_TSC);
                }
            }
            if (soTsImport > 0)
            {
                if (ListErr.Count == 0 && unionList.Count == 0)
                {
                    return JsonSuccessMessage("Import thành công " + soTsImport + " mã tài sản!");/*, "Hàng tài sản bị trùng trong file excel: " + string.Join("; ", listRowIdentical)*/
                }
                string Error = "";
                if (unionList.Count > 0)
                {
                    Error = String.Concat(Error, $"{string.Join("; ", unionList)}; ");
                }
                if (ListErr.Count > 0)
                {
                    Error = String.Concat(Error, $"{ListErr.Count} tài sản import lỗi: {string.Join("; ", ListErr)}");
                }
                return JsonSuccessMessage("Import thành công " + soTsImport + " mã tài sản!", Error);
            }
            else
            {
                string Error = "";
                if (unionList.Count > 0)
                {
                    Error = String.Concat(Error, $"{string.Join("; ", unionList)}; ");
                }
                if (ListErr.Count > 0)
                {
                    Error = String.Concat(Error, $"{ListErr.Count} tài sản import lỗi: {string.Join("; ", ListErr)}");
                }
                return JsonErrorMessage("Import mã tài sản thất bại", Error);
            }
            //return JsonErrorMessage("Thông tin tài sản chưa đúng!");
        }

        #endregion
    }
}