//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.BienDongs;
using GS.Web.Models.BienDongs;
namespace GS.Web.Factories.BienDongs
{
    public partial interface IBienDongChiTietModelFactory
    {
        #region BienDongChiTiet

        BienDongChiTietSearchModel PrepareBienDongChiTietSearchModel(BienDongChiTietSearchModel searchModel);

        BienDongChiTietListModel PrepareBienDongChiTietListModel(BienDongChiTietSearchModel searchModel);

        BienDongChiTietModel PrepareBienDongChiTietModel(BienDongChiTietModel model, BienDongChiTiet item, bool excludeProperties = false);

        void PrepareBienDongChiTiet(BienDongChiTietModel model, BienDongChiTiet item);
        void PrepareBienDongChiTietFromBDCT_TDTT(BienDongChiTiet item, BienDongChiTiet bienDongCT_TDTT = null);
        #endregion
    }
}
