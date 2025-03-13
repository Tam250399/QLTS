//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.NghiepVu;
using System;
using System.Collections.Generic;

namespace GS.Services.NghiepVu
{
    public partial interface IYeuCauService
    {
        #region YeuCau
        IList<YeuCau> GetYeuCaus(decimal? TaiSanId = 0, decimal? TrangThaiId = 0);
        IPagedList<YeuCau> SearchYeuCaus(int pageIndex = 0, int pageSize = int.MaxValue,
                                        string keySearch = null, DateTime? fromdate = null, DateTime? todate = null,
                                        decimal? loaiHinhTSId = 0, decimal? loaiTSId = 0, string chungTuSo = null,
                                        decimal? nguoiTaoId = 0, decimal? boPhanId = 0, decimal? donViId = 0,
                                         decimal? loaiBienDongId = 0, decimal? lyDoBienDongId = 0, decimal? trangThaiId = 0,
                                         decimal? taisanId = 0);
        YeuCau GetYeuCauById(decimal Id);
        YeuCau GetYeuCauByGuid(Guid guid);
        /// <summary>
        /// Description: Lấy yêu cầu cũ nhất của tài sản (yêu cầu tăng mới tài sản)
        /// </summary>
        /// <param name="id">tài sản id</param>
        /// <returns></returns>
        YeuCau GetYeuCauCuNhatByTSId(decimal id);
        IList<YeuCau> GetYeuCauByIds(decimal[] newsIds);
        void InsertYeuCau(YeuCau entity);
        void UpdateYeuCau(YeuCau entity);
        void UpdateNguoiTaoYeuCaus(IList<YeuCau> entities, decimal NguoiTaoId);
        void DeleteYeuCau(YeuCau entity);
        /// <summary>
        /// Description: Lay yeu cau bien dong moi nhat cua tai san
        /// </summary>
        /// <param name="TaiSanId"></param>
        /// <returns></returns>
        YeuCau GetYeuCauMoiNhatByTaiSanId(decimal TaiSanId, enumTRANG_THAI_YEU_CAU TrangThai = enumTRANG_THAI_YEU_CAU.TAT_CA, List<decimal> exceptTrangThais = null, DateTime? NgayBD = null);
        int CountYeuCauCuaTaiSan(decimal taisanId,  List<decimal> statusIds = null, List<decimal> excludeLoaiBD = null, DateTime? NgayBienDong = null);
        int CountYeuCauTrungNgayBD(decimal taisanId, DateTime? NgayBienDong, decimal excludeIDYC = 0);
        YeuCau GetYeuCauDieuChuyenKem(decimal TaiSanId);
        int CountYeuCau(decimal TaiSanId = 0, decimal? TrangThaiId = 0, decimal? exceptTrangThaiId = 0);
        bool IsYCNeedToEdit(Guid guid);
        YeuCau GetYCNeedToEdit(decimal p_taiSanId);
        string GetStringTuChoi(decimal p_taiSanId);
        int CountYeuCauLonHonNgayBD(decimal taisanId, DateTime? Ngaynhap, decimal excludeIDYC = 0);
        YeuCau GetYeuCauByDB_Guid(Guid guid);
        YeuCau GetYeuCau(decimal taiSanid, DateTime ngayBiendong, decimal loaiBienDongId);
        #endregion
    }
}
