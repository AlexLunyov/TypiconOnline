using JetBrains.Annotations;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    /// <summary>
    /// Возвращает коллекцию RuleElement запрашиваемого общего правила.
    /// </summary>
    public class CommonRuleChildElementQuery<T> : IDataQuery<T> where T : IRuleElement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typiconId"></param>
        /// <param name="name">Имя правила</param>
        public CommonRuleChildElementQuery(int typiconId, string name, [NotNull] IRuleSerializerRoot ruleSerializer)
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
