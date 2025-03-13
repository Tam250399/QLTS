//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/2/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.CCDC;
using GS.Web.Models.CCDC;
using System;

namespace GS.Web.Factories.CCDC
{
    public partial interface ISuaChuaBaoDuongModelFactory 
    {    
        #region SuaChuaBaoDuong
    
        SuaChuaBaoDuongSearchModel PrepareSuaChuaBaoDuongSearchModel(SuaChuaBaoDuongSearchModel searchModel);
        
        SuaChuaBaoDuongListModel PrepareSuaChuaBaoDuongListModel(SuaChuaBaoDuongSearchModel searchModel);
        
        void PrepareSuaChuaBaoDuong(SuaChuaBaoDuongModel model, SuaChuaBaoDuong item);

        SuaChuaBaoDuongModel PrepareSuaChuaBaoDuongModel(SuaChuaBaoDuongModel model, Decimal MapId, Decimal BoPhanId);

        SuaChuaBaoDuongListModel PrepareSuaChuaModelForChonCongCu(SuaChuaBaoDuongSearchModel searchModel);

        SuaChuaBaoDuongModel PrepareCongCu(SuaChuaBaoDuongModel model, NhapXuatCongCu map, Decimal BoPhanId);

        #endregion
    }
}
