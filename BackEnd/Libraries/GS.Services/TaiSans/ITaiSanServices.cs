//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.TaiSans;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Services.TaiSans
{
    public partial interface ITaiSanService
    {
        #region TaiSan
        IList<TaiSan> GetAllTaiSans(int LoaiTaiSan = 0, int LoaiHinhTaiSan = 0, Decimal? DonViBoPhanId = 0, enumTRANG_THAI_TAI_SAN trangthai = enumTRANG_THAI_TAI_SAN.ALL, List<int> arrLoaiHinhTaiSan = null, Decimal? donViId = null, DateTime? ngayTaoTu = null, DateTime? ngayTaoDen = null);
        IList<TaiSan> SearchTaiSanDongBo(string keySearch);
        IPagedList<TaiSan> SearchTaiSans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal TRANG_THAI_ID = 0, decimal? LOAI_HINH_TAI_SAN_ID = 0, decimal? DON_VI_BO_PHAN_ID = 0, DateTime? Fromdate = null, DateTime? Todate = null, decimal? donviId = 0, bool isDuyet = false, string strLoaiHinhTSIds = null, decimal NguoiTaoId = 0, decimal Loai_Ly_Do_ID = 0, enumTS_NGUYEN_GIA NguyenGia = enumTS_NGUYEN_GIA.TAT_CA, IList<int> ListTaiSanDaChon = null, decimal idKhaiThac = 0, decimal? donvikhaithacid = null, string tenTaiSan = null, string maTaiSan = null, decimal? LY_DO_BIEN_DONG_ID = null, DateTime? FromDateNgayTangMoi = null, DateTime? ToDateNgayTangMoi = null, bool isToanQuoc = false, bool IsExclueTSDKTS = false, bool isKhaiThac = false);
        IPagedList<TaiSan> DanhSachTaiSans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal TRANG_THAI_ID = 0, decimal? LOAI_HINH_TAI_SAN_ID = 0, decimal? DON_VI_BO_PHAN_ID = 0, DateTime? Fromdate = null, DateTime? Todate = null, decimal? donviId = 0, bool isDuyet = false, string[] strLoaiHinhTSIds = null, decimal NguoiTaoId = 0, decimal Loai_Ly_Do_ID = 0, enumTS_NGUYEN_GIA NguyenGia = enumTS_NGUYEN_GIA.TAT_CA, List<decimal> taiSanIdExclude = null, int? NguonTaiSanId = -1, bool isCheckDonVi = false, bool isToanQuoc = false, bool IsChonTaiSan = false, bool? IsDanhSachTaiSanDuAn = false, bool? IsFilter = false, bool? IsHaoMon = false);
        IPagedList<TaiSan> GetTaiSanOtoChuyenDung(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? donViId = null);
        IPagedList<DonVi> SearchAllDonViChuaNhapTaiSan(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string MaBo = null, decimal? ParentID = 0, decimal? CapDonViSearch = 0, IList<int> listCapDonVis = null, decimal? LoaiTaiSanId = 0, decimal? donViId = 0);
        IPagedList<DonVi> SearchAllDonViKiemTraTaiSan(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string MaBo = null, decimal? ParentID = 0, decimal? CapDonViSearch = 0, IList<int> listCapDonVis = null, decimal? LoaiTaiSanId = 0, decimal? donViId = 0, decimal? LoaiDonViSearch = 0, decimal? MucDichSuDungSearch = 0, decimal? DienTich_CompareSign = 0, decimal? DienTich_Value1 = 0, decimal? DienTich_Value2 = 0);
        TaiSan GetTaiSanById(decimal Id);
        TaiSan GetTaiSanByMaDBPmCu(string ma_db, string MaDonVi);
        IList<TaiSan> GetTaiSanByDonViId(decimal donViID);
        bool GetTaiSanByDonViBoPhanId(decimal donViBoPhanId);
        IList<TaiSan> GetTaiSanByDonViIdAndLoaiTaiSanId(decimal donViId, decimal loaiTaiSanId);
        bool GetTaiSanByTrangThaiId(decimal donViId);
        IList<TaiSan> GetTaiSanByNguoiTao(decimal nguoiTaoId, decimal donViId);
        IList<TaiSan> GetTaiSanByIds(decimal[] newsIds);
        IList<TaiSan> GetTaiSanByKhaiThacId(decimal id);
        TaiSan GetTaiSanByMaDKTS(string MaDKTS);
        void InsertTaiSan(TaiSan entity, bool isNguoiTaoNull = false);
        bool InsertTaiSanByProcedure(TaiSan entity, bool isNguoiTaoNull = false);
        void InsertTaiSan(List<TaiSan> entities, bool isNguoiTaoNull = false);
        void UpdateTaiSan(TaiSan entity);
        void UpdateTaiSan(List<TaiSan> entities);
        void DeleteTaiSan(TaiSan entity);
        TaiSan GetTaiSanByMa(string MaTS);
        TaiSan GetTaiSanByTen(string TenTS, decimal? donViId = 0);
        TaiSan GetTaiSanByGuId(Guid guid);
        IList<TaiSan> GetTaiSanByTrangThaiId(decimal TrangThaiId = 6, decimal donViId = 0);
        TaiSan GetTaiSanByMaDB(string Ma = null, Guid guid = new Guid(), string MaDonVi = null, decimal NguonTaiSanId = 0);
        bool DeleteTaiSansLogic(decimal? p_DonViId = null, string strLoaiHinhTaiSan = null, bool IsDeleteDVCon = true, string strMaTaiSan = null, decimal? nguon_tai_san_id = null);
        decimal Get_GTLC_Cua_TS(decimal p_idTaiSan, DateTime? p_NgayBienDong = null, bool p_isEqualDateTime = false);
        /// <summary>
        /// Kiểm tra tài sản có biến động giảm hay không
        /// </summary>
        /// <param name="guidTS"></param>
        /// <returns>true: không có biến động giảm; false: có biến động giảm</returns>
        bool IsNotHasBDGiamTaiSan(Guid guidTS);
        IPagedList<TaiSan> GetTaiSanCanhBao(decimal? DonViId, string MA, int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null);
        Task<IList<CauHinhCanhBaoTaiSan>> CountTaiSanCanhBao(decimal? DonViId);
        Task<bool> IsHasCanhBao(decimal? DonViId);

        List<TaiSan> GetTaiSanCanDongBo(decimal DonViId = 0, decimal NguonTaiSanId = 0);
        bool CheckTonTaiByMaDB(string ma, decimal NguonTaiSanId);
        #endregion
        #region Đồng bộ tài sản cũ
        /// <summary>
        /// lấy tất cả tài sản của đơn vị và đơn vị con
        /// </summary>
        /// <param name="DonViId"></param>
        /// <returns></returns>
        List<TaiSan> GetListTaiSanTheoDonVi(int DonViId);
        #endregion
        /// <summary>
        /// Xóa tài sản theo đơn vị
        /// </summary>
        /// <param name="DonViMa"></param>
        /// <param name="IsDeleteDVCon"></param>
        /// <param name="NgayTao"></param>
        /// <returns></returns>
        Boolean XoaTaiSanByDonViId(string DonViMa, bool IsDeleteDVCon, DateTime NgayTao);
        bool CheckTonTaiByMaQLDKTS40(string ma);
        List<TaiSan> GetTaiSanCanLayMa(string DonViMa = null, decimal NguonTaiSanId = 0);
        IPagedList<TaiSan> SearchAllTaiSanKiemTraTaiSan(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string MaBo = null, decimal? ParentID = 0, decimal? CapDonViSearch = 0, IList<int> listCapDonVis = null, decimal? LoaiTaiSanId = 0, decimal? donViId = 0, decimal? LoaiDonViSearch = 0, decimal? MucDichSuDungSearch = 0, decimal? DienTich_CompareSign = 0, decimal? DienTich_Value1 = 0, decimal? DienTich_Value2 = 0);
        #region Công cụ kiểm tra

        #endregion Công cụ kiểm tra

        #region Cảnh báo từ cấu hình chung của đơn vị
        Task<IList<CauHinhCanhBaoTaiSan>> CountTaiSanCanhBaoFromCauHinhDonVi(decimal? DonViId);
        #endregion

        #region serivice Cấu hình Thay đổi địa bàn tài sản
        Task<int> CountTaiSanCanThayDoiDiaBan(string strDonViId = null, int LoaiHinhTaiSanId = 0);

        Task<IPagedList<TaiSan>> GetTaiSanCanThayDoiDiaBan(string strDonViId = null, int LoaiHinhTaiSanId = 0, int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null);

        Task<CauHinhCanhBaoTaiSan> CauHinhTaiSanThayDoiDiaBan(decimal? DonViId);
        Task<IList<CauHinhCanhBaoTaiSan>> CountTaiSanCanhBaoChung(decimal? DonViId);

        #endregion

        #region service Cấu hình tài sản sai hiện trạng
        Task<IPagedList<TaiSan>> GetTaiSanSaiHienTrang(string strDonViId = null, int LoaiHinhTaiSanId = 0, int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null);
        #endregion

        #region Service cấu hình số chỗ ngồi ô tô 
        Task<IPagedList<TaiSan>> GetTaiSanOtoSaiSoChoNgoi(string strDonViId = null, int LoaiHinhTaiSanId = 0, int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null);
        #endregion

        #region Service cấu hình tài sản chưa tính hao mòn
        Task<IPagedList<TaiSan>> GetTaiSanChuaTinhHaoMon(string strDonViId = null, int LoaiHinhTaiSanId = 0, int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null);
        #endregion


        #region serivice Cấu hình tài sản nguyên giá dưới 10tr
        Task<IPagedList<TaiSan>> GetTaiSanDuoi10Trieu(string strDonViId = null, int LoaiHinhTaiSanId = 0, int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null);
        #endregion

        #region serivice Cấu hình tài sản nhận điều chuyển
        Task<IPagedList<TaiSan>> GetTaiSanNhanDieuChuyen(decimal donViId = 0, int LoaiHinhTaiSanId = 0, int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null);
        #endregion

        #region Khai thác tài sản
        IPagedList<TaiSan> SearchTaiSanKhaiThac(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal TRANG_THAI_ID = 0, decimal? LOAI_HINH_TAI_SAN_ID = 0, DateTime? Fromdate = null, DateTime? Todate = null, decimal? idKhaiThac = 0, decimal? donvikhaithacid = null, string tenTaiSan = null, string maTaiSan = null);
        bool ChuyenNguonTaiSan(decimal? p_DonViId = null, string strLoaiHinhTaiSan = null, bool IsDVCon = true, string strMaTaiSan = null, decimal? nguon_tai_san_id_from = null, decimal? nguon_tai_san_id_to = null);
        #endregion

        #region chotHM
        bool ChotToanBoHaoMonTS(decimal idTS);
        IList<TaiSan> GetAllTaiSanByMaBo(decimal? donViId = null, string mabo = null);
        #endregion chotHM
    }
}
