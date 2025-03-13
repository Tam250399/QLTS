//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 3/10/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DM;
using GS.Web.Models.DM;
namespace GS.Web.Factories.DM
{
    public partial interface ILoaiLyDoBienDongModelFactory 
    {    
        #region LoaiLyDoBienDong
    
        LoaiLyDoBienDongSearchModel PrepareLoaiLyDoBienDongSearchModel(LoaiLyDoBienDongSearchModel searchModel);
        
        LoaiLyDoBienDongListModel PrepareLoaiLyDoBienDongListModel(LoaiLyDoBienDongSearchModel searchModel);
        
        LoaiLyDoBienDongModel PrepareLoaiLyDoBienDongModel(LoaiLyDoBienDongModel model, LoaiLyDoBienDong item, bool excludeProperties = false);
        
        void PrepareLoaiLyDoBienDong(LoaiLyDoBienDongModel model, LoaiLyDoBienDong item);
        
        #endregion        
	}
}
