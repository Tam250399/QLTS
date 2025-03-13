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
    public partial interface IChucDanhModelFactory
    {
        #region ChucDanh

        ChucDanhSearchModel PrepareChucDanhSearchModel(ChucDanhSearchModel searchModel);

        ChucDanhListModel PrepareChucDanhListModel(ChucDanhSearchModel searchModel);

        ChucDanhModel PrepareChucDanhModel(ChucDanhModel model, ChucDanh item, bool excludeProperties = false);

        void PrepareChucDanh(ChucDanhModel model, ChucDanh item);

        bool CheckMaChucDanh(decimal? id = 0, string ma = null);
        IList<SelectListItem> PrepareSelectListChucDanh(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn chức danh --", bool isMultiple=false, List<int> valSelecteds = null, decimal? valueExcuted = null, bool IsPhanCap = false);
        #endregion
    }
}
