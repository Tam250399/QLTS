//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/2/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.CCDC;
using GS.Web.Models.CCDC;
using System;

namespace GS.Web.Factories.CCDC
{
    public partial interface IGiamHongmatModelFactory 
    {    
        #region GiamHongmat
    
        GiamHongmatSearchModel PrepareGiamHongmatSearchModel(GiamHongmatSearchModel searchModel);
        
        GiamHongmatListModel PrepareGiamHongmatListModel(GiamHongmatSearchModel searchModel);
        
        GiamHongmatModel PrepareGiamHongmatModel(GiamHongmatModel model, GiamHongmat item, bool excludeProperties = false);
        
        void PrepareGiamHongmat(GiamHongmatModel model, GiamHongmat item);

        GiamHongmatListModel PrepareGiamHongMatForChonCongCu(GiamHongmatSearchModel searchModel);

        GiamHongmatModel PrepareGiamHongMatModel(GiamHongmatModel model, Decimal MapId, Decimal BoPhanId);

        #endregion
    }
}
