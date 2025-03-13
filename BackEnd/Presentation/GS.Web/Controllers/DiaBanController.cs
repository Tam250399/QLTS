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
    public partial class DiaBanController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IDiaBanModelFactory _itemModelFactory;
        private readonly IDiaBanService _itemService;
        private readonly IDongBoFactory _dongBoFactory ;
        #endregion

        #region Ctor
        public DiaBanController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IDiaBanModelFactory itemModelFactory,
            IDiaBanService itemService,
            IDongBoFactory dongBoFactory
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
        }
        #endregion
        #region Methods

        public virtual IActionResult List(int? pageIndex = 0)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDiaBan))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new DiaBanSearchModel();
            var model = _itemModelFactory.PrepareDiaBanSearchModel(searchmodel);
            if (pageIndex > 0)
            {
                searchmodel.Page = (int)pageIndex;
            }
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(DiaBanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDiaBan))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareDiaBanListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDiaBan))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareDiaBanModel(new DiaBanModel(), null);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Create(DiaBanModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDiaBan))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = new DiaBan();
                _itemModelFactory.PrepareDiaBan(model, item);
                _itemService.InsertDiaBan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<DiaBanModel>(), "DiaBan");
                model.ID = item.ID;
                  _dongBoFactory.DongBoDanhMuc<DiaBanModel>(new List<DiaBanModel>() { model });
                return JsonSuccessMessage("Tạo mới dữ liệu thành công !", item.ToModel<DiaBanModel>());
            }
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return JsonErrorMessage("Error", list);
        }

        public virtual IActionResult Edit(int id, int pageIndex)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDiaBan))
                return AccessDeniedView();

            var item = _itemService.GetDiaBanById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareDiaBanModel(null, item);
            model.pageIndex = pageIndex;
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Edit(DiaBanModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDiaBan))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDiaBanById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareDiaBan(model, item);
                _itemService.UpdateDiaBan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<DiaBanModel>(), "DiaBan");
                _dongBoFactory.DongBoDanhMuc<DiaBanModel>(new List<DiaBanModel>() { model });
                return JsonSuccessMessage("Cập nhật dữ liệu thành công !", item.ToModel<DiaBanModel>());
            }
            //prepare model
            var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
            return Json( list);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLDiaBan))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDiaBanById(id);
            if (item == null)
                return RedirectToAction("List");
            var diabancon = _itemService.GetCountDiaBanSub(item.ID);
            if(diabancon == 0) 
            { 
                try
                {
                    DiaBanModel model = item.ToModel<DiaBanModel>();
                    _itemService.DeleteDiaBan(item);
                    _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<DiaBanModel>(), "DiaBan");
                    //đồng bộ dữ liệu qua kho
                    _dongBoFactory.XoaDanhMuc<DiaBanModel>(model);
                    //SuccessNotification("Xoá dữ liệu thành công");
                    //activity log  
                    //SuccessNotification("Xoá dữ liệu thành công");
                    //return RedirectToAction("List");
                    UpdateSessionSearchModel<DiaBanSearchModel>(true);
                    return JsonSuccessMessage("Xoá dữ liệu thành công");
                }
                catch (Exception exc)
                {
                    ErrorNotification(exc);
                    return RedirectToAction("Edit", new { id = item.ID });
                }
            }
            ErrorNotification("Bạn phải xoá địa bàn con");
            return RedirectToAction("Edit", new { id = item.ID });
        }
        public virtual IActionResult SearchDiaBan(string TenDiaBan = null)

        {
            var items = _itemService.GetDiaBansForInputSearch(tenDiaBan: TenDiaBan);
            var md = Json(items.Select(c =>
            {
                var m = c.ToModel<DiaBanModel>();
                return m;
            }
            ).ToList());
            return md;
        }
        #endregion
    }
}

