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
            var item = CachedCollection.GetItems(cacheStorage).Where(predicate).FirstOrDefault();
            
            if (item == null)
            {
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
            return repository.GetAll(predicate);
        }

        public void Insert(DomainType aggregate)
        {
            repository.Insert(aggregate);
        }

        public void Update(DomainType aggregate)
        {
            repository.Update(aggregate);
        }

        public void Delete(DomainType aggregate)
        {
            repository.Delete(aggregate);
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

        private void Store(string key, object entity)
        {
            cacheStorage.Store(key, entity, TimeSpan.FromMinutes(CacheDurationTime));
        }

        private void StoreItem(DomainType item)
        {
            string key = $"{KEY_GET}:{typeof(DomainType).Name}:{item.GetHashCode()}";

            CachedCollection.AddPointer(key, TimeSpan.FromMinutes(CacheDurationTime));

            Store(key, item);
            Store(CachedCollectionKey, cachedCollection);
        }
    }
}
