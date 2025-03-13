//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.ThuocTinhs;

namespace GS.Services.ThuocTinhs
{
    public partial interface IThuocTinhDataService 
    {    
    #region ThuocTinhData
        IList<ThuocTinhData> GetAllThuocTinhDatas(int TaiSanId = 0, int ThuocTinhId = 0, List<Decimal?> ListTaiSanId = null, List<Decimal?> ListThuocTinhId = null, string Data = null);
        IPagedList <ThuocTinhData> SearchThuocTinhDatas(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        ThuocTinhData GetThuocTinhDataById(decimal Id);
        IList<ThuocTinhData> GetThuocTinhDataByIds(decimal[] newsIds);
        List<ThuocTinhData> GetThuocTinhDataByTaiSanId(int TaiSanId = 0, int TaiSanTdXuLyId = 0);
        void InsertThuocTinhData(ThuocTinhData entity);
        void UpdateThuocTinhData(ThuocTinhData entity);
        void DeleteThuocTinhData(ThuocTinhData entity);
        ThuocTinhData GetThuocTinhDataByTaiSanIdAndData(string Data = null, int TaiSanId = 0);
        ThuocTinhData GetThuocTinhDataByTaiSanXuLyTDIdAndData(string Data = null, int TaiSanXuLyTDId = 0);
     #endregion
    }
}
