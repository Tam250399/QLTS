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
    public partial interface INguonVonModelFactory
    {
        #region NguonVon

        NguonVonSearchModel PrepareNguonVonSearchModel(NguonVonSearchModel searchModel);

        NguonVonListModel PrepareNguonVonListModel(NguonVonSearchModel searchModel);

        NguonVonModel PrepareNguonVonModel(NguonVonModel model, NguonVon item, bool excludeProperties = false);

        void PrepareNguonVon(NguonVonModel model, NguonVon item);
        IList<SelectListItem> PrepareSelectlistNguonVon(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn nguồn vốn --");
        IList<SelectListItem> PrepareMultiSelectNguonVon(IList<int> valSelecteds = null);
        IList<NguonVonModel> PrepareListNguonVonDefault();
        #endregion
    }
}
