//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
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
using GS.Web.Framework.Extensions;
using GS.Web.Factories.HeThong;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class KiemKeController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IKiemKeModelFactory _itemModelFactory;
        private readonly IKiemKeService _itemService;
        private readonly IKiemKeHoiDongService _kiemKeHoiDongService;
        private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
        private readonly IKiemKeCongCuService _kiemKeCongCuService;
        private readonly INhapXuatCongCuService _nhapXuatCongCuService;
        private readonly ICauHinhModelFactory _cauHinhModelFactory;
        #endregion

        #region Ctor
        public KiemKeController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IKiemKeModelFactory itemModelFactory,
            IKiemKeService itemService,
            IKiemKeHoiDongService kiemKeHoiDongService,
            IDonViBoPhanModelFactory donViBoPhanModelFactory,
            IKiemKeCongCuService kiemKeCongCuService,
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
            this._kiemKeHoiDongService = kiemKeHoiDongService;
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
            this._kiemKeCongCuService = kiemKeCongCuService;
            this._nhapXuatCongCuService = nhapXuatCongCuService;
            this._cauHinhModelFactory = cauHinhModelFactory;
        }
        #endregion
        #region Methods

        public virtual IActionResult List(/*int CurrentPage*/bool IsLoadSession = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new KiemKeSearchModel();
            var model = _itemModelFactory.PrepareKiemKeSearchModel(searchmodel);
            //if (CurrentPage > 0)
            //{
            //    model.Page = CurrentPage;
            //}
            var _searchModel = HttpContext.Session.GetObject<KiemKeSearchModel>(enumTENCAUHINH.KeySearch+typeof(KiemKeSearchModel).Name);
            if (_searchModel != null && IsLoadSession)
            {
                _cauHinhModelFactory.PrePareModelBySession(model, _searchModel);
                UpdateSessionSearchModel<KiemKeSearchModel>(false);
            }
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(KiemKeSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareKiemKeListModel(searchModel);
            HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch+searchModel.GetType().Name, searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrapareKiemKe(new KiemKeModel());
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(KiemKeModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<KiemKe>();
                var maxId = _itemService.GetKiemKeIdMax(_workContext.CurrentDonVi.ID);
                item.SO_KIEM_KE = maxId != null ? (Convert.ToInt32(maxId.SO_KIEM_KE) + 1).ToString().PadLeft(5, '0') : "00001";
                item.DON_VI_ID = _workContext.CurrentDonVi.ID;
                item.DON_VI_BO_PHAN_ID = model.DON_VI_BO_PHAN_ID > 0 ? model.DON_VI_BO_PHAN_ID : null;
                _itemService.InsertKiemKe(item);
                // kiểm kê công cụ
                var listCongCu = _nhapXuatCongCuService.GetMapForKiemKeCongCu(DonViBoPhanId: model.DON_VI_BO_PHAN_ID, NgayKiemKe: model.NGAY_KIEM_KE);
                foreach (var congcu in listCongCu)
                {
                    var kkcc = new KiemKeCongCu
                    {
                        CONG_CU_ID = congcu.CONG_CU_ID,
                        IS_PHAT_HIEN_THUA = false,
                        KIEM_KE_ID = item.ID,
                        SO_LUONG_KIEM_KE = congcu.SoLuongCoThePhanBo,
                        SO_LUONG_SO_SACH = congcu.SoLuongCoThePhanBo,
                        XUAT_NHAP_ID = congcu.NHAP_XUAT_ID,
                        DON_VI_BO_PHAN_ID = congcu.XuatNhap.DON_VI_BO_PHAN_ID,
                        DON_GIA = congcu.DON_GIA
                    };
                    _kiemKeCongCuService.InsertKiemKeCongCu(kkcc);
                }
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<KiemKeModel>(), "KiemKe");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                //return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("Detail", new { Id = item.ID });
                return RedirectToAction("Edit", new { id = item.ID });
            }

            //prepare model
            model = _itemModelFactory.PrepareKiemKeModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id, int CurrentPage)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();

            var item = _itemService.GetKiemKeById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareKiemKeModel(null, item);
            model.CurrentPage = CurrentPage;
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Edit(KiemKeModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKiemKeById(model.ID);
            if (item == null)
                return JsonErrorMessage("Cập nhật dữ liệu không thành công!");
            if (ModelState.IsValid)
            {
                //if (model.DON_VI_BO_PHAN_ID != item.DON_VI_BO_PHAN_ID)
                //{
                //    var listKKCC = _kiemKeCongCuService.GetKiemKeCongCus(item.ID);
                //    foreach (var kkcc in listKKCC)
                //    {
                //        _kiemKeCongCuService.DeleteKiemKeCongCu(kkcc);
                //    }
                //    var listCongCu = _nhapXuatCongCuService.GetMapForKiemKeCongCu(DonViBoPhanId: model.DON_VI_BO_PHAN_ID);
                //    foreach (var congcu in listCongCu)
                //    {
                //        var kkcc = new KiemKeCongCu
                //        {
                //            CONG_CU_ID = congcu.CONG_CU_ID,
                //            IS_PHAT_HIEN_THUA = false,
                //            KIEM_KE_ID = item.ID,
                //            SO_LUONG_KIEM_KE = congcu.SoLuongCoThePhanBo,
                //            SO_LUONG_SO_SACH = congcu.SoLuongCoThePhanBo,
                //            XUAT_NHAP_ID = congcu.NHAP_XUAT_ID,
                //            DON_VI_BO_PHAN_ID = congcu.XuatNhap.DON_VI_BO_PHAN_ID,
                //            DON_GIA = congcu.DON_GIA
                //        };
                //        _kiemKeCongCuService.InsertKiemKeCongCu(kkcc);
                //    }
                //}
                //_itemModelFactory.PrepareKiemKe(model, item);
                item.NGUOI_LAP_BIEU = model.NGUOI_LAP_BIEU;
                item.TRUONG_BAN = model.TRUONG_BAN;
                item.DAI_DIEN_BO_PHAN = model.DAI_DIEN_BO_PHAN;
                item.KE_TOAN = model.KE_TOAN;
                _itemService.UpdateKiemKe(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<KiemKeModel>(), "KiemKe");
               // SuccessNotification("Cập nhật dữ liệu thành công !");

                //return continueEditing ? RedirectToAction("Edit", new { id = item.ID, CurrentPage=model.CurrentPage }) : RedirectToAction("List", new { CurrentPage = model.CurrentPage });
                return JsonSuccessMessage("Cập nhật dữ liệu thành công!");
            }
            //prepare model
            var listError = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Cập nhật dữ liệu không thành công!", listError);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKiemKeById(id);
            if (item == null)
                return JsonErrorMessage("Cập nhật dữ liệu không thành công!");
            try
            {
                _itemService.DeleteKiemKe(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<KiemKeModel>(), "KiemKe");
                //activity log 
                return JsonSuccessMessage("Cập nhật dữ liệu thành công!");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Cập nhật dữ liệu không thành công!");
            }
        }

        public virtual IActionResult Detail(Decimal Id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();

            var item = _itemService.GetKiemKeById(Id);
            var model = item.ToModel<KiemKeModel>();
            model.MaDonVi = _workContext.CurrentDonVi.MA_DON_VI;
            model.TenDonVi = _workContext.CurrentDonVi.TEN_DON_VI;
            model.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(valSelected: model.DON_VI_BO_PHAN_ID, isAddFirst: true);
            var listHoiDong = _kiemKeHoiDongService.GetKiemKeHoiDongs(KiemKeId: Id);
            if (listHoiDong != null && listHoiDong.Count > 0)
            {
                model.ListKiemKeHoiDongModel = listHoiDong.Select(c => c.ToModel<KiemKeHoiDongModel>()).ToList();
            }
            var listCCThua = _kiemKeCongCuService.GetKiemKeCongCus(KiemKeId: Id);
            if (listCCThua != null && listCCThua.Count > 0)
            {
                model.ListKiemKeCongCuThua = listCCThua.Select(c => c.ToModel<KiemKeCongCuModel>()).ToList();
            }

            return View(model);
        }
        public virtual IActionResult _Detail(Decimal Id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();

            var item = _itemService.GetKiemKeById(Id);
            var model = item.ToModel<KiemKeModel>();
            model.MaDonVi = _workContext.CurrentDonVi.MA_DON_VI;
            model.TenDonVi = _workContext.CurrentDonVi.TEN_DON_VI;
            model.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(isAddFirst: true, DonViId:_workContext.CurrentDonVi.ID);
            var listHoiDong = _kiemKeHoiDongService.GetKiemKeHoiDongs(KiemKeId: Id);
            if (listHoiDong != null && listHoiDong.Count > 0)
            {
                model.ListKiemKeHoiDongModel = listHoiDong.Select(c => c.ToModel<KiemKeHoiDongModel>()).ToList();
            }
            var listCCThua = _kiemKeCongCuService.GetKiemKeCongCus(KiemKeId: Id);
            if (listCCThua != null && listCCThua.Count > 0)
            {
                model.ListKiemKeCongCuThua = listCCThua.Select(c => c.ToModel<KiemKeCongCuModel>()).ToList();
            }

            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult CreateHoiDongKiemKe(KiemKeModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();

            foreach (var hoidong in model.ListHoiDongKiemKe)
            {
                if (hoidong.ID > 0)
                {
                    var item = _kiemKeHoiDongService.GetKiemKeHoiDongById(hoidong.ID);
                    item.VI_TRI_ID = hoidong.VI_TRI_ID;
                    item.HO_TEN = hoidong.HO_TEN;
                    item.DAI_DIEN = hoidong.DAI_DIEN;
                    item.CHUC_VU = hoidong.CHUC_VU;
                    _kiemKeHoiDongService.UpdateKiemKeHoiDong(item);
                }
                else
                {
                    var item = new KiemKeHoiDong
                    {
                        KIEM_KE_ID = model.ID,
                        VI_TRI_ID = hoidong.VI_TRI_ID,
                        HO_TEN = hoidong.HO_TEN,
                        DAI_DIEN = hoidong.DAI_DIEN,
                        CHUC_VU = hoidong.CHUC_VU
                    };
                    _kiemKeHoiDongService.InsertKiemKeHoiDong(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<KiemKeHoiDongModel>(), "KiemKeHoiDong");
                }
            }

            return JsonSuccessMessage("Thành công");
        }
        #endregion
    }
}

