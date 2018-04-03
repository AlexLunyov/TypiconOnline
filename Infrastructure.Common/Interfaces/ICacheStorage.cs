﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.Interfaces
{
    public interface ICacheStorage
    {
        void Remove(string key);
        void Store(string key, object data);
        void Store(string key, object data, TimeSpan slidingExpiration);
        void Store(string key, object data, DateTime absoluteExpiration, TimeSpan? slidingExpiration = null);
        T Retrieve<T>(string key);
        void InvalidateCacheDependencies(string[] rootCacheKeys);
    }
}
