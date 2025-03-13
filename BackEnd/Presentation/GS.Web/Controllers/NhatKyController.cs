//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.HeThong;
using GS.Services;
//using GS.Services.Events;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Mvc;

namespace GS.Web.Controllers
{
    [HttpsRequirement(SslRequirement.No)]
    public partial class NhatKyController : BaseWorksController
    {
        #region Fields
        private readonly INhanHienThiService _NhanHienThiService;
        private readonly IWorkContext _workContext;
        private readonly INhatKyModelFactory _itemModelFactory;
        private readonly INhatKyService _itemService;
        private readonly IQuyenService _quyenService;
        #endregion

        #region Ctor
        public NhatKyController(
            INhanHienThiService NhanHienThiService,
            IWorkContext workContext,
            INhatKyModelFactory itemModelFactory,
            INhatKyService itemService,
            IQuyenService quyenService
            )
        {
            this._NhanHienThiService = NhanHienThiService;
            this._workContext = workContext;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._quyenService = quyenService;
        }
        #endregion
        #region Methods

        public virtual IActionResult List()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ADMINQLNhatKy))
            //    return AccessDeniedView();
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhatKy))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new NhatKySearchModel();
            var model = _itemModelFactory.PrepareNhatKySearchModel(searchmodel);
            model.CAPDOlist = ((LogLevel)model.capdoLevel).ToSelectList();

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(NhatKySearchModel searchModel)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhatKy))
                return AccessDeniedKendoGridJson();
            //prepare model
            var model = _itemModelFactory.PrepareNhatKyListModel(searchModel);

            return Json(model);
        }

        public virtual IActionResult Detail(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhatKy))
                return AccessDeniedView();

            var item = _itemService.GetNhatKyById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareNhatKyModel(null, item);
            model.CAPDOlist = ((LogLevel)model.capdoLevel).ToSelectList();
            model.tenCapDo = ((LogLevel)model.CAP_DO);
            return View(model);
        }

        public virtual IActionResult View(int id)
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.ADMINQLNhatKy))
                return AccessDeniedView();

            var item = _itemService.GetNhatKyById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareNhatKyModel(null, item);
            return View(model);
        }

        #endregion
    }
}

