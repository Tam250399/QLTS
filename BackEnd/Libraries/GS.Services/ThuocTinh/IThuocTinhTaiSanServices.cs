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
    public partial interface IThuocTinhTaiSanService 
    {    
    #region ThuocTinhTaiSan
        IList<ThuocTinhTaiSan> GetAllThuocTinhTaiSans();
        IPagedList <ThuocTinhTaiSan> SearchThuocTinhTaiSans(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        ThuocTinhTaiSan GetThuocTinhTaiSanById(decimal Id);
        IList<ThuocTinhTaiSan> GetThuocTinhTaiSanByIds(decimal[] newsIds);
        void InsertThuocTinhTaiSan(ThuocTinhTaiSan entity);
        void UpdateThuocTinhTaiSan(ThuocTinhTaiSan entity);
        void DeleteThuocTinhTaiSan(ThuocTinhTaiSan entity);
        List<ThuocTinhTaiSan> GetThuocTinhTaiSan(int LoaiHinhTaiSan = 0, int LoaiTaiSan = 0, List<decimal?> ListTaiSanId = null, List<decimal?> ListHinhTaiSan = null);
        ThuocTinhTaiSan GetThuocTinhTaiSanByLoaiTaiSanIdAndLoaiHinhTaiSanAndThuocTinhId(int LoaiTaiSan = 0, int LoaiHinhTaiSan = 0, int ThuocTinhId = 0);
     #endregion
    }
}
