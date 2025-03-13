namespace GS.Services.HeThong
{
    /// <summary>
    /// Represents default values related to configuration services
    /// </summary>
    public static partial class GSCauHinhMacDinh
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static string CauHinhsAllCacheKey => "GS.cauhinh.all";

        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        public static string CauHinhsPatternCacheKey => "GS.cauhinh.";
    }
}