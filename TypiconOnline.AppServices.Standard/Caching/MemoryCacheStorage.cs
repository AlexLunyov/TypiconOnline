using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.AppServices.Standard.Caching
{
    public class MemoryCacheStorage : ICacheStorage
    {
        readonly IMemoryCache _cache;

        public MemoryCacheStorage(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException("IMemoryCache");
        }

        //public MemoryCacheStorage()
        //{
        //    _cache = new MemoryCache(new MemoryCacheOptions());
        //}

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public T Retrieve<T>(string key)
        {
            return _cache.TryGetValue(key, out T value) ? value : default(T);
        }

        public void Store(string key, object data)
        {
            _cache.Set(key, data);
        }

        public void Store(string key, object data, TimeSpan slidingExpiration)
        {
            var options = new MemoryCacheEntryOptions()
            {
                SlidingExpiration = slidingExpiration
            };

            _cache.Set(key, data, options);
        }

        public void Store(string key, object data, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            var options = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = absoluteExpiration,
                SlidingExpiration = slidingExpiration
            };

            _cache.Set(key, data, options);
        }
    }
}
