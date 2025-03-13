//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
namespace GS.Web.Factories.TaiSans
{
    public partial interface ITaiSanMayMocModelFactory
    {
        #region TaiSanMayMoc

        TaiSanMayMocSearchModel PrepareTaiSanMayMocSearchModel(TaiSanMayMocSearchModel searchModel);

        TaiSanMayMocListModel PrepareTaiSanMayMocListModel(TaiSanMayMocSearchModel searchModel);

        TaiSanMayMocModel PrepareTaiSanMayMocModel(TaiSanMayMocModel model, TaiSanMayMoc item, bool excludeProperties = false);

        void PrepareTaiSanMayMoc(TaiSanMayMocModel model, TaiSanMayMoc item);
        #endregion
    }
}
