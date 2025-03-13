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
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GS.Web.HeThong.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class QuyenController : BaseWorksController
    {
        #region Fields
        private readonly INhanHienThiService _NhanHienThiService;
        private readonly IWorkContext _workContext;
        private readonly IQuyenModelFactory _itemModelFactory;
        private readonly IQuyenService _itemService;
        private readonly IHoatDongService _hoatdongService;
        #endregion

        #region Ctor
        public QuyenController(
            INhanHienThiService NhanHienThiService,
            IWorkContext workContext,
            IQuyenModelFactory itemModelFactory,
            IQuyenService itemService,
            IHoatDongService hoatDongService
            )
        {
            this._hoatdongService = hoatDongService;
            this._NhanHienThiService = NhanHienThiService;
            this._workContext = workContext;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_itemService.Authorize(StandardPermissionProvider.ADMINQLQuyen))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new QuyenSearchModel();
            var model = _itemModelFactory.PrepareQuyenSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(QuyenSearchModel searchModel)
        {
            if (!_itemService.Authorize(StandardPermissionProvider.ADMINQLQuyen))
                return AccessDeniedView();
            //prepare model
            if (searchModel.PageSize == 0) searchModel.PageSize = 10;
            var model = _itemModelFactory.PrepareQuyenListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_itemService.Authorize(StandardPermissionProvider.ADMINQLQuyen))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareQuyenModel(new QuyenModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(QuyenModel model, bool continueEditing)
        {
            if (!_itemService.Authorize(StandardPermissionProvider.ADMINQLQuyen))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<Quyen>();
               
                _itemService.InsertQuyen(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin", item.ToModel<QuyenModel>(), "QLQuyen");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareQuyenModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_itemService.Authorize(StandardPermissionProvider.ADMINQLQuyen))
                return AccessDeniedView();

            var item = _itemService.GetQuyenById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareQuyenModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(QuyenModel model, bool continueEditing)
        {
            if (!_itemService.Authorize(StandardPermissionProvider.ADMINQLQuyen))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetQuyenById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                var obj = item;
                _itemModelFactory.PrepareQuyen(model, item);
                _itemService.UpdateQuyen(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", obj.ToModel<QuyenModel>(), "QLQuyen");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareQuyenModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_itemService.Authorize(StandardPermissionProvider.ADMINQLQuyen))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetQuyenById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteQuyen(item);
                //activity log  
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<QuyenModel>(), "QLQuyen");
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

