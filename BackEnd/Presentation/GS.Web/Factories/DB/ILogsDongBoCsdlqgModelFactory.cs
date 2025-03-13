//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 22/3/2021
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DB;
using GS.Web.Models.DB;
namespace GS.Web.Factories.DB
{
    public partial interface ILogsDongBoCsdlqgModelFactory 
    {    
        #region LogsDongBoCsdlqg
    
        LogsDongBoCsdlqgSearchModel PrepareLogsDongBoCsdlqgSearchModel(LogsDongBoCsdlqgSearchModel searchModel);
        
        LogsDongBoCsdlqgListModel PrepareLogsDongBoCsdlqgListModel(LogsDongBoCsdlqgSearchModel searchModel);
        
        LogsDongBoCsdlqgModel PrepareLogsDongBoCsdlqgModel(LogsDongBoCsdlqgModel model, LogsDongBoCsdlqg item, bool excludeProperties = false);
        
        void PrepareLogsDongBoCsdlqg(LogsDongBoCsdlqgModel model, LogsDongBoCsdlqg item);
        
        #endregion        
	}
}
