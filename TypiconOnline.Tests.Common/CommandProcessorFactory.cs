using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Command;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Infrastructure.Common.Command;
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
    }
}
