//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.BienDongs;
using System;
using System.Collections.Generic;

namespace GS.Services.BienDongs
{
    public partial interface IBienDongChiTietService
    {
        #region BienDongChiTiet
        IList<BienDongChiTiet> GetAllBienDongChiTiets();
        IPagedList<BienDongChiTiet> SearchBienDongChiTiets(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        BienDongChiTiet GetBienDongChiTietById(decimal Id);
        /// <summary>
        /// Description: Lay bien dong chi tiet by bien dong Id
        /// </summary>
        /// <param name="bienDongId"></param>
        /// <returns></returns>
        BienDongChiTiet GetBienDongChiTietByBDId(decimal bienDongId);
        IList<BienDongChiTiet> GetBienDongChiTietByIds(decimal[] newsIds);
        void InsertBienDongChiTiet(BienDongChiTiet entity, bool isTinhKhauHao = true);
        void UpdateBienDongChiTiet(BienDongChiTiet entity, bool isTinhKhauHao = true);
        void DeleteBienDongChiTiet(BienDongChiTiet entity);
        IList<BienDongChiTiet> GetBienDongChiTietByBDIds(IList<decimal> bienDongIds);

        /// <summary>
        ///  Description: Chốt giá trị hao mòn khấu hao tài sản khi có biến động update, insert
        /// </summary>
        /// <param name="bienDongId">Id biến động thay đổi</param>
        void ChotHMKHTaiSan(decimal bienDongId, decimal taiSanId);
        /// <summary>
        /// Description: Chốt giá trị hao mòn, khấu hao
        /// </summary>
        /// <param name="taiSanId">Id tài sản</param>
        /// <param name="ngayBienDong">Ngày biến động của bản ghi bị hủy</param>
        void ChotHMKHTaiSanKhiHuyDuyet(decimal? taiSanId = 0, DateTime? ngayBienDong = null);

        BienDongChiTiet GetBienDongChiTietByBDIdForKiemKe(decimal bienDongId);
        void UpdateHienTrangBienDongChiTiet(BienDongChiTiet entity);
        void InsertToBienDongChiTiet(BienDongChiTiet entity);
        #endregion
    }
}
