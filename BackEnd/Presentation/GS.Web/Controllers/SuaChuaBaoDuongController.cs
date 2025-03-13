//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.CCDC;

using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.CCDC;
using GS.Web.Controllers;
using GS.Web.Factories.CCDC;
using GS.Services.CCDC;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Services.DanhMuc;
using GS.Web.Factories.DanhMuc;
using System.Linq;
using GS.Web.Framework.Extensions;
using GS.Web.Factories.HeThong;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class SuaChuaBaoDuongController : BaseWorksController
	{    
        #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly ISuaChuaBaoDuongModelFactory _itemModelFactory;
            private readonly ISuaChuaBaoDuongService _itemService;
            private readonly ICongCuService _congCuService;
            private readonly IXuatNhapService _xuatNhapService;
            private readonly IDonViBoPhanService _donViBoPhanService;
            private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
            private readonly INhapXuatCongCuService _nhapXuatCongCuService;
            private readonly ICauHinhModelFactory _cauHinhModelFactory;
        #endregion

        #region Ctor
        public SuaChuaBaoDuongController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ISuaChuaBaoDuongModelFactory itemModelFactory,
            ISuaChuaBaoDuongService itemService,
            ICongCuService congCuService,
            IXuatNhapService xuatNhapService,
            IDonViBoPhanService donViBoPhanService,
            IDonViBoPhanModelFactory donViBoPhanModelFactory,
            INhapXuatCongCuService nhapXuatCongCuService,
            ICauHinhModelFactory cauHinhModelFactory
            )
        {            
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;            
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._congCuService = congCuService;
            this._xuatNhapService = xuatNhapService;
            this._donViBoPhanService = donViBoPhanService;
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
            this._nhapXuatCongCuService = nhapXuatCongCuService;
            this._cauHinhModelFactory = cauHinhModelFactory;
        }
        #endregion

        #region Methods

        public virtual IActionResult List(bool IsLoadSession = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLSuaChuaCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new SuaChuaBaoDuongSearchModel ();
            var model = _itemModelFactory.PrepareSuaChuaBaoDuongSearchModel(searchmodel);
            var _searchModel = HttpContext.Session.GetObject<SuaChuaBaoDuongSearchModel>(enumTENCAUHINH.KeySearch + typeof(SuaChuaBaoDuongSearchModel).Name);
            if (_searchModel != null && IsLoadSession)
            {
                _cauHinhModelFactory.PrePareModelBySession(model, _searchModel);
                UpdateSessionSearchModel<SuaChuaBaoDuongSearchModel>(false);
            }
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(SuaChuaBaoDuongSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLSuaChuaCongCuDungCu))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareSuaChuaBaoDuongListModel(searchModel);
            HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch + searchModel.GetType().Name, searchModel);
            return Json(model);
        }

        public virtual IActionResult Create(Decimal MapId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLSuaChuaCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var map = _nhapXuatCongCuService.GetNhapXuatCongCuById(MapId);
            var model = _itemModelFactory.PrepareSuaChuaBaoDuongModel(new SuaChuaBaoDuongModel(), map.ID, (Decimal)map.XuatNhap.DON_VI_BO_PHAN_ID);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(SuaChuaBaoDuongModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLSuaChuaCongCuDungCu))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<SuaChuaBaoDuong>();
                var mapping = _nhapXuatCongCuService.GetNhapXuatCongCuById(model.MapId);
                item.CONG_CU_ID = mapping.CONG_CU_ID;
                item.NHAP_XUAT_ID = mapping.NHAP_XUAT_ID;
                _itemService.InsertSuaChuaBaoDuong(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<SuaChuaBaoDuongModel>(), "SuaChuaBaoDuong");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            var map = _nhapXuatCongCuService.GetNhapXuatCongCuById(model.MapId);
            model = _itemModelFactory.PrepareSuaChuaBaoDuongModel(model, map.ID, (Decimal)map.XuatNhap.DON_VI_BO_PHAN_ID);
            return View(model);
        }
        
        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLSuaChuaCongCuDungCu))
                return AccessDeniedView();
                
            var item = _itemService.GetSuaChuaBaoDuongById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var map = _nhapXuatCongCuService.GetNhapXuatCongCu(CongCuId: item.CONG_CU_ID, NhapXuatId: item.NHAP_XUAT_ID);
            var model = _itemModelFactory.PrepareSuaChuaBaoDuongModel(item.ToModel<SuaChuaBaoDuongModel>(), map.ID, (Decimal)map.XuatNhap.DON_VI_BO_PHAN_ID);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(SuaChuaBaoDuongModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLSuaChuaCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetSuaChuaBaoDuongById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareSuaChuaBaoDuong(model,item);
                _itemService.UpdateSuaChuaBaoDuong(item); 
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<SuaChuaBaoDuongModel>(), "SuaChuaBaoDuong");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            var map = _nhapXuatCongCuService.GetNhapXuatCongCu(CongCuId: item.CONG_CU_ID, NhapXuatId: item.NHAP_XUAT_ID);
            model = _itemModelFactory.PrepareSuaChuaBaoDuongModel(model, map.ID, (Decimal)map.XuatNhap.DON_VI_BO_PHAN_ID);
            return View(model);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLSuaChuaCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetSuaChuaBaoDuongById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteSuaChuaBaoDuong(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<SuaChuaBaoDuongModel>(), "SuaChuaBaoDuong");
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
        public virtual IActionResult DeleteFromList(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLSuaChuaCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetSuaChuaBaoDuongById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteSuaChuaBaoDuong(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<SuaChuaBaoDuongModel>(), "SuaChuaBaoDuong");
                //activity log  
                return JsonSuccessMessage("Xoá dữ liệu thành công",id);
                
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Xoá dữ liệu không thành công", id);
            }
        }
        public virtual IActionResult ChonTaiSan(bool Check = true)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLSuaChuaCongCuDungCu))
                return AccessDeniedView();

            var model = new SuaChuaBaoDuongSearchModel();
            model.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true).ToList();

            return View(model);
        }

        public virtual IActionResult _ChonTaiSan()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLSuaChuaCongCuDungCu))
                return AccessDeniedView();

            var model = new SuaChuaBaoDuongSearchModel();
            model.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true).ToList();
            return PartialView(model);

        }

        [HttpPost]
        public virtual IActionResult _ChonTaiSan(SuaChuaBaoDuongSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLSuaChuaCongCuDungCu))
                return AccessDeniedView();

            var model = _itemModelFactory.PrepareSuaChuaModelForChonCongCu(searchModel);

            return Json(model);
        }
        #endregion
    }
}

