//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 12/12/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DB;
using GS.Web.Models.DB;
using System;

namespace GS.Web.Factories.DB
{
    public partial interface IDB_QueueProcessModelFactory 
    {    
        #region DB_QueueProcess
    
        DB_QueueProcessSearchModel PrepareDB_QueueProcessSearchModel(DB_QueueProcessSearchModel searchModel);
        
        DB_QueueProcessListModel PrepareDB_QueueProcessListModel(DB_QueueProcessSearchModel searchModel);
        
        DB_QueueProcessModel PrepareDB_QueueProcessModel(DB_QueueProcessModel model, DB_QueueProcess item, bool excludeProperties = false);
        
        void PrepareDB_QueueProcess(DB_QueueProcessModel model, DB_QueueProcess item);

		void InsertActionToQueue(string action, object obj, decimal DonViId, int level = (int)enumLevelQueueProcessDB.MEDIUM);
        decimal InsertActionToQueueReturnId(string action, object obj, decimal DonViId, int level = (int)enumLevelQueueProcessDB.MEDIUM, Decimal? DoiTuongId = null);
        #endregion
    }
}
