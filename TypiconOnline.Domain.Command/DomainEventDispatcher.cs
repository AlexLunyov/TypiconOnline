using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Events;

namespace TypiconOnline.Domain.Command
{
    public class DomainEventDispatcher : IEventDispatcher
    {
        public DomainEventDispatcher(Container container)
        {
            Container = container;
        }

        protected Container Container { get; }

        public Result Dispatch<TEvent>(TEvent evnt) where TEvent : IDomainEvent
        {
            Result result = Result.Ok();

            var handlerType =
                typeof(IDomainEventHandler<>).MakeGenericType(evnt.GetType());

            foreach (dynamic h in Container.GetAllInstances(handlerType))
            {
                result = Result.Combine(result, h.Execute((dynamic)evnt));
            }

            return result;
        }
    }
}
