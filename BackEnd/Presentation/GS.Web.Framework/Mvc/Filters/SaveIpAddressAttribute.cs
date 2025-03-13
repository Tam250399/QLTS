using GS.Core;
using GS.Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;


namespace GS.Web.Framework.Mvc.Filters
{
    /// <summary>
    /// Represents filter attribute that saves last IP address of customer
    /// </summary>
    public class SaveIpAddressAttribute : TypeFilterAttribute
    {
        #region Ctor

        /// <summary>
        /// Create instance of the filter attribute
        /// </summary>
        public SaveIpAddressAttribute() : base(typeof(SaveIpAddressFilter))
        {
        }

        #endregion

        #region Nested filter

        /// <summary>
        /// Represents a filter that saves last IP address of customer
        /// </summary>
        private class SaveIpAddressFilter : IActionFilter
        {
            #region Fields

            private readonly IWebHelper _webHelper;
            private readonly IWorkContext _workContext;

            #endregion

            #region Ctor

            public SaveIpAddressFilter(
                IWebHelper webHelper,
                IWorkContext workContext
                )
            {
                this._webHelper = webHelper;
                this._workContext = workContext;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Called before the action executes, after model binding is complete
            /// </summary>
            /// <param name="context">A context for action filters</param>
            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (context == null)
                    throw new ArgumentNullException(nameof(context));

                if (context.HttpContext.Request == null)
                    return;

                //only in GET requests
                if (!context.HttpContext.Request.Method.Equals(WebRequestMethods.Http.Get, StringComparison.InvariantCultureIgnoreCase))
                    return;

                if (!DataSettingsManager.DatabaseIsInstalled)
                    return;



                //get current IP address
                var currentIpAddress = _webHelper.GetCurrentIpAddress();
                if (string.IsNullOrEmpty(currentIpAddress))
                    return;

                //update customer's IP address
                if (_workContext.CurrentCustomer != null && !currentIpAddress.Equals(_workContext.CurrentCustomer.IP_TRUY_CAP, StringComparison.InvariantCultureIgnoreCase))
                {
                    _workContext.CurrentCustomer.IP_TRUY_CAP = currentIpAddress;
                    //_customerService.UpdateCustomer(_workContext.CurrentCustomer);
                }
            }

            /// <summary>
            /// Called after the action executes, before the action result
            /// </summary>
            /// <param name="context">A context for action filters</param>
            public void OnActionExecuted(ActionExecutedContext context)
            {
                //do nothing
            }

            #endregion
        }

        #endregion
    }
}