//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
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


namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class HinhThucMuaSamController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IHinhThucMuaSamModelFactory _itemModelFactory;
        private readonly IHinhThucMuaSamService _itemService;
        #endregion

        #region Ctor
        public HinhThucMuaSamController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IHinhThucMuaSamModelFactory itemModelFactory,
            IHinhThucMuaSamService itemService
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
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHinhThucMuaSam))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new HinhThucMuaSamSearchModel();
            var model = _itemModelFactory.PrepareHinhThucMuaSamSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(HinhThucMuaSamSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHinhThucMuaSam))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareHinhThucMuaSamListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHinhThucMuaSam))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareHinhThucMuaSamModel(new HinhThucMuaSamModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(HinhThucMuaSamModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHinhThucMuaSam))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<HinhThucMuaSam>();
                _itemService.InsertHinhThucMuaSam(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<HinhThucMuaSamModel>(), "HinhThucMuaSam");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareHinhThucMuaSamModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHinhThucMuaSam))
                return AccessDeniedView();

            var item = _itemService.GetHinhThucMuaSamById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareHinhThucMuaSamModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(HinhThucMuaSamModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHinhThucMuaSam))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetHinhThucMuaSamById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareHinhThucMuaSam(model, item);
                _itemService.UpdateHinhThucMuaSam(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<HinhThucMuaSamModel>(), "HinhThucMuaSam");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareHinhThucMuaSamModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHinhThucMuaSam))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetHinhThucMuaSamById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteHinhThucMuaSam(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<HinhThucMuaSamModel>(), "HinhThucMuaSam");
                //activity log  
                //SuccessNotification("Xoá dữ liệu thành công");
                //return RedirectToAction("List");
                UpdateSessionSearchModel<HinhThucMuaSamSearchModel>(true);
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

