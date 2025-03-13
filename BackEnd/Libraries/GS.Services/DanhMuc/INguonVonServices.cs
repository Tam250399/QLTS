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
    public partial interface INguonVonService
    {
        #region NguonVon
        IList<NguonVon> GetNguonVons(decimal? TaiSanId = 0);
        IPagedList<NguonVon> SearchNguonVons(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        NguonVon GetNguonVonById(decimal Id);
        IList<NguonVon> GetNguonVonByIds(decimal[] newsIds);
        void InsertNguonVon(NguonVon entity);
        void UpdateNguonVon(NguonVon entity);
        void DeleteNguonVon(NguonVon entity);
        IList<NguonVon> GetAllNguonVon();
        IList<NguonVon> GetAllNguonVonActive();
        void InsertListNguonVon(List<NguonVon> entities);
        void UpdateListNguonVon(List<NguonVon> entities);
        #endregion
    }
}
