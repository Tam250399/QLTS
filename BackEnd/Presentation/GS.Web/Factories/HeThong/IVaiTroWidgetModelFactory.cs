//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 20/5/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.HeThong;
using GS.Web.Models.HeThong;

namespace GS.Web.Factories.HeThong
{
    public partial interface IVaiTroWidgetModelFactory 
    {    
        #region VaiTroWidget
    
        VaiTroWidgetSearchModel PrepareVaiTroWidgetSearchModel(VaiTroWidgetSearchModel searchModel);
        
        VaiTroWidgetListModel PrepareVaiTroWidgetListModel(VaiTroWidgetSearchModel searchModel);
        
        VaiTroWidgetModel PrepareVaiTroWidgetModel(VaiTroWidgetModel model, VaiTroWidget item, bool excludeProperties = false);
        
        void PrepareVaiTroWidget(VaiTroWidgetModel model, VaiTroWidget item);
        
        #endregion        
	}
}
