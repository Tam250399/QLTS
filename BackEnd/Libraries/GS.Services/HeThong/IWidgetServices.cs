//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 20/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.HeThong;

namespace GS.Services.HeThong
{
    public partial interface IWidgetService 
    {    
    #region Widget
        IList<Widget> GetAllWidgets();
        IPagedList <Widget> SearchWidgets(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        Widget GetWidgetById(decimal Id);
        IList<Widget> GetWidgetByIds(decimal[] newsIds);
        void InsertWidget(Widget entity);
        void UpdateWidget(Widget entity);
        void DeleteWidget(Widget entity);
     #endregion
    }
}
