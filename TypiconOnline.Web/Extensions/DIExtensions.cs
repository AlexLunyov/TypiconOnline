using CacheManager.Core;
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
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Repository.EFCore.Caching;
using TypiconOnline.AppServices.Configuration;
using TypiconOnline.Domain.Books.WeekDayApp;
using TypiconOnline.Domain.Typicon;
using SimpleInjector;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Domain.Query;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Domain.Command;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using TypiconOnline.WebServices.Hosting;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.AppServices.Viewers;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Web.Controllers;
using TypiconOnline.AppServices.Jobs.Scheduled;

namespace TypiconOnline.Web
{
    /// <summary>
    /// Не используется. Оставлен на всякий случай в качестве примера использования стандартных средств DI
    /// </summary>
    public static class DIExtensions
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
            container.Register<IScheduleDayViewer<string>, HtmlScheduleDayViewer>();
            container.Register<IScheduleWeekViewer<string>, TextScheduleWeekViewer>();
            container.Register<IScheduleWeekViewer<Result<DocxToStreamWeekResponse>>, DocxToStreamWeekViewer>();
            container.Register<ScheduleHandler, ServiceSequenceHandler>();

            //Все контроллеры
            container.Register<IScheduleDataCalculator, ScheduleDataCalculator>();
            //CustomSequence Controller
            container.Register<CustomScheduleDataCalculator>();

            //Configuration
            container.Register<IConfigurationRepository>(() => new ConfigurationRepository(configuration));

            //JobRepository
            container.RegisterSingleton<IJobRepository>(() 
                => new JobRepository(
                    //каждое воскресенье в 02.00 вычислять расписание на через следующую неделю
                    new NextWeekOutputFormsJob(new EveryWeekJobScheduler(DayOfWeek.Sunday, 02, 0), 2)
                    //каждое 1 декабря вычисляем переходящие праздники на следующий год
                    , new NextModifiedYearJob(new EveryYearJobScheduler(12, 1, 3, 0), 1)));

            container.RegisterDecorator<IJobRepository, LoggingJobRepository>(Lifestyle.Singleton);

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
            container.Register(typeof(IDataQueryHandler<,>), typeof(QueryProcessor).Assembly, typeof(TypiconDTO).Assembly);
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

                //PostgreSQL
                //optionsBuilder.UseNpgsql(configuration.GetConnectionString("Postgre"));
            });
        }
    }
}
