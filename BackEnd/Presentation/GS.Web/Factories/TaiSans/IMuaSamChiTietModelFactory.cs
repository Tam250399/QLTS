//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 21/7/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
namespace GS.Web.Factories.TaiSans
{
    public partial interface IMuaSamChiTietModelFactory 
    {    
        #region MuaSamChiTiet
    
        MuaSamChiTietSearchModel PrepareMuaSamChiTietSearchModel(MuaSamChiTietSearchModel searchModel);
        
        MuaSamChiTietListModel PrepareMuaSamChiTietListModel(MuaSamChiTietSearchModel searchModel);
        
        MuaSamChiTietModel PrepareMuaSamChiTietModel(MuaSamChiTietModel model, MuaSamChiTiet item, bool excludeProperties = false);
        
        void PrepareMuaSamChiTiet(MuaSamChiTietModel model, MuaSamChiTiet item);
        
        #endregion        
	}
}
