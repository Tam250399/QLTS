﻿namespace GS.Core.Configuration
{
    /// <summary>
    /// Represents startup GS configuration parameters
    /// </summary>
    public partial class GSConfig
    {
        /// <summary>
        /// Gets or sets a value indicating whether to display the full error in production environment.
        /// It's ignored (always enabled) in development environment
        /// </summary>
        public bool DisplayFullErrorStack { get; set; }

        /// <summary>
        /// Gets or sets connection string for Azure BLOB storage
        /// </summary>
        public string AzureBlobStorageConnectionString { get; set; }
        /// <summary>
        /// Gets or sets container name for Azure BLOB storage
        /// </summary>
        public string AzureBlobStorageContainerName { get; set; }
        /// <summary>
        /// Gets or sets end point for Azure BLOB storage
        /// </summary>
        public string AzureBlobStorageEndPoint { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we should use Redis server for caching (instead of default in-memory caching)
        /// </summary>
        public bool RedisCachingEnabled { get; set; }
        /// <summary>
        /// Gets or sets Redis connection string. Used when Redis caching is enabled
        /// </summary>
        public string RedisCachingConnectionString { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the data protection system should be configured to persist keys in the Redis database
        /// </summary>
        public bool PersistDataProtectionKeysToRedis { get; set; }

        /// <summary>
        /// Gets or sets path to database with user agent strings
        /// </summary>
        public string UserAgentStringsPath { get; set; }
        /// <summary>
        /// Gets or sets path to database with crawler only user agent strings
        /// </summary>
        public string CrawlerOnlyUserAgentStringsPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a store owner can install sample data during installation
        /// </summary>
        public bool DisableSampleDataDuringInstallation { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to use fast installation. 
        /// By default this setting should always be set to "False" (only for advanced users)
        /// </summary>
        public bool UseFastInstallationService { get; set; }
        /// <summary>
        /// Gets or sets a list of plugins ignored during nopCommerce installation
        /// </summary>
        public string PluginsIgnoredDuringInstallation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we should ignore startup tasks
        /// </summary>
        public bool IgnoreStartupTasks { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to clear /Plugins/bin directory on application startup
        /// </summary>
        public bool ClearPluginShadowDirectoryOnStartup { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to copy "locked" assemblies from /Plugins/bin directory to temporary subdirectories on application startup
        /// </summary>
        public bool CopyLockedPluginAssembilesToSubdirectoriesOnStartup { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to load an assembly into the load-from context, bypassing some security checks.
        /// </summary>
        public bool UseUnsafeLoadAssembly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to copy plugins library to the /Plugins/bin directory on application startup
        /// </summary>
        public bool UsePluginsShadowCopy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use backwards compatibility with SQL Server 2008 and SQL Server 2008R2
        /// </summary>
        public bool UseRowNumberForPaging { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to store TempData in the session state.
        /// By default the cookie-based TempData provider is used to store TempData in cookies.
        /// </summary>
        public bool UseSessionStateTempDataProvider { get; set; }
        public bool IsApiApplication { get; set; }
        public bool IsWriteLogFile { get; set; }
        public string ApiKeyEncrypt { get; set; }
        public string ApiValidIssuer { get; set; }
        public bool IsJobScheduler { get; set; }
        public bool IsJobChotTaiSan { get; set; }
        public string ApiUrlWebApi { get; set; }
        public string UrlQLTSC { get; set; }
        public bool IsUsingSSO { get; set; }
        public string UrlRenderReport { get; set; }
        public string UrlRenderReportDongBo { get; set; }
        public string UserNameKhoCSDL { get; set; }
        public string UserNameCTNS { get; set; }
        public string UserNameQLTSNN { get; set; }
        public string UserNameHTGTDB { get; set; }
		public string UserNameQLTSNoiNganh { get; set; }
		public ApiKhoCSDL ApiKhoCSDL { get; set; }
        
        public string UrlSendRequest { get; set; }
		public string UrlHaoMonTaiSanKT { get; set; }
		public bool IsUsingDBJobScheduler { get; set; }
		public int TimeJobChotTaiSan { get; set; }


	}
    public partial class ApiKhoCSDL
    {
        public string ApiUrlKhoCSDL { get; set; }
        public string Username { get; set; }
        //public string UsernameTsnn { get; set; }
        //public string UsernameTsc { get; set; }
        public string Password { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string UrlTokenKhoCSDLQG { get; set; }
        public string ApiUrlKhoCSDLResetPassword { get; set; }
    }
}