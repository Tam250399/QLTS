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


namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class TaiSanKiemKeTaiSanController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly ITaiSanKiemKeTaiSanModelFactory _itemModelFactory;
            private readonly ITaiSanKiemKeTaiSanService _itemService;
			private readonly ITaiSanKiemKeService _taiSanKiemKeService;
         #endregion
     
        #region Ctor
        public TaiSanKiemKeTaiSanController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ITaiSanKiemKeTaiSanModelFactory itemModelFactory,
            ITaiSanKiemKeTaiSanService itemService,
			ITaiSanKiemKeService taiSanKiemKeService
			)
        {            
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;            
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
			this._taiSanKiemKeService = taiSanKiemKeService;

		}
        #endregion
        #region Methods

        public virtual IActionResult List(Decimal kiemKeId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanKiemKeTaiSanSearchModel ();
            searchmodel.KiemKeId = kiemKeId;
            searchmodel.isTaiSanThua = false;
            var model = _itemModelFactory.PrepareTaiSanKiemKeTaiSanSearchModel(searchmodel);
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult List(TaiSanKiemKeTaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanKiemKeTaiSanListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult ListTaiSanThua(Decimal kiemKeId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new TaiSanKiemKeTaiSanSearchModel();
            searchmodel.KiemKeId = kiemKeId;
            searchmodel.isTaiSanThua = true;
            var model = _itemModelFactory.PrepareTaiSanKiemKeTaiSanSearchModel(searchmodel);
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult ListTaiSanThua(TaiSanKiemKeTaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanKiemKeTaiSanListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create(Decimal KiemKeId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanKiemKeTaiSanModel(new TaiSanKiemKeTaiSanModel(), null);
            model.IS_PHAT_HIEN_THUA = true;
            model.KIEM_KE_ID = KiemKeId;
            model.SO_LUONG_KIEM_KE = 1;
            return PartialView(model);
        }

		/// <summary>
		/// Tạo tài sản thừa
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
        [HttpPost]
        public virtual IActionResult Create(TaiSanKiemKeTaiSanModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<TaiSanKiemKeTaiSan>();
                item.IS_PHAT_HIEN_THUA = true;
				item.DON_VI_ID = _taiSanKiemKeService.GetTaiSanKiemKeById(item.KIEM_KE_ID).DON_VI_ID;
				_itemService.InsertTaiSanKiemKeTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<TaiSanKiemKeTaiSanModel>(), "TaiSanKiemKeTaiSan");
                return JsonSuccessMessage("Tạo mới dữ liệu thành công !");
            }

            //prepare model
            model = _itemModelFactory.PrepareTaiSanKiemKeTaiSanModel(model, null);            
            return View(model);
        } 
        
        public virtual IActionResult Edit(int Id, bool isTaiSanThua = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
                
            var item = _itemService.GetTaiSanKiemKeTaiSanById(Id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareTaiSanKiemKeTaiSanModel(null, item);
            model.IS_PHAT_HIEN_THUA = isTaiSanThua;
            model.TaiSanKeKhaiId = Id;
            model.SoLuongKiemKe = item.SO_LUONG_SO_SACH;
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult Edit(TaiSanKiemKeTaiSanModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanKiemKeTaiSanById(model.TaiSanKeKhaiId);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareTaiSanKiemKeTaiSan(model,item);
                _itemService.UpdateTaiSanKiemKeTaiSan(item); 
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<TaiSanKiemKeTaiSanModel>(), "TaiSanKiemKeTaiSan");
                return JsonSuccessMessage("Cập nhật dữ liệu thành công !") ;
            }
            //prepare model
            model = _itemModelFactory.PrepareTaiSanKiemKeTaiSanModel(model, item, true);
            return View(model);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLKiemKeTaiSan))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetTaiSanKiemKeTaiSanById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteTaiSanKiemKeTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<TaiSanKiemKeTaiSanModel>(), "TaiSanKiemKeTaiSan");
                //activity log  
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Detail", "TaiSanKiemKe", new { Id = item.KIEM_KE_ID });
            }
        }
        #endregion
    }
}
