//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GS.Web.Factories.DanhMuc
{
    public partial interface IDonViBoPhanModelFactory
    {
        #region DonViBoPhan

        DonViBoPhanSearchModel PrepareDonViBoPhanSearchModel(DonViBoPhanSearchModel searchModel);

        DonViBoPhanListModel PrepareDonViBoPhanListModel(DonViBoPhanSearchModel searchModel);

        DonViBoPhanModel PrepareDonViBoPhanModel(DonViBoPhanModel model, DonViBoPhan item, bool excludeProperties = false);
        
        void PrepareDonViBoPhan(DonViBoPhanModel model, DonViBoPhan item);
        IList<SelectListItem> PrepareSelectListDonViBoPhan(decimal? valSelected = 0, decimal? DonViId = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn bộ phận sử dụng --", string valueFirstRow = "");
        bool CheckTenDonViBoPhan(decimal? id, string ten = null);
        #endregion
        MessageReturn ImportDonViBoPhan(IMP_DonViBoPhanModel model);
    }
}
