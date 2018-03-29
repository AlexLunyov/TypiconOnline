using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Repository.EFCore.Caching
{
    public static class CachingExtensions 
    {
        /// <summary>
        /// Исключает из формирующегося запроса все кешированные сущности.
        /// Сравнение идет по Хеш-коду.
        /// </summary>
        /// <typeparam name="DomainType"></typeparam>
        /// <param name="repoItems"></param>
        /// <param name="cachedItems"></param>
        /// <returns></returns>
        public static IQueryable<DomainType> ExcludeCachedItems<DomainType>(this IQueryable<DomainType> repoItems
            , IEnumerable<DomainType> cachedItems) where DomainType : class, IAggregateRoot
        {
            foreach (var item in cachedItems)
            {
                repoItems = repoItems.Where(c => c.GetHashCode() != item.GetHashCode());
            }

            return repoItems;
        }

        public static IEnumerable<DomainType> UnionWithCachedItems<DomainType>(this IQueryable<DomainType> repoItems
            , IEnumerable<DomainType> cachedItems) where DomainType : class, IAggregateRoot
        {
            return cachedItems.Union(repoItems);
        }
    }
}
