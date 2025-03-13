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
    public partial interface IDongXeModelFactory
    {
        #region DongXe

        DongXeSearchModel PrepareDongXeSearchModel(DongXeSearchModel searchModel);

        DongXeListModel PrepareDongXeListModel(DongXeSearchModel searchModel);

        DongXeModel PrepareDongXeModel(DongXeModel model, DongXe item, bool excludeProperties = false);

        void PrepareDongXe(DongXeModel model, DongXe item);

        bool CheckMaDongXe(decimal id = 0, string ma = null);
        IList<SelectListItem> PrepareSelectDongXe(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn số loại --", decimal? nhanXeId = 0);
        #endregion
    }
}
