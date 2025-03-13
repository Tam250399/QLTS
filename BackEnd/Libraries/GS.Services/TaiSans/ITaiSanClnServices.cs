//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.TaiSans;
using System.Collections.Generic;

namespace GS.Services.TaiSans
{
    public partial interface ITaiSanClnService
    {
        #region TaiSanCln
        IList<TaiSanCln> GetAllTaiSanClns();
        IPagedList<TaiSanCln> SearchTaiSanClns(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        TaiSanCln GetTaiSanClnById(decimal Id);
        IList<TaiSanCln> GetTaiSanClnByIds(decimal[] newsIds);
        TaiSanCln GetTaiSanClnByTaiSanId(decimal taiSanId);
        void InsertTaiSanCln(TaiSanCln entity);
        void UpdateTaiSanCln(TaiSanCln entity);
        void DeleteTaiSanCln(TaiSanCln entity);
        #endregion
    }
}
