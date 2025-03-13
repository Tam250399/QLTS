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
using System.Collections;
using GS.Web.Models.ThuocTinh;
using System.Collections.Generic;
using System.Linq;
using GS.Core.Domain.ThuocTinhs;
using GS.Web.Factories.ThuocTinhs;
using GS.Services.ThuocTinhs;
using GS.Core.Domain.DB;
using GS.Services.DB;
using GS.Web.Factories.DB;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class XuLyController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly IXuLyModelFactory _itemModelFactory;
            private readonly IXuLyService _itemService;
            private readonly IThuocTinhModelFactory _thuocTinhModelFactory;
            private readonly IThuocTinhDataModelFactory _thuocTinhDataModelFactory;
            private readonly IThuocTinhDataService _thuocTinhDataService;
            private readonly IXuLyService _xuLyService;
            private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;
            private readonly ITaiSanTdXuLyModelFactory _taiSanTdXuLyModelFactory;
            private readonly ITaiSanTdService _taiSanTdService;
            private readonly IKetQuaService _ketQuaService;
            private readonly ITaiSanNhatKyService _taiSanNhatKyService;
            private readonly IDB_QueueProcessModelFactory _dB_QueueProcessModelFactory;
        #endregion

        #region Ctor
        public XuLyController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IXuLyModelFactory itemModelFactory,
            IXuLyService itemService,
            IThuocTinhModelFactory thuocTinhModelFactory,
            IThuocTinhDataModelFactory thuocTinhDataModelFactory,
            IXuLyService xuLyService,
            IThuocTinhDataService thuocTinhDataService,
            ITaiSanTdXuLyService taiSanTdXuLyService,
            ITaiSanTdXuLyModelFactory taiSanTdXuLyModelFactory,
            ITaiSanTdService taiSanTdService,
            IKetQuaService ketQuaService,
            ITaiSanNhatKyService taiSanNhatKyService,
            IDB_QueueProcessModelFactory dB_QueueProcessModelFactory
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
            this._xuLyService = xuLyService;
            this._thuocTinhDataService = thuocTinhDataService;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
            this._taiSanTdXuLyModelFactory = taiSanTdXuLyModelFactory;
            this._taiSanTdService = taiSanTdService;
            this._ketQuaService = ketQuaService;
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._dB_QueueProcessModelFactory = dB_QueueProcessModelFactory;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new XuLySearchModel ();
            var model = _itemModelFactory.PrepareXuLySearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(XuLySearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareXuLyListModel(searchModel);
            return Json(model);
        }
        public virtual IActionResult ListPhuongAn()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new XuLySearchModel();
            var model = _itemModelFactory.PrepareXuLySearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult ListPhuongAn(XuLySearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedKendoGridJson();
            //prepare model
            searchModel.LOAI_XU_LY_ID = (int)enumLoaiXuLy.PhuongAn;
            var model = _itemModelFactory.PrepareXuLyListModel(searchModel);
            return Json(model);
        }
        public virtual IActionResult ListDeXuat()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new XuLySearchModel() {LOAI_XU_LY_ID= (int)enumLoaiXuLy.DeXuat };
            var model = _itemModelFactory.PrepareXuLySearchModel(searchmodel);
            return View(model);
        }
        public virtual IActionResult _ListCopy()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new XuLySearchModel() { LOAI_XU_LY_ID = (int)enumLoaiXuLy.DeXuat };
            var model = _itemModelFactory.PrepareXuLySearchModel(searchmodel);
            return PartialView(model);
        }
        public virtual IActionResult ListKetQua()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new XuLySearchModel() { LOAI_XU_LY_ID = (int)enumLoaiXuLy.KetQua };
            var model = _itemModelFactory.PrepareXuLySearchModel(searchmodel);
            return View(model);
        }
        public virtual IActionResult CreateKetQua()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var model = new XuLyModel();
            model.DDLPhuongAnXuLyTS = _itemModelFactory.PrepareDDLPhuongAnXuLyTaiSan(true, 0);
            return View(model);
        }
        public virtual IActionResult EditKetQua(Guid Guid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var item = _itemService.GetXuLyByGuid(Guid);
            var model = new XuLyModel();
            model.PhuongAnXuLyTsID = item.ID;
            model.DDLPhuongAnXuLyTS = _itemModelFactory.PrepareDDLPhuongAnXuLyTaiSan(true, (int)model.PhuongAnXuLyTsID);
            return View(model);
        }
        public virtual IActionResult Create(int LoaiXuLy)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var item = new XuLy();
            item.DON_VI_ID = _workContext.CurrentDonVi.ID;
            //if (LoaiXuLy == (int)enumLoaiXuLy.PhuongAn)
            //{
            //    item.TRANG_THAI_ID = (int)enumTrangThaiXuLy.Nhap;
            //    //item.LOAI_XU_LY_ID = (int)enumLoaiXuLy.PhuongAn;
            //}
            //else
            //{
            //    item.TRANG_THAI_ID = (int)enumTrangThaiXuLy.TonTai;
            //    //item.LOAI_XU_LY_ID = (int)enumLoaiXuLy.KetQua;
            //}
            item.TRANG_THAI_ID = (int)enumTrangThaiXuLy.Nhap;
            _itemService.InsertXuLy(item);
            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<XuLyModel>(), "XuLy");
            //var model = _itemModelFactory.PrepareXuLyModel(new XuLyModel() {LOAI_XU_LY_ID=LoaiXuLy}, item, false,true);
            var model = _itemModelFactory.PrepareXuLyModel(new XuLyModel() {}, item, false, true);
            return View(model);
        }
        public virtual IActionResult _ViewXuLyTaiSan()
        {
            //prepare model
            var model = _itemModelFactory.PrepareXuLyModel(new XuLyModel(), null);
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult Create(XuLyModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();

            var item = _itemService.GetXuLyById(model.ID);
            if (item == null)
                return JsonErrorMessage("Cập không nhật thành công", model.ID);
            if (ModelState.IsValid)
            {
                if (_taiSanTdXuLyService.CheckTaiSanXuLy(item.ID).Count() > 0)
                {
                    return JsonErrorMessage("Chưa cập nhật hết tài sản", false);
                };
                if (_taiSanTdXuLyService.CheckDaTaoTSXL(XuLyId: item.ID).Count() == 0)
                {
                    return JsonErrorMessage("Chưa thêm phương án xử lý cho từng tài sản", false);
                }
                _itemModelFactory.PrepareXuLy(model, item);
                item.TRANG_THAI_ID = (int)enumTrangThaiXuLy.TonTai;
                _itemService.UpdateXuLy(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<XuLyModel>(), "XuLy");
                return JsonSuccessMessage("Cập nhật thành công", model);
            }
            //prepare model
            else
            {
                var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
                return JsonErrorMessage("Cập không nhật thành công", list);
            }
        }
        public virtual IActionResult Edit(Guid Guid, bool Is_Copy)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();

            var item = _itemService.GetXuLyByGuid(Guid);
            if (item == null)
                return RedirectToAction("ListPhuongAn");
            //prepare model           
            var model = _itemModelFactory.PrepareXuLyModel(new XuLyModel(), item,true,true);
            return View(model);
        }
        public virtual IActionResult _ThongTinChiTiet(Decimal XuLyID)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();

            var item = _itemService.GetXuLyById(XuLyID);
            if (item == null)
                return RedirectToAction("ListKetQua");
            //prepare model           
            var model = _itemModelFactory.PrepareXuLyModel(new XuLyModel(), item, true, true);
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult Edit(XuLyModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetXuLyById(model.ID);
            if (item == null)
                return JsonErrorMessage("Cập không nhật thành công", model.ID);
            if (ModelState.IsValid)
            {
                if (_taiSanTdXuLyService.CheckTaiSanXuLy(item.ID).Count() > 0)
                {
                    return JsonErrorMessage("Chưa cập nhật hết tài sản", false);
                };
                if (_taiSanTdXuLyService.CheckDaTaoTSXL(XuLyId: item.ID).Count() == 0)
                {
                    return JsonErrorMessage("Chưa thêm phương án xử lý cho từng tài sản", false);
                };
                //xóa hết những tsxl có sl = 0
                var ListTSXLZero = _taiSanTdXuLyService.GetTaiSanTdsXuLyByXuLyId(item.ID, (decimal)item.TRANG_THAI_ID, 0);
                if (ListTSXLZero.Count() > 0)
                {
                   foreach(var TSXLZero in ListTSXLZero)
                    {
                        _taiSanTdXuLyService.DeleteTaiSanTdXuLy(TSXLZero);
                        _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa thông tin ", TSXLZero.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                    }
                }
                _itemModelFactory.PrepareXuLy(model, item);
                item.TRANG_THAI_ID = (int)enumTrangThaiXuLy.TonTai;
                _itemService.UpdateXuLy(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<XuLyModel>(), "XuLy");
                //nhật ký đồng bộ                
                //đồng bộ xử lý sang kho
                _dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSanXacLap/UpdateXuLy", new List<XuLy>() { item }, _workContext.CurrentDonVi.ID, (int)enumLevelQueueProcessDB.HIGH);
                //đồng bộ tài sản xử lý sang kho
                var listTSTDXL = _taiSanTdXuLyService.GetTaiSanTdsXuLyByXuLyId(XuLyId: item.ID);
                if(listTSTDXL.Count()>0)
                _dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSanXacLap/UpdateTaiSanXuLy", listTSTDXL.Select(c=>c.ToModel<TaiSanTdXuLyDongBoModel>()), _workContext.CurrentDonVi.ID);               
                return JsonSuccessMessage("Cập nhật thành công", model);
            }
            //prepare model
            else
            {
                var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
                return JsonErrorMessage("Cập không nhật thành công", list);
            }
        }
        [HttpPost]
        public virtual IActionResult Delete(Guid Guid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetXuLyByGuid(Guid);
            if (item == null)
                return JsonSuccessMessage("Cập nhật thành công", Guid);
            try
            {
                var listtsxl = _taiSanTdXuLyService.GetTaiSanTdsXuLyByXuLyId(XuLyId: (int)item.ID);
                if (listtsxl != null && listtsxl.Count() > 0)
                {
                    foreach (var tsxl in listtsxl)
                    {
                        //xóa data thuoctinh
                        var thuoctinh = _thuocTinhDataService.GetThuocTinhDataByTaiSanId(TaiSanTdXuLyId: (int)tsxl.ID).FirstOrDefault();
                        if (thuoctinh != null)
                        {
                            _thuocTinhDataService.DeleteThuocTinhData(thuoctinh);
                            _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", thuoctinh.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                        }
                        _taiSanTdXuLyService.DeleteTaiSanTdXuLy(tsxl);
                        _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", tsxl.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");

                    }
                }
                _itemService.DeleteXuLy(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<XuLyModel>(), "XuLy");
                //activity log  
                return JsonSuccessMessage("Cập nhật thành công", item);
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonSuccessMessage("Cập nhật thành công", exc);
            }
        }
        [HttpPost]
        public virtual IActionResult DeleteKetQua(Guid Guid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetXuLyByGuid(Guid);
            if (item == null)
                return JsonSuccessMessage("Cập nhật thành công", Guid);
            try
            {
                var listtsxl = _taiSanTdXuLyService.GetTaiSanTdsXuLyByXuLyId(XuLyId: (int)item.ID);
                if (listtsxl != null && listtsxl.Count() > 0)
                {
                    foreach (var tsxl in listtsxl)
                    {
                        //xóa data thuoctinh
                        var thuoctinh = _thuocTinhDataService.GetThuocTinhDataByTaiSanId(TaiSanTdXuLyId: (int)tsxl.ID).FirstOrDefault();
                        if (thuoctinh != null)
                        {
                            _thuocTinhDataService.DeleteThuocTinhData(thuoctinh);
                            _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", thuoctinh.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                        }
                        var ketqua = _ketQuaService.GetKetQuaBys(TaiSanTDXuLy: tsxl.ID);
                        foreach (var kq in ketqua)
                        {
                            _ketQuaService.DeleteKetQua(kq);
                            _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", kq.ToModel<KetQuaModel>(), "KetQua");
                        }
                    }
                }
                //activity log  
                return JsonSuccessMessage("Cập nhật thành công", item);
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonSuccessMessage("Cập nhật thành công", exc);
            }
        }
        [HttpPost]
        public virtual IActionResult _XuLyRowTaiSan(TaiSanTdXuLyModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            if (ModelState.IsValid)
            {
                //chi phí tổng
                model.ChiPhiTong = model.ChiPhiTong ?? 0;
                //model.ChiPhiTong += ((decimal)model.CHI_PHI_XU_LY- (decimal)model.ChiPhiCu);
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
                }
                model = _taiSanTdXuLyModelFactory.PrepareTaiSanTdXuLyModel(model, null);
                return JsonSuccessMessage("Cập nhật thành công", model);
            }
            else
            {
                var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
                return JsonErrorMessage("Cập nhật không thành công", list);
            }

        }
        [HttpPost]
        public virtual IActionResult _RowTaiSanXuLy(TaiSanTdXuLyModel item)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var model = new TaiSanTdXuLyModel();
            var sl = item.ListTaiSanId.Count();
            if (sl > 0 && item.TAI_SAN_ID == 0)
            {
                foreach (var taisanid in item.ListTaiSanId)
                {
                    var soluongdachon = item.ListSL.Where(c => c.TAI_SAN_ID == taisanid).Select(c => c.SO_LUONG).FirstOrDefault();
                    var tstd = _taiSanTdService.GetTaiSanTdById(taisanid);
                    var row = new TaiSanTdXuLyModel();
                    row.GUID = Guid.NewGuid();
                    row.TAI_SAN_ID = taisanid;
                    row.TenTaiSan = tstd.TEN;
                    //row.DIEN_TICH = tstd.DIEN_TICH;
                    //row.NGUYEN_GIA = tstd.NGUYEN_GIA;
                    //row.GIA_TRI = tstd.GIA_TRI;
                    //row.KHOI_LUONG = tstd.KHOI_LUONG;
                    //row.CHI_PHI_XU_LY = item.CHI_PHI_XU_LY / sl;
                    //row.DON_VI_CHUYEN_ID = item.DON_VI_CHUYEN_ID;
                    //.GIA_TRI_TAI_SAN_XU_LY = item.GIA_TRI_TAI_SAN_XU_LY;
                    //row.TEN_DON_VI_NHAN_DIEU_CHUYEN = item.TEN_DON_VI_NHAN_DIEU_CHUYEN;
                    //row.NGAY_XU_LY = item.NGAY_XU_LY;
                    //row.GIA_TRI_GHI_TANG = item.GIA_TRI_GHI_TANG;
                    //row.GIA_TRI_NSNN = item.GIA_TRI_NSNN;
                    //row.GIA_TRI_TKTG = item.GIA_TRI_TKTG;
                    //row.HINH_THUC_XU_LY_ID = item.HINH_THUC_XU_LY_ID;
                    //row.HOP_DONG_NGAY = item.HOP_DONG_NGAY;
                    //row.HOP_DONG_SO = item.HOP_DONG_SO;
                    row.PHUONG_AN_XU_LY_ID = item.PHUONG_AN_XU_LY_ID;
                    row.XU_LY_ID = item.XU_LY_ID;
                    row.ThuocTinh = item.ThuocTinh;
                    row.GHI_CHU = item.GHI_CHU;
                    //row.HO_SO_GIAY_TO_KHAC = item.HO_SO_GIAY_TO_KHAC;
                    row.TenPhuongAn = item.TenPhuongAn;
                    row.TenHinhThuc = item.TenHinhThuc;
                    row.SO_LUONG = tstd.SO_LUONG - (item.LoaiXuLy==(int)enumLoaiXuLy.KetQua?(_taiSanTdXuLyService.GetTaiSanTdXuLys(TaiSanId: taisanid, LoaiXuLy: (int)enumLoaiXuLy.KetQua).Select(c => (int)c.SO_LUONG).Sum()):0)- soluongdachon;
                    model.ListModel.Add(row);
                }
            }
            else
            {
                model.ListModel.Add(item);
            }
            return PartialView(model);
        }
        public virtual IActionResult Copy(Guid Guid)
        {
            var copy = _itemService.GetXuLyByGuid(Guid);
            if (copy != null)
            {
                var item = new XuLy();
                item.QUYET_DINH_SO = copy.QUYET_DINH_SO;
                item.QUYET_DINH_NGAY = copy.QUYET_DINH_NGAY;
                //item.HINH_THUC = copy.HINH_THUC;
                //item.CHI_PHI = copy.CHI_PHI;
                item.GHI_CHU = copy.GHI_CHU;
                item.GUID = Guid.NewGuid();
                item.NGAY_TAO = DateTime.Now;
                item.DON_VI_ID = _workContext.CurrentDonVi.ID;
                //item.LOAI_XU_LY_ID = (int)enumLoaiXuLy.KetQua;
                _itemService.InsertXuLy(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<XuLyModel>(), "XuLy");

                //tài sản xử lý
                var listtsxl = _taiSanTdXuLyService.GetTaiSanTdsXuLyByXuLyId(XuLyId: (int)copy.ID);
                if (listtsxl != null && listtsxl.Count() > 0)
                {
                    foreach (var copytsxl in listtsxl)
                    {
                        var tsxl = new TaiSanTdXuLy();
                        tsxl.TAI_SAN_ID = copytsxl.TAI_SAN_ID;
                        tsxl.SO_LUONG = copytsxl.SO_LUONG;
                        //tsxl.DIEN_TICH = copytsxl.DIEN_TICH;
                        //tsxl.NGUYEN_GIA = copytsxl.NGUYEN_GIA;
                        //tsxl.GIA_TRI = copytsxl.GIA_TRI;
                        //tsxl.GIA_TRI_GHI_TANG = copytsxl.GIA_TRI_GHI_TANG;
                        //tsxl.GIA_TRI_NSNN = copytsxl.GIA_TRI_NSNN;
                        //tsxl.GIA_TRI_TKTG = copytsxl.GIA_TRI_TKTG;
                        tsxl.PHUONG_AN_XU_LY_ID = copytsxl.PHUONG_AN_XU_LY_ID;
                        tsxl.HINH_THUC_XU_LY_ID = copytsxl.HINH_THUC_XU_LY_ID;
                        //tsxl.CHI_PHI_XU_LY = copytsxl.CHI_PHI_XU_LY;
                        //tsxl.HOP_DONG_SO = copytsxl.HOP_DONG_SO;
                        //tsxl.HOP_DONG_NGAY = copytsxl.HOP_DONG_NGAY;
                        tsxl.GHI_CHU = copytsxl.GHI_CHU;
                        //tsxl.DON_VI_CHUYEN_ID = copytsxl.DON_VI_CHUYEN_ID;
                        //tsxl.GIA_TRI_TAI_SAN_XU_LY = copytsxl.GIA_TRI_TAI_SAN_XU_LY;
                        //tsxl.TEN_DON_VI_NHAN_DIEU_CHUYEN = copytsxl.TEN_DON_VI_NHAN_DIEU_CHUYEN;
                        //tsxl.SO_TIEN_THU = copytsxl.SO_TIEN_THU;
                        //tsxl.HO_SO_GIAY_TO_KHAC = copytsxl.HO_SO_GIAY_TO_KHAC;
                        tsxl.GUID = Guid.NewGuid();
                        tsxl.XU_LY_ID = item.ID;
                        _taiSanTdXuLyService.InsertTaiSanTdXuLy(tsxl);
                        _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", tsxl.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                    }
                }
                return JsonSuccessMessage("Cập nhật thành công", Guid);
            }
            return JsonErrorMessage("Cập không nhật thành công", Guid);
        }
        public virtual IActionResult ViewThuocTinhByHinhThuc(int PhuongAnXuLyId = 0, int TaiSanTdId = 0, Guid GuidView = new Guid(), Guid TaiSanXuLyGuid = new Guid())
        {
            var model = _itemModelFactory.PrepareListmodelThuocTinh(GUID: GuidView, PhuongAnXuLyId: PhuongAnXuLyId, TaiSanTdId: TaiSanTdId, TaiSanXuLyGuid: TaiSanXuLyGuid);
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult Save(XuLyModel model)
        {

            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            if (ModelState.IsValid)
            {
                //model.LOAI_XU_LY_ID = model.Is_Copy == true ? (int)enumLoaiXuLy.KetQua : model.LOAI_XU_LY_ID;
                //check xem tài sản xử lý tồn tại có số lượng max chưa khi lưu kết quả xử lý
                //if (model.LOAI_XU_LY_ID == (int)enumLoaiXuLy.KetQua)
                //{
                //    foreach (var tstdxl in model.listTaiSanXuLyModel)
                //    {
                //        var tstd = _taiSanTdService.GetTaiSanTdById(tstdxl.TAI_SAN_ID);
                //        if (tstd != null && model.LOAI_XU_LY_ID == (int)enumLoaiXuLy.KetQua)
                //        {
                //            var itemTSTDXL = _taiSanTdXuLyService.GetTaiSanTdXuLyByGuId(Guid: (Guid)tstdxl.GUID);
                //            //var soluongdachon = _taiSanTdXuLyService.GetTaiSanTdXuLysByTaiSanId((int)tstdxl.TAI_SAN_ID).Where(c => c.xuly.LOAI_XU_LY_ID == (int)enumLoaiXuLy.KetQua).Select(c => (decimal)c.SO_LUONG).Sum();
                //            var soluongdachon = _taiSanTdXuLyService.GetTaiSanTdXuLysByTaiSanId((int)tstdxl.TAI_SAN_ID).Where(c => c.xuly.TRANG_THAI_ID == (int)enumTrangThaiXuLy.TonTai).Select(c => (decimal)c.SO_LUONG).Sum();
                //            if (soluongdachon >= tstd.SO_LUONG && itemTSTDXL == null)
                //            {
                //                return JsonErrorMessage("Số lượng tài sản vượt quá cho phép", model.ID);
                //            }
                //            else if (itemTSTDXL != null && (soluongdachon - itemTSTDXL.SO_LUONG + tstdxl.SO_LUONG) > tstd.SO_LUONG)
                //            {
                //                return JsonErrorMessage("Số lượng tài sản vượt quá cho phép", model.ID);
                //            }
                //        }
                //    }
                //}
                //xử lý
                var item = _itemService.GetXuLyByGuid((Guid)model.GUID);
                if (item != null && model.Is_Copy == false) //edit
                {
                    item.QUYET_DINH_SO = model.QUYET_DINH_SO;
                    item.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY;
                    //item.HINH_THUC = model.HINH_THUC;
                    //item.CHI_PHI = model.CHI_PHI;
                    item.GHI_CHU = model.GHI_CHU;
                    item.CO_QUAN_BAN_HANH_ID = model.CO_QUAN_BAN_HANH_ID;
                    item.TRANG_THAI_ID = model.TRANG_THAI_ID;
                    _itemService.UpdateXuLy(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<XuLyModel>(), "XuLy");
                }
                else //insert
                {
                    item = new XuLy();
                    item = model.ToEntity<XuLy>();
                    item.NGAY_TAO = DateTime.Now;
                    item.GUID = Guid.NewGuid();
                    item.DON_VI_ID = _workContext.CurrentDonVi.ID;
                    _itemService.InsertXuLy(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<XuLyModel>(), "XuLy");
                }
                foreach (var tstdxl in model.listTaiSanXuLyModel)
                {
                    //tài sản xử lý                  
                    var itemTSTDXL = _taiSanTdXuLyService.GetTaiSanTdXuLyByGuId(Guid: (Guid)tstdxl.GUID);
                    var tstd = _taiSanTdService.GetTaiSanTdById(tstdxl.TAI_SAN_ID);
                    //var nguyengiaTSTD = (tstd.NGUYEN_GIA ?? 0) / tstd.SO_LUONG;
                    //var giatriTSTD = (tstd.GIA_TRI ?? 0) / tstd.SO_LUONG;
                    //var dientichTSTD = (tstd.DIEN_TICH ?? 0) / tstd.SO_LUONG;
                    //var khoiluongTSTD = (tstd.KHOI_LUONG ?? 0) / tstd.SO_LUONG;
                    if (itemTSTDXL != null && model.Is_Copy == false) // update
                    {
                        //itemTSTDXL.HOP_DONG_NGAY = tstdxl.HOP_DONG_NGAY;
                        //itemTSTDXL.HOP_DONG_SO = tstdxl.HOP_DONG_SO;
                        //itemTSTDXL.GHI_CHU = tstdxl.GHI_CHU;
                        //itemTSTDXL.SO_LUONG = tstdxl.SO_LUONG;
                        //itemTSTDXL.DIEN_TICH = Decimal.Round((Decimal)(tstdxl.SO_LUONG * dientichTSTD), 4);
                        //itemTSTDXL.NGUYEN_GIA = Decimal.Round((Decimal)(tstdxl.SO_LUONG * nguyengiaTSTD), 4);
                        //itemTSTDXL.GIA_TRI = Decimal.Round((Decimal)(tstdxl.SO_LUONG * giatriTSTD), 4);
                        //itemTSTDXL.KHOI_LUONG = Decimal.Round((Decimal)(tstdxl.SO_LUONG * khoiluongTSTD), 4);
                        //itemTSTDXL.GIA_TRI_GHI_TANG = tstdxl.GIA_TRI_GHI_TANG;
                        //itemTSTDXL.GIA_TRI_NSNN = tstdxl.GIA_TRI_NSNN;
                        //itemTSTDXL.GIA_TRI_TKTG = tstdxl.GIA_TRI_TKTG;
                        //itemTSTDXL.CHI_PHI_XU_LY = tstdxl.CHI_PHI_XU_LY;
                        //itemTSTDXL.PHUONG_AN_XU_LY_ID = tstdxl.PHUONG_AN_XU_LY_ID > 0 ? tstdxl.PHUONG_AN_XU_LY_ID : null;
                        //itemTSTDXL.HINH_THUC_XU_LY_ID = tstdxl.HINH_THUC_XU_LY_ID > 0 ? tstdxl.HINH_THUC_XU_LY_ID : null;
                        //itemTSTDXL.SO_TIEN_THU = tstdxl.SO_TIEN_THU;
                        //itemTSTDXL.HO_SO_GIAY_TO_KHAC = tstdxl.HO_SO_GIAY_TO_KHAC;
                        //itemTSTDXL.DON_VI_CHUYEN_ID = tstdxl.DON_VI_CHUYEN_ID;
                        //itemTSTDXL.GIA_TRI_TAI_SAN_XU_LY = tstdxl.GIA_TRI_TAI_SAN_XU_LY;
                        //itemTSTDXL.TEN_DON_VI_NHAN_DIEU_CHUYEN = tstdxl.TEN_DON_VI_NHAN_DIEU_CHUYEN;
                        //itemTSTDXL.NGAY_XU_LY = tstdxl.NGAY_XU_LY;
                        _taiSanTdXuLyService.UpdateTaiSanTdXuLy(itemTSTDXL);
                        _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", itemTSTDXL.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                    }
                    else //insert
                    {
                        itemTSTDXL = new TaiSanTdXuLy();
                        //itemTSTDXL.GUID = tstdxl.GUID;
                        itemTSTDXL.TAI_SAN_ID = tstdxl.TAI_SAN_ID;
                        itemTSTDXL.XU_LY_ID = item.ID;
                        itemTSTDXL.SO_LUONG = tstdxl.SO_LUONG;
                        //itemTSTDXL.DIEN_TICH = Decimal.Round((Decimal)(tstdxl.SO_LUONG * dientichTSTD), 4);
                        //itemTSTDXL.NGUYEN_GIA = Decimal.Round((Decimal)(tstdxl.SO_LUONG * nguyengiaTSTD), 4);
                        //itemTSTDXL.GIA_TRI = Decimal.Round((Decimal)(tstdxl.SO_LUONG * giatriTSTD), 4);
                        //itemTSTDXL.KHOI_LUONG = Decimal.Round((Decimal)(tstdxl.SO_LUONG * khoiluongTSTD), 4);
                        //itemTSTDXL.GIA_TRI_GHI_TANG = tstdxl.GIA_TRI_GHI_TANG;
                        //itemTSTDXL.GIA_TRI_NSNN = tstdxl.GIA_TRI_NSNN;
                        //itemTSTDXL.GIA_TRI_TKTG = tstdxl.GIA_TRI_TKTG;
                        //itemTSTDXL.HOP_DONG_NGAY = tstdxl.HOP_DONG_NGAY;
                        //itemTSTDXL.HOP_DONG_SO = tstdxl.HOP_DONG_SO;
                        //itemTSTDXL.GHI_CHU = tstdxl.GHI_CHU;
                        //itemTSTDXL.CHI_PHI_XU_LY = tstdxl.CHI_PHI_XU_LY;
                        //itemTSTDXL.PHUONG_AN_XU_LY_ID = tstdxl.PHUONG_AN_XU_LY_ID > 0 ? tstdxl.PHUONG_AN_XU_LY_ID : null;
                        //itemTSTDXL.HINH_THUC_XU_LY_ID = tstdxl.HINH_THUC_XU_LY_ID > 0 ? tstdxl.HINH_THUC_XU_LY_ID : null;
                        //itemTSTDXL.SO_TIEN_THU = tstdxl.SO_TIEN_THU;
                        //itemTSTDXL.HO_SO_GIAY_TO_KHAC = tstdxl.HO_SO_GIAY_TO_KHAC;
                        //itemTSTDXL.DON_VI_CHUYEN_ID = tstdxl.DON_VI_CHUYEN_ID;
                        //itemTSTDXL.GIA_TRI_TAI_SAN_XU_LY = tstdxl.GIA_TRI_TAI_SAN_XU_LY;
                        //itemTSTDXL.TEN_DON_VI_NHAN_DIEU_CHUYEN = tstdxl.TEN_DON_VI_NHAN_DIEU_CHUYEN;
                        //itemTSTDXL.NGAY_XU_LY = tstdxl.NGAY_XU_LY;
                        _taiSanTdXuLyService.InsertTaiSanTdXuLy(itemTSTDXL);
                        _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", itemTSTDXL.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                    }
                    var ttDaTa = _thuocTinhDataService.GetThuocTinhDataByTaiSanId(TaiSanTdXuLyId: (int)itemTSTDXL.ID).FirstOrDefault();
                    if (ttDaTa != null)
                    {
                        ttDaTa.NGAY_CAP_NHAT = DateTime.Now;
                        ttDaTa.DATA = tstdxl.ThuocTinh;
                        _thuocTinhDataService.UpdateThuocTinhData(ttDaTa);
                        _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", ttDaTa.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                    }
                    else
                    {
                        ttDaTa = new ThuocTinhData();
                        ttDaTa.NGAY_TAO = DateTime.Now;
                        ttDaTa.NGAY_CAP_NHAT = DateTime.Now;
                        ttDaTa.TAI_SAN_TD_XU_LY_ID = itemTSTDXL.ID;
                        ttDaTa.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                        ttDaTa.DON_VI_ID = _workContext.CurrentDonVi.ID;
                        //ttDaTa.DON_VI_BO_PHAN_ID = taisan != null ? taisan.DON_VI_BO_PHAN_ID : null;
                        ttDaTa.DATA = tstdxl.ThuocTinh;
                        _thuocTinhDataService.InsertThuocTinhData(ttDaTa);
                        _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", ttDaTa.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                    }
                }
                return JsonSuccessMessage("Cập nhật thành công");
            }
            else
            {
                var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
                return JsonErrorMessage("Cập nhật không thành công", list);
            }

        }
        [HttpPost]
        public virtual IActionResult ViewThuocTinhByHinhThuc(IList<modelThuocTinh> listTT, XuLyModel model, IList<TaiSanTdXuLyModel> listTSXL)
        {

            if (listTT.Count() > 0 && listTSXL.Count() > 0 && model != null)
            {
                //check vali
                var listvali = listTT.Where(c => c.IS_VALIDATE == true && (c.VALUE == null || c.VALUE == "") && c.LoaiDuLieuId != (int)enumLoaiDuLieuCauHinh.OBJ);
                if (listvali != null && listvali.Count() > 0)
                {
                    return JsonErrorMessage("Cập nhật không thành công", listTSXL);
                }
                try
                {
                    //xử lý
                    var item = _itemService.GetXuLyByGuid((Guid)model.GUID);
                    if (item != null) //edit
                    {
                        _itemModelFactory.PrepareXuLy(model, item);
                        _itemService.UpdateXuLy(item);
                        _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<XuLyModel>(), "XuLy");
                    }
                    else //insert
                    {
                        item = new XuLy();
                        item = model.ToEntity<XuLy>();
                        item.NGAY_TAO = DateTime.Now;
                        item.DON_VI_ID = _workContext.CurrentDonVi.ID;
                        _itemService.InsertXuLy(item);
                        _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<XuLyModel>(), "XuLy");
                    }
                    var listmodel = _thuocTinhModelFactory.ToThuocTinhModel(listmodel: listTT.ToList());
                    //thuộc tính
                    foreach (var tt in listmodel)
                    {

                        var tstdxl = listTSXL.Where(c => c.GuidView == tt.GuidView).FirstOrDefault();
                        var tstd = _taiSanTdService.GetTaiSanTdById(tstdxl.TAI_SAN_ID);
                        //var nguyengiaTSTD = tstd.NGUYEN_GIA ?? 0 / tstd.SO_LUONG;
                        //var giatriTSTD = tstd.GIA_TRI ?? 0 / tstd.SO_LUONG;
                        //var dientichTSTD = tstd.DIEN_TICH ?? 0 / tstd.SO_LUONG;
                        //var khoiluongTSTD = tstd.KHOI_LUONG ?? 0 / tstd.SO_LUONG;
                        //tài sản xử lý
                        var itemTSXL = _taiSanTdXuLyService.GetTaiSanTdXuLyByGuId(Guid: (Guid)tstdxl.GUID);
                        if (itemTSXL != null) // update
                        {
                            //itemTSXL.HOP_DONG_NGAY = tstdxl.HOP_DONG_NGAY;
                            //itemTSXL.HOP_DONG_SO = tstdxl.HOP_DONG_SO;
                            //itemTSXL.GHI_CHU = tstdxl.GHI_CHU;
                            //itemTSXL.SO_LUONG = tstdxl.SO_LUONG;
                            //itemTSXL.DIEN_TICH = Decimal.Round((Decimal)(tstdxl.SO_LUONG * dientichTSTD), 4);
                            //itemTSXL.NGUYEN_GIA = Decimal.Round((Decimal)(tstdxl.SO_LUONG * nguyengiaTSTD), 4);
                            //itemTSXL.GIA_TRI = Decimal.Round((Decimal)(tstdxl.SO_LUONG * giatriTSTD), 4);
                            //itemTSXL.KHOI_LUONG = Decimal.Round((Decimal)(tstdxl.SO_LUONG * khoiluongTSTD), 4);
                            //itemTSXL.GIA_TRI_GHI_TANG = tstdxl.GIA_TRI_GHI_TANG;
                            //itemTSXL.GIA_TRI_NSNN = tstdxl.GIA_TRI_NSNN;
                            //itemTSXL.GIA_TRI_TKTG = tstdxl.GIA_TRI_TKTG;
                            //itemTSXL.CHI_PHI_XU_LY = tstdxl.CHI_PHI_XU_LY;
                            //itemTSXL.PHUONG_AN_XU_LY_ID = tstdxl.PHUONG_AN_XU_LY_ID > 0 ? tstdxl.PHUONG_AN_XU_LY_ID : null;
                            //itemTSXL.HINH_THUC_XU_LY_ID = tstdxl.HINH_THUC_XU_LY_ID > 0 ? tstdxl.HINH_THUC_XU_LY_ID : null;
                            //itemTSXL.DON_VI_CHUYEN_ID = tstdxl.DON_VI_CHUYEN_ID;
                            //itemTSXL.GIA_TRI_TAI_SAN_XU_LY = tstdxl.GIA_TRI_TAI_SAN_XU_LY;
                            //itemTSXL.TEN_DON_VI_NHAN_DIEU_CHUYEN = tstdxl.TEN_DON_VI_NHAN_DIEU_CHUYEN;
                            //itemTSXL.NGAY_XU_LY = tstdxl.NGAY_XU_LY;
                            //itemTSXL.SO_TIEN_THU = tstdxl.SO_TIEN_THU;
                            //itemTSXL.HO_SO_GIAY_TO_KHAC = tstdxl.HO_SO_GIAY_TO_KHAC;
                            _taiSanTdXuLyService.UpdateTaiSanTdXuLy(itemTSXL);
                            _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", itemTSXL.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                        }
                        else //insert
                        {
                            itemTSXL = new TaiSanTdXuLy();
                            //itemTSXL.GUID = tstdxl.GUID;
                            itemTSXL.TAI_SAN_ID = tstdxl.TAI_SAN_ID;
                            itemTSXL.XU_LY_ID = item.ID;
                            itemTSXL.SO_LUONG = tstdxl.SO_LUONG;
                            //itemTSXL.DIEN_TICH = Decimal.Round((Decimal)(tstdxl.SO_LUONG * dientichTSTD), 4);
                            //itemTSXL.NGUYEN_GIA = Decimal.Round((Decimal)(tstdxl.SO_LUONG * nguyengiaTSTD), 4);
                            //itemTSXL.GIA_TRI = Decimal.Round((Decimal)(tstdxl.SO_LUONG * giatriTSTD), 4);
                            //itemTSXL.KHOI_LUONG = Decimal.Round((Decimal)(tstdxl.SO_LUONG * khoiluongTSTD), 4);
                            //itemTSXL.GIA_TRI_GHI_TANG = tstdxl.GIA_TRI_GHI_TANG;
                            //itemTSXL.GIA_TRI_NSNN = tstdxl.GIA_TRI_NSNN;
                            //itemTSXL.GIA_TRI_TKTG = tstdxl.GIA_TRI_TKTG;
                            //itemTSXL.HOP_DONG_NGAY = tstdxl.HOP_DONG_NGAY;
                            //itemTSXL.HOP_DONG_SO = tstdxl.HOP_DONG_SO;
                            //itemTSXL.GHI_CHU = tstdxl.GHI_CHU;
                            //itemTSXL.CHI_PHI_XU_LY = tstdxl.CHI_PHI_XU_LY;
                            //itemTSXL.PHUONG_AN_XU_LY_ID = tstdxl.PHUONG_AN_XU_LY_ID > 0 ? tstdxl.PHUONG_AN_XU_LY_ID : null;
                            //itemTSXL.HINH_THUC_XU_LY_ID = tstdxl.HINH_THUC_XU_LY_ID > 0 ? tstdxl.HINH_THUC_XU_LY_ID : null;
                            //itemTSXL.DON_VI_CHUYEN_ID = tstdxl.DON_VI_CHUYEN_ID;
                            //itemTSXL.GIA_TRI_TAI_SAN_XU_LY = tstdxl.GIA_TRI_TAI_SAN_XU_LY;
                            //itemTSXL.TEN_DON_VI_NHAN_DIEU_CHUYEN = tstdxl.TEN_DON_VI_NHAN_DIEU_CHUYEN;
                            //itemTSXL.NGAY_XU_LY = tstdxl.NGAY_XU_LY;
                            //itemTSXL.SO_TIEN_THU = tstdxl.SO_TIEN_THU;
                            //itemTSXL.HO_SO_GIAY_TO_KHAC = tstdxl.HO_SO_GIAY_TO_KHAC;
                            _taiSanTdXuLyService.InsertTaiSanTdXuLy(itemTSXL);
                            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", itemTSXL.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                        }
                        var ttDaTa = _thuocTinhDataService.GetThuocTinhDataByTaiSanXuLyTDIdAndData(Data: tt.GUID.ToString(), TaiSanXuLyTDId: (int)itemTSXL.ID);
                        if (ttDaTa != null)
                        {
                            ttDaTa.NGAY_CAP_NHAT = DateTime.Now;
                            ttDaTa.DATA = tt.ToEntity<ThuocTinhEntity>().toStringJson();
                            _thuocTinhDataService.UpdateThuocTinhData(ttDaTa);
                            _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", ttDaTa.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                        }
                        else
                        {
                            ttDaTa = new ThuocTinhData();
                            ttDaTa.NGAY_TAO = DateTime.Now;
                            ttDaTa.NGAY_CAP_NHAT = DateTime.Now;
                            ttDaTa.TAI_SAN_TD_XU_LY_ID = itemTSXL.ID;
                            ttDaTa.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                            ttDaTa.DON_VI_ID = _workContext.CurrentDonVi.ID;
                            //ttDaTa.DON_VI_BO_PHAN_ID = taisan != null ? taisan.DON_VI_BO_PHAN_ID : null;
                            ttDaTa.DATA = tt.ToEntity<ThuocTinhEntity>().toStringJson();
                            _thuocTinhDataService.InsertThuocTinhData(ttDaTa);
                            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", ttDaTa.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                        }
                    }
                    return JsonSuccessMessage("Cập nhật thành công", listTSXL);
                }
                catch
                {
                    return JsonErrorMessage("Cập nhật không thành công", listTSXL);
                }

            }
            else
            {
                // k tồn tại chỉ tiêu thống kê
                return JsonSuccessMessage("Cập nhật thành công", listTSXL);
            }
        }

        #endregion
    }
}

