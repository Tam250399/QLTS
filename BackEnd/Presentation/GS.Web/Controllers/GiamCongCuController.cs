using GS.Core;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.CCDC;
using GS.Core.Domain.DanhMuc;
using GS.Services;
using GS.Services.CCDC;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.CCDC;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Extensions;
using GS.Web.Models.CCDC;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Controllers
{
    public class GiamCongCuController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICongCuService _congCuService;
        private readonly IXuatNhapService _xuatNhapService;
        private readonly INhapXuatCongCuService _nhapXuatCongCuService;
        private readonly IXuatNhapModelFactory _xuatNhapModelFactory;
        private readonly IKiemKeCongCuModelFactory _kiemKeCongCuModelFactory;
        private readonly IGiamDieuChuyenModelFactory _itemModelFactory;
        private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
        private readonly IDonViService _donViService;
        private readonly IDonViModelFactory _donViModelFactory;
        private readonly IGiamHongmatService _giamHongmatService;
        private readonly INhomCongCuService _nhomCongCuService;
        private readonly ICauHinhModelFactory _cauHinhModelFactory;
        #endregion

        #region Ctor
        public GiamCongCuController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ICongCuService congCuService,
            IXuatNhapService xuatNhapService,
            INhapXuatCongCuService nhapXuatCongCuService,
            IXuatNhapModelFactory xuatNhapModelFactory,
            IKiemKeCongCuModelFactory kiemKeCongCuModelFactory,
            IGiamDieuChuyenModelFactory itemModelFactory,
            IDonViBoPhanModelFactory donViBoPhanModelFactory,
            IDonViService donViService,
            IDonViModelFactory donViModelFactory,
            IGiamHongmatService giamHongmatService,
            INhomCongCuService nhomCongCuService,
            ICauHinhModelFactory cauHinhModelFactory
            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._congCuService = congCuService;
            this._xuatNhapService = xuatNhapService;
            this._nhapXuatCongCuService = nhapXuatCongCuService;
            this._xuatNhapModelFactory = xuatNhapModelFactory;
            this._kiemKeCongCuModelFactory = kiemKeCongCuModelFactory;
            this._itemModelFactory = itemModelFactory;
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
            this._donViService = donViService;
            this._donViModelFactory = donViModelFactory;
            this._giamHongmatService = giamHongmatService;
            this._nhomCongCuService = nhomCongCuService;
            this._cauHinhModelFactory = cauHinhModelFactory;
        }
        #endregion

        #region Methods
        public virtual IActionResult List(bool IsLoadSession = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = _itemModelFactory.PrepareCongCuSearchModel(new GiamDieuChuyenSearchModel());
            searchmodel.DDLLoaiXuatNhap = (new enumLyDoGiam()).ToSelectList(valuesToExclude: new int[] { 5, 6 }).ToList();
            searchmodel.DDLLoaiXuatNhap.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = "Tất cả", Value = "0" });
            var _searchModel = HttpContext.Session.GetObject<GiamDieuChuyenSearchModel>(enumTENCAUHINH.KeySearch+typeof(GiamDieuChuyenSearchModel).Name);
            if (_searchModel != null && IsLoadSession)
            {
                _cauHinhModelFactory.PrePareModelBySession(searchmodel, _searchModel);
                UpdateSessionSearchModel<GiamDieuChuyenSearchModel>(false);
            };
            return View(searchmodel);
        }

        [HttpPost]
        public virtual IActionResult List(GiamDieuChuyenSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();
            var model = _itemModelFactory.PrepareGiamDieuChuyenListModel(searchModel);
            HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch+searchModel.GetType().Name, searchModel);
            return Json(model);
        }

        public virtual IActionResult ChonTaiSan(bool isDieuChuyenNgoai)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new GiamDieuChuyenSearchModel();
            searchmodel.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true).ToList();
            return View(searchmodel);
        }

        [HttpPost]
        public virtual IActionResult ChonTaiSan(GiamDieuChuyenSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();

            var model = _itemModelFactory.PrepareForChonCongCu(searchModel);

            return Json(model);
        }

        public virtual IActionResult Create(String StringId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();
            var model = _itemModelFactory.PrepareDieuChuyen(StringId);           
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Create(GiamDieuChuyenModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                foreach (var congcu in model.ListCongCuDieuChuyenModel)
                {
                    var map = _nhapXuatCongCuService.GetNhapXuatCongCuById(congcu.MapId);
                    // xuất
                    var sstXuat = _xuatNhapService.GetValueIdMax();
                    var madonvifrom = _donViService.GetDonViById((Decimal)map.XuatNhap.DON_VI_ID).MA;
                    var xuat = new XuatNhap
                    {
                        DON_VI_BO_PHAN_ID = map.XuatNhap.DON_VI_BO_PHAN_ID,
                        DON_VI_ID = map.XuatNhap.DON_VI_ID,
                        FROM_XUAT_NHAP_ID = map.NHAP_XUAT_ID,
                        GHI_CHU = model.GHI_CHU,
                        IS_XUAT = true,
                        MA_LIEN_QUAN = map.XuatNhap.MA_LIEN_QUAN,
                        QUYET_DINH_NGAY = model.NGAY_QUYET_DINH,
                        QUYET_DINH_SO = model.SO_QUYET_DINH,
                        MA = sstXuat != null ? madonvifrom + "-" + sstXuat : madonvifrom + "-1",
                        NGAY_XUAT_NHAP = model.NGAY_DIEU_CHUYEN
                    };
                    xuat.LOAI_XUAT_NHAP_ID =model.LoaiXuatNhapId;
                    _xuatNhapService.InsertXuatNhap(xuat);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", xuat.ToModel<XuatNhapModel>(), "XuatNhap");
                    // mapping xuất
                    var mapXuat = new NhapXuatCongCu
                    {
                        CHUNG_TU_NGAY = model.NGAY_CHUNG_TU,
                        CHUNG_TU_SO = model.SO_CHUNG_TU,
                        CONG_CU_ID = map.CONG_CU_ID,
                        DON_GIA = map.DON_GIA,
                        NHAP_XUAT_ID = xuat.ID,
                        SO_LUONG = congcu.SoLuongDieuChuyen,
                        TRANG_THAI_ID = map.TRANG_THAI_ID,
                        NHAP_KHO_ID = map.ID,
                        THANH_TIEN = map.DON_GIA * congcu.SoLuongDieuChuyen
                    };
                    _nhapXuatCongCuService.InsertNhapXuatCongCu(mapXuat);
                    if (model.LoaiXuatNhapId==(int)enumLoaiXuatNhap.DIEU_CHUYEN)
                    {
                        //thêm mới nhóm và công cụ cho đơn vị ngoài
                        var nhomcc = new NhomCongCu();
                        var cc = new CongCu();
                        var ncc = _nhomCongCuService.GetNhomCongCuById((decimal)map.CongCu.NHOM_CONG_CU_ID);
                        if (ncc != null)
                        {                    
                            // nhóm công cụ
                            nhomcc.DON_VI_TINH_ID = ncc.DON_VI_TINH_ID;
                            nhomcc.TEN = ncc.TEN;
                            nhomcc.THOI_HAN_PHAN_BO = ncc.THOI_HAN_PHAN_BO;
                            _nhomCongCuService.InsertNhomCongCu(nhomcc);
                            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", nhomcc.ToModel<NhomCongCuModel>(), "NhomCongCu");
                            //công cụ
                            var sttMaCongCu = _congCuService.GetValueIdMax();
                            cc.TEN = map.CongCu.TEN;
                            cc.NHOM_CONG_CU_ID = nhomcc.ID;                            
                            if (sttMaCongCu != null)
                            {
                                cc.MA = _workContext.CurrentDonVi.MA_DON_VI + "-" + cc.NHOM_CONG_CU_ID + "-" + sttMaCongCu;
                            }
                            else
                            {
                                cc.MA = _workContext.CurrentDonVi.MA_DON_VI + "-" + cc.NHOM_CONG_CU_ID + "-1";
                            }
                            _congCuService.InsertCongCu(cc);
                            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", cc.ToModel<CongCuModel>(), "CongCu");
                        }
                        // nhập
                        var sstNhap = _xuatNhapService.GetValueIdMax();
                        var maDonViTo = _donViService.GetDonViById(model.DON_VI_ID).MA;
                        var nhap = new XuatNhap
                        {
                            DON_VI_ID = model.DON_VI_ID,
                            FROM_XUAT_NHAP_ID = xuat.ID,
                            GHI_CHU = model.GHI_CHU,
                            IS_XUAT = false,
                            LOAI_XUAT_NHAP_ID = (Decimal)enumLoaiXuatNhap.DIEU_CHUYEN,
                            MA_LIEN_QUAN = map.XuatNhap.MA_LIEN_QUAN,
                            QUYET_DINH_NGAY = model.NGAY_QUYET_DINH,
                            QUYET_DINH_SO = model.SO_QUYET_DINH,
                            MA = sstNhap != null ? maDonViTo + "-" + sstNhap : maDonViTo + "-1",
                            NGAY_XUAT_NHAP = model.NGAY_DIEU_CHUYEN
                        };
                        _xuatNhapService.InsertXuatNhap(nhap);
                        _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", nhap.ToModel<XuatNhapModel>(), "XuatNhap");
                        // mapping nhập
                        var mapNhap = new NhapXuatCongCu
                        {
                            CHUNG_TU_NGAY = model.NGAY_CHUNG_TU,
                            CHUNG_TU_SO = model.SO_CHUNG_TU,
                            CONG_CU_ID = cc!=null? cc.ID:map.CONG_CU_ID,
                            DON_GIA = map.DON_GIA,
                            NHAP_XUAT_ID = nhap.ID,
                            SO_LUONG = congcu.SoLuongDieuChuyen,
                            TRANG_THAI_ID = map.TRANG_THAI_ID,
                            THANH_TIEN = map.DON_GIA * congcu.SoLuongDieuChuyen,
                        };
                        _nhapXuatCongCuService.InsertNhapXuatCongCu(mapNhap);
                    }
                    //giấy báo hỏng mất
                    if(model.LoaiXuatNhapId == (int)enumLyDoGiam.GIAM_HONG_MAT)
                    {
                        var itemHM = new GiamHongmat();
                        itemHM.DON_VI_ID = _workContext.CurrentDonVi.ID;
                        itemHM.CONG_CU_ID = map.CONG_CU_ID;
                        itemHM.NHAP_XUAT_ID = xuat.ID;
                        itemHM.DON_VI_BO_PHAN_ID = map.XuatNhap.DON_VI_BO_PHAN_ID;
                        itemHM.LY_DO = model.LyDo;
                        itemHM.NGAY_LAP = (DateTime)model.NGAY_DIEU_CHUYEN;
                        itemHM.SO_LUONG = congcu.SoLuongDieuChuyen;
                        itemHM.NGUOI_TAO = _workContext.CurrentCustomer.ID;
                        itemHM.NGAY_TAO = DateTime.Now;
                        _giamHongmatService.InsertGiamHongmat(itemHM);
                        _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", itemHM.ToModel<GiamHongmatModel>(), "GiamHongmat");
                    }
                }

                return JsonSuccessMessage("Cập nhật dữ liệu thành công !");
            }
            var listERR = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Cập nhật dữ liệu không thành công !",listERR);
        }

        public virtual IActionResult Edit(String MapId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();
            var item = _nhapXuatCongCuService.GetNhapXuatCongCuById(Decimal.Parse(MapId));
            if (item == null)
                return RedirectToAction("List");
            var model = _itemModelFactory.PrepareDieuChuyen(MapId, whenEdit: true);
            model.SO_QUYET_DINH = item.XuatNhap.QUYET_DINH_SO;
            model.NGAY_QUYET_DINH = item.XuatNhap.QUYET_DINH_NGAY;
            model.NGAY_DIEU_CHUYEN = item.XuatNhap.NGAY_XUAT_NHAP;
            if(model.DON_VI_ID<=0) model.DON_VI_ID = (Decimal)item.XuatNhap.DON_VI_ID;
            model.SO_CHUNG_TU = item.CHUNG_TU_SO;
            model.NGAY_CHUNG_TU = item.CHUNG_TU_NGAY;
            model.GHI_CHU = item.XuatNhap.GHI_CHU;
            model.LoaiXuatNhapId = (int)item.XuatNhap.LOAI_XUAT_NHAP_ID;
            model.ID = item.ID;

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Edit(GiamDieuChuyenModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();
            var item = _nhapXuatCongCuService.GetNhapXuatCongCuById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                var xuat = _xuatNhapService.GetXuatNhapById((Decimal)item.NHAP_XUAT_ID);
                xuat.GHI_CHU = model.GHI_CHU;
                xuat.QUYET_DINH_NGAY = model.NGAY_QUYET_DINH;
                xuat.QUYET_DINH_SO = model.SO_QUYET_DINH;
                xuat.NGAY_XUAT_NHAP = model.NGAY_DIEU_CHUYEN;
                xuat.LOAI_XUAT_NHAP_ID = model.LoaiXuatNhapId;
                _xuatNhapService.UpdateXuatNhap(xuat);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", xuat.ToModel<XuatNhapModel>(), "XuatNhap");
                var mapXuat = _nhapXuatCongCuService.GetNhapXuatCongCu(CongCuId: item.CONG_CU_ID, NhapXuatId: xuat.ID);
                foreach (var congcu in model.ListCongCuDieuChuyenModel)
                {
                    mapXuat.CHUNG_TU_NGAY = model.NGAY_CHUNG_TU;
                    mapXuat.CHUNG_TU_SO = model.SO_CHUNG_TU;
                    mapXuat.SO_LUONG = congcu.SoLuongDieuChuyen;
                    mapXuat.THANH_TIEN = mapXuat.DON_GIA * congcu.SoLuongDieuChuyen;
                    _nhapXuatCongCuService.UpdateNhapXuatCongCu(mapXuat);
                };
                //điều chuyển 
                if (model.LoaiXuatNhapId == (int)enumLoaiXuatNhap.DIEU_CHUYEN)
                {
                    var nhap = _xuatNhapService.GetXuatNhapForDieuChuyen(FromXuatNhap:item.NHAP_XUAT_ID,isXuat:false,LoaiXuatNhap:model.LoaiXuatNhapId);
                    if (nhap != null)
                    {
                        //thêm mới nhóm và công cụ cho đơn vị ngoài
                        var nhomcc = new NhomCongCu();
                        var cc = new CongCu();
                        if (nhap.DON_VI_ID != model.DON_VI_ID)
                        {                            
                            var ncc = _nhomCongCuService.GetNhomCongCuById((decimal)item.CongCu.NHOM_CONG_CU_ID);
                            if (ncc != null)
                            {
                                // nhóm công cụ
                                nhomcc.DON_VI_TINH_ID = ncc.DON_VI_TINH_ID;
                                nhomcc.TEN = ncc.TEN;
                                nhomcc.THOI_HAN_PHAN_BO = ncc.THOI_HAN_PHAN_BO;
                                _nhomCongCuService.InsertNhomCongCu(nhomcc);
                                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", nhomcc.ToModel<NhomCongCuModel>(), "NhomCongCu");
                                //công cụ
                                var sttMaCongCu = _congCuService.GetValueIdMax();
                                cc.TEN = item.CongCu.TEN;
                                cc.NHOM_CONG_CU_ID = nhomcc.ID;
                                if (sttMaCongCu != null)
                                {
                                    cc.MA = _workContext.CurrentDonVi.MA_DON_VI + "-" + cc.NHOM_CONG_CU_ID + "-" + sttMaCongCu;
                                }
                                else
                                {
                                    cc.MA = _workContext.CurrentDonVi.MA_DON_VI + "-" + cc.NHOM_CONG_CU_ID + "-1";
                                }
                                _congCuService.InsertCongCu(cc);
                                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", cc.ToModel<CongCuModel>(), "CongCu");
                            }
                        }
                        nhap.GHI_CHU = model.GHI_CHU;
                        nhap.QUYET_DINH_NGAY = model.NGAY_QUYET_DINH;
                        nhap.QUYET_DINH_SO = model.SO_QUYET_DINH;
                        nhap.NGAY_XUAT_NHAP = model.NGAY_DIEU_CHUYEN;
                        nhap.DON_VI_ID = model.DON_VI_ID;
                        _xuatNhapService.UpdateXuatNhap(nhap);
                        _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", nhap.ToModel<XuatNhapModel>(), "XuatNhap");
                        //map
                        var nhapXuat = _nhapXuatCongCuService.GetNhapXuatCongCuByNhapXuatId(nhap.ID).FirstOrDefault();
                        foreach (var congcu in model.ListCongCuDieuChuyenModel)
                        {
                            if (nhomcc.ID > 0)
                            {
                                if (cc != null)
                                {
                                    nhapXuat.CONG_CU_ID = cc.ID;
                                }
                                else
                                {
                                    nhapXuat.CONG_CU_ID = (decimal)nhapXuat?.CONG_CU_ID;
                                }
                            }
                            nhapXuat.CHUNG_TU_NGAY = model.NGAY_CHUNG_TU;
                            nhapXuat.CHUNG_TU_SO = model.SO_CHUNG_TU;
                            nhapXuat.SO_LUONG = congcu.SoLuongDieuChuyen;
                            nhapXuat.THANH_TIEN = mapXuat.DON_GIA * congcu.SoLuongDieuChuyen;
                            _nhapXuatCongCuService.UpdateNhapXuatCongCu(nhapXuat);
                        };
                    }
                    else
                    {
                        //thêm mới nhóm và công cụ cho đơn vị ngoài
                        var nhomcc = new NhomCongCu();
                        var cc = new CongCu();
                        var ncc = _nhomCongCuService.GetNhomCongCuById((decimal)item.CongCu.NHOM_CONG_CU_ID);
                        if (ncc != null)
                        {
                            // nhóm công cụ
                            nhomcc.DON_VI_TINH_ID = ncc.DON_VI_TINH_ID;
                            nhomcc.TEN = ncc.TEN;
                            nhomcc.THOI_HAN_PHAN_BO = ncc.THOI_HAN_PHAN_BO;
                            _nhomCongCuService.InsertNhomCongCu(nhomcc);
                            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", nhomcc.ToModel<NhomCongCuModel>(), "NhomCongCu");
                            //công cụ
                            var sttMaCongCu = _congCuService.GetValueIdMax();
                            cc.TEN = item.CongCu.TEN;
                            cc.NHOM_CONG_CU_ID = nhomcc.ID;
                            if (sttMaCongCu != null)
                            {
                                cc.MA = _workContext.CurrentDonVi.MA_DON_VI + "-" + cc.NHOM_CONG_CU_ID + "-" + sttMaCongCu;
                            }
                            else
                            {
                                cc.MA = _workContext.CurrentDonVi.MA_DON_VI + "-" + cc.NHOM_CONG_CU_ID + "-1";
                            }
                            _congCuService.InsertCongCu(cc);
                            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", cc.ToModel<CongCuModel>(), "CongCu");
                        }
                        
                        var sstNhap = _xuatNhapService.GetValueIdMax();
                        var maDonViTo = _donViService.GetDonViById(model.DON_VI_ID).MA;
                        nhap = new XuatNhap();
                        nhap.DON_VI_ID = model.DON_VI_ID;
                        nhap.FROM_XUAT_NHAP_ID = xuat.ID;
                        nhap.GHI_CHU = model.GHI_CHU;
                        nhap.IS_XUAT = false;
                        nhap.LOAI_XUAT_NHAP_ID = (Decimal)enumLoaiXuatNhap.DIEU_CHUYEN;
                        nhap.MA_LIEN_QUAN = xuat.MA_LIEN_QUAN;
                        nhap.QUYET_DINH_NGAY = model.NGAY_QUYET_DINH;
                        nhap.QUYET_DINH_SO = model.SO_QUYET_DINH;
                        nhap.MA = sstNhap != null ? maDonViTo + "-" + sstNhap : maDonViTo + "-1";
                        nhap.NGAY_XUAT_NHAP = model.NGAY_DIEU_CHUYEN;
                        _xuatNhapService.InsertXuatNhap(nhap);
                        _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", nhap.ToModel<XuatNhapModel>(), "XuatNhap");
                        //map
                        foreach (var congcu in model.ListCongCuDieuChuyenModel)
                        {
                            var mapNhap = new NhapXuatCongCu
                            {
                                CHUNG_TU_NGAY = model.NGAY_CHUNG_TU,
                                CHUNG_TU_SO = model.SO_CHUNG_TU,
                                CONG_CU_ID = cc!=null?cc.ID:item.CONG_CU_ID,
                                DON_GIA = item.DON_GIA,
                                NHAP_XUAT_ID = nhap.ID,
                                SO_LUONG = congcu.SoLuongDieuChuyen,
                                THANH_TIEN = item.DON_GIA * congcu.SoLuongDieuChuyen,
                            };
                            _nhapXuatCongCuService.InsertNhapXuatCongCu(mapNhap);
                        }                      
                    };                                            
                }
                // giảm hỏng mất 
                //
                if (model.LoaiXuatNhapId == (int)enumLyDoGiam.GIAM_HONG_MAT)
                {
                    var itemHM = _giamHongmatService.GetGiamHongmatByNhapXuatIdAndCongCuId(xuat.ID, item.CONG_CU_ID);
                    foreach (var congcu in model.ListCongCuDieuChuyenModel)
                    {
                        if (itemHM != null)
                        {
                            itemHM.SO_LUONG = congcu.SoLuongDieuChuyen;
                            itemHM.LY_DO = model.LyDo;
                            _giamHongmatService.UpdateGiamHongmat(itemHM);
                            _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", itemHM.ToModel<GiamHongmatModel>(), "GiamHongmat");
                        }
                        else
                        {
                            itemHM.DON_VI_ID = _workContext.CurrentDonVi.ID;
                            itemHM.CONG_CU_ID = item.CONG_CU_ID;
                            itemHM.NHAP_XUAT_ID = xuat.ID;
                            itemHM.DON_VI_BO_PHAN_ID = xuat.ID;
                            itemHM.LY_DO = model.LyDo;
                            itemHM.NGAY_LAP = (DateTime)model.NGAY_DIEU_CHUYEN;
                            itemHM.SO_LUONG = congcu.SoLuongDieuChuyen;
                            itemHM.NGUOI_TAO = _workContext.CurrentCustomer.ID;
                            itemHM.NGAY_TAO = DateTime.Now;
                            _giamHongmatService.InsertGiamHongmat(itemHM);
                            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", itemHM.ToModel<GiamHongmatModel>(), "GiamHongmat");
                        }
                    }
                }
                return JsonSuccessMessage("Cập nhật dữ liệu thành công !");
            }
            var listERR = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Cập nhật dữ liệu không thành công !", listERR);
        }

        public virtual IActionResult Delete(Decimal ID)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLPhanBoCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var map = _nhapXuatCongCuService.GetNhapXuatCongCuById(ID);
            if (map == null)
                return JsonSuccessMessage("Xoá dữ liệu không thành công");
            try
            {
                var xuat = _xuatNhapService.GetXuatNhapById(map.NHAP_XUAT_ID);
                //nếu là giảm hỏng mất thì xóa phiếu
                if(xuat !=null && xuat.LOAI_XUAT_NHAP_ID == (int)enumLyDoGiam.GIAM_HONG_MAT)
                {
                    var ghm = _giamHongmatService.GetGiamHongmatByNhapXuatIdAndCongCuId(NhapXuatId: xuat.ID, CongCuId: map.CONG_CU_ID);
                    if (ghm != null)
                    {
                        _giamHongmatService.DeleteGiamHongmat(ghm);
                        _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu", ghm.ToModel<GiamHongmatModel>(), "GiamHongmat");
                    }
                }
                _nhapXuatCongCuService.DeleteNhapXuatCongCu(map);              
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu", map.ToModel<NhapXuatCongCuModel>(), "NhapXuatCongCu");
                // kiểm tra xem nếu trong phiếu xuất ko còn công cụ giảm nào thì xóa luôn phiếu xuất
                var countNXCC = _nhapXuatCongCuService.GetNhapXuatCongCuByNhapXuatId(xuat.ID).Select(c => c.ID).ToList();
                if (countNXCC.Count() == 0)
                {
                    _xuatNhapService.DeleteXuatNhap(xuat);
                    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu", xuat.ToModel<XuatNhapModel>(), "XuatNhap");
                }
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                return JsonSuccessMessage("Xoá dữ liệu không thành công");
            }
        }
        public virtual IActionResult _ChonDonViDieuChuyen()
        {
            var model = new DonViSearchModel();
            model.nguoiDungId = _workContext.CurrentCustomer.ID;
            model = _donViModelFactory.PrepareDonViSearchModel(model);
            return PartialView(model);
        }

        public virtual IActionResult GetJsonDonVi(Decimal DonViId)
        {
            var item = _donViService.GetDonViById(DonViId).TEN;
            return Json(item);
        }
        #endregion
    }
}
