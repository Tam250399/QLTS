using GS.Core;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using GS.Core.Http;
using GS.Services.Authentication;
using GS.Services.DanhMuc;
using GS.Services.Helpers;
using GS.Services.HeThong;
using GS.Web.Framework.Extensions;
using Microsoft.AspNetCore.Http;
using System;

namespace GS.Web.Framework
{
    /// <summary>
    /// Represents work context for web application
    /// </summary>
    public partial class WebWorkContext : IWorkContext
    {
        #region Fields
        private readonly IAuthenticationService _authenticationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserAgentHelper _userAgentHelper;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IDonViService _DonViService;
        private NguoiDung _cachedCustomer;
        private InfoCacheDonvi _cachedDonVi;
        #endregion

        #region Ctor

        public WebWorkContext(
            IHttpContextAccessor httpContextAccessor,
            IUserAgentHelper userAgentHelper,
            INguoiDungService nguoiDungService,
            IAuthenticationService authenticationService,
            IDonViService DonViService
            )
        {
            this._authenticationService = authenticationService;
            this._httpContextAccessor = httpContextAccessor;
            this._nguoiDungService = nguoiDungService;
            this._userAgentHelper = userAgentHelper;
            this._DonViService = DonViService;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Get nop customer cookie
        /// </summary>
        /// <returns>String value of cookie</returns>
        protected virtual string GetCustomerCookie()
        {
            var cookieName = $"{GSCookieDefaults.Prefix}{GSCookieDefaults.CustomerCookie}";
            return _httpContextAccessor.HttpContext?.Request?.Cookies[cookieName];
        }

        /// <summary>
        /// Set nop customer cookie
        /// </summary>
        /// <param name="customerGuid">Guid of the customer</param>
        protected virtual void SetCustomerCookie(Guid customerGuid)
        {
            if (_httpContextAccessor.HttpContext?.Response == null)
                return;

            //delete current cookie value
            var cookieName = $"{GSCookieDefaults.Prefix}{GSCookieDefaults.CustomerCookie}";
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookieName);

            //get date of cookie expiration
            var cookieExpires = 24 * 365; //TODO make configurable
            var cookieExpiresDate = DateTime.Now.AddHours(cookieExpires);

            //if passed guid is empty set cookie as expired
            if (customerGuid == Guid.Empty)
                cookieExpiresDate = DateTime.Now.AddMonths(-1);

            //set new cookie value
            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = cookieExpiresDate
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, customerGuid.ToString(), options);
        }



        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current customer
        /// </summary>
        public virtual NguoiDung CurrentCustomer
        {
            get
            {
                //whether there is a cached value
                if (_cachedCustomer != null)
                    return _cachedCustomer;

                var customer = _httpContextAccessor.HttpContext.Session.GetObject<NguoiDung>("CurrentCustomer");
                //if (customer == null)
                //{
                //    var cookieCustomer = _httpContextAccessor.HttpContext.Request.Cookies["CurrentCustomer"];
                //    customer = cookieCustomer.toEntity<NguoiDung>();
                //}
                _cachedCustomer = customer;

                return _cachedCustomer;
                //return _httpContextAccessor.HttpContext.Session.GetObject<NguoiDung>("CurrentCustomer"); 
            }
            set
            {
                _cachedCustomer = value;
            }
        }
        /// <summary>
        /// Gets or sets the current DonVi
        /// </summary>
        public virtual InfoCacheDonvi CurrentDonVi
        {
            get
            {
                //whether there is a cached value
                if (_cachedDonVi != null)
                    return _cachedDonVi;
                var donViKey = "CurrentDonVi" + _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
                var DonVi = _httpContextAccessor.HttpContext.Session.GetObject<InfoCacheDonvi>(donViKey);
                //if (DonVi == null)
                //{
                //    var cookieDonVi = _httpContextAccessor.HttpContext.Request.Cookies[donViKey];
                //    DonVi = cookieDonVi.toEntity<InfoCacheDonvi>();
                //}
                _cachedDonVi = DonVi;

                return _cachedDonVi;
            }
            set
            {
                _cachedDonVi = value;
            }
        }
        /// <summary>
        /// Gets or sets value indicating whether we're in admin area
        /// </summary>
        public virtual bool IsAdmin { get; set; }

        #endregion
    }
}