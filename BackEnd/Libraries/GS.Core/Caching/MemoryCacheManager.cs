using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace GS.Core.Caching
{
    /// <summary>
    /// Represents a memory cache manager 
    /// </summary>
    public partial class MemoryCacheManager : ILocker, IStaticCacheManager
    {
        #region Fields

        private readonly IMemoryCache _cache;

        /// <summary>
        /// All keys of cache
        /// </summary>
        /// <remarks>Dictionary value indicating whether a key still exists in cache</remarks> 
        protected static readonly ConcurrentDictionary<string, bool> _allKeys;

        /// <summary>
        /// Cancellation token for clear cache
        /// </summary>
        protected CancellationTokenSource _cancellationTokenSource;

        #endregion

        #region Ctor

        static MemoryCacheManager()
        {
            _allKeys = new ConcurrentDictionary<string, bool>();
        }

        public MemoryCacheManager(IMemoryCache cache)
        {
            _cache = cache;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Create entry options to item of memory cache
        /// </summary>
        /// <param name="cacheTime">Cache time</param>
        protected MemoryCacheEntryOptions GetMemoryCacheEntryOptions(TimeSpan cacheTime)
        {
            var options = new MemoryCacheEntryOptions()
                // add cancellation token for clear cache
                .AddExpirationToken(new CancellationChangeToken(_cancellationTokenSource.Token))
                //add post eviction callback
                .RegisterPostEvictionCallback(PostEviction);

            //set cache time
            options.AbsoluteExpirationRelativeToNow = cacheTime;

            return options;
        }

        /// <summary>
        /// Add key to dictionary
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>Itself key</returns>
        protected string AddKey(string key)
        {
            _allKeys.TryAdd(key, true);
            return key;
        }

        /// <summary>
        /// Remove key from dictionary
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>Itself key</returns>
        protected string RemoveKey(string key)
        {
            TryRemoveKey(key);
            return key;
        }

        /// <summary>
        /// Try to remove a key from dictionary, or mark a key as not existing in cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        protected void TryRemoveKey(string key)
        {
            //try to remove key from dictionary
            if (!_allKeys.TryRemove(key, out _))
                //if not possible to remove key from dictionary, then try to mark key as not existing in cache
                _allKeys.TryUpdate(key, false, true);
        }

        /// <summary>
        /// Remove all keys marked as not existing
        /// </summary>
        private void ClearKeys()
        {
            foreach (var key in _allKeys.Where(p => !p.Value).Select(p => p.Key).ToList())
            {
                RemoveKey(key);
            }
        }

        /// <summary>
        /// Post eviction
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <param name="value">Value of cached item</param>
        /// <param name="reason">Eviction reason</param>
        /// <param name="state">State</param>
        private void PostEviction(object key, object value, EvictionReason reason, object state)
        {
            //if cached item just change, then nothing doing
            if (reason == EvictionReason.Replaced)
                return;

            //try to remove all keys marked as not existing
            ClearKeys();

            //try to remove this key from dictionary
            TryRemoveKey(key.ToString());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <param name="cacheTime">Cache time in minutes; pass 0 to do not cache; pass null to use the default time</param>
        /// <returns>The cached value associated with the specified key</returns>
        public virtual T Get<T>(string key, Func<T> acquire, int? cacheTime = null)
        {
            //item already is in cache, so return it
            if (_cache.TryGetValue(key, out T value))
                return value;

            //or create it using passed function
            var result = acquire();

            //and set in cache (if cache time is defined)
            if ((cacheTime ?? GSCachingDefaults.CacheTime) > 0)
                Set(key, result, cacheTime ?? GSCachingDefaults.CacheTime);

            return result;
        }

        /// <summary>
        /// Adds the specified key and object to the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <param name="data">Value for caching</param>
        /// <param name="cacheTime">Cache time in minutes</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data != null)
            {
                _cache.Set(AddKey(key), data, GetMemoryCacheEntryOptions(TimeSpan.FromMinutes(cacheTime)));
            }
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>True if item already is in cache; otherwise false</returns>
        public virtual bool IsSet(string key)
        {
            return _cache.TryGetValue(key, out object _);
        }

        /// <summary>
        /// Perform some action with exclusive in-memory lock
        /// </summary>
        /// <param name="key">The key we are locking on</param>
        /// <param name="expirationTime">The time after which the lock will automatically be expired</param>
        /// <param name="action">Action to be performed with locking</param>
        /// <returns>True if lock was acquired and action was performed; otherwise false</returns>
        public bool PerformActionWithLock(string key, TimeSpan expirationTime, Action action)
        {
            //ensure that lock is acquired
            if (!_allKeys.TryAdd(key, true))
                return false;

            try
            {
                _cache.Set(key, key, GetMemoryCacheEntryOptions(expirationTime));

                //perform action
                action();

                return true;
            }
            finally
            {
                //release lock even if action fails
                Remove(key);
            }
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        public virtual void Remove(string key)
        {
            _cache.Remove(RemoveKey(key));
        }

        /// <summary>
        /// Removes items by key pattern
        /// </summary>
        /// <param name="pattern">String key pattern</param>
        public virtual void RemoveByPattern(string pattern)
        {
            //get cache keys that matches pattern
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matchesKeys = _allKeys.Where(p => p.Value).Select(p => p.Key).Where(key => regex.IsMatch(key)).ToList();

            //remove matching values
            foreach (var key in matchesKeys)
            {
                _cache.Remove(RemoveKey(key));
            }
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear()
        {
            //send cancellation request
            _cancellationTokenSource.Cancel();

            //releases all resources used by this cancellation token
            _cancellationTokenSource.Dispose();

            //recreate cancellation token
            _cancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// Dispose cache manager
        /// </summary>
        public virtual void Dispose()
        {
            //nothing special
        }

        #endregion
    }
}