using System.Threading.Tasks;

namespace Feedback.Helpers.CacheHelper
{
    public  interface ICacheHelpers
    {
        Task SetCache(string cacheName, string cacheValue);
        Task<string> GetCache(string cacheName);
        Task RemoveCache(string cacheName);
    }
}
