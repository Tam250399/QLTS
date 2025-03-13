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
    public partial interface IHinhThucMuaSamModelFactory
    {
        #region HinhThucMuaSam

        HinhThucMuaSamSearchModel PrepareHinhThucMuaSamSearchModel(HinhThucMuaSamSearchModel searchModel);

        HinhThucMuaSamListModel PrepareHinhThucMuaSamListModel(HinhThucMuaSamSearchModel searchModel);

        HinhThucMuaSamModel PrepareHinhThucMuaSamModel(HinhThucMuaSamModel model, HinhThucMuaSam item, bool excludeProperties = false);

        void PrepareHinhThucMuaSam(HinhThucMuaSamModel model, HinhThucMuaSam item);
        IList<SelectListItem> PrepareSelectListHinhThucMuaSam(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn hình thức mua sắm --", string valueFirstRow = "");
        bool CheckMaLoaiHinhThucMuaSam(decimal id, string ma);
        #endregion
    }
}
