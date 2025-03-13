//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.CCDC;

using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.CCDC;
using GS.Web.Controllers;
using GS.Web.Factories.CCDC;
using GS.Services.CCDC;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Services;
using System.Linq;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class KiemKeHoiDongController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly IKiemKeHoiDongModelFactory _itemModelFactory;
            private readonly IKiemKeHoiDongService _itemService;
         #endregion
     
        #region Ctor
        public KiemKeHoiDongController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IKiemKeHoiDongModelFactory itemModelFactory,
            IKiemKeHoiDongService itemService
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
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new KiemKeHoiDongSearchModel ();
            var model = _itemModelFactory.PrepareKiemKeHoiDongSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(KiemKeHoiDongSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareKiemKeHoiDongListModel(searchModel);
            return Json(model);
        }
        public virtual IActionResult _List(int KiemKeId = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new KiemKeHoiDongSearchModel();
            var model = _itemModelFactory.PrepareKiemKeHoiDongSearchModel(searchmodel);
            searchmodel.KiemKeId = KiemKeId;
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult _List(KiemKeHoiDongSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareKiemKeHoiDongListModelForKiemKe(searchModel);
            return Json(model);
        }
        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareKiemKeHoiDongModel(new KiemKeHoiDongModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(KiemKeHoiDongModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<KiemKeHoiDong>();                
                _itemService.InsertKiemKeHoiDong(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<KiemKeHoiDongModel>(), "KiemKeHoiDong");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareKiemKeHoiDongModel(model, null);            
            return View(model);
        } 
        
        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
                
            var item = _itemService.GetKiemKeHoiDongById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareKiemKeHoiDongModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(KiemKeHoiDongModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKiemKeHoiDongById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareKiemKeHoiDong(model,item);
                _itemService.UpdateKiemKeHoiDong(item); 
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<KiemKeHoiDongModel>(), "KiemKeHoiDong");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareKiemKeHoiDongModel(model, item, true);
            return View(model);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKiemKeHoiDongById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteKiemKeHoiDong(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<KiemKeHoiDongModel>(), "KiemKeHoiDong");
                //activity log  
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = item.ID });
            }
        }

        public virtual IActionResult _CreateOrUpdate(KiemKeHoiDongModel hdModel, bool isFirst = false)
        {
            var model = new KiemKeHoiDongModel();
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
        public virtual IActionResult _CreateForKiemKe(int ID,int KiemKeId)
        {
            var model = new KiemKeHoiDongModel();
            var item = _itemService.GetKiemKeHoiDongById(ID);          
            if (item != null)
            {
                model = item.ToModel<KiemKeHoiDongModel>();                              
            }
            else
            {
                model.KIEM_KE_ID = KiemKeId;
            }         
            model.DDLViTriHoiDong = ((enumViTriKiemKeHoiDong)model.viTriKiemKeHoiDong).ToSelectList();
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult _CreateForKiemKe(KiemKeHoiDongModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            if (ModelState.IsValid)
            {
                var item = _itemService.GetKiemKeHoiDongById(model.ID);
                if (item != null)
                {
                    _itemModelFactory.PrepareKiemKeHoiDong(model, item);
                    _itemService.UpdateKiemKeHoiDong(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<KiemKeHoiDongModel>(), "KiemKeHoiDong");
                }
                else
                {
                    item = model.ToEntity<KiemKeHoiDong>();
                    _itemService.InsertKiemKeHoiDong(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<KiemKeHoiDongModel>(), "KiemKeHoiDong");
                }
                return JsonSuccessMessage("Cập nhật thành công", model.ID);
            }
            var listERR = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Cập nhật không thành công", listERR);
        }
        [HttpPost]
        public virtual IActionResult DeleteForKiemKe(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKiemKeHoiDongById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteKiemKeHoiDong(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<KiemKeHoiDongModel>(), "KiemKeHoiDong");
                //activity log  
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Xoá dữ liệu không thành công",exc);
            }
        }

        #endregion
    }
}

