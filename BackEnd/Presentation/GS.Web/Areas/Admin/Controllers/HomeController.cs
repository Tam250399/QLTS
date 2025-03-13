using GS.Core.Domain.Common;
using GS.Services.HeThong;

namespace GS.Web.Areas.Admin.Controllers
{
    public partial class HomeController : BaseAdminController
    {
        #region Fields

        private readonly AdminAreaSettings _adminAreaSettings;
        private readonly INhanHienThiService _NhanHienThiService;
        private readonly ICauHinhService _settingService;

        #endregion

        #region Ctor

        public HomeController(AdminAreaSettings adminAreaSettings,
            INhanHienThiService NhanHienThiService,
            ICauHinhService settingService)
        {
            this._adminAreaSettings = adminAreaSettings;
            this._NhanHienThiService = NhanHienThiService;
            this._settingService = settingService;
        }

        #endregion

        //#region Methods
        //public virtual IActionResult Index()
        //{
        //    //display a warning to a store owner if there are some error
        //    if (_permissionService.Authorize(StandardPermissionProvider.ManageMaintenance))
        //    {
        //        var warnings = _commonModelFactory.PrepareSystemWarningModels();
        //        if (warnings.Any(warning => warning.Level == SystemWarningLevel.Fail ||
        //                                    warning.Level == SystemWarningLevel.CopyrightRemovalKey ||
        //                                    warning.Level == SystemWarningLevel.Warning))
        //            WarningNotification(__nhanHienThiService.GetGiaTri("Admin.System.Warnings.Errors"), false);
        //    }

        //    //prepare model
        //    var model = _homeModelFactory.PrepareDashboardModel(new DashboardModel());

        //    return Redirect("/Admin/Log/List");
        //}

        //[HttpPost]
        //public virtual IActionResult GSCommerceNewsHideAdv()
        //{
        //    _adminAreaSettings.HideAdvertisementsOnAdminArea = !_adminAreaSettings.HideAdvertisementsOnAdminArea;
        //    _settingService.SaveSetting(_adminAreaSettings);

        //    return Content("Setting changed");
        //}

        //#endregion
    }
}