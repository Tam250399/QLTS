//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.TaiSans;
using System;
using System.Collections.Generic;

namespace GS.Services.TaiSans
{
    public partial interface ITaiSanNguonVonService
    {
        #region TaiSanNguonVon
        IList<TaiSanNguonVon> GetTaiSanNguonVons(decimal? taisanId = 0, decimal? BienDongID = 0);
        IPagedList<TaiSanNguonVon> SearchTaiSanNguonVons(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        TaiSanNguonVon GetTaiSanNguonVonById(decimal Id);
        IList<TaiSanNguonVon> GetTaiSanNguonVonByIds(decimal[] newsIds);
        void InsertTaiSanNguonVon(TaiSanNguonVon entity);
        void InsertTaiSanNguonVons(List<TaiSanNguonVon> entities);
        void UpdateTaiSanNguonVon(TaiSanNguonVon entity);
        void DeleteTaiSanNguonVon(TaiSanNguonVon entity);
        void DeleteTaiSanNguonVons(IList<TaiSanNguonVon> entities);
        /// <summary>
        /// chỉ dùng để tạo mới tài sản
        /// </summary>
        /// <param name="TaiSanId"></param>
        /// <param name="NguonVonId"></param>
        /// <returns></returns>
        TaiSanNguonVon GetTaiSanNguonVonByTaiSanIdFirst(decimal TaiSanId, decimal NguonVonId);
        List<TaiSanNguonVon> GetTaiSanNguonVonByBienDongId(decimal IdBienDong);
        void DeleteTaiSanNguonVonByTaiSanId(decimal TaiSanId);
        IList<TaiSanNguonVon> GetAllSumNguonVonNumberOfTaiSan(decimal TaiSanId, DateTime? NgayBienDongDen = null);
        #endregion
    }
}
