//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 28/5/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.TaiSans;

using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.TaiSans;
using GS.Web.Controllers;
using GS.Web.Factories.TaiSans;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Services.DanhMuc;
using GS.Web.Factories.DanhMuc;
using GS.Web.Framework.Extensions;
using System.Linq;
using GS.Web.Models.NghiepVu;
using GS.Services.NghiepVu;
using GS.Web.Factories.NghiepVu;
using GS.Core.Domain.DanhMuc;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class TaiSanKhauHaoController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly ITaiSanKhauHaoModelFactory _itemModelFactory;
        private readonly ITaiSanModelFactory _taiSanModelFactory;
        private readonly ITaiSanKhauHaoService _itemService;
        private readonly IDonViService _donViService;
        private readonly IDonViModelFactory _donViModelFactory;
        private readonly ITaiSanService _taiSanService;
        private readonly IYeuCauModelFactory _yeucauModelFactory;
        #endregion

        #region Ctor
        public TaiSanKhauHaoController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ITaiSanKhauHaoModelFactory itemModelFactory,
            ITaiSanKhauHaoService itemService,
            IDonViService donViService,
            IDonViModelFactory donViModelFactory,
            ITaiSanModelFactory taiSanModelFactory,
            ITaiSanService taiSanService,
            IYeuCauModelFactory yeucauModelFactory

            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._donViService = donViService;
            this._donViModelFactory = donViModelFactory;
            this._taiSanModelFactory = taiSanModelFactory;
            this._taiSanService = taiSanService;
            this._yeucauModelFactory = yeucauModelFactory;
        }
        #endregion
        #region Methods

        public virtual IActionResult List(bool? isDanhSachTSDA = false)
        {
            //    if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
            //        return AccessDeniedView();
            //    //prepare model
            //    var searchmodel = new TaiSanKhauHaoSearchModel ();
            //    var model = _itemModelFactory.PrepareTaiSanKhauHaoSearchModel(searchmodel);
            //    return View(model);
            //}
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu) && !_quyenService.Authorize(StandardPermissionProvider.USERQLTraCuuTaiSan))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanSearchModel();
            var hasDonViQLDA = _donViService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
            if (hasDonViQLDA)
            {
                var donvi = _donViModelFactory.GetDonViWithConditions(donViId: _workContext.CurrentDonVi.ID, isBanQuanLyDuAn: hasDonViQLDA, isCreateTSDA: isDanhSachTSDA);
                searchmodel.donviId = donvi.ID;
            }
            else
                searchmodel.donviId = _workContext.CurrentDonVi.ID;
            searchmodel.IsDanhSachTaiSanDuAn = isDanhSachTSDA;
            var model = _taiSanModelFactory.PrepareTaiSanSearchModel(searchmodel);
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
            //if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
            //    return AccessDeniedKendoGridJson();
            ////prepare model
            //var model = _itemModelFactory.PrepareTaiSanKhauHaoListModel(searchModel);
            //return Json(model);

            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedView();
            //prepare model
            if (searchModel.PageSize == 0) searchModel.PageSize = 10;
            searchModel.NguoiTaoId = _workContext.CurrentCustomer.ID;
            searchModel.isDuyet = true; // check hiển thị tài sản đã giảm toàn bộ hay không
            var currentDonVi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            var model = new TaiSanListModel();
            if (!(currentDonVi.LA_DON_VI_NHAP_LIEU ?? true)
            || (currentDonVi.CHE_DO_HACH_TOAN_ID == (int)enumCHE_DO_HACH_TOAN.HAO_MON))
            {
                return Json(model); 
            }
             model = _taiSanModelFactory.PrepareTaiSanListModel(searchModel);
            HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch + searchModel.GetType().Name, searchModel);
            return Json(model);
        }
        public virtual IActionResult taiSanKhauHao(Guid guid, int? pageIndex = 0)
        {

            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            var taiSanItem = _taiSanService.GetTaiSanByGuId(guid);
            var yeuCauModel = new YeuCauModel();
            yeuCauModel.TaiSanGuid = taiSanItem.GUID;
            yeuCauModel.NGAY_SU_DUNG = taiSanItem.NGAY_SU_DUNG;
            yeuCauModel = _yeucauModelFactory.PrepareYeuCauModelFromBienDong(model: yeuCauModel, taiSan: taiSanItem, item: null);
            return View(yeuCauModel);
        }
        public virtual IActionResult _ListTaiSanKhauHao(decimal TaiSanId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedView();
            //prepare model
            TaiSanKhauHaoSearchModel searchModel = new TaiSanKhauHaoSearchModel();
            if (TaiSanId > 0)
                searchModel.TaiSanId = TaiSanId;
            var model = _itemModelFactory.PrepareTaiSanKhauHaoSearchModel(searchModel);
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult _ListTaiSanKhauHao(TaiSanKhauHaoSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanKhauHaoListModel(searchModel);
            return Json(model);
        }
        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanKhauHaoModel(new TaiSanKhauHaoModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(TaiSanKhauHaoModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<TaiSanKhauHao>();
                _itemService.InsertTaiSanKhauHao(item);
                _hoatdongService.InsertHoatDong("TaoMoi", "Tạo mới thông tin ", item.ToModel<TaiSanKhauHaoModel>(), "TaiSanKhauHao");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareTaiSanKhauHaoModel(model, null);
            return View(model);
        }
        public virtual IActionResult _CreateOrUpdateTaiSanKhauHao(decimal? idTaiSanKhauHao = 0, decimal? taisanid = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //prepare model
            if (taisanid == 0)
                return RedirectToAction("List");
            var model = new TaiSanKhauHaoModel();
            model.TAI_SAN_ID = taisanid;
            if (idTaiSanKhauHao > 0 && idTaiSanKhauHao != null)
            {
                var item = _itemService.GetTaiSanKhauHaoById(idTaiSanKhauHao ?? 0);
                model = _itemModelFactory.PrepareTaiSanKhauHaoModel(model, item);
            }
            else
            {
                model = _itemModelFactory.PrepareTaiSanKhauHaoModel(model, null);
            }
            model.taisankhauhaoID = idTaiSanKhauHao;
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult _CreateOrUpdateTaiSanKhauHao(TaiSanKhauHaoModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            if (ModelState.IsValid)
            {
                _itemModelFactory.CreateOrUpdateTaiSanKhauHao(model);
                return JsonSuccessMessage("Cập nhật thông tin thành công", model);
            }
            //prepare model
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return JsonErrorMessage("Thêm mới không thành công", list);
        }
        public virtual IActionResult Edit(Guid guid, int pageIndex)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();

            var taisan = _taiSanService.GetTaiSanByGuId(guid);
            if (taisan == null)
                return RedirectToAction("List");
            //prepare model
            var model = _taiSanModelFactory.PrepareTaiSanModel(null, taisan);
            //var model = _itemModelFactory.PrepareTaiSanModel(null,taisan);
            model.pageIndex = pageIndex;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(TaiSanKhauHaoModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanKhauHaoById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareTaiSanKhauHao(model, item);
                _itemService.UpdateTaiSanKhauHao(item);
                _hoatdongService.InsertHoatDong("CapNhat", "Cập nhật thông tin ", item.ToModel<TaiSanKhauHaoModel>(), "TaiSanKhauHao");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareTaiSanKhauHaoModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanKhauHaoById(id);
            if (item == null)
                return JsonErrorMessage("Không tìm thấy tài sản khấu hao");
            try
            {
                _itemService.DeleteTaiSanKhauHao(item);
                _hoatdongService.InsertHoatDong("Xoa", "Xóa dữ liệu ", item.ToModel<TaiSanKhauHaoModel>(), "TaiSanKhauHao");
                var getTaiSanKhauHao_cu = _itemService.GetTaiSanKhauHaoMoiNhatByTaiSanId(item.TAI_SAN_ID ?? 0);
                if (getTaiSanKhauHao_cu != null)
                {
                    getTaiSanKhauHao_cu.NGAY_KET_THUC = null;
                    _itemService.UpdateTaiSanKhauHao(getTaiSanKhauHao_cu);
                }

                //activity log  
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = item.ID });
            }
        }

        [HttpPost]
        public virtual IActionResult DungTinhKhauHao(decimal taiSanId = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //try to get a store with the specified id
            if (taiSanId == 0)
            {
                return JsonErrorMessage("Tài sản không tồn tại");
            }
            var listTSKH = _itemService.GetListTaiSanKhauHaoByTaiSanId(taiSanId);
            if (listTSKH == null || listTSKH.Count() ==0)
            {
                return JsonErrorMessage("Tài sản chưa có chế độ khấu hao!");
            }
            var lastTSKH = listTSKH.OrderByDescending(c => c.ID).FirstOrDefault();
            lastTSKH.NGAY_KET_THUC = DateTime.Today;
            
                _itemService.UpdateTaiSanKhauHao(lastTSKH);
                _hoatdongService.InsertHoatDong("Update", "Dừng tính khấu hao ", lastTSKH.ToModel<TaiSanKhauHaoModel>(), "TaiSanKhauHao");
                return JsonSuccessMessage("Dừng tính khấu hao thành công");

        }
        #endregion
    }
}

