//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.SHTD;
using GS.Web.Models.SHTD;
using GS.Web.Models.ThuocTinh;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace GS.Web.Factories.SHTD
{
    public partial interface IXuLyModelFactory 
    {
        #region XuLy
        List<SelectListItem> PrepareMultiDDLKetQuaXuLyTaiSan(bool isAddFirst = true, IList<int> valSelected = null);
        XuLySearchModel PrepareXuLySearchModel(XuLySearchModel searchModel);
        
        XuLyListModel PrepareXuLyListModel(XuLySearchModel searchModel);
        
        XuLyModel PrepareXuLyModel(XuLyModel model, XuLy item, bool excludeProperties = false, bool is_DLL = false);
        
        void PrepareXuLy(XuLyModel model, XuLy item);
        List<modelThuocTinh> PrepareListmodelThuocTinh(Guid GUID = new Guid(), int PhuongAnXuLyId = 0, int TaiSanTdId = 0, Guid TaiSanXuLyGuid = new Guid());
        XuLyModel PrepareEditXuLyModel(XuLyModel model, XuLy item, bool excludeProperties = false);
        List<SelectListItem> PrepareDDLPhuongAnXuLyTaiSan(bool isAddFirst = true, int valSelected = 0);
        List<SelectListItem> PrepareDDLKetQuaXuLyTaiSan(bool isAddFirst = true, int valSelected = 0);
        #endregion
    }
}
