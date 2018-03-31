using CacheManager.Core;
using EFSecondLevelCache.Core;
using EFSecondLevelCache.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Repository.EFCore.Caching;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Repository.EFCore.Tests.Common
{
    public class EFCacheUOFFactory
    {
        public static UnitOfWork Create()
        {
            var services = new ServiceCollection();
            services.AddEFSecondLevelCache();

            services.AddSingleton(typeof(ICacheManagerConfiguration),
                new CacheManager.Core.ConfigurationBuilder()
                    .WithJsonSerializer()
                    .WithMicrosoftMemoryCacheHandle()
                    .WithExpiration(ExpirationMode.Absolute, TimeSpan.FromMinutes(10))
                    .DisablePerformanceCounters()
                    .DisableStatistics()
                    .Build());
            services.AddSingleton(typeof(ICacheManager<>), typeof(BaseCacheManager<>));

            var serviceProvider = services.BuildServiceProvider();
            var cacheServiceProvider = serviceProvider.GetService<IEFCacheServiceProvider>();

            
            var optionsBuilder = new DbContextOptionsBuilder<DBContextBase>();

            //string path = $"Data Source=31.31.196.160;Initial Catalog=u0351320_Typicon;Integrated Security=False;User Id=u0351320_defaultuser;Password=DDOR0YUMg519DbT2ebzN;MultipleActiveResultSets=True";
            string path = $"Data Source=(LocalDB)\\MSSQLLocalDB;Database=TypiconDB;Integrated Security=True;Connect Timeout=30";
            optionsBuilder.UseSqlServer(path);

            var context = new EFCacheDBContext(optionsBuilder.Options, cacheServiceProvider);
            
            var cachingRepository = new EFCacheRepositoryFactory(new RepositoryFactory(context), serviceProvider);

            return new UnitOfWork(context, cachingRepository);
        }
    }
}
