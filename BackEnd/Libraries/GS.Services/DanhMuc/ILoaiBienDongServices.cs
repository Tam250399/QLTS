//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.DanhMuc;
using System.Collections.Generic;

namespace GS.Services.DanhMuc
{
    public partial interface ILoaiBienDongService
    {
        #region LoaiBienDong
        IList<LoaiBienDong> GetAllLoaiBienDongs();
        IPagedList<LoaiBienDong> SearchLoaiBienDongs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        LoaiBienDong GetLoaiBienDongById(decimal Id);
        IList<LoaiBienDong> GetLoaiBienDongByIds(decimal[] newsIds);
        void InsertLoaiBienDong(LoaiBienDong entity);
        void UpdateLoaiBienDong(LoaiBienDong entity);
        void DeleteLoaiBienDong(LoaiBienDong entity);
        #endregion
    }
}
