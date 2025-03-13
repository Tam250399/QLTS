using GS.Core;
using GS.Core.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace GS.Web.Framework.Globalization
{
    /// <summary>
    /// Represents middleware that set current culture based on request
    /// </summary>
    public class CultureMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="next">Next</param>
        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Set working culture
        /// </summary>
        /// <param name="webHelper">Web helper</param>
        /// <param name="workContext">Work context</param>
        protected void SetWorkingCulture(IWebHelper webHelper)
        {
            if (!DataSettingsManager.DatabaseIsInstalled)
                return;

            if (webHelper.IsStaticResource())
                return;

            var culture = new CultureInfo("vi-VN");
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoke middleware actions
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <param name="webHelper">Web helper</param>
        /// <param name="workContext">Work context</param>
        /// <returns>Task</returns>
        public Task Invoke(Microsoft.AspNetCore.Http.HttpContext context, IWebHelper webHelper)
        {
            //set culture
            SetWorkingCulture(webHelper);

            //call the next middleware in the request pipeline
            return _next(context);
        }

        #endregion
    }
}
