//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 20/5/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.HeThong;
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GS.Web.Factories.HeThong
{
    public partial interface IWidgetModelFactory 
    {    
        #region Widget
    
        WidgetSearchModel PrepareWidgetSearchModel(WidgetSearchModel searchModel);
        
        WidgetListModel PrepareWidgetListModel(WidgetSearchModel searchModel);
        
        WidgetModel PrepareWidgetModel(WidgetModel model, Widget item, bool excludeProperties = false);
        
        void PrepareWidget(WidgetModel model, Widget item);

        IList<SelectListItem> PrepareMultiSelectListWidget(IList<int> valSelecteds = null);

        #endregion
    }
}
