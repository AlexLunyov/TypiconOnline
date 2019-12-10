using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Events;
using TypiconOnline.Domain.Rules.Serialization;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Events;
using TypiconOnline.Domain.Rules.Extensions;

namespace TypiconOnline.Domain.Command.Events
{
    public class RuleDefinitionChangedEventHandler : IDomainEventHandler<RuleDefinitionChangedEvent>
    {
        public RuleDefinitionChangedEventHandler(CollectorSerializerRoot serializerRoot)
        {
            SerializerRoot = serializerRoot ?? throw new ArgumentNullException(nameof(serializerRoot));
        }

        protected CollectorSerializerRoot SerializerRoot { get; }

        public Result Execute(RuleDefinitionChangedEvent ev)
        {
            return ev.Entity.SyncRuleVariables(SerializerRoot);
        }
    }
}
