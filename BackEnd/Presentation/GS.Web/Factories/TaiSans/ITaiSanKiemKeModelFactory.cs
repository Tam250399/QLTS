//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
using System;

namespace GS.Web.Factories.TaiSans
{
    public partial interface ITaiSanKiemKeModelFactory 
    {    
        #region TaiSanKiemKe
    
        TaiSanKiemKeSearchModel PrepareTaiSanKiemKeSearchModel(TaiSanKiemKeSearchModel searchModel);
        
        TaiSanKiemKeListModel PrepareTaiSanKiemKeListModel(TaiSanKiemKeSearchModel searchModel);
        
        TaiSanKiemKeModel PrepareTaiSanKiemKeModel(TaiSanKiemKeModel model, TaiSanKiemKe item, bool excludeProperties = false);
        
        void PrepareTaiSanKiemKe(TaiSanKiemKeModel model, TaiSanKiemKe item);

        void PrepareKiemKeTaiSan(TaiSanKiemKeTaiSan item, TaiSan taisan, Decimal KiemKeId);
        void PrepareKiemKeTaiSanByBienDong(TaiSanKiemKeTaiSan item, BienDong biendong, Decimal KiemKeId, DateTime NgayKiemKe);

        #endregion
    }
}
