﻿
namespace GS.Core.Caching
{
    /// <summary>
    /// Represents default values related to caching
    /// </summary>
    public static partial class GSCachingDefaults
    {
        /// <summary>
        /// Gets the default cache time in minutes
        /// </summary>
        public static int CacheTime => 60;

        /// <summary>
        /// Gets the key used to store the protection key list to Redis (used with the PersistDataProtectionKeysToRedis option enabled)
        /// </summary>
        public static string RedisDataProtectionKey => "GS.DataProtectionKeys";
    }
}