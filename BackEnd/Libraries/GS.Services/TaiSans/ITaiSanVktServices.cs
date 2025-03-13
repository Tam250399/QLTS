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
    public partial interface ITaiSanVktService
    {
        #region TaiSanVkt
        IList<TaiSanVkt> GetAllTaiSanVkts();
        IPagedList<TaiSanVkt> SearchTaiSanVkts(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        TaiSanVkt GetTaiSanVktById(decimal Id);
        IList<TaiSanVkt> GetTaiSanVktByIds(decimal[] newsIds);
        TaiSanVkt GetTaiSanVktByTaiSanId(decimal taiSanId);
        void InsertTaiSanVkt(TaiSanVkt entity);
        void UpdateTaiSanVkt(TaiSanVkt entity);
        void DeleteTaiSanVkt(TaiSanVkt entity);
        #endregion
    }
}
