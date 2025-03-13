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
using GS.Core.Domain.TaiSans;

using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.TaiSans;
using GS.Web.Controllers;
using GS.Web.Factories.TaiSans;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Services.NghiepVu;
using System.Linq;
using GS.Services.BienDongs;
using GS.Web.Framework.Extensions;
using GS.Web.Factories.HeThong;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class TaiSanKiemKeController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly ITaiSanKiemKeModelFactory _itemModelFactory;
            private readonly ITaiSanKiemKeService _itemService;
            private readonly ITaiSanKiemKeHoiDongService _taiSanKiemKeHoiDongService;
            private readonly ITaiSanService _taiSanService;
            private readonly IYeuCauService _yeuCauService;
            private readonly ITaiSanKiemKeTaiSanService _taiSanKiemKeTaiSanService;
            private readonly IBienDongService _bienDongService;
            private readonly IKiemKeTaiSanServices _kiemKeTaiSanService;
            private readonly ICauHinhModelFactory _cauHinhModelFactory;
        #endregion

        #region Ctor
        public TaiSanKiemKeController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ITaiSanKiemKeModelFactory itemModelFactory,
            ITaiSanKiemKeService itemService,
            ITaiSanKiemKeHoiDongService taiSanKiemKeHoiDongService,
            ITaiSanService taiSanService,
            IYeuCauService yeuCauService,
            ITaiSanKiemKeTaiSanService taiSanKiemKeTaiSanService,
            IBienDongService bienDongService,
            IKiemKeTaiSanServices kiemKeTaiSanService,
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
            this._taiSanKiemKeHoiDongService = taiSanKiemKeHoiDongService;
            this._taiSanService = taiSanService;
            this._yeuCauService = yeuCauService;
            this._taiSanKiemKeTaiSanService = taiSanKiemKeTaiSanService;
            this._bienDongService = bienDongService;
            this._kiemKeTaiSanService = kiemKeTaiSanService;
            this._cauHinhModelFactory = cauHinhModelFactory;
        }
        #endregion
        #region Methods

        public virtual IActionResult List(bool IsLoadSession= false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanKiemKeSearchModel ();
            var model = _itemModelFactory.PrepareTaiSanKiemKeSearchModel(searchmodel);
            var _searchModel = HttpContext.Session.GetObject<TaiSanKiemKeSearchModel>(enumTENCAUHINH.KeySearch + typeof(TaiSanKiemKeSearchModel).Name);
            if (_searchModel != null && IsLoadSession)
            {
                _cauHinhModelFactory.PrePareModelBySession(model, _searchModel);
                UpdateSessionSearchModel<TaiSanKiemKeSearchModel>(false);
            }
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(TaiSanKiemKeSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedKendoGridJson();
            //prepare model
            searchModel.DonViId = _workContext.CurrentDonVi.ID;
            var model = _itemModelFactory.PrepareTaiSanKiemKeListModel(searchModel);
            HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch + searchModel.GetType().Name, searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //prepare model
             var model = _itemModelFactory.PrepareTaiSanKiemKeModel(new TaiSanKiemKeModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(TaiSanKiemKeModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var maxId = _itemService.GetMaxValueId();
                var item = new TaiSanKiemKe();
                item = model.ToEntity<TaiSanKiemKe>();
                item.DON_VI_ID = _workContext.CurrentDonVi.ID;
                item.SO_BIEN_BAN = maxId != null ? (Convert.ToInt32(maxId.SO_BIEN_BAN)+1).ToString().PadLeft(5, '0') : "00001";
                _itemService.InsertTaiSanKiemKe(item);
                // insert kiểm kê tài sản

                var result = _kiemKeTaiSanService.KIEM_KE_TAI_SAN(DonViId: _workContext.CurrentDonVi.ID, DonViBoPhanId:model.DON_VI_BO_PHAN_ID,NgayKiemKe:model.NGAY_KIEM_KE,KiemKeId:item.ID);
                if (result)
                {
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<TaiSanKiemKeModel>(), "TaiSanKiemKe");
                    SuccessNotification("Tạo mới dữ liệu thành công !");
                    //return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("Detail", new { Id = item.ID });
                    return RedirectToAction("Edit", new { id = item.ID });
                }
                else
                {
                    ErrorNotification("Tạo mới dữ liệu không thành công !");
                    return View(model);
                }
                //var bdTaiSan = _bienDongService.GetBienDongMoiNhatByNgayBienDongForKiemKe(model.NGAY_KIEM_KE, (int)_workContext.CurrentDonVi.ID, model.DON_VI_BO_PHAN_ID != null ? (int)model.DON_VI_BO_PHAN_ID : 0);
                //var listTaiSan = _taiSanService.GetAllTaiSans(DonViBoPhanId: model.DON_VI_BO_PHAN_ID, trangthai: enumTRANG_THAI_TAI_SAN.DA_DUYET);
                //foreach (var bd in bdTaiSan)
                //{
                //    var taiSan = _taiSanService.GetTaiSanById(bd.TAI_SAN_ID);
                //    if(taiSan.TRANG_THAI_ID != Convert.ToDecimal(enumTRANG_THAI_TAI_SAN.XOA))
                //    {
                //        var kkts = new TaiSanKiemKeTaiSan();
                //        _itemModelFactory.PrepareKiemKeTaiSanByBienDong(kkts, bd, item.ID, model.NGAY_KIEM_KE);
                //        _taiSanKiemKeTaiSanService.InsertTaiSanKiemKeTaiSan(kkts);
                //    }
                //}
               
            }

            //prepare model
            model = _itemModelFactory.PrepareTaiSanKiemKeModel(model, null);            
            return View(model);
        } 
        
        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
                
            var item = _itemService.GetTaiSanKiemKeById(id);
            if (item == null)
                return RedirectToAction("List", new {IsLoadSession= true});
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanKiemKeModel(null, item);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Edit(TaiSanKiemKeModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            var item = _itemService.GetTaiSanKiemKeById(model.ID);
            if (item == null)
                return JsonErrorMessage("Cập nhật dữ liệu không thành công!");
            if (ModelState.IsValid)
            {
                //if (model.DON_VI_BO_PHAN_ID != item.DON_VI_BO_PHAN_ID || model.NGAY_KIEM_KE != item.NGAY_KIEM_KE)
                //{
                //    var listKKTS = _taiSanKiemKeTaiSanService.GetTaiSanKiemKeTaiSans(model.ID);
                //    _taiSanKiemKeTaiSanService.DeleteMultiTaiSanKiemKeTaiSan(listKKTS.Select(c => (decimal?)c.ID).ToArray());
                //    // insert kiểm kê tài sản
                //    var bdTaiSan = _bienDongService.GetBienDongMoiNhatByNgayBienDongForKiemKe(model.NGAY_KIEM_KE, (int)_workContext.CurrentDonVi.ID, model.DON_VI_BO_PHAN_ID != null ? (int)model.DON_VI_BO_PHAN_ID : 0);
                //    //var listTaiSan = _taiSanService.GetAllTaiSans(DonViBoPhanId: model.DON_VI_BO_PHAN_ID, trangthai: enumTRANG_THAI_TAI_SAN.DA_DUYET);
                //    foreach (var bd in bdTaiSan)
                //    {
                //        var taiSan = _taiSanService.GetTaiSanById(bd.TAI_SAN_ID);
                //        if (taiSan.TRANG_THAI_ID != Convert.ToDecimal(enumTRANG_THAI_TAI_SAN.XOA))
                //        {
                //            var kkts = new TaiSanKiemKeTaiSan();
                //            _itemModelFactory.PrepareKiemKeTaiSanByBienDong(kkts, bd, item.ID, model.NGAY_KIEM_KE);
                //            _taiSanKiemKeTaiSanService.InsertTaiSanKiemKeTaiSan(kkts);
                //        }
                //    }
                //}
                //_itemModelFactory.PrepareTaiSanKiemKe(model, item);
                item.NGUOI_LAP_BIEU = model.NGUOI_LAP_BIEU;
                item.TRUONG_BAN = model.TRUONG_BAN;
                item.DAI_DIEN_BPSD = model.DAI_DIEN_BPSD;
                item.KE_TOAN = model.KE_TOAN;
                _itemService.UpdateTaiSanKiemKe(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<TaiSanKiemKeModel>(), "TaiSanKiemKe");
                //SuccessNotification("Cập nhật dữ liệu thành công !");
                //return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("Detail", new { Id = item.ID });
                return JsonSuccessMessage("Cập nhật dữ liệu thành công!");
            }
            //prepare model
            var listError = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Cập nhật dữ liệu không thành công!", listError);

        }
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanKiemKeById(id);
            if (item == null)
                return RedirectToAction("List", new { IsLoadSession = true });
            try
            {
                //xóa kiểm kê tài sản
                var tskks = _taiSanKiemKeTaiSanService.GetTaiSanKiemKeTaiSans(item.ID);
                if (tskks.Count() > 0)
                {
                    _taiSanKiemKeTaiSanService.DeleteMultiTaiSanKiemKeTaiSan((tskks.Select(c=> (decimal?)c.ID)).ToArray());
                    foreach (var tskk in tskks)
                    {
                        _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", tskk.ToModel<TaiSanKiemKeTaiSanModel>(), "TaiSanKiemKeTaiSan");
                    }
                }
                //xóa hội đồng
                var tskkhds = _taiSanKiemKeHoiDongService.GetTaiSanKiemKeHoiDongs(KiemKeId:item.ID);
                foreach(var tskkhd in tskkhds)
                {
                    _taiSanKiemKeHoiDongService.DeleteTaiSanKiemKeHoiDong(tskkhd);
                    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", tskkhd.ToModel<TaiSanKiemKeHoiDongModel>(), "TaiSanKiemKeHoiDong");
                }
                _itemService.DeleteTaiSanKiemKe(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<TaiSanKiemKeModel>(), "TaiSanKiemKe");
                //activity log  
                SuccessNotification("Xoá dữ liệu thành công");
                return RedirectToAction("List", new { IsLoadSession = true });
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = item.ID });
            }
        }
        [HttpPost]
        public virtual IActionResult DeleteForList(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanKiemKeById(id);
            if (item == null)
                return JsonErrorMessage("Xoá dữ liệu không thành công");
            try
            {
                //xóa kiểm kê tài sản
                var tskks = _taiSanKiemKeTaiSanService.GetTaiSanKiemKeTaiSans(item.ID);
                if (tskks.Count() > 0)
                {
                    _taiSanKiemKeTaiSanService.DeleteMultiTaiSanKiemKeTaiSan((tskks.Select(c => (decimal?)c.ID)).ToArray());
                    foreach (var tskk in tskks)
                    {
                        _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", tskk.ToModel<TaiSanKiemKeTaiSanModel>(), "TaiSanKiemKeTaiSan");
                    }
                }
                //xóa hội đồng
                var tskkhds = _taiSanKiemKeHoiDongService.GetTaiSanKiemKeHoiDongs(KiemKeId: item.ID);
                foreach (var tskkhd in tskkhds)
                {
                    _taiSanKiemKeHoiDongService.DeleteTaiSanKiemKeHoiDong(tskkhd);
                    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", tskkhd.ToModel<TaiSanKiemKeHoiDongModel>(), "TaiSanKiemKeHoiDong");
                }
                _itemService.DeleteTaiSanKiemKe(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<TaiSanKiemKeModel>(), "TaiSanKiemKe");
                //activity log                 
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Xoá dữ liệu không thành công");
            }
        }

        public virtual IActionResult Detail(Decimal Id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();

            var item = _itemService.GetTaiSanKiemKeById(Id);
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanKiemKeModel(null, item);
            model.MaDonVi = _workContext.CurrentDonVi.MA_DON_VI;
            model.TenDonVi = _workContext.CurrentDonVi.TEN_DON_VI;
            
            return View(model);
        }
        public virtual IActionResult _Detail(Decimal Id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();

            var item = _itemService.GetTaiSanKiemKeById(Id);
            var model = item.ToModel<TaiSanKiemKeModel>();
            model.MaDonVi = _workContext.CurrentDonVi.MA_DON_VI;
            model.TenDonVi = _workContext.CurrentDonVi.TEN_DON_VI;
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult CreateHoiDongKiemKe(TaiSanKiemKeModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();

            foreach (var hoidong in model.ListHoiDongKiemKe)
            {
                if (hoidong.ID > 0)
                {
                    var item = _taiSanKiemKeHoiDongService.GetTaiSanKiemKeHoiDongById(hoidong.ID);
                    item.VI_TRI_ID = hoidong.VI_TRI_ID;
                    item.HO_TEN = hoidong.HO_TEN;
                    item.DAI_DIEN = hoidong.DAI_DIEN;
                    item.CHUC_VU = hoidong.CHUC_VU;
                    _taiSanKiemKeHoiDongService.UpdateTaiSanKiemKeHoiDong(item);
                }
                else
                {
                    var item = new TaiSanKiemKeHoiDong
                    {
                        KIEM_KE_ID = model.ID,
                        VI_TRI_ID = hoidong.VI_TRI_ID,
                        HO_TEN = hoidong.HO_TEN,
                        DAI_DIEN = hoidong.DAI_DIEN,
                        CHUC_VU = hoidong.CHUC_VU
                    };
                    _taiSanKiemKeHoiDongService.InsertTaiSanKiemKeHoiDong(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<TaiSanKiemKeHoiDongModel>(), "KiemKeHoiDong");
                }
            }

            return JsonSuccessMessage("Thành công");
        }
#endregion
    }
}

