//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.CCDC;
using GS.Web.Models.CCDC;
namespace GS.Web.Factories.CCDC
{
    public partial interface IKiemKeHoiDongModelFactory 
    {    
        #region KiemKeHoiDong
    
        KiemKeHoiDongSearchModel PrepareKiemKeHoiDongSearchModel(KiemKeHoiDongSearchModel searchModel);
        
        KiemKeHoiDongListModel PrepareKiemKeHoiDongListModel(KiemKeHoiDongSearchModel searchModel);
        
        KiemKeHoiDongModel PrepareKiemKeHoiDongModel(KiemKeHoiDongModel model, KiemKeHoiDong item, bool excludeProperties = false);

        KiemKeHoiDongListModel PrepareKiemKeHoiDongListModelForKiemKe(KiemKeHoiDongSearchModel searchModel);

        void PrepareKiemKeHoiDong(KiemKeHoiDongModel model, KiemKeHoiDong item);
        
        #endregion        
	}
}
