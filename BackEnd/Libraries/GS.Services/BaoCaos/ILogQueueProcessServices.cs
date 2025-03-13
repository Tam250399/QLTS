//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 28/6/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.BaoCaos;

namespace GS.Services.BaoCaos
{
    public partial interface ILogQueueProcessService 
    {    
    #region LogQueueProcess
        IList<LogQueueProcess> GetAllLogQueueProcesss();
        IPagedList <LogQueueProcess> SearchLogQueueProcesss(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        LogQueueProcess GetLogQueueProcessById(decimal Id);
        IList<LogQueueProcess> GetLogQueueProcessByIds(decimal[] newsIds);
        void InsertLogQueueProcess(LogQueueProcess entity);
        void UpdateLogQueueProcess(LogQueueProcess entity);
        void DeleteLogQueueProcess(LogQueueProcess entity);    
     #endregion
	}
}
