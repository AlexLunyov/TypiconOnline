using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.Repository.EFCore.Caching
{
    public class CachedCollection<T> where T : IAggregateRoot
    {
        //List<CachedPointer<T>> pointers = new List<CachedPointer<T>>();
        Dictionary<string, DateTime> pointers = new Dictionary<string, DateTime>();

        public void AddPointer(string key, TimeSpan cacheDuration)
        {
            if (pointers.ContainsKey(key))
            {
                pointers[key] = DateTime.Now.Add(cacheDuration);
            }
            else
            {
                pointers.Add(key, DateTime.Now.Add(cacheDuration));
            }
        }

        public IQueryable<T> GetItems(ICacheStorage cacheStorage)
        {
            ClearExpiredPointers();

            var items = new List<T>();

            foreach (var pointer in pointers)
            {
                var t = cacheStorage.Retrieve<T>(pointer.Key);
                if (t != null)
                {
                    items.Add(t);
                }
            }

            return items.AsQueryable();
        }

        /// <summary>
        /// Удаляет все просроченные указатели
        /// </summary>
        private void ClearExpiredPointers()
        {
            pointers = pointers.Where(pair => (DateTime.Now < pair.Value))
                                 .ToDictionary(pair => pair.Key,
                                               pair => pair.Value);
        }
    }
}
