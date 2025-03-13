//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 12/12/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.DB;

namespace GS.Services.DB
{
    public partial interface IDB_QueueProcessHistoryService 
    {    
    #region DB_QueueProcessHistory
        IList<DB_QueueProcessHistory> GetAllDB_QueueProcessHistorys();
        IPagedList <DB_QueueProcessHistory> SearchDB_QueueProcessHistorys(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        DB_QueueProcessHistory GetDB_QueueProcessHistoryById(decimal Id);
        IList<DB_QueueProcessHistory> GetDB_QueueProcessHistoryByIds(decimal[] newsIds);
        void InsertDB_QueueProcessHistory(DB_QueueProcessHistory entity);
        void UpdateDB_QueueProcessHistory(DB_QueueProcessHistory entity);
        void DeleteDB_QueueProcessHistory(DB_QueueProcessHistory entity);    
     #endregion
	}
}
