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

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class ThuocTinhController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly IThuocTinhModelFactory _itemModelFactory;
            private readonly IThuocTinhService _itemService;
            private readonly IThuocTinhTaiSanService _thuocTinhTaiSanService;
            private readonly ILoaiTaiSanService _loaiTaiSanService;
        #endregion

        #region Ctor
        public ThuocTinhController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IThuocTinhModelFactory itemModelFactory,
            IThuocTinhService itemService,
            IThuocTinhTaiSanService thuocTinhTaiSanService,
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
            this._thuocTinhTaiSanService = thuocTinhTaiSanService;
            this._loaiTaiSanService = loaiTaiSanService;
        }
        #endregion
            #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new ThuocTinhSearchModel ();
            var model = _itemModelFactory.PrepareThuocTinhSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(ThuocTinhSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareThuocTinhListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareThuocTinhModel(new ThuocTinhModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(IList<modelThuocTinh> ListModel,ThuocTinhModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();

            if (ListModel.Count() > 0)
            {
                var cauhinh = _itemModelFactory.ChuyenDoi(listmodel: ListModel.ToList()).FirstOrDefault();
                if (ModelState.IsValid)
                {
                    if (cauhinh != null)
                    {

                        model.CAU_HINH = cauhinh.toStringJson();
                        model.NGAY_TAO = DateTime.Now;
                        model.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                        model.DON_VI_ID = _workContext.CurrentDonVi.ID;
                        var item = model.ToEntity<ThuocTinh>();
                        _itemService.InsertThuocTinh(item);
                        if(model.ListLoaiTaiSanId !=null && model.ListLoaiTaiSanId.Count() > 0)
                        {
                            foreach (var tt in model.ListLoaiTaiSanId)
                            {
                                var taisan = _loaiTaiSanService.GetLoaiTaiSanById(tt);
                                var ttTaiSan = new ThuocTinhTaiSan();
                                ttTaiSan.LOAI_TAI_SAN_ID = tt;
                                ttTaiSan.SAP_XEP = model.SapXep;
                                ttTaiSan.THUOC_TINH_ID = item.ID;
                                ttTaiSan.LOAI_HINH_TAI_SAN_ID = (decimal)taisan.LOAI_HINH_TAI_SAN_ID;
                                ttTaiSan.TREE_NOTE = taisan.TREE_NODE;
                                _thuocTinhTaiSanService.InsertThuocTinhTaiSan(ttTaiSan);
                                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", ttTaiSan.ToModel<ThuocTinhTaiSanModel>(), "ThuocTinhTaiSan");
                            };
                        }
                        _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<ThuocTinhModel>(), "ThuocTinh");
                        return JsonSuccessMessage("Cập nhật thành công", model); ;
                    }
                }
            }

            //prepare model
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return JsonErrorMessage("Cập không nhập thành công", list);
        } 
        public virtual IActionResult _LoaiDuLieu(Guid guid,int LoaiDuLieuIdParrent,int stt,bool is_viewtong=false)
        {
           var model = _itemModelFactory.PreparemodelThuocTinh(new modelThuocTinh {GuidParrent=guid,LoaiDuLieuIdParrent=LoaiDuLieuIdParrent,stt=stt,is_viewtong=is_viewtong});
            return PartialView(model);
        }
        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
                
            var item = _itemService.GetThuocTinhById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareThuocTinhModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(ThuocTinhModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetThuocTinhById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareThuocTinh(model,item);
                _itemService.UpdateThuocTinh(item); 
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<ThuocTinhModel>(), "ThuocTinh");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareThuocTinhModel(model, item, true);
            return View(model);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetThuocTinhById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteThuocTinh(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<ThuocTinhModel>(), "ThuocTinh");
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

