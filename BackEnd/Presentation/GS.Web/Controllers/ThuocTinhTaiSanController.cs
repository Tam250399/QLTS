//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.ThuocTinhs;

using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.ThuocTinh;
using GS.Web.Controllers;
using GS.Web.Factories.ThuocTinhs;
using GS.Services.ThuocTinhs;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;


namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class ThuocTinhTaiSanController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly IThuocTinhTaiSanModelFactory _itemModelFactory;
            private readonly IThuocTinhTaiSanService _itemService;
         #endregion
     
        #region Ctor
        public ThuocTinhTaiSanController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IThuocTinhTaiSanModelFactory itemModelFactory,
            IThuocTinhTaiSanService itemService
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
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new ThuocTinhTaiSanSearchModel ();
            var model = _itemModelFactory.PrepareThuocTinhTaiSanSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(ThuocTinhTaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareThuocTinhTaiSanListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareThuocTinhTaiSanModel(new ThuocTinhTaiSanModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(ThuocTinhTaiSanModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<ThuocTinhTaiSan>();                
                _itemService.InsertThuocTinhTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<ThuocTinhTaiSanModel>(), "ThuocTinhTaiSan");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareThuocTinhTaiSanModel(model, null);            
            return View(model);
        } 
        
        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
                
            var item = _itemService.GetThuocTinhTaiSanById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareThuocTinhTaiSanModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(ThuocTinhTaiSanModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetThuocTinhTaiSanById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareThuocTinhTaiSan(model,item);
                _itemService.UpdateThuocTinhTaiSan(item); 
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<ThuocTinhTaiSanModel>(), "ThuocTinhTaiSan");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareThuocTinhTaiSanModel(model, item, true);
            return View(model);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetThuocTinhTaiSanById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteThuocTinhTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<ThuocTinhTaiSanModel>(), "ThuocTinhTaiSan");
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
        #endregion       			
	}
}

