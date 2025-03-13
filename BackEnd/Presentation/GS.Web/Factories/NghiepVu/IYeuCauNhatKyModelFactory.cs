//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.NghiepVu;
using GS.Web.Models.BienDongs;
using GS.Web.Models.NghiepVu;
namespace GS.Web.Factories.NghiepVu
{
    public partial interface IYeuCauNhatKyModelFactory
    {
        #region YeuCauNhatKy

        YeuCauNhatKySearchModel PrepareYeuCauNhatKySearchModel(YeuCauNhatKySearchModel searchModel);

        YeuCauNhatKyListModel PrepareYeuCauNhatKyListModel(YeuCauNhatKySearchModel searchModel);

        YeuCauNhatKyModel PrepareYeuCauNhatKyModel(YeuCauNhatKyModel model, YeuCauNhatKy item, bool excludeProperties = false);

        YeuCauNhatKy PrepareYeuCauNhatKy(YeuCauModel yeucau, YeuCauNhatKy item);
        YeuCauNhatKy PrepareYeuCauNhatKy(BienDongModel bienDongModel, YeuCauNhatKy item);
        void InsertYeuCauNhatKy(YeuCauModel yeuCauModel, enumLOAI_NHATKY_YEUCAU LOAI_NHATKY_YEUCAU);
        void InsertYeuCauNhatKy(BienDongModel bienDongModel, enumLOAI_NHATKY_YEUCAU LOAI_NHATKY_YEUCAU);
        #endregion
    }
}
