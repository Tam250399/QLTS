//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.ThuocTinhs;
using GS.Web.Models.ThuocTinh;
namespace GS.Web.Factories.ThuocTinhs
{
    public partial interface IThuocTinhTaiSanModelFactory 
    {    
        #region ThuocTinhTaiSan
    
        ThuocTinhTaiSanSearchModel PrepareThuocTinhTaiSanSearchModel(ThuocTinhTaiSanSearchModel searchModel);
        
        ThuocTinhTaiSanListModel PrepareThuocTinhTaiSanListModel(ThuocTinhTaiSanSearchModel searchModel);
        
        ThuocTinhTaiSanModel PrepareThuocTinhTaiSanModel(ThuocTinhTaiSanModel model, ThuocTinhTaiSan item, bool excludeProperties = false);
        
        void PrepareThuocTinhTaiSan(ThuocTinhTaiSanModel model, ThuocTinhTaiSan item);
        
        #endregion        
	}
}
