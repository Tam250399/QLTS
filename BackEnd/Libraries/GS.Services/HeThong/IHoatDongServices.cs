using GS.Core;
using GS.Core.Domain.HeThong;
using System;
using System.Collections.Generic;

namespace GS.Services.HeThong
{

    public partial interface IHoatDongService
    {

        IPagedList<HoatDong> SearchActivities(int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null, string TEN_DANG_NHAP = null, string TEN_DOI_TUONG = null, int KIEU_HOAT_DONG = 0, DateTime? FromDate = null, DateTime? ToDate = null);
        /// <summary>
        /// Inserts an activity log type item
        /// </summary>
        /// <param name="activityLogType">Activity log type item</param>
        void InsertKieuHoatDong(LoaiHoatDong activityLogType);

        /// <summary>
        /// Updates an activity log type item
        /// </summary>
        /// <param name="activityLogType">Activity log type item</param>
        void UpdateKieuHoatDong(LoaiHoatDong activityLogType);

        /// <summary>
        /// Deletes an activity log type item
        /// </summary>
        /// <param name="activityLogType">Activity log type</param>
        void DeleteKieuHoatDong(LoaiHoatDong activityLogType);

        /// <summary>
        /// Gets all activity log type items
        /// </summary>
        /// <returns>Activity log type items</returns>
        IList<LoaiHoatDong> GetAllLoaiHoatDong();

        /// <summary>
        /// Gets an activity log type item
        /// </summary>
        /// <param name="activityLogTypeId">Activity log type identifier</param>
        /// <returns>Activity log type item</returns>
        LoaiHoatDong GetLoaiHoatDongById(decimal activityLogTypeId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">TU_KHOA_HE_THONG</param>
        /// <returns></returns>
        LoaiHoatDong GetLoaiHoatDongByCode(string code);
        /// <summary>
        /// Inserts an activity log item
        /// </summary>
        /// <param name="systemKeyword">System keyword</param>
        /// <param name="comment">Comment</param>
        /// <param name="entity">Entity</param>
        /// <returns>Activity log item</returns>
        HoatDong InsertHoatDong(string systemKeyword, string comment, object entity = null, string DoiTuong = null);

        /// <summary>
        /// Inserts an activity log item
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="systemKeyword">System keyword</param>
        /// <param name="comment">Comment</param>
        /// <param name="entity">Entity</param>
        /// <returns>Activity log item</returns>
        HoatDong InsertHoatDong(NguoiDung customer, string systemKeyword, string comment, decimal entity = 0, string DoiTuong = null, object obj = null);
        /// <summary>
        /// Update an logs cho 1 số trường hợp đặc biệt
        /// </summary>
        /// <param name="activityLog"></param>
        void UpdateHoatDong(HoatDong activityLog);
        /// <summary>
        /// Deletes an activity log item
        /// </summary>
        /// <param name="activityLog">Activity log</param>
        void DeleteHoatDong(HoatDong activityLog);

        /// <summary>
        /// Gets all activity log items
        /// </summary>
        /// <param name="createdOnFrom">Log item creation from; pass null to load all records</param>
        /// <param name="createdOnTo">Log item creation to; pass null to load all records</param>
        /// <param name="customerId">Customer identifier; pass null to load all records</param>
        /// <param name="activityLogTypeId">Activity log type identifier; pass null to load all records</param>
        /// <param name="ipAddress">IP address; pass null or empty to load all records</param>
        /// <param name="entityName">Entity name; pass null to load all records</param>
        /// <param name="entityId">Entity identifier; pass null to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Activity log items</returns>
        IPagedList<HoatDong> GetAllHoatDong(DateTime? createdOnFrom = null, DateTime? createdOnTo = null,
            int? customerId = null, int? activityLogTypeId = null, string ipAddress = null, string entityName = null, int? entityId = null,
            int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keywordSystem"></param>
        /// <param name="name">tên loại hoạt động</param>
        /// <returns></returns>
        IPagedList<LoaiHoatDong> SearchLoaiHoatDongs(int pageIndex = 0, int pageSize = int.MaxValue, string keywordSystem = null, string name = null);

        /// <summary>
        /// Gets an activity log item
        /// </summary>
        /// <param name="activityLogId">Activity log identifier</param>
        /// <returns>Activity log item</returns>
        HoatDong GetHoatDongById(decimal activityLogId);

        /// <summary>
        /// Clears activity log
        /// </summary>
        void ClearAllHoatDong();

    }
}
