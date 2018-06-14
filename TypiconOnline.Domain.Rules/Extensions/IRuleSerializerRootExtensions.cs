using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Rules.Extensions
{
    static class IRuleSerializerRootExtensions
    {
        /// <summary>
        /// Возвращает строку из системного правила, где определен только один элемент ItemText
        /// </summary>
        /// <returns></returns>
        public static ItemText GetCommonRuleItemTextValue(this IRuleSerializerRoot serializer, int typiconId, string name)
        {
            var textHolder = serializer.GetCommonRuleFirstChild<TextHolder>(typiconId, name);

            return (textHolder?.Paragraphs.Count > 0) ? textHolder.Paragraphs[0] : new ItemText();
        }

        /// <summary>
        /// Возвращает строку из системного правила, где определен только один элемент ItemText
        /// </summary>
        /// <returns></returns>
        public static string GetCommonRuleStringValue(this IRuleSerializerRoot serializer, int typiconId, string name, string language)
        {
            return serializer.GetCommonRuleItemTextValue(typiconId, name).FirstOrDefault(language).Text;
        }

        /// <summary>
        /// Возвращает коллекцию RuleElement запрашиваемого общего правила.
        /// </summary>
        /// <returns></returns>
        public static IReadOnlyList<RuleElement> GetCommonRuleChildren(this IRuleSerializerRoot serializer, int typiconId, string key)
        {
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            var container = serializer.QueryProcessor.Process(new CommonRuleChildElementQuery<RootContainer>(typiconId, key,
                serializer));

            return (container != null) ? container.ChildElements : new List<RuleElement>();
        }

        public static IReadOnlyList<T> GetCommonRuleChildren<T>(this IRuleSerializerRoot serializer, int typiconId,
            string key) where T : class, IRuleElement
        {
            return serializer.GetCommonRuleChildren(typiconId, key).Cast<T>().ToList();
        }

        public static T GetCommonRuleFirstChild<T>(this IRuleSerializerRoot serializer, int typiconId, string key) where T : class, IRuleElement
        {
            var collection = serializer.GetCommonRuleChildren(typiconId, key);

            return (collection?.Count() > 0) ? collection.FirstOrDefault() as T : default(T);
        }

        /// <summary>
        /// Возвращает строку из Правила, представляющего из себя коллекцию TextHolder, согласно индекса
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="typiconId"></param>
        /// <param name="name"></param>
        /// <param name="language"></param>
        /// <param name="index">Номер TextHolder-ы в коллекции Правила</param>
        /// <returns></returns>
        public static string GetCommonRuleIndexedString(this IRuleSerializerRoot serializer, int typiconId, string name, string language, int index)
        {
            var result = "";
            if (serializer.GetCommonRuleChildren(typiconId, name).ElementAtOrDefault(index) is TextHolder t && t.Paragraphs?.Count > 0)
            {
                result = t.Paragraphs[0]?.FirstOrDefault(language).Text;
            }
            return result;
        }
    }
}
