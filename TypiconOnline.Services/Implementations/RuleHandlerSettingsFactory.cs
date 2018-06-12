using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.AppServices.Implementations
{
    public class RuleHandlerSettingsFactory : IRuleHandlerSettingsFactory
    {
        IRuleSerializerRoot ruleSerializer;

        public RuleHandlerSettingsFactory(IRuleSerializerRoot ruleSerializer)
        {
            this.ruleSerializer = ruleSerializer ?? throw new ArgumentNullException("ruleSerializer");
        }
        /// <summary>
        /// Создает настройки для IRuleHandler
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual RuleHandlerSettings Create(GetRuleSettingsRequest req)
        {
            if (req == null)
            {
                throw new ArgumentNullException("GetRuleSettingsRequest in Create");
            }

            RuleHandlerSettings settings = null;

            (ITemplateHavingEntity existingRule, RootContainer container) = GetFirstExistingRule(req.Rule, ruleSerializer);

            if (existingRule != null)
            {
                settings = InnerCreate(req, container);

                if (existingRule.IsAddition && existingRule.Template != null)
                {
                    req.Rule = existingRule.Template;

                    settings = Create(req);
                }
            }

            return settings;
        }

        /// <summary>
        /// Возвращает первое Правило с имеющимся определением из цепочки шаблонов, либо NULL
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        private (ITemplateHavingEntity existingRule, RootContainer container) GetFirstExistingRule(ITemplateHavingEntity rule, IRuleSerializerRoot serializer)
        {
            ITemplateHavingEntity r = null;
            var cont = rule.GetRule<RootContainer>(serializer);

            if (cont != null)
            {
                r = rule;
            }
            else if (rule.Template != null)
            {
                return GetFirstExistingRule(rule.Template, serializer);
            }

            return (r, cont);
        }

        private RuleHandlerSettings InnerCreate(GetRuleSettingsRequest req, RootContainer container)
        {
            return new RuleHandlerSettings()
            {
                Addition = req.AdditionalSettings,
                TypiconId = req.TypiconId,
                Date = req.Date,
                RuleContainer = container,
                DayWorships = req.DayWorships?.ToList(),
                OktoikhDay = req.OktoikhDay,
                Language = LanguageSettingsFactory.Create(req.Language),
                SignNumber = req.SignNumber,
                ApplyParameters = req.ApplyParameters,
                CheckParameters = req.CheckParameters
            };
        }
    }
}
