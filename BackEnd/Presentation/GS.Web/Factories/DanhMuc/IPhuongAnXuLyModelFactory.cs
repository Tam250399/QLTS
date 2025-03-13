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
    public partial interface IHinhThucXuLyModelFactory 
    {    
        #region HinhThucXuLy
    
        HinhThucXuLySearchModel PrepareHinhThucXuLySearchModel(HinhThucXuLySearchModel searchModel);
        
        HinhThucXuLyListModel PrepareHinhThucXuLyListModel(HinhThucXuLySearchModel searchModel);
        
        HinhThucXuLyModel PrepareHinhThucXuLyModel(HinhThucXuLyModel model, HinhThucXuLy item, bool excludeProperties = false);
        
        void PrepareHinhThucXuLy(HinhThucXuLyModel model, HinhThucXuLy item);
        List<SelectListItem> DDLPhuongAnByHinhThuc(int HinhThucId = 0);
        IList<SelectListItem> PrepareSelectListHinhThucXuLy(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "Tất cả");
        #endregion
    }
}
