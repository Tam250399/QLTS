//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using GS.Web.Framework.Extensions;
using GS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Web.Factories.DongBo;
using System.Threading.Tasks;
using System.Collections.Generic;
using GS.Web.Factories.BaoCaos;
using System.IO;
using GS.Services.DMDC;
using GS.Services.TaiSans;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class DonViController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IDonViModelFactory _itemModelFactory;
        private readonly IDonViService _itemService;
        private readonly ILoaiDonViService _itemLoaiDonViService;
        private readonly INguoiDungDonViService _itemNguoiDungDonViService;
        private readonly INguoiDungService _itemNguoiDungService;
        private readonly INguoiDungModelFactory _itemNguoiDungModelFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDonViLichSuService _donViLichSuService;
        private readonly IDongBoFactory _dongBoFactory;
        private readonly IReportModelFactory _reportModelFactory;
        private readonly IDMDC_DonViNganSachService _dMDC_DonViNganSachService;
        private readonly ITaiSanService _taiSanService;
        #endregion

        #region Ctor
        public DonViController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IDonViModelFactory itemModelFactory,
            IDonViService itemService,
            ILoaiDonViService itemLoaiDonViService,
            INguoiDungDonViService itemNguoiDungDonViService,
            INguoiDungService itemNguoiDungService,
            INguoiDungModelFactory itemNguoiDungModelFactory,
            IHttpContextAccessor httpContextAccessor,
            IDonViLichSuService donViLichSuService,
            IDongBoFactory dongBoFactory,
            IReportModelFactory reportModelFactory,
            IDMDC_DonViNganSachService dMDC_DonViNganSachService,
            ITaiSanService taiSanService
            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._itemLoaiDonViService = itemLoaiDonViService;
            this._itemNguoiDungDonViService = itemNguoiDungDonViService;
            this._itemNguoiDungService = itemNguoiDungService;
            this._itemNguoiDungModelFactory = itemNguoiDungModelFactory;
            this._httpContextAccessor = httpContextAccessor;
            this._donViLichSuService = donViLichSuService;
            this._dongBoFactory = dongBoFactory;
            this._reportModelFactory = reportModelFactory;
            _dMDC_DonViNganSachService = dMDC_DonViNganSachService;
            this._taiSanService = taiSanService;
        }
        #endregion

        #region Methods

        public virtual IActionResult List(int? pageIndex = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDonVi))
                return AccessDeniedView();
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
                currentDonViId: _workContext.CurrentDonVi.ID))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new DonViSearchModel();
            var nguoiDung = _workContext.CurrentCustomer;
            if (nguoiDung.IS_QUAN_TRI == true)
            {
                searchmodel.IsQuangTri = true;
            }
            var model = _itemModelFactory.PrepareDonViSearchModel(searchmodel);
            if (pageIndex > 0)
            {
                searchmodel.Page = (int)pageIndex;
            }
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(DonViSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDonVi))
                return AccessDeniedKendoGridJson();

            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
                currentDonViId: _workContext.CurrentDonVi.ID,
                donViID: searchModel.donViId))
                return AccessDeniedKendoGridJson();
            //prepare model
            var nguoiDung = _workContext.CurrentCustomer;
            if (!searchModel.isIncludeAll)
            {
                searchModel.nguoiDungId = nguoiDung.ID;
                searchModel.IsQuangTri = nguoiDung.IS_QUAN_TRI;
                searchModel.isSelectList = true;
            }
            var model = _itemModelFactory.PrepareDonViListModel(searchModel);
            return Json(model);
        }
        public virtual IActionResult ExportDonVi(String KeySearch, decimal? LoaiDonViSearch, decimal? CapDonViSearch, decimal? donViId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhSachDanhMuc))
                return AccessDeniedView();
            //if(donViId.HasValue)
            //{
            //    DonVi donVi = _itemService.GetDonViById(donViId.Value);
            //    if (donVi != null && donVi.MA == "997003")
            //        donViId = null;
            //}
            DonViSearchModel searchModel = new DonViSearchModel()
            {
                KeySearch = KeySearch,
                LoaiDonViSearch = LoaiDonViSearch,
                CapDonViSearch = CapDonViSearch,
                donViId = donViId
            };

            var nguoiDung = _workContext.CurrentCustomer;
            searchModel.nguoiDungId = nguoiDung.ID;
            searchModel.IsQuangTri = nguoiDung.IS_QUAN_TRI;
            searchModel.isSelectList = true;
            searchModel.isIncludeAll = true;
            searchModel.TreeLevel = 2;
            searchModel.xuatExcel = true;
            var model = _itemModelFactory.PrepareDonViExport(searchModel);
            int total = model != null ? model.Count() : 0;
            bool addSTT = false;
            string fName = $"DonVi_{total}({DateTime.Now.ToString("dd-MM-yyyy hh24-mm-ss")})";
            MemoryStream stream = new MemoryStream();
            stream = _reportModelFactory.prepareExcelEntity<DonViExport>(stream, model, "DonVi", addSTT);
            var bytes = stream.ToArray();
            return new FileContentResult(bytes, MimeTypes.TextXlsx)
            {
                FileDownloadName = fName + ".xlsx"
            };
        }
        public virtual IActionResult ExportDonViChuaNhapTaiSan(DonViSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhSachDanhMuc))
                return AccessDeniedView();
            //if(donViId.HasValue)
            //{
            //    DonVi donVi = _itemService.GetDonViById(donViId.Value);
            //    if (donVi != null && donVi.MA == "997003")
            //        donViId = null;
            //}


            searchModel.donViId = _workContext.CurrentDonVi.ID;
            searchModel.MaBo = _itemService.GetDonViById(Convert.ToDecimal(searchModel.donViId)).MA_BO;
            var model = _itemModelFactory.PrepareDonViChuaNhapTaiSanExport(searchModel);
            int total = model != null ? model.Count() : 0;
            bool addSTT = true;
            string fName = $"DonVi_{total}({DateTime.Now.ToString("dd-MM-yyyy hh24-mm-ss")})";
            MemoryStream stream = new MemoryStream();
            stream = _reportModelFactory.prepareExcelEntity<DonViExport>(stream, model, "DonVi", addSTT);
            var bytes = stream.ToArray();
            return new FileContentResult(bytes, MimeTypes.TextXlsx)
            {
                FileDownloadName = fName + ".xlsx"
            };
        }

        public virtual IActionResult ExportDonViKiemTraTaiSan(DonViSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhSachDanhMuc))
                return AccessDeniedView();
            //if(donViId.HasValue)
            //{
            //    DonVi donVi = _itemService.GetDonViById(donViId.Value);
            //    if (donVi != null && donVi.MA == "997003")
            //        donViId = null;
            //}


            searchModel.donViId = _workContext.CurrentDonVi.ID;
            //searchModel.MaBo = _itemService.GetDonViById(Convert.ToDecimal(searchModel.donViId)).MA_BO;
            var model = _itemModelFactory.PrepareDonViKiemTraTaiSanExport(searchModel);
            int total = model != null ? model.Count() : 0;
            bool addSTT = true;
            string fName = $"DonVi_{total}({DateTime.Now.ToString("dd-MM-yyyy hh24-mm-ss")})";
            MemoryStream stream = new MemoryStream();
            stream = _reportModelFactory.prepareExcelEntity<DonViExport>(stream, model, "DonVi", addSTT);
            var bytes = stream.ToArray();
            return new FileContentResult(bytes, MimeTypes.TextXlsx)
            {
                FileDownloadName = fName + ".xlsx"
            };
        }
        [HttpPost]
        public virtual IActionResult ListChonDonViDieuChuyen(DonViSearchModel searchModel)
        {
            //prepare model
            var nguoiDung = _workContext.CurrentCustomer;
            if (!searchModel.isIncludeAll)
            {
                searchModel.nguoiDungId = nguoiDung.ID;
                searchModel.IsQuangTri = nguoiDung.IS_QUAN_TRI;
                searchModel.isSelectList = true;
            }
            //var model = _itemModelFactory.PrepareDonViListModel(searchModel);
            var model = _itemModelFactory.PrepareDonViDieuChuyenListModel(searchModel);
            return Json(model);
        }
        /// <summary>
        /// Su dung de hien thi danh sach ap dung cho nghiệp vụ chọn đơn vị, ko áp dụng quyền trên chức năng này
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult ListChonDonVi(DonViSearchModel searchModel)
        {
            //prepare model
            var nguoiDung = _workContext.CurrentCustomer;
            searchModel.nguoiDungId = nguoiDung.ID;
            searchModel.IsQuangTri = nguoiDung.IS_QUAN_TRI;
            searchModel.isSelectList = true;
            var model = _itemModelFactory.PrepareDonViListChonDonViModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDonVi))
                return AccessDeniedView();

            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
                currentDonViId: _workContext.CurrentDonVi.ID))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareDonViModel(new DonViModel(), null);
            return View(model);
        }


        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(DonViModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDonVi))
                return AccessDeniedView();
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID,
               donViID: model.ID,
               donViChaID: model.PARENT_ID))
                return AccessDeniedView();
            if (ModelState.IsValid)
            {
                //if (model.PARENT_ID > 0)
                //{
                //    var checkQuyenDonVi = _itemService.CheckDonVi(model.PARENT_ID.Value, _workContext.CurrentCustomer.ID);
                //    if (!checkQuyenDonVi)
                //    {
                //        return AccessDeniedView();
                //    }
                //}
                if (model.IsMaQHNS == false)
                {
                    model.MA_DVQHNS = "";

                }
                model.MA = model.MA_DON_VI_CHA + model.MA;
                var item = model.ToEntity<DonVi>();
                _itemService.InsertDonVi(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<DonViModel>(), "DonVi");
                model.ID = item.ID;
                _dongBoFactory.DongBoThuCongDanhMuc2<DonViModel>(new List<DonViModel>() { model });
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareDonViModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id, int pageIndex)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDonVi))
                return AccessDeniedView();

            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID,
               donViID: id))
                return AccessDeniedView();
            var item = _itemService.GetDonViById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareDonViModel(null, item);
            model.pageIndex = pageIndex;
            if (item.MA == item.MA_BO || _workContext.CurrentCustomer.IS_QUAN_TRI || _workContext.CurrentDonVi.MA_DON_VI == item.MA_BO)
            {
                model.IsQuyenSuaMaCha = true;
            }
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(DonViModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDonVi))
                return AccessDeniedView();

            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID,
               donViID: model.ID))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDonViById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                if (model.IsMaQHNS == false)
                {
                    model.MA_DVQHNS = "";
                }
                model.MA = model.MA_DON_VI_CHA + model.MA;
                //Nếu là đơn vị tổng hợp có đơn vị con
                //+ Không được sửa mã đơn vị cha
                //+ Không được thay đổi mã của chính mình
                //+ LA_DON_VI_NHAP_LIEU = true là đơn vị đăng kí
                int soDonViCon = _itemService.GetSoDonViConByID(item.ID);
                if (soDonViCon > 0 && model.LA_DON_VI_NHAP_LIEU == true)
                {
                    ErrorNotification("Có đơn vị con không thể đổi thành đơn vị nhập liệu");
                    return RedirectToAction("List", new { pageIndex = model.pageIndex });
                }
                if (item.LA_DON_VI_NHAP_LIEU != true)
                {
                    if (soDonViCon > 0 && (item.DonViCha.MA != model.MA_DON_VI_CHA || item.DonViCha.ID != model.PARENT_ID || item.MA != model.MA))
                    {
                        ErrorNotification("Cập nhật dữ liệu thất bại !");
                        return RedirectToAction("List", new { pageIndex = model.pageIndex });
                    }
                }

                var donViLichSu = _itemModelFactory.GetDonViLichSu(item, model?.ToEntity<DonVi>());

                _itemModelFactory.PrepareDonVi(model, item);
                    _itemService.UpdateDonVi(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<DonViModel>(), "DonVi");
                _dongBoFactory.DongBoThuCongDanhMuc2<DonViModel>(new List<DonViModel>() { model });
                SuccessNotification("Cập nhật dữ liệu thành công !");
                _donViLichSuService.InsertDonViLichSu(donViLichSu);
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List", new { pageIndex = model.pageIndex });
            }
            //prepare model
            model = _itemModelFactory.PrepareDonViModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDonVi))
                return AccessDeniedView();

            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID,
               donViID: id))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDonViById(id);
            var dvmodel = _itemModelFactory.PrepareDonViModel(null, item);
            //var taiSan = _taiSanService.GetTaiSanByDonViId(id);
            if (item == null)
                return RedirectToAction("List");
            var tschuaxoa = _taiSanService.GetTaiSanByTrangThaiId(item.ID);
            if ((item.LA_DON_VI_NHAP_LIEU == true && tschuaxoa == false) || (item.LA_DON_VI_NHAP_LIEU == false && dvmodel.SO_DON_VI_CON == 0))
            {
                try
                {
                    item.TRANG_THAI_ID = false;
                    item.MA = string.Concat("XOA - ", item.MA);
                    DonViModel model = item.ToModel<DonViModel>();
                        _itemService.UpdateDonVi(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Xóa dữ liệu Not Active ", item.ToModel<DonViModel>(), "DonVi");
                    //đồng bộ dữ liệu qua kho
                    _dongBoFactory.XoaDanhMuc<DonViModel>(model);
                    //SuccessNotification("Xoá dữ liệu thành công");
                    //return RedirectToAction("List");
                    UpdateSessionSearchModel<DonViSearchModel>(true);
                    return JsonSuccessMessage("Xoá dữ liệu thành công");
                }
                catch (Exception exc)
                {
                    ErrorNotification(exc);
                    return RedirectToAction("Edit", new { id = item.ID });
                }
            }
            else if (dvmodel.SO_DON_VI_CON > 0)
            {
                return JsonErrorMessage("Đơn vị đang có đơn vị con không thể xóa!");
            }
            else return JsonErrorMessage("Đơn vị đang có tài sản không thể xóa!");
        }

        #region Search đơn vị for input
        public virtual async Task<IActionResult> SearchDonViForMultilSelectInput(string keySearch = null, string ListSelectedId = null)
        {
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID))
                return AccessDeniedView();
            var result = await _itemModelFactory.PrepareSelectListDonViForMultilSelectInput(keySearch: keySearch, ListSelectedId: ListSelectedId);
            return Json(result);
        }

        public virtual IActionResult SearchDonViCha(string TenDonVi = null)
        {
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID))
                return AccessDeniedView();
            var nguoiDungId = _workContext.CurrentCustomer.ID;
            var items = _itemService.GetDonViCapDuoiTrucThuocForInputSearch(TenDonVi, nguoiDungId, _workContext.CurrentDonVi.ID)
                .Where(c => !(c.LA_DON_VI_NHAP_LIEU ?? false))
                .OrderBy(c => c.DIA_BAN_ID).ToList();
            return Json(items.Select(c =>
            {
                var m = c.ToModel<DonViModel>();
                return m;
            }
            ).ToList());
        }
        public virtual IActionResult SearchDonViTongHop(string TenDonVi = null)
        {
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID))
                return AccessDeniedView();
            var nguoiDungId = _workContext.CurrentCustomer.ID;
            var items = _itemService.GetDonViTongHopForInputSearch(TenDonVi);

            return Json(items.Select(c =>
            {
                var m = c.ToModel<DonViModel>();
                return m;
            }
            ).ToList());
        }
        public virtual IActionResult SearchDonViMuaSam(string TenDonVi = null)
        {
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID))
                return AccessDeniedView();
            var items = _itemModelFactory.PrepareListDonViMuaSamForInputSearch(TenDonVi);
            return Json(items.ToList());
        }

        public virtual IActionResult SearchDonViNhanDieuChuyen(string TenDonVi = null)
        {
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID))
                return AccessDeniedView();
            var nguoiDungId = _workContext.CurrentCustomer.ID;
            var items = _itemService.GetDonViForInputSearch(TenDonVi, nguoiDungId, _workContext.CurrentDonVi.ID).Where(c => c.LA_DON_VI_NHAP_LIEU == true).OrderBy(c => c.DIA_BAN_ID).ToList();
            return Json(items.Select(c =>
            {
                var m = c.ToModel<DonViModel>();
                return m;
            }
            ).ToList());
        }


        public virtual IActionResult SearchDonViCon(string TenDonVi = null, decimal? donViSelected = 0)
        {
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID))
                return AccessDeniedView();
            var donViId = _workContext.CurrentDonVi.ID;
            var donvicon = _itemService.GetDonViConForInputSearch(TenDonVi, donViId, donViSelected);
            var items = _itemService.GetDonViConForInputSearch(TenDonVi, donViId, donViSelected);
            return Json(items.Select(c =>
            {
                var m = c.ToModel<DonViModel>();
                return m;
            }
            ).ToList());
        }
        #endregion

        public virtual IActionResult GetDonViConFromDonViCha(decimal donViChaId)
        {
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID,
               donViChaID:donViChaId))
                return AccessDeniedView();
            if (donViChaId != (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG && donViChaId != 0)
            {
                var res = _itemModelFactory.PrepareSelectListDonViUsingProc(isAddFirst: true, IdDonVi: donViChaId);
                return JsonSuccessMessage("OK", res);
            }
            return JsonSuccessMessage("OK", new List<SelectListItem>());
        }

        // return 3 mã gợi ý  trống chưa sử dụng, ví dụ: 012, 013, 014... dựa vào id đơn vị cha
        [HttpPost]
        public virtual IActionResult GetMaGoiYFromDonViCha(decimal donViChaId)
        {
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID,
               donViChaID:donViChaId))
                return AccessDeniedView();
            List<string> ListMaGoiY = new List<string>();
            var MaDonViCha = "";
            decimal MaxId = 0;
            decimal loaiCapDonViCha = 0;
            SelectList ddlLoaiCapDonViCha = (new EnumLoaiCapDonVi()).ToSelectList();
            if (donViChaId != 0)
            {

                MaDonViCha = _itemService.GetDonViById(donViChaId)?.MA;
                loaiCapDonViCha = _itemService.GetDonViById(donViChaId)?.LOAI_CAP_DON_VI_ID ?? 0;
                ListMaGoiY = _itemService.GetListGoiYMaDonViCon(donViChaId);
            }
            if (loaiCapDonViCha == 1)
            {
                ddlLoaiCapDonViCha = (new EnumLoaiCapDonVi()).ToSelectList(valuesToExclude: new int[] { (int)EnumLoaiCapDonVi.CapDiaPhuong });
            }
            if (loaiCapDonViCha == 2)
            {
                ddlLoaiCapDonViCha = (new EnumLoaiCapDonVi()).ToSelectList(valuesToExclude: new int[] { (int)EnumLoaiCapDonVi.CapTrungUong });
            }
            //ma: Mã đơn vị cha, data: List gợi ý, loaiCap: Loại cấp đơn vị cha (trung ương, hoặc địa phương)
            return JsonSuccessMessage("OK", new { data = ListMaGoiY, ma = MaDonViCha, ddlLoaiCapDonVi = ddlLoaiCapDonViCha });
        }
        public virtual IActionResult _ListNguoiDung(decimal NGUOI_DUNG_ID, decimal Id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMuc))
                return AccessDeniedView();
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID))
                return AccessDeniedView();
            //try to get a store with the specified id

            var nguoiDungDonVi = new NguoiDungDonViMapping();
            nguoiDungDonVi.NGUOI_DUNG_ID = NGUOI_DUNG_ID;
            nguoiDungDonVi.DON_VI_ID = Id;
            _itemNguoiDungDonViService.InsertNguoiDungDonVi(nguoiDungDonVi);
            var item = _itemNguoiDungService.GetNguoiDungById(NGUOI_DUNG_ID);
            var model = item.ToModel<NguoiDungModel>();
            SuccessNotification("Cập nhật dữ liệu thành công");
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult SearchListNguoiDungByDonVi(NguoiDungSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMuc))
                return AccessDeniedView();
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID,
               donViID:searchModel.DonViId))
                return AccessDeniedView();
            //prepare model 
            var model = _itemNguoiDungModelFactory.PrepareNguoiDungListModel(searchModel);
            return Json(model);
        }
        [HttpPost]
        public virtual IActionResult DeleteNguoiDungDonVi(decimal NGUOI_DUNG_ID, decimal Id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMuc))
                return AccessDeniedView();
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID,
               donViID:Id))
                return AccessDeniedView();
            //try to get a store with the specified id
            _itemNguoiDungDonViService.DeleteNguoiDungDonViMapping(DonViId: Id, NguoiDungId: NGUOI_DUNG_ID);
            var maps = _itemNguoiDungDonViService.GetMapByDonViId(Id);
            //activity log
            SuccessNotification("Xoá dữ liệu thành công");
            return Json(maps);
        }
        public virtual IActionResult _AddNguoiDung(decimal donViId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMuc))
                return AccessDeniedView();
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID,
               donViID:donViId))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new NguoiDungSearchModel();
            searchmodel.DonViId = donViId;
            var model = _itemNguoiDungModelFactory.PrepareNguoiDungSearchModel(searchmodel);
            return PartialView(model);
        }
        public virtual IActionResult _ChonDonVi(decimal NguoiDungId, bool isStayInPage = false)
        {
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID))
                return AccessDeniedView();
            var model = new DonViSearchModel();
            model.nguoiDungId = NguoiDungId;
            model.isStayInPage = isStayInPage;
            model = _itemModelFactory.PrepareDonViSearchModel(model);
            return PartialView(model);
        }

        public virtual IActionResult _ChonDonVi2(decimal NguoiDungId, bool isStayInPage = false)
        {
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID))
                return AccessDeniedView();
            var model = new DonViSearchModel();
            model.nguoiDungId = NguoiDungId;
            model.isStayInPage = isStayInPage;
            model = _itemModelFactory.PrepareDonViSearchModel2(model);
            return PartialView(model);
        }

        public virtual IActionResult _SelectDonViQuanLy(decimal DonViId = 0)
        {
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
               currentDonViId: _workContext.CurrentDonVi.ID))
                return AccessDeniedView();
           
            var userGuid = _workContext.CurrentCustomer.GUID;
            var donvi = _itemService.GetProfileUser(guidNguoiDung: userGuid, donviId: DonViId);
            var donViKey = "CurrentDonVi" + _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
            HttpContext.Session.SetObject(donViKey, donvi);
            return RedirectToAction("Index", "AppWork");
        }

        public virtual IActionResult ddlLoaiCapDV_change(decimal LoaiCapDV)
        {
            if (LoaiCapDV == (int)(EnumLoaiCapDonVi.CapDiaPhuong))
            {
                var dllCapDonVi = (new EnumCapDonViDiaPhuong()).ToSelectList();
                return Json(dllCapDonVi);
            }
            else
            {
                int[] valuesToExcute = new int[] { (int)CapEnum.TongCuc, (int)CapEnum.ChiCuc, (int)CapEnum.Cuc };
                var dllCapDonVi = (new EnumCapDonViTrungUong()).ToSelectList(valuesToExclude: valuesToExcute);
                return Json(dllCapDonVi);
            }
        }
        public virtual IActionResult SearchDonVi(string Ten, int ID)
        {
            var items = _itemService.GetDonVisForCBB(Ten, ID);
            return Json(items.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.TEN
            }
            ).ToList());
        }
        public virtual IActionResult SearchDonViDieuChuyen(string tenDonVi = null)
        {
            var items = _itemService.SearchDonViDieuChuyens(Keysearch: tenDonVi, isTinh: false, ParentID: null).Take(10);
            var md = Json(items.Select(c =>
            {
                var m = c.ToModel<DonViModel>();
                return m;
            }
            ).ToList());
            return md;
        }
        public virtual IActionResult ListKiemTraLoaiHinhDonVi(int? pageIndex = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLCongCuKiemTraLoaiHinhDonVi))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new DonViSearchModel();
            var model = _itemModelFactory.PrepareDonViSearchModel(searchmodel);
            if (pageIndex > 0)
                searchmodel.Page = (int)pageIndex;
            searchmodel.donViId = _workContext.CurrentDonVi.ID;
            return View(model);
        }
        [HttpPost]
        public virtual IActionResult ListKiemTraLoaiHinhDonVi(DonViSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLCongCuKiemTraLoaiHinhDonVi))
                return AccessDeniedKendoGridJson();
            if (!_itemService.CheckQuyen(currentNguoiDungId: _workContext.CurrentCustomer.ID,
              currentDonViId: _workContext.CurrentDonVi.ID,
              donViID: searchModel.donViId))
                return AccessDeniedView();
            //prepare model
            var nguoiDung = _workContext.CurrentCustomer;
            if (!searchModel.isIncludeAll)
            {
                searchModel.nguoiDungId = nguoiDung.ID;
                searchModel.IsQuangTri = nguoiDung.IS_QUAN_TRI;
                searchModel.isSelectList = true;
            }
            var model = _itemModelFactory.PrepareListKiemTraLoaiHinhDonVi(searchModel);
            return Json(model);
        }
        public virtual IActionResult ListDonViChuaNhapTaiSan(int? pageIndex = 0)
        {
            var searchmodel = new DonViSearchModel();
            var model = _itemModelFactory.PrepareDonViChuaNhapTaiSanSearchModel(searchmodel);
            if (pageIndex > 0)
                searchmodel.Page = (int)pageIndex;
            searchmodel.donViId = _workContext.CurrentDonVi.ID;
            return View(model);
        }
        [HttpPost]
        public virtual IActionResult ListDonViChuaNhapTaiSan(DonViSearchModel searchModel)
        {
            searchModel.donViId = _workContext.CurrentDonVi.ID;
            searchModel.MaBo = _itemService.GetDonViById(Convert.ToDecimal(searchModel.donViId)).MA_BO;
            var model = _itemModelFactory.PrepareDonViChuaNhapTaiSanListModel(searchModel);
            return Json(model);
        }
        public virtual IActionResult ListKiemTraTaiSanDatNha(int? pageIndex = 0)
        {
            var searchmodel = new DonViSearchModel();
            var model = _itemModelFactory.PrepareKiemTraTaiSanSearchModel(searchmodel);
            if (pageIndex > 0)
                searchmodel.Page = (int)pageIndex;
            searchmodel.donViId = _workContext.CurrentDonVi.ID;
            return View(model);
        }
        [HttpPost]
        public virtual IActionResult ListKiemTraTaiSanDatNha(DonViSearchModel searchModel)
        {
            searchModel.donViId = _workContext.CurrentDonVi.ID;
            var model = _itemModelFactory.PrepareKiemTraTaiSanListModel(searchModel);
            return Json(model);
        }
        public virtual IActionResult UpdateKiemTraLoaiHinhDonVi(string DonViIds)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLCongCuKiemTraLoaiHinhDonVi))
                return AccessDeniedView();
            var model = _itemModelFactory.PrepareKiemTraLoaiHinhDonViModel(new KiemTraLoaiHinhDonViModel());
            model.DonViIds = DonViIds;
            return View(model);
        }
        [HttpPost]
        public virtual IActionResult UpdateKiemTraLoaiHinhDonVi(KiemTraLoaiHinhDonViModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLCongCuKiemTraLoaiHinhDonVi))
                return AccessDeniedView();
            if (ModelState.IsValid)
            {
                var DonViIds = model.DonViIds.Split('-').Select(p => decimal.Parse(p)).ToArray();

                var donVis = _itemService.GetDonViByIds(DonViIds);

                foreach (var donVi in donVis)
                {
                    donVi.LOAI_CAP_DON_VI_ID = model.LOAI_CAP_DON_VI_ID;
                    donVi.CAP_DON_VI_ID = model.CAP_DON_VI_ID;
                    donVi.DIA_BAN_ID = model.DIA_BAN_ID;
                    donVi.LOAI_DON_VI_ID = model.LOAI_DON_VI_ID;
                }

                _itemService.UpdateListDonVi(donVis.ToList());

                return RedirectToAction("ListKiemTraLoaiHinhDonVi");
            }

            return View(_itemModelFactory.PrepareKiemTraLoaiHinhDonViModel(model));
        }

        public virtual IActionResult ListDonViDaXacNhan()
        {
            var searchmodel = new DonViSearchModel();
            var model = _itemModelFactory.PrepareDonViXacNhanSearchModel(searchmodel);

            return View(model);
        }
        [HttpPost]
        public virtual IActionResult ListDonViDaXacNhan(DonViSearchModel searchModel)
        {
            var model = _itemModelFactory.PrepareDonViXacNhanListModel(searchModel);
            return Json(model);
        }


        #endregion

        #region Đơn vị chưa cập nhật mã T
        public virtual IActionResult ThongBaoDonViChuaCapNhatMaT()
        {
            return PartialView();
        }

        public virtual IActionResult _ListDonViChuaCapNhatMaT()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMuc))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new DonViSearchModel();
            var model = _itemModelFactory.PrepareDonViSearchModel(searchmodel);
            return View(model);//PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult ListDonViChuaCapNhatMaT(DonViSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhSachDanhMuc))
                return AccessDeniedKendoGridJson();

            var model = _itemModelFactory.PrepareDonViChuaCoMaTListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult CreateDonViByDVDC(string MA_DVQHNS)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMuc))
                return AccessDeniedView();
            //prepare model
            var dvDMDC = _dMDC_DonViNganSachService.GetDMDC_DonViNganSachByMa(MA_DVQHNS);
            var model = _itemModelFactory.PrepareDonViModelFromDMDC(dvDMDC);
            model.IsCreateFromDMDC = true;
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult CreateDonViByDVDC(DonViModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMuc))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                if (model.IsMaQHNS == false)
                {
                    model.MA_DVQHNS = "";

                }
                model.MA = model.MA_DON_VI_CHA + model.MA;
                var item = model.ToEntity<DonVi>();
                _itemService.InsertDonVi(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<DonViModel>(), "DonVi");
                model.ID = item.ID;
                _dongBoFactory.DongBoThuCongDanhMuc2<DonViModel>(new List<DonViModel>() { model });
                return JsonSuccessMessage("Thành công");
            }

            //prepare model
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return JsonErrorMessage("Error", list);
        }
        #endregion
    }
}

