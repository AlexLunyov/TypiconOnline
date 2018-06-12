using JetBrains.Annotations;
using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    /// <summary>
    /// Возвращает строку из системного Общего правила, где определен только один элемент ItemText
    /// </summary>
    public class CommonRuleItemTextValueQuery : IDataQuery<ItemText>
    {
        public CommonRuleItemTextValueQuery(int typiconId, string name, [NotNull] IRuleSerializerRoot ruleSerializer)
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
