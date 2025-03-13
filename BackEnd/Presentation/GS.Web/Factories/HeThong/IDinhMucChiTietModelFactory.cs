//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/6/2021
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.HeThong;
using GS.Web.Models.HeThong;
namespace GS.Web.Factories.HeThong
{
    public partial interface IDinhMucChiTietModelFactory 
    {    
        #region DinhMucChiTiet
    
        DinhMucChiTietSearchModel PrepareDinhMucChiTietSearchModel(DinhMucChiTietSearchModel searchModel);
        
        DinhMucChiTietListModel PrepareDinhMucChiTietListModel(DinhMucChiTietSearchModel searchModel);
        
        DinhMucChiTietModel PrepareDinhMucChiTietModel(DinhMucChiTietModel model, DinhMucChiTiet item, bool excludeProperties = false);
        
        void PrepareDinhMucChiTiet(DinhMucChiTietModel model, DinhMucChiTiet item);
        
        #endregion        
	}
}
