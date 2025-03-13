//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.CCDC;

using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.CCDC;
using GS.Web.Controllers;
using GS.Web.Factories.CCDC;
using GS.Services.CCDC;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Services.DanhMuc;
using System.Linq;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class KiemKeCongCuController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly IKiemKeCongCuModelFactory _itemModelFactory;
            private readonly IKiemKeCongCuService _itemService;
        private readonly ICongCuService _congCuService;
        private readonly INhapXuatCongCuService _nhapXuatCongCuService;
        private readonly IXuatNhapService _xuatNhapService;
        private readonly INhomCongCuService _nhomCongCuService;
         #endregion
     
        #region Ctor
        public KiemKeCongCuController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IKiemKeCongCuModelFactory itemModelFactory,
            IKiemKeCongCuService itemService,
            ICongCuService congCuService,
            INhapXuatCongCuService nhapXuatCongCuService,
            IXuatNhapService xuatNhapService,
            INhomCongCuService nhomCongCuService
            )
        {            
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;            
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._congCuService = congCuService;
            this._nhapXuatCongCuService = nhapXuatCongCuService;
            this._xuatNhapService = xuatNhapService;
            this._nhomCongCuService = nhomCongCuService;
        }
        #endregion
        #region Methods

        public virtual IActionResult List(int KiemKeId = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new KiemKeCongCuSearchModel ();
            var model = _itemModelFactory.PrepareKiemKeCongCuSearchModel(searchmodel);
            searchmodel.KiemKeId = KiemKeId;
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult List(KiemKeCongCuSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareKiemKeCongCuListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult ListCongCuThua(int KiemKeId=0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new KiemKeCongCuSearchModel();
            var model = _itemModelFactory.PrepareKiemKeCongCuSearchModel(searchmodel);
            searchmodel.KiemKeId = KiemKeId;
            searchmodel.isPhatHienThua = true;
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult ListCongCuThua(KiemKeCongCuSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareKiemKeCongCuListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create(Decimal KiemKeId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareKiemKeCongCuModel(new KiemKeCongCuModel(), null);
            model.IS_PHAT_HIEN_THUA = true;
            model.KIEM_KE_ID = KiemKeId;
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult Create(KiemKeCongCuModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<KiemKeCongCu>();
                _itemService.InsertKiemKeCongCu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<KiemKeCongCuModel>(), "KiemKeCongCu");
                return JsonSuccessMessage("Tạo mới dữ liệu thành công !"); ;
            }

            //prepare model
            //model = _itemModelFactory.PrepareKiemKeCongCuModel(model, null);            
            //return View(model);
            var listERR = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Cập nhật không thành công", listERR);
        } 
        
        public virtual IActionResult Edit(int id, bool isPhatHienThua = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
                
            var item = _itemService.GetKiemKeCongCuById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareKiemKeCongCuModel(null, item);
            model.IS_PHAT_HIEN_THUA = isPhatHienThua;
            model.KiemKeCongCuId = id;
            if (!isPhatHienThua)
            {
                var congcu = _congCuService.GetCongCuById((Decimal)item.CONG_CU_ID);
                model.MaCongCu = congcu.MA;
                model.TenCongCu = congcu.TEN;
            }
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult Edit(KiemKeCongCuModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKiemKeCongCuById(model.KiemKeCongCuId);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareKiemKeCongCu(model,item);
                _itemService.UpdateKiemKeCongCu(item); 
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<KiemKeCongCuModel>(), "KiemKeCongCu");
                return JsonSuccessMessage("Cập nhật dữ liệu thành công !");
            }
            //prepare model
            //model = _itemModelFactory.PrepareKiemKeCongCuModel(model, item, true);
            //return View(model);
            var listERR = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Cập nhật không thành công", listERR);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLKiemKeCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKiemKeCongCuById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteKiemKeCongCu(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<KiemKeCongCuModel>(), "KiemKeCongCu");
                //activity log  
                return JsonSuccessMessage("Xoá dữ liệu thành công");
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

