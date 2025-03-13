//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
namespace GS.Web.Factories.TaiSans
{
    public partial interface ITaiSanClnModelFactory
    {
        #region TaiSanCln

        TaiSanClnSearchModel PrepareTaiSanClnSearchModel(TaiSanClnSearchModel searchModel);

        TaiSanClnListModel PrepareTaiSanClnListModel(TaiSanClnSearchModel searchModel);

        TaiSanClnModel PrepareTaiSanClnModel(TaiSanClnModel model, TaiSanCln item, bool excludeProperties = false);

        void PrepareTaiSanCln(TaiSanClnModel model, TaiSanCln item);

        #endregion
    }
}
