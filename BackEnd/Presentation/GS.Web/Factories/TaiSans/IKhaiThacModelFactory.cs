//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/7/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
namespace GS.Web.Factories.TaiSans
{
    public partial interface IKhaiThacModelFactory 
    {    
        #region KhaiThac
    
        KhaiThacSearchModel PrepareKhaiThacSearchModel(KhaiThacSearchModel searchModel);
        
        KhaiThacListModel PrepareKhaiThacListModel(KhaiThacSearchModel searchModel);
        
        KhaiThacModel PrepareKhaiThacModel(KhaiThacModel model, KhaiThac item, bool excludeProperties = false);
        
        void PrepareKhaiThac(KhaiThacModel model, KhaiThac item);
        
        #endregion        
	}
}
