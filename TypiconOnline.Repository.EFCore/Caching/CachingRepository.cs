using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Repository.EFCore.Caching;

namespace TypiconOnline.Repository.EFCore.Caching
{
    public class CachingRepository<DomainType> : IRepository<DomainType> where DomainType : class//, IAggregateRoot
    {
        const string KEY_CACHEDURATION = "ShortCacheDuration";
        const string KEY_GET = "RepositoryGet";
        const string KEY_GETALL = "RepositoryGetAll";
        const string KEY_GETCOLLECTION = "CachedGet";
        const string KEY_GETALLCOLLECTION = "CachedGetAll";

        TimeSpan? cacheDurationTime;
        CachedGetCollection getCollection;
        CachedGetAllCollection getAllCollection;

        readonly IRepository<DomainType> repository;
        readonly ICacheStorage cacheStorage;
        readonly IConfigurationRepository configurationRepository;

        public CachingRepository(IRepository<DomainType> repository, ICacheStorage cacheStorage, IConfigurationRepository configurationRepository)
        {
            this.repository = repository ?? throw new ArgumentNullException("repository in CachingRepository");
            this.cacheStorage = cacheStorage ?? throw new ArgumentNullException("ICacheStorage");
            this.configurationRepository = configurationRepository ?? throw new ArgumentNullException("IConfigurationRepository");
        }

        /// <summary>
        /// Возвращает сущность по заданному лямбда-выражению.
        /// Если таковое сохраненно в кеше, то обращения к БД не происходит.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public DomainType Get(Expression<Func<DomainType, bool>> predicate, IncludeOptions options = null)
        {
            //делаем выборку из кешированных данных
            var item = cacheStorage.GetEntities<DomainType>(GetCollection, CacheDurationTime).Where(predicate).FirstOrDefault();
            
            if (item == null)
            {
                //обращаемся к репозиторию
                item = repository.Get(predicate);

                if (item != null)
                {
                    StoreItem(item);
                }
            }

            return item;
        }

        /// <summary>
        /// Возвращает коллекцию сущностей по заданному лямбда-выражению.
        /// Кеширование совершается по лямбда-выражению, но не по отдельным сущностям.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<DomainType> GetAll(Expression<Func<DomainType, bool>> predicate = null, IncludeOptions options = null)
        {
            var predicateKey = (predicate != null) ? predicate.Body.ToString() : string.Empty;

            //Ищем введеный запрос среди сохраненных
            //При обращении дата просрочки обновляется найденного запроса
            var cachedQuery = GetAllCollection.GetItem(predicateKey);

            if (cachedQuery != null)
            {
                //Продляем дату просрочки у запрошенных сущностей
                GetCollection.ProlongExpirationDates(cachedQuery);

                //Возвращаем коллекцию сущностей по указателям на сущности, сохраненные в кеше
                return cacheStorage.GetEntities<DomainType>(cachedQuery);
            }
            else
            {
                //делаем явную выборку из репозитория
                var repoItems = repository.GetAll(predicate);

                //сохраняем выборку в кеше
                StoreItems(repoItems, predicateKey);

                return repoItems;
            }
        }

        public void Add(DomainType aggregate) => repository.Add(aggregate);

        public void Update(DomainType aggregate) => repository.Update(aggregate);

        public void Remove(DomainType aggregate) => repository.Remove(aggregate);

        private TimeSpan CacheDurationTime
        {
            get
            {
                if (cacheDurationTime == null)
                {
                    cacheDurationTime = TimeSpan.FromMinutes(configurationRepository.GetConfigurationValue<int>(KEY_CACHEDURATION));
                }

                return (TimeSpan)cacheDurationTime;
            }
        }

        private CachedGetCollection GetCollection
        {
            get
            {
                if (getCollection == null)
                {
                    getCollection = cacheStorage.Retrieve<CachedGetCollection>(CachedGetCollectionKey) ?? new CachedGetCollection(CacheDurationTime);
                }

                return getCollection;
            }
        }

        private CachedGetAllCollection GetAllCollection
        {
            get
            {
                if (getAllCollection == null)
                {
                    getAllCollection = cacheStorage.Retrieve<CachedGetAllCollection>(CachedGetAllCollectionKey) ?? new CachedGetAllCollection(CacheDurationTime);
                }

                return getAllCollection;
            }
        }

        private string CachedGetCollectionKey => $"{KEY_GETCOLLECTION}:{typeof(DomainType).FullName}";
        private string CachedGetAllCollectionKey => $"{KEY_GETALLCOLLECTION}:{typeof(DomainType).FullName}";
        private string GetItemCachedKey(DomainType item) => $"{KEY_GET}:{typeof(DomainType).FullName}:{item.GetHashCode()}";

        private void Store(string key, object entity)
        {
            cacheStorage.Store(key, entity, CacheDurationTime);
        }

        private string StoreItem(DomainType item)
        {
            string key = GetItemCachedKey(item);
            //сохраняем в кеш объект
            Store(key, item);

            //добавляем указатель
            GetCollection.AddPointer(GetItemCachedKey(item), DateTime.Now.Add(CacheDurationTime));
            
            //сохраняем в кеш коллекцию
            Store(CachedGetCollectionKey, GetCollection);

            return key;
        }

        private void StoreItems(IEnumerable<DomainType> items, string predicateKey)
        {
            //создаем коллекцию ключей для кешированного запроса
            var cachedQuery = new GetAllCollectionItem
            {
                ExpirationDate = DateTime.Now.Add(CacheDurationTime)
            };

            //сохраняем объекты в кеше и добавляем ссылки в коллекцию
            foreach (var item in items)
            {
                var key = StoreItem(item);

                cachedQuery.Collection.Add(key);
            }

            //добавялем кешированный запрос в общую коллекцию
            GetAllCollection.AddPointer(predicateKey, cachedQuery);

            //сохраняем в кеш коллекцию
            Store(CachedGetAllCollectionKey, GetAllCollection);
        }
    }
}
