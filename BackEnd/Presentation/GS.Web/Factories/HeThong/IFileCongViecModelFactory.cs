//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.HeThong;
using GS.Web.Models.HeThong;

namespace GS.Web.Factories.HeThong
{
    public partial interface IFileCongViecModelFactory
    {
        #region FileCongViec

        FileCongViecSearchModel PrepareFileCongViecSearchModel(FileCongViecSearchModel searchModel);

        FileCongViecListModel PrepareFileCongViecListModel(FileCongViecSearchModel searchModel);

        FileCongViecModel PrepareFileCongViecModel(FileCongViecModel model, FileCongViec item, bool excludeProperties = false);

        void PrepareFileCongViec(FileCongViecModel model, FileCongViec item);
        void SaveWorkFileOnDisk(FileCongViec item, byte[] fileContent);
        #endregion
    }
}
