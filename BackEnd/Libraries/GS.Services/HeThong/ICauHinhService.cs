using GS.Core;
using GS.Core.Configuration;
using GS.Core.Domain.HeThong;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GS.Services.HeThong
{
    /// <summary>
    /// CauHinh service interface
    /// </summary>
    public partial interface ICauHinhService
    {
        IPagedList<CauHinh> SearchCauHinhs(int pageIndex = 0, int pageSize = int.MaxValue, string Ten = null, string GiaTri = null);
        void InsertCauHinh(CauHinh item, bool clearCache = true);
        void UpdateCauHinh(CauHinh item, bool clearCache = true);
        /// <summary>
        /// Gets a item by identifier
        /// </summary>
        /// <param name="itemId">CauHinh identifier</param>
        /// <returns>CauHinh</returns>
        CauHinh GetCauHinhById(decimal itemId);

        /// <summary>
        /// Deletes a item
        /// </summary>
        /// <param name="item">CauHinh</param>
        void DeleteCauHinh(CauHinh item);

        /// <summary>
        /// Deletes items
        /// </summary>
        /// <param name="items">CauHinhs</param>
        void DeleteCauHinhs(IList<CauHinh> items);

        /// <summary>
        /// Get item by key
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="storeId">Store identifier</param>
        /// <param name="loadSharedValueIfNotFound">A value indicating whether a shared (for all stores) value should be loaded if a value specific for a certain is not found</param>
        /// <returns>CauHinh</returns>
        CauHinh GetCauHinh(string key, decimal DON_VI_ID = 0, bool loadSharedValueIfNotFound = false);

        /// <summary>
        /// Get item value by key
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="storeId">Store identifier</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="loadSharedValueIfNotFound">A value indicating whether a shared (for all stores) value should be loaded if a value specific for a certain is not found</param>
        /// <returns>CauHinh value</returns>
        T GetCauHinhByKey<T>(string key, T defaultValue = default(T), decimal DON_VI_ID = 0,
            bool loadSharedValueIfNotFound = false);

        /// <summary>
        /// Set item value
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="storeId">Store identifier</param>
        /// <param name="clearCache">A value indicating whether to clear cache after item update</param>
        void SetCauHinh<T>(string key, T value, decimal DON_VI_ID = 0, bool clearCache = true);

        /// <summary>
        /// Gets all items
        /// </summary>
        /// <returns>CauHinhs</returns>
        IList<CauHinh> GetAllCauHinhs();

        /// <summary>
        /// Determines whether a item exists
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="items">CauHinhs</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="storeId">Store identifier</param>
        /// <returns>true -item exists; false - does not exist</returns>
        bool CauHinhExists<T, TPropType>(T items,
            Expression<Func<T, TPropType>> keySelector)
            where T : ICauHinh, new();

        /// <summary>
        /// Load items
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="storeId">Store identifier for which items should be loaded</param>
        T LoadCauHinh<T>(decimal DON_VI_ID = 0) where T : ICauHinh, new();
        /// <summary>
        /// Load cấu hình đơn vị cấp 1 bằng id đơn vị con
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="DON_VI_ID">Đơn vị con</param>
        /// <returns></returns>
        T LoadCauHinhDonViBo<T>(decimal DON_VI_ID = 0) where T : ICauHinh, new();
        /// <summary>
        /// Load items
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="storeId">Store identifier for which items should be loaded</param>
        ICauHinh LoadCauHinh(Type type, decimal DON_VI_ID = 0);

        /// <summary>
        /// Save items object
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="storeId">Store identifier</param>
        /// <param name="items">CauHinh instance</param>
        void SaveCauHinh<T>(T items,decimal DON_VI_ID = 0) where T : ICauHinh, new();
		/// <summary>
		/// Save cấu hình đơn vị cấp 1 bằng id đơn vị con
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">CauHinh instance</param>
		/// <param name="DON_VI_ID">Đơn vị con</param>
		void SaveCauHinhDonViBo<T>(T items,decimal DON_VI_ID = 0) where T : ICauHinh, new();

        /// <summary>
        /// Save items object
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="items">CauHinhs</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="storeId">Store ID</param>
        /// <param name="clearCache">A value indicating whether to clear cache after item update</param>
        void SaveCauHinh<T, TPropType>(T items,
            Expression<Func<T, TPropType>> keySelector, decimal DON_VI_ID = 0, bool clearCache = true) where T : ICauHinh, new();


        /// <summary>
        /// Delete all items
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        void DeleteCauHinh<T>() where T : ICauHinh, new();

        /// <summary>
        /// Delete items object
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="items">CauHinhs</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="storeId">Store ID</param>
        void DeleteCauHinh<T, TPropType>(T items,
            Expression<Func<T, TPropType>> keySelector, decimal DON_VI_ID = 0) where T : ICauHinh, new();

        /// <summary>
        /// Clear cache
        /// </summary>
        void ClearCache();

        /// <summary>
        /// Get item key (stored into database)
        /// </summary>
        /// <typeparam name="TCauHinhs">Type of items</typeparam>
        /// <typeparam name="T">Property type</typeparam>
        /// <param name="items">CauHinhs</param>
        /// <param name="keySelector">Key selector</param>
        /// <returns>Key</returns>
        string GetCauHinhKey<TCauHinhs, T>(TCauHinhs items, Expression<Func<TCauHinhs, T>> keySelector)
            where TCauHinhs : ICauHinh, new();
    }
}