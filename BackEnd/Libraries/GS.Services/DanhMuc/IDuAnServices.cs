//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.DanhMuc;
using System;
using System.Collections.Generic;

namespace GS.Services.DanhMuc
{
    public partial interface IDuAnService
    {
        #region DuAn
        IList<DuAn> GetAllDuAns();
        IList<DuAn> GetAllDuAnsChuaDb();
        IPagedList<DuAn> SearchDuAns(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal? donViId = 0);
        DuAn GetDuAnById(decimal Id);
        IList<DuAn> GetDuAnByIds(decimal[] newsIds);
        void InsertDuAn(DuAn entity);
        void UpdateDuAn(DuAn entity);
        void InsertDuAn(List<DuAn> entities);
        void UpdateDuAn(List<DuAn> entities);
        void DeleteDuAn(DuAn entity);

        bool CheckMaDuAn(decimal id = 0, string ma = null, decimal? donViID = null);
        IList<DuAn> GetDuAnByDonViId(decimal donViId);
        DuAn GetDuAnByMA(string ma);
        #endregion
    }
}
