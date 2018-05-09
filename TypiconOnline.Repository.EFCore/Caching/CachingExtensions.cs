using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.Repository.EFCore.Caching
{
    public static class CachingExtensions 
    {
        /// <summary>
        /// Исключает из формирующегося запроса все кешированные сущности.
        /// Сравнение идет по Хеш-коду. НЕ ИСПОЛЬЗУЕТСЯ.
        /// </summary>
        /// <typeparam name="DomainType"></typeparam>
        /// <param name="repoItems"></param>
        /// <param name="cachedItems"></param>
        /// <returns></returns>
        public static IQueryable<DomainType> ExcludeCachedItems<DomainType>(this IQueryable<DomainType> repoItems
            , IEnumerable<DomainType> cachedItems) where DomainType : class//, IAggregateRoot
        {
            foreach (var item in cachedItems)
            {
                repoItems = repoItems.Where(c => c.GetHashCode() != item.GetHashCode());
            }

            return repoItems;
        }

        public static IEnumerable<DomainType> UnionWithCachedItems<DomainType>(this IQueryable<DomainType> repoItems
            , IEnumerable<DomainType> cachedItems) where DomainType : class//, IAggregateRoot
        {
            return cachedItems.Union(repoItems);
        }

        public static IQueryable<T> GetEntities<T>(this ICacheStorage cacheStorage, 
            GetAllCollectionItem cachedQuery, bool prolongStoring = true) //where T : IAggregateRoot
        {
            var items = new List<T>();

            foreach (var key in cachedQuery.Collection)
            {
                var t = cacheStorage.Retrieve<T>(key);
                if (t != null)
                {
                    items.Add(t);

                    if (prolongStoring)
                    {
                        cacheStorage.Store(key, t, cachedQuery.ExpirationDate);
                    }
                }
            }

            return items.AsQueryable();
        }

        public static IQueryable<T> GetEntities<T>(this ICacheStorage cacheStorage, CachedGetCollection collection, 
            TimeSpan durationTime, bool prolongStoring = true) //where T : IAggregateRoot
        {
            var items = new List<T>();

            foreach (var pointer in collection.Items)
            {
                var t = cacheStorage.Retrieve<T>(pointer.Key);
                if (t != null)
                {
                    items.Add(t);
                }

                if (prolongStoring)
                {
                    var expDate = DateTime.Now.Add(durationTime);
                    collection.AddPointer(pointer.Key, expDate);

                    cacheStorage.Store(pointer.Key, t, expDate);
                }
            }

            return items.AsQueryable();
        }
    }
}
