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
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Services.ThuocTinhs;
using GS.Web.Models.ThuocTinh;
using GS.Web.Factories.HeThong;
using GS.Web.Factories.DanhMuc;
using GS.Core.Domain.DanhMuc;
using GS.Services.DanhMuc;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class TaiSanTdController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly ITaiSanTdModelFactory _itemModelFactory;
            private readonly ITaiSanTdService _itemService;
            private readonly IQuyetDinhTichThuService _quyetDinhTichThuService;
            private readonly IQuyetDinhTichThuModelFactory _quyetDinhTichThuModelFactory;
            private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;
            private readonly IThuocTinhDataService _thuocTinhDataService;
            private readonly IXuLyService _xuLyService;
            private readonly INhanXeModelFactory _nhanXeModelFactory;
            private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
            private readonly ILoaiTaiSanService _loaiTaiSanService;
        #endregion

        #region Ctor
        public TaiSanTdController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ITaiSanTdModelFactory itemModelFactory,
            ITaiSanTdService itemService,
            IQuyetDinhTichThuService quyetDinhTichThuService,
            IQuyetDinhTichThuModelFactory quyetDinhTichThuModelFactory,
            ITaiSanTdXuLyService taiSanTdXuLyService,
            IXuLyService xuLyService,
            IThuocTinhDataService thuocTinhDataService,
            INhanXeModelFactory _nhanXeModelFactory,
            ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
            ILoaiTaiSanService loaiTaiSanService
            )
        {            
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;            
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._quyetDinhTichThuService = quyetDinhTichThuService;
            this._quyetDinhTichThuModelFactory = quyetDinhTichThuModelFactory;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
            this._xuLyService = xuLyService;
            this._thuocTinhDataService = thuocTinhDataService;
            this._nhanXeModelFactory = _nhanXeModelFactory;
            this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
            this._loaiTaiSanService = loaiTaiSanService;
        }
        #endregion
        #region Methods

        public virtual IActionResult List(int QuyetDinhId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanTdSearchModel() {QuyetDinhId = QuyetDinhId };
            var model = _itemModelFactory.PrepareTaiSanTdSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(TaiSanTdSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanTdListModel(searchModel);
            return Json(model);
        }
        public virtual IActionResult _List(int QuyetDinhId, bool is_detail)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanTdSearchModel() { QuyetDinhId = QuyetDinhId };
            var model = _itemModelFactory.PrepareTaiSanTdSearchModel(searchmodel);
            model.is_detail = is_detail;
            return PartialView(model);
        }
        public virtual IActionResult _ListNhaTrenDat(int TaiSanDatId,string strListNhaId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            var listNhaId = new List<Decimal>();
            if (strListNhaId != null)
            {
                listNhaId = strListNhaId.Split(',').Select(c => Convert.ToDecimal(c)).ToList();
            }
            var searchmodel = new TaiSanTdSearchModel() { TaiSanDatId = TaiSanDatId,TrangThaiId= (int)enumTRANGTHAITSTD.NHAP,ListNhaNhapId=listNhaId };
            var model = _itemModelFactory.PrepareTaiSanTdSearchModel(searchmodel);
            return PartialView(model);
        }
        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanTdModel(new TaiSanTdModel(), null, true);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(TaiSanTdModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                //quyết định
                var qd = new QuyetDinhTichThu();
                qd = model.quyetdinh.ToEntity<QuyetDinhTichThu>();
                _quyetDinhTichThuService.InsertQuyetDinhTichThu(qd);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", qd.ToModel<QuyetDinhTichThuModel>(), "QuyetDinhTichThu");
                //tài sản td
                model.QUYET_DINH_TICH_THU_ID = qd.ID;
                var item = model.ToEntity<TaiSanTd>();
                item.TRANG_THAI_ID = (int)enumTRANGTHAITSTD.TONTAI;
                _itemService.InsertTaiSanTd(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<TaiSanTdModel>(), "TaiSanTd");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareTaiSanTdModel(model, null);            
            return View(model);
        }
        public virtual IActionResult _ViewThemTaiSan(bool IS_DDL)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanTdModel(new TaiSanTdModel(), null,true,IS_DDL:IS_DDL);
            return PartialView(model);
        }
        /// <summary>
        /// Description: Thực hiện update, insert quyết định với danh sách tài sản toàn dân kèm theo quyết định
        /// </summary>
        /// <param name="listmodel"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult _ViewThemTaiSan(IList<TaiSanTdModel> listmodel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                //quyết định
                var qd = new QuyetDinhTichThu();
                var modelqd = listmodel.FirstOrDefault();
                if (modelqd != null)
                {
                    var itemqd = _quyetDinhTichThuService.GetQuyetDinhTichThuByGuid((Guid)modelqd.quyetdinh.GUID);
                    if (itemqd == null)
                    {

                        qd = modelqd.quyetdinh.ToEntity<QuyetDinhTichThu>();
                        qd.NGAY_TAO = DateTime.Now;
                        qd.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                        qd.DON_VI_ID = _workContext.CurrentDonVi.ID;
                        _quyetDinhTichThuService.InsertQuyetDinhTichThu(qd);
                        _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", qd.ToModel<QuyetDinhTichThuModel>(), "QuyetDinhTichThu");
                    }
                    else
                    {
                        itemqd.QUYET_DINH_SO = modelqd.quyetdinh.QUYET_DINH_SO;
                        itemqd.QUYET_DINH_NGAY = modelqd.quyetdinh.QUYET_DINH_NGAY;
                        itemqd.CO_QUAN_BAN_HANH_ID = modelqd.quyetdinh.CO_QUAN_BAN_HANH_ID;
                        itemqd.TEN = modelqd.quyetdinh.TEN;
                        itemqd.GHI_CHU = modelqd.quyetdinh.GHI_CHU;
                        _quyetDinhTichThuService.UpdateQuyetDinhTichThu(itemqd);
                        _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", qd.ToModel<QuyetDinhTichThuModel>(), "QuyetDinhTichThu");
                        qd.ID = itemqd.ID;
                    }
                    foreach (var model in listmodel)
                    {

                        //tài sản td
                        var item = _itemService.GetTaiSanTdByGuid((Guid)model.GUID);
                        if (item == null)
                        {
                            item = new TaiSanTd();
                            model.QUYET_DINH_TICH_THU_ID = qd.ID;
                            item = model.ToEntity<TaiSanTd>();
                            //if (item.NGUON_GOC_TAI_SAN_ID == 0)
                            //{
                            //    item.NGUON_GOC_TAI_SAN_ID = null;
                            //}
                            item.TRANG_THAI_ID = (int)enumTRANGTHAITSTD.NHAP;
                            _itemService.InsertTaiSanTd(item);
                            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<TaiSanTdModel>(), "TaiSanTd");
                        }
                        else
                        {
                            _itemModelFactory.PrepareTaiSanTd(model, item);
                            item.QUYET_DINH_TICH_THU_ID = qd.ID;
                            //if (item.NGUON_GOC_TAI_SAN_ID == 0)
                            //{
                            //    item.NGUON_GOC_TAI_SAN_ID = null;
                            //}
                            _itemService.UpdateTaiSanTd(item);
                            _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<TaiSanTdModel>(), "TaiSanTd");
                        }
                        
                    }
                    return JsonSuccessMessage("Thêm mới thành công", qd);
                }
            }
            //prepare model
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return JsonErrorMessage("Cập không nhập thành công", list);
        }
        public virtual IActionResult GetDDL()
        {           
            var ddl = _itemModelFactory.GetDDLTaiSan();
            return Json(ddl);
        }
        public virtual IActionResult Edit(int QuyetDinhId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            var listitem = _itemService.GetTaiSanTdByQuyetDinhId(QuyetDinhId);
            //var item = _itemService.GetTaiSanTdById(id);
            if (listitem == null || listitem.Count()==0)
                return Redirect("QuyetDinhTichThu/List");
            //prepare model
            var model = new TaiSanTdModel();
            var listmodel = listitem.Select(c => _itemModelFactory.PrepareTaiSanTdModel(null, c,true)).ToList();
            
            model = listmodel.OrderBy(c=>c.ID).FirstOrDefault();
            //cho những model còn lại vào list của model đầu
            listmodel.Remove(model);
            foreach(var lst in listmodel)
            {
                model.listModel.Add(lst);
            }
            model.quyetdinh.is_delete = _quyetDinhTichThuModelFactory.CheckDaTonTaiKetQuaTheoTaiSan(QuyetDinhId);
            return View(model);
        }
        public virtual IActionResult _EditByTaiSan(Guid GUID)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            var item = _itemService.GetTaiSanTdByGuid(GUID);
            //var item = _itemService.GetTaiSanTdById(id);
            if (item == null)
                return Redirect("/QuyetDinhTichThuTai/List");
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanTdModel(null,item,excludeProperties:true,IS_DDL:true);
            //if (_taiSanTdXuLyService.GetTaiSanTdXuLysByTaiSanId(item.ID).Where(c => c.xuly.LOAI_XU_LY_ID == (int)enumLoaiXuLy.KetQua).Count() > 0)
            //{
            //    model.is_delete = false;
            //}
            if (_taiSanTdXuLyService.GetTaiSanTdXuLysByTaiSanId(item.ID).Count() > 0)
            {
                model.is_delete = false;
            }
            return PartialView(model);
        }
        public virtual IActionResult EditByTaiSan(Guid GUID)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            var item = _itemService.GetTaiSanTdByGuid(GUID);
            //var item = _itemService.GetTaiSanTdById(id);
            if (item == null)
                return Redirect("/QuyetDinhTichThuTai/List");
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanTdModel(null, item, excludeProperties: true);
            //if (_taiSanTdXuLyService.GetTaiSanTdXuLysByTaiSanId(item.ID).Where(c => c.xuly.LOAI_XU_LY_ID == (int)enumLoaiXuLy.KetQua).Count() > 0)
            //{
            //    model.is_delete = false;
            //}
            if (_taiSanTdXuLyService.GetTaiSanTdXuLysByTaiSanId(item.ID).Count() > 0)
            {
                model.is_delete = false;
            }
            return PartialView(model);
        }
        /// <summary>
        /// Description: Sửa 1 tài sản toàn dân
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult EditByTaiSan(TaiSanTdModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanTdByGuid((Guid)model.GUID);
            if (item == null)
                return JsonErrorMessage("Cập không nhập thành công",model);
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareTaiSanTd(model, item);
                _itemService.UpdateTaiSanTd(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<TaiSanTdModel>(), "TaiSanTd");
                return JsonSuccessMessage("Cập nhật thành công",item.ToModel<TaiSanTdModel>());
            }
            //prepare model
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return JsonErrorMessage("Cập không nhập thành công", list);
        }

        /// <summary>
        /// </summary>
        /// <param name="model"></param>
        /// <param name="continueEditing"></param>
        /// <returns></returns>
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(TaiSanTdModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanTdById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareTaiSanTd(model,item);
                _itemService.UpdateTaiSanTd(item); 
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<TaiSanTdModel>(), "TaiSanTd");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareTaiSanTdModel(model, item, true);
            return View(model);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(Guid Guid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanTdByGuid(Guid);
            if (item == null)
                return JsonErrorMessage("Xoá dữ liệu không thành công", Guid);
            try
            {
                var listtsxl = _taiSanTdXuLyService.GetTaiSanTdXuLysByTaiSanId((int)item.ID);
                if(listtsxl != null && listtsxl.Count() > 0)
                {
                    foreach(var tsxl in listtsxl)
                    {                       
                        //var cpxl = tsxl.CHI_PHI_XU_LY;
                        var xl = _xuLyService.GetXuLyById(tsxl.xuly.ID);
                        var tt = _thuocTinhDataService.GetThuocTinhDataByTaiSanId(TaiSanTdXuLyId: (int)tsxl.ID).FirstOrDefault();
                        _taiSanTdXuLyService.DeleteTaiSanTdXuLy(tsxl);                        
                        _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", tsxl.ToModel<TaiSanTdXuLyModel>(), "TaiSanTdXuLy");
                        // update thuộc tính
                        //xl.CHI_PHI = (xl.CHI_PHI - cpxl)>=0? (xl.CHI_PHI - cpxl):0;
                        _xuLyService.UpdateXuLy(xl);
                        _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật dữ liệu ", xl.ToModel<XuLyModel>(), "XuLy");
                        //xóa thuộc tính data
                        _thuocTinhDataService.DeleteThuocTinhData(tt);
                        _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", tt.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                    }
                    //// quyết định xử lý
                    //var listxl = listtsxl.Select(c => c.XU_LY_ID).Distinct().ToList();
                    //foreach (var xltdID in listxl)
                    //{
                    //    var checkxl = _taiSanTdXuLyService.GetTaiSanTdsXuLyByXuLyId(xltdID);//khi xóa hết phương án xử lý thì mới xóa xử lý
                    //    if (checkxl.Count() == 0)
                    //    {
                    //        var xltd = _xuLyService.GetXuLyById(xltdID);
                    //        if (xltd != null)
                    //        {
                    //            _xuLyService.DeleteXuLy(xltd);
                    //        }
                    //    }
                    //}
                }
                item.TRANG_THAI_ID = (int)enumTRANGTHAITSTD.XOA;
                _itemService.UpdateTaiSanTd(item);
                //xóa hết map nếu là tài sản đất
                if(item.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT)
                {
                    // xóa hết nhà liên quan đến đất
                    var listTaiSanNha = _itemService.GetTaiSanTds(TaiSanDatId: (decimal)item.ID);
                    if (listTaiSanNha.Count() > 0)
                    {
                        foreach (var taisannha in listTaiSanNha)
                        {
                            taisannha.TAI_SAN_DAT_ID = null;
                            _itemService.UpdateTaiSanTd(taisannha);
                            _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", taisannha.ToModel<TaiSanTdModel>(), "TaiSanTd");
                        }
                    }
                }
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật dữ liệu ", item.ToModel<TaiSanTdModel>(), "TaiSanTd");
                //activity log  
                return JsonSuccessMessage("Xoá dữ liệu thành công", Guid);
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Xoá dữ liệu không thành công",exc);
            }
        }
        public virtual IActionResult _EditTaiSan(Guid Guid)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            var item = _itemService.GetTaiSanTdByGuid(Guid);
            var model = _itemModelFactory.PrepareTaiSanTdModel(new TaiSanTdModel(), item, true);
            //đất
            if (model.LOAI_HINH_TAI_SAN_ID != null && model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT)
            {
                var listTaiSanNha = _itemService.GetTaiSanTds(TaiSanDatId: (int)item.ID).Select(c => (int)c.ID).ToList();
                if (listTaiSanNha.Count() > 0)
                {
                    model.ListTaiSanNha = listTaiSanNha;
                }
                model.DDLNHA = _itemModelFactory.PrepareSelectListTSTD(loaiHinhTaiSan: (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA,isAddFirst:false);
            }
            //nhà
            if (model.LOAI_HINH_TAI_SAN_ID!= null && model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA)
            {
                model.DDLDat = _itemModelFactory.PrepareSelectListTSTD(selected:model.TAI_SAN_DAT_ID!=null?(decimal)model.TAI_SAN_DAT_ID:0, loaiHinhTaiSan: (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT);
                model.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(cheDoId: 23, isAddFirst: true, isDisabled: true, loaiHinhTaiSanId: (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, valSelected: model.LOAI_TAI_SAN_ID ?? 0).ToList();
                model.DDLLoaiTaiSan.Insert(model.DDLLoaiTaiSan.Count(), new SelectListItem { Value = "-1", Text = "Chưa xác định" });
                if (model.TAI_SAN_DAT_ID != null && model.TAI_SAN_DAT_ID != 0 )
                {
                    model.is_dat =true;
                }
                else
                {
                    model.is_dat = false;
                }
                
            }
            //ô tô
            if(model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.OTO)
            {
                model.DDLNhanXe = _nhanXeModelFactory.PrepareSelectListNhanXe(valSelected: (model.NHAN_XE_ID != null ? model.NHAN_XE_ID : 0), isAddFirst: true).ToList();
                model.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(cheDoId: 23, isAddFirst: true, isDisabled: true, loaiHinhTaiSanId: (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO, valSelected: model.LOAI_TAI_SAN_ID ?? 0).ToList();
                if(model.BIEN_KIEM_SOAT!= null)
                {
                    var bks = model.BIEN_KIEM_SOAT.Split('-').ToList();
                    model.Pre_BIEN_KIEM_SOAT = bks.FirstOrDefault();
                    model.Suff_BIEN_KIEM_SOAT = bks.LastOrDefault();
                }
            }
            return PartialView(model);
        }
        public virtual IActionResult _Dat(Decimal? QuyetDinhId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            var item = new TaiSanTd()
            {
                QUYET_DINH_TICH_THU_ID = QuyetDinhId,
                LOAI_HINH_TAI_SAN_ID = (decimal)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT,
                SO_LUONG = 1,
                DON_VI_TINH = "m2"
            };
            item.TRANG_THAI_ID = (int)enumTRANGTHAITSTD.NHAP;
            _itemService.InsertTaiSanTd(item);
            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<TaiSanTdModel>(), "TaiSanTd");
            var model = _itemModelFactory.PrepareTaiSanTdModel(new TaiSanTdModel(), item, true);
            return PartialView(model);
        }
        public virtual IActionResult _Nha(Decimal? QuyetDinhId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanTdModel(new TaiSanTdModel(), null, true);
            model.QUYET_DINH_TICH_THU_ID = QuyetDinhId;
            model.LOAI_HINH_TAI_SAN_ID = (decimal)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA;
            model.DDLDat = _itemModelFactory.PrepareSelectListTSTD(loaiHinhTaiSan:(int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT);
            model.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(cheDoId: 23, isAddFirst: true, isDisabled: true, loaiHinhTaiSanId: (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, valSelected: model.LOAI_TAI_SAN_ID ?? 0).ToList();
            model.DON_VI_TINH = "m2";
            //model.DDLLoaiTaiSan.Insert(model.DDLLoaiTaiSan.Count(), new SelectListItem { Value = "-1", Text = "Chưa xác định" });
            return PartialView(model);
        }
        public virtual IActionResult _Oto(Decimal? QuyetDinhId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanTdModel(new TaiSanTdModel(), null, true);
            model.QUYET_DINH_TICH_THU_ID = QuyetDinhId;
            model.LOAI_HINH_TAI_SAN_ID = (decimal)enumLOAI_HINH_TAI_SAN_TOAN_DAN.OTO;
            model.DDLNhanXe = _nhanXeModelFactory.PrepareSelectListNhanXe(valSelected: (model.NHAN_XE_ID != null ? model.NHAN_XE_ID : 0), isAddFirst: true).ToList();
            model.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(cheDoId: 23, isAddFirst: true, isDisabled: true, loaiHinhTaiSanId: (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO, valSelected: model.LOAI_TAI_SAN_ID ?? 0).ToList();
            model.DON_VI_TINH = "Chiếc";
            return PartialView(model);
        }
        public virtual IActionResult _TaiSanKhac(Decimal? QuyetDinhId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanTdModel(new TaiSanTdModel(), null, true);
            model.QUYET_DINH_TICH_THU_ID = QuyetDinhId;
            model.LOAI_HINH_TAI_SAN_ID = (decimal)enumLOAI_HINH_TAI_SAN_TOAN_DAN.TAI_SAN_KHAC;
            return PartialView(model);
        }
        public virtual IActionResult _NhaTrenDat(Guid Guid,Decimal? QuyetDinhId,Decimal? TaiSanDatId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            var item = _itemService.GetTaiSanTdByGuid(Guid);
            var model = _itemModelFactory.PrepareTaiSanTdModel(new TaiSanTdModel(), item, true);
            model.LOAI_HINH_TAI_SAN_ID = (decimal)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA;
            model.TAI_SAN_DAT_ID = item!=null?item.TAI_SAN_DAT_ID:TaiSanDatId;
            model.SO_LUONG = 1;
            model.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(cheDoId: 23, isAddFirst: true, isDisabled: true, loaiHinhTaiSanId:(int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, valSelected:model.LOAI_TAI_SAN_ID??0).ToList();
            //model.DDLLoaiTaiSan.Insert(model.DDLLoaiTaiSan.Count(), new SelectListItem { Value = "-1", Text = "Chưa xác định" });
            model.DON_VI_TINH = "m2";
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult _NhaTrenDat(TaiSanTdModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //prepare model
            if (ModelState.IsValid)
            {
                var item = _itemService.GetTaiSanTdByGuid(model.GUID);
                if (item != null)
                {
                    item.TEN = model.TEN;
                    item.LOAI_TAI_SAN_ID = model.LOAI_TAI_SAN_ID;
                    item.GIA_TRI_TINH = model.GIA_TRI_TINH;
                    item.NGAY_SU_DUNG =new DateTime((int)(model.NamSuDung??DateTime.Now.Year),01,01);
                    item.GIA_TRI_XAC_LAP = model.GIA_TRI_XAC_LAP;
                    item.DON_VI_TINH = "m2";
                    item.SO_LUONG = 1;
                    if (item.LOAI_TAI_SAN_ID != null && item.LOAI_TAI_SAN_ID > 0)
                    {
                        var lts = _loaiTaiSanService.GetLoaiTaiSanById((decimal)model.LOAI_TAI_SAN_ID);
                        if (lts != null)
                            item.TEN_LOAI_TAI_SAN = lts.TEN;
                    }
                    _itemService.UpdateTaiSanTd(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<TaiSanTdModel>(), "TaiSanTd");
                    return JsonSuccessMessage("Cập nhật dữ liệu thành công !",0);
                }
                else
                {
                    item = model.ToEntity<TaiSanTd>();
                    item.NGAY_SU_DUNG = new DateTime((int)(model.NamSuDung ?? DateTime.Now.Year), 01, 01);
                    item.SO_LUONG = 1;
                    item.TRANG_THAI_ID = (int)enumTRANGTHAITSTD.NHAP;
                    // lúc này chưa cập nhật đất vào chỉ khi lưu đất mới cập nhật
                    item.TAI_SAN_DAT_ID = null;
                    _itemService.InsertTaiSanTd(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<TaiSanTdModel>(), "TaiSanTd");
                    return JsonSuccessMessage("Tạo mới dữ liệu thành công !",item.ID);
                }
                
            }
            //prepare model
            var listError = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Tạo mới dữ liệu không thành công !", listError);
        }
        [HttpPost]
        public virtual IActionResult CreateTSTD(TaiSanTdModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            if (ModelState.IsValid)
            {
                if (model.LOAI_TAI_SAN_ID != null && model.LOAI_TAI_SAN_ID > 0)
                {
                    var lts = _loaiTaiSanService.GetLoaiTaiSanById((decimal)model.LOAI_TAI_SAN_ID);
                    if (lts != null)
                        model.TEN_LOAI_TAI_SAN = lts.TEN;
                }
                // nếu là đất thì chỉ việc cập nhập lại vì đã insert 
                if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT)
                {
                    var item = _itemService.GetTaiSanTdByGuid(model.GUID);
                    _itemModelFactory.PrepareTaiSanTd(model, item);
                    item.DON_VI_TINH = "m2";
                    item.TRANG_THAI_ID = (int)enumTRANGTHAITSTD.TONTAI;
                    _itemService.UpdateTaiSanTd(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<TaiSanTdModel>(), "TaiSanTd");
                    // nhà trên đất cập nhập lại trạng thái
                    if (model.ListNhaNhapId.Count() > 0)
                    {
                        var listnha = _itemService.GetTaiSanNhaNhapTrenDats(ListNhaId: model.ListNhaNhapId.ToList()).ToList();
                        if (listnha.Count() > 0)
                        {
                            foreach (var taisannha in listnha)
                            {
                                taisannha.TAI_SAN_DAT_ID = item.ID;
                                taisannha.TRANG_THAI_ID = (int)enumTRANGTHAITSTD.TONTAI;
                                _itemService.UpdateTaiSanTd(taisannha);
                                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", taisannha.ToModel<TaiSanTdModel>(), "TaiSanTd");
                            }
                        }
                    }
                    if (model.ListTaiSanNha.Count() > 0)
                    {
                        foreach (var taisannhaid in model.ListTaiSanNha)
                        {
                            var taisannha = _itemService.GetTaiSanTdById(taisannhaid);
                            if (taisannha != null)
                            {
                                taisannha.TAI_SAN_DAT_ID = item.ID;
                                _itemService.UpdateTaiSanTd(taisannha);
                                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", taisannha.ToModel<TaiSanTdModel>(), "TaiSanTd");
                            }
                        }
                    }
                }
                else
                {
                    var item = model.ToEntity<TaiSanTd>();
                    if (item.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.TAI_SAN_KHAC)
                    {
                        item.SO_LUONG = 1;
                    }
                    if(item.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.OTO)
                    {
                        if(!string.IsNullOrWhiteSpace(model.Pre_BIEN_KIEM_SOAT) && !string.IsNullOrWhiteSpace(model.Suff_BIEN_KIEM_SOAT))
                        {
                            item.BIEN_KIEM_SOAT = model.Pre_BIEN_KIEM_SOAT + '-' + model.Suff_BIEN_KIEM_SOAT;
                        };
                        item.GIA_TRI_TINH = item.SO_LUONG;
                        item.DON_VI_TINH = "Chiếc";
                    }
                    if (item.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA)
                    {
                        item.DON_VI_TINH = "m2";
                    }
                    item.TRANG_THAI_ID = (int)enumTRANGTHAITSTD.TONTAI;
                    _itemService.InsertTaiSanTd(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<TaiSanTdModel>(), "TaiSanTd");
                }                              
                return JsonSuccessMessage("Tạo mới dữ liệu thành công !");
            }

            //prepare model
            var listError = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Tạo mới dữ liệu không thành công !", listError);
        }
        [HttpPost]
        public virtual IActionResult EditTSTD(TaiSanTdModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanTdByGuid(model.GUID);
            if (item == null)
                return JsonErrorMessage("Cập nhập dữ liệu không thành công !", model.GUID);
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareTaiSanTd(model, item);
                if(model.LOAI_TAI_SAN_ID!= null && model.LOAI_TAI_SAN_ID > 0)
                {
                    var lts = _loaiTaiSanService.GetLoaiTaiSanById((decimal)model.LOAI_TAI_SAN_ID);
                    if(lts!= null)
                    item.TEN_LOAI_TAI_SAN = lts.TEN;
                }
                if (item.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.OTO)
                {
                    if (!string.IsNullOrWhiteSpace(model.Pre_BIEN_KIEM_SOAT) && !string.IsNullOrWhiteSpace(model.Suff_BIEN_KIEM_SOAT))
                    {
                        item.BIEN_KIEM_SOAT = model.Pre_BIEN_KIEM_SOAT + '-' + model.Suff_BIEN_KIEM_SOAT;
                    }
                }
                _itemService.UpdateTaiSanTd(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<TaiSanTdModel>(), "TaiSanTd");
                if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT)
                {
                    // xóa hết nhà liên quan đến đất
                    var listTaiSanNha = _itemService.GetTaiSanTds(TaiSanDatId: (decimal)item.ID);                   
                    if (listTaiSanNha.Count() > 0)
                    {
                        foreach(var taisannha in listTaiSanNha)
                        {
                            taisannha.TAI_SAN_DAT_ID = null;
                            _itemService.UpdateTaiSanTd(taisannha);
                            _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", taisannha.ToModel<TaiSanTdModel>(), "TaiSanTd");
                        }                                              
                    }
                    // update tài nhà trên đất
                    if (model.ListTaiSanNha.Count() > 0)
                    {
                        foreach (var taisannhaid in model.ListTaiSanNha)
                        {
                            var taisannha = _itemService.GetTaiSanTdById(taisannhaid);
                            if (taisannha != null)
                            {
                                taisannha.TAI_SAN_DAT_ID = item.ID;
                                _itemService.UpdateTaiSanTd(taisannha);
                                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", taisannha.ToModel<TaiSanTdModel>(), "TaiSanTd");
                            }
                        }
                    }
                    // nhà trên đất cập nhập lại trạng thái
                    if (model.ListNhaNhapId.Count() > 0)
                    {
                        var listnha = _itemService.GetTaiSanNhaNhapTrenDats(ListNhaId: model.ListNhaNhapId.ToList()).ToList();
                        if (listnha.Count() > 0)
                        {
                            foreach (var taisannha in listnha)
                            {
                                taisannha.TAI_SAN_DAT_ID = item.ID;
                                taisannha.TRANG_THAI_ID = (int)enumTRANGTHAITSTD.TONTAI;
                                _itemService.UpdateTaiSanTd(taisannha);
                                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", taisannha.ToModel<TaiSanTdModel>(), "TaiSanTd");
                            }
                        }
                    }
                }
                
                return JsonSuccessMessage("Cập nhập dữ liệu thành công !");
            }
            //prepare model
            var listError = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Cập nhập dữ liệu không thành công !", listError);           
        }
        public virtual IActionResult _TaiSanKhacTable(Decimal? QuyetDinhId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            var model = _itemModelFactory.PrepareTaiSanTdModel(new TaiSanTdModel(), null, true);
            model.QUYET_DINH_TICH_THU_ID = QuyetDinhId;
            model.LOAI_HINH_TAI_SAN_ID = (decimal)enumLOAI_HINH_TAI_SAN_TOAN_DAN.TAI_SAN_KHAC;
            model.listModel.Add(new TaiSanTdModel() { is_delete = false });
            return PartialView(model);
        }
        public virtual IActionResult _TaiSanKhacRow()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            var model = _itemModelFactory.PrepareTaiSanTdModel(new TaiSanTdModel(), null, true);
            model.is_delete = true;
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult CreateTaiSanKhac(TaiSanTdModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDQuyetDinhTichThu))
                return AccessDeniedView();
            try {
                foreach(var tstd in model.listModel)
                {
                    var item =new TaiSanTd()
                    {
                        QUYET_DINH_TICH_THU_ID = model.QUYET_DINH_TICH_THU_ID,
                        LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID,
                        TEN = tstd.TEN,
                        GIA_TRI_XAC_LAP = tstd.GIA_TRI_XAC_LAP,
                        GIA_TRI_TINH = tstd.GIA_TRI_TINH,
                        DON_VI_TINH = tstd.DON_VI_TINH,
                        TEN_LOAI_TAI_SAN = tstd.TEN_LOAI_TAI_SAN,
                        SO_LUONG = 1,
                        //item.NGUON_GOC_TAI_SAN_ID = model.NGUON_GOC_TAI_SAN_ID;                   
                        TRANG_THAI_ID = (int)enumTRANGTHAITSTD.TONTAI
                    };
                   
                    _itemService.InsertTaiSanTd(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<TaiSanTdModel>(), "TaiSanTd");
                }               
                return JsonSuccessMessage("Tạo mới dữ liệu thành công !");
            }
            catch(Exception ex)
            {
                return JsonErrorMessage("Tạo mới dữ liệu không thành công !",ex);
            }
        }
        #endregion
    }
}

