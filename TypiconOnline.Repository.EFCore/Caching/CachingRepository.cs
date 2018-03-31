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
    public class CachingRepository<DomainType> : IRepository<DomainType> where DomainType : class, IAggregateRoot
    {
        const string KEY_CACHEDURATION = "ShortCacheDuration";
        const string KEY_GET = "RepositoryGet";
        const string KEY_GETALL = "RepositoryGetAll";
        const string KEY_COLLECTION = "CachedCollection";

        int? cacheDurationTime;
        CachedCollection<DomainType> cachedCollection;

        readonly IRepository<DomainType> repository;
        readonly ICacheStorage cacheStorage;
        readonly IConfigurationRepository configurationRepository;

        public CachingRepository(IRepository<DomainType> repository, ICacheStorage cacheStorage, IConfigurationRepository configurationRepository)
        {
            this.repository = repository ?? throw new ArgumentNullException("repository in CachingRepository");
            this.cacheStorage = cacheStorage ?? throw new ArgumentNullException("ICacheStorage");
            this.configurationRepository = configurationRepository ?? throw new ArgumentNullException("IConfigurationRepository");
        }

        public DomainType Get(Expression<Func<DomainType, bool>> predicate)
        {
            //делаем выборку из кешированных данных
            var item = CachedCollection.GetItems(cacheStorage).Where(predicate).FirstOrDefault();
            
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

        public IQueryable<DomainType> GetAll(Expression<Func<DomainType, bool>> predicate = null)
        {
            //находим сущности в кеше
            var cachedItems = (predicate != null)
                ? CachedCollection.GetItems(cacheStorage).Where(predicate)
                : CachedCollection.GetItems(cacheStorage);

            //делаем явную выборку из репозитория, исключая те сущности, что уже есть в кеше
            var repoItems = repository.GetAll(predicate).ExcludeCachedItems(cachedItems).ToList();

            //var repoItems = ((predicate == null) ? repository.GetAll() : repository.GetAll(predicate)).ExcludeCachedItems(cachedItems);//.ToList();

            //объединяем полученные данные с кешированными
            repoItems.AddRange(cachedItems);
            //var repoItems = cachedItems.Union((predicate == null) ? repository.GetAll() : repository.GetAll(predicate));

            //сохраняем выборку в кеше
            StoreItems(repoItems);

            return repoItems.AsQueryable();
        }

        public void Insert(DomainType aggregate)
        {
            repository.Insert(aggregate);

            StoreItem(aggregate);
        }

        public void Update(DomainType aggregate)
        {
            repository.Update(aggregate);

            StoreItem(aggregate);
        }

        public void Delete(DomainType aggregate)
        {
            repository.Delete(aggregate);

            //удаляем указатель
            CachedCollection.RemovePointer(GetItemCachedKey(aggregate));
            Store(CachedCollectionKey, cachedCollection);

            //удаляем из кеша сам объект
            cacheStorage.Remove(GetItemCachedKey(aggregate));
        }

        private int CacheDurationTime
        {
            get
            {
                if (cacheDurationTime == null)
                {
                    cacheDurationTime = configurationRepository.GetConfigurationValue<int>(KEY_CACHEDURATION);
                }

                return (int)cacheDurationTime;
            }
        }

        private CachedCollection<DomainType> CachedCollection
        {
            get
            {
                if (cachedCollection == null)
                {
                    cachedCollection = cacheStorage.Retrieve<CachedCollection<DomainType>>(CachedCollectionKey) ?? new CachedCollection<DomainType>();
                }

                return cachedCollection;
            }
        }

        private string CachedCollectionKey => $"{KEY_COLLECTION}:{typeof(DomainType).Name}";

        private string GetItemCachedKey(DomainType item) => $"{KEY_GET}:{typeof(DomainType).Name}:{item.GetHashCode()}";

        private void Store(string key, object entity)
        {
            cacheStorage.Store(key, entity, TimeSpan.FromMinutes(CacheDurationTime));
        }

        private void StoreItem(DomainType item)
        {
            //сохраняем в кеш объект
            Store(GetItemCachedKey(item), item);

            //добавялем указатель
            CachedCollection.AddPointer(GetItemCachedKey(item), TimeSpan.FromMinutes(CacheDurationTime));
            
            //сохраняем в кеш коллекцию
            Store(CachedCollectionKey, cachedCollection);
        }

        private void StoreItems(IEnumerable<DomainType> items)
        {
            foreach (var item in items)
            {
                StoreItem(item);
            }
        }
    }
}
