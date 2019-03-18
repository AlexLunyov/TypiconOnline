using System;
using System.Linq;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.AppServices.Implementations
{
    public class RuleHandlerSettingsFactory : IRuleHandlerSettingsFactory
    {
        readonly IRuleSerializerRoot ruleSerializer;

        public RuleHandlerSettingsFactory(IRuleSerializerRoot ruleSerializer)
        {
            this.ruleSerializer = ruleSerializer ?? throw new ArgumentNullException(nameof(ruleSerializer));
        }
        /// <summary>
        /// Создает настройки для IRuleHandler
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual RuleHandlerSettings Create(CreateRuleSettingsRequest req)
        {
            if (req == null)
            {
                throw new ArgumentNullException(nameof(req));
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
        private (ITemplateHavingEntity, RootContainer) GetFirstExistingRule(ITemplateHavingEntity rule, IRuleSerializerRoot serializer)
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

        private RuleHandlerSettings InnerCreate(CreateRuleSettingsRequest req, RootContainer container)
        {
            return new RuleHandlerSettings()
            {
                Addition = req.AdditionalSettings,
                TypiconVersionId = req.TypiconVersionId,
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
