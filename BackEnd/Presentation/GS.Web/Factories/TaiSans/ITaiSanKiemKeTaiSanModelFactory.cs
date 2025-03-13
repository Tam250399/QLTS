//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
namespace GS.Web.Factories.TaiSans
{
    public partial interface ITaiSanKiemKeTaiSanModelFactory 
    {    
        #region TaiSanKiemKeTaiSan
    
        TaiSanKiemKeTaiSanSearchModel PrepareTaiSanKiemKeTaiSanSearchModel(TaiSanKiemKeTaiSanSearchModel searchModel);
        
        TaiSanKiemKeTaiSanListModel PrepareTaiSanKiemKeTaiSanListModel(TaiSanKiemKeTaiSanSearchModel searchModel);
        
        TaiSanKiemKeTaiSanModel PrepareTaiSanKiemKeTaiSanModel(TaiSanKiemKeTaiSanModel model, TaiSanKiemKeTaiSan item, bool excludeProperties = false);
        
        void PrepareTaiSanKiemKeTaiSan(TaiSanKiemKeTaiSanModel model, TaiSanKiemKeTaiSan item);
        
        #endregion        
	}
}
