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
    public partial class MucDichSuDungController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IMucDichSuDungModelFactory _itemModelFactory;
        private readonly IMucDichSuDungService _itemService;
        private readonly IDongBoFactory _dongboFactory;
        #endregion

        #region Ctor
        public MucDichSuDungController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IMucDichSuDungModelFactory itemModelFactory,
            IMucDichSuDungService itemService,
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
            this._dongboFactory = dongBoFactory;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMMucDichSuDung))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new MucDichSuDungSearchModel();
            var model = _itemModelFactory.PrepareMucDichSuDungSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(MucDichSuDungSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMMucDichSuDung))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareMucDichSuDungListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMMucDichSuDung))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareMucDichSuDungModel(new MucDichSuDungModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(MucDichSuDungModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMMucDichSuDung))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<MucDichSuDung>();
                _itemService.InsertMucDichSuDung(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<MucDichSuDungModel>(), "MucDichSuDung");
                model.ID = item.ID;
                _dongboFactory.DongBoDanhMuc<MucDichSuDungModel>(new List<MucDichSuDungModel>() { model });
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareMucDichSuDungModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMMucDichSuDung))
                return AccessDeniedView();

            var item = _itemService.GetMucDichSuDungById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareMucDichSuDungModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(MucDichSuDungModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMMucDichSuDung))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetMucDichSuDungById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareMucDichSuDung(model, item);
                _itemService.UpdateMucDichSuDung(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<MucDichSuDungModel>(), "MucDichSuDung");
                _dongboFactory.DongBoDanhMuc<MucDichSuDungModel>(new List<MucDichSuDungModel>() { model });
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareMucDichSuDungModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMMucDichSuDung))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetMucDichSuDungById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                MucDichSuDungModel model = item.ToModel<MucDichSuDungModel>();
                _itemService.DeleteMucDichSuDung(item);
                // đồng bộ dữ liệu sang kho
                _dongboFactory.XoaDanhMuc<MucDichSuDungModel>(model);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<MucDichSuDungModel>(), "MucDichSuDung");
                
                //activity log  
                //SuccessNotification("Xoá dữ liệu thành công");
                //return RedirectToAction("List");
                UpdateSessionSearchModel<MucDichSuDungSearchModel>(true);
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

