using GS.Core;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class NguonTaiSanController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly INguonTaiSanModelFactory _itemModelFactory;
        private readonly INguonTaiSanService _itemService;
        #endregion

        #region Ctor
        public NguonTaiSanController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            INguonTaiSanModelFactory itemModelFactory,
            INguonTaiSanService itemService
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
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNguonVon))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new NguonTaiSanSearchModel();
            var model = _itemModelFactory.PrepareNguonTaiSanSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(NguonTaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNguonVon))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareNguonTaiSanListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNguonVon))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareNguonTaiSanModel(new NguonTaiSanModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(NguonTaiSanModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNguonVon))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<NguonTaiSan>();
                _itemService.InsertNguonTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<NguonTaiSanModel>(), "NguonTaiSan");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareNguonTaiSanModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNguonVon))
                return AccessDeniedView();

            var item = _itemService.GetNguonTaiSanById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareNguonTaiSanModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(NguonTaiSanModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNguonVon))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetNguonTaiSanById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareNguonTaiSan(model, item);
                _itemService.UpdateNguonTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<NguonTaiSanModel>(), "NguonTaiSan");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareNguonTaiSanModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNguonVon))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetNguonTaiSanById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteNguonTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<NguonTaiSanModel>(), "NguonTaiSan");
                //activity log  
                //SuccessNotification("Xoá dữ liệu thành công");
                //return RedirectToAction("List");
                UpdateSessionSearchModel<NguonTaiSanSearchModel>(true);
                return JsonSuccessMessage("Xoá dữ liệu thành công");
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
