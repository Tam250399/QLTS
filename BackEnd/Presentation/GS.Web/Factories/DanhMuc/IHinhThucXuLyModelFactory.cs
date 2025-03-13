//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GS.Web.Factories.DanhMuc
{
    public partial interface IPhuongAnXuLyModelFactory 
    {    
        #region PhuongAnXuLy
    
        PhuongAnXuLySearchModel PreparePhuongAnXuLySearchModel(PhuongAnXuLySearchModel searchModel);
        
        PhuongAnXuLyListModel PreparePhuongAnXuLyListModel(PhuongAnXuLySearchModel searchModel);
        
        PhuongAnXuLyModel PreparePhuongAnXuLyModel(PhuongAnXuLyModel model, PhuongAnXuLy item, bool excludeProperties = false);
        
        void PreparePhuongAnXuLy(PhuongAnXuLyModel model, PhuongAnXuLy item);
        List<SelectListItem> DDLPhuongAn();

        #endregion
    }
}
