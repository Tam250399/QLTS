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
    public partial class XuLyKetQuaController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly IXuLyKetQuaModelFactory _itemModelFactory;
        private readonly IXuLyKetQuaServices _itemService;
        private readonly ITaiSanTdService _taiSanTdService;
        private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;
        private readonly IXuLyService _xuLyService;
        private readonly IKetQuaTaiSanServices _ketQuaTaiSanServices;
        private readonly IXuLyModelFactory _xuLyModelFactory;
        private readonly IKetQuaTaiSanModelFactory _ketQuaTaiSanModelFactory;
        #endregion

        #region Ctor
        public XuLyKetQuaController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IXuLyKetQuaModelFactory itemModelFactory,
            IXuLyKetQuaServices itemService,
            ITaiSanTdService taiSanTdService,
            ITaiSanTdXuLyService taiSanTdXuLyService,
            IXuLyService xuLyService,
            IKetQuaTaiSanServices ketQuaTaiSanServices,
            IXuLyModelFactory xuLyModelFactory,
            IKetQuaTaiSanModelFactory ketQuaTaiSanModelFactory
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
            this._ketQuaTaiSanModelFactory = ketQuaTaiSanModelFactory;
        }
        #endregion
        #region Methods
        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new XuLyKetQuaSearchModel();
            var model = _itemModelFactory.PrepareXuLyKetQuaSearchModel(searchmodel);
            return View(model);
        }
        [HttpPost]
        public virtual IActionResult List(XuLyKetQuaSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareXuLyKetQuaListModel(searchModel);
            return Json(model);
        }
        public virtual IActionResult _ListKetQuaTaiSan(int XuLyKetQuaId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new KetQuaTaiSanSearchModel() {XuLyKetQuaId = XuLyKetQuaId } ;
            var model = _ketQuaTaiSanModelFactory.PrepareKetQuaTaiSanSearchModel(searchmodel);
            return PartialView(model);
        }
        public virtual IActionResult _ListKetQuaXuLy()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new XuLySearchModel() { LOAI_XU_LY_ID = (int)enumLoaiXuLy.KetQua };
            var model = _xuLyModelFactory.PrepareXuLySearchModel(searchmodel);
            return PartialView(model);
        }
        public virtual IActionResult ListKetQuaXuLy()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new XuLySearchModel() { LOAI_XU_LY_ID = (int)enumLoaiXuLy.KetQua };
            var model = _xuLyModelFactory.PrepareXuLySearchModel(searchmodel);
            return View(model);
        }
        public virtual IActionResult Create(int XuLyId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //prepare model
            var item = new XuLyKetQua
            {               
                SO_TIEN = 0,
                NGUOI_TAO_ID = _workContext.CurrentCustomer.ID,
                NGAY_TAO = DateTime.Now,
                TRANG_THAI_ID = (int)enumTrangThaiXuLyKetQua.NHAP,
                XU_LY_ID = XuLyId
            };
            _itemService.InsertXuLyKetQua(item);
            _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<XuLyKetQuaModel>(), "XuLyKetQua");
            var model = _itemModelFactory.PrepareXuLyKetQuaModel(new XuLyKetQuaModel(), item);
            model.Is_Create = true;
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Create(XuLyKetQuaModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();

            var item = _itemService.GetXuLyKetQuaById(model.ID);
            if (item == null)
                return JsonErrorMessage("Tạo mới dữ liệu không thành công !", model.ID);
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareXuLyKetQua(model, item);
                item.TRANG_THAI_ID = (int)enumTrangThaiXuLyKetQua.TONTAI;
                _itemService.UpdateXuLyKetQua(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<XuLyKetQuaModel>(), "XuLyKetQua");
                return JsonSuccessMessage("Tạo mới dữ liệu thành công !");
            }
            //prepare model
            var listError = ModelState.Values.Where(c => c.Errors.Count() > 0).ToList();
            return JsonErrorMessage("Tạo mới dữ liệu không thành công !", listError);
        }
       
        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();

            var item = _itemService.GetXuLyKetQuaById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareXuLyKetQuaModel(null, item);
            return View(model);
        }       
        [HttpPost]
        public virtual IActionResult Edit(XuLyKetQuaModel model)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLTSTDXuLy))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetXuLyKetQuaById(model.ID);
            if (item == null)
                return JsonErrorMessage("Cập nhật dữ liệu không thành công !", model.ID);
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareXuLyKetQua(model, item);
                _itemService.UpdateXuLyKetQua(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<XuLyKetQuaModel>(), "XuLyKetQua");
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
            var item = _itemService.GetXuLyKetQuaById(ID);
            if (item == null)
                return JsonErrorMessage("Xoá dữ liệu không thành công", ID);
            try
            {
                var listkqts = _ketQuaTaiSanServices.GetKetQuaTaiSans(XuLyKetQuaId:item.ID);
                if (listkqts != null && listkqts.Count() > 0)
                {
                    //kq tài sản td
                    foreach (var kqts in listkqts)
                    {

                        _ketQuaTaiSanServices.DeleteKetQuaTaiSan(kqts);
                        _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", kqts.ToModel<KetQuaTaiSanModel>(), "KetQuaTaiSan");
                    }

                }
                _itemService.DeleteXuLyKetQua(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<XuLyKetQuaModel>(), "XuLyKetQua");
                //activity log  
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                return JsonErrorMessage("Xoá dữ liệu không thành công", exc);
            }
        }
        #endregion
    }
}

