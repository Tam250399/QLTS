//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/1/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GS.Web.Factories.DanhMuc
{
    public partial interface INhomCongCuModelFactory 
    {    
        #region NhomCongCu
    
        NhomCongCuSearchModel PrepareNhomCongCuSearchModel(NhomCongCuSearchModel searchModel);
        
        NhomCongCuListModel PrepareNhomCongCuListModel(NhomCongCuSearchModel searchModel);
        
        NhomCongCuModel PrepareNhomCongCuModel(NhomCongCuModel model, NhomCongCu item, bool excludeProperties = false);
        
        void PrepareNhomCongCu(NhomCongCuModel model, NhomCongCu item);

        List<SelectListItem> PrepareDDLNhomCongCu(decimal donViId = 0, List<decimal> list_exceptID = null, bool isAddFirst = false, string strFirstRow = "-- Chọn nhóm công cụ, dụng cụ --", string valueFirstRow = "");

        #endregion
        MessageReturn ImportNhomCongCu(IMP_NhomCongCuModel model);
    }
}
