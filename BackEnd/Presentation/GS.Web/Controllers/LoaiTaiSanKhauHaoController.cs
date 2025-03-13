//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/6/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Mvc;
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;

using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.DanhMuc;
using GS.Web.Controllers;
using GS.Web.Factories.DanhMuc;
using GS.Services.DanhMuc;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Framework.Extensions;
using GS.Web.Factories.HeThong;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class LoaiTaiSanKhauHaoController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly ILoaiTaiSanKhauHaoModelFactory _itemModelFactory;
            private readonly ILoaiTaiSanKhauHaoService _itemService;
            private readonly ILoaiTaiSanService _loaiTaiSanService;
            private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
		private readonly ICauHinhModelFactory _cauHinhModelFactory;

		#endregion

		#region Ctor
		public LoaiTaiSanKhauHaoController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ILoaiTaiSanKhauHaoModelFactory itemModelFactory,
            ILoaiTaiSanKhauHaoService itemService,
            ILoaiTaiSanService loaiTaiSanService,
            ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
            ICauHinhModelFactory cauHinhModelFactory
            )
        {            
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;            
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
			_cauHinhModelFactory = cauHinhModelFactory;
		}
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucLoaiTaiSanKhauHao))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new LoaiTaiSanKhauHaoSearchModel();
            var model = _itemModelFactory.PrepareLoaiTaiSanKhauHaoSearchModel(searchmodel);
            //var searchmodel = new LoaiTaiSanKhauHaoSearchModel ();
            //var model = _loaiTaiSanModelFactory.PrepareLoaiTaiSanKhauHaoSearchModel(searchmodel);
            //session
            var _searchModel = HttpContext.Session.GetObject<LoaiTaiSanKhauHaoSearchModel>(enumTENCAUHINH.KeySearch + typeof(LoaiTaiSanKhauHaoSearchModel).Name);
            if (_searchModel != null && _searchModel.IsLoadSession)
            {
                _cauHinhModelFactory.PrePareModelBySession(model, _searchModel);
                UpdateSessionSearchModel<LoaiTaiSanKhauHaoSearchModel>(false);
            }
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(LoaiTaiSanKhauHaoSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucLoaiTaiSanKhauHao))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareLoaiTaiSanKhauHaoListModel(searchModel);
            //session
            if (searchModel.ParentId == null)
                HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch + searchModel.GetType().Name, searchModel);
            return Json(model);
        }

        public virtual IActionResult Create(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucLoaiTaiSanKhauHao))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareLoaiTaiSanKhauHaoModel(new LoaiTaiSanKhauHaoModel(), null);
            var itemlts = _loaiTaiSanService.GetLoaiTaiSanById(id);
            var modellts = _loaiTaiSanModelFactory.PrepareLoaiTaiSanModel(null, itemlts);
            model.TEN_LOAI_TAI_SAN = modellts.TEN;
            model.LOAI_HINH_TAI_SAN_ID = modellts.LOAI_HINH_TAI_SAN_ID;
            model.LOAI_TAI_SAN_ID = modellts.ID;
            model.DON_VI_ID = _workContext.CurrentDonVi.ID;
            model.MA_LOAI_TAI_SAN = modellts.MA;
            model.MA_DON_VI = _workContext.CurrentDonVi.MA_DON_VI;
            model.TEN_DON_VI = _workContext.CurrentDonVi.TEN_DON_VI;
            
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(LoaiTaiSanKhauHaoModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucLoaiTaiSanKhauHao))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<LoaiTaiSanKhauHao>();                
                _itemService.InsertLoaiTaiSanKhauHao(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<LoaiTaiSanKhauHaoModel>(), "LoaiTaiSanKhauHao");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                UpdateSessionSearchModel<LoaiTaiSanKhauHaoSearchModel>(true);
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareLoaiTaiSanKhauHaoModel(model, null);
            var itemlts = _loaiTaiSanService.GetLoaiTaiSanById(model.LOAI_TAI_SAN_ID);
            var modellts = _loaiTaiSanModelFactory.PrepareLoaiTaiSanModel(null, itemlts);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucLoaiTaiSanKhauHao))
                return AccessDeniedView();

            //var item = _itemService.GetLoaiTaiSanKhauHaoById(id);
            var item = _itemService.GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(loaiTaiSanId: id, donViId: _workContext.CurrentDonVi.ID);
            //if (item == null)
            //{
            //    var model = _itemModelFactory.PrepareLoaiTaiSanKhauHaoModel(new LoaiTaiSanKhauHaoModel(), null);
            //}
            //prepare model
            
            var model = _itemModelFactory.PrepareLoaiTaiSanKhauHaoModel(null, item);
            var itemlts = _loaiTaiSanService.GetLoaiTaiSanById(id);
            var modellts = _loaiTaiSanModelFactory.PrepareLoaiTaiSanModel(null, itemlts);
            model.TEN_LOAI_TAI_SAN = modellts.TEN;
            model.LOAI_HINH_TAI_SAN_ID = modellts.LOAI_HINH_TAI_SAN_ID;
            model.LOAI_TAI_SAN_ID = modellts.ID;
            model.DON_VI_ID = _workContext.CurrentDonVi.ID;
            model.MA_LOAI_TAI_SAN = modellts.MA;
            model.MA_DON_VI = _workContext.CurrentDonVi.MA_DON_VI;
            model.TEN_DON_VI = _workContext.CurrentDonVi.TEN_DON_VI;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(LoaiTaiSanKhauHaoModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucLoaiTaiSanKhauHao))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(loaiTaiSanId: model.LOAI_TAI_SAN_ID,donViId: _workContext.CurrentDonVi.ID);
            if (item == null)
                return RedirectToAction("Create", new { id = model.LOAI_TAI_SAN_ID });
            if (ModelState.IsValid)
            {
                if(model.DON_VI_ID == item.DON_VI_ID)
                {
                    _itemModelFactory.PrepareLoaiTaiSanKhauHao(model, item);
                    _itemService.UpdateLoaiTaiSanKhauHao(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<LoaiTaiSanKhauHaoModel>(), "LoaiTaiSanKhauHao");
                    SuccessNotification("Cập nhật dữ liệu thành công !");
                    //session
                    UpdateSessionSearchModel<LoaiTaiSanKhauHaoSearchModel>(true);
                    return continueEditing ? RedirectToAction("Edit", new { id = item.LOAI_TAI_SAN_ID }) : RedirectToAction("List");
                }
                else
                {
                    var itemi = model.ToEntity<LoaiTaiSanKhauHao>();
                    _itemService.InsertLoaiTaiSanKhauHao(itemi);
                    _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", itemi.ToModel<LoaiTaiSanKhauHaoModel>(), "LoaiTaiSanKhauHao");
                    SuccessNotification("Tạo mới dữ liệu thành công !");
                    UpdateSessionSearchModel<LoaiTaiSanKhauHaoSearchModel>(true);
                    return continueEditing ? RedirectToAction("Edit", new { id = itemi.LOAI_TAI_SAN_ID }) : RedirectToAction("List");
                }
            }
            //prepare model
            model = _itemModelFactory.PrepareLoaiTaiSanKhauHaoModel(model, item, true);
            var itemlts = _loaiTaiSanService.GetLoaiTaiSanById(model.LOAI_TAI_SAN_ID);
            var modellts = _loaiTaiSanModelFactory.PrepareLoaiTaiSanModel(null, itemlts);
            return View(modellts);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucLoaiTaiSanKhauHao))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(loaiTaiSanId: id, donViId: _workContext.CurrentDonVi.ID);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteLoaiTaiSanKhauHao(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<LoaiTaiSanKhauHaoModel>(), "LoaiTaiSanKhauHao");
                //activity log  
                SuccessNotification("Xoá dữ liệu thành công");
                //session
                UpdateSessionSearchModel<LoaiTaiSanKhauHaoSearchModel>(true);
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = item.ID });
            }
        }

        public virtual IActionResult Checkltskh(int idlts)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDanhMucLoaiTaiSanKhauHao))
                return AccessDeniedView();

            var item = _itemService.GetLoaiTaiSanKhauHaoByDonViIdAndLoaiTaiSanId(loaiTaiSanId: idlts, donViId: _workContext.CurrentDonVi.ID);
            if (item == null)
                return RedirectToAction("Create", new { id = idlts });
            //prepare model
            
            return RedirectToAction("Edit", new { id = item.LOAI_TAI_SAN_ID });
        }
        #endregion
    }
}

