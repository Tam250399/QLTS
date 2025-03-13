using GS.Core;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.CCDC;
using GS.Services.CCDC;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.CCDC;
using GS.Web.Factories.DanhMuc;
using GS.Web.Models.CCDC;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Controllers
{
    public class GiamDieuChuyenController : BaseWorksController
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
        #endregion

        #region Ctor
        public GiamDieuChuyenController(
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
            IDonViModelFactory donViModelFactory
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
        }
        #endregion

        #region Methods
        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new GiamDieuChuyenSearchModel();

            return View(searchmodel);
        }

        [HttpPost]
        public virtual IActionResult List(GiamDieuChuyenSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();
            searchModel.LoaiDieuChuyen = (Decimal)enumLoaiXuatNhap.DIEU_CHUYEN;
            var model = _itemModelFactory.PrepareGiamDieuChuyenListModel(searchModel);

            return Json(model);
        }

        public virtual IActionResult _ChonTaiSan(bool isDieuChuyenNgoai)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();

            //prepare model
            var searchmodel = new GiamDieuChuyenSearchModel();
            searchmodel.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID,isAddFirst:true).ToList();
            searchmodel.IsDieuChuyenNgoai = isDieuChuyenNgoai;

            return PartialView(searchmodel);
        }

        [HttpPost]
        public virtual IActionResult _ChonTaiSan(GiamDieuChuyenSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();

            var model = _itemModelFactory.PrepareForChonCongCu(searchModel);

            return Json(model);
        }

        public virtual IActionResult Create(String StringId, bool isDieuChuyenNgoai)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();

            var model = _itemModelFactory.PrepareDieuChuyen(StringId);
            model.IsDieuChuyenNgoai = isDieuChuyenNgoai;

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
                    xuat.LOAI_XUAT_NHAP_ID = model.IsDieuChuyenNgoai ? (Decimal)enumLoaiXuatNhap.DIEU_CHUYEN_NGOAI : (Decimal)enumLoaiXuatNhap.DIEU_CHUYEN;
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
                        THANH_TIEN = map.DON_GIA * congcu.SoLuongDieuChuyen,
                    };
                    _nhapXuatCongCuService.InsertNhapXuatCongCu(mapXuat);
                    if (!model.IsDieuChuyenNgoai)
                    {
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
                            CONG_CU_ID = map.CONG_CU_ID,
                            DON_GIA = map.DON_GIA,
                            NHAP_XUAT_ID = nhap.ID,
                            SO_LUONG = congcu.SoLuongDieuChuyen,
                            THANH_TIEN = map.DON_GIA * congcu.SoLuongDieuChuyen,
                        };
                        _nhapXuatCongCuService.InsertNhapXuatCongCu(mapNhap);
                    }
                }

                return JsonSuccessMessage("Cập nhật dữ liệu thành công !");
            }

            model = _itemModelFactory.PrepareDieuChuyen(model.stringMapId);
            return View(model);
        }

        public virtual IActionResult Edit(String MapId, bool isDieuChuyenNgoai)
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
            model.DON_VI_ID = (Decimal)item.XuatNhap.DON_VI_ID;
            model.SO_CHUNG_TU = item.CHUNG_TU_SO;
            model.NGAY_CHUNG_TU = item.CHUNG_TU_NGAY;
            model.GHI_CHU = item.XuatNhap.GHI_CHU;
            model.IsDieuChuyenNgoai = isDieuChuyenNgoai;

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
                if (!model.IsDieuChuyenNgoai)
                {
                    var nhap = _xuatNhapService.GetXuatNhapById((Decimal)item.NHAP_XUAT_ID);
                    nhap.GHI_CHU = model.GHI_CHU;
                    nhap.QUYET_DINH_NGAY = model.NGAY_QUYET_DINH;
                    nhap.QUYET_DINH_SO = model.SO_QUYET_DINH;
                    nhap.NGAY_XUAT_NHAP = model.NGAY_DIEU_CHUYEN;
                    _xuatNhapService.UpdateXuatNhap(nhap);
                    _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", nhap.ToModel<XuatNhapModel>(), "XuatNhap");
                    var nhapXuat = _nhapXuatCongCuService.GetNhapXuatCongCu(CongCuId: item.CONG_CU_ID, NhapXuatId: nhap.ID);
                    foreach (var congcu in model.ListCongCuDieuChuyenModel)
                    {
                        nhapXuat.CHUNG_TU_NGAY = model.NGAY_CHUNG_TU;
                        nhapXuat.CHUNG_TU_SO = model.SO_CHUNG_TU;
                        nhapXuat.SO_LUONG = congcu.SoLuongDieuChuyen;
                        nhapXuat.THANH_TIEN = mapXuat.DON_GIA * congcu.SoLuongDieuChuyen;
                        _nhapXuatCongCuService.UpdateNhapXuatCongCu(nhapXuat);
                    };
                }
                return JsonSuccessMessage("Thành công !");
            }

            model = _itemModelFactory.PrepareDieuChuyen(model.stringMapId);
            return View(model);
        }

        public virtual IActionResult Delete(String MapId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLPhanBoCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var map = _nhapXuatCongCuService.GetNhapXuatCongCuById(Decimal.Parse(MapId));
            if (map == null)
                return RedirectToAction("List");
            try
            {
                var xuatMap = _nhapXuatCongCuService.GetNhapXuatCongCu(CongCuId: map.CONG_CU_ID, NhapXuatId: (Decimal)map.XuatNhap.FROM_XUAT_NHAP_ID);
                _nhapXuatCongCuService.DeleteNhapXuatCongCu(xuatMap);
                _nhapXuatCongCuService.DeleteNhapXuatCongCu(map);
                var xuat = _xuatNhapService.GetXuatNhapById((Decimal)map.XuatNhap.FROM_XUAT_NHAP_ID);
                _xuatNhapService.DeleteXuatNhap(xuat);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu", xuat.ToModel<XuatNhapModel>(), "XuatNhap");
                var nhap = _xuatNhapService.GetXuatNhapById(map.NHAP_XUAT_ID);
                _xuatNhapService.DeleteXuatNhap(nhap);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu", nhap.ToModel<XuatNhapModel>(), "XuatNhap"); var mapNew = _nhapXuatCongCuService.GetNhapXuatCongCus(NhapXuatId: map.NHAP_XUAT_ID);

                SuccessNotification("Xoá dữ liệu thành công");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { MapId = MapId });
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
