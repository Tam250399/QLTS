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
using System.Linq;
using GS.Services;
using GS.Services.DanhMuc;
using System.Collections.Generic;
using GS.Web.Models.DanhMuc;
using GS.Core.Domain.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using DevExpress.CodeParser;
using Microsoft.AspNetCore.Http;
using GS.Web.Factories.HeThong;
using DevExpress.XtraRichEdit.Layout.Engine;
using GS.Core.Infrastructure;
using GS.Core.Data;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using GS.Web.Framework.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class CongCuController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICongCuModelFactory _itemModelFactory;
        private readonly ICongCuService _itemService;
        private readonly INhomCongCuModelFactory _nhomCongCuModelFactory;
        private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
        private readonly INhomCongCuService _nhomCongCuService;
        private readonly IXuatNhapService _xuatNhapService;
        private readonly INhapXuatCongCuService _nhapXuatCongCuService;
        private readonly IFileCongViecService _fileCongViecService;
        private readonly IFileCongViecModelFactory _fileCongViecModelFactory;
        private readonly IDonViService _donViService;
        private readonly IGSFileProvider _fileProvider;
        private readonly IKiemKeService _kiemKeService;
        private readonly ICauHinhModelFactory _cauHinhModelFactory;
        #endregion

        #region Ctor
        public CongCuController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ICongCuModelFactory itemModelFactory,
            ICongCuService itemService,
            INhomCongCuModelFactory nhomCongCuModelFactory,
            IDonViBoPhanModelFactory donViBoPhanModelFactory,
            INhomCongCuService nhomCongCuService,
            IXuatNhapService xuatNhapService,
            INhapXuatCongCuService nhapXuatCongCuService,
            IDonViService donViService,
            IFileCongViecModelFactory fileCongViecModelFactory,
            IFileCongViecService fileCongViecService,
            IGSFileProvider fileProvider,
            IKiemKeService kiemKeService,
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
            this._nhomCongCuModelFactory = nhomCongCuModelFactory;
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
            this._nhomCongCuService = nhomCongCuService;
            this._xuatNhapService = xuatNhapService;
            this._nhapXuatCongCuService = nhapXuatCongCuService;
            this._donViService = donViService;
            this._fileCongViecService = fileCongViecService;
            this._fileCongViecModelFactory = fileCongViecModelFactory;
            this._fileProvider = fileProvider;
            this._kiemKeService = kiemKeService;
            this._cauHinhModelFactory = cauHinhModelFactory;
        }
        #endregion

        #region Methods

        public virtual IActionResult List(bool IsLoadSession = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLNhapLoCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            //HttpContext.Session.GetObject<NguoiDung>("CurrentCustomer");
            var searchmodel = new CongCuSearchModel();
            var model = _itemModelFactory.PrepareCongCuSearchModel(searchmodel);
            if (IsLoadSession)
            {
                var _searchModel = HttpContext.Session.GetObject<CongCuSearchModel>(enumTENCAUHINH.KeySearch + typeof(CongCuSearchModel).Name);
                if (_searchModel != null)
                {
                    _cauHinhModelFactory.PrePareModelBySession(model, _searchModel);
                    UpdateSessionSearchModel<CongCuSearchModel>(false);
                }
            }
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(CongCuSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLNhapLoCongCuDungCu))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareCongCuListModel(searchModel);
            HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch + searchModel.GetType().Name, searchModel);
            return Json(model);
        }

        [HttpPost]
        public virtual IActionResult ListXuatNhap(CongCuSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLNhapLoCongCuDungCu))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareXuatNhapListModel(searchModel);
            HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch + searchModel.GetType().Name, searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLNhapLoCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareCongCuModel(new CongCuModel(), null);
            var donvi = _workContext.CurrentDonVi;
            model.CurrentDonViMa = donvi.MA_DON_VI;
            model.CurrentDonViTen = donvi.TEN_DON_VI;
            model.DON_VI_BO_PHAN_ID = 0;
            //thêm trạng thái phân bổ
            model.TRANG_THAI_PHAN_BO = 0;
            model.TrangThaiPhanBoAvailable = enumTrangThaiPhanBo.ChuaPhanBo.ToSelectList();
            //
            model.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true).ToList();
            model.DDLLyDoTang = ((enumMucDichXuatNhap)model.MucDichXuatNhapEnum).ToSelectList();
            model.DDLTrangThai = model.DDLTrangThai = new enumTrangThaiCongCu().ToSelectList(valuesToExclude: new int[] { (int)enumTrangThaiCongCu.CHON, (int)enumTrangThaiCongCu.DANG_SU_DUNG, (int)enumTrangThaiCongCu.HONG_CHO_XU_LY });
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Create(CongCuModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLNhapLoCongCuDungCu))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                //check trong list công cụ có bị trùng 
                if (model.ListLoCongCu.Count() > 1)
                {
                    var checkTrung = true;
                    foreach (var cck in model.ListLoCongCu)
                    {
                        var itemtrung = model.ListLoCongCu.Where(c => c.NHOM_CONG_CU_ID == cck.NHOM_CONG_CU_ID && (c.CongCuId != null ? c.CongCuId : 0) == (cck.CongCuId != null ? cck.CongCuId : 0) && cck.TEN == c.TEN && c.DON_GIA == cck.DON_GIA && c.TRANG_THAI_ID == cck.TRANG_THAI_ID);
                        if (itemtrung.Count() > 1)
                        {
                            checkTrung = false;
                        }
                    }
                    if (!checkTrung)
                    {
                        return JsonErrorMessage("Công cụ bị trùng", model.ID);
                    }
                }
                // insert kho nhập
                var sttnhapkho = _xuatNhapService.GetValueIdMax();
                var nhapkho = new XuatNhap()
                {
                    TEN = model.TenXuatNhap,
                    DON_VI_ID = _workContext.CurrentDonVi.ID,
                    IS_XUAT = false,
                    NGAY_XUAT_NHAP = model.NgayXuatNhap,
                    MA = sttnhapkho != null ? model.CurrentDonViMa + "-" + sttnhapkho : model.CurrentDonViMa + "-1",
                    MA_LIEN_QUAN = sttnhapkho != null ? model.CurrentDonViMa + "-" + sttnhapkho : model.CurrentDonViMa + "-1",
                    GHI_CHU = model.GhiChuXuatNhap,
                    MUC_DICH_XUAT_NHAP_ID = model.MucDichXuatNhapId,
                    TRANG_THAI_ID = (int)enumTrangThaiCongCu.CHUA_SU_DUNG,
                    LOAI_XUAT_NHAP_ID = (Decimal)enumLoaiXuatNhap.NHAP_KHO
                };
                _xuatNhapService.InsertXuatNhap(nhapkho);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", nhapkho.ToModel<XuatNhapModel>(), "XuatNhap");
                // insert phân bổ công cụ
                var xuatkho = new XuatNhap();
                var nhapdvbp = new XuatNhap();
                if (model.DON_VI_BO_PHAN_ID > 0)
                {
                    // xuất kho
                    var sttXuatKho = _xuatNhapService.GetValueIdMax();
                    xuatkho.FROM_XUAT_NHAP_ID = nhapkho.ID;
                    xuatkho.TEN = model.TenXuatNhap;
                    xuatkho.DON_VI_ID = _workContext.CurrentDonVi.ID;
                    xuatkho.IS_XUAT = true;
                    xuatkho.NGAY_XUAT_NHAP = model.NgayXuatNhap;
                    xuatkho.MA = sttXuatKho != null ? model.CurrentDonViMa + "-" + sttXuatKho : model.CurrentDonViMa + "-1";
                    xuatkho.MA_LIEN_QUAN = nhapkho.MA_LIEN_QUAN;
                    xuatkho.GHI_CHU = model.GhiChuXuatNhap;
                    xuatkho.MUC_DICH_XUAT_NHAP_ID = model.MucDichXuatNhapId;
                    xuatkho.LOAI_XUAT_NHAP_ID = (Decimal)enumLoaiXuatNhap.PHAN_BO;
                    xuatkho.TRANG_THAI_ID = (int)enumTrangThaiCongCu.CHUA_SU_DUNG;
                    xuatkho.IS_PHAN_BO_FIRST = true;
                    _xuatNhapService.InsertXuatNhap(xuatkho);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", xuatkho.ToModel<XuatNhapModel>(), "XuatNhap");

                    // nhập đơn vị bộ phận
                    var sstNhapDVBP = _xuatNhapService.GetValueIdMax();
                    nhapdvbp.FROM_XUAT_NHAP_ID = xuatkho.ID;
                    nhapdvbp.TEN = model.TenXuatNhap;
                    nhapdvbp.DON_VI_BO_PHAN_ID = model.DON_VI_BO_PHAN_ID;
                    nhapdvbp.DON_VI_ID = _workContext.CurrentDonVi.ID;
                    nhapdvbp.IS_XUAT = false;
                    nhapdvbp.NGAY_XUAT_NHAP = model.NgayXuatNhap;
                    nhapdvbp.MA = sstNhapDVBP != null ? model.CurrentDonViMa + "-" + sstNhapDVBP : model.CurrentDonViMa + "-1";
                    nhapdvbp.MA_LIEN_QUAN = nhapkho.MA_LIEN_QUAN;
                    nhapdvbp.GHI_CHU = model.GhiChuXuatNhap;
                    nhapdvbp.MUC_DICH_XUAT_NHAP_ID = model.MucDichXuatNhapId;
                    nhapdvbp.LOAI_XUAT_NHAP_ID = (Decimal)enumLoaiXuatNhap.PHAN_BO;
                    nhapdvbp.TRANG_THAI_ID = model.TRANG_THAI_ID;
                    _xuatNhapService.InsertXuatNhap(nhapdvbp);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", nhapdvbp.ToModel<XuatNhapModel>(), "XuatNhap");
                }
                // insert lô công cụ
                foreach (var LCC in model.ListLoCongCu)
                {

                    if (LCC.CongCuId != 0 && LCC.CongCuId != null)
                    {
                        // insert mapping
                        var mapping = new NhapXuatCongCu()
                        {
                            CONG_CU_ID = LCC.CongCuId.Value,
                            NHAP_XUAT_ID = nhapkho.ID,
                            SO_LUONG = LCC.SO_LUONG,
                            CHUNG_TU_SO = model.ChungTuSoXuatNhap,
                            CHUNG_TU_NGAY = model.ChungTuNgayXuatNhap,
                            DON_GIA = LCC.DON_GIA,
                            THANH_TIEN = LCC.SO_LUONG * LCC.DON_GIA,
                            TRANG_THAI_ID = (int)enumTrangThaiCongCu.CHUA_SU_DUNG
                        };
                        _nhapXuatCongCuService.InsertNhapXuatCongCu(mapping);
                        // insert xuất/nhập đơn vị bộ phận
                        if (model.DON_VI_BO_PHAN_ID > 0)
                        {
                            // mapping
                            var mappingXuatKho = new NhapXuatCongCu()
                            {
                                CONG_CU_ID = LCC.CongCuId.Value,
                                NHAP_XUAT_ID = xuatkho.ID,
                                SO_LUONG = LCC.SO_LUONG,
                                DON_GIA = LCC.DON_GIA,
                                TRANG_THAI_ID = (int)enumTrangThaiCongCu.CHUA_SU_DUNG,
                                NHAP_KHO_ID = mapping.ID
                            };
                            _nhapXuatCongCuService.InsertNhapXuatCongCu(mappingXuatKho);
                            var mappingNhapDonVi = new NhapXuatCongCu()
                            {
                                CONG_CU_ID = LCC.CongCuId.Value,
                                NHAP_XUAT_ID = nhapdvbp.ID,
                                SO_LUONG = LCC.SO_LUONG,
                                DON_GIA = LCC.DON_GIA,
                                TRANG_THAI_ID = model.TRANG_THAI_ID
                            };
                            _nhapXuatCongCuService.InsertNhapXuatCongCu(mappingNhapDonVi);
                        }
                    }
                    else
                    {
                        var congCu = new CongCu();
                        var sttMaCongCu = _itemService.GetValueIdMax();
                        if (sttMaCongCu != null)
                        {
                            congCu.MA = _workContext.CurrentDonVi.MA_DON_VI + "-" + LCC.NHOM_CONG_CU_ID + "-" + sttMaCongCu;
                        }
                        else
                        {
                            congCu.MA = _workContext.CurrentDonVi.MA_DON_VI + "-" + LCC.NHOM_CONG_CU_ID + "-1";
                        }
                        congCu.TEN = LCC.TEN;
                        congCu.DON_VI_ID = _workContext.CurrentDonVi.ID;
                        congCu.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                        congCu.NGAY_TAO = DateTime.Now;
                        congCu.NHOM_CONG_CU_ID = LCC.NHOM_CONG_CU_ID;
                        _itemService.InsertCongCu(congCu);
                        var mapping = new NhapXuatCongCu()
                        {
                            CONG_CU_ID = congCu.ID,
                            NHAP_XUAT_ID = nhapkho.ID,
                            SO_LUONG = LCC.SO_LUONG,
                            CHUNG_TU_SO = model.ChungTuSoXuatNhap,
                            CHUNG_TU_NGAY = model.ChungTuNgayXuatNhap,
                            DON_GIA = LCC.DON_GIA,
                            THANH_TIEN = LCC.SO_LUONG * LCC.DON_GIA,
                            TRANG_THAI_ID = (int)enumTrangThaiCongCu.CHUA_SU_DUNG
                        };
                        _nhapXuatCongCuService.InsertNhapXuatCongCu(mapping);
                        // insert xuất/nhập đơn vị bộ phận
                        if (model.DON_VI_BO_PHAN_ID > 0)
                        {
                            // mapping
                            var mappingXuatKho = new NhapXuatCongCu()
                            {
                                CONG_CU_ID = congCu.ID,
                                NHAP_XUAT_ID = xuatkho.ID,
                                SO_LUONG = LCC.SO_LUONG,
                                DON_GIA = LCC.DON_GIA,
                                TRANG_THAI_ID = (int)enumTrangThaiCongCu.CHUA_SU_DUNG,
                                NHAP_KHO_ID = mapping.ID
                            };
                            _nhapXuatCongCuService.InsertNhapXuatCongCu(mappingXuatKho);
                            var mappingNhapDonVi = new NhapXuatCongCu()
                            {
                                CONG_CU_ID = congCu.ID,
                                NHAP_XUAT_ID = nhapdvbp.ID,
                                SO_LUONG = LCC.SO_LUONG,
                                DON_GIA = LCC.DON_GIA,
                                TRANG_THAI_ID = model.TRANG_THAI_ID
                            };
                            _nhapXuatCongCuService.InsertNhapXuatCongCu(mappingNhapDonVi);
                        }
                    }
                }

                return JsonSuccessMessage("Tạo mới dữ liệu thành công !");
            }

            //prepare model
            //model = _itemModelFactory.PrepareCongCuModel(model, null);
            var listERR = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Cập nhật không thành công", listERR);
        }

        public virtual IActionResult Edit(Guid GUID)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLNhapLoCongCuDungCu))
                return AccessDeniedView();

            var item = _xuatNhapService.GetXuatNhapByGuid(GUID);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = new CongCuModel();
            var donvi = _workContext.CurrentDonVi;
            model.XuatNhapId = item.ID;
            model.CurrentDonViMa = donvi.MA_DON_VI;
            model.CurrentDonViTen = donvi.TEN_DON_VI;
            model.TenXuatNhap = item.TEN;
            model.DON_VI_BO_PHAN_ID = item.DON_VI_BO_PHAN_ID;
                    
            //
            model.NgayXuatNhap = item.NGAY_XUAT_NHAP;
            model.GhiChuXuatNhap = item.GHI_CHU;
            model.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true).ToList();
            var listMap = _nhapXuatCongCuService.GetNhapXuatCongCus(NhapXuatId: item.ID);
            foreach (var map in listMap)
            {
                var LoCongCu = new LoCongCuModel();
                _itemModelFactory.PrepareLoCongCu(map, LoCongCu);
                model.ListLoCongCu.Add(LoCongCu);
            }
            model.MucDichXuatNhapId = item.MUC_DICH_XUAT_NHAP_ID;
            model.ChungTuSoXuatNhap = listMap.FirstOrDefault().CHUNG_TU_SO;
            model.ChungTuNgayXuatNhap = listMap.FirstOrDefault().CHUNG_TU_NGAY;
            model.DDLLyDoTang = ((enumMucDichXuatNhap)model.MucDichXuatNhapEnum).ToSelectList();
            model.XuatNhapId = item.ID;
            var phanbo = _xuatNhapService.GetXuatNhaps(isXuat: false, maLienQuan: item.MA).Where(c => c.DON_VI_BO_PHAN_ID != null).FirstOrDefault();
            if (phanbo != null)
            {
                model.DON_VI_BO_PHAN_ID = phanbo.DON_VI_BO_PHAN_ID;
                var ccdcPhanBo = _nhapXuatCongCuService.GetNhapXuatCongCuByNhapXuatId(phanbo.ID).FirstOrDefault();
                model.TRANG_THAI_ID = ccdcPhanBo.TRANG_THAI_ID;
            }
            //thêm trạng thái phân bổ
            if (model.DON_VI_BO_PHAN_ID.GetValueOrDefault() <= 0 )
            {
                model.TRANG_THAI_PHAN_BO = 0;
                model.TrangThaiPhanBoAvailable = enumTrangThaiPhanBo.ChuaPhanBo.ToSelectList();
            }
            else
            {
                model.TRANG_THAI_PHAN_BO = 1;
                model.TrangThaiPhanBoAvailable = enumTrangThaiPhanBo.DaPhanBo.ToSelectList();
            }
            if (model.DON_VI_BO_PHAN_ID.GetValueOrDefault() > 0)
            {
                model.DDLTrangThai = new enumTrangThaiCongCu().ToSelectList(valuesToExclude: new int[] { (int)enumTrangThaiCongCu.CHUA_SU_DUNG });
            }
            else
            {
                model.DDLTrangThai = new enumTrangThaiCongCu().ToSelectList(valuesToExclude: new int[] { (int)enumTrangThaiCongCu.CHON, (int)enumTrangThaiCongCu.DANG_SU_DUNG, (int)enumTrangThaiCongCu.HONG_CHO_XU_LY });
            }
            var _searchModel = HttpContext.Session.GetObject<CongCuSearchModel>(enumTENCAUHINH.KeySearch + typeof(CongCuSearchModel).Name);
            if (_searchModel != null && _searchModel.IsLoadSession)
            {
                model.PageSize = _searchModel.Page;
            }
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Edit(CongCuModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLNhapLoCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var nhapkho = _xuatNhapService.GetXuatNhapById(model.XuatNhapId);
            if (nhapkho == null)
                return JsonErrorMessage("Cập nhật không thành công", model.ID);
            if (ModelState.IsValid)
            {
                //check trong list công cụ có bị trùng 
                if (model.ListLoCongCu.Count() > 1)
                {
                    var checkTrung = true;
                    foreach (var cck in model.ListLoCongCu)
                    {
                        var itemtrung = model.ListLoCongCu.Where(c => c.NHOM_CONG_CU_ID == cck.NHOM_CONG_CU_ID && (c.CongCuId != null ? c.CongCuId : 0) == (cck.CongCuId != null ? cck.CongCuId : 0) && cck.TEN == c.TEN && c.DON_GIA == cck.DON_GIA && c.TRANG_THAI_ID == cck.TRANG_THAI_ID);
                        if (itemtrung.Count() > 1)
                        {
                            checkTrung = false;
                        }
                    }
                    if (!checkTrung)
                    {
                        return JsonErrorMessage("Công cụ bị trùng", model.ID);
                    }
                }
                // update nhập kho
                nhapkho.TEN = model.TenXuatNhap;
                nhapkho.NGAY_XUAT_NHAP = model.NgayXuatNhap;
                nhapkho.MUC_DICH_XUAT_NHAP_ID = model.MucDichXuatNhapId;
                nhapkho.GHI_CHU = model.GhiChuXuatNhap;
                //nhapkho.TRANG_THAI_ID = model.TRANG_THAI_ID;
                _xuatNhapService.UpdateXuatNhap(nhapkho);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", nhapkho.ToModel<XuatNhapModel>(), "XuatNhap");

                // phân bổ
                var xuatkho = new XuatNhap();
                var nhapdvbp = new XuatNhap();
                if (model.DON_VI_BO_PHAN_ID > 0)
                {
                    xuatkho = _xuatNhapService.GetXuatNhaps(isXuat: true, fromId: nhapkho.ID, LoaiXuatNhap: (int)enumLoaiXuatNhap.PHAN_BO, IsPhanBoFirst: true).FirstOrDefault();
                    if (xuatkho != null)
                    {
                        xuatkho.TEN = model.TenXuatNhap;
                        xuatkho.NGAY_XUAT_NHAP = model.NgayXuatNhap;
                        xuatkho.MUC_DICH_XUAT_NHAP_ID = model.MucDichXuatNhapId;
                        xuatkho.GHI_CHU = model.GhiChuXuatNhap;
                        xuatkho.TRANG_THAI_ID = (int)enumTrangThaiCongCu.CHUA_SU_DUNG;
                        _xuatNhapService.UpdateXuatNhap(xuatkho);
                        _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", xuatkho.ToModel<XuatNhapModel>(), "XuatNhap");
                        // nhập kho đơn vị bộ phận
                        nhapdvbp = _xuatNhapService.GetXuatNhaps(isXuat: false, fromId: xuatkho.ID, LoaiXuatNhap: (int)enumLoaiXuatNhap.PHAN_BO).FirstOrDefault();
                        if (nhapdvbp != null)
                        {
                            nhapdvbp.TEN = model.TenXuatNhap;
                            nhapdvbp.NGAY_XUAT_NHAP = model.NgayXuatNhap;
                            nhapdvbp.MUC_DICH_XUAT_NHAP_ID = model.MucDichXuatNhapId;
                            nhapdvbp.GHI_CHU = model.GhiChuXuatNhap;
                            nhapdvbp.TRANG_THAI_ID = model.TRANG_THAI_ID;
                            _xuatNhapService.UpdateXuatNhap(nhapdvbp);
                            _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", nhapdvbp.ToModel<XuatNhapModel>(), "XuatNhap");
                        }
                    }
                    else
                    {
                        xuatkho = new XuatNhap();
                        // xuất kho
                        var sttXuatKho = _xuatNhapService.GetValueIdMax();
                        xuatkho.FROM_XUAT_NHAP_ID = nhapkho.ID;
                        xuatkho.TEN = model.TenXuatNhap;
                        xuatkho.DON_VI_ID = _workContext.CurrentDonVi.ID;
                        xuatkho.IS_XUAT = true;
                        xuatkho.NGAY_XUAT_NHAP = model.NgayXuatNhap;
                        xuatkho.MA = sttXuatKho != null ? model.CurrentDonViMa + "-" + sttXuatKho : model.CurrentDonViMa + "-1";
                        xuatkho.MA_LIEN_QUAN = nhapkho.MA_LIEN_QUAN;
                        xuatkho.GHI_CHU = model.GhiChuXuatNhap;
                        xuatkho.MUC_DICH_XUAT_NHAP_ID = model.MucDichXuatNhapId;
                        xuatkho.LOAI_XUAT_NHAP_ID = (Decimal)enumLoaiXuatNhap.PHAN_BO;
                        xuatkho.TRANG_THAI_ID = (int)enumTrangThaiCongCu.CHUA_SU_DUNG;
                        xuatkho.IS_PHAN_BO_FIRST = true;
                        _xuatNhapService.InsertXuatNhap(xuatkho);
                        _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", xuatkho.ToModel<XuatNhapModel>(), "XuatNhap");

                        // nhập đơn vị bộ phận                     
                        var sstNhapDVBP = _xuatNhapService.GetValueIdMax();
                        nhapdvbp.FROM_XUAT_NHAP_ID = xuatkho.ID;
                        nhapdvbp.TEN = model.TenXuatNhap;
                        nhapdvbp.DON_VI_BO_PHAN_ID = model.DON_VI_BO_PHAN_ID;
                        nhapdvbp.DON_VI_ID = _workContext.CurrentDonVi.ID;
                        nhapdvbp.IS_XUAT = false;
                        nhapdvbp.NGAY_XUAT_NHAP = model.NgayXuatNhap;
                        nhapdvbp.MA = sstNhapDVBP != null ? model.CurrentDonViMa + "-" + sstNhapDVBP : model.CurrentDonViMa + "-1";
                        nhapdvbp.MA_LIEN_QUAN = nhapkho.MA_LIEN_QUAN;
                        nhapdvbp.GHI_CHU = model.GhiChuXuatNhap;
                        nhapdvbp.MUC_DICH_XUAT_NHAP_ID = model.MucDichXuatNhapId;
                        nhapdvbp.LOAI_XUAT_NHAP_ID = (Decimal)enumLoaiXuatNhap.PHAN_BO;
                        nhapdvbp.TRANG_THAI_ID = model.TRANG_THAI_ID;
                        _xuatNhapService.InsertXuatNhap(nhapdvbp);
                        _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", nhapdvbp.ToModel<XuatNhapModel>(), "XuatNhap");
                    }
                }
                #region xóa nhập xuất công cụ
                // xóa hết nxcc để insert mới
                var allnxccNK = _nhapXuatCongCuService.GetNhapXuatCongCuByNhapXuatId(NhapXuatId: nhapkho.ID);
                if (allnxccNK.Count() > 0)
                {
                    _nhapXuatCongCuService.DeleteNhapXuatCongCus(allnxccNK);

                }
                // xóa xuất kho
                if (xuatkho.ID > 0)
                {
                    var allnxccXK = _nhapXuatCongCuService.GetNhapXuatCongCuByNhapXuatId(NhapXuatId: xuatkho.ID);
                    if (allnxccXK.Count() > 0)
                    {
                        _nhapXuatCongCuService.DeleteNhapXuatCongCus(allnxccXK);

                    }
                }
                // xóa kho đơn vị
                if (nhapdvbp.ID > 0)
                {
                    var allnxccNKDVBP = _nhapXuatCongCuService.GetNhapXuatCongCuByNhapXuatId(NhapXuatId: nhapdvbp.ID);
                    if (allnxccNKDVBP.Count() > 0)
                    {
                        _nhapXuatCongCuService.DeleteNhapXuatCongCus(allnxccNKDVBP);

                    }
                }
                #endregion
                #region insert nhập xuất công cụ
                // insert map nhập xuất công cụ
                foreach (var LCC in model.ListLoCongCu)
                {

                    if (LCC.CongCuId != 0 && LCC.CongCuId != null)
                    {
                        // insert mapping
                        var mapping = new NhapXuatCongCu()
                        {
                            CONG_CU_ID = LCC.CongCuId.Value,
                            NHAP_XUAT_ID = nhapkho.ID,
                            SO_LUONG = LCC.SO_LUONG,
                            CHUNG_TU_SO = model.ChungTuSoXuatNhap,
                            CHUNG_TU_NGAY = model.ChungTuNgayXuatNhap,
                            DON_GIA = LCC.DON_GIA,
                            THANH_TIEN = LCC.SO_LUONG * LCC.DON_GIA,
                            TRANG_THAI_ID = (int)enumTrangThaiCongCu.CHUA_SU_DUNG
                        };
                        _nhapXuatCongCuService.InsertNhapXuatCongCu(mapping);
                        // insert xuất/nhập đơn vị bộ phận
                        if (model.DON_VI_BO_PHAN_ID > 0)
                        {
                            // mapping
                            var mappingXuatKho = new NhapXuatCongCu()
                            {
                                CONG_CU_ID = LCC.CongCuId.Value,
                                NHAP_XUAT_ID = xuatkho.ID,
                                SO_LUONG = LCC.SO_LUONG,
                                DON_GIA = LCC.DON_GIA,
                                TRANG_THAI_ID = (int)enumTrangThaiCongCu.CHUA_SU_DUNG,
                                NHAP_KHO_ID = mapping.ID
                            };
                            _nhapXuatCongCuService.InsertNhapXuatCongCu(mappingXuatKho);
                            var mappingNhapDonVi = new NhapXuatCongCu()
                            {
                                CONG_CU_ID = LCC.CongCuId.Value,
                                NHAP_XUAT_ID = nhapdvbp.ID,
                                SO_LUONG = LCC.SO_LUONG,
                                DON_GIA = LCC.DON_GIA,
                                TRANG_THAI_ID = model.TRANG_THAI_ID
                            };
                            _nhapXuatCongCuService.InsertNhapXuatCongCu(mappingNhapDonVi);
                        }
                    }
                    else
                    {
                        var congCu = new CongCu();
                        var sttMaCongCu = _itemService.GetValueIdMax();
                        if (sttMaCongCu != null)
                        {
                            congCu.MA = _workContext.CurrentDonVi.MA_DON_VI + "-" + LCC.NHOM_CONG_CU_ID + "-" + sttMaCongCu;
                        }
                        else
                        {
                            congCu.MA = _workContext.CurrentDonVi.MA_DON_VI + "-" + LCC.NHOM_CONG_CU_ID + "-1";
                        }
                        congCu.TEN = LCC.TEN;
                        congCu.DON_VI_ID = _workContext.CurrentDonVi.ID;
                        congCu.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                        congCu.NGAY_TAO = DateTime.Now;
                        congCu.NHOM_CONG_CU_ID = LCC.NHOM_CONG_CU_ID;
                        _itemService.InsertCongCu(congCu);
                        var mapping = new NhapXuatCongCu()
                        {
                            CONG_CU_ID = congCu.ID,
                            NHAP_XUAT_ID = nhapkho.ID,
                            SO_LUONG = LCC.SO_LUONG,
                            CHUNG_TU_SO = model.ChungTuSoXuatNhap,
                            CHUNG_TU_NGAY = model.ChungTuNgayXuatNhap,
                            DON_GIA = LCC.DON_GIA,
                            THANH_TIEN = LCC.SO_LUONG * LCC.DON_GIA,
                            TRANG_THAI_ID = (int)enumTrangThaiCongCu.CHUA_SU_DUNG,
                        };
                        _nhapXuatCongCuService.InsertNhapXuatCongCu(mapping);
                        // insert xuất/nhập đơn vị bộ phận
                        if (model.DON_VI_BO_PHAN_ID > 0)
                        {
                            // mapping
                            var mappingXuatKho = new NhapXuatCongCu()
                            {
                                CONG_CU_ID = congCu.ID,
                                NHAP_XUAT_ID = xuatkho.ID,
                                SO_LUONG = LCC.SO_LUONG,
                                DON_GIA = LCC.DON_GIA,
                                TRANG_THAI_ID = (int)enumTrangThaiCongCu.CHUA_SU_DUNG,
                                NHAP_KHO_ID = mapping.ID
                            };
                            _nhapXuatCongCuService.InsertNhapXuatCongCu(mappingXuatKho);
                            var mappingNhapDonVi = new NhapXuatCongCu()
                            {
                                CONG_CU_ID = congCu.ID,
                                NHAP_XUAT_ID = nhapdvbp.ID,
                                SO_LUONG = LCC.SO_LUONG,
                                DON_GIA = LCC.DON_GIA,
                                TRANG_THAI_ID = model.TRANG_THAI_ID
                            };
                            _nhapXuatCongCuService.InsertNhapXuatCongCu(mappingNhapDonVi);
                        }
                    }
                }
                #endregion
                return JsonSuccessMessage("Cập nhật dữ liệu thành công !");
            }
            var listERR = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Cập nhật không thành công", listERR);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLNhapLoCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _xuatNhapService.GetXuatNhapById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                //xoa phan bo 
                var listXN = _xuatNhapService.GetXuatNhapsByMaLienQuan(maLienQuan: item.MA);
                if (listXN.Count() > 0)
                {
                    foreach (var pb in listXN)
                    {
                        var listMapPB = _nhapXuatCongCuService.GetNhapXuatCongCus(NhapXuatId: pb.ID);
                        _nhapXuatCongCuService.DeleteNhapXuatCongCus(listMapPB);
                    }
                    _xuatNhapService.DeleteXuatNhaps(listXN);
                    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<XuatNhapModel>(), "XuatNhap");
                    // return luôn vì listXN đã chứa item
                    return JsonSuccessMessage("Xoá dữ liệu thành công");
                }
                // xoa mapping
                var listMap = _nhapXuatCongCuService.GetNhapXuatCongCus(NhapXuatId: item.ID);
                _nhapXuatCongCuService.DeleteNhapXuatCongCus(listMap);
                // xoa xuat nhap
                _xuatNhapService.DeleteXuatNhap(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<XuatNhapModel>(), "XuatNhap");
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Có lỗi trong quá trình xử lý");
            }
        }

        public virtual IActionResult _AddNewLoCongCu(Decimal MapId)
        {
            var model = new CongCuModel();
            if (MapId > 0)
            {
                var item = _nhapXuatCongCuService.GetNhapXuatCongCuById(MapId);
                model.NHOM_CONG_CU_ID = item.CongCu.NHOM_CONG_CU_ID;
                model.ID = item.CONG_CU_ID;
                model.SO_LUONG = item.SO_LUONG;
                model.DON_GIA = item.DON_GIA;
                model.TRANG_THAI_ID = item.TRANG_THAI_ID;
                model.MapId = MapId;
            }
            model.DDLNhomCongCu = _nhomCongCuModelFactory.PrepareDDLNhomCongCu(_workContext.CurrentDonVi.ID);
            model.DDLTrangThai = ((enumTrangThaiCongCu)model.TrangThaiCongCuEnum).ToSelectList();

            return PartialView(model);
        }

        public virtual IActionResult GetJsonCongCuByNhom(Decimal nhomCongCu)
        {
            var item = _itemModelFactory.PrepareDDLCongCuByNhom(nhomCongCu);
            return Json(item);
        }

        public virtual IActionResult GetJsonCongCuTheoNhom(int maNhomCongCu, int congCuSelected)
        {
            if (congCuSelected != 0)
            {
                var congCuSel = _itemService.GetCongCus(0, maNhomCongCu).Where(x => x.ID == congCuSelected).FirstOrDefault();
                var allCongCu = _itemService.GetCongCus(0, maNhomCongCu).Where(x => x.ID != congCuSelected).ToList();
                allCongCu = allCongCu.Prepend(congCuSel).ToList();
                var listCongCu = allCongCu.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.TEN,
                    Selected = c.ID == congCuSelected
                }).ToList();

                return Json(listCongCu);
            }
            else
            {
                var listCongCu = _itemService.GetCongCus(0, maNhomCongCu).Select(c => new { Value = c.ID, Text = c.TEN }).ToList(); ;
                return Json(listCongCu);
            }

        }

        [HttpPost]
        public virtual IActionResult GetJsonNhomCongCu(int sel)
        {
            var ListNhomCC = _nhomCongCuModelFactory.PrepareDDLNhomCongCu(_workContext.CurrentDonVi.ID, isAddFirst: true);
            return Json(ListNhomCC);
        }
        public virtual IActionResult GetDDLTrangThai(int BoPhanId)
        {
            var model = new List<SelectListItem>();
            if (BoPhanId > 0)
            {
                model = new enumTrangThaiCongCu().ToSelectList(valuesToExclude: new int[] { (int)enumTrangThaiCongCu.CHUA_SU_DUNG }).ToList();
            }
            else
            {
                model = new enumTrangThaiCongCu().ToSelectList(valuesToExclude: new int[] { (int)enumTrangThaiCongCu.CHON, (int)enumTrangThaiCongCu.DANG_SU_DUNG, (int)enumTrangThaiCongCu.HONG_CHO_XU_LY }).ToList();
            }
            return Json(model);
        }

        public virtual IActionResult _ThemMoiCongCu(String IDF)
        {
            //prepare model
            var model = _nhomCongCuModelFactory.PrepareNhomCongCuModel(new NhomCongCuModel(), null);
            model.stringGuid = IDF;
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult _ThemMoiCongCu(NhomCongCuModel model)
        {
            if (ModelState.IsValid)
            {
                var item = model.ToEntity<NhomCongCu>();
                _nhomCongCuService.InsertNhomCongCu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<NhomCongCuModel>(), "NhomCongCu");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return JsonSuccessMessage("Thành công", item.ID);
            }
            //prepare model
            var check = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Không thành công", check);
        }

        [HttpPost]
        public virtual IActionResult DeleteCongCu(Decimal id)
        {
            var map = _nhapXuatCongCuService.GetNhapXuatCongCuById(id);
            if (map == null)
                return RedirectToAction("List");
            var listXuatNhapKhac = _xuatNhapService.GetXuatNhaps(isXuat: false, maLienQuan: map.XuatNhap.MA_LIEN_QUAN).Select(c => c.ID);
            var mapKhac = _nhapXuatCongCuService.GetNhapXuatCongCus(CongCuId: map.CONG_CU_ID, ListNhapXuatId: listXuatNhapKhac.ToArray());
            if (mapKhac != null && mapKhac.Count() > 0)
                return JsonErrorMessage("Có lỗi trong quá trình xử lý", map.ToModel<NhapXuatCongCuModel>());
            try
            {
                _nhapXuatCongCuService.DeleteNhapXuatCongCu(map);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", map.ToModel<NhapXuatCongCuModel>(), "Mapping");
                return JsonSuccessMessage("Thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Có lỗi trong quá trình xử lý");
            }
        }

        public virtual IActionResult ImportCCDC()
        {
            return View();
        }
        public virtual IActionResult ImportKiemKeCCDC()
        {
            return View();
        }


        [HttpPost]
        //[RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        //[RequestSizeLimit(209715200)]
        public IActionResult ImportCCDCFromFile(IFormFile file)
        {
            if (file == null)
                return JsonErrorMessage("Bạn chưa nhập file CCDC");
            var dataByte = _fileCongViecService.GetWorkFileBits(file);
            var fileName = file.FileName;
            var fileExtension = _fileProvider.GetFileExtension(fileName);
            if (!string.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();
            //  lưu file 
            var contentType = file.ContentType;
            var fwork = new FileCongViec
            {
                GUID = Guid.NewGuid(),
                NOI_DUNG_FILE = null,
                LOAI_FILE = contentType,
                TEN_FILE = _fileProvider.GetFileNameWithoutExtension(fileName),
                DUOI_FILE = fileExtension,
                NGAY_TAO = DateTime.Now,
                NGUOI_TAO = _workContext.CurrentCustomer.ID
            };
            _fileCongViecModelFactory.SaveWorkFileOnDisk(fwork, dataByte);
            //Đọc file
            var DataImport = GetWorkFileContentOnDisk(fwork);
            var path = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), fwork.NGAY_TAO.ToPathFolderStore(), fwork.GUID.ToString() + fileExtension);
            string dataString = _fileProvider.ReadAllText(path, Encoding.UTF8);
            if (fwork.DUOI_FILE == ".xls" || fwork.DUOI_FILE == ".xlsx")
            {
                Stream stream = new MemoryStream(DataImport);
                //var result = _dBTaiSanService.ImportExcel(stream);
            }
            else if (fwork.DUOI_FILE == ".json")
            {
                List<IMP_CCDCModel> lstCCDC = new List<IMP_CCDCModel>();
                List<IMP_CCDCModel> lstCCDCerr = new List<IMP_CCDCModel>();
                List<IMP_CCDCModel> lstCCDCsuccess = new List<IMP_CCDCModel>();
                var serializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
                serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializerSettings.DateFormatString = CommonHelper.DATE_FORMAT_DB;
                lstCCDC = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IMP_CCDCModel>>(dataString, serializerSettings);
                //lstCCDC = dataString.toEntities<IMP_CCDCModel>();
                if (lstCCDC != null && lstCCDC.Count > 0)
                {
                    foreach (IMP_CCDCModel item in lstCCDC)
                    {
                        //MessageReturn mss = _itemModelFactory.importCCDC(item);
                        MessageReturn mss = _itemService.insertOrupdateCCDCByJson(item.toStringJson());
                        if (mss.Code != MessageReturn.SUCCESS_CODE)
                        {
                            item.MESSAGE = mss.Message;
                            if (item.CONG_CU.ID > 0)
                                _itemService.DelelteCongCuAndThongTinLienQuan(item.CONG_CU.ID, 1);
                            lstCCDCerr.Add(item);
                        }
                        else
                            lstCCDCsuccess.Add(item);
                    }
                }
                else
                    return JsonErrorMessage("Không có dữ liệu cập nhập!");
                if (lstCCDCerr.Count > 0)
                {
                    //_fileCongViecService.DeleteFileCongViec(fileCongViec);
                    string _pathStore = DateTime.Now.ToPathFolderStore();
                    _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
                    _fileProvider.CreateDirectory(_pathStore);

                    string fName = "";
                    string fullpath = "";
                    fName = string.Format("LOG_ERR_IMPORT_CCDC_{0}({1}).json", lstCCDCerr.Count(), DateTime.Now.ToString("dd-MM-yyyy hh-mm"));
                    fullpath = _fileProvider.Combine(_pathStore, fName);
                    string json = "";
                    serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    serializerSettings.Formatting = Formatting.None;
                    json = JsonConvert.SerializeObject(lstCCDCerr, serializerSettings);

                    _fileProvider.WriteAllText(fullpath, json, Encoding.UTF8);
                    return Json(new
                    {
                        success = false,
                        ListSuccess = lstCCDCsuccess,
                        ListError = lstCCDCerr,
                        filePath = fullpath,
                        fileName = fName,
                        fileType = MimeTypes.ApplicationJson
                    });
                }

                else
                {
                    //_fileCongViecService.DeleteFileCongViec(fileCongViec);
                    //SuccessNotification("Import tài sản thành công");
                    return Json(new
                    {
                        success = false,
                        ListSuccess = lstCCDCsuccess,
                        //ListError = ListResultError,
                    });
                }
            }
            return Json(dataString);

        }
        [HttpPost]
        //[RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        //[RequestSizeLimit(209715200)]
        public IActionResult ImportCCDCFromDirectory(string pathFolder)
        {
            if (string.IsNullOrEmpty(pathFolder))
            {
                ErrorNotification("Bạn chưa Nhập đường dẫn thư mục file tài sản");
                return RedirectToAction("DongBoTaiSan");
            }
            List<IMP_CCDCModel> lstCCDC = new List<IMP_CCDCModel>();
            List<IMP_CCDCModel> lstCCDCerr = new List<IMP_CCDCModel>();
            List<IMP_CCDCModel> lstCCDCsuccess = new List<IMP_CCDCModel>();
            var serializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
            serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            serializerSettings.DateFormatString = CommonHelper.DATE_FORMAT_DB;
            string[] filePaths = Directory.GetFiles(pathFolder, "*.json", SearchOption.AllDirectories);
            foreach (string filePath in filePaths)
            {
                if (_fileProvider.FileExists(filePath))
                {
                    var dataByte = _fileProvider.ReadAllBytes(filePath);
                    var fileName = _fileProvider.GetFileName(filePath);
                    var fileExtension = _fileProvider.GetFileExtension(fileName);
                    if (!string.IsNullOrEmpty(fileExtension))
                        fileExtension = fileExtension.ToLowerInvariant();
                    //  lưu file 
                    var contentType = MimeTypes.ApplicationJson;
                    var fwork = new FileCongViec
                    {
                        GUID = Guid.NewGuid(),
                        NOI_DUNG_FILE = null,
                        LOAI_FILE = contentType,
                        //we store filename without extension for downloads
                        TEN_FILE = fileName,
                        DUOI_FILE = fileExtension,
                        NGAY_TAO = DateTime.Now,
                        NGUOI_TAO = _workContext.CurrentCustomer.ID
                    };
                    //_fileCongViecService.InsertFileCongViec(fwork);
                    _fileCongViecModelFactory.SaveWorkFileOnDisk(fwork, dataByte);
                    //Đọc file
                    //var fileCongViec = _fileCongViecService.GetFileByGuid(fwork.GUID);
                    var DataImport = GetWorkFileContentOnDisk(fwork);
                    var path = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), fwork.NGAY_TAO.ToPathFolderStore(), fwork.GUID.ToString() + fileExtension);
                    string dataString = _fileProvider.ReadAllText(path, Encoding.UTF8);
                    lstCCDC = JsonConvert.DeserializeObject<List<IMP_CCDCModel>>(dataString, serializerSettings);

                    if (lstCCDC != null && lstCCDC.Count > 0)
                    {
                        foreach (IMP_CCDCModel item in lstCCDC)
                        {
                            MessageReturn mss = _itemService.insertOrupdateCCDCByJson(item.toStringJson());
                            if (mss.Code != MessageReturn.SUCCESS_CODE)
                            {
                                item.MESSAGE = mss.Message;
                                if (item.CONG_CU.ID > 0)
                                    _itemService.DelelteCongCuAndThongTinLienQuan(item.CONG_CU.ID, 1);
                                lstCCDCerr.Add(item);
                            }
                            else
                                lstCCDCsuccess.Add(item);
                        }
                    }
                    else
                        return JsonErrorMessage("Không có dữ liệu cập nhập!");
                }
            }
            if (lstCCDCerr.Count > 0)
            {
                //_fileCongViecService.DeleteFileCongViec(fileCongViec);
                string _pathStore = DateTime.Now.ToPathFolderStore();
                _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
                _fileProvider.CreateDirectory(_pathStore);

                string fName = "";
                string fullpath = "";
                fName = string.Format("LOG_ERR_IMPORT_CCDC_{0}({1}).json", lstCCDCerr.Count(), DateTime.Now.ToString("dd-MM-yyyy hh-mm"));
                fullpath = _fileProvider.Combine(_pathStore, fName);
                string json = "";
                serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                json = JsonConvert.SerializeObject(lstCCDCerr, serializerSettings);

                _fileProvider.WriteAllText(fullpath, json, Encoding.UTF8);
                return Json(new
                {
                    success = false,
                    ListSuccess = lstCCDCsuccess,
                    ListError = lstCCDCerr,
                    filePath = fullpath,
                    fileName = fName,
                    fileType = MimeTypes.ApplicationJson
                });
            }

            else
            {
                //_fileCongViecService.DeleteFileCongViec(fileCongViec);
                //SuccessNotification("Import tài sản thành công");
                return Json(new
                {
                    success = false,
                    ListSuccess = lstCCDCsuccess,
                    //ListError = ListResultError,
                });
            }

        }

        [HttpPost]
        //[RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        //[RequestSizeLimit(209715200)]
        public IActionResult ImportKiemKeCCDCFromFile(IFormFile file)
        {
            if (file == null)
                return JsonErrorMessage("Bạn chưa nhập file CCDC");
            var dataByte = _fileCongViecService.GetWorkFileBits(file);
            var fileName = file.FileName;
            var fileExtension = _fileProvider.GetFileExtension(fileName);
            if (!string.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();
            //  lưu file 
            var contentType = file.ContentType;
            var fwork = new FileCongViec
            {
                GUID = Guid.NewGuid(),
                NOI_DUNG_FILE = null,
                LOAI_FILE = contentType,
                TEN_FILE = _fileProvider.GetFileNameWithoutExtension(fileName),
                DUOI_FILE = fileExtension,
                NGAY_TAO = DateTime.Now,
                NGUOI_TAO = _workContext.CurrentCustomer.ID
            };
            _fileCongViecModelFactory.SaveWorkFileOnDisk(fwork, dataByte);
            //Đọc file
            var DataImport = GetWorkFileContentOnDisk(fwork);
            var path = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), fwork.NGAY_TAO.ToPathFolderStore(), fwork.GUID.ToString() + fileExtension);
            string dataString = _fileProvider.ReadAllText(path, Encoding.UTF8);
            if (fwork.DUOI_FILE == ".xls" || fwork.DUOI_FILE == ".xlsx")
            {
                Stream stream = new MemoryStream(DataImport);
                //var result = _dBTaiSanService.ImportExcel(stream);
            }
            else if (fwork.DUOI_FILE == ".json")
            {
                List<IMP_KEMKE_CCDC> lstKiemKe = new List<IMP_KEMKE_CCDC>();
                List<IMP_KEMKE_CCDC> lstKiemKeerr = new List<IMP_KEMKE_CCDC>();
                List<IMP_KEMKE_CCDC> lstKiemKesuccess = new List<IMP_KEMKE_CCDC>();
                var serializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
                serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializerSettings.DateFormatString = CommonHelper.DATE_FORMAT_DB;
                //lstKiemKe = dataString.toEntities<IMP_KEMKE_CCDC>();
                lstKiemKe = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IMP_KEMKE_CCDC>>(dataString, serializerSettings);
                if (lstKiemKe != null && lstKiemKe.Count > 0)
                {
                    foreach (IMP_KEMKE_CCDC item in lstKiemKe)
                    {
                        //MessageReturn mss = _itemModelFactory.importKiemKe(item);
                        MessageReturn mss = _itemService.insertOrupdateKiemKeByJson(item.toStringJson());
                        if (mss.Code != MessageReturn.SUCCESS_CODE)
                        {
                            item.MESSAGE = mss.Message;
                            if (item.ID > 0)
                                _kiemKeService.DelelteKiemKeCCDCByStore(item.ID);
                            lstKiemKeerr.Add(item);
                        }
                        else
                            lstKiemKesuccess.Add(item);
                    }
                }
                else
                    return JsonErrorMessage("Không có dữ liệu cập nhập!");
                if (lstKiemKeerr.Count > 0)
                {
                    //_fileCongViecService.DeleteFileCongViec(fileCongViec);
                    string _pathStore = DateTime.Now.ToPathFolderStore();
                    _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
                    _fileProvider.CreateDirectory(_pathStore);

                    string fName = "";
                    string fullpath = "";
                    fName = string.Format("LOG_ERR_IMPORT_KIEMKE_CCDC_{0}({1}).json", lstKiemKeerr.Count(), DateTime.Now.ToString("dd-MM-yyyy hh-mm"));
                    fullpath = _fileProvider.Combine(_pathStore, fName);
                    string json = "";
                    serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    serializerSettings.Formatting = Formatting.None;
                    json = JsonConvert.SerializeObject(lstKiemKeerr, serializerSettings);

                    _fileProvider.WriteAllText(fullpath, json, Encoding.UTF8);
                    return Json(new
                    {
                        success = false,
                        ListSuccess = lstKiemKesuccess,
                        ListError = lstKiemKeerr,
                        filePath = fullpath,
                        fileName = fName,
                        fileType = MimeTypes.ApplicationJson
                    });
                }

                else
                {
                    //_fileCongViecService.DeleteFileCongViec(fileCongViec);
                    //SuccessNotification("Import tài sản thành công");
                    return Json(new
                    {
                        success = false,
                        ListSuccess = lstKiemKesuccess,
                        //ListError = ListResultError,
                    });
                }
            }
            return Json(dataString);

        }
        byte[] GetWorkFileContentOnDisk(FileCongViec item)
        {
            var _fileStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), item.NGAY_TAO.ToPathFolderStore(), item.GUID.ToString() + item.DUOI_FILE);
            return _fileProvider.ReadAllBytes(_fileStore);
        }

        [HttpPost]
        public virtual IActionResult SearchCCDCDongBo(String Keysearch)
        {
            if (String.IsNullOrEmpty(Keysearch))
                return null;
            var rs = _itemService.SearchCongCuDongBo(Keysearch).Select(c => new { MA = c.MA, TEN = c.TEN, MA_DB = c.MA_DB });
            return Json(rs);
        }
        #endregion
    }
}

