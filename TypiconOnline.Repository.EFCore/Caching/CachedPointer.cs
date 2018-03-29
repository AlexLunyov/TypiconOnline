using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Repository.EFCore.Caching
{
    public class CachedPointer<T> where T : IAggregateRoot
    {
        readonly DateTime expirationTime;

        public CachedPointer(string key, TimeSpan cacheDuration)
        {
            Key = key;
            expirationTime = DateTime.Now.Add(cacheDuration);
        }

        public bool IsExpired => (DateTime.Now > expirationTime);

        public string Key { get; }
    }
}
