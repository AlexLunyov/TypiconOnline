using EFSecondLevelCache.Core;
using EFSecondLevelCache.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Repository.Caching
{
    public class EFCacheRepository<DomainType> : IRepository<DomainType> where DomainType : class, IAggregateRoot
    {
        const string KEY_SALT = "EF2ndLvlCache";

        readonly IRepository<DomainType> repository;
        EFCacheDebugInfo debugInfo = new EFCacheDebugInfo();
        IEFCacheKeyProvider cacheKeyProvider;
        IEFCacheServiceProvider cacheServiceProvider;

        public EFCacheRepository(IRepository<DomainType> repository, IEFCacheKeyProvider cacheKeyProvider, IEFCacheServiceProvider cacheServiceProvider)
        {
            this.repository = repository ?? throw new ArgumentNullException("repository in CachingRepository");
            this.cacheKeyProvider = cacheKeyProvider ?? throw new ArgumentNullException("cacheKeyProvider in CachingRepository");
            this.cacheServiceProvider = cacheServiceProvider ?? throw new ArgumentNullException("cacheServiceProvider in CachingRepository");
        }

        public void Delete(DomainType aggregate)
        {
            repository.Delete(aggregate);
        }

        public DomainType Get(Expression<Func<DomainType, bool>> predicate)
        {
            return GetAll(predicate).FirstOrDefault();
        }

        public IQueryable<DomainType> GetAll(Expression<Func<DomainType, bool>> predicate = null)
        {
            return repository.GetAll(predicate).Cacheable(KEY_SALT, debugInfo, cacheKeyProvider, cacheServiceProvider);
        }

        public void Insert(DomainType aggregate)
        {
            repository.Insert(aggregate);
        }

        public void Update(DomainType aggregate)
        {
            repository.Update(aggregate);
        }
    }
}
