//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/6/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.HeThong;

using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.HeThong;
using GS.Web.Controllers;
using GS.Web.Factories.HeThong;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Services.DanhMuc;
using System.Collections.Generic;
using GS.Web.Factories.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Web.Framework.Extensions;
using System.Linq;
using GS.Core.Domain.DanhMuc;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class DinhMucController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly IDinhMucModelFactory _itemModelFactory;
            private readonly IDinhMucService _itemService;
            private readonly IDinhMucChiTietService _dinhMucChiTietService;
            private readonly IDonViService _donViService;
            private readonly IChucDanhService _chucDanhService;
            private readonly ILoaiTaiSanService _loaiTaiSanService;
            private readonly IChucDanhModelFactory _chucDanhModelFactory;
            private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
            private readonly IDinhMucChiTietModelFactory _dinhMucChiTietModelFactory;
         #endregion
     
        #region Ctor
        public DinhMucController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IDinhMucModelFactory itemModelFactory,
            IDinhMucService itemService,
            IDinhMucChiTietService dinhMucChiTietService,
            IDonViService donViService,
            IChucDanhService chucDanhService,
            ILoaiTaiSanService loaiTaiSanService,
            IChucDanhModelFactory chucDanhModelFactory,
            ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
            IDinhMucChiTietModelFactory dinhMucChiTietModelFactory
            )
        {            
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;            
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._dinhMucChiTietService = dinhMucChiTietService;
            this._donViService = donViService;
            this._chucDanhService = chucDanhService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._chucDanhModelFactory = chucDanhModelFactory;
            this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
            this._dinhMucChiTietModelFactory = dinhMucChiTietModelFactory;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDinhMuc))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new DinhMucSearchModel ();
            var model = _itemModelFactory.PrepareDinhMucSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(DinhMucSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDinhMuc))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareDinhMucListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDinhMuc))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareDinhMucModel(new DinhMucModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(DinhMucModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDinhMuc))
                return AccessDeniedView();
            
            //check trùng chi tiết dinhmuc
            foreach (var chiTietDinhMuc in model.ChiTietDinhMuc)
            {
                var key = string.Format("err_DINH_MUC_CHITIET_" + chiTietDinhMuc._arr);
                var value = "";
                foreach(var chitietdinhmuc1 in model.ChiTietDinhMuc) { 
                    if(chiTietDinhMuc._arr != chitietdinhmuc1._arr)
                    {
                        if(chiTietDinhMuc.LOAI_TAI_SAN_ID == chitietdinhmuc1.LOAI_TAI_SAN_ID && chiTietDinhMuc.CHUC_DANH_ID == chitietdinhmuc1.CHUC_DANH_ID)
                        {
                            //ModelState.AddModelError(string.Format("err_DINH_MUC_CHITIET_" + chiTietDinhMuc._arr), "Chi tiết khai thác đã tồn tại");
                            value = "Chi tiết khai thác đã tồn tại";
                        }
                    }
                }
                var lstdinhmucids = _itemService.GetAllDinhMucs().Where(c => c.DON_VI_ID == (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG || c.DON_VI_ID == model.DON_VI_ID).Select(c => c.ID).ToList<decimal>();
                var lstchitietDinhmuc = _dinhMucChiTietService.GetAllDinhMucChiTiets().Where(c => c.CHUC_DANH_ID == chiTietDinhMuc.CHUC_DANH_ID && c.LOAI_TAI_SAN_ID == chiTietDinhMuc.LOAI_TAI_SAN_ID && lstdinhmucids.Contains(c.DINH_MUC_ID) && c.ID != chiTietDinhMuc.ID).ToList();
                //var lstdinhmucid = lstchitietDinhmuc.Select(c => c.DINH_MUC_ID).ToArray();
                //var lstdinhmuc = _itemService.GetDinhMucByIds(lstdinhmucid).Where(c => c.TU_NGAY <= model.TU_NGAY || model.TU_NGAY < c.DEN_NGAY);
                if (lstchitietDinhmuc.Count > 0)
                {
                    value = "Quyết định cũ chứa định mức vẫn còn hiệu lực";
                }
                if (!string.IsNullOrEmpty(value))
                {
                    ModelState.AddModelError(key, value);
                }
                if (chiTietDinhMuc.LOAI_TAI_SAN_ID == 0)
                {
                    ModelState.AddModelError(string.Format("err_LOAI_TAI_SAN_ID_" + chiTietDinhMuc._arr), "Loại tài sản không được để trống");
                }
                string MaLoaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(chiTietDinhMuc.LOAI_TAI_SAN_ID)?.MA;
                if (chiTietDinhMuc.CHUC_DANH_ID == 0 &&  int.Parse(MaLoaiTaiSan) == (int)enumLoaiTaiSanOto.OTO_CHUC_DANH)
                {
                    ModelState.AddModelError(string.Format("err_MA_CHUC_DANH_" + chiTietDinhMuc._arr), "Chức danh không được để trống");
                }
            }

            if (ModelState.IsValid)
            {
                // check quyet định tồn tại trong khoảng thời gian
                var donviid = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
                var donvicha = _donViService.GetDonViLonNhat(donviid.ID, donviid.TREE_NODE);
                if (donviid.ID == (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG || donvicha.ID == (int)enumMaDPAC.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
                {
                    model.DON_VI_ID = null;
                }
                else
                {
                    model.DON_VI_ID = donvicha.ID;
                }
                //var lstdinhmuc = _itemService.GetListDinhMucByDonViIds(model.DON_VI_ID);
                //var lstdinhmucdadong = lstdinhmuc.Where(c => c.TU_NGAY <= model.TU_NGAY && model.TU_NGAY <= c.DEN_NGAY).ToList();
                //if (lstdinhmucdadong.Count>0 ) {
                //    return JsonErrorMessage("Đã có quyết định khác tồn tại");
                //}
                //var lstdinhmucchuadong = lstdinhmuc.Where(c => c.TU_NGAY <= model.TU_NGAY && !c.DEN_NGAY.HasValue).ToList();
                //if (lstdinhmucchuadong.Count > 0)
                //{
                //    foreach(var dinhmucchuadong in lstdinhmucchuadong)
                //    {
                //        dinhmucchuadong.DEN_NGAY = DateTime.Now.AddDays(-1);
                //        _itemService.UpdateDinhMuc(dinhmucchuadong);
                //    }
                //}

                model.MA = CommonHelper.GenTreeNode(null, model.ID, null);
                var item = model.ToEntity<DinhMuc>();                
                _itemService.InsertDinhMuc(item);
                if (model.ChiTietDinhMuc.Count > 0)
                {
                    foreach(var chitietdinhmuc in model.ChiTietDinhMuc)
                    {
                        chitietdinhmuc.DINH_MUC_ID = item.ID;
                        //var checktrung =  model.ChiTietDinhMuc.Where(c => c.CHUC_DANH_ID == chitietdinhmuc.CHUC_DANH_ID && c.LOAI_TAI_SAN_ID == chitietdinhmuc.LOAI_TAI_SAN_ID).ToList();
                        //if (checktrung.Count > 1)
                        //{
                        //    return JsonErrorMessage("Chi tiết định mức tạo đã bị trùng", chitietdinhmuc);
                        //}
                        _dinhMucChiTietService.InsertDinhMucChiTiet(chitietdinhmuc.ToEntity<DinhMucChiTiet>());
                    }
                }
                _hoatdongService.InsertHoatDong("TaoMoi", "Tạo mới thông tin ", item.ToModel<DinhMucModel>(), "DinhMuc");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return JsonSuccessMessage("Cập nhật thành công");
            }

            ////prepare model
            //model = _itemModelFactory.PrepareDinhMucModel(model, null);            
            //return View(model);
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return JsonErrorMessage("Thêm mới không thành công, kiểm tra lại dữ liệu", list);
        } 
        
        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDinhMuc))
                return AccessDeniedView();
                
            var item = _itemService.GetDinhMucById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareDinhMucModel(null, item);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Edit(DinhMucModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDinhMuc))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDinhMucById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            //check trùng chi tiết dinhmuc
            foreach (var chiTietDinhMuc in model.ChiTietDinhMuc)
            {
                var key = string.Format("err_DINH_MUC_CHITIET_" + chiTietDinhMuc._arr);
                var value = "";
                foreach (var chitietdinhmuc1 in model.ChiTietDinhMuc)
                {
                    if (chiTietDinhMuc._arr != chitietdinhmuc1._arr)
                    {
                        if (chiTietDinhMuc.LOAI_TAI_SAN_ID == chitietdinhmuc1.LOAI_TAI_SAN_ID && chiTietDinhMuc.CHUC_DANH_ID == chitietdinhmuc1.CHUC_DANH_ID)
                        {
                            value = "Chi tiết khai thác đã tồn tại";
                            ModelState.AddModelError(key, value);
                            value = "";
                        }
                    }
                }
                var lstdinhmucids = _itemService.GetAllDinhMucs().Where(c => c.DON_VI_ID == (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG || c.DON_VI_ID == model.DON_VI_ID).Select(c=>c.ID).ToList<decimal>();
                var lstchitietDinhmuc = _dinhMucChiTietService.GetAllDinhMucChiTiets().Where(c => c.CHUC_DANH_ID == chiTietDinhMuc.CHUC_DANH_ID && c.LOAI_TAI_SAN_ID == chiTietDinhMuc.LOAI_TAI_SAN_ID && lstdinhmucids.Contains( c.DINH_MUC_ID) && c.ID != chiTietDinhMuc.ID).ToList();
                //var lstdinhmucid = lstchitietDinhmuc.Select(c => c.DINH_MUC_ID).ToArray();
                //var lstdinhmuc = _itemService.GetDinhMucByIds(lstdinhmucid).Where(c => c.TU_NGAY <= model.TU_NGAY || model.TU_NGAY < c.DEN_NGAY);
                if (lstchitietDinhmuc.Count > 0)
                {
                    value = "Quyết định cũ chứa định mức vẫn còn hiệu lực";
                }
                if (!string.IsNullOrEmpty(value))
                {
                    ModelState.AddModelError(key, value);
                }
                if (chiTietDinhMuc.LOAI_TAI_SAN_ID == 0)
                {
                    ModelState.AddModelError(string.Format("err_LOAI_TAI_SAN_ID_" + chiTietDinhMuc._arr), "Loại tài sản không được để trống");
                }
                string MaLoaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(chiTietDinhMuc.LOAI_TAI_SAN_ID)?.MA;
                if (chiTietDinhMuc.CHUC_DANH_ID == 0 && int.Parse(MaLoaiTaiSan) == (int)enumLoaiTaiSanOto.OTO_CHUC_DANH)
                {
                    ModelState.AddModelError(string.Format("err_MA_CHUC_DANH_" + chiTietDinhMuc._arr), "Chức danh không được để trống");
                }
            }

            if (ModelState.IsValid)
            {

                var donviid = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
                var donvicha = _donViService.GetDonViLonNhat(donviid.ID, donviid.TREE_NODE);
                if (donviid.ID == (int)enumDonVi.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG || donvicha.ID == (int)enumMaDPAC.TRUNG_TAM_DU_LIEU_QUOC_GIA_VE_TS_CONG)
                {
                    model.DON_VI_ID = null ;
                }
                else
                {
                    model.DON_VI_ID = donvicha.ID;
                }
                _itemModelFactory.PrepareDinhMuc(model,item);
                //var lstdinhmuc = _itemService.GetListDinhMucByDonViIds(model.DON_VI_ID).Where(c=>c.ID != model.ID).ToList();
                //var lstdinhmucdadong = lstdinhmuc.Where(c => c.TU_NGAY <= model.TU_NGAY && model.TU_NGAY <= c.DEN_NGAY).ToList();
                //if (lstdinhmucdadong.Count > 0)
                //{
                //    return JsonErrorMessage("Đã có quyết định khác tồn tại");
                //}
                //var lstdinhmucchuadong = lstdinhmuc.Where(c => c.TU_NGAY <= model.TU_NGAY && !c.DEN_NGAY.HasValue).ToList();
                //if (lstdinhmucchuadong.Count > 0)
                //{
                //    foreach (var dinhmucchuadong in lstdinhmucchuadong)
                //    {
                //        dinhmucchuadong.DEN_NGAY = DateTime.Now.AddDays(-1);
                //        _itemService.UpdateDinhMuc(dinhmucchuadong);
                //    }
                //}
                _itemService.UpdateDinhMuc(item);
                if (model.ChiTietDinhMuc.Count > 0)
                {
                    foreach (var chitietdinhmuc in model.ChiTietDinhMuc)
                    {
                        chitietdinhmuc.DINH_MUC_ID = item.ID;
                        var chitietdinhmucitem = _dinhMucChiTietService.GetDinhMucChiTietByDinhMucIdChucDanhIdAndLoaiTaiSanId(DinhMucId: chitietdinhmuc.DINH_MUC_ID, ChucDanhId: chitietdinhmuc.CHUC_DANH_ID, LoaiTaiSanId: chitietdinhmuc.LOAI_TAI_SAN_ID);
                        if(chitietdinhmucitem!= null)
                        {
                            _dinhMucChiTietModelFactory.PrepareDinhMucChiTiet(chitietdinhmuc,chitietdinhmucitem);
                            if(!chitietdinhmucitem.LOAI_HINH_TAI_SAN_ID.HasValue || chitietdinhmucitem.LOAI_HINH_TAI_SAN_ID == 0)
                            {
                                chitietdinhmucitem.LOAI_HINH_TAI_SAN_ID = _loaiTaiSanService.GetLoaiTaiSanById(chitietdinhmucitem.LOAI_TAI_SAN_ID).LOAI_HINH_TAI_SAN_ID;
                            }
                            _dinhMucChiTietService.UpdateDinhMucChiTiet(chitietdinhmucitem);
                        }
                        else { 
                            _dinhMucChiTietService.InsertDinhMucChiTiet(chitietdinhmuc.ToEntity<DinhMucChiTiet>());
                        }
                    }
                }
                _hoatdongService.InsertHoatDong("CapNhat", "Cập nhật thông tin ", item.ToModel<DinhMucModel>(), "DinhMuc");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return JsonSuccessMessage("Cập nhật thành công");
            }
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            //prepare model
            model = _itemModelFactory.PrepareDinhMucModel(model, item, true);
            return JsonErrorMessage("Cập nhật không thành công, kiểm tra lại dữ liệu", list);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDinhMuc))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDinhMucById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                var lstdinhmucchitiet = _dinhMucChiTietService.GetDinhMucChiTietByDinhMucId(item.ID);
                if (lstdinhmucchitiet.Count > 0)
                {
                    foreach(var dinhmucchitiet in lstdinhmucchitiet)
                    {
                        _dinhMucChiTietService.DeleteDinhMucChiTiet(dinhmucchitiet);
                    }
                }
                _itemService.DeleteDinhMuc(item);
                _hoatdongService.InsertHoatDong("Xoa", "Xóa dữ liệu ", item.ToModel<DinhMucModel>(), "DinhMuc");
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
        public virtual IActionResult DeleteDinhMucChiTiet(decimal DinhMucId, decimal ChucDanhId, decimal LoaiTaiSanId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDinhMuc))
                return AccessDeniedView();
            //try to get a store with the specified id
            if (DinhMucId > 0)
            {
                var item = _dinhMucChiTietService.GetDinhMucChiTietByDinhMucIdChucDanhIdAndLoaiTaiSanId(DinhMucId: DinhMucId, ChucDanhId: ChucDanhId, LoaiTaiSanId: LoaiTaiSanId);
                if (item == null)
                    return JsonErrorMessage("Xóa dữ liệu chi tiết thành công");
                try
                {
                    _dinhMucChiTietService.DeleteDinhMucChiTiet(item);
                    _hoatdongService.InsertHoatDong("Xoa", "Xóa dữ liệu ", item.ToModel<DinhMucChiTietModel>(), "DinhMucChiTiet");
                    //activity log  
                    return JsonSuccessMessage("Xoá dữ liệu chi tiết thành công");
                }
                catch (Exception exc)
                {
                    ErrorNotification(exc);
                    return JsonErrorMessage("Xóa dữ liệu chi tiết thành công");
                }
            }
            else
                return JsonSuccessMessage("Xóa dữ liệu chi tiết thành công");
        }
        public virtual IActionResult View(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDinhMuc))
                return AccessDeniedView();
                
            var item = _itemService.GetDinhMucById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareDinhMucModel(null, item);
            return View(model);
        }
        [HttpPost]
        public virtual IActionResult _AddNewChiTiet(decimal? DinhMucID, decimal? ChucDanhId, decimal? LoaiTaiSanId, decimal? LoaiHinhTaiSan)
        {
            if (DinhMucID>0)
            {  
                var ChiTietDinhMuc = _dinhMucChiTietService.GetDinhMucChiTietByDinhMucIdChucDanhIdAndLoaiTaiSanId(DinhMucID??0,ChucDanhId:ChucDanhId??0,LoaiTaiSanId:LoaiTaiSanId??0);
                //chiTiet.
                if (ChiTietDinhMuc != null) { 
                    var model = new DinhMucChiTietModel();
                    model.LOAI_TAI_SAN_ID = ChiTietDinhMuc.LOAI_TAI_SAN_ID;
                    model.LOAI_HINH_TAI_SAN_ID = ChiTietDinhMuc.LOAI_HINH_TAI_SAN_ID;
                    model.SO_LUONG = ChiTietDinhMuc.SO_LUONG;
                    model.DINH_MUC = ChiTietDinhMuc.DINH_MUC;
                    //model.TEN_CHUC_DANH = ChiTietDinhMuc.TEN_CHUC_DANH;
                    model.TEN_CHUC_DANH = _chucDanhService.GetChucDanhById(ChiTietDinhMuc.CHUC_DANH_ID).TEN_CHUC_DANH;
                    //model.TEN_NHOM_TAI_SAN = ChiTietDinhMuc.TEN_NHOM_TAI_SAN;
                    model.TEN_NHOM_TAI_SAN = _loaiTaiSanService.GetLoaiTaiSanById(ChiTietDinhMuc.LOAI_TAI_SAN_ID).TEN;
                    model.CHUC_DANH_ID = ChiTietDinhMuc.CHUC_DANH_ID;
                    model.DDLChucDanh = _chucDanhModelFactory.PrepareSelectListChucDanh(valSelected: model.CHUC_DANH_ID);

                    var loaiHinhTaiSanModels = _loaiTaiSanModelFactory.PrepareListLoaiHinhTaiSanDinhMucModel();
                    model.DDLloaiHinhTaiSan = loaiHinhTaiSanModels.Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString(),
                        Selected = model.LOAI_HINH_TAI_SAN_ID == c.Id
                    }).OrderBy(c => c.Text).ToList();

                    if (model.LOAI_TAI_SAN_ID > 0)
                    {
                        var loaihinhtaisanid = _loaiTaiSanService.GetLoaiTaiSanById(model.LOAI_TAI_SAN_ID).LOAI_HINH_TAI_SAN_ID;
                        var selectList = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(valSelected: model.LOAI_TAI_SAN_ID, loaiHinhTaiSanId: model.LOAI_HINH_TAI_SAN_ID); ;
                        if (loaihinhtaisanid == (int)enumLOAI_HINH_TAI_SAN.OTO)
                        {
                            var loaitaisanoto = _loaiTaiSanService.GetLoaiTaiSanByTreeLevel(2);
                            selectList = loaitaisanoto.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaihinhtaisanid && c.CHE_DO_HAO_MON_ID == 23)
                                .Select(c => new SelectListItem
                                {
                                    Text = c.TEN,
                                    Value = c.ID.ToString(),
                                    Selected = c.ID == model.LOAI_TAI_SAN_ID

                                }).ToList();
                        }
                        model.DDLNhomTaiSan = selectList;
                    }
                    return PartialView(model);
                }
                else
                {
                    var model = new DinhMucChiTietModel();
                    model.DDLChucDanh = _chucDanhModelFactory.PrepareSelectListChucDanh();
                    //model.DDLNhomTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan();
                    model.DDLNhomTaiSan = new List<SelectListItem>();
                    model.DDLNhomTaiSan.AddFirstRow("--Chọn loại tài sản");
                    model.DDLloaiHinhTaiSan = _loaiTaiSanModelFactory.PrepareListLoaiHinhTaiSanDinhMucModel().Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }).ToList();
                    model.DDLloaiHinhTaiSan.AddFirstRow("--Chọn loại hình tài sản");
                    return PartialView(model);
                }
                //model.DDLloaiHinhTaiSan model.LOAI_HINH_TAI_SAN_ID

            }
            else
            {
                var model = new DinhMucChiTietModel();
                model.DDLChucDanh = _chucDanhModelFactory.PrepareSelectListChucDanh(isAddFirst:true);
                //model.DDLNhomTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan();
                model.DDLNhomTaiSan = new List<SelectListItem>();
                model.DDLNhomTaiSan.AddFirstRow("--Chọn loại tài sản");
                model.DDLloaiHinhTaiSan = _loaiTaiSanModelFactory.PrepareListLoaiHinhTaiSanDinhMucModel().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList();
                model.DDLloaiHinhTaiSan.AddFirstRow("--Chọn loại hình tài sản");
                return PartialView(model);
            }
        }


        [HttpPost]
        public virtual IActionResult CheckCollisionDateRange(DateTime tungay, DateTime denngay)
        {
            var donvi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            var donvitinh = _donViService.GetDonViLonNhat(donvi.ID, donvi.TREE_NODE);
            var listDinhMuc = _itemService.GetListDinhMucByDonViIds(donviId: donvitinh.ID);
            foreach (var DinhMuc in listDinhMuc)
            {
                if (DateTime.Compare(tungay, DinhMuc.DEN_NGAY.Value) < 0 && DateTime.Compare(denngay, DinhMuc.TU_NGAY) > 0)
                {
                    return JsonErrorMessage("collided");
                }
            }
            return JsonSuccessMessage("ok");
        }
        [HttpPost]
        public virtual IActionResult getddlloaitaisan(decimal loaihinhtaisan, string str = null)
        {
            var selectList = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(loaiHinhTaiSanId: loaihinhtaisan, strFirstRow: str);
            if (loaihinhtaisan == (int)enumLOAI_HINH_TAI_SAN.OTO)
            {
                var loaitaisanoto = _loaiTaiSanService.GetLoaiTaiSanByTreeLevel(2);
                selectList = loaitaisanoto.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaihinhtaisan && c.CHE_DO_HAO_MON_ID == 23)
                    .Select(c => new SelectListItem
                    {
                        Text = c.TEN,
                        Value = c.ID.ToString()
                    }).ToList();
            }
            selectList.AddFirstRow("Chọn loại tài sản");
            return Json(selectList);
        }
        #endregion
    }
}

