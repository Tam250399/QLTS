//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 12/12/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DB;
using GS.Web.Models.DB;
namespace GS.Web.Factories.DB
{
    public partial interface IDB_QueueProcessHistoryModelFactory 
    {    
        #region DB_QueueProcessHistory
    
        DB_QueueProcessHistorySearchModel PrepareDB_QueueProcessHistorySearchModel(DB_QueueProcessHistorySearchModel searchModel);
        
        DB_QueueProcessHistoryListModel PrepareDB_QueueProcessHistoryListModel(DB_QueueProcessHistorySearchModel searchModel);
        
        DB_QueueProcessHistoryModel PrepareDB_QueueProcessHistoryModel(DB_QueueProcessHistoryModel model, DB_QueueProcessHistory item, bool excludeProperties = false);
        
        void PrepareDB_QueueProcessHistory(DB_QueueProcessHistoryModel model, DB_QueueProcessHistory item);
        
        #endregion        
	}
}
