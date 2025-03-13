//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.SHTD;
using GS.Web.Models.SHTD;
namespace GS.Web.Factories.SHTD
{
    public partial interface INhatKyTaiSanToanDanModelFactory 
    {    
        #region NhatKyTaiSanToanDan
    
        NhatKyTaiSanToanDanSearchModel PrepareNhatKyTaiSanToanDanSearchModel(NhatKyTaiSanToanDanSearchModel searchModel);
        
        NhatKyTaiSanToanDanListModel PrepareNhatKyTaiSanToanDanListModel(NhatKyTaiSanToanDanSearchModel searchModel);
        
        NhatKyTaiSanToanDanModel PrepareNhatKyTaiSanToanDanModel(NhatKyTaiSanToanDanModel model, NhatKyTaiSanToanDan item, bool excludeProperties = false);
        
        void PrepareNhatKyTaiSanToanDan(NhatKyTaiSanToanDanModel model, NhatKyTaiSanToanDan item);
        
        #endregion        
	}
}
