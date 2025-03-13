//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.HeThong;
using GS.Web.Models.HeThong;

namespace GS.Web.Factories.HeThong
{
    public partial interface INhanHienThiModelFactory
    {
        #region NhanHienThi

        NhanHienThiSearchModel PrepareNhanHienThiSearchModel(NhanHienThiSearchModel searchModel);

        NhanHienThiListModel PrepareNhanHienThiListModel(NhanHienThiSearchModel searchModel);

        NhanHienThiModel PrepareNhanHienThiModel(NhanHienThiModel model, NhanHienThi item, bool excludeProperties = false);

        void PrepareNhanHienThi(NhanHienThiModel model, NhanHienThi item);
        bool CheckTrungMa(string Ma, decimal id = 0);

        #endregion
    }
}
