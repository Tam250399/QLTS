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
    public partial interface IDiaBanService
    {
        #region DiaBan
        IList<DiaBan> GetDiaBans(decimal? CapDiaban = 0, decimal? ParentId = 0, decimal? QuocGiaId = 0);
        IList<DiaBan> GetDiaBansChuaDb();
        IPagedList<DiaBan> SearchDiaBans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? ParentId = 0);
        DiaBan GetDiaBanById(decimal Id);
        IList<DiaBan> GetDiaBanByIds(decimal[] newsIds);
        void InsertDiaBan(DiaBan entity);
        void UpdateDiaBan(DiaBan entity);
        void InsertDiaBan(List<DiaBan> entities);
        void UpdateDiaBan(List<DiaBan> entities);
        void DeleteDiaBan(DiaBan entity);
        int GetCountDiaBanSub(decimal ParentId);
        DiaBan GetDiaBanByMa(string Ma);
        IList<DiaBan> GetDiaBansForInputSearch(string tenDiaBan = null);

        #endregion
        #region Cache
        IList<DiaBan> GetTable();
        DiaBan GetCacheDiaBanByMa(string Ma);
        #endregion
    }
}
