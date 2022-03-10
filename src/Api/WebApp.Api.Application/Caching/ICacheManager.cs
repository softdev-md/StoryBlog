using System;
using System.Threading.Tasks;

namespace WebApp.Api.Application.Caching
{
    /// <summary>
    /// Cache manager interface
    /// </summary>
    public interface ICacheManager : IDisposable
    {
        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <param name="cacheTime">Cache time in minutes; pass 0 to do not cache; pass null to use the default time</param>
        /// <returns>The cached value associated with the specified key</returns>
        Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, int? cacheTime = null);

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <param name="cacheTime">Cache time in minutes; pass 0 to do not cache; pass null to use the default time</param>
        /// <returns>The cached value associated with the specified key</returns>
        Task<T> GetAsync<T>(string key, Func<T> acquire, int? cacheTime = null);

        /// <summary>
        /// Adds the specified key and object to the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <param name="data">Value for caching</param>
        /// <param name="cacheTime">Cache time in minutes</param>
        Task SetAsync(string key, object data, int cacheTime);

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>True if item already is in cache; otherwise false</returns>
        Task<bool> IsSetAsync(string key);

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        Task RemoveAsync(string key);

        /// <summary>
        /// Removes items by key pattern
        /// </summary>
        /// <param name="pattern">String key pattern</param>
        Task RemoveByPatternAsync(string pattern);

        /// <summary>
        /// Clear all cache data
        /// </summary>
        Task ClearAsync();
    }
}
