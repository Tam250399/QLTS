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
    public partial interface IMucDichSuDungModelFactory
    {
        #region MucDichSuDung

        MucDichSuDungSearchModel PrepareMucDichSuDungSearchModel(MucDichSuDungSearchModel searchModel);

        MucDichSuDungListModel PrepareMucDichSuDungListModel(MucDichSuDungSearchModel searchModel);

        MucDichSuDungModel PrepareMucDichSuDungModel(MucDichSuDungModel model, MucDichSuDung item, bool excludeProperties = false);

        void PrepareMucDichSuDung(MucDichSuDungModel model, MucDichSuDung item);
        IList<SelectListItem> PrepareSelectListMucDichSuDung(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn mục đích sử dụng --", string valueFirstRow = "");

        #endregion
    }
}
