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
using GS.Web.Factories.DongBo;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class NguonGocTaiSanController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly INguonGocTaiSanModelFactory _itemModelFactory;
        private readonly INguonGocTaiSanService _itemService;
        private readonly IDongBoFactory _dongBoFactory;
        #endregion

        #region Ctor
        public NguonGocTaiSanController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            INguonGocTaiSanModelFactory itemModelFactory,
            INguonGocTaiSanService itemService,
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
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNguonGocTaiSan))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new NguonGocTaiSanSearchModel();
            var model = _itemModelFactory.PrepareNguonGocTaiSanSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(NguonGocTaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNguonGocTaiSan))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareNguonGocTaiSanListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNguonGocTaiSan))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareNguonGocTaiSanModel(new NguonGocTaiSanModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(NguonGocTaiSanModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNguonGocTaiSan))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<NguonGocTaiSan>();
                _itemService.InsertNguonGocTaiSan(item);
                model.ID = item.ID;
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<NguonGocTaiSanModel>(), "NguonGocTaiSan");
                _dongBoFactory.DongBoDanhMuc<NguonGocTaiSanModel>(new List<NguonGocTaiSanModel>() { model });
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareNguonGocTaiSanModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNguonGocTaiSan))
                return AccessDeniedView();

            var item = _itemService.GetNguonGocTaiSanById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareNguonGocTaiSanModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(NguonGocTaiSanModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNguonGocTaiSan))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetNguonGocTaiSanById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareNguonGocTaiSan(model, item);
                _itemService.UpdateNguonGocTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<NguonGocTaiSanModel>(), "NguonGocTaiSan");
                _dongBoFactory.DongBoDanhMuc<NguonGocTaiSanModel>(new List<NguonGocTaiSanModel>() { model });

                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareNguonGocTaiSanModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNguonGocTaiSan))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetNguonGocTaiSanById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                NguonGocTaiSanModel model = item.ToModel<NguonGocTaiSanModel>();
                _itemService.DeleteNguonGocTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<NguonGocTaiSanModel>(), "NguonGocTaiSan");
                // đồng bộ dữ liệu sang kho
                _dongBoFactory.XoaDanhMuc<NguonGocTaiSanModel>(model);
                //activity log  
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

