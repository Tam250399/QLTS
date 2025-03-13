//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/2/2020
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
using GS.Web.Factories.DanhMuc;
using System.Linq;
using GS.Services.DanhMuc;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class GiamHongmatController : BaseWorksController
	{    
        #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly IGiamHongmatModelFactory _itemModelFactory;
            private readonly IGiamHongmatService _itemService;
        private readonly ICongCuService _congCuService;
        private readonly IXuatNhapService _xuatNhapService;
        private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
        private readonly INhapXuatCongCuService _nhapXuatCongCuService;
        private readonly IDonViService _donViService;
         #endregion     
        #region Ctor
        public GiamHongmatController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IGiamHongmatModelFactory itemModelFactory,
            IGiamHongmatService itemService,
            ICongCuService congCuService,
            IXuatNhapService xuatNhapService,
            IDonViBoPhanModelFactory donViBoPhanModelFactory,
            INhapXuatCongCuService nhapXuatCongCuService,
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
            this._congCuService = congCuService;
            this._xuatNhapService = xuatNhapService;
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
            this._nhapXuatCongCuService = nhapXuatCongCuService;
            this._donViService = donViService;
        }
        #endregion

        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new GiamHongmatSearchModel ();
            var model = _itemModelFactory.PrepareGiamHongmatSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(GiamHongmatSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareGiamHongmatListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create(Decimal MapId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var map = _nhapXuatCongCuService.GetNhapXuatCongCuById(MapId);
            var model = _itemModelFactory.PrepareGiamHongMatModel(new GiamHongmatModel(), map.ID, (Decimal)map.XuatNhap.DON_VI_BO_PHAN_ID);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(GiamHongmatModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();

            var mapping = _nhapXuatCongCuService.GetNhapXuatCongCuById(model.MapId);
            if (ModelState.IsValid)
            {                
                var sstXuat = _xuatNhapService.GetValueIdMax();
                var madonvifrom = _donViService.GetDonViById((Decimal)mapping.XuatNhap.DON_VI_ID).MA;
                var xuat = new XuatNhap
                {
                    NGAY_TAO = DateTime.Now,
                    DON_VI_BO_PHAN_ID = mapping.XuatNhap.DON_VI_BO_PHAN_ID,
                    DON_VI_ID = mapping.XuatNhap.DON_VI_ID,
                    FROM_XUAT_NHAP_ID = mapping.NHAP_XUAT_ID,
                    GHI_CHU = model.GHI_CHU,
                    IS_XUAT = true,
                    MA_LIEN_QUAN = mapping.XuatNhap.MA_LIEN_QUAN,
                    MA = sstXuat != null ? madonvifrom + "-" + sstXuat : madonvifrom + "-1",
                    NGAY_XUAT_NHAP = model.NGAY_LAP,
                    LOAI_XUAT_NHAP_ID = (Decimal)enumLoaiXuatNhap.GIAM_HONG_MAT
                };             
                _xuatNhapService.InsertXuatNhap(xuat);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", xuat.ToModel<XuatNhapModel>(), "XuatNhap");
                // mappingping xuất
                var mappingXuat = new NhapXuatCongCu
                {                   
                    CONG_CU_ID = mapping.CONG_CU_ID,
                    DON_GIA = mapping.DON_GIA,
                    NHAP_XUAT_ID = xuat.ID,
                    SO_LUONG = model.SO_LUONG,
                    THANH_TIEN = mapping.DON_GIA * model.SO_LUONG,
                };
                _nhapXuatCongCuService.InsertNhapXuatCongCu(mappingXuat);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", mappingXuat.ToModel<NhapXuatCongCuModel>(), "NhapXuatCongCu");

                var item = model.ToEntity<GiamHongmat>();
                item.DON_VI_ID = _workContext.CurrentDonVi.ID;
                item.CONG_CU_ID = mapping.CONG_CU_ID;
                item.NHAP_XUAT_ID = xuat.ID;
                item.DON_VI_BO_PHAN_ID = mapping.XuatNhap.DON_VI_BO_PHAN_ID;
                _itemService.InsertGiamHongmat(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<GiamHongmatModel>(), "GiamHongmat");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            var map = _nhapXuatCongCuService.GetNhapXuatCongCuById(model.MapId);
            model = _itemModelFactory.PrepareGiamHongMatModel(model, map.ID, (Decimal)map.XuatNhap.DON_VI_BO_PHAN_ID);
            return View(model);
        } 
        
        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();
                
            var item = _itemService.GetGiamHongmatById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var map = _nhapXuatCongCuService.GetNhapXuatCongCu(CongCuId: item.CONG_CU_ID, NhapXuatId: item.NHAP_XUAT_ID);
            var model = _itemModelFactory.PrepareGiamHongMatModel(new GiamHongmatModel(), map.ID, (Decimal)map.XuatNhap.DON_VI_BO_PHAN_ID);
            model.SO_LUONG = item.SO_LUONG;
            model.LY_DO = item.LY_DO;
            model.GHI_CHU = item.GHI_CHU;
            model.ID = item.ID;

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(GiamHongmatModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetGiamHongmatById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareGiamHongmat(model,item);
                _itemService.UpdateGiamHongmat(item); 
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<GiamHongmatModel>(), "GiamHongmat");
                //map
                var mapping = _nhapXuatCongCuService.GetNhapXuatCongCu(CongCuId:item.CONG_CU_ID,NhapXuatId:item.NHAP_XUAT_ID);
                if (mapping != null)
                {
                    mapping.SO_LUONG = model.SO_LUONG;
                    mapping.THANH_TIEN = mapping.DON_GIA * model.SO_LUONG;
                    _nhapXuatCongCuService.UpdateNhapXuatCongCu(mapping);
                    _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", mapping.ToModel<NhapXuatCongCuModel>(), "NhapXuatCongCu");
                }
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            var map = _nhapXuatCongCuService.GetNhapXuatCongCu(CongCuId: item.CONG_CU_ID, NhapXuatId: item.NHAP_XUAT_ID);
            model = _itemModelFactory.PrepareGiamHongMatModel(model, map.ID, (Decimal)map.XuatNhap.DON_VI_BO_PHAN_ID);
            return View(model);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetGiamHongmatById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                var mapping = _nhapXuatCongCuService.GetNhapXuatCongCu(CongCuId: item.CONG_CU_ID, NhapXuatId: item.NHAP_XUAT_ID);
                if (mapping!= null)
                {
                    _nhapXuatCongCuService.DeleteNhapXuatCongCu(mapping);
                    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", mapping.ToModel<NhapXuatCongCuModel>(), "NhapXuatCongCu");
                }
                var xn = _xuatNhapService.GetXuatNhapById(item.NHAP_XUAT_ID);
                if (xn != null)
                {
                    _xuatNhapService.DeleteXuatNhap(xn);
                    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", xn.ToModel<XuatNhapModel>(), "XuatNhap");
                }
                _itemService.DeleteGiamHongmat(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<GiamHongmatModel>(), "GiamHongmat");
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

        public virtual IActionResult _ChonTaiSan()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();
            var searchModel = new GiamHongmatSearchModel();
            searchModel.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true).ToList();

            return View(searchModel);
        }

        [HttpPost]
        public virtual IActionResult _ChonTaiSan(GiamHongmatSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();
            var model = _itemModelFactory.PrepareGiamHongMatForChonCongCu(searchModel);

            return Json(model);
        }
        #endregion
    }
}

