using GS.Core;
using GS.Core.Caching;
using GS.Core.Infrastructure;
using GS.Services.DanhMuc;
using GS.Services.Helpers;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Models.Common;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace GS.Web.Controllers
{
    public class AppWorkController : BaseWorksController
    {
        #region Fields
        private readonly INhanHienThiService _NhanHienThiService;
        private readonly IWorkContext _workContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IGSFileProvider _fileProvider;
        private readonly IStaticCacheManager _cacheManager;
        private readonly IDonViService _donViService;
        private readonly IQuyenService _quyenService;
        #endregion

        #region Ctor

        public AppWorkController(
            INhanHienThiService NhanHienThiService,
            IWorkContext workContext,
            IHttpContextAccessor httpContextAccessor,
            IDateTimeHelper dateTimeHelper,
            IGSFileProvider fileProvider,
            IStaticCacheManager cacheManager,
            IDonViService donViService,
            IQuyenService quyenService
            )
        {
            this._NhanHienThiService = NhanHienThiService;
            this._workContext = workContext;
            this._httpContextAccessor = httpContextAccessor;
            this._dateTimeHelper = dateTimeHelper;
            this._fileProvider = fileProvider;
            this._cacheManager = cacheManager;
            _donViService = donViService;
            this._quyenService = quyenService;
        }

        #endregion#

        public virtual IActionResult Index()
        {
            /*var isChuyenMa = _donViService.CheckTaiKhoanDuocCapNhatMaT(_workContext.CurrentDonVi.ID,
                                                                       _workContext.CurrentCustomer.ID,
                                                                       _workContext.CurrentCustomer.IS_QUAN_TRI);
            var model = new DonViModel() { };
            model.IsThongBaoChuyenMa = isChuyenMa;*/
            return View();
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "";
        }

        private string GetLocalIPAddress(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    IPInterfaceProperties adapterProperties = item.GetIPProperties();

                    if (adapterProperties.GatewayAddresses.FirstOrDefault() != null)
                    {
                        foreach (UnicastIPAddressInformation ip in adapterProperties.UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                output = String.Format(@"{0},{1}", output, ip.Address.ToString());
                            }
                        }
                    }
                }
            }
            return output;
        }

        public virtual IActionResult SystemInfo()
        {

            if (!_quyenService.Authorize(StandardPermissionProvider.USERQLDMWidget))
                return AccessDeniedView();

            //prepare model
            var model = new SystemInfoModel();

            model.AppVersion = "6.0";
            model.ServerTimeZone = TimeZoneInfo.Local.StandardName;
            model.ServerLocalTime = DateTime.Now;
            model.UtcTime = DateTime.UtcNow;
            model.CurrentUserTime = _dateTimeHelper.ConvertToUserTime(DateTime.Now);
            model.HttpHost = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Host]+"("+ System.Net.Dns.GetHostName() + ": "+ GetLocalIPAddress(NetworkInterfaceType.Ethernet) +")";

            //ensure no exception is thrown
            try
            {
                model.OperatingSystem = Environment.OSVersion.VersionString;
                model.AspNetInfo = RuntimeEnvironment.GetSystemVersion();
                model.IsFullTrust = AppDomain.CurrentDomain.IsFullyTrusted.ToString();
            }
            catch { }

            foreach (var header in _httpContextAccessor.HttpContext.Request.Headers)
            {
                model.Headers.Add(new SystemInfoModel.HeaderModel
                {
                    Name = header.Key,
                    Value = header.Value
                });
            }

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var loadedAssemblyModel = new SystemInfoModel.LoadedAssembly
                {
                    FullName = assembly.FullName
                };

                //ensure no exception is thrown
                try
                {
                    loadedAssemblyModel.Location = assembly.IsDynamic ? null : assembly.Location;
                    loadedAssemblyModel.IsDebug = assembly.GetCustomAttributes(typeof(DebuggableAttribute), false)
                        .FirstOrDefault() is DebuggableAttribute attribute && attribute.IsJITOptimizerDisabled;

                    //https://stackoverflow.com/questions/2050396/getting-the-date-of-a-net-assembly
                    //we use a simple method because the more Jeff Atwood's solution doesn't work anymore 
                    //more info at https://blog.codinghorror.com/determining-build-date-the-hard-way/
                    loadedAssemblyModel.BuildDate = assembly.IsDynamic ? null : (DateTime?)TimeZoneInfo.ConvertTimeFromUtc(_fileProvider.GetLastWriteTimeUtc(assembly.Location), TimeZoneInfo.Local);

                }
                catch { }
                model.LoadedAssemblies.Add(loadedAssemblyModel);
            }

            model.CurrentStaticCacheManager = _cacheManager.GetType().Name;

            

            return View(model);
        }
		public virtual IActionResult EntryHeThong()
		{
			return View();
		}
		public virtual IActionResult EntryCongCu()
		{
			return View();
		}
		public virtual IActionResult EntryDuyet()
		{
			return View();
		}
        public virtual IActionResult EntryDanhMuc()
        {
            return View();
        }
    }
}
