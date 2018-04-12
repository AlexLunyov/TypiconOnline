using EFSecondLevelCache.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Repository.EFCore;

namespace TypiconOnline.Repository.Caching
{
    public class EFCacheRepositoryFactory : IRepositoryFactory
    {
        readonly IRepositoryFactory repositoryFactory;
        readonly IEFCacheKeyProvider cacheKeyProvider;
        readonly IEFCacheServiceProvider cacheServiceProvider;

        public EFCacheRepositoryFactory(IRepositoryFactory repositoryFactory, IEFCacheKeyProvider cacheKeyProvider, IEFCacheServiceProvider cacheServiceProvider)
        {
            this.repositoryFactory = repositoryFactory ?? throw new ArgumentNullException("repositoryFactory in EFCacheRepositoryFactory");
            this.cacheKeyProvider = cacheKeyProvider ?? throw new ArgumentNullException("cacheKeyProvider in EFCacheRepositoryFactory");
            this.cacheServiceProvider = cacheServiceProvider ?? throw new ArgumentNullException("cacheServiceProvider in EFCacheRepositoryFactory");
        }

        public EFCacheRepositoryFactory(IRepositoryFactory repositoryFactory, IServiceProvider serviceProvider)
        {
            this.repositoryFactory = repositoryFactory ?? throw new ArgumentNullException("repositoryFactory in EFCacheRepositoryFactory");

            if (serviceProvider == null) throw new ArgumentNullException("serviceProvider in EFCacheRepositoryFactory");

            cacheKeyProvider = serviceProvider.GetService<IEFCacheKeyProvider>();
            cacheServiceProvider = serviceProvider.GetService<IEFCacheServiceProvider>();
        }

        IRepository<AggregateType> IRepositoryFactory.Create<AggregateType>(TypiconDBContext dbContext) 
        {
            return new EFCacheRepository<AggregateType>(repositoryFactory.Create<AggregateType>(dbContext), cacheKeyProvider, cacheServiceProvider);
        }
    }
}
