//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.DongBo;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class LyDoBienDongController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly ILyDoBienDongModelFactory _itemModelFactory;
        private readonly ILyDoBienDongService _itemService;
        private readonly IDongBoFactory _dongBoFactory;
        private readonly ITaiSanService _taiSanService;
        #endregion

        #region Ctor
        public LyDoBienDongController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ILyDoBienDongModelFactory itemModelFactory,
            ILyDoBienDongService itemService,
            IDongBoFactory dongBoFactory,
            ITaiSanService taiSanService
            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._dongBoFactory = dongBoFactory;
            this._taiSanService = taiSanService;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLyDoBienDong))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new LyDoBienDongSearchModel();
            var model = _itemModelFactory.PrepareLyDoBienDongSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(LyDoBienDongSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLyDoBienDong))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareLyDoBienDongListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLyDoBienDong))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareLyDoBienDongModel(new LyDoBienDongModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(LyDoBienDongModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLyDoBienDong))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = new LyDoBienDong();
                if(model.SelectedLoaiDonViIds.Count > 0)
                {
                    if(model.SelectedLoaiDonViIds[0] != 0)
                    {
                        model.LOAI_DON_VI += ",";
                        foreach (var ldv in model.SelectedLoaiDonViIds)
                        {
                            model.LOAI_DON_VI += ldv + ",";
                        }
                    }
                    else
                    {
                        model.LOAI_DON_VI = null;
                    }

                }
                if (model.SelectedLoaiHinhTSIds.Count > 0)
                {
                    if (model.SelectedLoaiHinhTSIds[0] != 0)
                    {
                        //model.LOAI_HINH_TAI_SAN_AP_DUNG_ID += "-";
                        //foreach (var lhts in model.SelectedLoaiHinhTSIds)
                        //{
                        //    model.LOAI_HINH_TAI_SAN_AP_DUNG_ID += lhts + "-";
                        //}
                        model.LOAI_HINH_TAI_SAN_AP_DUNG_ID = model.SelectedLoaiHinhTSIds.toStringJson();
                    }
                    else
                    {
                        model.LOAI_HINH_TAI_SAN_AP_DUNG_ID = null;
                    }
                }
                _itemModelFactory.PrepareLyDoBienDong(model, item);
                _itemService.InsertLyDoBienDong(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<LyDoBienDongModel>(), "LyDoBienDong");
                model.ID = item.ID;
                 _dongBoFactory.DongBoDanhMuc<LyDoBienDongModel>(new List<LyDoBienDongModel>() { model });
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareLyDoBienDongModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLyDoBienDong))
                return AccessDeniedView();

            var item = _itemService.GetLyDoBienDongById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareLyDoBienDongModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(LyDoBienDongModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLyDoBienDong))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetLyDoBienDongById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                if (model.SelectedLoaiDonViIds.Count > 0)
                {
                    if (model.SelectedLoaiDonViIds[0] != 0)
                    {
                        model.LOAI_DON_VI += ",";
                        foreach (var ldv in model.SelectedLoaiDonViIds)
                        {
                            model.LOAI_DON_VI += ldv + ",";
                        }
                    }
                    else
                    {
                        model.LOAI_DON_VI = null;
                    }
                }
                if (model.SelectedLoaiHinhTSIds.Count > 0)
                {
                    if (model.SelectedLoaiHinhTSIds[0] != 0)
                    {
                        model.LOAI_HINH_TAI_SAN_AP_DUNG_ID = model.SelectedLoaiHinhTSIds.toStringJson();
                    }
                    else
                    {
                        model.LOAI_HINH_TAI_SAN_AP_DUNG_ID = null;
                    }
                }
                _itemModelFactory.PrepareLyDoBienDong(model, item);

                _itemService.UpdateLyDoBienDong(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<LyDoBienDongModel>(), "LyDoBienDong");
                _dongBoFactory.DongBoDanhMuc<LyDoBienDongModel>(new List<LyDoBienDongModel>() { model });
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareLyDoBienDongModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMLyDoBienDong))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetLyDoBienDongById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                var checktaisan = _taiSanService.SearchTaiSans(LY_DO_BIEN_DONG_ID: item.ID);
                if (checktaisan == null || checktaisan.Count==0) {
                    LyDoBienDongModel model = item.ToModel<LyDoBienDongModel>();
                    _itemService.DeleteLyDoBienDong(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<LyDoBienDongModel>(), "LyDoBienDong");
                    // đồng bộ dữ liệu sang kho
                    _dongBoFactory.XoaDanhMuc<LyDoBienDongModel>(model);
                    //activity log  
                    UpdateSessionSearchModel<LyDoBienDongSearchModel>(true);
                    return JsonSuccessMessage("Xoá dữ liệu thành công");
                }
                else
                {
                    ErrorNotification("Có tài sản được tạo với lý do này");
                    return RedirectToAction("Edit", new { id = item.ID });
                }
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = item.ID });
            }
        }
		public virtual IActionResult GetMaByIdLyDoTangGiam(decimal id)
		{
			var item = _itemService.GetLyDoBienDongById(id);
			if (item!= null)
				return Json(item.MA);
			return Json("");
		}
		#endregion
	}
}

