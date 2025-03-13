//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace GS.Web.Factories.DanhMuc
{
    public partial interface INguonGocTaiSanModelFactory 
    {    
        #region NguonGocTaiSan
    
        NguonGocTaiSanSearchModel PrepareNguonGocTaiSanSearchModel(NguonGocTaiSanSearchModel searchModel);
        
        NguonGocTaiSanListModel PrepareNguonGocTaiSanListModel(NguonGocTaiSanSearchModel searchModel);
        
        NguonGocTaiSanModel PrepareNguonGocTaiSanModel(NguonGocTaiSanModel model, NguonGocTaiSan item, bool excludeProperties = false);
        
        void PrepareNguonGocTaiSan(NguonGocTaiSanModel model, NguonGocTaiSan item);
        
        List<SelectListItem> PrepareSelectListNguonGocTaiSan(decimal selected = 0, bool isAddFirst = true, bool isDisable = false, bool isTichThu = true, bool isAll = true);
        String PrepareTenNguonGocByListId(string stringID);
        bool CheckMaNguonGocTaiSan(decimal Id, string Ma);
        #endregion
    }
}
