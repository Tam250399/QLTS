//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/1/2020
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
    public partial interface INhomCongCuService
    {
        #region NhomCongCu
        IList<NhomCongCu> GetAllNhomCongCus(int DonViId = 0);
        IPagedList<NhomCongCu> SearchNhomCongCus(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? ParentId = 0, decimal donViId = 0);
        NhomCongCu GetNhomCongCuById(decimal Id);
        IList<NhomCongCu> GetNhomCongCuByIds(decimal[] newsIds);
        IList<NhomCongCu> GetNhomCongCus(decimal? donViId = 0, List<decimal> list_exceptID = null, decimal? ParentId = 0);
        void InsertNhomCongCu(NhomCongCu entity);
        void UpdateNhomCongCu(NhomCongCu entity);
        void DeleteNhomCongCu(NhomCongCu entity);
        int CountNhomCongCuSub(decimal Id);
		string GetStringNhomCongCu(List<decimal> Ids);
		#endregion
		NhomCongCu GetNhomCongCuByMa(string ma);
        NhomCongCu GetNhomCongCu(decimal donViId, string ten = null, string ma = null);
        #region Read only
        NhomCongCu GetReadOnlyNhomCongCu(decimal? donViId, string ten = null, string ma = null);
        #endregion
    }
}
