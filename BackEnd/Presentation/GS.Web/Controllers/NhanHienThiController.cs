//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.HeThong;
//using GS.Services.Events;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Controllers;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Kendoui;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GS.Web.HeThong.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class NhanHienThiController : BaseWorksController
    {
        #region Fields
        private readonly INhanHienThiService _NhanHienThiService;
        private readonly IWorkContext _workContext;
        private readonly INhanHienThiModelFactory _itemModelFactory;
        private readonly INhanHienThiService _itemService;
        private readonly IQuyenService _quyenService;
        #endregion

        #region Ctor
        public NhanHienThiController(
            IQuyenService quyenService,
            INhanHienThiService NhanHienThiService,
            IWorkContext workContext,
            INhanHienThiModelFactory itemModelFactory,
            INhanHienThiService itemService
            )
        {
            this._quyenService = quyenService;
            this._NhanHienThiService = NhanHienThiService;
            this._workContext = workContext;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhanHienThi))
                return AccessDeniedView();
            //prepare model
            var model = new NhanHienThiSearchModel();
            model = _itemModelFactory.PrepareNhanHienThiSearchModel(model);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(NhanHienThiSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhanHienThi))
                return AccessDeniedKendoGridJson();
            //prepare model
            if (searchModel.PageSize == 0) searchModel.PageSize = 10;
            var model = _itemModelFactory.PrepareNhanHienThiListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhanHienThi))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareNhanHienThiModel(new NhanHienThiModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(NhanHienThiModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhanHienThi))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                model.MA = model.MA.Trim().ToLower();
                //var query = _itemService.GetAllNhanHienThis().Select(c => c.MA);
                //if (query.Contains(model.MA) && !(model.ID > 0))
                //{
                //    ErrorNotification("Mã đã tồn tại !");
                //    return RedirectToAction("Create");
                //}
                var item = model.ToEntity<NhanHienThi>();
                _itemService.InsertNhanHienThi(item);
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareNhanHienThiModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhanHienThi))
                return AccessDeniedView();

            var item = _itemService.GetNhanHienThiById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareNhanHienThiModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(NhanHienThiModel model, bool continueEditing)
        {
            var item = _itemService.GetNhanHienThiById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                model.MA = model.MA.Trim().ToLower();
                //var query = _itemService.GetAllNhanHienThis().Where(c => c.MA == model.MA).FirstOrDefault();
                //if (query != null && query.ID == model.ID)
                //{
                //    ErrorNotification("Mã đã tồn tại !");
                //    return RedirectToAction("Create");
                //}
                _itemModelFactory.PrepareNhanHienThi(model, item);
                _itemService.UpdateNhanHienThi(item);
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareNhanHienThiModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhanHienThi))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetNhanHienThiById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteNhanHienThi(item);
                //activity log  
                SuccessNotification("Xoá dữ liệu thành công");

                //return RedirectToAction("List");
                return this.JsonSuccessMessage();
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }
        [HttpPost]
        public virtual IActionResult TaoMoi(NhanHienThiModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhanHienThi))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                model.MA = model.MA.Trim().ToLower();
                var _nhanht = _itemService.GetNhanHienThiByMa(model.MA);
                if (_nhanht != null)
                    return JsonErrorMessage("Tên nhãn đã tồn tại");

                var item = model.ToEntity<NhanHienThi>();
                _itemService.InsertNhanHienThi(item);
                SuccessNotification("Thêm mới dữ liệu thành công");
            }
            else
                return JsonErrorMessage(ModelState.SerializeErrors().ToString());
            return this.JsonSuccessMessage();
        }
        [HttpPost]
        public virtual IActionResult CapNhat(NhanHienThiModel model)
        {
            var item = _itemService.GetNhanHienThiById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                model.MA = model.MA.Trim().ToLower();
                var _nhanht = _itemService.GetNhanHienThiByMa(model.MA);
                if (_nhanht != null && _nhanht.ID != item.ID)
                    return JsonErrorMessage("Tên nhãn đã tồn tại");
                _itemModelFactory.PrepareNhanHienThi(model, item);
                _itemService.UpdateNhanHienThi(item);
                SuccessNotification("Cập nhật dữ liệu thành công");
            }
            else
                return JsonErrorMessage(ModelState.SerializeErrors().ToString());
            return this.JsonSuccessMessage();
        }
        #endregion
    }
}

