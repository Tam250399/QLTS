//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using System.Collections.Generic;

namespace GS.Services.DanhMuc
{
    public partial interface IDonViBoPhanService
    {
        #region DonViBoPhan
        IList<DonViBoPhan> GetDonViBoPhans(decimal? DonViId = 0,decimal TreeLevel = 0);
        IPagedList<DonViBoPhan> SearchDonViBoPhans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? DonViID = 0, decimal? ParentId = 0);
        DonViBoPhan GetDonViBoPhanById(decimal Id);
        IList<DonViBoPhan> GetDonViBoPhanByIds(decimal[] newsIds);
        void InsertDonViBoPhan(DonViBoPhan entity,bool isGenCode = true);
        void UpdateDonViBoPhan(DonViBoPhan entity);
        void DeleteDonViBoPhan(DonViBoPhan entity);
        bool CheckTenDonViBoPhan(decimal? id = 0, string ten = null);
        int CountPhongBanSub(decimal Id);
        void GenTreeNode(DonViBoPhan entity);
        void GenMaDonViBoPhan(DonViBoPhan entity);
        IList<DonViBoPhan> GetListDonViBoPhanTreeNodeByRoot(decimal? donViId = 0);
        DonViBoPhan GetDonViBoPhanByMa(string MA);
        DonViBoPhan GetDonViBoPhanByTen(string Ten);
		string GetStringTenDonViBoPhan(List<decimal> Ids);
        #endregion
        #region Cache
        IList<DonViBoPhan> GetTable();
        DonViBoPhan GetCacheDonViBoPhanByMa(string MA);
        DonViBoPhan GetCacheDonViBoPhanByTen(string Ten);
        #endregion
        bool CheckMaDonViBoPhan(decimal? donViId, decimal? id = 0, string ma = null);
        #region read only
        DonViBoPhan GetReadOnlyDonViBoPhanByMa(string MA);
        DonViBoPhan GetReadOnlyDonViBoPhanByMaAndDonViId(decimal? donViId = null, string MA = null);
        #endregion
    }
}
