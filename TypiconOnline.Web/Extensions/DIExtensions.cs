using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SimpleInjector;
using System;
using TypiconOnline.AppServices.Configuration;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.AppServices.Jobs.Scheduled;
using TypiconOnline.AppServices.Messaging.Common;
using TypiconOnline.AppServices.Migration.Books;
using TypiconOnline.AppServices.Migration.Typicon;
using TypiconOnline.AppServices.Viewers;
using TypiconOnline.Domain.Command;
using TypiconOnline.Domain.Command.Events;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Serialization;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Events;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Web.Models.TypiconViewModels;
using TypiconOnline.Web.Services;
using TypiconOnline.WebServices.Authorization;
using TypiconOnline.WebServices.Hosting;

namespace TypiconOnline.Web
{
    /// <summary>
    /// Не используется. Оставлен на всякий случай в качестве примера использования стандартных средств DI
    /// </summary>
    public static class DIExtensions
    {
        public static void AddWebDI(this Container container, IConfiguration configuration
            , IWebHostEnvironment hostingEnv)
        {
            //Email
            container.Register<IEmailSender, EmailSender>();

            //Queries and Commands
            //container.Register(typeof(IQuery<>), typeof(QueryProcessor).Assembly);
            container.Register(typeof(IQueryHandler<,>), typeof(QueryProcessor).Assembly, typeof(TypiconEntityModel).Assembly);
            container.Register<IQueryProcessor, DataQueryProcessor>();

            //authorization handler
            //Декорируем только те запросы, у которых есть реализация IAuthorizedAccess
            container.RegisterDecorator(
                typeof(IQueryHandler<,>),
                typeof(AuthorizationQueryHandler<,>),
                context => typeof(IHasAuthorizedAccess).IsAssignableFrom(
                    context.ServiceType.GetGenericArguments()[0]));

            container.Register(typeof(ICommandHandler<>), typeof(CommandProcessor).Assembly, typeof(ScheduleDataCalculator).Assembly);

            //events handler
            container.RegisterDecorator(
                typeof(ICommandHandler<>),
                typeof(EventsCommandHandler<>));

            //authorization handler
            container.RegisterDecorator(
                typeof(ICommandHandler<>),
                typeof(AuthorizationCommandHandler<>),
                context => typeof(IHasAuthorizedAccess).IsAssignableFrom(
                    context.ServiceType.GetGenericArguments()[0]));

            container.RegisterConditional<ICommandProcessor, AsyncCommandProcessor>(
                c => c.Consumer.ImplementationType == typeof(JobExecutor));
            container.RegisterConditional<ICommandProcessor, CommandProcessor>(c => !c.Handled);

            //events
            container.Register<IEventDispatcher, DomainEventDispatcher>();

            var assemblies = new[] { typeof(DomainEventDispatcher).Assembly };
            container.Collection.Register(typeof(IDomainEventHandler<>), assemblies);

            //OutputForms
            container.Register<IRuleHandlerSettingsFactory, RuleHandlerSettingsFactory>();
            container.Register<IScheduleDayNameComposer, ScheduleDayNameComposer>();
            container.Register<IRuleSerializerRoot, RuleSerializerRoot>();
            container.Register<ITypiconSerializer, TypiconSerializer>();
            //Для TypiconVariables
            container.Register<VariablesCollectorSerializerRoot>(); 

            //container.Register<IOutputForms, OutputForms>();
            container.Register<IOutputDayFactory, NonexistedOutputDayFactory>();
            container.Register<IScheduleDayViewer<string>, HtmlScheduleDayViewer>(); 
            container.Register<IScheduleWeekViewer<string>, HtmlSimpleScheduleWeekViewer>(); 
            //container.Register<IScheduleWeekViewer<Result<FileDownloadResponse>>, DocxToStreamWeekViewer>();

            container.Register<IScheduleWeekViewer<Result<FileDownloadResponse>>, DocxFromOutputTemplatesWeekViewer>();

            container.Register<ITypiconExportManager, TypiconXmlExportManager>();
            container.Register<ITypiconImportManager, TypiconXmlImportManager>();
            container.Register<IProjector<TypiconVersion, TypiconVersionProjection>, TypiconExportProjector>();
            container.Register<IProjector<TypiconVersionProjection, TypiconEntity>, TypiconImportProjector>();

            container.Register<IBooksExportManager, BooksXmlExportManager>();

            //регистрируем просто ScheduleHandler - будет формировать только расписание, без последовательностей
            //но зато повыситься производительность
            container.Register<ScheduleHandler, ServiceSequenceHandler>();
            //container.Register<ScheduleHandler>();

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
            container.Register<OutputDayFactory>();

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

            //container.Register(() => {
            //    var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            //    //SqlServer
            //    //var connectionString = configuration.GetConnectionString("MSSql");
            //    //optionsBuilder.UseSqlServer(connectionString);

            //    //SQLite
            //    //var connectionString = configuration.GetConnectionString("DBTypicon");
            //    //optionsBuilder.UseSqlite(connectionString);

            //    //MySQL
            //    optionsBuilder.UseMySql(configuration.GetConnectionString("MySql"),
            //            mySqlOptions =>
            //            {
            //                mySqlOptions.ServerVersion(new Version(5, 6, 43), ServerType.MySql);
            //            });

            //    //PostgreSQL
            //    //optionsBuilder.UseNpgsql(configuration.GetConnectionString("Postgre"));

            //    return new TypiconDBContext(optionsBuilder.Options);
            //});

            //For SQLite
            //container.Register<JobHostedService>();
            //services.AddHostedServiceFromContainer<JobHostedService>(container);

            //For MySql
            //container.Register<JobAsyncHostedService>();

            #region Validation

            // Register ModelValidator<TModel> adapter class
            container.Register(typeof(ModelValidator<>), typeof(ModelValidator<>),
                Lifestyle.Singleton);

            // Auto-register all validator implementations
            container.Collection.Register(
                typeof(IValidator<>), typeof(MenologyRuleCreateModelValidator).Assembly, typeof(CreateTypiconModelValidator).Assembly);

            #endregion

            #region AuthorizationHandlers

            //container.Register(typeof(AuthorizationHandler<,>), typeof(DefaultAuthorization).Assembly);

            container.Collection.Register(typeof(IAuthorizationHandler), (typeof(OutputDayCanEditAuthorization).Assembly));
            //container.Register<OutputDayCanEditAuthorization>();
            //container.Register<TypiconCanEditAuthorizationHandler>();

            #endregion
        }
    }
}
