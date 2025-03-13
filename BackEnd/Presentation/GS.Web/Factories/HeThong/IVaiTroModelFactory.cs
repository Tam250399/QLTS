//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.HeThong;
using GS.Web.Models.HeThong;
namespace GS.Web.Factories.HeThong
{
    public partial interface IVaiTroModelFactory
    {
        #region VaiTro

        VaiTroSearchModel PrepareVaiTroSearchModel(VaiTroSearchModel searchModel);

        VaiTroListModel PrepareVaiTroListModel(VaiTroSearchModel searchModel);

        VaiTroModel PrepareVaiTroModel(VaiTroModel model, VaiTro item, bool excludeProperties = false);

        void PrepareVaiTro(VaiTroModel model, VaiTro item);
        bool CheckMaVaiTro(string MA, decimal id = 0);
        #endregion
    }
}
