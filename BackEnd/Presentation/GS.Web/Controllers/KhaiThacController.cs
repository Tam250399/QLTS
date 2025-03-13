//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/7/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Configuration;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.TaiSans;
using GS.Services;
using GS.Services.BienDongs;
using GS.Services.Common;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.DongBo;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Kendoui;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.DongBoTaiSan;
using GS.Web.Models.TaiSans;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class KhaiThacController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IKhaiThacModelFactory _itemModelFactory;
        private readonly IKhaiThacService _itemService;
        private readonly ITaiSanModelFactory _taiSanModelFactory;
        private readonly IKhaiThacTaiSanService _khaiThacTaiSanService;
        private readonly IKhaiThacTaiSanModelFactory _khaiThacTaiSanModelFactory;
        private readonly IKhaiThacChiTietServices _khaiThacChiTietServices;
        private readonly ITaiSanService _taiSanService;
        private readonly IGSAPIService _gsApiService;
        private readonly GSConfig _gSConfig;
        private readonly IBienDongService _biendongService;
        private readonly IDoiTacModelFactory _doiTacModelFactory;
        private readonly IDongBoFactory _dongBoFactory;
        private readonly ILoaiTaiSanService _loaitaisanService;
        private readonly IKhaiThacChiTietModelFactory _khaiThacChiTietModelFactory;
        private readonly IDB_QueueProcessService _dB_QueueProcessService;
        #endregion

        #region Ctor
        public KhaiThacController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IKhaiThacModelFactory itemModelFactory,
            IKhaiThacService itemService,
            ITaiSanModelFactory taiSanModelFactory,
            IKhaiThacTaiSanService khaiThacTaiSanService,
            ITaiSanService taiSanService,
            IKhaiThacTaiSanModelFactory khaiThacTaiSanModelFactory,
            IKhaiThacChiTietServices khaiThacChiTietServices,
            GSConfig gSConfig,
            IGSAPIService gSAPIService,
            IBienDongService biendongService,
            IDoiTacModelFactory doiTacModelFactory,
            IDongBoFactory dongBoFactory,
            ILoaiTaiSanService loaitaisanService,
            IKhaiThacChiTietModelFactory khaiThacChiTietModelFactory,
            IDB_QueueProcessService dB_QueueProcessService
            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._taiSanModelFactory = taiSanModelFactory;
            this._khaiThacTaiSanService = khaiThacTaiSanService;
            this._taiSanService = taiSanService;
            this._khaiThacTaiSanModelFactory = khaiThacTaiSanModelFactory;
            _khaiThacChiTietServices = khaiThacChiTietServices;
            this._gsApiService = gSAPIService;
            this._gSConfig = gSConfig;
            this._biendongService = biendongService;
            this._doiTacModelFactory = doiTacModelFactory;
            this._dongBoFactory = dongBoFactory;
            this._loaitaisanService = loaitaisanService;
            this._khaiThacChiTietModelFactory = khaiThacChiTietModelFactory;
            this._dB_QueueProcessService = dB_QueueProcessService;
        }
        #endregion
        #region Methods
        [HttpGet]
        public virtual IActionResult List(decimal? hinhthuckhaithac = 0, decimal? isdanhsach = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new KhaiThacSearchModel();
            searchmodel.isdanhsach = isdanhsach ?? 0;
            searchmodel.LOAI_HINH_KHAI_THAC_ID = hinhthuckhaithac.Value;
            var model = _itemModelFactory.PrepareKhaiThacSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(KhaiThacSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareKhaiThacListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create(decimal hinhthuckhaithac = 0)
        {
            //if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
            //    return AccessDeniedView();
            //prepare model
            var item = new KhaiThac();
            item.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.NHAP;
            item.LOAI_HINH_KHAI_THAC_ID = hinhthuckhaithac;
            item.NGAY_TAO = DateTime.Now;
            item.KHAI_THAC_NGAY_TU = DateTime.Now;
            item.KHAI_THAC_NGAY_DEN = DateTime.Now;
            item.QUYET_DINH_SO = " ";
            _itemService.InsertKhaiThac(item);
            KhaiThacModel khaithacmodel = new KhaiThacModel();
            var model = _itemModelFactory.PrepareKhaiThacModel(khaithacmodel, null);
            model.LOAI_HINH_KHAI_THAC_ID = hinhthuckhaithac;
            model.ID = item.ID;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(KhaiThacModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<KhaiThac>();
                item.DON_VI_ID = _workContext.CurrentDonVi.ID;
                _itemService.InsertKhaiThac(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<KhaiThacModel>(), "KhaiThac");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return JsonSuccessMessage("Tạo mới dữ liệu thành công !");
            }

            //prepare model
            model = _itemModelFactory.PrepareKhaiThacModel(model, null);
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return JsonErrorMessage("Thêm mới dữ liệu không thành công", list);
        }

        public virtual IActionResult Edit(int id, decimal hinhthuckhaithac)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedView();

            var item = _itemService.GetKhaiThacById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareKhaiThacModel(null, item);
            return View(model);
        }
        [HttpPost]
        public virtual IActionResult Edit(KhaiThacModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKhaiThacById(model.ID);
            item.DON_VI_ID = _workContext.CurrentDonVi.ID;
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareKhaiThac(model, item);
                item.TRANG_THAI_ID = (int)enumTRANG_THAI_TAI_SAN.DA_DUYET;
                _itemService.UpdateKhaiThac(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<KhaiThacModel>(), "KhaiThac");
                // nếu là khai thác khác hoặc sxkd thì update cả ngày bắt đầu và ngày kết thúc của khai thác chi tiết
                if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.SXKD || model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.KHAI_THAC_KHAC)
                {
                    var listKTCT = _khaiThacChiTietServices.GetKhaiThacChiTietByKhaiThacId(model.ID);
                    if (listKTCT != null && listKTCT.Count() > 0)
                    {
                        foreach (var ktct in listKTCT)
                        {
                            if (ktct != null)
                            {
                                ktct.NGAY_KHAI_THAC = model.KHAI_THAC_NGAY_TU;
                                ktct.NGAY_KHAI_THAC_DEN = model.KHAI_THAC_NGAY_DEN;
                                _khaiThacChiTietServices.UpdateKhaiThacChiTiet(ktct);
                                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", ktct.ToModel<KhaiThacChiTietModel>(), "KhaiThacChiTiet");
                            }
                        }
                    }
                }
                // đồng bộ khai thác tài sản
                ParameterKhaiThacTaiSan param = new ParameterKhaiThacTaiSan()
                {
                    KhaiThacId = item.ID,
                    HinhThucKhaiThacId = (int)item.LOAI_HINH_KHAI_THAC_ID,
                    nguonId = (decimal)enumNguonTaiSan.QLTSC
                };
                _dB_QueueProcessService.InsertActionToQueue(action: enumSendRequest.DongBoKhaiThacTaiSan, obj: param, level: (int)enumLevelQueueProcessDB.HIGHEST);
                //_gsApiService.GetObjectGSApi<ResponseApi>("ConsumingTaiSan/DongBoKhaiThacTaiSan", param, _gsApiService.GetToken(true), _gSConfig.ApiUrlWebApi);
                //SuccessNotification("Cập nhật dữ liệu thành công !");
                return JsonSuccessMessage("Cập nhật dữ liệu thành công !");

            }
            //prepare model
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return JsonErrorMessage("Thêm mới dữ liệu không thành công", list);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id, decimal hinhthuckhaithac)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKhaiThacById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                //var khaithactaisans = _khaiThacTaiSanService.GetMapByKhaiThacId(item.ID);
                //if (khaithactaisans != null)
                //	_khaiThacTaiSanService.DeleteKhaiThacTaiSans(khaithactaisans);
                var khaithacchitiets = _khaiThacChiTietServices.GetKhaiThacChiTietByKhaiThacId(item.ID);
                if (khaithacchitiets != null)
                    _khaiThacChiTietServices.DeleteKhaiThacChiTiets(khaithacchitiets);

                //var khaiThacChiTiets = _khaiThacChiTietServices.GetKhaiThacChiTietByKhaiThacId(item.ID);
                //if (khaiThacChiTiets != null && khaiThacChiTiets.Count > 0)
                //	_khaiThacChiTietServices.DeleteKhaiThacChiTiets(khaiThacChiTiets);

                _itemService.DeleteKhaiThac(item);

                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<KhaiThacModel>(), "KhaiThac");
                //activity log  
                SuccessNotification("Xoá dữ liệu thành công");
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Xoá dữ liệu không thành công");
            }
        }
        [HttpPost]
        public virtual IActionResult DeleteAjax(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKhaiThacById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                var khaithactaisans = _khaiThacTaiSanService.GetMapByKhaiThacId(item.ID);
                if (khaithactaisans != null && khaithactaisans.Count > 0)
                    _khaiThacTaiSanService.DeleteKhaiThacTaiSans(khaithactaisans);

                var khaiThacChiTiets = _khaiThacChiTietServices.GetKhaiThacChiTietByKhaiThacId(item.ID);
                if (khaiThacChiTiets != null && khaiThacChiTiets.Count > 0)
                    _khaiThacChiTietServices.DeleteKhaiThacChiTiets(khaiThacChiTiets);

                _itemService.DeleteKhaiThac(item);

                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<KhaiThacModel>(), "KhaiThac");
                //activity log  
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                return JsonErrorMessage("Xoá dữ liệu không thành công");
            }
        }
        //_ListTaiSan
        //public virtual IActionResult _ListTaiSan(TaiSanSearchModel searchModel)
        //{
        //	if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
        //		return AccessDeniedView();
        //	//prepare model
        //	//var searchmodel = new TaiSanSearchModel();
        //	//searchmodel.idKhaiThac = idKhaiThac;
        //	//searchmodel.ngayTu = ngaytu;
        //	//searchmodel.ngayDen = ngayden;
        //	var model = _taiSanModelFactory.PrepareTaiSanSearchModel(searchModel);
        //	//var selectListlhtss = ((enumLOAI_HINH_TAI_SAN)searchModel.LOAI_HINH_TAI_SAN_ID).ToSelectList(valuesToExclude: new int[] { (int)enumLOAI_HINH_TAI_SAN.ALL, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.KHAC });
        //	//searchModel.LoaiHinhTaiSanAvailable = new SelectList(selectListlhtss.Where(x => x.Value != "0").ToList(),"Value","Text");
        //	searchModel.LoaiHinhTaiSanAvailable = ((enumLOAI_HINH_TAI_SAN)searchModel.LOAI_HINH_TAI_SAN_ID).ToSelectList(valuesToExclude: new int[] {  (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.KHAC });
        //	return PartialView(model);
        //}
        //public virtual IActionResult _ListTaiSan(int idKhaiThac = 0, DateTime? ngaytu = null, DateTime? ngayden = null)
        public virtual IActionResult _ListTaiSan(int idKhaiThac = 0, DateTime? ngaytu = null)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanSearchModel();
            searchmodel.idKhaiThac = idKhaiThac;
            searchmodel.ngayTu = ngaytu;
            //searchmodel.ngayDen = ngayden;
            var model = _taiSanModelFactory.PrepareTaiSanSearchModel(searchmodel);
            var valueExclude = new int[] { (int)enumLOAI_HINH_TAI_SAN.KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT
                                            , (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO
                                            , (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC,(int)enumLOAI_HINH_TAI_SAN.TSCD_KHAC,
                                            (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_QUAN_LY_NHU_TSCD };
            searchmodel.LoaiHinhTaiSanAvailable = ((enumLOAI_HINH_TAI_SAN)searchmodel.LOAI_HINH_TAI_SAN_ID).ToSelectList(valuesToExclude: valueExclude);
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult _ListTaiSan_(TaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedView();
            //prepare model 
            searchModel.donvikhaithacid = _workContext.CurrentDonVi.ID;

            var model = _taiSanModelFactory.PrepareTaiSanchonListModel(searchModel);
            return Json(model);
        }
        //_AddTaiSan
        [HttpPost]
        public virtual IActionResult _AddTaiSan(decimal TAI_SAN_ID, decimal KHAI_THAC_ID, decimal? DIEN_TICH_KHAI_THAC)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedView();
            //try to get a store with the specified id
            var khaithac = new KhaiThacChiTiet();
            khaithac.TAI_SAN_ID = TAI_SAN_ID;
            khaithac.KHAI_THAC_ID = KHAI_THAC_ID;
            khaithac.DIEN_TICH_KHAI_THAC = DIEN_TICH_KHAI_THAC ?? 0;
            //var iskhaithactaisan = _khaiThacTaiSanService.checkTrungKhaiThacTaiSan(KHAI_THAC_ID, TAI_SAN_ID);
            //if (iskhaithactaisan == true)
            //{
            //	_khaiThacTaiSanService.UpdateKhaiThacTaiSan(khaithac);
            //}
            //else
            //	_khaiThacTaiSanService.InsertKhaiThacTaiSan(khaithac);
            var iskhaithacchitiet = _khaiThacChiTietServices.checkTrungKhaiThacTaiSan(KHAI_THAC_ID, TAI_SAN_ID);
            if (iskhaithacchitiet == true)
            {
                _khaiThacChiTietServices.UpdateKhaiThacChiTiet(khaithac);
            }
            else
                _khaiThacChiTietServices.InsertKhaiThacChiTiet(khaithac);
            var item = _taiSanService.GetTaiSanById(TAI_SAN_ID);
            var model = item.ToModel<TaiSanModel>();

            SuccessNotification("Cập nhật dữ liệu thành công");
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult CapNhat(TaiSanSearchModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedKendoGridJson();
            var itemTaiSan = _taiSanService.GetTaiSanByMa(model.MA);
            //var item = _khaiThacTaiSanService.GetMapByKhaiThacIdAbTaiSanId(model.KHAI_THAC_ID ?? 0, itemTaiSan.ID);
            var item = _khaiThacChiTietServices.GetMapByKhaiThacIdAndTaiSanId(model.KHAI_THAC_ID ?? 0, itemTaiSan.ID);


            if (item == null)
            {
                var khaiThacTaiSan = new KhaiThacChiTietModel();
                khaiThacTaiSan.KHAI_THAC_ID = model.KHAI_THAC_ID ?? 0;
                khaiThacTaiSan.TAI_SAN_ID = itemTaiSan.ID;
                khaiThacTaiSan.DIEN_TICH_KHAI_THAC = model.DIEN_TICH_KHAI_THAC ?? 0;
                item = khaiThacTaiSan.ToEntity<KhaiThacChiTiet>();
                _khaiThacChiTietServices.InsertKhaiThacChiTiet(item);
                SuccessNotification("Thêm mới dữ liệu thành công");
            }
            if (ModelState.IsValid)
            {
                var modelkhaithactaisan = item.ToModel<KhaiThacChiTietModel>();
                modelkhaithactaisan.TAI_SAN_ID = itemTaiSan.ID;
                modelkhaithactaisan.KHAI_THAC_ID = model.KHAI_THAC_ID ?? 0;
                modelkhaithactaisan.DIEN_TICH_KHAI_THAC = model.DIEN_TICH_KHAI_THAC ?? 0;
                //_khaiThacTaiSanModelFactory.PrepareKhaiThacTaiSan(modelkhaithactaisan, item);
                _khaiThacChiTietModelFactory.PrepareKhaiThacChiTiet(modelkhaithactaisan, item);
                _khaiThacChiTietServices.UpdateKhaiThacChiTiet(item);
                SuccessNotification("Cập nhật dữ liệu thành công");
            }
            else
                return JsonErrorMessage(ModelState.SerializeErrors().ToString());
            return this.JsonSuccessMessage("cập nhật thành công");
        }
        [HttpDelete]
        public virtual IActionResult _deleteKhaiThacTaiSan(Decimal khaiThacId, Decimal taiSanId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedView();
            //KhaiThacTaiSan khaiThacTaiSan = _khaiThacTaiSanService.GetMapByKhaiThacIdAbTaiSanId(khaiThacId, taiSanId);
            KhaiThacChiTiet khaiThacChiTiet = _khaiThacChiTietServices.GetMapByKhaiThacIdAndTaiSanId(khaiThacId, taiSanId);
            if (khaiThacChiTiet != null)
            {
                _khaiThacChiTietServices.DeleteKhaiThacChiTiet(khaiThacChiTiet);
                return JsonSuccessMessage("Xoá tài sản thành công");
            }
            //activity log
            return JsonErrorMessage("Xoá dữ liệu không thành công");
        }
        [HttpPost]
        public virtual IActionResult _deleteNhieuKhaiThacTaiSan(Decimal khaiThacId, string strTaiSanIds)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedView();
            var taiSanIds = strTaiSanIds.Split(",").Select(p => decimal.Parse(p)).ToList();
            if (taiSanIds != null && taiSanIds.Count > 0)
            {
                foreach (var taiSanId in taiSanIds)
                {
                    //KhaiThacTaiSan khaiThacTaiSan = _khaiThacTaiSanService.GetMapByKhaiThacIdAbTaiSanId(khaiThacId, taiSanId);
                    KhaiThacChiTiet khaiThacChiTiet = _khaiThacChiTietServices.GetMapByKhaiThacIdAndTaiSanId(khaiThacId, taiSanId);
                    if (khaiThacChiTiet != null)
                        _khaiThacChiTietServices.DeleteKhaiThacChiTiet(khaiThacChiTiet);
                }
                return JsonSuccessMessage("Xoá tài sản thành công");

            }
            //activity log
            return JsonErrorMessage("Xoá dữ liệu không thành công");
        }
        [HttpPost]
        public virtual IActionResult ChonTaiSan(string strTaiSanIds, decimal KHAI_THAC_ID, string strGiaTriTaiSanIdS)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedView();
            var lstTaiSanId = strTaiSanIds.Split("-").ToList();
            var lstGiaTriTaiSanId = strGiaTriTaiSanIdS.Split("-").ToList();
            //if (strGiaTriTaiSanIdS != null) { 
            for (int i = 0; i < lstTaiSanId.Count(); i++)
            {
                var taiSanId = Convert.ToDecimal(lstTaiSanId[i]);
                var giaTriTaiSanId = Convert.ToDecimal(lstGiaTriTaiSanId[i]);
                //var khaiThacTaiSan = _khaiThacTaiSanService.GetMapByKhaiThacIdAbTaiSanId(khaiThacId: KHAI_THAC_ID, taiSanId: Convert.ToDecimal(taiSanId));
                var khaiThacChitiet = _khaiThacChiTietServices.GetMapByKhaiThacIdAndTaiSanId(khaiThacId: KHAI_THAC_ID, taiSanId: Convert.ToDecimal(taiSanId));

                if (khaiThacChitiet != null)
                {
                    if (lstGiaTriTaiSanId != null)
                    {
                        khaiThacChitiet.DIEN_TICH_KHAI_THAC = giaTriTaiSanId;
                    }
                    _khaiThacChiTietServices.UpdateKhaiThacChiTiet(khaiThacChitiet);
                }
                else
                {
                    var model = new KhaiThacChiTietModel();
                    model.KHAI_THAC_ID = KHAI_THAC_ID;
                    model.TAI_SAN_ID = Convert.ToDecimal(taiSanId);
                    if (lstGiaTriTaiSanId != null)
                        model.DIEN_TICH_KHAI_THAC = giaTriTaiSanId;
                    var item = model.ToEntity<KhaiThacChiTiet>();
                    _khaiThacChiTietServices.InsertKhaiThacChiTiet(item);
                };
            }
            return JsonSuccessMessage(string.Format("thêm mới thành công {0} tài sản", lstTaiSanId.Count));
        }
        #endregion
        #region khai thác tài sản
        public virtual IActionResult _DanhSachTaiSanKhaiThac(decimal khaiThacId, decimal? LOAI_HINH_KHAI_THAC_ID)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedView();
            //prepare model
            KhaiThacChiTietSearchModel searchModel = new KhaiThacChiTietSearchModel();
            if (khaiThacId > 0)
                searchModel.KHAI_THAC_ID = khaiThacId;
            searchModel.LOAI_HINH_KHAI_THAC_ID = LOAI_HINH_KHAI_THAC_ID;
            var model = _khaiThacChiTietModelFactory.PrepareKhaiThacChiTietSearchModel(searchModel);
            return PartialView(model);
        }
        /// <summary>
        /// Danh sách tài sản khai thác
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult DanhSachTaiSansKhaiThac(KhaiThacChiTietSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLBDNhapSoDu))
                return AccessDeniedKendoGridJson();
            //prepare model
            //var model = _khaiThacTaiSanModelFactory.PrepareKhaiThacTaiSanListModel(searchModel);
            var model = _khaiThacChiTietModelFactory.PrepareKhaiThacChiTietListModel(searchModel);
            return Json(model);
        }
        #endregion khai thác tài sản
        #region Khai thác chi tiết
        //public virtual IActionResult CapNhatSoTienKhaiThac(int id, decimal hinhthuckhaithac)
        //{
        //    if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
        //        return AccessDeniedView();

        //    var item = _itemService.GetKhaiThacById(id);
        //    if (item == null)
        //        return RedirectToAction("List", new { hinhthuckhaithac = enumHinhThucKhaiThac.ALL, isdanhsach = 1 });
        //    //prepare model
        //    var model = _itemModelFactory.PrepareKhaiThacModel(null, item);
        //    return View(model);
        //}

        [HttpGet]
        [HttpPost]
        public virtual IActionResult _ChiTietTaiSanKhaiThac(decimal? TaiSanId, decimal? KhaiThacId = 0, decimal? LOAI_HINH_KHAI_THAC_ID = 0, decimal? KhaiThacChiTietId = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedView();
            var model = new KhaiThacChiTietModel();
            KhaiThacChiTiet item = null;
            if (KhaiThacChiTietId > 0)
            {
                item = _khaiThacChiTietServices.GetKhaiThacChiTietById(KhaiThacChiTietId ?? 0);
            }
            model.TAI_SAN_ID = TaiSanId;
            model.KHAI_THAC_ID = KhaiThacId ?? 0;
            model.LOAI_HINH_KHAI_THAC_ID = LOAI_HINH_KHAI_THAC_ID ?? 0;
            var ts = _taiSanService.GetTaiSanById(TaiSanId ?? 0);
            if(ts != null)
            {
                model.LOAI_HINH_TAI_SAN_ID = ts.LOAI_HINH_TAI_SAN_ID;
            }
            model = _khaiThacChiTietModelFactory.PrepareKhaiThacChiTietModel(model, item);
            if (LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.LDLK)
            {
                return PartialView("_ChiTietKhaiThacLDLK", model);
            }
            else
            {
                return PartialView(model);
            }

        }

        [HttpPost]
        public virtual IActionResult savekhaithacchitiet(KhaiThacChiTietModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ID > 0)
                {
                    var item = _khaiThacChiTietServices.GetKhaiThacChiTietById(model.ID);
                    _khaiThacChiTietModelFactory.PrepareKhaiThacChiTiet(model, item);
                    if (item != null)
                    {
                        _khaiThacChiTietServices.UpdateKhaiThacChiTiet(item);
                        _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<KhaiThacChiTietModel>(), "KhaiThacChiTiet");
                        SuccessNotification("Update dữ liệu thành công");
                        return JsonSuccessMessage("Cập nhật chi tiết thành công", model);
                    }

                }
                else
                {
                    var item = model.ToEntity<KhaiThacChiTiet>();
                    _khaiThacChiTietServices.InsertKhaiThacChiTiet(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<KhaiThacChiTietModel>(), "KhaiThacChiTiet");
                    model.ID = item.ID;
                    SuccessNotification("Tạo mới dữ liệu thành công !");
                    return JsonSuccessMessage("Cập nhật chi tiết thành công", model);
                }

            }
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return JsonErrorMessage("Cập nhật thông tin thất bại", list);
        }
        #endregion

        #region Cập nhật số tiền khai thác Theo Tài sản(Yêu cầu hiện tại)
        public virtual IActionResult CapNhatSoTienKhaiThac(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThac))
                return AccessDeniedView();

            var item = _khaiThacChiTietServices.GetKhaiThacChiTietById(id);
            if (item == null)
                return RedirectToAction("List", "KhaiThacChiTiet");
            //prepare model
            var model = _khaiThacChiTietModelFactory.PrepareKhaiThacChiTietModel(null, item);
            // Hiển thị thông tin khai thác chi tiết
            model.IsShowInfo = true;
            return View(model);
        }
        #endregion
    }
}

