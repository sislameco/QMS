using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Utilities.Redis
{
    public interface ICacheService
    {
        T GetString<T>(string key);
        T SetString<T>(string key, T value, int hourToCache = 3);
        T GetCachedData<T>(string cacheKey, Func<T> getDataFunc, int hourToCache = 12);
        Task<T> GetCachedDataAsync<T>(string cacheKey, Func<Task<T>> getDataFunc, int hourToCache = 12);
        void Remove(string key);
    }
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T GetString<T>(string key)
        {
            var value = _cache.GetString(key);
            if (value != null)
                return JsonConvert.DeserializeObject<T>(value);
            return default;
        }

        public T SetString<T>(string key, T value, int hourToCache = 3)
        {
            _cache.Remove(key);
            _cache.SetString(key, JsonConvert.SerializeObject(value), new() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(hourToCache) });
            return value;
        }

        public void Remove(string key) => _cache.Remove(key);

        // Asynchronous method to retrieve data from the cache or fetch it if not found in the cache.
        // T represents the type of data to be cached and retrieved.
        public async Task<T> GetCachedDataAsync<T>(string cacheKey, Func<Task<T>> getDataFunc, int hourToCache = 12)
        {
            // Attempt to get the cached data using the provided cache key.
            // GetString<T> retrieves the cached data if available; otherwise, it returns null.
            var cachedData = GetString<T>(cacheKey);

            // If cached data is found, return it without needing to fetch it again.
            if (cachedData != null)
            {
                return cachedData;
            }

            // If data is not found in the cache, asynchronously invoke the provided function to fetch the data.
            // The getDataFunc is expected to return a Task, so we await its result.
            var data = await getDataFunc();

            // Cache the newly fetched data for the specified duration (in hours).
            SetString(cacheKey, data, hourToCache);

            // Return the newly fetched and cached data.
            return data;
        }


        // Generic method to retrieve data from the cache or fetch it if not found in the cache.
        // T represents the type of data to be cached and retrieved.
        T ICacheService.GetCachedData<T>(string cacheKey, Func<T> getDataFunc, int hourToCache)
        {
            // Attempt to get the cached data using the provided cache key.
            // GetString<T> retrieves the cached data if available; otherwise, it returns null.
            var cachedData = GetString<T>(cacheKey);

            // If cached data is found, return it without needing to fetch it again.
            if (cachedData != null)
            {
                return cachedData;
            }

            // If data is not found in the cache, invoke the provided function to fetch the data.
            var data = getDataFunc();

            // Cache the newly fetched data for the specified duration (in hours).
            SetString(cacheKey, data, hourToCache);

            // Return the newly fetched and cached data.
            return data;
        }
    }
}
