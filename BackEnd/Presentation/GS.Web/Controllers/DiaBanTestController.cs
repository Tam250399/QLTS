//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/3/2020
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
using System.Linq;
using GS.Services.DanhMuc;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;


namespace GS.Web.Controllers
{
     [HttpsRequirement(SslRequirement.No)]
	public partial class DiaBanTestController : BaseWorksController
	{    
         #region Fields
            private readonly IHoatDongService _hoatdongService;
            private readonly INhanHienThiService _nhanHienThiService; 
            private readonly IQuyenService _quyenService;
            private readonly IWorkContext _workContext;
            private readonly CauHinhChung _cauhinhChung;
            private readonly IDiaBanTestModelFactory _itemModelFactory;
            private readonly IDiaBanTestService _itemService;
         #endregion
     
        #region Ctor
        public DiaBanTestController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,            
            IQuyenService quyenService,            
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            IDiaBanTestModelFactory itemModelFactory,
            IDiaBanTestService itemService
            )
        {            
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;            
            this._cauhinhChung = cauhinhChung;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new DiaBanTestSearchModel ();
            var model = _itemModelFactory.PrepareDiaBanTestSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(DiaBanTestSearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareDiaBanTestListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareDiaBanTestModel(new DiaBanTestModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(DiaBanTestModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            Console.WriteLine(model);
            if (ModelState.IsValid)
            {
                Console.WriteLine("Dia ban test cha:"+ model.PARENT_ID == null);
                var item = new DiaBanTest();
                _itemModelFactory.PrepareDiaBanTest(model, item);
                _itemService.InsertDiaBanTest(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<DiaBanTestModel>(), "DiaBanTest");
                SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareDiaBanTestModel(model, null);            
            return View(model);
        } 
        
        public virtual IActionResult Edit(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
                
            var item = _itemService.GetDiaBanTestById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareDiaBanTestModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(DiaBanTestModel model, bool continueEditing)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDiaBanTestById(model.ID);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareDiaBanTest(model,item);
                _itemService.UpdateDiaBanTest(item); 
                _hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<DiaBanTestModel>(), "DiaBanTest");
                SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareDiaBanTestModel(model, item, true);
            return View(model);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDiaBanTestById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteDiaBanTest(item);
                _hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<DiaBanTestModel>(), "DiaBanTest");
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

        public virtual IActionResult SearchDiaBanTestParent(string parentName)
        {
            var items = _itemService.GetDiaBanForInputSearch(name: parentName);
            var results = Json(items.Select(c =>
            {
                var m = c.ToModel<DiaBanTestModel>();
                return m;
            }
            ).ToList());
            return results;
        }
        #endregion       			
	}
}

