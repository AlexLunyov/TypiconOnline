using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command
{
    public class CommandProcessor : ICommandProcessor, IDisposable
    {
        private readonly Container container;

        public CommandProcessor(Container container)
        {
            this.container = container;
        }

        //[DebuggerStepThrough]
        public virtual void Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            dynamic handler = GetHandler(command);

            handler.Execute((dynamic)command);
        }

        //[DebuggerStepThrough]
        public virtual async Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            dynamic handler = GetHandler(command);

            await handler.ExecuteAsync((dynamic)command);
        }

        private dynamic GetHandler<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handlerType =
                typeof(ICommandHandler<>).MakeGenericType(command.GetType());

            return container.GetInstance(handlerType);
        }

        public void Dispose()
        {
            //scope.Dispose();
        }
    }
}
