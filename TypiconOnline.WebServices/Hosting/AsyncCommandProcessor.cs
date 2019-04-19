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
    public sealed class AsyncCommandProcessor : CommandProcessor, IDisposable
    {
        public AsyncCommandProcessor(Container container) : base(container)
        {
        }

        public override Result Execute<TCommand>(TCommand command) 
        {
            return ExecuteAsync(command).Result;
        }

        public override async Task<Result> ExecuteAsync<TCommand>(TCommand command) 
        {
            using (AsyncScopedLifestyle.BeginScope(Container))
            {
                return await base.ExecuteAsync(command);
            }
        }
    }
}
