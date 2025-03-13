//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;
namespace GS.Web.Factories.DanhMuc
{
    public partial interface IBaoCaoModelFactory 
    {    
        #region BaoCao
    
        BaoCaoSearchModel PrepareBaoCaoSearchModel(BaoCaoSearchModel searchModel);
        
        BaoCaoListModel PrepareBaoCaoListModel(BaoCaoSearchModel searchModel);
        
        BaoCaoModel PrepareBaoCaoModel(BaoCaoModel model, GS.Core.Domain.DanhMuc.BaoCao item, bool excludeProperties = false);
        
        void PrepareBaoCao(BaoCaoModel model, GS.Core.Domain.DanhMuc.BaoCao item);

        bool CheckMaBaoCao(decimal id = 0, string ma = null);

        #endregion
    }
}
