//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
namespace GS.Web.Factories.TaiSans
{
    public partial interface ITaiSanVktModelFactory
    {
        #region TaiSanVkt

        TaiSanVktSearchModel PrepareTaiSanVktSearchModel(TaiSanVktSearchModel searchModel);

        TaiSanVktListModel PrepareTaiSanVktListModel(TaiSanVktSearchModel searchModel);

        TaiSanVktModel PrepareTaiSanVktModel(TaiSanVktModel model, TaiSanVkt item, bool excludeProperties = false, bool isTangMoi = true);

        void PrepareTaiSanVkt(TaiSanVktModel model, TaiSanVkt item);

        #endregion
    }
}
