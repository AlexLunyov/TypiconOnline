using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypiconOnline.Repository.EFCore.Caching
{
    /// <summary>
    /// Коллекция указателей на кеш для метода GetAll
    /// </summary>
    public class CachedGetAllCollection : CachedCollection<GetAllCollectionItem>
    {
        public CachedGetAllCollection(TimeSpan durationTime) : base(durationTime) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="prolongStoring">Продлить ли хранение указателя на запрос</param>
        /// <returns></returns>
        public GetAllCollectionItem GetItem(string key, bool prolongStoring = true)
        {
            //Удаляем просроченные запросы
            ClearExpiredPointers();

            if (pointers.ContainsKey(key))
            {
                var item = pointers[key];

                if (prolongStoring)
                {
                    item.ExpirationDate = DateTime.Now.Add(DurationTime);
                }

                return item;
            }
            return default(GetAllCollectionItem);
        }

        protected override void ClearExpiredPointers()
        {
            pointers = pointers.Where(pair => (DateTime.Now < pair.Value.ExpirationDate))
                                  .ToDictionary(pair => pair.Key,
                                                pair => pair.Value);
        }
    }
}
