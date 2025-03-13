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
    public partial interface ITaiSanMayMocService
    {
        #region TaiSanMayMoc
        IList<TaiSanMayMoc> GetAllTaiSanMayMocs();
        IPagedList<TaiSanMayMoc> SearchTaiSanMayMocs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        TaiSanMayMoc GetTaiSanMayMocById(decimal Id);
        IList<TaiSanMayMoc> GetTaiSanMayMocByIds(decimal[] newsIds);
        TaiSanMayMoc GetTaiSanMaymocByTaiSanId(decimal taiSanId);
        void InsertTaiSanMayMoc(TaiSanMayMoc entity);
        void UpdateTaiSanMayMoc(TaiSanMayMoc entity);
        void DeleteTaiSanMayMoc(TaiSanMayMoc entity);
        #endregion
    }
}
