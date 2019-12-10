using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Events;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Rules.Serialization;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Events;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Domain.Rules.Extensions;

namespace TypiconOnline.Domain.Command.Events
{
    /// <summary>
    /// Обработчик синхронизирует ссылки на Переменные Устава
    /// </summary>
    public class ModRuleChangedVariablesEventHandler : IDomainEventHandler<RuleModDefinitionChangedEvent>
    {
        public ModRuleChangedVariablesEventHandler(CollectorSerializerRoot serializerRoot)
        {
            SerializerRoot = serializerRoot ?? throw new ArgumentNullException(nameof(serializerRoot));
        }

        protected CollectorSerializerRoot SerializerRoot { get; }

        public Result Execute(RuleModDefinitionChangedEvent ev)
        {
            return ev.Entity.SyncModRuleVariables(SerializerRoot);
        }
    }
}
