using JetBrains.Annotations;
using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    /// <summary>
    /// Возвращает строку из Правила, представляющего из себя коллекцию TextHolder, согласно индекса 
    /// (Номер TextHolder-а в коллекции Правила. "ноль" ориентированный)
    /// </summary>
    public class CommonRuleIndexedStringValueQuery : IDataQuery<string>
    {
        public CommonRuleIndexedStringValueQuery(int typiconId, string name, [NotNull] IRuleSerializerRoot ruleSerializer, string language, int index)
        {
            TypiconId = typiconId;
            Name = name;
            RuleSerializer = ruleSerializer;
            Language = language;
            Index = index;
        }

        public int TypiconId { get; }
        /// <summary>
        /// Имя правила
        /// </summary>
        public string Name { get; }
        public IRuleSerializerRoot RuleSerializer { get; }
        public string Language { get; }
        public int Index { get; }
    }
}
