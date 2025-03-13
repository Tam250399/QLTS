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
using GS.Core.Domain.SHTD;

using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.SHTD;
using GS.Web.Controllers;
using GS.Web.Factories.SHTD;
using GS.Services.SHTD;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using System.Collections.Generic;
using GS.Web.Models.ThuocTinh;
using System.Linq;
using GS.Core.Domain.ThuocTinhs;
using GS.Web.Factories.ThuocTinhs;
using GS.Services.DanhMuc;
using GS.Web.Factories.DanhMuc;
using GS.Services.ThuocTinhs;
using GS.Web.Factories.DB;
using GS.Core.Domain.DB;
using GS.Web.Models.DanhMuc;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class TaiSanTdXuLyController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly ITaiSanTdXuLyModelFactory _itemModelFactory;
        private readonly ITaiSanTdXuLyService _itemService;
        private readonly IThuocTinhModelFactory _thuocTinhModelFactory;
        private readonly IThuocTinhDataModelFactory _thuocTinhDataModelFactory;
        private readonly IPhuongAnXuLyService _phuongAnXuLyService;
        private readonly ITaiSanTdService _taiSanTdService;
        private readonly IHinhThucXuLyService hinhThucXuLyService;
        private readonly IThuocTinhDataService _thuocTinhDataService;
        private readonly IXuLyService _xuLyService;
        private readonly ITaiSanTdModelFactory _taiSanTdModelFactory;
        private readonly IKetQuaService _ketQuaService;
        private readonly IKetQuaModelFactory _ketQuaModelFactory;
        private readonly IPhuongAnXuLyModelFactory _phuongAnXuLyModelFactory;
        private readonly IDB_QueueProcessModelFactory _dB_QueueProcessModelFactory;
        private readonly IDonViModelFactory _donViModelFactory;

        #endregion

        #region Ctor
        public TaiSanTdXuLyController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ITaiSanTdXuLyModelFactory itemModelFactory,
            ITaiSanTdXuLyService itemService,
            IThuocTinhModelFactory thuocTinhModelFactory,
            IThuocTinhDataModelFactory thuocTinhDataModelFactory,
             IPhuongAnXuLyService phuongAnXuLyService,
            ITaiSanTdService taiSanTdService,
            IHinhThucXuLyService hinhThucXuLyService,
             IThuocTinhDataService thuocTinhDataService,
             IXuLyService xuLyService,
             ITaiSanTdModelFactory taiSanTdModelFactory,
             IKetQuaService ketQuaService,
             IKetQuaModelFactory ketQuaModelFactory,
             IPhuongAnXuLyModelFactory phuongAnXuLyModelFactory,
             IDB_QueueProcessModelFactory dB_QueueProcessModelFactory,
             IDonViModelFactory donViModelFactory
            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._thuocTinhModelFactory = thuocTinhModelFactory;
            this._thuocTinhDataModelFactory = thuocTinhDataModelFactory;
            this._phuongAnXuLyService = phuongAnXuLyService;
            this._taiSanTdService = taiSanTdService;
            this.hinhThucXuLyService = hinhThucXuLyService;
            this._thuocTinhDataService = thuocTinhDataService;
            this._xuLyService = xuLyService;
            this._taiSanTdModelFactory = taiSanTdModelFactory;
            this._ketQuaService = ketQuaService;
            this._ketQuaModelFactory = ketQuaModelFactory;
            this._phuongAnXuLyModelFactory = phuongAnXuLyModelFactory;
            this._dB_QueueProcessModelFactory = dB_QueueProcessModelFactory;
            _donViModelFactory = donViModelFactory;
        }
        #endregion
        #region Methods

        public virtual IActionResult List(Guid XuLyGuid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanTdXuLySearchModel() { XuLyGuid = XuLyGuid };
            var model = _itemModelFactory.PrepareTaiSanTdXuLySearchModel(searchmodel);
            return PartialView(model);
        }
        public virtual IActionResult _ListForKetQuaXuLy(int XuLyId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanTdXuLySearchModel() { XuLyId = XuLyId };
            var model = _itemModelFactory.PrepareTaiSanTdXuLySearchModel(searchmodel);
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult List(TaiSanTdXuLySearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanTdXuLyListModel(searchModel);
            return Json(model);
        }
        public virtual IActionResult ListPhuongAn(Guid XuLyGuid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanTdXuLySearchModel() { XuLyGuid = XuLyGuid };
            var model = _itemModelFactory.PrepareTaiSanTdXuLySearchModel(searchmodel);
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult ListPhuongAn(TaiSanTdXuLySearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanTdXuLyListModelForPhuongAn(searchModel);
            return Json(model);
        }
        [HttpPost]
        public virtual IActionResult ListPhuongAnAppend(TaiSanTdXuLySearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedKendoGridJson();
            //prepare model
            // hungnt xử lý để append list tsxl
            var model = _itemModelFactory.PrepareTaiSanTdXuLyListModelByListQuyetDinh(searchModel);
            return Json(model);
        }

        [HttpPost]
        public virtual IActionResult ListKetQua(TaiSanTdXuLySearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedKendoGridJson();
            //prepare model
            searchModel.isKetQua = true;
            var model = _itemModelFactory.PrepareTaiSanTdXuLyListModel(searchModel);
            return Json(model);
        }
        [HttpPost]
        public virtual IActionResult CreateByXuLy(Decimal XuLyId,IList<decimal> listQuyetDinh)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var xuly = _xuLyService.GetXuLyById(XuLyId);
            if (xuly != null)
            {
                var listTSXL = _itemService.GetTaiSanTdsXuLyByXuLyId(XuLyId: xuly.ID, TrangThaiId: (decimal)xuly.TRANG_THAI_ID);
                // những quyết định tài sản đã đc insert
                var listQuyetDinhDaChon = listTSXL.Select(c => c.taisantd.quyetdinh.ID).ToList();
                // bỏ những quyết định đã chọn
                listTSXL = listTSXL.Where(c => !listQuyetDinh.Contains(c.taisantd.quyetdinh.ID)).ToList();
                // xóa hết tstdxl cũ
                foreach (var tsxl in listTSXL)
                {
                    try
                    {
                        //xóa data thuoctinh
                        var thuoctinh = _thuocTinhDataService.GetThuocTinhDataByTaiSanId(TaiSanTdXuLyId: (int)tsxl.ID).FirstOrDefault();
                        if (thuoctinh != null)
                        {
                            _thuocTinhDataService.DeleteThuocTinhData(thuoctinh);
                            _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", thuoctinh.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                        }
                        _itemService.DeleteTaiSanTdXuLy(tsxl);
                        _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", tsxl.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                    }
                    catch (Exception exc)
                    {
                        ErrorNotification(exc);
                        return JsonErrorMessage("Cập nhật không thành công", exc);
                    }
                }
                // thêm theo quyết định mới
                //bỏ những quyết định đã thêm từ trc
                listQuyetDinh = listQuyetDinh.Where(c => !listQuyetDinhDaChon.Contains(c)).ToList();
                if (listQuyetDinh != null && listQuyetDinh.Count() > 0)
                {
                    foreach (var QD in listQuyetDinh)
                    {
                        // lấy được list tài sản chưa được xử lý kèm theo số lượng còn lại theo quyết định
                        var listTSTD = _taiSanTdService.GetTaiSanTdByPhuongAn(QuyetDinhId: QD);
                        foreach (var tstd in listTSTD)
                        {
                            var tstdXL = new TaiSanTdXuLy()
                            {
                                TAI_SAN_ID = tstd.ID,
                                XU_LY_ID = xuly.ID,
                                SO_LUONG = tstd.SO_LUONG
                            };
                            _itemService.InsertTaiSanTdXuLy(tstdXL);
                            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", tstdXL.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                        }
                    }
                }
                return JsonSuccessMessage("Cập nhật thành công", XuLyId);
            }
            return JsonErrorMessage("Cập nhật không thành công", XuLyId);
        }

        // Sửa không xóa những tstdxl đã tồn tại nữa
        [HttpPost]
        public virtual IActionResult CreateByXuLyWhenEdit(Decimal XuLyId, IList<decimal> listQuyetDinh)
        {
           
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var xuly = _xuLyService.GetXuLyById(XuLyId);
            if (xuly != null)
            {

                if (listQuyetDinh != null && listQuyetDinh.Count() > 0)
                {
                    foreach (var QD in listQuyetDinh)
                    {
                        // lấy được list tài sản chưa được xử lý kèm theo số lượng còn lại theo quyết định
                        var listTSTD = _taiSanTdService.GetTaiSanTdByPhuongAn(QuyetDinhId: QD);
                        foreach (var tstd in listTSTD)
                        {
                            var tstdXL = new TaiSanTdXuLy()
                            {
                                TAI_SAN_ID = tstd.ID,
                                XU_LY_ID = xuly.ID,
                                SO_LUONG = tstd.SO_LUONG
                            };
                            _itemService.InsertTaiSanTdXuLy(tstdXL);
                            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", tstdXL.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                        }
                    }
                }
                return JsonSuccessMessage("Cập nhật thành công", XuLyId);
            }
            return JsonErrorMessage("Cập nhật không thành công", XuLyId);
        }
        [HttpPost]
        public virtual IActionResult ViewThuocTinhByTaiSanTDXuLy(int PhuongAnXuLyId = 0, int TaiSanTdId = 0, Guid TaiSanXuLyGuid = new Guid())
        {
            var model = new List<modelThuocTinh>();
            model = _thuocTinhDataModelFactory.PrepareListmodelThuocTinhForTaiSanTdXuLy(PhuongAnXuLyId: PhuongAnXuLyId, TaiSanTdId: TaiSanTdId, TaiSanXuLyGuid: TaiSanXuLyGuid);
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult ViewEditRowXuLyTaiSan(IList<ListSoLuong> listSL, TaiSanTdXuLyModel model, string listVuViec, int LoaiXuLy)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            if (model == null) model = new TaiSanTdXuLyModel();
            model.ListSL = listSL.ToList();
            if (listVuViec != null && listVuViec != "")
            {
                model.listVuViec = listVuViec.Split(',').Select(c => c.ToNumberInt32()).ToList();
            }
            model.is_vali = true;
            model.LoaiXuLy = LoaiXuLy;
            model = _itemModelFactory.PrepareTaiSanTdXuLyModel(model, null, true);
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult ViewEditAllRowXuLyTaiSan(IList<ListSoLuong> listSL, TaiSanTdXuLyModel model, string listVuViec, int LoaiXuLy)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            if (model == null) model = new TaiSanTdXuLyModel();
            model.ListSL = listSL.ToList();
            if (listVuViec != null && listVuViec != "")
            {
                model.listVuViec = listVuViec.Split(',').Select(c => c.ToNumberInt32()).ToList();
            }
            model.is_vali = true;
            model.LoaiXuLy = LoaiXuLy;
            model = _itemModelFactory.PrepareTaiSanTdXuLyModel(model, null, true);
            model.DDLTaiSanTD.Remove(model.DDLTaiSanTD[0]);
            return PartialView(model);
        }
        public virtual IActionResult _ViewXuLyTaiSan(Guid Guid, Guid XuLyGuid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var item = _itemService.GetTaiSanTdXuLyByGuId(Guid);
            var model = _itemModelFactory.PrepareTaiSanTdXuLyModelForKetQua(new TaiSanTdXuLyModel() {}, item, true);
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult _ViewXuLyTaiSan(TaiSanTdXuLyModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            if (ModelState.IsValid)
            {
                var item = _itemService.GetTaiSanTdXuLyByGuId((Guid)model.GUID);
                if (item != null)
                {
                    //update tài sản xử lý
                    item.HINH_THUC_XU_LY_ID = model.HINH_THUC_XU_LY_ID;
                    _itemService.UpdateTaiSanTdXuLy(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                    //tìm kết quả xử lý
                    var ketquas = _ketQuaService.GetKetQuaBys(TaiSanTDXuLy: item.ID);
                    //update
                    if (ketquas != null && ketquas.Count() > 0)
                    {
                        //điều chuyển
                        if (model.ListKetQuaDC != null && model.ListKetQuaDC.Count() > 0)
                        {
                            // check vali
                            if (model.ListKetQuaDC.Where(c => c.DON_VI_CHUYEN_ID == 0 || c.DON_VI_CHUYEN_ID == null || c.SO_LUONG == 0 || c.SO_LUONG == null || c.NGAY_XU_LY == null).Count() > 0)
                            {
                                return JsonErrorMessage("Cập nhật không thành công", model);
                            }
                            // xóa hết r insert
                            foreach (var kq in ketquas)
                            {
                                _ketQuaService.DeleteKetQua(kq);
                                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", kq.ToModel<KetQuaModel>(), "KetQua");
                            }
                            //insert lại
                            foreach (var modelkq in model.ListKetQuaDC)
                            {
                                var ketqua = new KetQua()
                                {
                                    TAI_SAN_TD_XU_LY_ID = item.ID,
                                    NGAY_XU_LY = modelkq.NGAY_XU_LY,
                                    SO_LUONG = modelkq.SO_LUONG,
                                    DON_VI_CHUYEN_ID = model.DON_VI_DIEU_CHUYEN_ID,
                                    GUID = modelkq.GUID,
                                    GIA_TRI_BAN = modelkq.GIA_TRI_BAN,
                                    GIA_TRI_NSNN = model.ketQuaModel.GIA_TRI_NSNN,
                                    GIA_TRI_TKTG = model.ketQuaModel.GIA_TRI_TKTG,
                                    CHI_PHI_XU_LY = model.ketQuaModel.CHI_PHI_XU_LY,
                                    HOP_DONG_SO = model.ketQuaModel.HOP_DONG_SO,
                                    HOP_DONG_NGAY = model.ketQuaModel.HOP_DONG_NGAY,
                                    HO_SO_GIAY_TO_KHAC = model.ketQuaModel.HO_SO_GIAY_TO_KHAC
                                };
                                _ketQuaService.InsertKetQua(ketqua);
                                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", ketqua.ToModel<KetQuaModel>(), "KetQua");
                            }
                        }
                        else
                        {
                            var ketqua = ketquas.FirstOrDefault();
                            ketqua.NGAY_XU_LY = model.ketQuaModel.NGAY_XU_LY;
                            ketqua.SO_LUONG = model.ketQuaModel.SO_LUONG;
                            ketqua.DON_VI_CHUYEN_ID = model.ketQuaModel.DON_VI_CHUYEN_ID;
                            ketqua.GIA_TRI_NSNN = model.ketQuaModel.GIA_TRI_NSNN;
                            ketqua.GIA_TRI_TKTG = model.ketQuaModel.GIA_TRI_TKTG;
                            ketqua.CHI_PHI_XU_LY = model.ketQuaModel.CHI_PHI_XU_LY;
                            ketqua.HOP_DONG_SO = model.ketQuaModel.HOP_DONG_SO;
                            ketqua.HOP_DONG_NGAY = model.ketQuaModel.HOP_DONG_NGAY;
                            ketqua.GIA_TRI_BAN = model.ketQuaModel.GIA_TRI_BAN;
                            ketqua.HO_SO_GIAY_TO_KHAC = model.ketQuaModel.HO_SO_GIAY_TO_KHAC;
                            _ketQuaService.UpdateKetQua(ketqua);
                            _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", ketqua.ToModel<KetQuaModel>(), "KetQua");
                        }
                    }
                    //insert
                    else
                    {
                        //điều chuyển
                        if (model.ListKetQuaDC != null && model.ListKetQuaDC.Count() > 0)
                        {
                            // check vali
                            if (model.ListKetQuaDC.Where(c => c.DON_VI_CHUYEN_ID == 0 || c.DON_VI_CHUYEN_ID == null || c.SO_LUONG == 0 || c.SO_LUONG == null || c.NGAY_XU_LY == null).Count() > 0)
                            {
                                return JsonErrorMessage("Cập nhật không thành công", model);
                            }
                            //insert lại
                            foreach (var modelkq in model.ListKetQuaDC)
                            {
                                var ketqua = new KetQua()
                                {
                                    TAI_SAN_TD_XU_LY_ID = item.ID,
                                    NGAY_XU_LY = modelkq.NGAY_XU_LY,
                                    SO_LUONG = modelkq.SO_LUONG,
                                    DON_VI_CHUYEN_ID = modelkq.DON_VI_CHUYEN_ID,
                                    GUID = modelkq.GUID,
                                    GIA_TRI_BAN = modelkq.GIA_TRI_BAN,
                                    GIA_TRI_NSNN = model.ketQuaModel.GIA_TRI_NSNN,
                                    GIA_TRI_TKTG = model.ketQuaModel.GIA_TRI_TKTG,
                                    CHI_PHI_XU_LY = model.ketQuaModel.CHI_PHI_XU_LY,
                                    HOP_DONG_SO = model.ketQuaModel.HOP_DONG_SO,
                                    HOP_DONG_NGAY = model.ketQuaModel.HOP_DONG_NGAY,
                                    HO_SO_GIAY_TO_KHAC = model.ketQuaModel.HO_SO_GIAY_TO_KHAC
                                };
                                _ketQuaService.InsertKetQua(ketqua);
                                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", ketqua.ToModel<KetQuaModel>(), "KetQua");
                            }
                        }
                        else
                        {
                            var ketqua = new KetQua()
                            {
                                TAI_SAN_TD_XU_LY_ID = item.ID,
                                NGAY_XU_LY = model.ketQuaModel.NGAY_XU_LY,
                                SO_LUONG = model.ketQuaModel.SO_LUONG,
                                DON_VI_CHUYEN_ID = model.ketQuaModel.DON_VI_CHUYEN_ID,
                                GIA_TRI_NSNN = model.ketQuaModel.GIA_TRI_NSNN,
                                GIA_TRI_TKTG = model.ketQuaModel.GIA_TRI_TKTG,
                                CHI_PHI_XU_LY = model.ketQuaModel.CHI_PHI_XU_LY,
                                HOP_DONG_SO = model.ketQuaModel.HOP_DONG_SO,
                                HOP_DONG_NGAY = model.ketQuaModel.HOP_DONG_NGAY,
                                GUID = model.ketQuaModel.GUID,
                                GIA_TRI_BAN = model.ketQuaModel.GIA_TRI_BAN,
                                HO_SO_GIAY_TO_KHAC = model.ketQuaModel.HO_SO_GIAY_TO_KHAC
                            };
                            _ketQuaService.InsertKetQua(ketqua);
                            _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", ketqua.ToModel<KetQuaModel>(), "KetQua");
                        }
                    }
                    //thuộc tính
                    if (model.listTT != null && model.listTT.Count() > 0)
                    {
                        //check vali
                        
                        var listvali = model.listTT.Where(c => c.IS_VALIDATE == true && (c.VALUE == null || c.VALUE == "") && c.LoaiDuLieuId != (int)enumLoaiDuLieuCauHinh.OBJ);
                        if (listvali != null && listvali.Count() > 0)
                        {
                            return JsonErrorMessage("Cập nhật không thành công", model);
                        }
                        var tt = _thuocTinhModelFactory.ToThuocTinhModel(listmodel: model.listTT.ToList()).FirstOrDefault();
                        if (tt != null)
                            model.ThuocTinh = tt.ToEntity<ThuocTinhEntity>().toStringJson();
                        //thuộc tính xử lý
                        var ttDaTa = _thuocTinhDataService.GetThuocTinhDataByTaiSanId(TaiSanTdXuLyId: (int)item.ID).FirstOrDefault();
                        if (ttDaTa != null)
                        {
                            ttDaTa.NGAY_CAP_NHAT = DateTime.Now;
                            ttDaTa.DATA = model.ThuocTinh;
                            _thuocTinhDataService.UpdateThuocTinhData(ttDaTa);
                            _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", ttDaTa.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                        }
                        else
                        {
                            ttDaTa = new ThuocTinhData();
                            ttDaTa.NGAY_TAO = DateTime.Now;
                            ttDaTa.NGAY_CAP_NHAT = DateTime.Now;
                            ttDaTa.TAI_SAN_TD_XU_LY_ID = item.ID;
                            ttDaTa.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                            ttDaTa.DON_VI_ID = _workContext.CurrentDonVi.ID;
                            ttDaTa.DATA = model.ThuocTinh;
                            _thuocTinhDataService.InsertThuocTinhData(ttDaTa);
                            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", ttDaTa.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                        }
                    }
                    // đồng bộ
                    _dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSanXacLap/UpdateKetQua", new List<TaiSanTdXuLyDongBoModel>() { item.ToModel<TaiSanTdXuLyDongBoModel>() }, _workContext.CurrentDonVi.ID, (int)enumLevelQueueProcessDB.MEDIUM);
                    return JsonSuccessMessage("Cập nhật thành công", model);
                }
                else
                {
                    return JsonErrorMessage("Cập nhật không thành công", model);
                }
            }
            else
            {
                var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
                return JsonErrorMessage("Cập nhật không thành công", list);
            }
        }
        public virtual IActionResult _ViewThongTinTaiSan(int TaiSanId, int SLTaiSanDaChon, Guid TSXLGuid, int LoaiXuLy, int SoLuongTrenFrom)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var model = new TaiSanTdModel();
            var tstd = _taiSanTdService.GetTaiSanTdById(TaiSanId);
            if (tstd != null)
            {
                model = _taiSanTdModelFactory.PrepareTaiSanTdModel(model, tstd, is_soluongcon: true, SLDaChon: SLTaiSanDaChon, TSXLGuid: TSXLGuid, LoaiXuLy: LoaiXuLy, SoLuongTrenFrom: SoLuongTrenFrom);
            }
            return PartialView(model);
        }
        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanTdXuLyModel(new TaiSanTdXuLyModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(TaiSanTdXuLyModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<TaiSanTdXuLy>();
                _itemService.InsertTaiSanTdXuLy(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareTaiSanTdXuLyModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();

            var item = _itemService.GetTaiSanTdXuLyById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanTdXuLyModel(null, item);
            return View(model);
        }
        public virtual IActionResult ThemRow(Guid Guid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();

            var item = _itemService.GetTaiSanTdXuLyByGuId(Guid);
            var soluongcon = _taiSanTdService.GetSoLuongConByTaiSanId(Id: item.TAI_SAN_ID, LoaiXuLy: (int)enumLoaiXuLy.PhuongAn, xulyid: item.XU_LY_ID);
            var newRow = new TaiSanTdXuLy()
            {
                TAI_SAN_ID = item.TAI_SAN_ID,
                XU_LY_ID = item.XU_LY_ID,
                SO_LUONG = soluongcon
            };
            _itemService.InsertTaiSanTdXuLy(newRow);
            var row = _itemService.GetTaiSanTdXuLyByGuId(newRow.GUID);
            string PAXLJson = _phuongAnXuLyModelFactory.DDLPhuongAn().toStringJson();
            var model = _itemModelFactory.PrepareTaiSanTdXuLyModel(null, row, false, PAXLJson);
            return Json(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(TaiSanTdXuLyModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanTdXuLyById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareTaiSanTdXuLy(model, item);
                _itemService.UpdateTaiSanTdXuLy(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareTaiSanTdXuLyModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(Guid Guid, Decimal chiphi, Decimal chiphitong, bool is_row)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            //xử lý chi phí tổng
            if (chiphi > 0 && chiphitong > 0)
            {
                chiphitong -= chiphi;
            }
            var item = _itemService.GetTaiSanTdXuLyByGuId(Guid);
            if (item == null && is_row == false)
                return JsonErrorMessage("Cập nhật không thành công", Guid);
            else if (item == null && is_row == true)
            {
                return JsonSuccessMessage("Cập nhật thành công", chiphitong);
            }
            try
            {
                //xóa data thuoctinh
                var thuoctinh = _thuocTinhDataService.GetThuocTinhDataByTaiSanId(TaiSanTdXuLyId: (int)item.ID).FirstOrDefault();
                if (thuoctinh != null)
                {
                    _thuocTinhDataService.DeleteThuocTinhData(thuoctinh);
                    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", thuoctinh.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                }
                _itemService.DeleteTaiSanTdXuLy(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                //// quyết định xử lý

                //    var checkxl = _itemService.GetTaiSanTdsXuLyByXuLyId(item.XU_LY_ID);//khi xóa hết phương án xử lý thì mới xóa xử lý
                //    if (checkxl.Count() == 0)
                //    {
                //        var xltd = _xuLyService.GetXuLyById(item.XU_LY_ID);
                //        if (xltd != null)
                //        {
                //            _xuLyService.DeleteXuLy(xltd);
                //        }
                //    }

                //activity log  
                return JsonSuccessMessage("Cập nhật thành công", chiphitong);
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Cập nhật không thành công", exc);
            }
        }
        [HttpPost]
        public virtual IActionResult SaveRow(TaiSanTdXuLyModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            var item = _itemService.GetTaiSanTdXuLyById(model.ID);
            if (item == null)
                return JsonErrorMessage("Cập nhật không thành công", model.ID);
            try
            {
                decimal soluong = 0;
                if(model.SoLuongText!= null)
                {
                    model.SoLuongText = model.SoLuongText.Replace(",", null).Replace('.', ',');
                    decimal.TryParse(model.SoLuongText.ToVNStringNumber(true), out soluong);
                }
                if (model.PHUONG_AN_XU_LY_ID != null && model.PHUONG_AN_XU_LY_ID > 0)
                    item.PHUONG_AN_XU_LY_ID = model.PHUONG_AN_XU_LY_ID;
                else if (model.HINH_THUC_XU_LY_ID != null && model.HINH_THUC_XU_LY_ID > 0)
                    item.HINH_THUC_XU_LY_ID = model.HINH_THUC_XU_LY_ID;
                else if(model.SoLuongText != null && soluong >= 0) {
                    // check xem số lượng còn với số lượng nhập thêm có bị vượt qá
                    var soluongcon = _taiSanTdService.GetSoLuongConByTaiSanId(Id: item.TAI_SAN_ID, LoaiXuLy: (int)enumLoaiXuLy.PhuongAn,xulyid:item.XU_LY_ID);
                    if((soluongcon+item.SO_LUONG- soluong) < 0)
                    {
                        return JsonErrorMessage("Cập nhật không thành công", model.ID);
                    }
                    item.SO_LUONG = soluong;
                }
                else
                {
                    return JsonErrorMessage("Cập nhật không thành công", model.ID);
                }
                _itemService.UpdateTaiSanTdXuLy(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                //đồng bộ, nếu phương án xử lý đang ở trạng thái tồn tại thì mới đồng bộ
                var xl = _xuLyService.GetXuLyById(item.XU_LY_ID);
                if(xl!=null && xl.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai)
                {
                    _dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSanXacLap/UpdateTaiSanXuLy", new List<TaiSanTdXuLyDongBoModel>() { item.ToModel<TaiSanTdXuLyDongBoModel>() }, _workContext.CurrentDonVi.ID);
                }
                return JsonSuccessMessage("Cập nhật thành công",_taiSanTdService.GetSoLuongConByTaiSanId(Id: item.TAI_SAN_ID, LoaiXuLy: (int)enumLoaiXuLy.PhuongAn, xulyid: item.XU_LY_ID));
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Cập nhật không thành công", exc);
            }
        }
        public virtual IActionResult ThemTaiSanTdXuLy(Guid guid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            var item = _itemService.GetTaiSanTdXuLyByGuId(guid);
            if (item == null)
                return JsonErrorMessage("Cập nhật không thành công", guid);
            try
            {
                var soluongcon = _taiSanTdService.GetSoLuongConByTaiSanId(Id: item.TAI_SAN_ID, LoaiXuLy: (int)enumLoaiXuLy.PhuongAn, xulyid: item.XU_LY_ID);
                if (soluongcon < 1)
                    return JsonErrorMessage("Cập nhật không thành công", guid);
                else
                {
                    var newRow = new TaiSanTdXuLy()
                    {
                        TAI_SAN_ID = item.TAI_SAN_ID,
                        XU_LY_ID = item.XU_LY_ID,
                        SO_LUONG = soluongcon
                    };
                    _itemService.InsertTaiSanTdXuLy(newRow);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", newRow.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                    return JsonSuccessMessage("Cập nhật thành công");
                }
              
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Cập nhật không thành công", exc);
            }
        }
        public virtual IActionResult XoaXuLyTaiSan(Guid Guid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id          
            var item = _itemService.GetTaiSanTdXuLyByGuId(Guid);
            if(item == null)
            return JsonErrorMessage("Cập nhật không thành công", Guid);
            try
            {
                //xóa data thuoctinh
                var thuoctinh = _thuocTinhDataService.GetThuocTinhDataByTaiSanId(TaiSanTdXuLyId: (int)item.ID).FirstOrDefault();
                if (thuoctinh != null)
                {
                    _thuocTinhDataService.DeleteThuocTinhData(thuoctinh);
                    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", thuoctinh.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                }
                _itemService.DeleteTaiSanTdXuLy(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                return JsonSuccessMessage("Cập nhật thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Cập nhật không thành công", exc);
            }
        }
        public virtual IActionResult CheckSoLuongCon(Guid guid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            var item = _itemService.GetTaiSanTdXuLyByGuId(guid);
            if (item == null)
                return JsonErrorMessage("Cập nhật không thành công", guid);
            var soluongcon = _taiSanTdService.GetSoLuongConByTaiSanId(Id: item.TAI_SAN_ID, LoaiXuLy: (int)enumLoaiXuLy.PhuongAn, xulyid: item.XU_LY_ID);
            if (soluongcon < 1)
                return JsonErrorMessage("Cập nhật không thành công", guid);
            else
            {
                return JsonSuccessMessage("Cập nhật thành công");
            }
        }
        [HttpPost]
        public virtual IActionResult DeleteKetQua(Guid Guid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var xl = _xuLyService.GetXuLyByGuid(Guid);
            if (xl == null)
                return JsonErrorMessage("Cập nhật không thành công", Guid);
            var listTSXL = _itemService.GetTaiSanTdsXuLyByXuLyId(XuLyId:xl.ID).ToList();
            if(listTSXL != null && listTSXL.Count() > 0)
            {
                var ketqua = _ketQuaService.GetKetQuaBys(ListTaiSanTDXuLy: listTSXL.Select(c => c.ID).ToList());
                if (ketqua != null)
                {
                    try
                    {
                        foreach(var tsxl in listTSXL)
                        {
                            //xóa data thuoctinh
                            var thuoctinh = _thuocTinhDataService.GetThuocTinhDataByTaiSanId(TaiSanTdXuLyId: (int)tsxl.ID).FirstOrDefault();
                            if (thuoctinh != null)
                            {
                                _thuocTinhDataService.DeleteThuocTinhData(thuoctinh);
                                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", thuoctinh.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                            }
                        }
                        // xóa kết quả
                        foreach (var kq in ketqua)
                        {
                            _ketQuaService.DeleteKetQua(kq);
                            _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", kq.ToModel<KetQuaModel>(), "KetQua");
                        }
                        return JsonSuccessMessage("Cập nhật thành công", Guid);
                    }
                    catch (Exception exc)
                    {
                        ErrorNotification(exc);
                        return JsonErrorMessage("Cập nhật không thành công", exc);
                    }
                }
                else
                {
                    return JsonErrorMessage("Cập nhật không thành công", Guid);
                }
            }
            else
            {
                return JsonErrorMessage("Cập nhật không thành công", Guid);
            }
        }
        [HttpPost]
        public virtual IActionResult DeleteKetQuaTSXL(Guid Guid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanTdXuLyByGuId(Guid);
            if (item == null)
                return JsonErrorMessage("Cập nhật không thành công", Guid);
            var ketqua = _ketQuaService.GetKetQuaBys(TaiSanTDXuLy: item.ID);
            if(ketqua == null)
                return JsonErrorMessage("Cập nhật không thành công", Guid);
            try
            {
                //xóa data thuoctinh
                var thuoctinh = _thuocTinhDataService.GetThuocTinhDataByTaiSanId(TaiSanTdXuLyId: (int)item.ID).FirstOrDefault();
                if (thuoctinh != null)
                {
                    _thuocTinhDataService.DeleteThuocTinhData(thuoctinh);
                    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", thuoctinh.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                }
                // xóa kết quả
                foreach(var kq in ketqua)
                {
                    _ketQuaService.DeleteKetQua(kq);
                    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", kq.ToModel<KetQuaModel>(), "KetQua");
                }
                return JsonSuccessMessage("Cập nhật thành công", Guid);
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Cập nhật không thành công", exc);
            }
        }
        public virtual IActionResult _ChonDonViDieuChuyen()
        {
            var model = new DonViSearchModel();
            model.nguoiDungId = _workContext.CurrentCustomer.ID;
            model = _donViModelFactory.PrepareDonViSearchModel(model);
            return PartialView(model);
        }
        #endregion
    }
}

