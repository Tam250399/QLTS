//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GS.Web.Factories.DanhMuc
{
    public partial interface INguonTaiSanModelFactory
    {
        #region NguonVon

        NguonTaiSanSearchModel PrepareNguonTaiSanSearchModel(NguonTaiSanSearchModel searchModel);

        NguonTaiSanListModel PrepareNguonTaiSanListModel(NguonTaiSanSearchModel searchModel);

        NguonTaiSanModel PrepareNguonTaiSanModel(NguonTaiSanModel model, NguonTaiSan item, bool excludeProperties = false);

        void PrepareNguonTaiSan(NguonTaiSanModel model, NguonTaiSan item);
        //IList<SelectListItem> PrepareSelectlistNguonTaiSan(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn nguồn vốn --");
        //IList<SelectListItem> PrepareMultiSelectNguonTaiSan(IList<int> valSelecteds = null);
        //IList<NguonTaiSanModel> PrepareListNguonTaiSanDefault();
        #endregion
    }
}
