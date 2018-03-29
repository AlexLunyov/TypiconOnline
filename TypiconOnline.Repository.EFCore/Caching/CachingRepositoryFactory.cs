using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.Repository.EFCore.Caching
{
    public class CachingRepositoryFactory : IRepositoryFactory
    {
        readonly IRepositoryFactory repositoryFactory;
        readonly ICacheStorage cacheStorage;
        readonly IConfigurationRepository configurationRepository;

        public CachingRepositoryFactory(IRepositoryFactory repositoryFactory, ICacheStorage cacheStorage, IConfigurationRepository configurationRepository)
        {
            this.repositoryFactory = repositoryFactory ?? throw new ArgumentNullException("repositoryFactory in CachingRepositoryFactory");
            this.cacheStorage = cacheStorage ?? throw new ArgumentNullException("cacheStorage in CachingRepositoryFactory");
            this.configurationRepository = configurationRepository ?? throw new ArgumentNullException("configurationRepository in CachingRepositoryFactory");
        }

        IRepository<AggregateType> IRepositoryFactory.Create<AggregateType>() 
        {
            return new CachingRepository<AggregateType>(repositoryFactory.Create<AggregateType>(), cacheStorage, configurationRepository);
        }
    }
}
