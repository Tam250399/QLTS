//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.CCDC;
using GS.Core.Domain.Common;
using GS.Web.Models.CCDC;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace GS.Web.Factories.CCDC
{
    public partial interface ICongCuModelFactory 
    {    
        #region CongCu
    
        CongCuSearchModel PrepareCongCuSearchModel(CongCuSearchModel searchModel);
        
        CongCuListModel PrepareCongCuListModel(CongCuSearchModel searchModel);
        
        CongCuModel PrepareCongCuModel(CongCuModel model, CongCu item, bool excludeProperties = false);

        CongCuModel PrepareListCongCuModelFromNXCC(NhapXuatCongCu nxcc, CongCuModel model);

        void PrepareCongCu(CongCu item, LoCongCuModel LCCModel);

        void PrepareLoCongCu(NhapXuatCongCu map, LoCongCuModel LCCModel);

        void PrepareUpdateCongCu(CongCu item, CongCuModel model);

        XuatNhapListModel PrepareXuatNhapListModel(CongCuSearchModel searchModel);
        List<SelectListItem> PrepareDDLCongCuByNhom(Decimal nhomId);
        List<SelectListItem> PrepareDDLCongCuDonVi(Decimal donviId);
        #endregion
        MessageReturn importCCDC(IMP_CCDCModel model);
        MessageReturn importKiemKe(IMP_KEMKE_CCDC model);
    }
}
