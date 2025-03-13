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
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Extensions;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class NhanXeController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly INhanXeModelFactory _itemModelFactory;
        private readonly INhanXeService _itemService;
		private readonly ICauHinhModelFactory _cauHinhModelFactory;
        private readonly IDongBoFactory _dongBoFactory;
		#endregion

		#region Ctor
		public NhanXeController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            INhanXeModelFactory itemModelFactory,
            INhanXeService itemService,
            ICauHinhModelFactory cauHinhModelFactory,
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
			_cauHinhModelFactory = cauHinhModelFactory;
            this._dongBoFactory = dongBoFactory;
		}
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNhanXe))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new NhanXeSearchModel();
            var model = _itemModelFactory.PrepareNhanXeSearchModel(searchmodel);
            var _searchModel = HttpContext.Session.GetObject<NhanXeSearchModel>(enumTENCAUHINH.KeySearch+typeof(NhanXeSearchModel).Name);
            if (_searchModel != null && _searchModel.IsLoadSession)
            {
                _cauHinhModelFactory.PrePareModelBySession(model, _searchModel);
                UpdateSessionSearchModel<NhanXeSearchModel>(false);
            }
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(NhanXeSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNhanXe))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareNhanXeListModel(searchModel);
            HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch+searchModel.GetType().Name, searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNhanXe))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareNhanXeModel(new NhanXeModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(NhanXeModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNhanXe))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<NhanXe>();
                _itemService.InsertNhanXe(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<NhanXeModel>(), "NhanXe");
                model.ID = item.ID;
                _dongBoFactory.DongBoDanhMuc<NhanXeModel>(new List<NhanXeModel>() { model });
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareNhanXeModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNhanXe))
                return AccessDeniedView();

            var item = _itemService.GetNhanXeById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareNhanXeModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(NhanXeModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNhanXe))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetNhanXeById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareNhanXe(model, item);
                _itemService.UpdateNhanXe(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<NhanXeModel>(), "NhanXe");
                model.ID = item.ID;
                _dongBoFactory.DongBoDanhMuc<NhanXeModel>(new List<NhanXeModel>() { model });
                SuccessNotification("Cập nhật dữ liệu thành công !");
                UpdateSessionSearchModel<NhanXeSearchModel>(true);
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareNhanXeModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMNhanXe))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetNhanXeById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteNhanXe(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<NhanXeModel>(), "NhanXe");
                //activity log  
                UpdateSessionSearchModel<NhanXeSearchModel>(true);
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

