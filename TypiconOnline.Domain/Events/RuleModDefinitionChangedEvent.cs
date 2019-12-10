using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Events;

namespace TypiconOnline.Domain.Events
{
    public class RuleModDefinitionChangedEvent : IDomainEvent// where T: RuleEntity
    {
        public RuleModDefinitionChangedEvent(ModRuleEntity entity, string oldDef, string newDef)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            OldDefinition = oldDef;
            NewDefinition = newDef;
        }

        public ModRuleEntity Entity { get; }

        public string OldDefinition { get; }

        public string NewDefinition { get; }
    }
}
