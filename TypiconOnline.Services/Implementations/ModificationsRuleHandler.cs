using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Обработчик правил. Только добавляет ModifiedRule
    /// </summary>
    public class ModificationsRuleHandler : RuleHandlerBase
    {
        IRulesExtractor rulesExtractor;
        readonly ModifiedYear modifiedYear;

        public ModificationsRuleHandler(IRulesExtractor rulesExtractor, ModifiedYear modifiedYear) 
        {
            this.rulesExtractor = rulesExtractor ?? throw new ArgumentNullException("rulesExtractor in ModificationsRuleHandler");
            this.modifiedYear = modifiedYear ?? throw new ArgumentNullException("modifiedYear in ModificationsRuleHandler");

            AuthorizedTypes = new List<Type>()
            {
                typeof(ModifyDay)
            };
        }

        public override void ClearResult()
        {
            //nothing
        }

        public override bool Execute(ICustomInterpreted element)
        {
            bool result = false;

            if (element is ModifyReplacedDay modifyReplacedDay)
            {
                DayRule ruleToModify;

                if (modifyReplacedDay.Kind == KindOfReplacedDay.Menology)
                {
                    ruleToModify = rulesExtractor.GetMenologyRule(modifiedYear.TypiconEntityId, modifyReplacedDay.DateToReplaceCalculated);
                }
                else //if ((element as ModifyReplacedDay).Kind == RuleConstants.KindOfReplacedDay.triodion)
                {
                    int daysFromEaster = modifyReplacedDay.EasterContext.GetDaysFromCurrentEaster(modifyReplacedDay.DateToReplaceCalculated);
                    ruleToModify = rulesExtractor.GetTriodionRule(modifiedYear.TypiconEntityId, daysFromEaster);
                }

                int? priority = modifyReplacedDay.Priority;

                if (priority == null)
                {
                    priority = ruleToModify.Template.Priority;
                }

                var request = CreateRequest(ruleToModify, modifyReplacedDay, (int)priority);

                modifiedYear.AddModifiedRule(request);

                result = true;
            }
            else if ((element is ModifyDay modifyDay) 
                && (modifyDay.MoveDateCalculated.Year == modifiedYear.Year))
            {
                int? priority = modifyDay.Priority;

                DayRule dayRule = FindDayRule(_settings);

                if (priority == null)
                {
                    priority = dayRule.Template.Priority;
                }

                var request = CreateRequest(dayRule, modifyDay, (int)priority);

                modifiedYear.AddModifiedRule(request);

                result = true;
            }

            ModificationsRuleRequest CreateRequest(DayRule caller, ModifyDay md, int priority)
            {
                return new ModificationsRuleRequest()
                {
                    Caller = caller,
                    Date = md.MoveDateCalculated,
                    Priority = priority,
                    ShortName = md.ShortName,
                    AsAddition = md.AsAddition,
                    IsLastName = md.IsLastName,
                    UseFullName = md.UseFullName,
                    SignNumber = md.SignNumber,
                    Filter = md.Filter
                };
            }

            return result;
        }

        private DayRule FindDayRule(RuleHandlerSettings settings)
        {
            return (settings.TypiconRule is DayRule) ? settings.TypiconRule as DayRule : FindDayRule(settings.Addition);
        }

        //public override RenderContainer GetResult()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
