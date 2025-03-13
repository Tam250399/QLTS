//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 25/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.DanhMuc;

namespace GS.Services.DanhMuc
{
    public partial interface IDonViChuyenDoiService 
    {    
    #region DonViChuyenDoi
        IList<DonViChuyenDoi> GetAllDonViChuyenDois();
        IPagedList <DonViChuyenDoi> SearchDonViChuyenDois(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null, int forDonVi = 0);
        DonViChuyenDoi GetDonViChuyenDoiById(decimal Id);
        DonViChuyenDoi GetDonViChuyenDoiFromDonViId(int id);
        IList<DonViChuyenDoi> GetDonViChuyenDoiByIds(decimal[] newsIds);
        void InsertDonViChuyenDoi(DonViChuyenDoi entity);
        void UpdateDonViChuyenDoi(DonViChuyenDoi entity);
        void DeleteDonViChuyenDoi(DonViChuyenDoi entity);
        DonViChuyenDoi GetDonViChuyenDoiByDonViId(decimal? donViId, DateTime? ngayBC, decimal namBC);
     #endregion
    }
}
