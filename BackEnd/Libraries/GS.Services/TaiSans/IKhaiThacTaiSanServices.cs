//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/7/2020
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
    public partial interface IKhaiThacTaiSanService
    {
        #region KhaiThacTaiSan
        IList<KhaiThacTaiSan> GetAllKhaiThacTaiSans(int HinhThucKhaiThacId=0);

       decimal? TinhSoTienKhaiThacLuyKe(decimal? KhaiThacId);
        IPagedList<KhaiThacTaiSan> SearchKhaiThacTaiSans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal? KhaiThacId = 0, Decimal? KhaiThacChiTietId = 0);
        IList<KhaiThacTaiSan> GetKhaiThacTaiByKhaiThacChiTietID(decimal khaiThacChiTietId = 0);
        KhaiThacTaiSan GetKhaiThacTaiSanById(decimal Id);
        KhaiThacTaiSan GetKhaiThacTaiSanByMaTaiSanDBAndKhaiThacID(string ma_tai_san_DB, decimal khaiThacId = 0);
        KhaiThacTaiSan GetKhaiThacTaiSanByMaTaiSanAndKhaiThacID(string ma_tai_san, decimal khaiThacId = 0);
        IList<KhaiThacTaiSan> GetKhaiThacTaiSanByIds(decimal[] newsIds);
        IList<KhaiThacTaiSan> GetKhaiThacTaiSanKhacByIds(decimal[] newsIds);
        IList<KhaiThacTaiSan> GetMapByKhaiThacId(decimal khaiThacId);
        KhaiThacTaiSan GetMapByKhaiThacIdAbTaiSanId(decimal khaiThacId, decimal taiSanId);
        IList<KhaiThacTaiSan> GetKhaiThacTaiSanBytKhaiThacIds(decimal[] Ids);
        void InsertKhaiThacTaiSan(KhaiThacTaiSan entity);
        void UpdateKhaiThacTaiSan(KhaiThacTaiSan entity);
        void DeleteKhaiThacTaiSan(KhaiThacTaiSan entity);
        void DeleteKhaiThacTaiSans(IEnumerable<KhaiThacTaiSan> entities);
        void DeleteKhaiThacTaiSanId(decimal KhaiThacId, decimal TaiSanId);
        bool checkTrungKhaiThacTaiSan(decimal KhaiThacId, decimal TaiSanId);
     #endregion
    }
}
