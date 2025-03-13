//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 21/7/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using GS.Web.Models.BienDongs;
using GS.Web.Models.TaiSans;
namespace GS.Web.Factories.TaiSans
{
    public partial interface IMuaSamModelFactory 
    {    
        #region MuaSam
    
        MuaSamSearchModel PrepareMuaSamSearchModel(MuaSamSearchModel searchModel);
        
        MuaSamListModel PrepareMuaSamListModel(MuaSamSearchModel searchModel);
        
        MuaSamModel PrepareMuaSamModel(MuaSamModel model, MuaSam item, bool excludeProperties = false);
        
        void PrepareMuaSam(MuaSamModel model, MuaSam item);
        ResultAction DuyetMuaSamFunc(decimal MuaSamId, MuaSam muaSam = null);
        ResultAction KhongDuyetMuaSamFunc(decimal MuaSamId, string Note);

        #endregion
    }
}
