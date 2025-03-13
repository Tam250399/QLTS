//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.CCDC;
using GS.Web.Models.CCDC;
using System;
using System.Collections.Generic;

namespace GS.Web.Factories.CCDC
{
    public partial interface IXuatNhapModelFactory 
    {    
        #region XuatNhap
    
        XuatNhapSearchModel PrepareXuatNhapSearchModel(XuatNhapSearchModel searchModel);
        ThongTinCongCuListModel PrepareThongTinCongCuModel (decimal NhapXuatId);

        XuatNhapListModel PrepareXuatNhapListModel(XuatNhapSearchModel searchModel);

        XuatNhapListModel PrepareXuatNhapPhanBoListModel(XuatNhapSearchModel searchModel);

        XuatNhapModel PrepareXuatNhapModel(XuatNhapModel model, XuatNhap item, bool excludeProperties = false);
        
        void PrepareXuatNhap(XuatNhapModel model, XuatNhap item);

        XuatNhapModel PreparePhanBoModel(string StringGuid, bool whenEdit = false);

        XuatNhapListModel PrepareLuanChuyenCongCu(XuatNhapSearchModel searchModel);

        XuatNhapModel PrepareCongCuForLuanChuyen(XuatNhapModel model, Decimal MapId, Decimal BoPhanId, bool whenEdit = false);

        XuatNhapListModel PrepareLuanChuyenListModel(LuanChuyenSearchModel searchModel);

        void PrepareLuanChuyenEdit(XuatNhapModel model, XuatNhap item);
        LuanChuyenSearchModel PrepareLuanChuyenSearchModel(LuanChuyenSearchModel searchModel);

        #endregion
    }
}
