using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Domain.Command;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Tests.Common
{
    public class CommandProcessorFactory
    {
        private static ICommandProcessor commandProcessor;
        public static ICommandProcessor Instance
        {
            get
            {
                if (commandProcessor == null)
                {
                    commandProcessor = Create();
                }

                return commandProcessor;
            }
        }


        public static ICommandProcessor Create() => Create(TypiconDbContextFactory.Create());

        public static ICommandProcessor Create(TypiconDBContext dbContext)
        {
            var container = new SimpleInjector.Container();

            container.Register<ITypiconSerializer, TypiconSerializer>();

            container.RegisterTypiconCommandClasses();

            container.RegisterInstance(dbContext);

            var processor = container.GetInstance<ICommandProcessor>();

            return processor;
        }

        public static ICommandProcessor CreateJobProcessor(TypiconDBContext dbContext, JobRepository jobRepository)
        {
            var container = new SimpleInjector.Container();

            container.Register<ITypiconSerializer, TypiconSerializer>();

            container.Register(typeof(ICommandHandler<>), typeof(OutputForms).Assembly);
            container.Register<IRuleHandlerSettingsFactory, RuleHandlerSettingsFactory>();
            container.Register<IScheduleDayNameComposer, ScheduleDayNameComposer>();
            container.Register<IRuleSerializerRoot, RuleSerializerRoot>();
            container.Register<IOutputForms, OutputForms>();
            container.Register<IOutputFormFactory, OutputFormFactory>();
            container.Register<IScheduleDataCalculator, ScheduleDataCalculator>();

            container.Register(typeof(IDataQueryHandler<,>), typeof(QueryProcessor).Assembly, typeof(TypiconEntityModel).Assembly);
            container.Register<IDataQueryProcessor, DataQueryProcessor>();

            container.RegisterSingleton(typeof(IJobRepository), jobRepository);

            container.RegisterTypiconCommandClasses();

            container.RegisterInstance(dbContext);

            var processor = container.GetInstance<ICommandProcessor>();

            return processor;
        }
    }
}
