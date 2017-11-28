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
    public class CommonRuleService
    {
        #region Singletone pattern

        public static CommonRuleService Instance
        {
            get { return NestedCommonRuleService.instance; }
        }

        private class NestedCommonRuleService
        {
            static NestedCommonRuleService()
            {
            }

            internal static readonly CommonRuleService instance = new CommonRuleService();
        }

        #endregion

        /// <summary>
        /// Возвращает строку из системного правила, где определен только один элемент ItemText
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ItemText GetItemTextValue(CommonRuleServiceRequest request)
        {
            TextHolder textHolder = (TextHolder)GetCommonRuleChildren(request).FirstOrDefault();

            return (textHolder?.Paragraphs.Count > 0) ? textHolder.Paragraphs[0] : new ItemText();
        }

        /// <summary>
        /// Возвращает строку из системного правила, где определен только один элемент ItemText
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetTextValue(CommonRuleServiceRequest request, string language)
        {
            return GetItemTextValue(request)[language];
        }

        /// <summary>
        /// Возвращает коллекцию RuleElement запрашиваемого общего правила.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<RuleElement> GetCommonRuleChildren(CommonRuleServiceRequest request)
        {
            if (request == null 
                || request.RuleSerializer == null
                || request.Typicon == null) throw new ArgumentNullException("CommonRuleServiceRequest");

            CommonRule commonRule = request.Typicon.GetCommonRule(c => c.Name == request.Key);

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
    }
}
