//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
namespace GS.Web.Factories.TaiSans
{
    public partial interface ITaiSanNguonVonModelFactory
    {
        #region TaiSanNguonVon

        TaiSanNguonVonSearchModel PrepareTaiSanNguonVonSearchModel(TaiSanNguonVonSearchModel searchModel);

        TaiSanNguonVonListModel PrepareTaiSanNguonVonListModel(TaiSanNguonVonSearchModel searchModel);

        TaiSanNguonVonModel PrepareTaiSanNguonVonModel(TaiSanNguonVonModel model, TaiSanNguonVon item, bool excludeProperties = false);

        void PrepareTaiSanNguonVon(TaiSanNguonVonModel model, TaiSanNguonVon item);
        void InsertTaiSanNguonVonFromYeuCau(YeuCau yeuCau, BienDong bienDong);
        string GetNguonVonJsonFromListNguonVon(decimal idBienDong);
        #endregion
    }
}
