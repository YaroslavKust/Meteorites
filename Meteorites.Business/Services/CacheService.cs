using Meteorites.Business.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace Meteorites.Business.Services
{
    public class CacheService(IMemoryCache cache) : ICacheService
    {
        public void Set<T>(string key, T value, TimeSpan? expirationTime = null)
        {
            cache.Set(
                key,
                value,
                expirationTime ?? TimeSpan.FromMinutes(CacheConstants.DefaultExpirationTimeMinutes)
            );
        }

        public T? Get<T>(string key)
        {
            bool hasValue = cache.TryGetValue<T>(key, out T? value);

            return hasValue ? value : default;
        }

        public void Delete(string key)
        {
            cache.Remove(key);
        }
    }
}
