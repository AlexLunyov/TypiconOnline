using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    /// <summary>
    /// Возвращает коллекцию RuleElement запрашиваемого общего правила.
    /// </summary>
    public class CommonRuleChildrenQuery : IDataQuery<IEnumerable<RuleElement>>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typiconId"></param>
        /// <param name="name">Имя правила</param>
        public CommonRuleChildrenQuery(int typiconId, string name, [NotNull] IRuleSerializerRoot ruleSerializer)
        {
            TypiconId = typiconId;
            Name = name;
            RuleSerializer = ruleSerializer;
        }

        public int TypiconId { get; }
        /// <summary>
        /// Имя правила
        /// </summary>
        public string Name { get; }
        public IRuleSerializerRoot RuleSerializer { get; }
    }
}
