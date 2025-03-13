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
using GS.Core.Domain.DanhMuc;

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
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class PhuongAnXuLyController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly IPhuongAnXuLyModelFactory _itemModelFactory;
            private readonly IPhuongAnXuLyService _itemService;
            private readonly IHinhThucXuLyModelFactory _hinhThucXuLyModelFactory;
         #endregion
     
        #region Ctor
        public PhuongAnXuLyController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IPhuongAnXuLyModelFactory itemModelFactory,
            IPhuongAnXuLyService itemService,
            IHinhThucXuLyModelFactory hinhThucXuLyModelFactory
            )
        {            
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;            
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._hinhThucXuLyModelFactory = hinhThucXuLyModelFactory;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMPhuongAnXuLy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new PhuongAnXuLySearchModel ();
            var model = _itemModelFactory.PreparePhuongAnXuLySearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(PhuongAnXuLySearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMPhuongAnXuLy))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PreparePhuongAnXuLyListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMPhuongAnXuLy))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PreparePhuongAnXuLyModel(new PhuongAnXuLyModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(PhuongAnXuLyModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMPhuongAnXuLy))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<PhuongAnXuLy>();                
                _itemService.InsertPhuongAnXuLy(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<PhuongAnXuLyModel>(), "PhuongAnXuLy");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PreparePhuongAnXuLyModel(model, null);            
            return View(model);
        } 
        
        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMPhuongAnXuLy))
                return AccessDeniedView();
                
            var item = _itemService.GetPhuongAnXuLyById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PreparePhuongAnXuLyModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(PhuongAnXuLyModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMPhuongAnXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetPhuongAnXuLyById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PreparePhuongAnXuLy(model,item);
                _itemService.UpdatePhuongAnXuLy(item); 
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<PhuongAnXuLyModel>(), "PhuongAnXuLy");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PreparePhuongAnXuLyModel(model, item, true);
            return View(model);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMPhuongAnXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetPhuongAnXuLyById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeletePhuongAnXuLy(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<PhuongAnXuLyModel>(), "PhuongAnXuLy");
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
        public virtual IActionResult GetDDLHinhThucByPhuongAn(int PhuongAnId = 0)
        {
            return Json(_hinhThucXuLyModelFactory.DDLPhuongAnByHinhThuc(HinhThucId: PhuongAnId));
        }
        #endregion
    }
}

