using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.Domain.Command
{
    public class CommandProcessor : ICommandProcessor//, IDisposable
    {
        public CommandProcessor(Container container)
        {
            Container = container;
        }

        protected Container Container { get; }

        //[DebuggerStepThrough]
        public virtual Result Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            return ExecuteAsync(command).Result;
        }

        //[DebuggerStepThrough]
        public virtual async Task<Result> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            dynamic handler = GetHandler(command);

            return await handler.ExecuteAsync((dynamic)command);
        }

        private dynamic GetHandler<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handlerType =
                typeof(ICommandHandler<>).MakeGenericType(command.GetType());

            return Container.GetInstance(handlerType);
        }

        //public void Dispose()
        //{
        //    //scope.Dispose();
        //}
    }
}
