using System;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Helpers.CacheHelper
{
    public class CacheHelpers : ICacheHelpers
    {
        private const int ExpiryTime = 10;
        private readonly IDistributedCache _cache;
        public CacheHelpers(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task<string> GetCache(string cacheName)
        {
            var storedValue = await _cache.GetAsync(cacheName);
            return storedValue != null ? Encoding.UTF8.GetString(storedValue) : string.Empty;
        }

        public async Task SetCache(string cacheName, string cacheValue)
        {
            var storeValue = Encoding.UTF8.GetBytes(cacheValue);
            var options = new DistributedCacheEntryOptions()
               .SetSlidingExpiration(TimeSpan.FromMinutes(ExpiryTime));
            await _cache.SetAsync(cacheName, storeValue, options);
        }

        public async  Task RemoveCache(string cacheName)
        {
            await _cache.RemoveAsync(cacheName);
        }

    }
}
