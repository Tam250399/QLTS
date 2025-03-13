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
using GS.Web.Factories.DongBo;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class LoaiDonViController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly ILoaiDonViModelFactory _itemModelFactory;
        private readonly ILoaiDonViService _itemService;
        private readonly IDongBoFactory _dongBoFactory;
        #endregion

        #region Ctor
        public LoaiDonViController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ILoaiDonViModelFactory itemModelFactory,
            ILoaiDonViService itemService,
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
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiDonVi))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new LoaiDonViSearchModel();
            var model = _itemModelFactory.PrepareLoaiDonViSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(LoaiDonViSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiDonVi))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareLoaiDonViListModel(searchModel);
            return Json(model);
        }



        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiDonVi))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareLoaiDonViModel(new LoaiDonViModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(LoaiDonViModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiDonVi))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<LoaiDonVi>();
                _itemService.InsertLoaiDonVi(item);
                // _itemService.UpdateLoaiDonVi(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<LoaiDonViModel>(), "LoaiDonVi");
                model.ID = item.ID;
                _dongBoFactory.DongBoDanhMuc<LoaiDonViModel>(new List<LoaiDonViModel>() { model});
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareLoaiDonViModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiDonVi))
                return AccessDeniedView();

            var item = _itemService.GetLoaiDonViById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareLoaiDonViModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(LoaiDonViModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiDonVi))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetLoaiDonViById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareLoaiDonVi(model, item);
                _itemService.UpdateLoaiDonVi(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<LoaiDonViModel>(), "LoaiDonVi");
                _dongBoFactory.DongBoDanhMuc<LoaiDonViModel>(new List<LoaiDonViModel>() { model });
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareLoaiDonViModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLoaiDonVi))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetLoaiDonViById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                LoaiDonViModel model = item.ToModel<LoaiDonViModel>();
                _itemService.DeleteLoaiDonVi(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<LoaiDonViModel>(), "LoaiDonVi");
                // đồng bộ dữ liệu sang kho
                _dongBoFactory.XoaDanhMuc<LoaiDonViModel>(model);
                //activity log  
                //SuccessNotification("Xoá dữ liệu thành công");
                //return RedirectToAction("List");
                UpdateSessionSearchModel<LoaiDonViSearchModel>(true);
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = item.ID });
            }
        }
        #endregion
        [HttpPost]
       public ActionResult GetMaByParentID(decimal parentID)
        {
            var result = _itemService.GetLoaiDonViById(parentID);
            return Json(result);
        }
    }
}

