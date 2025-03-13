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
    public partial interface IQuocGiaModelFactory
    {
        #region QuocGia

        QuocGiaSearchModel PrepareQuocGiaSearchModel(QuocGiaSearchModel searchModel);

        QuocGiaListModel PrepareQuocGiaListModel(QuocGiaSearchModel searchModel);

        QuocGiaModel PrepareQuocGiaModel(QuocGiaModel model, QuocGia item, bool excludeProperties = false);

        void PrepareQuocGia(QuocGiaModel model, QuocGia item);

        bool CheckMaQuocGia(string ma = null, decimal id = 0);
        IList<SelectListItem> PrepareSelectListQuocGias(decimal? valSelected = 0, bool IsAddFirst = false, string strFirstRow = "-- Chọn Quốc gia --", string valueFirstRow = "");
        #endregion
    }
}
