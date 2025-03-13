//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/10/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DB;
using GS.Web.Models.DB;
namespace GS.Web.Factories.DB
{
    public partial interface ITaiSanNhatKyModelFactory 
    {    
        #region TaiSanNhatKy
    
        TaiSanNhatKySearchModel PrepareTaiSanNhatKySearchModel(TaiSanNhatKySearchModel searchModel);
        
        TaiSanNhatKyListModel PrepareTaiSanNhatKyListModel(TaiSanNhatKySearchModel searchModel);
        
        TaiSanNhatKyModel PrepareTaiSanNhatKyModel(TaiSanNhatKyModel model, TaiSanNhatKy item, bool excludeProperties = false);
        
        void PrepareTaiSanNhatKy(TaiSanNhatKyModel model, TaiSanNhatKy item);
        
        #endregion        
	}
}
