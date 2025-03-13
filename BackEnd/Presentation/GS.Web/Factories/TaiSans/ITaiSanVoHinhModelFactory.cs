//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
namespace GS.Web.Factories.TaiSans
{
    public partial interface ITaiSanVoHinhModelFactory
    {
        #region TaiSanVoHinh

        TaiSanVoHinhSearchModel PrepareTaiSanVoHinhSearchModel(TaiSanVoHinhSearchModel searchModel);

        TaiSanVoHinhListModel PrepareTaiSanVoHinhListModel(TaiSanVoHinhSearchModel searchModel);

        TaiSanVoHinhModel PrepareTaiSanVoHinhModel(TaiSanVoHinhModel model, TaiSanVoHinh item, bool excludeProperties = false);

        void PrepareTaiSanVoHinh(TaiSanVoHinhModel model, TaiSanVoHinh item);

        #endregion
    }
}
