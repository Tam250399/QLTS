//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/10/2021
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.BaoCaoDoiChieus;
using GS.Web.Models.BaoCao;
using System.Collections.Generic;

namespace GS.Web.Factories.BaoCao
{
    public partial interface IBaoCaoDoiChieuModelFactory
    {
        #region BaoCaoDoiChieu

        BaoCaoDoiChieuSearchModel PrepareBaoCaoDoiChieuSearchModel(BaoCaoDoiChieuSearchModel searchModel);

        BaoCaoDoiChieuListModel PrepareBaoCaoDoiChieuListModel(BaoCaoDoiChieuSearchModel searchModel);

        BaoCaoDoiChieuModel PrepareBaoCaoDoiChieuModel(BaoCaoDoiChieuModel model, BaoCaoDoiChieu item, bool excludeProperties = false);

        void PrepareBaoCaoDoiChieu(BaoCaoDoiChieuModel model, BaoCaoDoiChieu item);
        IList<string> InsertBaoCaoDoiChieuFromFolder(string folderName,int loai);

        #endregion
    }
}
