//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.SHTD;
using GS.Web.Models.SHTD;
namespace GS.Web.Factories.SHTD
{
    public partial interface ITaiSanTdXuLyModelFactory 
    {    
        #region TaiSanTdXuLy
    
        TaiSanTdXuLySearchModel PrepareTaiSanTdXuLySearchModel(TaiSanTdXuLySearchModel searchModel);
        
        TaiSanTdXuLyListModel PrepareTaiSanTdXuLyListModel(TaiSanTdXuLySearchModel searchModel);
        TaiSanTdXuLyListModel PrepareTaiSanTdXuLyListModelForPhuongAn(TaiSanTdXuLySearchModel searchModel);


        TaiSanTdXuLyModel PrepareTaiSanTdXuLyModel(TaiSanTdXuLyModel model, TaiSanTdXuLy item, bool excludeProperties = false, string PAXLJson = null, string HTXLJson = null);
        
        void PrepareTaiSanTdXuLy(TaiSanTdXuLyModel model, TaiSanTdXuLy item);
        void PrepareTaiSanTdXuLyForUpdate(TaiSanTdXuLyModel model, TaiSanTdXuLy item);
        TaiSanTdXuLyModel PrepareTaiSanTdXuLyModelForKetQua(TaiSanTdXuLyModel model, TaiSanTdXuLy item, bool excludeProperties = false);
        //add to hungnt to append list TSXL
        TaiSanTdXuLyListModel PrepareTaiSanTdXuLyListModelByListQuyetDinh(TaiSanTdXuLySearchModel searchModel);
        #endregion
    }
}
