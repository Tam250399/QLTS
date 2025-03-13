//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/10/2021
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.BaoCaoDienTu;
using GS.Web.Models.BaoCaoDienTu;
using System.Collections.Generic;

namespace GS.Web.Factories.BaoCaoDienTus
{
    public partial interface IBaoCaoDienTuModelFactory
    {
        #region BaoCaoDienTu

        BaoCaoDienTuSearchModel PrepareBaoCaoDienTuSearchModel(BaoCaoDienTuSearchModel searchModel);

        BaoCaoDienTuListModel PrepareBaoCaoDienTuListModel(BaoCaoDienTuSearchModel searchModel);
        BaoCaoDienTuListModel PrepareBaoCaoChoDuyetListModel(BaoCaoDienTuSearchModel searchModel);

        BaoCaoDienTuModel PrepareBaoCaoDienTuModel(BaoCaoDienTuModel model, BaoCaoDienTu item, bool excludeProperties = false);

        void PrepareBaoCaoDienTu(BaoCaoDienTuModel model, BaoCaoDienTu item);
        BaoCaoDienTuModel PrepareBaoCaoDienTuModelView(BaoCaoDienTuModel model, BaoCaoDienTu item);

        #endregion
    }
}
