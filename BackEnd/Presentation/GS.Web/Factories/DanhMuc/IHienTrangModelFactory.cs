//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using System.Collections.Generic;

namespace GS.Web.Factories.DanhMuc
{
    public partial interface IHienTrangModelFactory
    {
        #region HienTrang

        HienTrangSearchModel PrepareHienTrangSearchModel(HienTrangSearchModel searchModel);
        IList<HienTrangModel> PrepareListHienTrangModel(HienTrangSearchModel searchModel);
        HienTrangListModel PrepareHienTrangListModel(HienTrangSearchModel searchModel);
        HienTrangModel PrepareHienTrangModel(HienTrangModel model, HienTrang item, bool excludeProperties = false);
        void PrepareHienTrang(HienTrangModel model, HienTrang item);
        bool CheckIsHienTrangNhapSai(decimal donViID, ObjHienTrang ObjHienTrang = null);
        bool CheckIsListHienTrangNhapSai(decimal donViID, IList<ObjHienTrang> listHienTrangId = null);
        bool IsAnyHienTrangSai(IList<ObjHienTrang> lstHienTrang, decimal donViId);

        #endregion
    }
}
