using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.Repository.EFCore.Caching
{
    /// <summary>
    /// Коллекция указателей на кеш для метода Get
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CachedGetCollection : CachedCollection<DateTime> 
    {
        public CachedGetCollection(TimeSpan durationTime) : base(durationTime) { }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheStorage"></param>
        /// <param name="prolongStoring">Продлять ли хранение в кеше сущностей</param>
        /// <returns></returns>
        //public IQueryable<T> GetEntities<T>(ICacheStorage cacheStorage, bool prolongStoring = true) where T : IAggregateRoot
        //{
        //    ClearExpiredPointers();

        //    var items = new List<T>();

        //    foreach (var pointer in pointers)
        //    {
        //        var t = cacheStorage.Retrieve<T>(pointer.Key);
        //        if (t != null)
        //        {
        //            items.Add(t);
        //        }

        //        if (prolongStoring)
        //        {
        //            pointers[pointer.Key] = DateTime.Now.Add(DurationTime);

        //            cacheStorage.Store(pointer.Key, t, DurationTime);
        //        }
        //    }

        //    return items.AsQueryable();
        //}

        public IEnumerable<KeyValuePair<string, DateTime>> Items
        {
            get
            {
                ClearExpiredPointers();

                return pointers.AsEnumerable();
            }
        }

        /// <summary>
        /// Удаляет все просроченные указатели
        /// </summary>
        protected override void ClearExpiredPointers()
        {
            pointers = pointers.Where(pair => (DateTime.Now < pair.Value))
                                 .ToDictionary(pair => pair.Key,
                                               pair => pair.Value);
        }

        /// <summary>
        /// Обновляет даты просрочки на более поздние для совпадающих ключей
        /// </summary>
        /// <param name="collection"></param>
        public CachedGetCollection ProlongExpirationDates(GetAllCollectionItem collection)
        {
            foreach (var item in pointers.ToList())
            {
                if (collection.Collection.Contains(item.Key)
                    && collection.ExpirationDate > item.Value)
                {
                    pointers[item.Key] = collection.ExpirationDate;
                }
            }

            return this;
        }
    }
}
