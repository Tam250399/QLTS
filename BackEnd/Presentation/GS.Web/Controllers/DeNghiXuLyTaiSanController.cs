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

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class DeNghiXuLyTaiSanController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IDeNghiXuLyTaiSanModelFactory _itemModelFactory;
        private readonly IDeNghiXuLyTaiSanService _itemService;
        private readonly IDeNghiXuLyService _deNghiXuLyService;
        private readonly ITaiSanService _taiSanService;
        #endregion

        #region Ctor
        public DeNghiXuLyTaiSanController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IDeNghiXuLyTaiSanModelFactory itemModelFactory,
            IDeNghiXuLyTaiSanService itemService,
            IDeNghiXuLyService deNghiXuLyService,
            ITaiSanService taiSanService
            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._deNghiXuLyService = deNghiXuLyService;
            this._taiSanService = taiSanService;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDeNghiXuLy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new DeNghiXuLyTaiSanSearchModel();
            var model = _itemModelFactory.PrepareDeNghiXuLyTaiSanSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(DeNghiXuLyTaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDeNghiXuLy))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareDeNghiXuLyTaiSanListModel(searchModel);
            return Json(model);
        }
        public virtual IActionResult _List(decimal DE_NGHI_XU_LY_ID)
        {
            //prepare model
            var searchmodel = new DeNghiXuLyTaiSanSearchModel();
            var model = _itemModelFactory.PrepareDeNghiXuLyTaiSanSearchModel(searchmodel);
            model.DE_NGHI_XU_LY_ID = DE_NGHI_XU_LY_ID;
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult _List(DeNghiXuLyTaiSanSearchModel searchModel)
        {
            var model = _itemModelFactory.PrepareDeNghiXuLyTaiSanListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDeNghiXuLy))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareDeNghiXuLyTaiSanModel(new DeNghiXuLyTaiSanModel(), null);
            return View(model);
        }
        public virtual IActionResult _CreateOrUpdateForXuLy(decimal DE_NGHI_XU_LY_ID, decimal ID)
        {
            //prepare model
            DeNghiXuLyTaiSanModel model = new DeNghiXuLyTaiSanModel();
            if (ID > 0)
            {
                DeNghiXuLyTaiSan item = _itemService.GetDeNghiXuLyTaiSanById(ID);
                model = _itemModelFactory.PrepareDeNghiXuLyTaiSanModel(model, item);
                return PartialView(model);
            }
            else
            {
                _itemModelFactory.PrepareDeNghiXuLyTaiSanModel(model, null);
                model.DE_NGHI_XU_LY_ID = DE_NGHI_XU_LY_ID;
                return PartialView(model);
            }

        }
        [HttpPost]
        public virtual IActionResult _CreateOrUpdateForXuLy(DeNghiXuLyTaiSanModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDeNghiXuLy))
                    return AccessDeniedView();

            if (ModelState.IsValid)
            {
                if (model.ID > 0)
                {
                    var item = _itemService.GetDeNghiXuLyTaiSanById(model.ID);
                    _itemModelFactory.PrepareDeNghiXuLyTaiSan(model, item);
                    TaiSan taiSan = _taiSanService.GetTaiSanByMa(model.MA_TAI_SAN);
                    item.TAI_SAN_ID = taiSan.ID;
                    _itemService.UpdateDeNghiXuLyTaiSan(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<DeNghiXuLyTaiSanModel>(), "DeNghiXuLyTaiSan");
                    return JsonSuccessMessage("Cập nhật dữ liệu thành công !");
                }
                else
                {
                    var item = model.ToEntity<DeNghiXuLyTaiSan>();
                    TaiSan taiSan = _taiSanService.GetTaiSanByMa(model.MA_TAI_SAN);
                    item.TAI_SAN_ID = taiSan.ID;
                    _itemService.InsertDeNghiXuLyTaiSan(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<DeNghiXuLyTaiSanModel>(), "DeNghiXuLyTaiSan");
                    return JsonSuccessMessage("Tạo mới dữ liệu thành công !");
                }
            }
            else
            {
                var listERR = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
                return JsonErrorMessage("Cập nhật dữ liệu không thành công", listERR);
            }
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(DeNghiXuLyTaiSanModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDeNghiXuLy))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<DeNghiXuLyTaiSan>();
                _itemService.InsertDeNghiXuLyTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<DeNghiXuLyTaiSanModel>(), "DeNghiXuLyTaiSan");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareDeNghiXuLyTaiSanModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDeNghiXuLy))
                return AccessDeniedView();

            var item = _itemService.GetDeNghiXuLyTaiSanById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareDeNghiXuLyTaiSanModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(DeNghiXuLyTaiSanModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDeNghiXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDeNghiXuLyTaiSanById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareDeNghiXuLyTaiSan(model, item);
                _itemService.UpdateDeNghiXuLyTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<DeNghiXuLyTaiSanModel>(), "DeNghiXuLyTaiSan");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareDeNghiXuLyTaiSanModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDeNghiXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDeNghiXuLyTaiSanById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteDeNghiXuLyTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<DeNghiXuLyTaiSanModel>(), "DeNghiXuLyTaiSan");
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
            var item = _itemService.GetDeNghiXuLyTaiSanById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteDeNghiXuLyTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<DeNghiXuLyTaiSanModel>(), "DeNghiXuLyTaiSan");
                //activity log  
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                return JsonErrorMessage("Xoá dữ liệu thành công");
            }
        }
        #endregion
    }
}

