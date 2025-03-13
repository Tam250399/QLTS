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
using GS.Core.Domain.SHTD;

namespace GS.Services.SHTD
{
    public partial interface ITaiSanTdService 
    {    
    #region TaiSanTd
        IList<TaiSanTd> GetAllTaiSanTds();
        IList<TaiSanTd> GetAllTaiSanTdsChuaDongBo();
        IList<TaiSanTd> GetTaiSanNhaNhapTrenDats(Decimal TaiSanDatId = 0, List<decimal> ListNhaId = null);
        IPagedList <TaiSanTd> SearchTaiSanTds(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, int QuyetDinhId = 0, int NguonGocTaiSan = 0, int LoaiTaiSan = 0, string TenTaiSan = null, DateTime? NgayQuyetDinhTu = null, DateTime? NgayQuyetDinhDen = null, string SoQuyetDinh = null, int TaiSanDatId = 0, int TrangThaiID = (int)enumTRANGTHAITSTD.TONTAI, List<decimal> ListNhaNhapId = null);
        TaiSanTd GetTaiSanTdById(decimal Id);
        IList<TaiSanTd> GetTaiSanTdByIds(decimal[] newsIds);
        void InsertTaiSanTd(TaiSanTd entity);
        void UpdateTaiSanTd(TaiSanTd entity);
        void DeleteTaiSanTd(TaiSanTd entity);
        TaiSanTd GetTaiSanTdByGuid(Guid guid);
        IList<TaiSanTd> GetTaiSanTdByListQuyetDinhId(List<int> ListQuyetDinhId);
        IList<TaiSanTd> GetTaiSanTdByQuyetDinhId(decimal QuyetDinhId=0);
        IList<TaiSanTd> GetTaiSanTds(List<int> ListTaiSanKhongChon = null, Decimal? LoaiHinhTaiSan = null,Decimal TaiSanDatId = 0, Decimal QuyetDinhId = 0);
        IList<TaiSanTd> GetTaiSansChuaFullSoLuongForKetQua(List<ListSoLuong> listSL = null, List<int> listTaiSanTd = null);
        IList<TaiSanTd> GetTaiSansChuaFullSoLuongForDeXuat(List<ListSoLuong> listSL = null, List<int> listTaiSanTd = null);
        decimal? GetSoLuongConByTaiSanId(decimal Id, decimal SoLuong = 0, int LoaiXuLy = 0, decimal xulyid = 0);
        IList<TaiSanTd> GetTaiSanTdByPhuongAn(Decimal QuyetDinhId = 0);
        List<decimal> GetQuyetDinhTichThuCon(Decimal DonViId, List<int> ListQuyetDinhId = null);
        TaiSanTd GetTaiSanTdByDB_ID(string DB_Id);
     #endregion
    }
}
