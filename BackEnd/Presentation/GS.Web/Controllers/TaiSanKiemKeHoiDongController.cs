//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
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
using GS.Services;
using System.Linq;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class TaiSanKiemKeHoiDongController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly ITaiSanKiemKeHoiDongModelFactory _itemModelFactory;
            private readonly ITaiSanKiemKeHoiDongService _itemService;
         #endregion
     
        #region Ctor
        public TaiSanKiemKeHoiDongController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ITaiSanKiemKeHoiDongModelFactory itemModelFactory,
            ITaiSanKiemKeHoiDongService itemService
            )
        {            
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;            
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanKiemKeHoiDongSearchModel ();
            var model = _itemModelFactory.PrepareTaiSanKiemKeHoiDongSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(TaiSanKiemKeHoiDongSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanKiemKeHoiDongListModel(searchModel);
            return Json(model);
        }
        public virtual IActionResult _List(int KiemKeId = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanKiemKeHoiDongSearchModel();
            var model = _itemModelFactory.PrepareTaiSanKiemKeHoiDongSearchModel(searchmodel);
            searchmodel.KiemKeId = KiemKeId;
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult _List(TaiSanKiemKeHoiDongSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanKiemKeHoiDongListModel(searchModel);
            return Json(model);
        }
        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanKiemKeHoiDongModel(new TaiSanKiemKeHoiDongModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(TaiSanKiemKeHoiDongModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<TaiSanKiemKeHoiDong>();                
                _itemService.InsertTaiSanKiemKeHoiDong(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<TaiSanKiemKeHoiDongModel>(), "TaiSanKiemKeHoiDong");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareTaiSanKiemKeHoiDongModel(model, null);            
            return View(model);
        } 
        
        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
                
            var item = _itemService.GetTaiSanKiemKeHoiDongById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanKiemKeHoiDongModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(TaiSanKiemKeHoiDongModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanKiemKeHoiDongById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareTaiSanKiemKeHoiDong(model,item);
                _itemService.UpdateTaiSanKiemKeHoiDong(item); 
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<TaiSanKiemKeHoiDongModel>(), "TaiSanKiemKeHoiDong");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareTaiSanKiemKeHoiDongModel(model, item, true);
            return View(model);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanKiemKeHoiDongById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteTaiSanKiemKeHoiDong(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<TaiSanKiemKeHoiDongModel>(), "TaiSanKiemKeHoiDong");
                //activity log  
                SuccessNotification("Xoá dữ liệu thành công");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = item.ID });
            }
        }

        public virtual IActionResult _CreateOrUpdate(TaiSanKiemKeHoiDongModel hdModel, bool isFirst = false)
        {

            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //try to get a store with the specified id
            var model = new TaiSanKiemKeHoiDongModel();
            model.isFirst = isFirst;
            model.DDLViTriHoiDong = ((enumViTriKiemKeHoiDong)model.viTriKiemKeHoiDong).ToSelectList();
            if (hdModel != null)
            {
                model.CHUC_VU = hdModel.CHUC_VU;
                model.DAI_DIEN = hdModel.DAI_DIEN;
                model.HO_TEN = hdModel.HO_TEN;
                model.ID = hdModel.ID;
                model.KIEM_KE_ID = hdModel.KIEM_KE_ID;
                model.VI_TRI_ID = hdModel.VI_TRI_ID;
            }

            return PartialView(model);
        }
        public virtual IActionResult _CreateForKiemKe(int ID, int KiemKeId)
        {

            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //try to get a store with the specified id
            var model = new TaiSanKiemKeHoiDongModel();
            var item = _itemService.GetTaiSanKiemKeHoiDongById(ID);
            if (item != null)
            {
                model = item.ToModel<TaiSanKiemKeHoiDongModel>();
            }
            else
            {
                model.KIEM_KE_ID = KiemKeId;
            }
            model.DDLViTriHoiDong = ((enumViTriKiemKeHoiDong)model.viTriKiemKeHoiDong).ToSelectList();
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult _CreateForKiemKe(TaiSanKiemKeHoiDongModel model)
        {

            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //try to get a store with the specified id
            if (ModelState.IsValid)
            {
                var item = _itemService.GetTaiSanKiemKeHoiDongById(model.ID);
                if (item != null)
                {
                    _itemModelFactory.PrepareTaiSanKiemKeHoiDong(model, item);
                    _itemService.UpdateTaiSanKiemKeHoiDong(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<TaiSanKiemKeHoiDongModel>(), "TaiSanKiemKeHoiDong");
                }
                else
                {
                    item = model.ToEntity<TaiSanKiemKeHoiDong>();
                    _itemService.InsertTaiSanKiemKeHoiDong(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<TaiSanKiemKeHoiDongModel>(), "TaiSanKiemKeHoiDong");
                }
                return JsonSuccessMessage("Cập nhật thành công", model.ID);
            }
            var listERR = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Cập nhật không thành công", listERR);
        }
        [HttpPost]
        public virtual IActionResult DeleteForKiemKe(int id)
        {

            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanKiemKeHoiDongById(id);
            if (item == null)
                return JsonErrorMessage("Xoá dữ liệu không thành công");
            try
            {
                _itemService.DeleteTaiSanKiemKeHoiDong(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<TaiSanKiemKeHoiDongModel>(), "TaiSanKiemKeHoiDong");
                //activity log  
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Xoá dữ liệu không thành công", exc);
            }
        }
        #endregion
    }
}

