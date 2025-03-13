//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.SHTD;

namespace GS.Services.SHTD
{
    public partial interface IThuChiService 
    {
        #region ThuChi
        void UpdateSoTienThuChi(string ListThuChiId = null);
        ThuChi GetThuChiDauTien(string ListThuChiId = null);
        IList<ThuChi> GetAllThuChis();
        IList<ThuChi> GetAllThuChiChuaDongBo();
        IList<ThuChi> GetThuChis(decimal KetQuaId = 0, string ListThuChiId = null);
        IPagedList <ThuChi> SearchThuChis(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null, decimal KetQuaId = 0, string ListXuLyIdString = null);
        IPagedList<ThuChi> SearchThuChiKetQuas(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal KetQuaId = 0, string ListXuLyIdString = null);
        ThuChi GetThuChiById(decimal Id);
        IList<ThuChi> GetThuChiByIds(decimal[] newsIds);
        void InsertThuChi(ThuChi entity);
        void UpdateThuChi(ThuChi entity);
        void DeleteThuChi(ThuChi entity);
        ThuChi GetThuChiByDB_ID(string DB_Id);
     #endregion
    }
}
