//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.CCDC;
using GS.Web.Models.CCDC;
namespace GS.Web.Factories.CCDC
{
    public partial interface ICongCuDonViModelFactory 
    {    
        #region CongCuDonVi
    
        CongCuDonViSearchModel PrepareCongCuDonViSearchModel(CongCuDonViSearchModel searchModel);
        
        CongCuDonViListModel PrepareCongCuDonViListModel(CongCuDonViSearchModel searchModel);
        
        CongCuDonViModel PrepareCongCuDonViModel(CongCuDonViModel model, CongCuDonVi item, bool excludeProperties = false);
        
        void PrepareCongCuDonVi(CongCuDonViModel model, CongCuDonVi item);
        
        #endregion        
	}
}
