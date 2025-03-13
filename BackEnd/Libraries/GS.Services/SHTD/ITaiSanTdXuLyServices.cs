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
    public partial interface ITaiSanTdXuLyService 
    {    
    #region TaiSanTdXuLy
        IList<TaiSanTdXuLy> GetAllTaiSanTdXuLys();
        IList<TaiSanTdXuLy> GetAllTaiSanTdXuLyChuaDongBo();
        IPagedList<TaiSanTdXuLy> SearchTaiSanTdXuLys(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, int XuLyId = 0, bool is_null = false, decimal DonViId = 0,bool isKetQua = false);
        IPagedList<TaiSanTdXuLy> SearchTaiSanTdXuLysForPhuongAn(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, int XuLyId = 0, bool is_null = false, decimal TrangThai = (decimal)enumTrangThaiXuLy.TonTai, decimal DonViId = 0);
        TaiSanTdXuLy GetTaiSanTdXuLyById(decimal Id);
        TaiSanTdXuLy GetTaiSanTdXuLyByDB_ID(string DB_Id);
        IList<TaiSanTdXuLy> GetTaiSanTdXuLyByIds(decimal[] newsIds);
        void InsertTaiSanTdXuLy(TaiSanTdXuLy entity);
        void UpdateTaiSanTdXuLy(TaiSanTdXuLy entity);
        void DeleteTaiSanTdXuLy(TaiSanTdXuLy entity);
        IList<TaiSanTdXuLy> GetTaiSanTdXuLyByTaiSanIdAndXuLyId(decimal TaiSanId = 0, decimal XuLyId = 0);
        IList<TaiSanTdXuLy> GetTaiSanTdsXuLyByXuLyId(decimal XuLyId = 0, decimal TrangThaiId = (int)enumTrangThaiXuLy.TonTai, decimal? soluong = null);
        TaiSanTdXuLy GetTaiSanTdXuLyByGuId(Guid Guid);
        IList<TaiSanTdXuLy> GetTaiSanTdXuLysByTaiSanId(decimal TaiSanId=0);
        IList<decimal> CheckTaiSanXuLy(decimal XuLyId);
        IList<TaiSanTdXuLy> GetTaiSanTdXuLys(int HinhThucXuLy = 0, int PhuongAnXuLy = 0, List<decimal> ListTSTDId = null, int LoaiXuLy = 0, int TaiSanId = 0, decimal quyetDinhId = 0);
        IList<decimal> CheckDaTaoTSXL(decimal XuLyId);
     #endregion
    }
}
