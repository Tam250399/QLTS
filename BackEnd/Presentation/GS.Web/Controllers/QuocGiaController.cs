//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Configuration;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Services.Common;
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
using GS.Web.Models.DongBoTaiSan;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class QuocGiaController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IQuocGiaModelFactory _itemModelFactory;
        private readonly IQuocGiaService _itemService;
        private readonly IGSAPIService _gSApiService;
        private readonly GSConfig _gsConfig;
        private readonly IDongBoFactory _dongBoFactory;
        #endregion

        #region Ctor
        public QuocGiaController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IQuocGiaModelFactory itemModelFactory,
            IQuocGiaService itemService,
            IGSAPIService gSAPIService,
            GSConfig gSConfig,
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
            this._gSApiService = gSAPIService;
            this._gsConfig = gSConfig;
            this._dongBoFactory = dongBoFactory;
        }
        #endregion

        #region Methods

        public virtual IActionResult List(int? pageIndex = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMQuocGia))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new QuocGiaSearchModel();

            var model = _itemModelFactory.PrepareQuocGiaSearchModel(searchmodel);
            if (pageIndex > 0)
            {
                model.Page = (int)pageIndex;
            }
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(QuocGiaSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMQuocGia))
                return AccessDeniedKendoGridJson();
            //prepare model

            var model = _itemModelFactory.PrepareQuocGiaListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create(int? currentPage = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMQuocGia))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareQuocGiaModel(new QuocGiaModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public IActionResult Create(QuocGiaModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMQuocGia))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<QuocGia>();
                _itemService.InsertQuocGia(item);
                model.ID = item.ID;
                _dongBoFactory.DongBoDanhMuc<QuocGiaModel>(new List<QuocGiaModel>() { model });
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<QuocGiaModel>(), "QuocGia");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }        
            //prepare model
            model = _itemModelFactory.PrepareQuocGiaModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id, int pageIndex)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMQuocGia))
                return AccessDeniedView();

            var item = _itemService.GetQuocGiaById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareQuocGiaModel(null, item);
            model.pageIndex = pageIndex;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(QuocGiaModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMQuocGia))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetQuocGiaById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareQuocGia(model, item);
                _itemService.UpdateQuocGia(item);   
              _dongBoFactory.DongBoDanhMuc<QuocGiaModel>(new List<QuocGiaModel>() { model });
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<QuocGiaModel>(), "QuocGia");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID, pageIndex = model.pageIndex }) : RedirectToAction("List", new { pageIndex = model.pageIndex });
            }
            //prepare model
            model = _itemModelFactory.PrepareQuocGiaModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual  IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMQuocGia))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetQuocGiaById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                QuocGiaModel model = item.ToModel<QuocGiaModel>();
                _itemService.DeleteQuocGia(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<QuocGiaModel>(), "QuocGia");
                // đồng bộ dữ liệu sang kho
               _dongBoFactory.XoaDanhMuc<QuocGiaModel>(model);
                //activity log  
                //SuccessNotification("Xoá dữ liệu thành công");
                //return RedirectToAction("List");
                UpdateSessionSearchModel<QuocGiaSearchModel>(true);
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

