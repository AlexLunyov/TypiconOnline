using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SimpleInjector;
using System;
using TypiconOnline.AppServices.Configuration;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.AppServices.Jobs.Scheduled;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.AppServices.Viewers;
using TypiconOnline.Domain.Command;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.WebServices.Authorization;
using TypiconOnline.WebServices.Hosting;

namespace TypiconOnline.Web
{
    /// <summary>
    /// Не используется. Оставлен на всякий случай в качестве примера использования стандартных средств DI
    /// </summary>
    public static class DIExtensions
    {
        public static void AddTypiconOnlineService(this IServiceCollection services
            , IConfiguration configuration
            , Container container,
            IHostingEnvironment hostingEnv)
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
            //container.Register<IOutputForms, OutputForms>();
            container.Register<IOutputDayFactory, OutputDayFactory>();
            container.Register<IScheduleDayViewer<string>, HtmlScheduleDayViewer>();
            container.Register<IScheduleWeekViewer<string>, TextScheduleWeekViewer>();
            container.Register<IScheduleWeekViewer<Result<DocxToStreamWeekResponse>>, DocxToStreamWeekViewer>();
            //регистрируем просто ScheduleHandler - будет формировать только расписание, без последовательностей
            //но зато повыситься производительность
            //container.Register<ScheduleHandler, ServiceSequenceHandler>();
            container.Register<ScheduleHandler>();

            services.AddScoped<IRuleSerializerRoot, RuleSerializerRoot>();

            //Все контроллеры
            container.Register<IScheduleDataCalculator, MajorDataCalculator>();

            container.RegisterDecorator(
                typeof(IScheduleDataCalculator),
                typeof(TransparentDataCalculator));

            container.RegisterDecorator(
                typeof(IScheduleDataCalculator),
                typeof(AsAdditionDataCalculator));

            container.RegisterDecorator(
                typeof(IScheduleDataCalculator),
                typeof(ExplicitDataCalculator));

            //CustomSequence Controller
            container.Register<CustomScheduleDataCalculator>();

            //Configuration
            container.Register<IConfigurationRepository>(() => new ConfigurationRepository(configuration));

            //JobRepository
            if (hostingEnv.IsDevelopment())
            {
                container.RegisterSingleton<IJobRepository>(() => new JobRepository());
            }
            else
            {
                container.RegisterSingleton<IJobRepository>(()
                => new JobRepository(
                    //каждое воскресенье в 02.00 вычислять расписание на через следующую неделю
                    new NextWeekOutputFormsJob(new EveryWeekJobScheduler(DayOfWeek.Sunday, 02, 0), 2)
                    //каждое 1 декабря вычисляем переходящие праздники на следующий год
                    , new NextModifiedYearJob(new EveryYearJobScheduler(12, 1, 3, 0), 1)));

                container.RegisterDecorator<IJobRepository, LoggingJobRepository>(Lifestyle.Singleton);
            }

            //For SQLite
            //container.Register<JobHostedService>();
            //services.AddHostedServiceFromContainer<JobHostedService>(container);

            //For MySql
            container.Register<JobAsyncHostedService>();
            services.AddHostedServiceFromContainer<JobAsyncHostedService>(container);

            //container.Register(typeof(IQuery<>), typeof(QueryProcessor).Assembly);
            container.Register(typeof(IQueryHandler<,>), typeof(QueryProcessor).Assembly, typeof(TypiconEntityModel).Assembly);
            container.Register<IQueryProcessor, DataQueryProcessor>();


            container.Register(typeof(ICommandHandler<>), typeof(CommandProcessor).Assembly, typeof(ScheduleDataCalculator).Assembly);

            container.RegisterConditional<ICommandProcessor, AsyncCommandProcessor>(
                c => c.Consumer.ImplementationType == typeof(JobAsyncHostedService));
            container.RegisterConditional<ICommandProcessor, CommandProcessor>(c => !c.Handled);

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
                            mySqlOptions.ServerVersion(new Version(5, 6, 43), ServerType.MySql);
                        });

                //PostgreSQL
                //optionsBuilder.UseNpgsql(configuration.GetConnectionString("Postgre"));
            });

            #region AuthorizationHandlers
            // Authorization handlers.
            services.AddScoped<IAuthorizationHandler,
                                  TypiconCanEditAuthorizationHandler>();
            #endregion
        }
    }
}
