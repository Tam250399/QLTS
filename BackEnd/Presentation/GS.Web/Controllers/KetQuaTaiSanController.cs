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
using GS.Core.Domain.SHTD;
using GS.Services.Logging;
using GS.Services.Security;
using GS.Services.HeThong;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.SHTD;
using GS.Web.Controllers;
using GS.Web.Factories.SHTD;
using GS.Services.SHTD;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using System.Linq;
using GS.Web.Models.ThuocTinh;
using GS.Services.ThuocTinhs;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class KetQuaTaiSanController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IKetQuaTaiSanModelFactory _itemModelFactory;
        private readonly IKetQuaTaiSanServices _itemService;
        private readonly ITaiSanTdService _taiSanTdService;
        private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;
        private readonly IXuLyService _xuLyService;
        private readonly IKetQuaTaiSanServices _ketQuaTaiSanServices;
        private readonly IXuLyModelFactory _xuLyModelFactory;
        #endregion

        #region Ctor
        public KetQuaTaiSanController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IKetQuaTaiSanModelFactory itemModelFactory,
            IKetQuaTaiSanServices itemService,
            ITaiSanTdService taiSanTdService,
            ITaiSanTdXuLyService taiSanTdXuLyService,
            IXuLyService xuLyService,
            IKetQuaTaiSanServices ketQuaTaiSanServices,
            IXuLyModelFactory xuLyModelFactory
            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._taiSanTdService = taiSanTdService;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
            this._xuLyService = xuLyService;
            this._ketQuaTaiSanServices = ketQuaTaiSanServices;
            this._xuLyModelFactory = xuLyModelFactory;
        }
        #endregion
        #region Methods
        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new KetQuaTaiSanSearchModel();
            var model = _itemModelFactory.PrepareKetQuaTaiSanSearchModel(searchmodel);
            return View(model);
        }
        [HttpPost]
        public virtual IActionResult List(KetQuaTaiSanSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareKetQuaTaiSanListModel(searchModel);
            return Json(model);
        }
        public virtual IActionResult _CreateKetQuaTaiSan(int XuLyKetQuaId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model            
            var model = _itemModelFactory.PrepareKetQuaTaiSanModel(new KetQuaTaiSanModel() {XU_LY_KET_QUA_ID = XuLyKetQuaId}, null,true);           
            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult _CreateKetQuaTaiSan(KetQuaTaiSanModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<KetQuaTaiSan>();
                _itemService.InsertKetQuaTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<KetQuaTaiSanModel>(), "KetQuaTaiSan");
                return JsonSuccessMessage("Tạo mới dữ liệu thành công !");
            }
            //prepare model
            var listError = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Tạo mới dữ liệu không thành công !", listError);
        }

        public virtual IActionResult _EditKetQuaTaiSan(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();

            var item = _itemService.GetKetQuaTaiSanById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareKetQuaTaiSanModel(null, item,true);
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult _EditKetQuaTaiSan(KetQuaTaiSanModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKetQuaTaiSanById(model.ID);
            if (item == null)
                return JsonErrorMessage("Cập nhật dữ liệu không thành công !", model.ID);
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareKetQuaTaiSan(model, item);
                _itemService.UpdateKetQuaTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<KetQuaTaiSanModel>(), "KetQuaTaiSan");
                return JsonSuccessMessage("Cập nhật dữ liệu thành công !");
            }
            //prepare model
            var listError = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Cập nhật dữ liệu không thành công !", listError);
        }
        [HttpPost]
        public virtual IActionResult Delete(int ID)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKetQuaTaiSanById(ID);
            if (item == null)
                return JsonErrorMessage("Xoá dữ liệu không thành công", ID);
            try
            {

                _ketQuaTaiSanServices.DeleteKetQuaTaiSan(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<KetQuaTaiSanModel>(), "KetQuaTaiSan");


                //activity log  
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                return JsonErrorMessage("Xoá dữ liệu không thành công", exc);
            }
        }
        public virtual IActionResult GetSoLuongCon(int TaiSanXuLyId,int XuLyKetQuaId,int id)
        {
            var soluongcon = _itemModelFactory.PrePareSoLuongConLai(TaiSanXuLyId, XuLyKetQuaId, id);
            return Json(soluongcon);
        }
        #endregion
    }
}

