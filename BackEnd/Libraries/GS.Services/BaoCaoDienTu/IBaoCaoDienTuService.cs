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
using GS.Core.Domain.BaoCaoDienTu;
using System.Collections;

namespace GS.Services.BaoCaoDienTus
{
    public partial interface IBaoCaoDienTuService
    {
        #region BaoCaoDienTu
        IList<BaoCaoDienTu> GetAllBaoCaoDienTu();
        IPagedList<BaoCaoDienTu> SearchBaoCaoDienTu(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? donviId = 0, decimal namBaoCao = 0, decimal? donVi = 0,DateTime? NGAY_BAO_CAO = null, decimal TRANG_THAI_ID = 0);
        IPagedList<BaoCaoDienTu> SearchBaoCaoChoDuyet(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? donviId = 0, decimal namBaoCao = 0, decimal? donVi = 0,DateTime? NGAY_BAO_CAO = null, decimal TRANG_THAI_ID = 0);
        BaoCaoDienTu GetBaoCaoDienTuById(decimal Id);
        IList<BaoCaoDienTu> GetBaoCaoDienTuByIds(decimal[] newsIds);
        void InsertBaoCaoDienTu(BaoCaoDienTu entity);
        void UpdateBaoCaoDienTu(BaoCaoDienTu entity);
        void DeleteBaoCaoDienTu(BaoCaoDienTu entity);
        BaoCaoDienTu GetBaoCaoDienTuBySeachModel(Decimal? donViId = 0, Decimal? baoCaoId = 0, Decimal? heThongId = 0, Decimal? nam = 0, string TenBaoCao = null);
        void DeleteBaoCaoDienTu(List<BaoCaoDienTu> entity);
        List<BaoCaoDienTu> GetBaoCaoDienTu(decimal? donViId = null, decimal? namBaoCao = null, decimal? baoCaoId = null, decimal? heThongId = null);
        #endregion
    }
}
