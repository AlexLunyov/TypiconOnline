using System;
using System.Collections.Generic;
using System.Text;
using SimpleInjector;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command
{
    public static class SimpleInjectorExtensions
    {
        public static Container RegisterTypiconCommandClasses(this Container container)
        {
            container.Register(typeof(ICommandHandler<>), typeof(CommandProcessor).Assembly);
            container.Register<ICommandProcessor, CommandProcessor>();

            return container;
        }
    }
}
