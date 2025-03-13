using GS.Core;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Security;
using GS.Services.HeThong;
using GS.Services.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GS.Web.Controllers
{
    public partial class CommonController : BasePublicController
    {
        #region Fields

        private readonly CaptchaSettings _captchaSettings;
        private readonly CauHinhChung _commonSettings;
        private readonly INhanHienThiService _NhanHienThiService;
        private readonly ILogger _logger;
        private readonly IWorkContext _workContext;
        private readonly IWebHelper _webHelper;
        #endregion

        #region Ctor

        public CommonController(CaptchaSettings captchaSettings,
            CauHinhChung commonSettings,
            INhanHienThiService NhanHienThiService,
            ILogger logger,
            IWorkContext workContext,
            IWebHelper webHelper
            )
        {
            this._captchaSettings = captchaSettings;
            this._commonSettings = commonSettings;
            this._NhanHienThiService = NhanHienThiService;
            this._logger = logger;
            this._workContext = workContext;
            this._webHelper = webHelper;
        }

        #endregion

        #region Methods

        //page not found
        public virtual IActionResult PageNotFound()
        {
            if (_commonSettings.Log404Errors)
            {
                var statusCodeReExecuteFeature = HttpContext?.Features?.Get<IStatusCodeReExecuteFeature>();
                //TODO add locale resource
                _logger.Error($"Error 404. The requested page ({statusCodeReExecuteFeature?.OriginalPath}) was not found",
                    customer: _workContext.CurrentCustomer);
            }

            Response.StatusCode = 404;
            Response.ContentType = "text/html";

            return View();
        }



        //helper method to redirect users. Workaround for GenericPathRoute class where we're not allowed to do it
        public virtual IActionResult InternalRedirect(string url, bool permanentRedirect)
        {
            //ensure it's invoked from our GenericPathRoute class
            if (HttpContext.Items["nop.RedirectFromGenericPathRoute"] == null ||
                !Convert.ToBoolean(HttpContext.Items["nop.RedirectFromGenericPathRoute"]))
            {
                url = Url.RouteUrl("HomePage");
                permanentRedirect = false;
            }

            //home page
            if (string.IsNullOrEmpty(url))
            {
                url = Url.RouteUrl("HomePage");
                permanentRedirect = false;
            }

            //prevent open redirection attack
            if (!Url.IsLocalUrl(url))
            {
                url = Url.RouteUrl("HomePage");
                permanentRedirect = false;
            }

            if (permanentRedirect)
                return RedirectPermanent(url);

            return Redirect(url);
        }

        //Restart application domain
        [HttpPost]
        public virtual IActionResult RestartApplication(string returnUrl = "")
        {

            //restart application
            _webHelper.RestartAppDomain();

            //home page
            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index", "Home");

            //prevent open redirection attack
            if (!Url.IsLocalUrl(returnUrl))
                return RedirectToAction("Index", "Home");

            return Redirect(returnUrl);
        }

        #endregion
    }
}