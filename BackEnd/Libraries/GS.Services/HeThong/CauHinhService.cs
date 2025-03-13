using GS.Core;
using GS.Core.Caching;
using GS.Core.Configuration;
using GS.Core.Data;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using GS.Services.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GS.Services.HeThong
{
    /// <summary>
    /// CauHinh manager
    /// </summary>
    public partial class CauHinhService : ICauHinhService
    {
        #region Fields

        private readonly IRepository<CauHinh> _itemRepository;
        private readonly IStaticCacheManager _cacheManager;
        private readonly IRepository<DonVi> _donViRepository;

        #endregion

        #region Ctor

        public CauHinhService(
            IRepository<CauHinh> itemRepository,
            IStaticCacheManager cacheManager,
			IRepository<DonVi> donViRepository)
        {
            this._itemRepository = itemRepository;
            this._cacheManager = cacheManager;
            this._donViRepository = donViRepository;
        }

        #endregion

        #region Nested classes

        /// <summary>
        /// CauHinh (for caching)
        /// </summary>
        [Serializable]
        public class CauHinhForCaching
        {
            public decimal ID { get; set; }

            public string TEN { get; set; }

            public string GIA_TRI { get; set; }
            public decimal DON_VI_ID { get; set; }
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Gets all items
        /// </summary>
        /// <returns>CauHinhs</returns>
        protected virtual IDictionary<string, IList<CauHinhForCaching>> GetAllCauHinhsCached()
        {
            //cache
            return _cacheManager.Get(GSCauHinhMacDinh.CauHinhsAllCacheKey, () =>
            {
                //we use no tracking here for performance optimization
                //anyway records are loaded only for read-only operations
                var query = from s in _itemRepository.TableNoTracking
                            orderby s.TEN
                            select s;
                var items = query.ToList();
                var dictionary = new Dictionary<string, IList<CauHinhForCaching>>();
                foreach (var s in items)
                {
                    var resourceName = s.TEN.ToLowerInvariant();
                    var itemForCaching = new CauHinhForCaching
                    {
                        ID = s.ID,
                        TEN = s.TEN,
                        GIA_TRI = s.GIA_TRI,
                        DON_VI_ID=s.DON_VI_ID
                    };
                    if (!dictionary.ContainsKey(resourceName))
                    {
                        //first item
                        dictionary.Add(resourceName, new List<CauHinhForCaching>
                        {
                            itemForCaching
                        });
                    }
                    else
                    {
                        //already added
                        //most probably it's the item with the same name but for some certain store (storeId > 0)
                        dictionary[resourceName].Add(itemForCaching);
                    }
                }

                return dictionary;
            });
        }

        /// <summary>
        /// Set item value
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="storeId">Store identifier</param>
        /// <param name="clearCache">A value indicating whether to clear cache after item update</param>
        protected virtual void SetCauHinh(Type type, string key, object value, decimal DON_VI_ID = 0, bool clearCache = true)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            key = key.Trim().ToLowerInvariant();
            var valueStr = TypeDescriptor.GetConverter(type).ConvertToInvariantString(value);

            var allCauHinhs = GetAllCauHinhsCached();
            var itemForCaching = allCauHinhs.ContainsKey(key) ?
                allCauHinhs[key].FirstOrDefault(x => x.DON_VI_ID == DON_VI_ID) : null;
            if (itemForCaching != null)
            {
                //update
                var item = GetCauHinhById(itemForCaching.ID);
                item.GIA_TRI = valueStr;
                UpdateCauHinh(item, clearCache);
            }
            else
            {
                //insert
                var item = new CauHinh
                {
                    TEN = key,
                    GIA_TRI = valueStr,
                    DON_VI_ID=DON_VI_ID
                };
                InsertCauHinh(item, clearCache);
            }
        }

        #endregion

        #region Methods
        public virtual IPagedList<CauHinh> SearchCauHinhs(int pageIndex = 0, int pageSize = int.MaxValue, string Ten = null, string GiaTri = null)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(Ten))
            {
                Ten = Ten.ToLower();
                query = query.Where(c => c.TEN.Contains(Ten));
            }
            if (!string.IsNullOrEmpty(GiaTri))
            {
                query = query.Where(c => c.GIA_TRI.Contains(GiaTri, StringComparison.OrdinalIgnoreCase));
            }
            return new PagedList<CauHinh>(query, pageIndex, pageSize); ;
        }
        /// <summary>
        /// Adds a item
        /// </summary>
        /// <param name="item">CauHinh</param>
        /// <param name="clearCache">A value indicating whether to clear cache after item update</param>
        public virtual void InsertCauHinh(CauHinh item, bool clearCache = true)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            item.TEN = item.TEN.ToLower();
            _itemRepository.Insert(item);

            //cache
            if (clearCache)
                _cacheManager.RemoveByPattern(GSCauHinhMacDinh.CauHinhsPatternCacheKey);


        }

        /// <summary>
        /// Updates a item
        /// </summary>
        /// <param name="item">CauHinh</param>
        /// <param name="clearCache">A value indicating whether to clear cache after item update</param>
        public virtual void UpdateCauHinh(CauHinh item, bool clearCache = true)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            item.TEN = item.TEN.ToLower();
            _itemRepository.Update(item);

            //cache
            if (clearCache)
                _cacheManager.RemoveByPattern(GSCauHinhMacDinh.CauHinhsPatternCacheKey);

        }

        /// <summary>
        /// Deletes a item
        /// </summary>
        /// <param name="item">CauHinh</param>
        public virtual void DeleteCauHinh(CauHinh item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _itemRepository.Delete(item);

            //cache
            _cacheManager.RemoveByPattern(GSCauHinhMacDinh.CauHinhsPatternCacheKey);


        }

        /// <summary>
        /// Deletes items
        /// </summary>
        /// <param name="items">CauHinhs</param>
        public virtual void DeleteCauHinhs(IList<CauHinh> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            _itemRepository.Delete(items);

            //cache
            _cacheManager.RemoveByPattern(GSCauHinhMacDinh.CauHinhsPatternCacheKey);


        }

        /// <summary>
        /// Gets a item by identifier
        /// </summary>
        /// <param name="itemId">CauHinh identifier</param>
        /// <returns>CauHinh</returns>
        public virtual CauHinh GetCauHinhById(Decimal itemId)
        {
            if (itemId == 0)
                return null;

            return _itemRepository.GetById(itemId);
        }

        /// <summary>
        /// Get item by key
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="storeId">Store identifier</param>
        /// <param name="loadSharedValueIfNotFound">A value indicating whether a shared (for all stores) value should be loaded if a value specific for a certain is not found</param>
        /// <returns>CauHinh</returns>
        public virtual CauHinh GetCauHinh(string key, decimal DON_VI_ID = 0, bool loadSharedValueIfNotFound = false)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            var items = GetAllCauHinhsCached();
            key = key.Trim().ToLowerInvariant();
            if (!items.ContainsKey(key))
                return null;

            var itemsByKey = items[key];
            var item = itemsByKey.FirstOrDefault(c=>c.DON_VI_ID==DON_VI_ID);

            //load shared value?
            if (item == null && DON_VI_ID>0 && loadSharedValueIfNotFound)
                item = itemsByKey.FirstOrDefault(c => c.DON_VI_ID ==0);

            return item != null ? GetCauHinhById(item.ID) : null;
        }

        /// <summary>
        /// Get item value by key
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="storeId">Store identifier</param>
        /// <param name="loadSharedValueIfNotFound">A value indicating whether a shared (for all stores) value should be loaded if a value specific for a certain is not found</param>
        /// <returns>CauHinh value</returns>
        public virtual T GetCauHinhByKey<T>(string key, T defaultValue = default(T),decimal DON_VI_ID = 0
            , bool loadSharedValueIfNotFound = false)
        {
            if (string.IsNullOrEmpty(key))
                return defaultValue;

            var items = GetAllCauHinhsCached();
            key = key.Trim().ToLowerInvariant();
            if (!items.ContainsKey(key))
                return defaultValue;

            var itemsByKey = items[key];
            var item = itemsByKey.FirstOrDefault(x => x.DON_VI_ID == DON_VI_ID);

            //load shared value?
            if (item == null && DON_VI_ID>0 && loadSharedValueIfNotFound)
                item = itemsByKey.FirstOrDefault(x => x.DON_VI_ID ==0);

            return item != null ? CommonHelper.To<T>(item.GIA_TRI) : defaultValue;
        }

        /// <summary>
        /// Set item value
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="storeId">Store identifier</param>
        /// <param name="clearCache">A value indicating whether to clear cache after item update</param>
        public virtual void SetCauHinh<T>(string key, T value, decimal DON_VI_ID = 0, bool clearCache = true)
        {
            SetCauHinh(typeof(T), key, value, DON_VI_ID, clearCache);
        }

        /// <summary>
        /// Gets all items
        /// </summary>
        /// <returns>CauHinhs</returns>
        public virtual IList<CauHinh> GetAllCauHinhs()
        {
            var query = from s in _itemRepository.Table
                        orderby s.TEN
                        select s;
            var items = query.ToList();
            return items;
        }

        /// <summary>
        /// Determines whether a item exists
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="items">Entity</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="storeId">Store identifier</param>
        /// <returns>true -item exists; false - does not exist</returns>
        public virtual bool CauHinhExists<T, TPropType>(T items,
            Expression<Func<T, TPropType>> keySelector)
            where T : ICauHinh, new()
        {
            var key = GetCauHinhKey(items, keySelector);

            var item = GetCauHinhByKey<string>(key);
            return item != null;
        }

        /// <summary>
        /// Load items
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="storeId">Store identifier for which items should be loaded</param>
        public virtual T LoadCauHinh<T>(decimal DON_VI_ID = 0) where T : ICauHinh, new()
        {
            return (T)LoadCauHinh(typeof(T), DON_VI_ID);
        }
        /// <summary>
        /// Load cấu hình đơn vị cấp 1 bằng id đơn vị con
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="DON_VI_ID">Đơn vị con</param>
        /// <returns></returns>
        public virtual T LoadCauHinhDonViBo<T>(decimal DON_VI_ID = 0) where T : ICauHinh, new()
        {
            DonVi donVi = _donViRepository.GetById(DON_VI_ID);
            if (donVi.PARENT_ID == null)
                DON_VI_ID = donVi.ID;
            else
            {
                string treeNodeParent = donVi.TREE_NODE.Split('-').FirstOrDefault();
                if(!String.IsNullOrEmpty(treeNodeParent))
                {
                    DON_VI_ID = treeNodeParent.Trim().TrimStart('0').ToNumber();
                }    
            }    
            return (T)LoadCauHinh(typeof(T), DON_VI_ID);
        }

        /// <summary>
        /// Load items
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="storeId">Store identifier for which items should be loaded</param>
        public virtual ICauHinh LoadCauHinh(Type type, decimal DON_VI_ID = 0)
       {
            var items = Activator.CreateInstance(type);

            foreach (var prop in type.GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var key = type.Name + "." + prop.Name;
                //load by store
                var item = GetCauHinhByKey<string>(key, DON_VI_ID: DON_VI_ID, loadSharedValueIfNotFound: true);
                if (item == null)
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).IsValid(item))
                    continue;

                var value = TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromInvariantString(item);

                //set property
                prop.SetValue(items, value, null);
            }

            return items as ICauHinh;
        }

        /// <summary>
        /// Save items object
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="storeId">Store identifier</param>
        /// <param name="items">CauHinh instance</param>
        public virtual void SaveCauHinh<T>(T items,decimal DON_VI_ID = 0) where T : ICauHinh, new()
        {
            /* We do not clear cache after each item update.
             * This behavior can increase performance because cached items will not be cleared 
             * and loaded from database after each update */
            foreach (var prop in typeof(T).GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                var key = typeof(T).Name + "." + prop.Name;
                var value = prop.GetValue(items, null);
                if (value != null)
                    SetCauHinh(prop.PropertyType, key, value, DON_VI_ID, false);
                else
                    SetCauHinh(key, string.Empty, DON_VI_ID, false);
            }

            //and now clear cache
            ClearCache();
        }
		public virtual void SaveCauHinhDonViBo<T>(T items, decimal DON_VI_ID = 0) where T : ICauHinh, new()
		{
			DonVi donVi = _donViRepository.GetById(DON_VI_ID);
			if (donVi.PARENT_ID == null)
				DON_VI_ID = donVi.ID;
			else
			{
				string treeNodeParent = donVi.TREE_NODE.Split('-').FirstOrDefault();
				if (!String.IsNullOrEmpty(treeNodeParent))
				{
					DON_VI_ID = treeNodeParent.Trim().TrimStart('0').ToNumber();
				}
			}

			/* We do not clear cache after each item update.
			* This behavior can increase performance because cached items will not be cleared 
			* and loaded from database after each update */
			foreach (var prop in typeof(T).GetProperties())
			{
				// get properties we can read and write to
				if (!prop.CanRead || !prop.CanWrite)
					continue;

				if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
					continue;

				var key = typeof(T).Name + "." + prop.Name;
				var value = prop.GetValue(items, null);
				if (value != null)
					SetCauHinh(prop.PropertyType, key, value, DON_VI_ID, false);
				else
					SetCauHinh(key, string.Empty, DON_VI_ID, false);
			}

			//and now clear cache
			ClearCache();
		}
		/// <summary>
		/// Save items object
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <typeparam name="TPropType">Property type</typeparam>
		/// <param name="items">CauHinhs</param>
		/// <param name="keySelector">Key selector</param>
		/// <param name="storeId">Store ID</param>
		/// <param name="clearCache">A value indicating whether to clear cache after item update</param>
		public virtual void SaveCauHinh<T, TPropType>(T items,
            Expression<Func<T, TPropType>> keySelector, decimal DON_VI_ID = 0, bool clearCache = true) where T : ICauHinh, new()
        {
            if (!(keySelector.Body is MemberExpression member))
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    keySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                       "Expression '{0}' refers to a field, not a property.",
                       keySelector));
            }

            var key = GetCauHinhKey(items, keySelector);
            var value = (TPropType)propInfo.GetValue(items, null);
            if (value != null)
                SetCauHinh(key, value, DON_VI_ID, clearCache);
            else
                SetCauHinh(key, string.Empty, DON_VI_ID, clearCache);
        }



        /// <summary>
        /// Delete all items
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public virtual void DeleteCauHinh<T>() where T : ICauHinh, new()
        {
            var itemsToDelete = new List<CauHinh>();
            var allCauHinhs = GetAllCauHinhs();
            foreach (var prop in typeof(T).GetProperties())
            {
                var key = typeof(T).Name + "." + prop.Name;
                itemsToDelete.AddRange(allCauHinhs.Where(x => x.TEN.Equals(key, StringComparison.InvariantCultureIgnoreCase)));
            }

            DeleteCauHinhs(itemsToDelete);
        }

        /// <summary>
        /// Delete items object
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="items">CauHinhs</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="storeId">Store ID</param>
        public virtual void DeleteCauHinh<T, TPropType>(T items,
            Expression<Func<T, TPropType>> keySelector, decimal DON_VI_ID = 0) where T : ICauHinh, new()
        {
            var key = GetCauHinhKey(items, keySelector);
            key = key.Trim().ToLowerInvariant();

            var allCauHinhs = GetAllCauHinhsCached();
            var itemForCaching = allCauHinhs.ContainsKey(key) ?
                allCauHinhs[key].FirstOrDefault(x=>x.DON_VI_ID== DON_VI_ID) : null;
            if (itemForCaching == null)
                return;

            //update
            var item = GetCauHinhById(itemForCaching.ID);
            DeleteCauHinh(item);
        }

        /// <summary>
        /// Clear cache
        /// </summary>
        public virtual void ClearCache()
        {
            _cacheManager.RemoveByPattern(GSCauHinhMacDinh.CauHinhsPatternCacheKey);
        }

        /// <summary>
        /// Get item key (stored into database)
        /// </summary>
        /// <typeparam name="TCauHinhs">Type of items</typeparam>
        /// <typeparam name="T">Property type</typeparam>
        /// <param name="items">CauHinhs</param>
        /// <param name="keySelector">Key selector</param>
        /// <returns>Key</returns>
        public virtual string GetCauHinhKey<TCauHinhs, T>(TCauHinhs items, Expression<Func<TCauHinhs, T>> keySelector)
            where TCauHinhs : ICauHinh, new()
        {
            if (!(keySelector.Body is MemberExpression member))
                throw new ArgumentException($"Expression '{keySelector}' refers to a method, not a property.");

            if (!(member.Member is PropertyInfo propInfo))
                throw new ArgumentException($"Expression '{keySelector}' refers to a field, not a property.");

            var key = $"{typeof(TCauHinhs).Name}.{propInfo.Name}";

            return key;
        }
        #endregion
    }
}