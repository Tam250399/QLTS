
namespace GS.Core.Data
{
    /// <summary>
    /// Represents default values related to data settings
    /// </summary>
    public static partial class GSDataSettingsDefaults
    {
        /// <summary>
        /// Gets a path to the file that was used in old nopCommerce versions to contain data settings
        /// </summary>
        public static string ObsoleteFilePath => @"~/Settings.txt";

        /// <summary>
        /// Gets a path to the file that contains data settings
        /// </summary>
        public static string FilePath => "~/App_Data/dataSettings.json";
        public static string FolderWorkFiles => "~/App_Data/WorkFiles/";
        public static string FolderTrashFiles => "~/App_Data/TrashFiles/";
        
    }
}