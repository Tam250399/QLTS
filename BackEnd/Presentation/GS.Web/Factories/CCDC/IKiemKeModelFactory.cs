//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.CCDC;
using GS.Web.Models.CCDC;
namespace GS.Web.Factories.CCDC
{
    public partial interface IKiemKeModelFactory 
    {
        #region KiemKe

        KiemKeModel PrapareKiemKe(KiemKeModel model);

        KiemKeSearchModel PrepareKiemKeSearchModel(KiemKeSearchModel searchModel);
        
        KiemKeListModel PrepareKiemKeListModel(KiemKeSearchModel searchModel);
        
        KiemKeModel PrepareKiemKeModel(KiemKeModel model, KiemKe item, bool excludeProperties = false);
        
        void PrepareKiemKe(KiemKeModel model, KiemKe item);
        
        #endregion        
	}
}
