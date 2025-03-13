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
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using GS.Web.Factories.DongBo;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class HinhThucXuLyController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IHinhThucXuLyModelFactory _itemModelFactory;
        private readonly IHinhThucXuLyService _itemService;
        private readonly IDongBoFactory _dongBoFactory;
        #endregion

        #region Ctor
        public HinhThucXuLyController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IHinhThucXuLyModelFactory itemModelFactory,
            IHinhThucXuLyService itemService,
            IDongBoFactory dongBoFactory
            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._dongBoFactory = dongBoFactory;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHinhThucXuLy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new HinhThucXuLySearchModel();
            var model = _itemModelFactory.PrepareHinhThucXuLySearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(HinhThucXuLySearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHinhThucXuLy))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareHinhThucXuLyListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHinhThucXuLy))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareHinhThucXuLyModel(new HinhThucXuLyModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(HinhThucXuLyModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHinhThucXuLy))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                if (model.ListHinhXuLyId != null && model.ListHinhXuLyId.Count() > 0)
                {
                    foreach (var htxl in model.ListHinhXuLyId)
                    {
                        model.PHUONG_AN_XU_LY_ID = htxl;
                        var item = model.ToEntity<HinhThucXuLy>();
                        _itemService.InsertHinhThucXuLy(item);
                        _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<HinhThucXuLyModel>(), "HinhThucXuLy");
                        _dongBoFactory.DongBoDanhMuc<HinhThucXuLyModel>(new List<HinhThucXuLyModel>() { item.ToModel<HinhThucXuLyModel>() });
                    }
                }
                else
                {
                    var item = model.ToEntity<HinhThucXuLy>();
                    _itemService.InsertHinhThucXuLy(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<HinhThucXuLyModel>(), "HinhThucXuLy");
                }

                SuccessNotification("Tạo mới dữ liệu thành công !");
                return RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareHinhThucXuLyModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHinhThucXuLy))
                return AccessDeniedView();

            var item = _itemService.GetHinhThucXuLyById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareHinhThucXuLyModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(HinhThucXuLyModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHinhThucXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetHinhThucXuLyById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareHinhThucXuLy(model, item);
                _itemService.UpdateHinhThucXuLy(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<HinhThucXuLyModel>(), "HinhThucXuLy");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareHinhThucXuLyModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHinhThucXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetHinhThucXuLyById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteHinhThucXuLy(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<HinhThucXuLyModel>(), "HinhThucXuLy");
                //activity log  
                //SuccessNotification("Xoá dữ liệu thành công");
                //return RedirectToAction("List");
                UpdateSessionSearchModel<HinhThucXuLySearchModel>(true);
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = item.ID });
            }
        }
        public virtual IActionResult GetDDLPhuongAnByHinhThuc(int HinhThucId = 0)
        {
            return Json(_itemModelFactory.DDLPhuongAnByHinhThuc(HinhThucId: HinhThucId));
        }
        #endregion
    }
}

