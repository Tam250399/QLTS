//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 28/6/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.BaoCaos;

using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.BaoCaos;
using GS.Web.Controllers;
using GS.Web.Factories.BaoCaos;
using GS.Services.BaoCaos;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;


namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class LogQueueProcessController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly ILogQueueProcessModelFactory _itemModelFactory;
            private readonly ILogQueueProcessService _itemService;
         #endregion
     
        #region Ctor
        public LogQueueProcessController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ILogQueueProcessModelFactory itemModelFactory,
            ILogQueueProcessService itemService
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
            var searchmodel = new LogQueueProcessSearchModel ();
            var model = _itemModelFactory.PrepareLogQueueProcessSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(LogQueueProcessSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareLogQueueProcessListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareLogQueueProcessModel(new LogQueueProcessModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(LogQueueProcessModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<LogQueueProcess>();                
                _itemService.InsertLogQueueProcess(item);
                _hoatdongService.InsertHoatDong("TaoMoi", "Tạo mới thông tin ", item.ToModel<LogQueueProcessModel>(), "LogQueueProcess");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareLogQueueProcessModel(model, null);            
            return View(model);
        } 
        
        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
                
            var item = _itemService.GetLogQueueProcessById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareLogQueueProcessModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(LogQueueProcessModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetLogQueueProcessById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareLogQueueProcess(model,item);
                _itemService.UpdateLogQueueProcess(item); 
                _hoatdongService.InsertHoatDong("CapNhat", "Cập nhật thông tin ", item.ToModel<LogQueueProcessModel>(), "LogQueueProcess");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareLogQueueProcessModel(model, item, true);
            return View(model);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetLogQueueProcessById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteLogQueueProcess(item);
                _hoatdongService.InsertHoatDong("Xoa", "Xóa dữ liệu ", item.ToModel<LogQueueProcessModel>(), "LogQueueProcess");
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
        
        public virtual IActionResult View(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
                
            var item = _itemService.GetLogQueueProcessById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareLogQueueProcessModel(null, item);
            return View(model);
        }
        #endregion       			
	}
}

