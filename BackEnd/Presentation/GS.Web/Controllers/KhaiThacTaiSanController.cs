//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/7/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.TaiSans;

using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.TaiSans;
using GS.Web.Controllers;
using GS.Web.Factories.TaiSans;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Collections.Generic;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class KhaiThacTaiSanController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly IKhaiThacTaiSanModelFactory _itemModelFactory;
            private readonly IKhaiThacTaiSanService _itemService;
        private readonly IKhaiThacService _khaiThacService;
        private readonly IKhaiThacChiTietServices _khaiThacChiTietServices;
        #endregion

        #region Ctor
        public KhaiThacTaiSanController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IKhaiThacTaiSanModelFactory itemModelFactory,
            IKhaiThacTaiSanService itemService,
            IKhaiThacService khaiThacService,
            IKhaiThacChiTietServices khaiThacChiTietServices
            )
        {            
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;            
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._khaiThacService = khaiThacService;
            this._khaiThacChiTietServices = khaiThacChiTietServices;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThacTaiSan))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new KhaiThacTaiSanSearchModel ();
            var model = _itemModelFactory.PrepareKhaiThacTaiSanSearchModel(searchmodel);
            return View(model);
        }
         
        [HttpPost]
        public virtual IActionResult List(KhaiThacTaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThacTaiSan))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareKhaiThacTaiSanListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThacTaiSan))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareKhaiThacTaiSanModel(new KhaiThacTaiSanModel(), null);
            return View(model);
        }
   

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(KhaiThacTaiSanModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThacTaiSan))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<KhaiThacTaiSan>();                
                _itemService.InsertKhaiThacTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<KhaiThacTaiSanModel>(), "KhaiThacTaiSan");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareKhaiThacTaiSanModel(model, null);            
            return View(model);
        }
        [HttpGet]
        public virtual IActionResult _CreateOrUpdate(decimal KhaiThacChiTietId, decimal Id = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThacTaiSan))
                return AccessDeniedView();
            var model = new KhaiThacTaiSanModel();
            if (KhaiThacChiTietId > 0)
            {
                
                model.KHAI_THAC_CHI_TIET_ID = KhaiThacChiTietId;
                var KhaiThacChiTiet = _khaiThacChiTietServices.GetKhaiThacChiTietById(KhaiThacChiTietId);
                model.KHAI_THAC_ID = KhaiThacChiTiet?.KHAI_THAC_ID??0;
                model.TAI_SAN_ID = KhaiThacChiTiet.TAI_SAN_ID;
            }
           var khaiThacTaiSan = _itemService.GetKhaiThacTaiSanById(Id);
            //prepare model
            model = _itemModelFactory.PrepareKhaiThacTaiSanModel(model, khaiThacTaiSan);
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult _CreateOrUpdate(KhaiThacTaiSanModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThacTaiSan))
                return AccessDeniedView();
            //try to get a store with the specified id
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQuanLyThuChi))
                return AccessDeniedView();
            try
            {
                
                //_itemModelFactory.CheckValidNgayThuDauTien(ModelState, model);
                if (ModelState.IsValid)
                {
                    if (model.ID > 0)
                    {
                        var item = _itemService.GetKhaiThacTaiSanById(model.ID);// model.ToEntity<ThuChi>();
                        if (item != null)
                        {
                            _itemModelFactory.PrepareKhaiThacTaiSan(model, item);
                            _itemService.UpdateKhaiThacTaiSan(item);
                            _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<KhaiThacTaiSanModel>(), "KhaiThacTaiSan");
                            var SoTienKhaiThacLuyKe = _itemService.TinhSoTienKhaiThacLuyKe(model.KHAI_THAC_ID);
                            return JsonSuccessMessage("Cập nhật thành công", SoTienKhaiThacLuyKe);
                        }
                        return JsonErrorMessage("Có lỗi xảy ra, xin vui lòng thử lại!", new List<ModelStateEntry>() { });
                    }
                    else
                    {
                        var item = model.ToEntity<KhaiThacTaiSan>();
                        _itemService.InsertKhaiThacTaiSan(item);
                        _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<KhaiThacTaiSanModel>(), "KhaiThacTaiSan");
                        var SoTienKhaiThacLuyKe = _itemService.TinhSoTienKhaiThacLuyKe(model.KHAI_THAC_ID);
                        return JsonSuccessMessage("Thêm mới thành công", SoTienKhaiThacLuyKe);
                    }

                }

                var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
                return JsonErrorMessage("Error", list);
            }
            catch (Exception e)
            {

                return JsonErrorMessage("Có lỗi xảy ra, xin vui lòng thử lại!", new List<ModelStateEntry>() { });
            }
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThacTaiSan))
                return AccessDeniedView();
                
            var item = _itemService.GetKhaiThacTaiSanById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareKhaiThacTaiSanModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(KhaiThacTaiSanModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThacTaiSan))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKhaiThacTaiSanById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareKhaiThacTaiSan(model,item);
                _itemService.UpdateKhaiThacTaiSan(item); 
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<KhaiThacTaiSanModel>(), "KhaiThacTaiSan");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareKhaiThacTaiSanModel(model, item, true);
            return View(model);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKhaiThacTaiSan))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKhaiThacTaiSanById(id);
            if (item == null)
                return JsonErrorMessage("Error");
            try
            {
                _itemService.DeleteKhaiThacTaiSan(item);
                var SoTienKhaiThacLuyKe = _itemService.TinhSoTienKhaiThacLuyKe(item.KHAI_THAC_ID);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<KhaiThacTaiSanModel>(), "KhaiThacTaiSan");
                //activity log  
                return JsonSuccessMessage("Xóa thành công", SoTienKhaiThacLuyKe);
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Error", exc);
            }
        }
        #endregion       			
	}
}

