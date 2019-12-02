using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Configuration;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.AppServices.Jobs.Scheduled;
using TypiconOnline.AppServices.Messaging.Common;
using TypiconOnline.AppServices.Migration.Typicon;
using TypiconOnline.AppServices.Viewers;
using TypiconOnline.Domain.Command;
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
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.WebServices.Hosting;

namespace TypiconOnline.Tests.Common
{
    public class SIContainer : Container
    {
        public SIContainer() : base()
        {
            Register();
        }

        private void Register()
        {
            //Queries and Commands
            //Register(typeof(IQuery<>), typeof(QueryProcessor).Assembly);
            Register(typeof(IQueryHandler<,>), typeof(QueryProcessor).Assembly, typeof(TypiconEntityModel).Assembly);
            Register<IQueryProcessor, DataQueryProcessor>();

            Register(typeof(ICommandHandler<>), typeof(CommandProcessor).Assembly, typeof(ScheduleDataCalculator).Assembly);

            RegisterConditional<ICommandProcessor, AsyncCommandProcessor>(
                c => c.Consumer.ImplementationType == typeof(JobExecutor));
            RegisterConditional<ICommandProcessor, CommandProcessor>(c => !c.Handled);

            //OutputForms
            Register<IRuleHandlerSettingsFactory, RuleHandlerSettingsFactory>();
            Register<IScheduleDayNameComposer, ScheduleDayNameComposer>();
            Register<IRuleSerializerRoot, RuleSerializerRoot>();
            Register<ITypiconSerializer, TypiconSerializer>();
            //Для TypiconVariables
            Register<CollectorSerializerRoot>();

            //Register<IOutputForms, OutputForms>();
            Register<IOutputDayFactory, OutputDayFactory>();
            Register<IScheduleDayViewer<string>, HtmlScheduleDayViewer>();
            Register<IScheduleWeekViewer<string>, TextScheduleWeekViewer>();
            Register<IScheduleWeekViewer<Result<FileDownloadResponse>>, DocxToStreamWeekViewer>();

            Register<ITypiconExportManager, TypiconXmlExportManager>();
            Register<ITypiconImportManager, TypiconXmlImportManager>();
            Register<IProjector<TypiconVersion, TypiconVersionProjection>, TypiconExportProjector>();
            Register<IProjector<TypiconVersionProjection, TypiconEntity>, TypiconImportProjector>();

            //регистрируем просто ScheduleHandler - будет формировать только расписание, без последовательностей
            //но зато повыситься производительность
            Register<ScheduleHandler, ServiceSequenceHandler>();
            //Register<ScheduleHandler>();

            ////Все контроллеры
            Register<IScheduleDataCalculator, MajorDataCalculator>();

            RegisterDecorator(
                typeof(IScheduleDataCalculator),
                typeof(TransparentDataCalculator));

            RegisterDecorator(
                typeof(IScheduleDataCalculator),
                typeof(AsAdditionDataCalculator));

            RegisterDecorator(
                typeof(IScheduleDataCalculator),
                typeof(ExplicitDataCalculator));

            //CustomSequence Controller
            Register<CustomScheduleDataCalculator>();

            RegisterSingleton<IJobRepository>(() => new JobRepository());

            RegisterInstance(TypiconDbContextFactory.Create());
        }
    }
}
