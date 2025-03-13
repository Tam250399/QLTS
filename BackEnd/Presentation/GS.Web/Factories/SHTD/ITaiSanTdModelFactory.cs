//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.SHTD;
using GS.Web.Models.SHTD;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace GS.Web.Factories.SHTD
{
    public partial interface ITaiSanTdModelFactory 
    {    
        #region TaiSanTd
    
        TaiSanTdSearchModel PrepareTaiSanTdSearchModel(TaiSanTdSearchModel searchModel);
        
        TaiSanTdListModel PrepareTaiSanTdListModel(TaiSanTdSearchModel searchModel);
        
        TaiSanTdModel PrepareTaiSanTdModel(TaiSanTdModel model, TaiSanTd item, bool excludeProperties = false,bool is_soluongcon = false, int SLDaChon = 0, Guid TSXLGuid = new Guid(), int LoaiXuLy = 0, bool IS_DDL = true, int SoLuongTrenFrom = 0);
        
        void PrepareTaiSanTd(TaiSanTdModel model, TaiSanTd item);
        List<SelectListItem> GetDDLTaiSan();
        List<SelectListItem> PrepareSelectListTSTD(decimal selected = 0, bool isAddFirst = true, decimal loaiHinhTaiSan = 0, string strAddFirst = "-- Chọn đất --");

        #endregion
    }
}
