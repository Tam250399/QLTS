//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.SHTD;
using GS.Web.Models.SHTD;
namespace GS.Web.Factories.SHTD
{
    public partial interface IKetQuaModelFactory 
    {    
        #region KetQua
    
        KetQuaSearchModel PrepareKetQuaSearchModel(KetQuaSearchModel searchModel);
        
        KetQuaListModel PrepareKetQuaListModel(KetQuaSearchModel searchModel);
        
        KetQuaModel PrepareKetQuaModel(KetQuaModel model, KetQua item, bool excludeProperties = false, bool isDDL = false);
        
        void PrepareKetQua(KetQuaModel model, KetQua item);
        
        #endregion        
	}
}
