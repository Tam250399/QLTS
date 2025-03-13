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
    public partial interface IChucDanhService
    {
        #region ChucDanh
        IList<ChucDanh> GetAllChucDanhs();
        IPagedList<ChucDanh> SearchChucDanhs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? KhoiDonViIdSearch = 0);
        ChucDanh GetChucDanhById(decimal Id);
        IList<ChucDanh> GetChucDanhByIds(decimal[] newsIds);
        void InsertChucDanh(ChucDanh entity);
        void UpdateChucDanh(ChucDanh entity);
        void InsertChucDanh(List<ChucDanh> entities);
        void UpdateChucDanh(List<ChucDanh> entities);
        void DeleteChucDanh(ChucDanh entity);
        bool CheckMaChucDanh(decimal? id = 0, string ma = null);
        ChucDanh GetChucDanhByMa(string ma);
        #endregion
    }
}
