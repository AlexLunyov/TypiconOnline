using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Handlers
{
    /// <summary>
    /// Обработчик правил. Только добавляет ModifiedRule
    /// </summary>
    public class ModificationsRuleHandler : RuleHandlerBase
    {
        private readonly int _yearToModify;

        public ModificationsRuleHandler(RuleHandlerSettings settings)// : base(request)
        {
            _settings = settings;
            //Initialize(settings);

            AuthorizedTypes = new List<Type>()
            {
                typeof(ModifyDay)
            };
        }

        public ModificationsRuleHandler(RuleHandlerSettings request, int year) : this(request)
        {
            _yearToModify = year;
        }

        public override void Execute(ICustomInterpreted element)
        {
            if (element is ModifyReplacedDay)
            {
                //

                TypiconEntity typiconEntity = _settings.Rule.Owner; //Rules[0].Owner;

                TypiconRule ruleToModify;

                if ((element as ModifyReplacedDay).Kind == KindOfReplacedDay.Menology)
                {
                    ruleToModify = typiconEntity.GetMenologyRule((element as ModifyReplacedDay).DateToReplaceCalculated);
                }
                else //if ((element as ModifyReplacedDay).Kind == RuleConstants.KindOfReplacedDay.triodion)
                {
                    ruleToModify = typiconEntity.GetTriodionRule((element as ModifyReplacedDay).DateToReplaceCalculated);
                }

                int priority = (element as ModifyReplacedDay).Priority;

                if (priority == 0)
                {
                    priority = ruleToModify.Template.Priority;
                }

                ModificationsRuleRequest request = new ModificationsRuleRequest()
                {
                    Caller = ruleToModify,
                    Date = (element as ModifyReplacedDay).MoveDateCalculated,
                    Priority = priority,
                    ShortName = (element as ModifyReplacedDay).ShortName,
                    AsAddition = (element as ModifyReplacedDay).AsAddition,
                    IsLastName = (element as ModifyReplacedDay).IsLastName,
                    UseFullName = (element as ModifyReplacedDay).UseFullName
                };

                typiconEntity.AddModifiedRule(request);
            }
            else if ((element is ModifyDay) 
                && ((element as ModifyDay).MoveDateCalculated.Year == _yearToModify))
            {
                int priority = (element as ModifyDay).Priority;

                //TypiconRule seniorTypiconRule = Rules[0];

                if (priority == 0)
                {
                    priority = /*seniorTypicon*/_settings.Rule.Template.Priority;
                }

                ModificationsRuleRequest request = new ModificationsRuleRequest()
                {
                    Caller = /*seniorTypicon*/_settings.Rule,
                    Date = (element as ModifyDay).MoveDateCalculated,
                    Priority = priority,
                    ShortName = (element as ModifyDay).ShortName,
                    AsAddition = (element as ModifyDay).AsAddition,
                    IsLastName = (element as ModifyDay).IsLastName,
                    UseFullName = (element as ModifyDay).UseFullName
                };

                TypiconEntity typiconEntity = /*seniorTypicon*/_settings.Rule.Owner;//Folder.GetOwner();

                typiconEntity.AddModifiedRule(request);
            }
        }

        //public override RenderContainer GetResult()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
