//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/11/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
namespace GS.Web.Factories.TaiSans
{
    public partial interface IDeNghiXuLyTaiSanModelFactory 
    {    
        #region DeNghiXuLyTaiSan
    
        DeNghiXuLyTaiSanSearchModel PrepareDeNghiXuLyTaiSanSearchModel(DeNghiXuLyTaiSanSearchModel searchModel);
        
        DeNghiXuLyTaiSanListModel PrepareDeNghiXuLyTaiSanListModel(DeNghiXuLyTaiSanSearchModel searchModel);
        
        DeNghiXuLyTaiSanModel PrepareDeNghiXuLyTaiSanModel(DeNghiXuLyTaiSanModel model, DeNghiXuLyTaiSan item, bool excludeProperties = false);
        
        void PrepareDeNghiXuLyTaiSan(DeNghiXuLyTaiSanModel model, DeNghiXuLyTaiSan item);
        
        #endregion        
	}
}
