//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Web.Framework.Extensions;
using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.DanhMuc;
using GS.Web.Controllers;
using GS.Web.Factories.DanhMuc;
using GS.Services.DanhMuc;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using System.Linq;
using DevExpress.Web.Internal;
using iTextSharp.text;
using System.Collections.Generic;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class LoaiTaiSanVoHinhController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly ILoaiTaiSanVoHinhModelFactory _itemModelFactory;
            private readonly ILoaiTaiSanDonViServices _itemService;
            private readonly ILoaiTaiSanService _loaiTaiSanService;
            private readonly IDonViService _donViService;
        #endregion

        #region Ctor
        public LoaiTaiSanVoHinhController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ILoaiTaiSanVoHinhModelFactory itemModelFactory,
            ILoaiTaiSanDonViServices itemService,
            ILoaiTaiSanService loaiTaiSanService,
            IDonViService donViService
            )
        {            
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;            
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._donViService = donViService;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiTaiSanVoHinh))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new LoaiTaiSanVoHinhSearchModel();
            var model = _itemModelFactory.PrepareLoaiTaiSanVoHinhSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(LoaiTaiSanVoHinhSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiTaiSanVoHinh))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareLoaiTaiSanVoHinhListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiTaiSanVoHinh))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareLoaiTaiSanVoHinhModel(new LoaiTaiSanVoHinhModel() {DON_VI_ID= _workContext.CurrentDonVi.ID }, null);
            return View(model);
        }

        //[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        //public virtual IActionResult Create(LoaiTaiSanVoHinhModel model, bool continueEditing)
        //{
        //    if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMuc))
        //        return AccessDeniedView();

        //    if (ModelState.IsValid)
        //    {
        //        if (_workContext.CurrentDonVi.ID == (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
        //        {
        //            var item = model.ToEntity<LoaiTaiSanDonVi>();
        //            item.DON_VI_ID = null;
        //            item.CHE_DO_HAO_MON_ID = 23;
        //            _itemService.InsertLoaiTaiSanVoHinh(item);
        //            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<LoaiTaiSanVoHinhModel>(), "LoaiTaiSanVoHinh");
        //            return RedirectToAction("List");
        //        }
        //        //get donvi
        //        var donviapdung = _donViService.GetDonViById(model.DON_VI_ID.Value);
        //        //var listChild = _donViService.GetListDonViChild(model.DON_VI_ID.Value, true);
        //        var listChild = _donViService.GetListIdDonViChild(model.DON_VI_ID.Value, true);
        //        var listLTSVH = new List<LoaiTaiSanDonVi>();
        //        foreach (var donvicon in listChild)
        //        {
        //            var item = model.ToEntity<LoaiTaiSanDonVi>();
        //            item.DON_VI_ID = donvicon;
        //            item.CHE_DO_HAO_MON_ID = 23;
        //            listLTSVH.Add(item);
                   
        //        }
        //        _itemService.InsertLoaiTaiSanVoHinh(listLTSVH);
        //        foreach(var l in listLTSVH)
        //        {
        //            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", l.ToModel<LoaiTaiSanVoHinhModel>(), "LoaiTaiSanVoHinh");
        //        }
        //        SuccessNotification("Tạo mới dữ liệu thành công !");
        //        return RedirectToAction("List");
        //        //continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : 
        //    }
        //    //prepare model
        //    model = _itemModelFactory.PrepareLoaiTaiSanVoHinhModel(model, null);            
        //    return View(model);
        //}

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(LoaiTaiSanVoHinhModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiTaiSanVoHinh))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                    var item = model.ToEntity<LoaiTaiSanDonVi>();
                    item.LOAI_HINH_TAI_SAN_ID = _itemService.GetLoaiTaiSanVoHinhById(model.PARENT_ID ?? 0)?.LOAI_HINH_TAI_SAN_ID;
                    item.CHE_DO_HAO_MON_ID = 23;
                    _itemService.InsertLoaiTaiSanVoHinh(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<LoaiTaiSanVoHinhModel>(), "LoaiTaiSanVoHinh");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Create") : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareLoaiTaiSanVoHinhModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiTaiSanVoHinh))
                return AccessDeniedView();
                
            var item = _itemService.GetLoaiTaiSanVoHinhById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareLoaiTaiSanVoHinhModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(LoaiTaiSanVoHinhModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiTaiSanVoHinh))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetLoaiTaiSanVoHinhById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareLoaiTaiSanDonVi(model,item);
                _itemService.UpdateLoaiTaiSanVoHinh(item); 
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<LoaiTaiSanVoHinhModel>(), "LoaiTaiSanVoHinh");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareLoaiTaiSanVoHinhModel(model, item, true);
            return View(model);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiTaiSanVoHinh))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetLoaiTaiSanVoHinhById(id);
            if (item == null)
                return JsonErrorMessage("Có lỗi xảy ra!");
            try
            {
                _itemService.DeleteLoaiTaiSanVoHinh(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<LoaiTaiSanVoHinhModel>(), "LoaiTaiSanDonVi");
                //activity log
                return JsonSuccessMessage("Xóa thành công.");
            }
            catch (Exception exc)
            {
                //ErrorNotification(exc);
                //return RedirectToAction("Edit", new { id = item.ID });
                return JsonErrorMessage("Có lỗi xảy ra!");
            }
        }

        public virtual IActionResult SearchLoaiTaiSanVoHinh(string KeySearch = null)
        {
            var donViLonNhat = _donViService.GetDonViLonNhat(_workContext.CurrentDonVi.ID);
            var items = _itemService.GetForInputSearch(KeySearch:KeySearch, donViId:donViLonNhat.ID).OrderBy(c=>c.LOAI_HINH_TAI_SAN_ID).ThenBy(c => c.TREE_NODE);
            return Json(items.Select(c => new
            {
                ID = c.ID,
                TEN = c.MA + " - " + c.TEN
            }).ToList());
        }

        public virtual IActionResult SearchDanhSachDonVi(string TenDonVi = null)
        {
            var donViId = _workContext.CurrentDonVi.ID;
            var items = _donViService.GetDonViDuocPhepNhapTSDVForInputSearch(TenDonVi, donViId,_workContext.CurrentCustomer.ID);
            return Json(items.Select(c =>
            {
                var m = c.ToModel<DonViModel>();
                return m;
            }
            ).ToList());
        }
        #endregion
    }
}

