//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace GS.Web.Factories.DanhMuc
{
    public partial interface IDoiTacModelFactory
    {
        #region DoiTac

        DoiTacSearchModel PrepareDoiTacSearchModel(DoiTacSearchModel searchModel);

        DoiTacListModel PrepareDoiTacListModel(DoiTacSearchModel searchModel);

        DoiTacModel PrepareDoiTacModel(DoiTacModel model, DoiTac item, bool excludeProperties = false);

        void PrepareDoiTac(DoiTacModel model, DoiTac item);

        List<SelectListItem> PrepareSelectListDoiTac(Decimal LoaiDoiTacId = 0, decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn đối tác / khách hàng --", string valueFirstRow = "");
        bool CheckMaTrung(string Ma,decimal ID, decimal Don_vi_id);
        #endregion

        #region Import DoiTac
        MessageReturn ImportDoiTac(IMP_DoiTacModel model);
        
        #endregion Import DoiTac
    }
}
