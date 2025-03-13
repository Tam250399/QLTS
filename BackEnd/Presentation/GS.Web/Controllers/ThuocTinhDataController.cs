//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.ThuocTinhs;
using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.ThuocTinh;
using GS.Web.Controllers;
using GS.Web.Factories.ThuocTinhs;
using GS.Services.ThuocTinhs;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using System.Collections.Generic;
using System.Linq;
using GS.Services.DanhMuc;
using GS.Services.TaiSans;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class ThuocTinhDataController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly IThuocTinhDataModelFactory _itemModelFactory;
            private readonly IThuocTinhDataService _itemService;
            private readonly IThuocTinhModelFactory _thuocTinhModelFactory;
            private readonly IDonViBoPhanService _donViBoPhanService;
            private readonly ITaiSanService _taiSanService;
            private readonly IThuocTinhService _thuocTinhService;
         #endregion
     
        #region Ctor
        public ThuocTinhDataController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IThuocTinhDataModelFactory itemModelFactory,
            IThuocTinhDataService itemService,
            IThuocTinhModelFactory thuocTinhModelFactory,
            IDonViBoPhanService donViBoPhanService,
            ITaiSanService taiSanService,
            IThuocTinhService thuocTinhService
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
            this._donViBoPhanService = donViBoPhanService;
            this._taiSanService = taiSanService;
            this._thuocTinhService = thuocTinhService;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new ThuocTinhDataSearchModel ();
            var model = _itemModelFactory.PrepareThuocTinhDataSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(ThuocTinhDataSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareThuocTinhDataListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareThuocTinhDataModel(new ThuocTinhDataModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(ThuocTinhDataModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<ThuocTinhData>();                
                _itemService.InsertThuocTinhData(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareThuocTinhDataModel(model, null);            
            return View(model);
        } 
        public virtual IActionResult ViewThuocTinhTaiSan(string LoaiHinhTaiSanId=null,int LoaiTaiSanId=0,int TaiSanId =0)
        {
            var lhts = 0;
            if (LoaiHinhTaiSanId != null)
            {
                lhts = (LoaiHinhTaiSanId.Split(",")[0]).ToNumberInt32();
            }
            var model = _itemModelFactory.PrepareListmodelThuocTinh(LoaiHinhTaiSan: lhts, LoaiTaiSan: LoaiTaiSanId,TaiSanId:TaiSanId).OrderBy(c=>c.SapXep).ToList();
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult ViewThuocTinhTaiSan(IList<modelThuocTinh> listmodel,int TaiSanId = 0)
        {
            
            if (listmodel.Count() > 0 && TaiSanId!=0)
            {
                //check vali
                var listvali = listmodel.Where(c => c.IS_VALIDATE == true && (c.VALUE == null || c.VALUE == "") && c.LoaiDuLieuId!= (int)enumLoaiDuLieuCauHinh.OBJ);
                if (listvali!= null && listvali.Count()>0)
                {
                    return JsonErrorMessage("Cập nhật không thành công", TaiSanId);
                }
                    try
                {
                    var taisan = _taiSanService.GetTaiSanById(TaiSanId);
                    var listitem = _thuocTinhModelFactory.ToThuocTinhEntites(listmodel: listmodel.ToList());
                    foreach (var item in listitem)
                    {
                        var ttDaTa = _itemService.GetThuocTinhDataByTaiSanIdAndData(Data: item.GUID.ToString(), TaiSanId: TaiSanId);
                        if (ttDaTa != null)
                        {
                            ttDaTa.NGAY_CAP_NHAT = DateTime.Now;
                            ttDaTa.DATA = item.toStringJson();
                            _itemService.UpdateThuocTinhData(ttDaTa);
                            _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", ttDaTa.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                        }
                        else
                        {
                            var tt = _thuocTinhService.GetAllThuocTinhs(CauHinh: item.GUID.ToString()).FirstOrDefault();
                            if (tt != null)
                            {
                                ttDaTa = new ThuocTinhData();
                                ttDaTa.NGAY_TAO = DateTime.Now;
                                ttDaTa.THUOC_TINH_ID = tt.ID;
                                ttDaTa.NGAY_CAP_NHAT = DateTime.Now;
                                ttDaTa.TAI_SAN_ID = TaiSanId;
                                ttDaTa.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                                ttDaTa.DON_VI_ID = _workContext.CurrentDonVi.ID;
                                ttDaTa.DON_VI_BO_PHAN_ID = taisan != null ? taisan.DON_VI_BO_PHAN_ID : null;
                                ttDaTa.DATA = item.toStringJson();
                                _itemService.InsertThuocTinhData(ttDaTa);
                                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", ttDaTa.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                            }
                        }
                    }
                    return JsonSuccessMessage("Cập nhật thành công", TaiSanId);
                }
                catch
                {
                    return JsonErrorMessage("Cập nhật không thành công", TaiSanId);
                }
            }
            else
            {
                // k tồn tại chỉ tiêu thống kê
                return JsonSuccessMessage("Cập nhật thành công", TaiSanId);
            }
        }
        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
                
            var item = _itemService.GetThuocTinhDataById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareThuocTinhDataModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(ThuocTinhDataModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetThuocTinhDataById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareThuocTinhData(model,item);
                _itemService.UpdateThuocTinhData(item); 
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareThuocTinhDataModel(model, item, true);
            return View(model);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetThuocTinhDataById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteThuocTinhData(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<ThuocTinhDataModel>(), "ThuocTinhData");
                //activity log  
                SuccessNotification("Xoá dữ liệu thành công");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = item.ID });
            }
        }
        #endregion       			
	}
}

