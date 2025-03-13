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
    public partial class CheDoHaoMonController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICheDoHaoMonModelFactory _itemModelFactory;
        private readonly ICheDoHaoMonService _itemService;
        private readonly IDongBoFactory _dongBoFactory;
        #endregion

        #region Ctor
        public CheDoHaoMonController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ICheDoHaoMonModelFactory itemModelFactory,
            ICheDoHaoMonService itemService,
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
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMCheDoHaoMon))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new CheDoHaoMonSearchModel();
            var model = _itemModelFactory.PrepareCheDoHaoMonSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(CheDoHaoMonSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMCheDoHaoMon))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareCheDoHaoMonListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMCheDoHaoMon))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareCheDoHaoMonModel(new CheDoHaoMonModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(CheDoHaoMonModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMCheDoHaoMon))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<CheDoHaoMon>();
                _itemService.InsertCheDoHaoMon(item);
                model.ID = item.ID;
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<CheDoHaoMonModel>(), "CheDoHaoMon");
                _dongBoFactory.DongBoDanhMuc<CheDoHaoMonModel>(new List<CheDoHaoMonModel>() { model });
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareCheDoHaoMonModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMCheDoHaoMon))
                return AccessDeniedView();

            var item = _itemService.GetCheDoHaoMonById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareCheDoHaoMonModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(CheDoHaoMonModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMCheDoHaoMon))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetCheDoHaoMonById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareCheDoHaoMon(model, item);
                _itemService.UpdateCheDoHaoMon(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<CheDoHaoMonModel>(), "CheDoHaoMon");
                _dongBoFactory.DongBoDanhMuc<CheDoHaoMonModel>(new List<CheDoHaoMonModel>() { model });
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareCheDoHaoMonModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMCheDoHaoMon))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetCheDoHaoMonById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                CheDoHaoMonModel model = item.ToModel<CheDoHaoMonModel>();
                _itemService.DeleteCheDoHaoMon(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<CheDoHaoMonModel>(), "CheDoHaoMon");
                //activity log  
                //đồng bộ dữ liệu qua kho
                _dongBoFactory.XoaDanhMuc<CheDoHaoMonModel>(model);
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

