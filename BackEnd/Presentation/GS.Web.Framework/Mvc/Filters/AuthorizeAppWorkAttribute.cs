﻿using GS.Core.Data;
using GS.Core.Domain.HeThong;
using GS.Web.Framework.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace GS.Web.Framework.Mvc.Filters
{
    public class AuthorizeAppWorkAttribute : TypeFilterAttribute
    {
        #region Fields

        private readonly bool _ignoreFilter;

        #endregion

        #region Ctor

        /// <summary>
        /// Create instance of the filter attribute
        /// </summary>
        /// <param name="ignore">Whether to ignore the execution of filter actions</param>
        public AuthorizeAppWorkAttribute(bool ignore = false) : base(typeof(AuthorizeAppWorkFilter))
        {
            this._ignoreFilter = ignore;
            this.Arguments = new object[] { ignore };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether to ignore the execution of filter actions
        /// </summary>
        public bool IgnoreFilter => _ignoreFilter;

        #endregion


        #region Nested filter

        /// <summary>
        /// Represents a filter that confirms access to the admin panel
        /// </summary>
        private class AuthorizeAppWorkFilter : IAuthorizationFilter
        {
            #region Fields

            private readonly bool _ignoreFilter;
            //private readonly IPermissionService _permissionService;
            private readonly IHttpContextAccessor _httpContextAccessor;
            #endregion

            #region Ctor

            public AuthorizeAppWorkFilter(bool ignoreFilter
                , IHttpContextAccessor httpContextAccessor
                )
            {
                this._ignoreFilter = ignoreFilter;
                this._httpContextAccessor = httpContextAccessor;
                //this._permissionService = permissionService;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Called early in the filter pipeline to confirm request is authorized
            /// </summary>
            /// <param name="filterContext">Authorization filter context</param>
            public void OnAuthorization(AuthorizationFilterContext filterContext)
            {
                if (filterContext == null)
                    throw new ArgumentNullException(nameof(filterContext));

                //check whether this filter has been overridden for the action
                var actionFilter = filterContext.ActionDescriptor.FilterDescriptors
                    .Where(filterDescriptor => filterDescriptor.Scope == FilterScope.Action)
                    .Select(filterDescriptor => filterDescriptor.Filter).OfType<AuthorizeAdminAttribute>().FirstOrDefault();

                //ignore filter (the action is available even if a customer hasn't access to the admin area)
                if (actionFilter?.IgnoreFilter ?? _ignoreFilter)
                    return;

                if (!DataSettingsManager.DatabaseIsInstalled)
                    return;
                //mat session
                if (_httpContextAccessor.HttpContext.Session.GetObject<NguoiDung>("CurrentCustomer") == null)
                {
                    filterContext.Result = new ChallengeResult();
                }
                //there is AdminAuthorizeFilter, so check access
                //if (filterContext.Filters.Any(filter => filter is AuthorizeAppWorkFilter))
                //{
                //    //authorize permission of access to the admin area
                //    if (!_permissionService.Authorize(StandardPermissionProvider.AccessAppWorkPanel))
                //        filterContext.Result = new ChallengeResult();
                //}
            }

            #endregion
        }

        #endregion
    }
}
