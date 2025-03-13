//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
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
using GS.Web.Factories.DanhMuc;
using GS.Services.CCDC;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using System.Collections.Generic;
using System.Linq;
using GS.Services;
using GS.Web.Framework.Extensions;
using GS.Web.Factories.HeThong;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class XuatNhapController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IXuatNhapModelFactory _itemModelFactory;
        private readonly IXuatNhapService _itemService;
        private readonly ICongCuModelFactory _congCuModelFactory;
        private readonly ICongCuService _congCuService;
        private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
        private readonly INhapXuatCongCuService _nhapXuatCongCuService;
        private readonly INhapXuatCongCuModelFactory _nhapXuatCongCuModelFactory;
        private readonly ISuaChuaBaoDuongService _suaChuaBaoDuongService;
        private readonly IKiemKeCongCuService _kiemKeCongCuService;
        private readonly IChoThueService _choThueService;
        private readonly ICauHinhModelFactory _cauHinhModelFactory;
        #endregion

        #region Ctor
        public XuatNhapController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IXuatNhapModelFactory itemModelFactory,
            IXuatNhapService itemService,
            ICongCuModelFactory congCuModelFactory,
            ICongCuService congCuService,
            IDonViBoPhanModelFactory donViBoPhanModelFactory,
            INhapXuatCongCuService nhapXuatCongCuService,
            INhapXuatCongCuModelFactory nhapXuatCongCuModelFactory,
            ISuaChuaBaoDuongService suaChuaBaoDuongService,
            IKiemKeCongCuService kiemKeCongCuService,
            IChoThueService choThueService,
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
            this._congCuModelFactory = congCuModelFactory;
            this._congCuService = congCuService;
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
            this._nhapXuatCongCuService = nhapXuatCongCuService;
            this._nhapXuatCongCuModelFactory = nhapXuatCongCuModelFactory;
            this._suaChuaBaoDuongService = suaChuaBaoDuongService;
            this._kiemKeCongCuService = kiemKeCongCuService;
            this._choThueService = choThueService;
            this._cauHinhModelFactory = cauHinhModelFactory;
        }
        #endregion
        #region Methods

        public virtual IActionResult List(bool IsLoadSession = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLPhanBoCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new XuatNhapSearchModel();
            var model = _itemModelFactory.PrepareXuatNhapSearchModel(searchmodel);
            var _searchModel = HttpContext.Session.GetObject<XuatNhapSearchModel>(enumTENCAUHINH.KeySearch + typeof(XuatNhapSearchModel).Name);
            if (_searchModel != null && IsLoadSession)
            {
                _cauHinhModelFactory.PrePareModelBySession(model, _searchModel);
                UpdateSessionSearchModel<XuatNhapSearchModel>(false);
            }
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(XuatNhapSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLPhanBoCongCuDungCu))
                return AccessDeniedKendoGridJson();
            //prepare model
            searchModel.IsPhanBo = true;
            var model = _itemModelFactory.PrepareXuatNhapPhanBoListModel(searchModel);
            HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch + searchModel.GetType().Name, searchModel);
            return Json(model);
        }
        [HttpGet]
        public virtual IActionResult _ThongTinCongCu(decimal NhapXuatId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLPhanBoCongCuDungCu))
                return AccessDeniedKendoGridJson();
            //prepare model
          
            return PartialView(NhapXuatId);
        }
        [HttpPost]
        public virtual IActionResult ListThongTinCongCu(decimal NhapXuatId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLPhanBoCongCuDungCu))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareThongTinCongCuModel(NhapXuatId);
            return Json(model);
        }

        public virtual IActionResult Create(String StringId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLPhanBoCongCuDungCu))
                return AccessDeniedView();

            var model = _itemModelFactory.PreparePhanBoModel(StringId);

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Create(XuatNhapModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLPhanBoCongCuDungCu))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                foreach (var m in model.ListMap)
                {
                    var map = _nhapXuatCongCuService.GetNhapXuatCongCuById(m.ID);
                    // xuất kho
                    var sttxuatkho = _itemService.GetValueIdMax().GetValueOrDefault(1);
                    var xuatkho = new XuatNhap()
                    {
                        IS_XUAT = true,
                        DON_VI_ID = _workContext.CurrentDonVi.ID,
                        NGUOI_DUNG_ID = _workContext.CurrentCustomer.ID,
                        GHI_CHU = model.GHI_CHU,
                        MA_LIEN_QUAN = map.XuatNhap.MA_LIEN_QUAN,
                        FROM_XUAT_NHAP_ID = map.XuatNhap.ID,
                        NGAY_XUAT_NHAP = model.NGAY_XUAT_NHAP,
                        TRANG_THAI_ID = map.XuatNhap.TRANG_THAI_ID,
                        MA =  _workContext.CurrentDonVi.MA_DON_VI + "-" + sttxuatkho,
                        LOAI_XUAT_NHAP_ID = (Decimal)enumLoaiXuatNhap.PHAN_BO
                    };
                    _itemService.InsertXuatNhap(xuatkho);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", xuatkho.ToModel<XuatNhapModel>(), "XuatNhap");
                    // nhập đơn vị bộ phận
                    var sttnhapdvbp = _itemService.GetValueIdMax().GetValueOrDefault(1);
                    var nhapdvbp = new XuatNhap()
                    {
                        FROM_XUAT_NHAP_ID = xuatkho.ID,
                        DON_VI_BO_PHAN_ID = model.DON_VI_BO_PHAN_ID,
                        NGUOI_DUNG_ID = _workContext.CurrentCustomer.ID,
                        IS_XUAT = false,
                        DON_VI_ID = _workContext.CurrentDonVi.ID,
                        GHI_CHU = model.GHI_CHU,
                        TRANG_THAI_ID = model.TRANG_THAI_ID,
                        NGAY_XUAT_NHAP = model.NGAY_XUAT_NHAP,
                        MA_LIEN_QUAN = map.XuatNhap.MA_LIEN_QUAN,
                        MA = _workContext.CurrentDonVi.MA_DON_VI + "-" + sttnhapdvbp,
                        LOAI_XUAT_NHAP_ID = (Decimal)enumLoaiXuatNhap.PHAN_BO
                    };
                    _itemService.InsertXuatNhap(nhapdvbp);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", nhapdvbp.ToModel<XuatNhapModel>(), "XuatNhap");
                    // mapping
                    var khoXuatMap = new NhapXuatCongCu()
                    {
                        CONG_CU_ID = map.CONG_CU_ID,
                        NHAP_XUAT_ID = xuatkho.ID,
                        SO_LUONG = m.SO_LUONG,
                        TRANG_THAI_ID = model.TRANG_THAI_ID,
                        NHAP_KHO_ID = map.ID,
                        DON_GIA = map.DON_GIA
                    };
                    _nhapXuatCongCuService.InsertNhapXuatCongCu(khoXuatMap);
                    var dvbpNhapMap = new NhapXuatCongCu()
                    {
                        CONG_CU_ID = map.CONG_CU_ID,
                        NHAP_XUAT_ID = nhapdvbp.ID,
                        SO_LUONG = m.SO_LUONG,
                        TRANG_THAI_ID = model.TRANG_THAI_ID,
                        DON_GIA = map.DON_GIA
                    };
                    _nhapXuatCongCuService.InsertNhapXuatCongCu(dvbpNhapMap);
                }

                return JsonSuccessMessage("Phân bổ thành công !");
            }

            //prepare model
            var listmodel = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Phân bổ không thành công !", listmodel);
        }

        public virtual IActionResult Edit(String MapId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLPhanBoCongCuDungCu))
                return AccessDeniedView();
            var item = _nhapXuatCongCuService.GetNhapXuatCongCuById(Decimal.Parse(MapId));
            if (item == null)
                return RedirectToAction("List");
            var model = _itemModelFactory.PreparePhanBoModel(MapId, whenEdit: true);
            model.NGAY_XUAT_NHAP = item.XuatNhap.NGAY_XUAT_NHAP;
            model.DON_VI_BO_PHAN_ID = item.XuatNhap.DON_VI_BO_PHAN_ID;
            model.TRANG_THAI_ID = item.TRANG_THAI_ID;
            model.GHI_CHU = item.XuatNhap.GHI_CHU;

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Edit(XuatNhapModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLPhanBoCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var map = _nhapXuatCongCuService.GetNhapXuatCongCuById(Decimal.Parse(model.StringMapId));
            if (map == null)
                return JsonErrorMessage("Phân bổ không thành công !", model);
            if (ModelState.IsValid)
            {
                //nhập kho
                var item = _itemService.GetXuatNhapById(map.NHAP_XUAT_ID);
                item.NGAY_XUAT_NHAP = model.NGAY_XUAT_NHAP;
                item.DON_VI_BO_PHAN_ID = model.DON_VI_BO_PHAN_ID;
                item.TRANG_THAI_ID = model.TRANG_THAI_ID;
                item.GHI_CHU = model.GHI_CHU;
                _itemService.UpdateXuatNhap(item);
                //xuất kho
                var xk = _itemService.GetXuatNhaps(maLienQuan: item.MA_LIEN_QUAN, isXuat: true).FirstOrDefault();
                foreach (var cc in model.ListMap)
                {
                    // map xuất kho
                    if (xk != null)
                    {
                        var xkcc = _nhapXuatCongCuService.GetNhapXuatCongCus(CongCuId: map.CONG_CU_ID, NhapXuatId: xk.ID, TrangThai: (decimal)map.TRANG_THAI_ID, DonGia: (decimal)map.DON_GIA).Where(c => c.SO_LUONG == map.SO_LUONG).FirstOrDefault();
                        if (xkcc != null)
                        {
                            xkcc.SO_LUONG = cc.SO_LUONG;
                            xkcc.TRANG_THAI_ID = model.TRANG_THAI_ID;
                            _nhapXuatCongCuService.UpdateNhapXuatCongCu(xkcc);
                            _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", xkcc.ToModel<NhapXuatCongCuModel>(), "NhapXuatCongCu");
                        }
                    }
                    map.SO_LUONG = cc.SO_LUONG;
                    map.TRANG_THAI_ID = model.TRANG_THAI_ID;
                    _nhapXuatCongCuService.UpdateNhapXuatCongCu(map);
                    _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", map.ToModel<NhapXuatCongCuModel>(), "NhapXuatCongCu");
                }

                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<XuatNhapModel>(), "XuatNhap");
                return JsonSuccessMessage("Phân bổ thành công !");
            }
            //prepare model
            var listmodel = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Phân bổ không thành công !", listmodel);
        }

        public virtual IActionResult Delete(String MapId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLPhanBoCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var map = _nhapXuatCongCuService.GetNhapXuatCongCuById(Decimal.Parse(MapId));
            if (map == null)
                return RedirectToAction("List");
            var xnLienQuan = _itemService.GetXuatNhaps(isXuat: false, fromId: map.NHAP_XUAT_ID);
            if (xnLienQuan != null && xnLienQuan.Count() > 0)
                return JsonErrorMessage("Không được phép xóa", MapId);
            try
            {
                var xuatMap = _nhapXuatCongCuService.GetNhapXuatCongCu(CongCuId: map.CONG_CU_ID, NhapXuatId: (Decimal)map.XuatNhap.FROM_XUAT_NHAP_ID);
                
                _nhapXuatCongCuService.DeleteNhapXuatCongCu(map);
                _nhapXuatCongCuService.DeleteNhapXuatCongCu(xuatMap);
                var mapNew = _nhapXuatCongCuService.GetNhapXuatCongCus(NhapXuatId: map.NHAP_XUAT_ID);
                if (mapNew.Count == 0 || mapNew == null)
                {
                    var xuat = _itemService.GetXuatNhapById((Decimal)map.XuatNhap.FROM_XUAT_NHAP_ID);
                    _itemService.DeleteXuatNhap(xuat);
                    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", xuat.ToModel<XuatNhapModel>(), "XuatNhap");
                    var nhap = _itemService.GetXuatNhapById(map.NHAP_XUAT_ID);
                    _itemService.DeleteXuatNhap(nhap);
                    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", nhap.ToModel<XuatNhapModel>(), "XuatNhap");
                }
                //activity log
                return JsonSuccessMessage("Xoá dữ liệu thành công", MapId);
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                ErrorNotification("Có lỗi trong quá trình xử lý");
                return JsonErrorMessage("Có lỗi trong quá trình xử lý", MapId);
            }
        }

        public virtual IActionResult ListGhiTangCongCu()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLPhanBoCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new CongCuSearchModel();
            var model = _congCuModelFactory.PrepareCongCuSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult ListGhiTangCongCu(CongCuSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLPhanBoCongCuDungCu))
                return AccessDeniedKendoGridJson();
            //prepare model
            searchModel.isPhanBo = true;
            var model = _nhapXuatCongCuModelFactory.PrepareForPhanBo(searchModel);
            return Json(model);
        }

        public virtual IActionResult ListLuanChuyen(bool IsLoadSession = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLLuanChuyenCongCuDungCu))
                return AccessDeniedKendoGridJson();
            var searchModel = _itemModelFactory.PrepareLuanChuyenSearchModel(new LuanChuyenSearchModel());
            var _searchModel = HttpContext.Session.GetObject<LuanChuyenSearchModel>(enumTENCAUHINH.KeySearch + typeof(LuanChuyenSearchModel).Name);
            if (_searchModel != null && IsLoadSession)
            {
                _cauHinhModelFactory.PrePareModelBySession(searchModel, _searchModel);
                UpdateSessionSearchModel<LuanChuyenSearchModel>(false);
            }
            return View(searchModel);
        }

        [HttpPost]
        public virtual IActionResult ListLuanChuyen(LuanChuyenSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLLuanChuyenCongCuDungCu))
                return AccessDeniedKendoGridJson();

            var model = _itemModelFactory.PrepareLuanChuyenListModel(searchModel);
            HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch + searchModel.GetType().Name, searchModel);
            return Json(model);
        }
        public virtual IActionResult ChonTaiSanLuanChuyen()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLLuanChuyenCongCuDungCu))
                return AccessDeniedKendoGridJson();

            var searchModel = new XuatNhapSearchModel();
            searchModel.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true).ToList();

            return View(searchModel);
        }
        public virtual IActionResult _ChonTaiSanLuanChuyen()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLLuanChuyenCongCuDungCu))
                return AccessDeniedKendoGridJson();

            var searchModel = new XuatNhapSearchModel();
            searchModel.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true).ToList();

            return PartialView(searchModel);
        }

        [HttpPost]
        public virtual IActionResult _ChonTaiSanLuanChuyen(XuatNhapSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLLuanChuyenCongCuDungCu))
                return AccessDeniedKendoGridJson();

            var model = _itemModelFactory.PrepareLuanChuyenCongCu(searchModel);

            return Json(model);
        }

        public virtual IActionResult CreateLuanChuyen(Decimal MapId, Decimal boPhanId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLLuanChuyenCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareCongCuForLuanChuyen(new XuatNhapModel(), MapId, boPhanId);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult CreateLuanChuyen(XuatNhapModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLLuanChuyenCongCuDungCu))
                return AccessDeniedView();

            var map = _nhapXuatCongCuService.GetNhapXuatCongCuById(model.MapId);
            var xuatnhap = _itemService.GetXuatNhapById(map.NHAP_XUAT_ID);
            if (ModelState.IsValid)
            {
                // xuat
                var xuat = model.ToEntity<XuatNhap>();
                var sttxuat = _itemService.GetValueIdMax().GetValueOrDefault(1);
                xuat.FROM_XUAT_NHAP_ID = xuatnhap.ID;
                xuat.DON_VI_ID = _workContext.CurrentDonVi.ID;
                xuat.MA = _workContext.CurrentDonVi.MA_DON_VI + "-" + sttxuat;
                xuat.MA_LIEN_QUAN = xuatnhap.MA_LIEN_QUAN;
                xuat.IS_XUAT = true;
                xuat.NGAY_XUAT_NHAP = model.NGAY_XUAT_NHAP;
                xuat.DON_VI_BO_PHAN_ID = model.BoPhanId;
                xuat.LOAI_XUAT_NHAP_ID = (Decimal)enumLoaiXuatNhap.LUAN_CHUYEN;
                xuat.TRANG_THAI_ID = xuatnhap.TRANG_THAI_ID;
                xuat.NGUOI_DUNG_ID = _workContext.CurrentCustomer.ID;
                _itemService.InsertXuatNhap(xuat);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", xuat.ToModel<XuatNhapModel>(), "LuanChuyen");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                // mapping xuat
                var mapxuat = new NhapXuatCongCu()
                {
                    CONG_CU_ID = map.CONG_CU_ID,
                    NHAP_XUAT_ID = xuat.ID,
                    SO_LUONG = model.SoLuongPhanBo,
                    DON_GIA = map.DON_GIA,
                    TRANG_THAI_ID = map.TRANG_THAI_ID,
                    NHAP_KHO_ID = map.ID
                };
                _nhapXuatCongCuService.InsertNhapXuatCongCu(mapxuat);
                // nhap
                var sttnhap = _itemService.GetValueIdMax().GetValueOrDefault(1);
                var nhap = new XuatNhap
                {
                    FROM_XUAT_NHAP_ID = xuat.ID,
                    DON_VI_ID = _workContext.CurrentDonVi.ID,
                    MA = _workContext.CurrentDonVi.MA_DON_VI + "-" + sttnhap,
                    MA_LIEN_QUAN = xuatnhap.MA_LIEN_QUAN,
                    IS_XUAT = false,
                    LOAI_XUAT_NHAP_ID = (Decimal)enumLoaiXuatNhap.LUAN_CHUYEN,
                    DON_VI_BO_PHAN_ID = model.DON_VI_BO_PHAN_ID,
                    GHI_CHU = model.GHI_CHU,
                    MUC_DICH_XUAT_NHAP_ID = null,
                    QUYET_DINH_NGAY = model.QUYET_DINH_NGAY,
                    QUYET_DINH_SO = model.QUYET_DINH_SO,
                    NGAY_XUAT_NHAP = model.NGAY_XUAT_NHAP,
                    NGUOI_DUNG_ID = _workContext.CurrentCustomer.ID,
                    TRANG_THAI_ID = xuatnhap.TRANG_THAI_ID

            };
                _itemService.InsertXuatNhap(nhap);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", nhap.ToModel<XuatNhapModel>(), "LuanChuyen");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                // mapping nhap
                var mapnhap = new NhapXuatCongCu()
                {
                    SO_LUONG = model.SoLuongPhanBo,
                    NHAP_XUAT_ID = nhap.ID,
                    CONG_CU_ID = map.CONG_CU_ID,
                    DON_GIA = map.DON_GIA,
                    TRANG_THAI_ID = map.TRANG_THAI_ID
                };
                _nhapXuatCongCuService.InsertNhapXuatCongCu(mapnhap);

                return continueEditing ? RedirectToAction("Edit", new { GUID = nhap.GUID }) : RedirectToAction("ListLuanChuyen");
            }

            var returnmodel = _itemModelFactory.PrepareCongCuForLuanChuyen(model, model.MapId, model.BoPhanId);

            return View(returnmodel);
        }

        public virtual IActionResult EditLuanChuyen(Guid GUID)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLLuanChuyenCongCuDungCu))
                return AccessDeniedKendoGridJson();

            var item = _itemService.GetXuatNhapByGuid(GUID);
            if (item == null)
                return RedirectToAction("ListLuanChuyen");
            var mapFrom = _nhapXuatCongCuService.GetNhapXuatCongCus(NhapXuatId: item.FROM_XUAT_NHAP_ID).FirstOrDefault();
            var map = _nhapXuatCongCuService.GetNhapXuatCongCus(NhapXuatId: item.ID).FirstOrDefault();
            var model = item.ToModel<XuatNhapModel>();
            var XuatFromDieuChuyen = _itemService.GetXuatNhapById((decimal)item.FROM_XUAT_NHAP_ID);
            model = _itemModelFactory.PrepareCongCuForLuanChuyen(model, mapFrom.ID, (Decimal)(XuatFromDieuChuyen != null? XuatFromDieuChuyen.DON_VI_BO_PHAN_ID:0), whenEdit: true);
            model.SoLuongPhanBo = (Decimal)map.SO_LUONG;

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult EditLuanChuyen(XuatNhapModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLLuanChuyenCongCuDungCu))
                return AccessDeniedKendoGridJson();
            var item = _itemService.GetXuatNhapById(model.ID);
            if (item == null)
                return RedirectToAction("ListLuanChuyen");
            var mapFrom = _nhapXuatCongCuService.GetNhapXuatCongCus(NhapXuatId: item.FROM_XUAT_NHAP_ID).FirstOrDefault();
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareLuanChuyenEdit(model, item);
                _itemService.UpdateXuatNhap(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<XuatNhapModel>(), "LuanChuyen");
                // xuất kho
                var xk = _itemService.GetXuatNhaps(maLienQuan: item.MA_LIEN_QUAN, isXuat: true).FirstOrDefault();
                // update mapping
                var map = _nhapXuatCongCuService.GetNhapXuatCongCus(NhapXuatId: item.ID).FirstOrDefault();
                // map xuất kho
                if (xk != null)
                {
                    var xkcc = _nhapXuatCongCuService.GetNhapXuatCongCus(CongCuId: map.CONG_CU_ID, NhapXuatId: xk.ID).Where(c => c.SO_LUONG == map.SO_LUONG).FirstOrDefault();
                    if (xkcc != null)
                    {
                        xkcc.SO_LUONG = model.SoLuongPhanBo;
                        _nhapXuatCongCuService.UpdateNhapXuatCongCu(xkcc);
                        _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", xkcc.ToModel<NhapXuatCongCuModel>(), "NhapXuatCongCu");
                    }
                }
                map.SO_LUONG = model.SoLuongPhanBo;
                _nhapXuatCongCuService.UpdateNhapXuatCongCu(map);
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("EditLuanChuyen", new { GUID = item.GUID }) : RedirectToAction("ListLuanChuyen");
            }

            var returnmodel = _itemModelFactory.PrepareCongCuForLuanChuyen(model, mapFrom.ID, (Decimal)_itemService.GetXuatNhapById((Decimal)item.FROM_XUAT_NHAP_ID).DON_VI_BO_PHAN_ID, whenEdit: true);

            return View(returnmodel);
        }

        [HttpPost]
        public virtual IActionResult DeleteLuanChuyen(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLLuanChuyenCongCuDungCu))
                return AccessDeniedKendoGridJson();
            //try to get a store with the specified id
            var item = _itemService.GetXuatNhapById(id);
            if (item == null)
                return JsonErrorMessage("Có lỗi trong quá trình xử lý", id);
            var map = _nhapXuatCongCuService.GetNhapXuatCongCus(NhapXuatId: id).FirstOrDefault();
            if (map == null)
                return JsonErrorMessage("Có lỗi trong quá trình xử lý", id);
            var xnLienQuan = _itemService.GetXuatNhaps(isXuat: false, fromId: id);
            if (xnLienQuan != null && xnLienQuan.Count() > 0)
                return JsonErrorMessage("Không được phép xóa", id);
            var baoduong = _suaChuaBaoDuongService.GetSuaChuaBaoDuongs(item.ID);
            if (baoduong != null && baoduong.Count() > 0)
                return JsonErrorMessage("Không được phép xóa", id);
            var kiemke = _kiemKeCongCuService.GetKiemKeCongCus(XuatNhapId: item.ID);
            if (kiemke != null && kiemke.Count() > 0)
                return JsonErrorMessage("Không được phép xóa", id);
            var chothue = _choThueService.GetChoThues(item.ID);
            if (chothue != null && chothue.Count() > 0)
                return JsonErrorMessage("Không được phép xóa", id);
            try
            {
                // delete xuat from luan chuyen
                var xuat = _itemService.GetXuatNhapById((decimal)item.FROM_XUAT_NHAP_ID);
                if (xuat == null)
                    return JsonErrorMessage("Có lỗi trong quá trình xử lý", id);
                var xuatmap = _nhapXuatCongCuService.GetNhapXuatCongCuByNhapXuatId(xuat.ID).FirstOrDefault();
                if (xuatmap == null)
                    return JsonErrorMessage("Có lỗi trong quá trình xử lý", id);
                _nhapXuatCongCuService.DeleteNhapXuatCongCu(xuatmap);
                _itemService.DeleteXuatNhap(xuat);
                // delete map
                _nhapXuatCongCuService.DeleteNhapXuatCongCu(map);
                // delete item
                _itemService.DeleteXuatNhap(item);
                //activity log
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<XuatNhapModel>(), "LuanChuyen");
                return JsonSuccessMessage("Xoá dữ liệu thành công", id);
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Có lỗi trong quá trình xử lý", id);
            }
        }
        #endregion
    }
}

