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
using TypiconOnline.Infrastructure.Common.Query;
using JetBrains.Annotations;
using TypiconOnline.Domain.Query.Typicon;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Обработчик правил. Только добавляет ModifiedRule
    /// </summary>
    public class ModificationsRuleHandler : RuleHandlerBase
    {
        IDataQueryProcessor queryProcessor;
        readonly ModifiedYear modifiedYear;

        public ModificationsRuleHandler([NotNull] IDataQueryProcessor queryProcessor, ModifiedYear modifiedYear) 
        {
            this.queryProcessor = queryProcessor;
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
                    ruleToModify = queryProcessor.Process(new MenologyRuleQuery(modifiedYear.TypiconEntityId, modifyReplacedDay.DateToReplaceCalculated));
                }
                else //if ((element as ModifyReplacedDay).Kind == RuleConstants.KindOfReplacedDay.triodion)
                {
                    ruleToModify = queryProcessor.Process(new TriodionRuleQuery(modifiedYear.TypiconEntityId, modifyReplacedDay.DateToReplaceCalculated));
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
