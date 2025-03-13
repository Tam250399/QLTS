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
    public partial interface IVaiTroWidgetService 
    {    
    #region VaiTroWidget
        IList<VaiTroWidget> GetAllVaiTroWidgets();
        IPagedList <VaiTroWidget> SearchVaiTroWidgets(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        VaiTroWidget GetVaiTroWidgetById(decimal Id);
        IList<VaiTroWidget> GetVaiTroWidgetByIds(decimal[] newsIds);
        IList<VaiTroWidget> GetVaiTroWidgetsByVaiTro(decimal vaiTroId);
        void InsertVaiTroWidget(VaiTroWidget entity);
        void UpdateVaiTroWidget(VaiTroWidget entity);
        void DeleteVaiTroWidget(VaiTroWidget entity);
        bool AuthorizeWidget(decimal userId, string partialViewName);
     #endregion
    }
}
