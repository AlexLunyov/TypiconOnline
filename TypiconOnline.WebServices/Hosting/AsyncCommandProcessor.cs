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
    public sealed class AsyncCommandProcessor : CommandProcessor, IDisposable
    {
        private readonly Container container;

        public AsyncCommandProcessor(Container container) : base(container)
        {
        }

        public override void Execute<TCommand>(TCommand command) 
        {
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                base.Execute(command);
            }
        }

        public override async Task ExecuteAsync<TCommand>(TCommand command) 
        {
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                await base.ExecuteAsync(command);
            }
        }
    }
}
