using Microsoft.Extensions.Caching.Memory;
using System;

namespace Pokedex.Data.Caching
{
    public class CachingService : ICachingService
    {
        private readonly IMemoryCache _cache;

        public CachingService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool ContainsKey(string key)
        {
            return _cache.TryGetValue(key, out _);
        }

        public T Get<T>(string key)
        {
            if (_cache.TryGetValue(key, out T value))
                return value;
            else
                return default(T);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Set<T>(string key, T obj, int seconds)
        {
            _cache.Set(key, obj, TimeSpan.FromSeconds(seconds));
        }
    }
}
