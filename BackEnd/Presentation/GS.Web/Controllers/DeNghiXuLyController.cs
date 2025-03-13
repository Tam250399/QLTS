//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/11/2020
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
using System.Linq;
using System.Collections.Generic;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class DeNghiXuLyController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IDeNghiXuLyModelFactory _itemModelFactory;
        private readonly IDeNghiXuLyService _itemService;
        private readonly IDeNghiXuLyTaiSanService _deNghiXuLyTaiSanService;
        #endregion

        #region Ctor
        public DeNghiXuLyController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IDeNghiXuLyModelFactory itemModelFactory,
            IDeNghiXuLyService itemService,
            IDeNghiXuLyTaiSanService deNghiXuLyTaiSanService
            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._deNghiXuLyTaiSanService = deNghiXuLyTaiSanService;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDeNghiXuLy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new DeNghiXuLySearchModel();
            var model = _itemModelFactory.PrepareDeNghiXuLySearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(DeNghiXuLySearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDeNghiXuLy))
                return AccessDeniedKendoGridJson();
            //prepare model
            searchModel.DON_VI_ID = _workContext.CurrentDonVi.ID;
            var model = _itemModelFactory.PrepareDeNghiXuLyListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDeNghiXuLy))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareDeNghiXuLyModel(new DeNghiXuLyModel(), null);
            model.DON_VI_ID = _workContext.CurrentDonVi.ID;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(DeNghiXuLyModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDeNghiXuLy))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<DeNghiXuLy>();
                _itemService.InsertDeNghiXuLy(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<DeNghiXuLyModel>(), "DeNghiXuLy");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareDeNghiXuLyModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDeNghiXuLy))
                return AccessDeniedView();

            var item = _itemService.GetDeNghiXuLyById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareDeNghiXuLyModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(DeNghiXuLyModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDeNghiXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDeNghiXuLyById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareDeNghiXuLy(model, item);
                _itemService.UpdateDeNghiXuLy(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<DeNghiXuLyModel>(), "DeNghiXuLy");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareDeNghiXuLyModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDeNghiXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDeNghiXuLyById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                List<DeNghiXuLyTaiSan> ArrTaiSanDeNghi = _deNghiXuLyTaiSanService.GetAllDeNghiXuLyTaiSans(DeNghiXuLyID: id).ToList();
                if (ArrTaiSanDeNghi != null && ArrTaiSanDeNghi.Count > 0)
                    _deNghiXuLyTaiSanService.DeleteDeNghiXuLyTaiSan(ArrTaiSanDeNghi);
                _itemService.DeleteDeNghiXuLy(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<DeNghiXuLyModel>(), "DeNghiXuLy");
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
        [HttpPost]
        public virtual IActionResult _delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDeNghiXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDeNghiXuLyById(id);
            if (item == null)
                return JsonErrorMessage("Xoá dữ liệu thất bại");
            try
            {
                List<DeNghiXuLyTaiSan> ArrTaiSanDeNghi = _deNghiXuLyTaiSanService.GetAllDeNghiXuLyTaiSans(DeNghiXuLyID: id).ToList();
                if (ArrTaiSanDeNghi != null && ArrTaiSanDeNghi.Count > 0)
                    _deNghiXuLyTaiSanService.DeleteDeNghiXuLyTaiSan(ArrTaiSanDeNghi);
                _itemService.DeleteDeNghiXuLy(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<DeNghiXuLyModel>(), "DeNghiXuLy");
                //activity log  
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                return JsonErrorMessage("Xoá dữ liệu thất bại");
            }
        }
        #endregion
    }
}

