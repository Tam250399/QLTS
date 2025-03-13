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

namespace GS.Services.CCDC
{
    public partial interface IXuatNhapService 
    {    
    #region XuatNhap
        IList<XuatNhap> GetAllXuatNhaps();
        IPagedList <XuatNhap> SearchXuatNhaps(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null, bool isPhanBo = false, int LoaiCongCu = 0, DateTime? NgayNhapTu = null, DateTime? NgayNhapDen = null, string SoChungTu = null);
        IPagedList<NhapXuatCongCu> SearchXuatNhapPhanBos(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, bool isPhanBo = false, Decimal LoaiCongCu = 0, Decimal DonViBoPhanId = 0, DateTime? TuNgay = null, DateTime? DenNgay = null);
        XuatNhap GetXuatNhapById(decimal Id);
        IList<XuatNhap> GetXuatNhapByIds(decimal[] newsIds);
        Decimal? GetValueIdMax();
        XuatNhap GetNhapKhoByCongCuId(Decimal CongCuId);
        XuatNhap GetXuatNhapByGuid(Guid GUID);
        String GetMaLienQuan(decimal? CongCuId);
        Int32 GetCountCongCuByXuatNhapId(decimal? XuatNhapId = 0);
        IList<XuatNhap> GetXuatNhaps(bool isXuat, Decimal fromId = 0, Decimal BoPhanId = 0, bool isKho = false, String maLienQuan = null, Decimal LoaiXuatNhap = 0, Boolean IsPhanBoFirst = false, decimal FromXuatNhap = 0);
        IPagedList<XuatNhap> SearchLuanChuyen(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal LoaiCongCu = 0, DateTime? TuNgay = null, DateTime? DenNgay = null);
        IPagedList<XuatNhap> SearchDieuChuyen(Decimal LoaiDieuChuyen, int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        XuatNhap GetXuatNhap(String Ma = null, decimal? From_xuat_nhap_id = null);
        void InsertXuatNhap(XuatNhap entity);
        IList<XuatNhap> GetXuatNhapsByMaLienQuan(String maLienQuan = null);
        void UpdateXuatNhap(XuatNhap entity);
        void DeleteXuatNhap(XuatNhap entity);
        void DeleteXuatNhaps(IList<XuatNhap> entitys);
        XuatNhap GetXuatNhapForDieuChuyen(decimal FromXuatNhap = 0, bool isXuat = false, decimal LoaiXuatNhap = 0);
     #endregion
    }
}
