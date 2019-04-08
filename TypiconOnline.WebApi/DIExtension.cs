using CacheManager.Core;
using EFSecondLevelCache.Core;
using EFSecondLevelCache.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Caching;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Repository.EFCore;
using TypiconOnline.Repository.Caching;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Repository.EFCore.Caching;
using TypiconOnline.AppServices.Configuration;
using TypiconOnline.AppServices.Jobs;
using SimpleInjector;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Command;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.WebApi.DIExtensions;
using TypiconOnline.Infrastructure.Common.Query;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using TypiconOnline.WebServices.Hosting;

namespace TypiconOnline.WebApi
{
    /// <summary>
    /// Не используется. Оставлен на всякий случай в качестве примера использования стандартных средств DI
    /// </summary>
    public static class DIExtension
    {
        public static void AddTypiconOnlineService(this IServiceCollection services, IConfiguration configuration, Container container)
        {
            ////typiconservices
            //services.AddScoped<IEvangelionContext, EvangelionContext>();
            //services.AddScoped<IApostolContext, ApostolContext>();
            //services.AddScoped<IOldTestamentContext, OldTestamentContext>();
            //services.AddScoped<IPsalterContext, PsalterContext>();
            //services.AddScoped<IOktoikhContext, OktoikhContext>();
            //services.AddScoped<ITheotokionAppContext, TheotokionAppContext>();
            //services.AddScoped<IEasterContext, EasterContext>();
            //services.AddScoped<IKatavasiaContext, KatavasiaContext>();

            //services.AddScoped<IScheduleService, ScheduleService>();

            //OutputForms
            container.Register<IRuleHandlerSettingsFactory, RuleHandlerSettingsFactory>();
            container.Register<IScheduleDayNameComposer, ScheduleDayNameComposer>();
            container.Register<IRuleSerializerRoot, RuleSerializerRoot>();
            container.Register<ITypiconSerializer, TypiconSerializer>(); 
            container.Register<IOutputForms, OutputForms>();
            container.Register<IOutputFormFactory, OutputFormFactory>();
            container.Register<IScheduleDataCalculator, ScheduleDataCalculator>();
            container.Register<IScheduleDayViewer<string>, TextScheduleDayViewer>();
            container.Register<IScheduleWeekViewer<string>, TextScheduleWeekViewer>();


            //Configuration
            container.Register<IConfigurationRepository>(() => new ConfigurationRepository(configuration));

            //Queue
            container.RegisterSingleton<IJobRepository, JobRepository>();

            //For SQLite
            //container.Register<JobHostedService>();
            //services.AddHostedServiceFromContainer<JobHostedService>(container);

            //For MySql
            container.Register<JobAsyncHostedService>();
            services.AddHostedServiceFromContainer<JobAsyncHostedService>(container);

            //query command jobs
            //container.RegisterTypiconQueryClasses();
            //container.RegisterTypiconCommandClasses();


            //container.Register(typeof(IDataQuery<>), typeof(QueryProcessor).Assembly);
            container.Register(typeof(IDataQueryHandler<,>), typeof(QueryProcessor).Assembly);
            container.Register<IDataQueryProcessor, DataQueryProcessor>();

            //container.Register(typeof(IQuery<>), typeof(QueryProcessor).Assembly);
            container.Register(typeof(IQueryHandler<,>), typeof(QueryProcessor).Assembly);
            container.Register<IQueryProcessor, QueryProcessor>();


            container.Register(typeof(ICommandHandler<>), typeof(CommandProcessor).Assembly, typeof(OutputForms).Assembly);
            container.Register<ICommandProcessor, CommandProcessor>();

            services.AddDbContext<TypiconDBContext>(optionsBuilder =>
            {
                //SqlServer
                //var connectionString = configuration.GetConnectionString("MSSql");
                //optionsBuilder.UseSqlServer(connectionString);

                //SQLite
                //var connectionString = configuration.GetConnectionString("DBTypicon");
                //optionsBuilder.UseSqlite(connectionString);

                //MySQL
                optionsBuilder.UseMySql(configuration.GetConnectionString("MySql"),
                        mySqlOptions =>
                        {
                            mySqlOptions.ServerVersion(new Version(8, 0, 15), ServerType.MySql);
                        });
            });
        }
    }
}
