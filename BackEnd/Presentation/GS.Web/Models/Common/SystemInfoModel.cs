using GS.Web.Framework.Models;
using GS.Web.Framework.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.Common
{
    public class SystemInfoModel : BaseGSModel
    {
        public SystemInfoModel()
        {
            Headers = new List<HeaderModel>();
            LoadedAssemblies = new List<LoadedAssembly>();
        }

        public string AspNetInfo { get; set; }

        public string IsFullTrust { get; set; }

        public string AppVersion { get; set; }

        public string OperatingSystem { get; set; }

        public DateTime ServerLocalTime { get; set; }

        public string ServerTimeZone { get; set; }

        public DateTime UtcTime { get; set; }

        public DateTime CurrentUserTime { get; set; }

        public string CurrentStaticCacheManager { get; set; }

        public string HttpHost { get; set; }

        public IList<HeaderModel> Headers { get; set; }

        public IList<LoadedAssembly> LoadedAssemblies { get; set; }

        public bool RedisEnabled { get; set; }

        public bool UseRedisToStoreDataProtectionKeys { get; set; }

        public bool UseRedisForCaching { get; set; }



        public partial class HeaderModel : BaseGSModel
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public partial class LoadedAssembly : BaseGSModel
        {
            public string FullName { get; set; }
            public string Location { get; set; }
            public bool IsDebug { get; set; }
            public DateTime? BuildDate { get; set; }
        }
    }
}
