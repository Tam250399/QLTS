//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using System;
using System.Collections.Generic;

namespace GS.Services.BienDongs
{
    public partial interface IBienDongService
    {
        #region BienDong
        IList<BienDong> GetAllBienDongs(decimal? donViId = null, decimal? donViChaId = null, decimal? trangThai = null, decimal? loaiBienDong = null, decimal? loaiHinhTaiSan = null,decimal? taiSanId=null, Decimal? nguonTaiSan = null);
        /// <summary>
        /// Description: Tìm kiếm danh sách biến động theo điều kiện <br />
        /// Return: List entity BienDong
        /// </summary>
        /// <returns>List entity BienDong</returns>
        IList<BienDong> SearchListBienDongs(string keySearch = null, DateTime? fromdate = null, DateTime? todate = null, decimal? taiSanId = 0,
                                        decimal? loaiHinhTSId = 0, decimal? loaiTSId = 0, string chungTuSo = null,
                                        decimal? nguoiTaoId = 0, decimal? boPhanId = 0, decimal? donViId = 0,
                                         decimal? loaiBienDongId = 0, decimal? lyDoBienDongId = 0);
        IPagedList<BienDong> SearchBienDongs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string MaDonVi = null
            , IList<int> ListLoaiHinhTaiSan = null, IList<int> ListLoaiBienDong = null, DateTime? fromdate = null, DateTime? todate = null, IList<int> ListTrangThaiDongBo = null, decimal? nguonTaiSanId = null);
        BienDong GetBienDongById(decimal Id);
        /// <summary>
        /// Description: Lay danh sach bien dong theo tai san id
        /// </summary>
        /// <param name="Id">TAI_SAN_ID</param>
        /// <returns></returns>
        IList<BienDong> GetBienDongsByTaiSanId(decimal? taiSanId = 0, DateTime? NgayBDDen = null);
        int CountBienDongsByTaiSanId(decimal? taiSanId = 0, DateTime? NgayBDDen = null);
        IList<BienDong> GetBienDongByIds(decimal[] newsIds);
        void InsertBienDong(BienDong entity, bool IsDongBo = false);
        void UpdateBienDong(BienDong entity, bool IsGhiLogHaoMon = true);
        void UpdateBienDong(IList<BienDong> entities);
        void UpdateNguoiTaoBienDongs(IList<BienDong> entities, decimal NguoiTaoId);
        void DeleteBienDong(BienDong entity = null, bool IsChotHaoMon = true);
        void DeleteBienDongs(IList<BienDong> entities);
        BienDong GetBienDongCuNhatByTaiSanId(decimal taiSanId);
        BienDong GetBienDongMoiNhatByTaiSanId(decimal? taiSanId = 0, DateTime? ngayBienDong = null, int? LoaiBienDong = null);
        /// <summary>
        /// Description: Tính giá trị nguyên giá cũ, tổng diện tích cũ
        /// </summary>
        /// <param name="TaiSanID"></param>
        /// <param name="To_Date_BienDong"></param>
        /// <param name="isIncludeYC"></param>
        /// <returns></returns>
        GiaTriTaiSan Tinh_GiaTriTaiSan(decimal TaiSanID, DateTime? To_Date_BienDong = null, bool isIncludeYC = true);
        BienDong GetBienDongCuoiByTaiSanId(decimal? taiSanId = 0);
        /// <summary>
        /// Tính tổng nguyên giá tài sản
        /// </summary>
        /// <param name="listBienDongTS"></param>
        /// <param name="listYeuCauTS"></param>
        /// <param name="taiSanId"></param>
        /// <param name="To_Date_BienDong">Ngày tính nguyên giá tài sản đến</param>
        /// <returns></returns>
        Decimal TinhNguyenGiaTaiSan(IList<BienDong> listBienDongTS = null, IList<YeuCau> listYeuCauTS = null, Decimal? taiSanId = 0, DateTime? To_Date_BienDong = null, bool isEqualDate = true);
        BienDong GetBienDongByGuid(Guid? Guid);
        void DB_InsertBienDong(BienDong entity);
        decimal TinhNguyenGiaTaiSanOnlyBD(Decimal taiSanId, DateTime? To_Date_BienDong = null, bool IncludeYeuCau = true);
        void DB_UpdateBienDong(BienDong entity);
        IList<BienDong> GetBienDongMoiNhatByNgayBienDong(DateTime? ngayBienDong = null, int DonViId = 0, int DonViBoPhanId = 0);
        decimal CountBDSau(decimal TaiSanId, DateTime NgayBD);
        BienDong GetBienDongByID_DB(string ID_DB);
        decimal CountBDByLyDoOfTS(decimal p_idTaiSan, enumLOAI_LY_DO_TANG_GIAM p_loaiBD);
        void GhiLogHaoMonTaiSan(BienDong bienDong);
        IList<BienDong> GetBienDongMoiNhatByNgayBienDongForKiemKe(DateTime? ngayBienDong = null, int DonViId = 0, int DonViBoPhanId = 0);
        void updateBienDongCuoi(decimal? tai_san_id);
        IList<BienDong> GetBienDongsByTaiSanIdForKK(decimal? taiSanId = 0, DateTime? NgayBDDen = null, DateTime? NgayBDTu = null);
        #endregion
        //decimal getGiaTriConLai(decimal taiSanID);

        #region Calculate Function
        GiaTriTaiSan ProcTinhGiaTriTaiSan(Decimal? taiSanId = 0, DateTime? ngayBienDong = null, Decimal? bienDongId = null);
        GiaTriNguonVon ProcTinhGiaTriNguonVon(Decimal? taiSanId = 0, DateTime? ngayBienDong = null);
        GiaTriNguyenGia ProcTinhNguyenGia(Decimal? taiSanId = 0, DateTime? ngayBienDong = null);
        #endregion Calculate Function
        DongBoApi_BienDongTaiSan GET_INFO_BIEN_DONG_BY_ID(decimal BienDongId);

        List<BienDong> GetAllBienDongTangMoiCanDongBo(decimal? donViChaId = null, int TakeNumber = 100, Decimal? nguonTaiSan = null);
        List<BienDong> GetAllBienDongKhacTangMoiCanDongBo(decimal? donViChaId = null, int TakeNumber = 100, decimal? nguonTaiSan = null, Decimal? loaiBienDongId = null);
        void UPDATE_TRANG_THAI_DONG_BO_BIEN_DONG(List<BienDong> bienDongs, decimal trangThai);
        IList<BienDong> GetAllBienDongsByTrangThaiDongBo(string maDonVi = null, decimal nguonTaiSan = 0, decimal? trangThaiDongBoId = null, decimal? loaiBienDongId = null);
        List<BienDong> GetAllBienDongTangMoi(decimal? donViChaId = null, int TakeNumber = 100, decimal? nguonTaiSan = null);
    }
}
