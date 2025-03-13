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
    public partial interface IDB_QueueProcessService
    {
        #region DB_QueueProcess
        IList<DB_QueueProcess> GetAllDB_QueueProcesss(Guid? guid = null, String guidResponse = null);
        IPagedList<DB_QueueProcess> SearchDB_QueueProcesss(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        DB_QueueProcess GetDB_QueueProcessById(decimal Id);
        DB_QueueProcess GetDB_QueueProcessByIdNeedToSendRequest();

        IList<DB_QueueProcess> GetDB_QueueProcessByIds(decimal[] newsIds);
        void InsertDB_QueueProcess(DB_QueueProcess entity);
        void UpdateDB_QueueProcess(DB_QueueProcess entity);
        void DeleteDB_QueueProcess(DB_QueueProcess entity);
        DB_QueueProcess GetDB_QueueProcessByDoiTuongID(string urlCall, decimal doiTuongId);
        DB_QueueProcess InsertActionToQueue(string action, object obj, decimal DonViId = 0, int level = 2, decimal? DoiTuongId = null);
        #endregion
    }
}
