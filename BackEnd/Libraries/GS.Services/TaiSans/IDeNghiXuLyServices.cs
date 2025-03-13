//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/11/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.TaiSans;

namespace GS.Services.TaiSans
{
    public partial interface IDeNghiXuLyService
    {
        #region DeNghiXuLy
        IList<DeNghiXuLy> GetAllDeNghiXuLys();
        IPagedList<DeNghiXuLy> SearchDeNghiXuLys(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string soPhieu = null, DateTime? ngayDeNghi = null, DateTime? ngayXuLy = null, Decimal? donViId = null);
        DeNghiXuLy GetDeNghiXuLyById(decimal Id);
        IList<DeNghiXuLy> GetDeNghiXuLyByIds(decimal[] newsIds);
        void InsertDeNghiXuLy(DeNghiXuLy entity);
        void UpdateDeNghiXuLy(DeNghiXuLy entity);
        void DeleteDeNghiXuLy(DeNghiXuLy entity);
        bool CheckTrungSoPhieuTrongNgay(Decimal ID,String soPhieu, DateTime ngay, Decimal donViId);
        #endregion
    }
}
