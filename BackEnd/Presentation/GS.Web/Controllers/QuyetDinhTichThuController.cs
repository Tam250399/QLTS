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
using System.Linq;
using GS.Web.Models.ThuocTinh;
using GS.Services.ThuocTinhs;
using static GS.Core.Domain.SHTD.QuyetDinhTichThu;
using GS.Web.Factories.DanhMuc;
using GS.Services;
using GS.Core.Domain.DB;
using GS.Services.DB;
using GS.Web.Factories.DB;
using System.Collections.Generic;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class QuyetDinhTichThuController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly IQuyetDinhTichThuModelFactory _itemModelFactory;
            private readonly IQuyetDinhTichThuService _itemService;
            private readonly ITaiSanTdService _taiSanTdService;
            private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;
            private readonly IXuLyService _xuLyService;
            private readonly IThuocTinhDataService _thuocTinhDataService;
            private readonly INguonGocTaiSanModelFactory _nguonGocTaiSanModelFactory;
            private readonly INhatKyTaiSanToanDanService _nhatKyTaiSanToanDanService;
            private readonly ITaiSanNhatKyService _taiSanNhatKyService;
            private readonly IDB_QueueProcessModelFactory _dB_QueueProcessModelFactory;
        #endregion

        #region Ctor
        public QuyetDinhTichThuController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IQuyetDinhTichThuModelFactory itemModelFactory,
            IQuyetDinhTichThuService itemService,
            ITaiSanTdService taiSanTdService,
            ITaiSanTdXuLyService taiSanTdXuLyService,
            IXuLyService xuLyService,
            IThuocTinhDataService thuocTinhDataService,
            INguonGocTaiSanModelFactory nguonGocTaiSanModelFactory,
            INhatKyTaiSanToanDanService nhatKyTaiSanToanDanService,
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
            this._taiSanTdService = taiSanTdService;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
            this._xuLyService = xuLyService;
            this._thuocTinhDataService = thuocTinhDataService;
            this._nguonGocTaiSanModelFactory = nguonGocTaiSanModelFactory;
            this._nhatKyTaiSanToanDanService = nhatKyTaiSanToanDanService;
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._dB_QueueProcessModelFactory = dB_QueueProcessModelFactory;
        }
        #endregion
        #region Methods
        public virtual IActionResult _ListForBaoCao()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new QuyetDinhTichThuSearchModel();
            var model = _itemModelFactory.PrepareQuyetDinhTichThuSearchModel(searchmodel);
            return PartialView(model);
        }
        public virtual IActionResult ListNhapBoSung()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new QuyetDinhTichThuSearchModel();
            var model = _itemModelFactory.PrepareQuyetDinhTichThuSearchModel(searchmodel);
            return View(model);
        }
        public virtual IActionResult ListDuyetQuyetDinh()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDuyetQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            var searchModel = new QuyetDinhTichThuSearchModel();
            var model = _itemModelFactory.PrepareQuyetDinhTichThuSearchModel(searchModel);
            return View(model);
        }
        [HttpPost]
        public virtual IActionResult ListDuyetQuyetDinh(QuyetDinhTichThuSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDuyetQuyetDinhTichThu))
                return AccessDeniedKendoGridJson();
            //prepare model           
            var model = _itemModelFactory.PrepareQuyetDinhTichThuListModel(searchModel);
            return Json(model);
        }
        [HttpPost]
        public virtual IActionResult ListNhapBoSung(QuyetDinhTichThuSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedKendoGridJson();
            //prepare model
            searchModel.TrangThaiId = (int)enumTRANGTHAI_QUYETDINH_TSTD.CHO_DUYET;
            var model = _itemModelFactory.PrepareQuyetDinhTichThuListModel(searchModel);
            return Json(model);
        }
        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new QuyetDinhTichThuSearchModel ();
            var model = _itemModelFactory.PrepareQuyetDinhTichThuSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(QuyetDinhTichThuSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareQuyetDinhTichThuListModel(searchModel);
            return Json(model);
        }
        public virtual IActionResult ListVuViecForXuLy(int LoaiXuLy)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new QuyetDinhTichThuSearchModel() { LoaiXuLy = LoaiXuLy};
            var model = _itemModelFactory.PrepareQuyetDinhTichThuSearchModel(searchmodel);
            return PartialView(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            var item = new QuyetDinhTichThu
            {
                DON_VI_ID = _workContext.CurrentDonVi.ID,
                NGUOI_TAO_ID = _workContext.CurrentCustomer.ID,
                NGAY_TAO = DateTime.Now,
                TRANG_THAI_ID  = (int)enumTRANGTHAI_QUYETDINH_TSTD.NHAP
        };
            _itemService.InsertQuyetDinhTichThu(item);
            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<QuyetDinhTichThuModel>(), "QuyetDinhTichThu");
            var model = _itemModelFactory.PrepareQuyetDinhTichThuModel(new QuyetDinhTichThuModel(), item,prepareDDL:true);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(QuyetDinhTichThuModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<QuyetDinhTichThu>();
                item.DON_VI_ID = _workContext.CurrentDonVi.ID;
                item.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                item.NGAY_TAO = DateTime.Now;
                item.TRANG_THAI_ID = (int)enumTRANGTHAI_QUYETDINH_TSTD.CHO_DUYET;
                _itemService.InsertQuyetDinhTichThu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<QuyetDinhTichThuModel>(), "QuyetDinhTichThu");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return RedirectToAction("Detail", new { id = item.ID });
            }

            //prepare model
            model = _itemModelFactory.PrepareQuyetDinhTichThuModel(model, null);            
            return View(model);
        }
        [HttpPost]
        public virtual IActionResult CreateQuyetDinh(QuyetDinhTichThuModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetQuyetDinhTichThuByGuid(model.GUID);
            if (item == null)
                return JsonErrorMessage("Tạo mới dữ liệu không thành công !", model.GUID);
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareQuyetDinhTichThu(model, item);
                var listTaiSan = _taiSanTdService.GetTaiSanTdByQuyetDinhId(item.ID);
                if (listTaiSan.Count() > 0)
                {
                    item.TRANG_THAI_ID = (int)enumTRANGTHAI_QUYETDINH_TSTD.CHO_DUYET;
                    _itemService.UpdateQuyetDinhTichThu(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<QuyetDinhTichThuModel>(), "QuyetDinhTichThu");
                // update lại nguồn gốc
                    foreach (var taisan in listTaiSan)
                    {
                        taisan.quyetdinh.NGUOI_TAO_ID = item.NGUON_GOC_ID;
                        _taiSanTdService.UpdateTaiSanTd(taisan);
                        _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", taisan.ToModel<TaiSanTdModel>(), "TaiSanTd");
                    }
                    return JsonSuccessMessage("Tạo mới dữ liệu thành công !");
                }
                
                return JsonErrorMessage("Quyết định chưa bao gồm tài sản !");
            }
            //prepare model
            var listError = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Tạo mới dữ liệu không thành công !", listError);
        }
        /// <summary>
        /// check Tổng Giá trị tịch thu/xác lập của các tài sản trong quyết định tịch thu phải <= Tổng giá trị tịch thu/xác lập
        /// Nếu thỏa mãn dk => return true, không thì false
        /// </summary>
        /// <param name="Guid"></param>
        /// <param name="TongGiaTriTichThu"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult CheckTongGiaTriTichThu(Guid Guid, decimal TongGiaTriTichThu)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //Nếu TGT = 0 thì return true luôn
            if (TongGiaTriTichThu == 0)
            {
                return JsonSuccessMessage(objdata: true);
            }
            var isValid = _itemModelFactory.CheckTongGiaTriTichThu(Guid, TongGiaTriTichThu);
            // tìm ra list tài sản
            if (isValid)
            {
                return JsonSuccessMessage(objdata: true);
            }
            else
            {
                return JsonSuccessMessage(msg: "Tổng giá trị tịch thu/xác lập phải >= tổng \"Giá trị tịch thu/xác lập\" của các tài sản trong quyết định tịch thu", objdata: false);
            }       
                                       
        }

        public virtual IActionResult Detail(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();

            var item = _itemService.GetQuyetDinhTichThuById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareQuyetDinhTichThuModel(null, item);
            model.is_detail = true;
            return View(model);
        }
        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
                
            var item = _itemService.GetQuyetDinhTichThuById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareQuyetDinhTichThuModel(null, item, prepareDDL: true);           
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(QuyetDinhTichThuModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetQuyetDinhTichThuById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareQuyetDinhTichThu(model,item);
                _itemService.UpdateQuyetDinhTichThu(item); 
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<QuyetDinhTichThuModel>(), "QuyetDinhTichThu");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareQuyetDinhTichThuModel(model, item, true, prepareDDL: true);
            return View(model);
        }
        [HttpPost]
        public virtual IActionResult EditQuyetDinh(QuyetDinhTichThuModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetQuyetDinhTichThuByGuid(model.GUID);
            if (item == null)
                return JsonErrorMessage("Cập nhật dữ liệu không thành công !",model.GUID);
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareQuyetDinhTichThu(model, item);
                if(item.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.TU_CHOI)
                {
                    item.TRANG_THAI_ID = (int)enumTRANGTHAI_QUYETDINH_TSTD.CHO_DUYET;
                }
                _itemService.UpdateQuyetDinhTichThu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<QuyetDinhTichThuModel>(), "QuyetDinhTichThu");
                // update lại nguồn gốc
                var listTaiSan = _taiSanTdService.GetTaiSanTdByQuyetDinhId(item.ID);
                foreach (var taisan in listTaiSan)
                {
                    taisan.quyetdinh.NGUON_GOC_ID = item.NGUON_GOC_ID;
                    _taiSanTdService.UpdateTaiSanTd(taisan);
                    _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", taisan.ToModel<TaiSanTdModel>(), "TaiSanTd");
                }
                return JsonSuccessMessage("Cập nhật dữ liệu thành công !");
            }
            //prepare model
            var listError = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Cập nhật dữ liệu không thành công !", listError);
        }
        [HttpPost]
        public virtual IActionResult Delete(Guid Guid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetQuyetDinhTichThuByGuid(Guid);
            if (item == null)
                return JsonErrorMessage("Xoá dữ liệu không thành công", Guid);
            try
            {
                var listts = _taiSanTdService.GetTaiSanTdByQuyetDinhId((int)item.ID);
                if (listts != null && listts.Count() > 0) 
                {
                    //tài sản td
                    foreach (var ts in listts)
                    {
                        //update tài sản 
                        ts.TRANG_THAI_ID = (int)enumTRANGTHAITSTD.XOA;
                        _taiSanTdService.UpdateTaiSanTd(ts);
                        _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", ts.ToModel<TaiSanTdModel>(), "TaiSanTd");

                        // xử lý tài sản
                        var listtsxl = _taiSanTdXuLyService.GetTaiSanTdXuLysByTaiSanId((int)ts.ID);
                        if (listtsxl != null && listtsxl.Count() > 0)
                        {                           
                            foreach (var tsxl in listtsxl)
                            {
                                var tt = _thuocTinhDataService.GetThuocTinhDataByTaiSanId(TaiSanTdXuLyId: (int)tsxl.ID).FirstOrDefault();
                                _taiSanTdXuLyService.DeleteTaiSanTdXuLy(tsxl);
                                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", tsxl.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                                _thuocTinhDataService.DeleteThuocTinhData(tt);
                                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", tt.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                            }
                            // quyết định xử lý
                            var listxl = listtsxl.Select(c => c.XU_LY_ID).Distinct().ToList();
                            foreach (var xltdID in listxl)
                            {
                                var xltd = _xuLyService.GetXuLyById(xltdID);
                                if (xltd != null)
                                {
                                    
                                    _xuLyService.DeleteXuLy(xltd);
                                    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", xltd.ToModel<XuLyModel>(), "XuLy");
                                    
                                }
                            }                                                     
                        }                       
                    }
 
                }
                item.TRANG_THAI_ID = (int)enumTRANGTHAI_QUYETDINH_TSTD.XOA;
                _itemService.UpdateQuyetDinhTichThu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<QuyetDinhTichThuModel>(), "QuyetDinhTichThu");
                //activity log  
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = item.ID });
            }
        }
        [HttpPost]
        public virtual IActionResult Duyet(Guid Guid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDuyetQuyetDinhTichThu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetQuyetDinhTichThuByGuid(Guid);
            if (item == null)
                return JsonErrorMessage("Cập nhật dữ liệu không thành công !", Guid);
            if (item.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.CHO_DUYET)
            {
                item.TRANG_THAI_ID = (int)enumTRANGTHAI_QUYETDINH_TSTD.DUYET;
                _itemService.UpdateQuyetDinhTichThu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<QuyetDinhTichThuModel>(), "QuyetDinhTichThu");
                var nhatky = new NhatKyTaiSanToanDan()
                {
                    DATA_JSON = item.toStringJson(),
                    QUYET_DINH_ID = item.ID,
                    NGUOI_DUNG_ID = _workContext.CurrentCustomer.ID,
                    TRANG_THAI_ID = (int)enumTRANGTHAI_QUYETDINH_TSTD.DUYET
                };
                _nhatKyTaiSanToanDanService.InsertNhatKyTaiSanToanDan(nhatky);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Thêm mới thông tin ", nhatky.ToModel<NhatKyTaiSanToanDanModel>(), "NhatKyTaiSanToanDan");
                //nhật ký đồng bộ
                //quyết định tịch thu
                TaiSanNhatKy taiSanNhatKy = _taiSanNhatKyService.GetTaiSanNhatKyByQuyetDinhTichThu(item.ID);
                if (taiSanNhatKy == null)
                {
                    taiSanNhatKy = new TaiSanNhatKy();
                    taiSanNhatKy.QUYET_DINH_TICH_THU_ID = item.ID;
                    taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
                    taiSanNhatKy.NGAY_CAP_NHAT = DateTime.Now;
                    taiSanNhatKy.IS_TAI_SAN_TOAN_DAN = true;
                    _taiSanNhatKyService.InsertTaiSanNhatKy(taiSanNhatKy);
                }
                else
                {
                    taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
                    taiSanNhatKy.NGAY_CAP_NHAT = DateTime.Now;
                    taiSanNhatKy.IS_TAI_SAN_TOAN_DAN = true;
                    _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                }
                //đồng bộ quyết định tịch thu sang kho
                _dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSanXacLap/UpdateQuyetDinhTichThu",new List<QuyetDinhTichThuDongBoModel>(){ item.ToModel<QuyetDinhTichThuDongBoModel>() }, _workContext.CurrentDonVi.ID,(int)enumLevelQueueProcessDB.HIGH);
                //tài sản toàn dân
                var listTSTD = _taiSanTdService.GetTaiSanTdByQuyetDinhId(item.ID);
                if (listTSTD.Count() > 0)
                {
                    //foreach (var tstd in listTSTD)
                    //{

                    //    taiSanNhatKy = _taiSanNhatKyService.GetByTaiSanTdId(tstd.ID);
                    //    if (taiSanNhatKy == null)
                    //    {
                    //        taiSanNhatKy = new TaiSanNhatKy();
                    //        taiSanNhatKy.QUYET_DINH_TICH_THU_ID = item.ID;
                    //        taiSanNhatKy.TAI_SAN_ID = tstd.ID;
                    //        taiSanNhatKy.LOAI_HINH_TAI_SAN_ID = tstd.LOAI_HINH_TAI_SAN_ID;
                    //        taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
                    //        taiSanNhatKy.NGAY_CAP_NHAT = DateTime.Now;
                    //        taiSanNhatKy.IS_TAI_SAN_TOAN_DAN = true;
                    //        _taiSanNhatKyService.InsertTaiSanNhatKy(taiSanNhatKy);

                    //    }
                    //    else
                    //    {
                    //        taiSanNhatKy.LOAI_HINH_TAI_SAN_ID = tstd.LOAI_HINH_TAI_SAN_ID;
                    //        taiSanNhatKy.TRANG_THAI_ID = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
                    //        taiSanNhatKy.NGAY_CAP_NHAT = DateTime.Now;
                    //        taiSanNhatKy.IS_TAI_SAN_TOAN_DAN = true;
                    //        _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                    //    }
                    //}
                    //đồng bộ tài sản toàn dân sang kho
                    _dB_QueueProcessModelFactory.InsertActionToQueue("ConsumingTaiSanXacLap/UpdateTaiSanTd", listTSTD.Select(c => c.ToModel<TaiSanTdDongBoModel>()), _workContext.CurrentDonVi.ID);
                }
                return JsonSuccessMessage("Cập nhật dữ liệu thành công !");
            }
            else
            {
                return JsonErrorMessage("Cập nhật dữ liệu không thành công !", Guid);
            }
        }

        [HttpPost]
        public virtual IActionResult DuyetListQuyetDinh(string strListQuyetDinhID)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDuyetQuyetDinhTichThu))
                return AccessDeniedView();
            if (!string.IsNullOrEmpty(strListQuyetDinhID))
            {
                var count = _itemModelFactory.DuyetListQuyetDinhTichThu(strListQuyetDinhID);
                if (count > 0)
                {
                    return JsonSuccessMessage($"Duyệt thành công {count} quyết định tịch thu!");
                }
                return JsonErrorMessage("Cập nhật dữ liệu không thành công !");
            }
            return JsonErrorMessage("Cập nhật dữ liệu không thành công !");
        }
        [HttpPost]
        public virtual IActionResult BoDuyet(Guid Guid, string LyDo)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDuyetQuyetDinhTichThu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetQuyetDinhTichThuByGuid(Guid);
            if (item == null)
                return JsonErrorMessage("Cập nhật dữ liệu không thành công !", Guid);
            if (item.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.DUYET)
            {
                item.TRANG_THAI_ID = (int)enumTRANGTHAI_QUYETDINH_TSTD.TU_CHOI;
                item.GHI_CHU = "Lý do bỏ duyệt:" + LyDo + "; " + item.GHI_CHU;
                _itemService.UpdateQuyetDinhTichThu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<QuyetDinhTichThuModel>(), "QuyetDinhTichThu");
                var nhatky = new NhatKyTaiSanToanDan()
                {
                    DATA_JSON = item.toStringJson(),
                    QUYET_DINH_ID = item.ID,
                    NGUOI_DUNG_ID = _workContext.CurrentCustomer.ID,
                    TRANG_THAI_ID = (int)enumTRANGTHAI_QUYETDINH_TSTD.TU_CHOI
                };
                _nhatKyTaiSanToanDanService.InsertNhatKyTaiSanToanDan(nhatky);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Thêm mới thông tin ", nhatky.ToModel<NhatKyTaiSanToanDanModel>(), "NhatKyTaiSanToanDan");
                return JsonSuccessMessage("Cập nhật dữ liệu thành công !");
            }
            else
            {
                return JsonErrorMessage("Cập nhật dữ liệu không thành công !", Guid);
            }
        }
        [HttpPost]
        public virtual IActionResult TuChoi(Guid Guid,string LyDo)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDuyetQuyetDinhTichThu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetQuyetDinhTichThuByGuid(Guid);
            if (item == null)
                return JsonErrorMessage("Cập nhật dữ liệu không thành công !", Guid);
            if (item.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.CHO_DUYET)
            {
                item.TRANG_THAI_ID = (int)enumTRANGTHAI_QUYETDINH_TSTD.TU_CHOI;
                item.GHI_CHU = "Lý do từ chối:" + LyDo + "; " + item.GHI_CHU;
                _itemService.UpdateQuyetDinhTichThu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<QuyetDinhTichThuModel>(), "QuyetDinhTichThu");
                var nhatky = new NhatKyTaiSanToanDan()
                {
                    DATA_JSON = item.toStringJson(),
                    QUYET_DINH_ID = item.ID,
                    NGUOI_DUNG_ID = _workContext.CurrentCustomer.ID,
                    TRANG_THAI_ID = (int)enumTRANGTHAI_QUYETDINH_TSTD.TU_CHOI
                };
                _nhatKyTaiSanToanDanService.InsertNhatKyTaiSanToanDan(nhatky);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Thêm mới thông tin ", nhatky.ToModel<NhatKyTaiSanToanDanModel>(), "NhatKyTaiSanToanDan");
                return JsonSuccessMessage("Cập nhật dữ liệu thành công !");
            }
            else
            {
                return JsonErrorMessage("Cập nhật dữ liệu không thành công !", Guid);
            }
        }
        public virtual IActionResult GetNguonGoc(bool? isTichThu, decimal NguonGocID,bool isDisable)
        {
            //if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
            //    return AccessDeniedView();
            return Json(_nguonGocTaiSanModelFactory.PrepareSelectListNguonGocTaiSan(selected: NguonGocID, isAddFirst: true,isDisable: isDisable, isTichThu: isTichThu??false, isAll: isTichThu == null));
        }
        #endregion
    }
}

