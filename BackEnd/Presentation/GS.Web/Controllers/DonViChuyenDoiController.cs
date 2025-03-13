//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 25/3/2020
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


namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class DonViChuyenDoiController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService;
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly IDonViChuyenDoiModelFactory _itemModelFactory;
            private readonly IDonViChuyenDoiService _itemService;
        private readonly IDonViService _donViService;
        #endregion

        #region Ctor
        public DonViChuyenDoiController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IDonViChuyenDoiModelFactory itemModelFactory,
            IDonViChuyenDoiService itemService,
            IDonViService donViService
            )
        {            
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;            
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            _donViService = donViService;
        }
        #endregion
        #region Methods

        [HttpPost]
        public virtual IActionResult List(DonViChuyenDoiSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMuc))
                return AccessDeniedKendoGridJson();
            //prepare model
            searchModel = _itemModelFactory.PrepareDonViChuyenDoiSearchModel(searchModel);
            var model = _itemModelFactory.PrepareDonViChuyenDoiListModel(searchModel);

            return Json(model);
        }

        [HttpPost]
        public virtual IActionResult GetThongTinDonVi(int donViId)
        {
            var searchmodel = new DonViChuyenDoiSearchModel(donViId);
            var model = _itemModelFactory.PrepareDonViChuyenDoiSearchModel(searchmodel);
            return Json(model);
        }

        public virtual IActionResult Create(int donViId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMuc))
                return AccessDeniedView();
            //prepare model
            var tmp = _donViService.CheckDonVi(donViId, _workContext.CurrentCustomer.ID);
            if (!tmp)
            {
                return RedirectToAction("List", "DonVi");
            }
            var item = _itemService.GetDonViChuyenDoiFromDonViId(donViId);
            var donVi = _donViService.GetDonViById(donViId);
            var model = _itemModelFactory.PrepareDonViChuyenDoiModel(new DonViChuyenDoiModel(), item);
            model.NgayTaoDonVi = donVi.NGAY_TAO;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(DonViChuyenDoiModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMuc))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var tmp = _donViService.CheckDonVi(model.DON_VI_ID, _workContext.CurrentCustomer.ID);
                if (!tmp)
                {
                    ErrorNotification("Thay đổi dữ liệu thất bại !");
                    return RedirectToAction("List", "DonVi");
                }
                var item = model.ToEntity<DonViChuyenDoi>();
                item.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                item.LOAI_CAP_DON_VI_ID = model.LOAI_CAP_DON_VI_ID_MOI;
                item.CAP_DON_VI_ID = model.CAP_DON_VI_ID_MOI;
                item.MA_DVQHNS = model.MA_DVQHNS_MOI;
                _itemService.InsertDonViChuyenDoi(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<DonViChuyenDoiModel>(), "DonViChuyenDoi");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return RedirectToAction("List", "DonVi");
            }
            //prepare model
            var dvcd = _itemService.GetDonViChuyenDoiFromDonViId(Convert.ToInt32(model.DON_VI_ID));
            model = _itemModelFactory.PrepareDonViChuyenDoiModel(model, dvcd);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMuc))
                return AccessDeniedView();
            var item = _itemService.GetDonViChuyenDoiById(id);
            if (item == null)
                return RedirectToAction("List", "DonVi");
            var tmp = _donViService.CheckDonVi(item.DON_VI_ID, _workContext.CurrentCustomer.ID);
            if (!tmp)
            {
                return RedirectToAction("List", "DonVi");
            }
            //prepare model
            var model = _itemModelFactory.PrepareDonViChuyenDoiModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(DonViChuyenDoiModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMuc))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDonViChuyenDoiById(model.ID);
            if (item == null)
                return RedirectToAction("List", "DonVi");
            if (ModelState.IsValid)
            {

                var tmp = _donViService.CheckDonVi(item.DON_VI_ID, _workContext.CurrentCustomer.ID);
                if (!tmp)
                {
                    ErrorNotification("Thay đổi dữ liệu thất bại !");
                    return RedirectToAction("List", "DonVi");
                }
                _itemModelFactory.PrepareDonViChuyenDoi(model, item);
                item.LOAI_CAP_DON_VI_ID = model.LOAI_CAP_DON_VI_ID_MOI;
                item.CAP_DON_VI_ID = model.CAP_DON_VI_ID_MOI;
                item.MA_DVQHNS = model.MA_DVQHNS_MOI;
                _itemService.UpdateDonViChuyenDoi(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<DonViChuyenDoiModel>(), "DonViChuyenDoi");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List", "DonVi");
            }
            //prepare model
            model = _itemModelFactory.PrepareDonViChuyenDoiModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id, bool redirectToList = true)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMuc))
                return AccessDeniedView();
            //try to get a store with the specified id
            var tmp = _donViService.CheckDonVi(id, _workContext.CurrentCustomer.ID);
            if (!tmp)
            {
                return RedirectToAction("List", "DonVi");
            }
            var item = _itemService.GetDonViChuyenDoiById(id);
            if (item == null)
                return RedirectToAction("List", "DonVi");
            try
            {
                var donViId = item.DON_VI_ID;
                _itemService.DeleteDonViChuyenDoi(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<DonViChuyenDoiModel>(), "DonViChuyenDoi");
                //activity log  
                SuccessNotification("Xoá dữ liệu thành công");
                if(redirectToList == true)
                {
                    return RedirectToAction("List", "DonVi");
                }
                else
                {
                    return Json(new { success = true });
                }
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("List", "DonVi");
            }
        }
        #endregion       			
	}
}

