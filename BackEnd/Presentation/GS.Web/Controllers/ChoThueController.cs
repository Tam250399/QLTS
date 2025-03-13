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
using GS.Web.Factories.DanhMuc;
using System.Linq;
using GS.Web.Framework.Extensions;
using GS.Web.Factories.HeThong;

namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class ChoThueController : BaseWorksController
	{    
        #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly IChoThueModelFactory _itemModelFactory;
            private readonly IChoThueService _itemService;
            private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
            private readonly INhapXuatCongCuService _nhapXuatCongCuService;
            private readonly IXuatNhapService _xuatNhapService;
            private readonly ICauHinhModelFactory _cauHinhModelFactory;
        #endregion

        #region Ctor
        public ChoThueController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IChoThueModelFactory itemModelFactory,
            IChoThueService itemService,
            IDonViBoPhanModelFactory donViBoPhanModelFactory,
            INhapXuatCongCuService nhapXuatCongCuService,
            IXuatNhapService xuatNhapService,
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
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
            this._nhapXuatCongCuService = nhapXuatCongCuService;
            this._xuatNhapService = xuatNhapService;
            this._cauHinhModelFactory = cauHinhModelFactory;
        }
        #endregion

        #region Methods

        public virtual IActionResult List(bool IsLoadSession = false)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLChoThueCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new ChoThueSearchModel ();
            var model = _itemModelFactory.PrepareChoThueSearchModel(searchmodel);
            var _searchModel = HttpContext.Session.GetObject<ChoThueSearchModel>(enumTENCAUHINH.KeySearch+typeof(ChoThueSearchModel).Name );
            if (_searchModel != null && IsLoadSession)
            {
                _cauHinhModelFactory.PrePareModelBySession(model, _searchModel);
                UpdateSessionSearchModel<ChoThueSearchModel>(false);
            }
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(ChoThueSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLChoThueCongCuDungCu))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareChoThueListModel(searchModel);
            HttpContext.Session.SetObject(enumTENCAUHINH.KeySearch+searchModel.GetType().Name, searchModel);
            return Json(model);
        }

        public virtual IActionResult Create(Decimal MapId)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLChoThueCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            //var map = _nhapXuatCongCuService.GetNhapXuatCongCuById(MapId);           
            var model = _itemModelFactory.PrepareChoThueModel(new ChoThueModel(), MapId);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(ChoThueModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLChoThueCongCuDungCu))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<ChoThue>();
                var mapping = _nhapXuatCongCuService.GetNhapXuatCongCuById(model.MapId);
                item.CONG_CU_ID = mapping.CONG_CU_ID;
                item.NHAP_XUAT_ID = mapping.NHAP_XUAT_ID;
                _itemService.InsertChoThue(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<ChoThueModel>(), "ChoThue");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareChoThueModel(model, model.MapId);
            return View(model);
        } 
        
        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLChoThueCongCuDungCu))
                return AccessDeniedView();
                
            var item = _itemService.GetChoThueById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var  model = _itemModelFactory.PrepareChoThueModel(item.ToModel<ChoThueModel>(),0);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(ChoThueModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLChoThueCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetChoThueById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareChoThue(model,item);
                _itemService.UpdateChoThue(item); 
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<ChoThueModel>(), "ChoThue");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List",new { IsLoadSession=true});
            }
            //prepare model;
            model = _itemModelFactory.PrepareChoThueModel(model, model.MapId);
            return View(model);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLChoThueCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetChoThueById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteChoThue(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<ChoThueModel>(), "ChoThue");
                ////xóa nhập xuất công cụ
                //var itemNXCCs = _nhapXuatCongCuService.GetNhapXuatCongCuByNhapXuatId(item.NHAP_XUAT_ID).ToList();
                //if (itemNXCCs.Count()>0)
                //{
                //    foreach (var itemNXCC in itemNXCCs)
                //    {
                //        _nhapXuatCongCuService.DeleteNhapXuatCongCu(itemNXCC);
                //        _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", itemNXCC.ToModel<NhapXuatCongCuModel>(), "NhapXuatCongCu");
                //    }
                //    // xóa xuất nhập
                //    var itemXN = _xuatNhapService.GetXuatNhapById(item.NHAP_XUAT_ID);
                //    {
                //        if (itemXN != null)
                //        {
                //            _xuatNhapService.DeleteXuatNhap(itemXN);
                //            _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", itemXN.ToModel<XuatNhapModel>(), "XuatNhap");
                //        }
                //    }
                //}
                
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
        [HttpPost]
        public virtual IActionResult DeleteForList(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLChoThueCongCuDungCu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetChoThueById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteChoThue(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<ChoThueModel>(), "ChoThue");
                return JsonSuccessMessage("Xoá dữ liệu thành công");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return JsonErrorMessage("Xoá dữ liệu không thành công"); ;
            }
        }
        public virtual IActionResult ChonTaiSan()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLChoThueCongCuDungCu))
                return AccessDeniedView();

            var model = new ChoThueSearchModel();
            model.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID, isAddFirst: true).ToList();

            return View(model);
        }
        public virtual IActionResult _ChonTaiSan()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLChoThueCongCuDungCu))
                return AccessDeniedView();

            var model = new ChoThueSearchModel();
            model.DDLDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: _workContext.CurrentDonVi.ID,isAddFirst:true).ToList();

            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult _ChonTaiSan(ChoThueSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLChoThueCongCuDungCu))
                return AccessDeniedView();

            var model = _itemModelFactory.PrepareChoThueModelForChonCongCu(searchModel);

            return Json(model);
        }
#endregion
    }
}

