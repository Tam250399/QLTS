//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/10/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GS.Core;
using GS.Core.Domain.BaoCaoDoiChieus;
using System.Collections;

namespace GS.Services.BaoCaoDoiChieus
{
    public partial interface IBaoCaoDoiChieuService
    {
        #region BaoCaoDoiChieu
        IList<BaoCaoDoiChieu> GetAllBaoCaoDoiChieus();
        IPagedList<BaoCaoDoiChieu> SearchBaoCaoDoiChieus(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? donviId = 0, decimal namBaoCao = 0, decimal donVi = 0);
        BaoCaoDoiChieu GetBaoCaoDoiChieuById(decimal Id);
        IList<BaoCaoDoiChieu> GetBaoCaoDoiChieuByIds(decimal[] newsIds);
        void InsertBaoCaoDoiChieu(BaoCaoDoiChieu entity);
        void UpdateBaoCaoDoiChieu(BaoCaoDoiChieu entity);
        void DeleteBaoCaoDoiChieu(BaoCaoDoiChieu entity);
        BaoCaoDoiChieu GetBaoCaoDoiChieuBySeachModel(Decimal? donViId = 0, Decimal? baoCaoId = 0, Decimal? heThongId = 0, Decimal? nam = 0);
        BaoCaoDoiChieu GetBaoCaoDoiChieu1ABySeachModel(Decimal? donViId = 0, Decimal? heThongId = 0, Decimal? nam = 0);
        void DeleteBaoCaoDoiChieu(List<BaoCaoDoiChieu> entity);
        List<BaoCaoDoiChieu> GetBaoCaoDoiChieu(decimal? donViId = null, decimal? namBaoCao = null, decimal? baoCaoId = null, decimal? heThongId = null);
        #endregion
    }
}
