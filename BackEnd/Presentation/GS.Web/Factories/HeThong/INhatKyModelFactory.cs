//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.HeThong;
using GS.Web.Models.HeThong;
namespace GS.Web.Factories.HeThong
{
    public partial interface INhatKyModelFactory
    {
        #region NhatKy

        NhatKySearchModel PrepareNhatKySearchModel(NhatKySearchModel searchModel);

        NhatKyListModel PrepareNhatKyListModel(NhatKySearchModel searchModel);

        NhatKyModel PrepareNhatKyModel(NhatKyModel model, NhatKy item, bool excludeProperties = false);

        void PrepareNhatKy(NhatKyModel model, NhatKy item);

        #endregion
    }
}
