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

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Обработчик правил. Только добавляет ModifiedRule
    /// </summary>
    public class ModificationsRuleHandler : RuleHandlerBase
    {
        private readonly int _yearToModify;

        public ModificationsRuleHandler(int year) : this (new RuleHandlerSettings(), year) { }

        public ModificationsRuleHandler(RuleHandlerSettings settings, int year) 
        {
            _settings = settings;
            //Initialize(settings);

            AuthorizedTypes = new List<Type>()
            {
                typeof(ModifyDay)
            };

            _yearToModify = year;
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
                //

                TypiconEntity typiconEntity = _settings.Rule.Owner; //Rules[0].Owner;

                DayRule ruleToModify;

                if (modifyReplacedDay.Kind == KindOfReplacedDay.Menology)
                {
                    ruleToModify = typiconEntity.GetMenologyRule(modifyReplacedDay.DateToReplaceCalculated);
                }
                else //if ((element as ModifyReplacedDay).Kind == RuleConstants.KindOfReplacedDay.triodion)
                {
                    ruleToModify = typiconEntity.GetTriodionRule(modifyReplacedDay.DateToReplaceCalculated);
                }

                int priority = modifyReplacedDay.Priority;

                if (priority == 0)
                {
                    priority = ruleToModify.Template.Priority;
                }

                var request = CreateRequest(ruleToModify, modifyReplacedDay, priority);

                //ModificationsRuleRequest request = new ModificationsRuleRequest()
                //{
                //    Caller = ruleToModify,
                //    Date = modifyReplacedDay.MoveDateCalculated,
                //    Priority = priority,
                //    ShortName = modifyReplacedDay.ShortName,
                //    AsAddition = modifyReplacedDay.AsAddition,
                //    IsLastName = modifyReplacedDay.IsLastName,
                //    UseFullName = modifyReplacedDay.UseFullName,
                //    Filter = modifyReplacedDay.Filter
                //};

                typiconEntity.AddModifiedRule(request);

                result = true;
            }
            else if ((element is ModifyDay modifyDay) 
                && (modifyDay.MoveDateCalculated.Year == _yearToModify))
            {
                int priority = modifyDay.Priority;

                //TypiconRule seniorTypiconRule = Rules[0];

                if (priority == 0)
                {
                    priority = /*seniorTypicon*/_settings.Rule.Template.Priority;
                }

                var request = CreateRequest((DayRule)_settings.Rule, modifyDay, priority);

                //ModificationsRuleRequest request = new ModificationsRuleRequest()
                //{
                //    Caller = /*seniorTypicon*/(DayRule)_settings.Rule,
                //    Date = modifyDay.MoveDateCalculated,
                //    Priority = priority,
                //    ShortName = modifyDay.ShortName,
                //    AsAddition = modifyDay.AsAddition,
                //    IsLastName = modifyDay.IsLastName,
                //    UseFullName = modifyDay.UseFullName,
                //    Filter = modifyDay.Filter
                //};

                _settings.Rule.Owner.AddModifiedRule(request);

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
                    Filter = md.Filter
                };
            }

            return result;
        }

        //public override RenderContainer GetResult()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
