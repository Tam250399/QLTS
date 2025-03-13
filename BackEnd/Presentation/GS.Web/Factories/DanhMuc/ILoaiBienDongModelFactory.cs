//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;
namespace GS.Web.Factories.DanhMuc
{
    public partial interface ILoaiBienDongModelFactory
    {
        #region LoaiBienDong

        LoaiBienDongSearchModel PrepareLoaiBienDongSearchModel(LoaiBienDongSearchModel searchModel);

        LoaiBienDongListModel PrepareLoaiBienDongListModel(LoaiBienDongSearchModel searchModel);

        LoaiBienDongModel PrepareLoaiBienDongModel(LoaiBienDongModel model, LoaiBienDong item, bool excludeProperties = false);

        void PrepareLoaiBienDong(LoaiBienDongModel model, LoaiBienDong item);

        #endregion
    }
}
