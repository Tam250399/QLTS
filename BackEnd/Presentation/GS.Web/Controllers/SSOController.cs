using GS.Core;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.Security;
using GS.Services.Logging;
using GS.Services.Authentication;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Framework.Extensions;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security.Captcha;
using GS.Web.Models.Common;
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using GS.Services.BaoCaos;
using GS.Core.Domain.BaoCaos;
using GS.Services.Security;
using GS.Services.BienDongs;
using GS.Web.Reports.TS_BCCT;
using System.Linq;
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;

namespace GS.Web.Controllers
{
    public class SSOController : BasePublicController
    {
        #region fields
        private readonly CaptchaSettings _captchaSettings;
        private readonly CauHinhNguoiDung _nguoiDungCauHinh;
        private readonly CauHinhChung _cauHinhChung;
        private readonly XacThucChungThucSoCauHinh _cauHinhXacThuc;

        private readonly INhanHienThiService _NhanHienThiService;
        private readonly IWebHelper _webHelper;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICauHinhService _cauHinhService;
        private readonly IDonViService _DonViService;
        private readonly IWorkContext _workContext;
        private readonly IHoatDongService _hoatdongService;
        private readonly ILogger _logger;
        private readonly IQuyenService _quyenService;
        private readonly IBienDongService _bienDongService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region ctor
        public SSOController(
            XacThucChungThucSoCauHinh cauHinhXacThuc,
            CauHinhChung cauHinhChung,
            CaptchaSettings captchaSettings,
             CauHinhNguoiDung customerSettings,
             INhanHienThiService NhanHienThiService,
             IWebHelper webHelper,
            INguoiDungService nguoiDungService,
            IAuthenticationService authenticationService,
            ICauHinhService cauHinhService,
            IDonViService DonViService,
            IHoatDongService hoatDongService,
            IWorkContext workContext,
            ILogger logger,
            IBienDongService bienDongService,
            IQuyenService quyenService,
            IHttpContextAccessor httpContextAccessor


            )
        {
            this._cauHinhXacThuc = cauHinhXacThuc;
            this._cauHinhChung = cauHinhChung;
            this._authenticationService = authenticationService;
            this._webHelper = webHelper;
            this._captchaSettings = captchaSettings;
            this._nguoiDungCauHinh = customerSettings;
            this._NhanHienThiService = NhanHienThiService;
            this._nguoiDungService = nguoiDungService;
            this._cauHinhService = cauHinhService;
            this._DonViService = DonViService;
            this._hoatdongService = hoatDongService;
            this._workContext = workContext;
            this._logger = logger;
            this._quyenService = quyenService;
            this._bienDongService = bienDongService;
            this._httpContextAccessor = httpContextAccessor;
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }
        [PublicAntiForgery]
        public virtual IActionResult Index(LoginModel model, string ReturnUrl, bool captchaValid)
        {
            //validate CAPTCHA
            if (_captchaSettings.Enabled && _captchaSettings.ShowOnLoginPage && !captchaValid)
            {
                ModelState.AddModelError("", _captchaSettings.GetWrongCaptchaMessage(_NhanHienThiService));
            }

            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(model.Username))
                {
                    model.Username = model.Username.Trim();
                    model.Username = model.Username.ToLower();
                }
                var loginResult = _nguoiDungService.ValidateNguoiDung(model.Username, model.Password);
                switch (loginResult)
                {
                    case NguoiDungKetQuaDangNhap.Successful:
                        {
                            var customer = _nguoiDungService.GetNguoiDungByUsername(model.Username);
                            //var toaanID = _nguoiDungToaAnService.GetByNguoiDungId(customer.ID).FirstOrDefault().DON_VI_ID;
                            //var toaan = _toaAnService.GetToaAnById(toaanID);
                            var DonVi = _DonViService.GetProfileUser(guidNguoiDung: customer.GUID);

                            //sign in new customer
                            _authenticationService.DangNhap(customer, model.RememberMe);
                            HttpContext.Session.SetObject("CurrentCustomer", customer.ToModelCache<NguoiDungCache>());
                            var donViKey = "CurrentDonVi" + _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
                            HttpContext.Session.SetObject(donViKey, DonVi);
                            ////
                            //CookieOptions option = new CookieOptions();
                            //option.Expires = DateTime.Now.AddMinutes(120);
                            ////Add cookie customer
                            //Response.Cookies.Append("CurrentCustomer", customer.ToModelCache<NguoiDungCache>().toStringJson(), option);
                            ////
                            ////Add cookie CurrentDonVi
                            //Response.Cookies.Append(donViKey, customer.ToModelCache<NguoiDungCache>().toStringJson(), option);
                            //
                            //HttpContext.Session.SetObject("CurrentToaAn", toaan);

                            //raise event       
                            //_eventPublisher.Publish(new CustomerLoggedinEvent(customer));

                            //Clean Password firt activity log
                            model.Password = String.Empty;
                            //activity log
                            _hoatdongService.InsertHoatDong(enumHoatDong.DangNhap, "Đăng nhập ", model, "Home");

                            //_nhanHienThiService.GetGiaTri("ActivityLog.PublicStore.Login"), customer);
                            //tao notification
                            //var notifyItem = Notification.CreateSimple(customer.Id
                            //    , _nhanHienThiService.GetGiaTri("Account.Login.Notification.Subject")
                            //    , string.Format(_nhanHienThiService.GetGiaTri("Account.Login.Notification.Body"), DateTime.Now.toDateVNString(true), _webHelper.GetCurrentIpAddress())
                            //    );
                            //_notificationService.InsertNotification(notifyItem);

                            if (string.IsNullOrEmpty(ReturnUrl) || !Url.IsLocalUrl(ReturnUrl))
                                return Redirect("AppWork/Index");

                            return Redirect(ReturnUrl);
                        }
                    case NguoiDungKetQuaDangNhap.CustomerNotExist:
                        ModelState.AddModelError("", "Tài khoản không tồn tại");
                        model.messageResult = "Tài khoản không tồn tại";
                        break;
                    case NguoiDungKetQuaDangNhap.LockedOut:
                        ModelState.AddModelError("", "Tài khoản bị khóa");
                        model.messageResult = "Tài khoản bị khóa";
                        break;
                    case NguoiDungKetQuaDangNhap.WrongPassword:
                    default:
                        ModelState.AddModelError("", "Mật khẩu sai");
                        model.messageResult = "Mật khẩu sai";
                        break;
                }
            }
            return View(model);
        }
    }
}