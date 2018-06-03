using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Typicon
{
    public static class CommonRuleExtensions
    {
        /// <summary>
        /// Возвращает строку из системного правила, где определен только один элемент ItemText
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ItemText GetItemTextValue(this TypiconEntity typicon, CommonRuleServiceRequest request)
        {
            TextHolder textHolder = (TextHolder)GetChildren(typicon, request).FirstOrDefault();

            return (textHolder?.Paragraphs.Count > 0) ? textHolder.Paragraphs[0] : new ItemText();
        }

        /// <summary>
        /// Возвращает строку из системного правила, где определен только один элемент ItemText
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetTextValue(this TypiconEntity typicon, CommonRuleServiceRequest request, string language)
        {
            return GetItemTextValue(typicon, request).FirstOrDefault(language).Text;
        }

        /// <summary>
        /// Возвращает коллекцию RuleElement запрашиваемого общего правила.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static IEnumerable<RuleElement> GetChildren(this TypiconEntity typicon, CommonRuleServiceRequest request)
        {
            if (request == null
                || request.RuleSerializer == null) throw new ArgumentNullException("CommonRuleServiceRequest");

            CommonRule commonRule = typicon.GetCommonRule(c => c.Name == request.Key);

            if (commonRule == null) throw new NullReferenceException("CommonRule");

            if (!commonRule.IsValid)
            {
                //if (request.Handler.Settings.ThrowExceptionIfInvalid)
                //{
                //    commonRule.ThrowExceptionIfInvalid();
                //}
                //else
                //{
                return new List<RuleElement>();
                //}
            }

            var container = commonRule.GetRule<ExecContainer>(request.RuleSerializer);

            return (container != null) ? container.ChildElements : new List<RuleElement>();
        }

        /// <summary>
        /// Возвращает строку из Правила, представляющего из себя коллекцию TextHolder, согласно индекса
        /// </summary>
        /// <param name="typicon"></param>
        /// <param name="request"></param>
        /// <param name="index">Номер TextHolder-ы в коллекции Правила</param>
        /// <returns></returns>
        public static string GetIndexedString(this TypiconEntity typicon, CommonRuleServiceRequest request, int index, string language)
        {
            string result = "";
            if (GetChildren(typicon, request).ElementAtOrDefault(index) is TextHolder t && t.Paragraphs?.Count > 0)
            {
                result = t.Paragraphs[0]?.FirstOrDefault(language).Text;
            }
            return result;
        }
    }
}
