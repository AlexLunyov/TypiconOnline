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
            if (request == null || request.Handler == null) throw new ArgumentNullException();

            CommonRule commonRule = request.Handler.Settings.Rule.Owner.GetCommonRule(c => c.Name == request.Key);

            if (commonRule == null) throw new NullReferenceException("CommonRule");

            commonRule.ThrowExceptionIfInvalid();

            if (!(commonRule.Rule is ExecContainer) || (commonRule.Rule as ExecContainer).ChildElements?.Count == 0)
            {
                throw new ArgumentException("CommonRule");
            }

            TextHolder textHolder = (TextHolder)(commonRule.Rule as ExecContainer).ChildElements[0];

            if (textHolder.Paragraphs?.Count == 0)
            {
                throw new ArgumentException("CommonRule");
            }

            return textHolder.Paragraphs[0];
        }

        /// <summary>
        /// Возвращает строку из системного правила, где определен только один элемент ItemText
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetTextValue(CommonRuleServiceRequest request)
        {
            return GetItemTextValue(request)[request.Handler.Settings.Language];
        }
    }
}
