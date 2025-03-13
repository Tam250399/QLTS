//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
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
    public partial interface ITaiSanKiemKeTaiSanService 
    {    
    #region TaiSanKiemKeTaiSan
        IList<TaiSanKiemKeTaiSan> GetAllTaiSanKiemKeTaiSans();
        IList<TaiSanKiemKeTaiSan> GetTaiSanKiemKeTaiSans(Decimal KiemKeId = 0);
        IPagedList <TaiSanKiemKeTaiSan> SearchTaiSanKiemKeTaiSans(bool isTaiSanThua, int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null, Decimal KiemKeId = 0);
        TaiSanKiemKeTaiSan GetTaiSanKiemKeTaiSanById(decimal Id);
        IList<TaiSanKiemKeTaiSan> GetTaiSanKiemKeTaiSanByIds(decimal[] newsIds);
        void InsertTaiSanKiemKeTaiSan(TaiSanKiemKeTaiSan entity);
        void UpdateTaiSanKiemKeTaiSan(TaiSanKiemKeTaiSan entity);
        void DeleteTaiSanKiemKeTaiSan(TaiSanKiemKeTaiSan entity);
        void DeleteMultiTaiSanKiemKeTaiSan(decimal?[] taisanId);
     #endregion
    }
}
