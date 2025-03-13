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
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class HienTrangController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IHienTrangModelFactory _itemModelFactory;
        private readonly IHienTrangService _itemService;
        private readonly IDongBoFactory _dongBoFactory;
        private readonly IDonViService _donViService;
        private readonly ILoaiDonViService _loaiDonViService;
        #endregion

        #region Ctor
        public HienTrangController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IHienTrangModelFactory itemModelFactory,
            IHienTrangService itemService,
            IDongBoFactory dongBoFactory,
            IDonViService donViService,
            ILoaiDonViService loaiDonViService
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
            this._donViService = donViService;
            this._loaiDonViService = loaiDonViService;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHienTrang))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new HienTrangSearchModel();
            var model = _itemModelFactory.PrepareHienTrangSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(HienTrangSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHienTrang))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareHienTrangListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHienTrang))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareHienTrangModel(new HienTrangModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(HienTrangModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHienTrang))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                if (model.SelectedLoaiDonViIds != null && model.SelectedLoaiDonViIds.Count > 0)
                    model.LOAI_DON_VI_AP_DUNG = String.Join(",", model.SelectedLoaiDonViIds);
                var item = model.ToEntity<HienTrang>();
                _itemService.InsertHienTrang(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<HienTrangModel>(), "HienTrang");
                model.ID = item.ID;
                _dongBoFactory.DongBoDanhMuc<HienTrangModel>(new List<HienTrangModel>() { model });
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareHienTrangModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHienTrang))
                return AccessDeniedView();

            var item = _itemService.GetHienTrangById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareHienTrangModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(HienTrangModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHienTrang))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetHienTrangById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                if (model.SelectedLoaiDonViIds != null && model.SelectedLoaiDonViIds.Count > 0)
                    model.LOAI_DON_VI_AP_DUNG = String.Join(",", model.SelectedLoaiDonViIds);
                _itemModelFactory.PrepareHienTrang(model, item);
                _itemService.UpdateHienTrang(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<HienTrangModel>(), "HienTrang");
                _dongBoFactory.DongBoDanhMuc<HienTrangModel>(new List<HienTrangModel>() { model });
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareHienTrangModel(model, item, true);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMHienTrang))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetHienTrangById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                HienTrangModel model = item.ToModel<HienTrangModel>();
                _itemService.DeleteHienTrang(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<HienTrangModel>(), "HienTrang");
                //đồng bộ dữ liệu qua kho
                _dongBoFactory.XoaDanhMuc<HienTrangModel>(model);
                //activity log  
                //SuccessNotification("Xoá dữ liệu thành công");
                //return RedirectToAction("List");
                UpdateSessionSearchModel<HienTrangSearchModel>(true);
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = item.ID });
            }
        }

        /// <summary>
        /// Check hiện trạng có được hiển thị không theo đơn vị 
        /// </summary>
        /// <returns></returns>
        /// HungNT 
        // Do ban đầu người làm đã tách listHienTrang ra riêng từng loại tài sản model nên giờ làm tiếp phải tách ra
        // Ae làm sau đừng chửi mình chứ mình cũng muốn gộp vào lắm :D
        public virtual IActionResult CheckHienThiHienTrang(decimal HienTrangId = 0, decimal donViId = 0, bool isCapNhatTuCanhBao = false)
        {
          
            if (!isCapNhatTuCanhBao)
            {
                var donViCheck = _workContext.CurrentDonVi.ID;
                if (donViId > 0)
                {
                    donViCheck = donViId;
                }
                var isDisable = !( _itemService.CheckHienTrangTheoLoaiDonVi(donViCheck, HienTrangId));

                return JsonSuccessMessage(objdata: isDisable);
            }
            // cập nhật từ cảnh báo thì mở hết cho sửa luôn, bắt validate 
            return JsonSuccessMessage(objdata: false);
        }
        #endregion
    }
}

