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
    public partial interface INguonTaiSanService
    {
        #region NguonTaiSan
        IPagedList<NguonTaiSan> SearchNguonTaiSan(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        NguonTaiSan GetNguonTaiSanById(decimal Id);
        IList<NguonTaiSan> GetNguonTaiSanByIds(decimal[] newsIds);
        void InsertNguonTaiSan(NguonTaiSan entity);
        void UpdateNguonTaiSan(NguonTaiSan entity);
        void DeleteNguonTaiSan(NguonTaiSan entity);
        IList<NguonTaiSan> GetAllNguonTaiSan();
        IList<NguonTaiSan> GetAllNguonTaiSanActive();
        #endregion
    }
}
