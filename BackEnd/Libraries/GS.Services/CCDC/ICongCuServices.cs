//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.CCDC;
using GS.Core.Domain.Common;

namespace GS.Services.CCDC
{
    public partial interface ICongCuService
    {
        #region CongCu
        IList<CongCu> GetAllCongCus(String ma_db = "", decimal? donViId = null);
        IList<CongCu> SearchCongCuDongBo(string keySearch = null);
        CongCu GetCongCuByGuid(Guid GUID);
        IPagedList<CongCu> SearchCongCus(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, bool isPhanBo = false, Decimal XuatNhapId = 0);
        CongCu GetCongCuById(decimal Id);
        CongCu GetCongCuByMaDB(string maDB);
        IList<CongCu> GetCongCuByIds(decimal[] newsIds);
        Decimal? GetValueIdMax();
        IList<CongCu> GetCongCus(Decimal? NhapXuatId = 0, Decimal? NhomCongCuId = 0, Decimal DonViId = 0);
        void InsertCongCu(CongCu entity);
        void UpdateCongCu(CongCu entity);
        void DeleteCongCu(CongCu entity);
        MessageReturn DelelteCongCuAndThongTinLienQuan(decimal CONG_CU_ID, decimal DEL_LO = 0);
        CongCu GetCongCu(string ma = null, string madb = null, decimal? donViId = null);
        decimal GetDonGiaCCDC(decimal id);
        MessageReturn insertOrupdateCCDCByJson(string json);
        MessageReturn insertOrupdateKiemKeByJson(string json);
        #endregion
    }
}
