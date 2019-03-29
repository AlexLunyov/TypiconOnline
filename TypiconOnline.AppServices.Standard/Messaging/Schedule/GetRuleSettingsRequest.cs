using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    /// <summary>
    /// Используется для создания настроек для IRuleHandler
    /// </summary>
    public class CreateRuleSettingsRequest : ScheduleDataCalculatorRequest
    {
        public CreateRuleSettingsRequest() { }

        public CreateRuleSettingsRequest(ScheduleDataCalculatorRequest request)
        {
            
            TypiconVersionId = request.TypiconVersionId;
            Date = request.Date;
            Language = request.Language;
            ApplyParameters = request.ApplyParameters;
            CheckParameters = request.CheckParameters;
        }

        public ITemplateHavingEntity Rule { get; set; }
        public IEnumerable<DayWorship> Menologies { get; set; }
        public IEnumerable<DayWorship> Triodions { get; set; }
        public OktoikhDay OktoikhDay { get; set; }
        public RuleHandlerSettings AdditionalSettings { get; set; }
        public int? SignNumber { get; set; }
        /// <summary>
        /// Опция определяет, какое определение Правила используется: RuleDefinition или ModRuleDefinition
        /// </summary>
        public RuleMode RuleMode { get; set; } = RuleMode.Rule;
    }

    public enum RuleMode
    {
        Rule = 0,
        ModRule = 1
    }
}
