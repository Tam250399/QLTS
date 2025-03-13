//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
namespace GS.Web.Factories.TaiSans
{
    public partial interface ITaiSanKiemKeHoiDongModelFactory 
    {    
        #region TaiSanKiemKeHoiDong
    
        TaiSanKiemKeHoiDongSearchModel PrepareTaiSanKiemKeHoiDongSearchModel(TaiSanKiemKeHoiDongSearchModel searchModel);
        
        TaiSanKiemKeHoiDongListModel PrepareTaiSanKiemKeHoiDongListModel(TaiSanKiemKeHoiDongSearchModel searchModel);
        
        TaiSanKiemKeHoiDongModel PrepareTaiSanKiemKeHoiDongModel(TaiSanKiemKeHoiDongModel model, TaiSanKiemKeHoiDong item, bool excludeProperties = false);
        
        void PrepareTaiSanKiemKeHoiDong(TaiSanKiemKeHoiDongModel model, TaiSanKiemKeHoiDong item);
        
        #endregion        
	}
}
