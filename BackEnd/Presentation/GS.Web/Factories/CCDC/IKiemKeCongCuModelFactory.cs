//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.CCDC;
using GS.Web.Models.CCDC;
using System;

namespace GS.Web.Factories.CCDC
{
    public partial interface IKiemKeCongCuModelFactory 
    {    
        #region KiemKeCongCu
    
        KiemKeCongCuSearchModel PrepareKiemKeCongCuSearchModel(KiemKeCongCuSearchModel searchModel);
        
        KiemKeCongCuListModel PrepareKiemKeCongCuListModel(KiemKeCongCuSearchModel searchModel);
        
        KiemKeCongCuModel PrepareKiemKeCongCuModel(KiemKeCongCuModel model, KiemKeCongCu item, bool excludeProperties = false);
        
        void PrepareKiemKeCongCu(KiemKeCongCuModel model, KiemKeCongCu item);

        KiemKeCongCuListModel PrepareTimKiemCongCu(KiemKeCongCuSearchModel searchModel);

        KiemKeCongCuModel PrepareCongCu(KiemKeCongCuModel model, NhapXuatCongCu map, Decimal BoPhanId);

        CongCuListModel PrepareListCongCu(CongCuSearchModel searchModel);

        #endregion
    }
}
