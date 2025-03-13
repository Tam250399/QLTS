//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 22/3/2021
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
    public partial interface ILogsDongBoCsdlqgService
    {
        #region LogsDongBoCsdlqg
        IList<LogsDongBoCsdlqg> GetAllLogsDongBoCsdlqgs(string uuid = null, string matsc = null);
        IPagedList<LogsDongBoCsdlqg> SearchLogsDongBoCsdlqgs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        LogsDongBoCsdlqg GetLogsDongBoCsdlqgById(decimal Id);
        IList<LogsDongBoCsdlqg> GetLogsDongBoCsdlqgByIds(decimal[] newsIds);
        void InsertLogsDongBoCsdlqg(LogsDongBoCsdlqg entity);
        void UpdateLogsDongBoCsdlqg(LogsDongBoCsdlqg entity);
        void DeleteLogsDongBoCsdlqg(LogsDongBoCsdlqg entity);
        #endregion
    }
}
