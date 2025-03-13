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
    public partial interface INhanXeModelFactory
    {
        #region NhanXe

        NhanXeSearchModel PrepareNhanXeSearchModel(NhanXeSearchModel searchModel);

        NhanXeListModel PrepareNhanXeListModel(NhanXeSearchModel searchModel);

        NhanXeModel PrepareNhanXeModel(NhanXeModel model, NhanXe item, bool excludeProperties = false);

        void PrepareNhanXe(NhanXeModel model, NhanXe item);

        bool CheckMaNhanXe(decimal id = 0, string ma = null);

        IList<SelectListItem> PrepareSelectListNhanXe(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn nhãn xe --");
        #endregion
    }
}
