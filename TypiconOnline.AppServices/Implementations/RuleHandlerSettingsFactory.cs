using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Implementations
{
    public class RuleHandlerSettingsFactory : IRuleHandlerSettingsFactory
    {
        readonly IRuleSerializerRoot _ruleSerializer;

        public RuleHandlerSettingsFactory(IRuleSerializerRoot ruleSerializer)
        {
            _ruleSerializer = ruleSerializer ?? throw new ArgumentNullException(nameof(ruleSerializer));
        }
        /// <summary>
        /// Создает настройки для IRuleHandler
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual Result<RuleHandlerSettings> CreateRecursive(CreateRuleSettingsRequest req)
        {
            if (req == null)
            {
                throw new ArgumentNullException(nameof(req));
            }

            (ITemplateHavingEntity existingRule, RootContainer container) = GetFirstExistingRule(req.Rule, req.RuleMode);

            if (existingRule != null)
            {
                var settings = InnerCreate(req, container);

                if (existingRule.IsAddition && existingRule.Template != null)
                {
                    req.Rule = existingRule.Template;

                    settings = CreateRecursive(req);
                }

                return settings;
            }

           return Result.Fail<RuleHandlerSettings>("");
        }

        /// <summary>
        /// Создает настройки из ExplicitAddRule
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Result<RuleHandlerSettings> CreateExplicit(CreateExplicitRuleSettingsRequest req)
        {
            if (req == null)
            {
                throw new ArgumentNullException(nameof(req));
            }

            var container = req.Rule.GetRule<RootContainer>(_ruleSerializer);

            return InnerCreate(req, container);
        }

        /// <summary>
        /// Возвращает первое Правило с имеющимся определением из цепочки шаблонов, либо NULL
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        private (ITemplateHavingEntity, RootContainer) GetFirstExistingRule(ITemplateHavingEntity rule, RuleMode ruleMode)
        {
            ITemplateHavingEntity r = null;

            var cont = (ruleMode == RuleMode.Rule) 
                        ? rule.GetRule<RootContainer>(_ruleSerializer)
                        : rule.GetModRule<RootContainer>(_ruleSerializer);

            if (cont != null)
            {
                r = rule;
            }
            else if (rule.Template != null)
            {
                return GetFirstExistingRule(rule.Template, ruleMode);
            }

            return (r, cont);
        }

        private Result<RuleHandlerSettings> InnerCreate(CreateRuleSettingsRequest req, RootContainer container)
        {
            var settings = new RuleHandlerSettings()
            {
                Addition = req.AdditionalSettings,
                TypiconVersionId = req.TypiconVersionId,
                Date = req.Date,
                RuleContainer = container,
                PrintDayTemplate = req.PrintDayTemplate,
                ApplyParameters = req.ApplyParameters,
                CheckParameters = req.CheckParameters,
                OktoikhDay = req.OktoikhDay,
            };

            if (req.Menologies != null)
            {
                settings.Menologies = req.Menologies.ToList();
            }

            if (req.Triodions != null)
            {
                settings.Triodions = req.Triodions.ToList();
            }

            return Result.Ok(settings);
        }

        private Result<RuleHandlerSettings> InnerCreate(CreateExplicitRuleSettingsRequest req, RootContainer container)
        {
            return Result.Ok(
                new RuleHandlerSettings()
                {
                    TypiconVersionId = req.TypiconVersionId,
                    Date = req.Date,
                    RuleContainer = container,
                    ApplyParameters = req.ApplyParameters,
                    CheckParameters = req.CheckParameters
                });
        }
    }
}
