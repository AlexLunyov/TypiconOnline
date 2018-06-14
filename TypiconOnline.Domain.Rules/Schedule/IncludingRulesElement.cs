using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public abstract class IncludingRulesElement : ExecContainer
    {
        protected IRuleSerializerRoot SerializerRoot { get; }

        public IncludingRulesElement(string name, IRuleSerializerRoot serializerRoot) : base(name)
        {
            SerializerRoot = serializerRoot ?? throw new ArgumentNullException("IRuleSerializerRoot");
        }
    }
}
