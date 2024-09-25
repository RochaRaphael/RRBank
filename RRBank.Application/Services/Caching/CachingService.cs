using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace RRBank.Application.Services.Caching
{
    public class CachingService : ICachingService
    {
        private readonly IDistributedCache cache;
        private readonly DistributedCacheEntryOptions options;
        public CachingService(IDistributedCache cache)
        {
            this.cache = cache;
            this.options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                SlidingExpiration = TimeSpan.FromSeconds(1200)
            };
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var jsonString = await cache.GetStringAsync(key);
            return string.IsNullOrEmpty(jsonString) ? default : JsonSerializer.Deserialize<T>(jsonString);
        }

        // Método para armazenar um valor no cache após serializá-lo
        public async Task SetAsync<T>(string key, T value)
        {
            var jsonString = JsonSerializer.Serialize(value);
            await cache.SetStringAsync(key, jsonString, options);
        }

        public async Task SetListAsync<T>(string key, List<T> list)
        {
            var jsonString = JsonSerializer.Serialize(list);
            await cache.SetStringAsync(key, jsonString, options);
        }

        public async Task<List<T>> GetListAsync<T>(string key)
        {
            var jsonString = await cache.GetStringAsync(key);
            return string.IsNullOrEmpty(jsonString) ? new List<T>() : JsonSerializer.Deserialize<List<T>>(jsonString);
        }
    }
}
