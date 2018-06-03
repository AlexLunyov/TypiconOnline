using CacheManager.Core;
using EFSecondLevelCache.Core;
using EFSecondLevelCache.Core.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Services;
using TypiconOnline.AppServices.Standard.Caching;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Books.Evangelion;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.OldTestament;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore;
using TypiconOnline.Repository.Caching;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Repository.EFCore.Caching;
using TypiconOnline.AppServices.Configuration;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.WebApi
{
    /// <summary>
    /// Не используется. Оставлен на всякий случай в качестве примера использования стандартных средств DI
    /// </summary>
    public static class DIExtension
    {
        public static void AddTypiconOnlineService(this IServiceCollection services, IConfiguration configuration)
        {
            //typiconservices
            services.AddScoped<ITypiconEntityService, TypiconEntityService>();
            services.AddScoped<IEvangelionContext, EvangelionContext>();
            services.AddScoped<IApostolContext, ApostolContext>();
            services.AddScoped<IOldTestamentContext, OldTestamentContext>();
            services.AddScoped<IPsalterContext, PsalterContext>();
            services.AddScoped<IOktoikhContext, OktoikhContext>();
            services.AddScoped<ITheotokionAppContext, TheotokionAppContext>();
            services.AddScoped<IEasterContext, EasterContext>();
            services.AddScoped<IKatavasiaContext, KatavasiaContext>();
            
            //scheduleservice
            services.AddScoped<IRuleHandlerSettingsFactory, RuleHandlerSettingsFactory>();
            services.AddScoped<IModifiedYearFactory, ModifiedYearFactory>();
            services.AddScoped<IRulesExtractor, RulesExtractor>();
            services.AddScoped<IModifiedRuleService, ModifiedRuleService>();
            services.AddScoped<IScheduleDayNameComposer, ScheduleDayNameComposer>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IRuleSerializerRoot, RuleSerializerRoot>();
            services.AddScoped<BookStorage>();

            //unitofwork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //caching
            //services.AddTypiconCaching(configuration);

            services.EFCache(configuration);
        }

        private static void AddTypiconCaching(this IServiceCollection services, IConfiguration configuration)
        {
            //DbContext
            services.AddScoped<TypiconDBContext>(serviceProvider =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();
                //SqlServer
                var connectionString = configuration.GetConnectionString("MSSql");
                optionsBuilder.UseSqlServer(connectionString);
                //SQLite
                //var connectionString = configuration.GetConnectionString("DBTypicon");
                //optionsBuilder.UseSqlite(connectionString);

                return new CachedDbContext(optionsBuilder.Options);
            });

            //MemoryCache
            services.AddMemoryCache();
            services.AddSingleton(typeof(ICacheStorage), typeof(MemoryCacheStorage));

            //Configuration
            services.AddScoped<IConfigurationRepository>(serviceProvider => new ConfigurationRepository(configuration));

            services.AddScoped<IRepositoryFactory>(serviceProvider =>
            {
                var dbContext = serviceProvider.GetService<TypiconDBContext>();
                var innerRepository = new RepositoryFactory();

                return new CachingRepositoryFactory(innerRepository,
                    serviceProvider.GetRequiredService<ICacheStorage>(),
                    serviceProvider.GetRequiredService<IConfigurationRepository>());
            });
        }

        private static void EFCache(this IServiceCollection services, IConfiguration configuration)
        {
            //caching
            services.AddEFSecondLevelCache();

            services.AddSingleton(typeof(ICacheManager<>), typeof(BaseCacheManager<>));
            services.AddSingleton(typeof(ICacheManagerConfiguration),
                new CacheManager.Core.ConfigurationBuilder()
                    .WithJsonSerializer()
                    .WithMicrosoftMemoryCacheHandle()
                    .WithExpiration(ExpirationMode.Sliding, TimeSpan.FromMinutes(configuration.GetValue<int>("ShortCacheDuration")))
                    .DisablePerformanceCounters()
                    .DisableStatistics()
                    .Build());

            //DbContext
            services.AddScoped<TypiconDBContext>(serviceProvider =>
            {
                var cacheServiceProvider = serviceProvider.GetService<IEFCacheServiceProvider>();

                var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();
                //SqlServer
                var connectionString = configuration.GetConnectionString("MSSql");
                optionsBuilder.UseSqlServer(connectionString);
                //SQLite
                //var connectionString = configuration.GetConnectionString("DBTypicon");
                //optionsBuilder.UseSqlite(connectionString);

                return new EFCacheDBContext(optionsBuilder.Options, cacheServiceProvider);
            });



            services.AddScoped<IRepositoryFactory>(serviceProvider =>
            {
                var dbContext = serviceProvider.GetService<TypiconDBContext>();
                var innerRepository = new RepositoryFactory();

                return new EFCacheRepositoryFactory(innerRepository, serviceProvider);
            });
        }
    }
}
