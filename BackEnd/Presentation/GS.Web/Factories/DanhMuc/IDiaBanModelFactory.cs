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
    public partial interface IDiaBanModelFactory
    {
        #region DiaBan

        DiaBanSearchModel PrepareDiaBanSearchModel(DiaBanSearchModel searchModel);

        DiaBanListModel PrepareDiaBanListModel(DiaBanSearchModel searchModel);

        DiaBanModel PrepareDiaBanModel(DiaBanModel model, DiaBan item, bool excludeProperties = false);

        void PrepareDiaBan(DiaBanModel model, DiaBan item);
        bool CheckMaDiaBan(decimal Id, string Ma);
        IList<SelectListItem> PrepareDiaBanAvailabele(decimal? ParentId = 0, decimal? Valselected = 0, decimal? CapDiaBan = (int)enumLOAI_DIABAN.ALL, decimal? QuocGiaId = 0, bool IsAddFirst = false, string strFirstRow = null, bool IsListChaCon = true);
        bool CheckDiaBanXaByHuyenId(decimal HuyenId);
        #endregion
    }
}
