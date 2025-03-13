//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/7/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
namespace GS.Web.Factories.TaiSans
{
    public partial interface IKhaiThacTaiSanModelFactory 
    {    
        #region KhaiThacTaiSan
    
        KhaiThacTaiSanSearchModel PrepareKhaiThacTaiSanSearchModel(KhaiThacTaiSanSearchModel searchModel);

        KhaiThacTaiSanListModel PrepareKhaiThacTaiSanListModel(KhaiThacTaiSanSearchModel searchModel);
        
        KhaiThacTaiSanModel PrepareKhaiThacTaiSanModel(KhaiThacTaiSanModel model, KhaiThacTaiSan item, bool excludeProperties = false);
        
        void PrepareKhaiThacTaiSan(KhaiThacTaiSanModel model, KhaiThacTaiSan item);
        
        #endregion        
	}
}
