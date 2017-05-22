using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Rules.Handlers
{
    /// <summary>
    /// Обработчик правил. Только добавляет ModifiedRule
    /// </summary>
    public class ModificationsRuleHandler : RuleHandlerBase
    {
        private readonly int _yearToModify;

        public ModificationsRuleHandler(RuleHandlerRequest request)// : base(request)
        {
            Initialize(request);

            AuthorizedTypes = new List<Type>()
            {
                typeof(ModifyDay)
            };
        }

        public ModificationsRuleHandler(RuleHandlerRequest request, int year) : this(request)
        {
            _yearToModify = year;
        }

        public override void Execute(ICustomInterpreted element)
        {
            if ((element is ModifyDay) 
                && ((element as ModifyDay).MoveDateCalculated.Year == _yearToModify))
            {
                int priority = (element as ModifyDay).Priority.Value;

                TypiconRule seniorTypiconRule = Rules[0];

                if (priority == 0)
                {
                    priority = seniorTypiconRule.Template.Priority;
                }

                ModificationsRuleRequest request = new ModificationsRuleRequest()
                {
                    Caller = seniorTypiconRule,
                    Date = (element as ModifyDay).MoveDateCalculated,
                    Priority = priority,
                    ShortName = (element as ModifyDay).ShortName,
                    AsAddition = (element as ModifyDay).AsAddition.Value,
                    IsLastName = (element as ModifyDay).IsLastName.Value,
                    UseFullName = (element as ModifyDay).UseFullName.Value
                };

                TypiconEntity typiconEntity = seniorTypiconRule.Owner;//Folder.GetOwner();

                typiconEntity.AddModifiedRule(request);
            }
        }

        public override RuleContainer GetResult()
        {
            throw new NotImplementedException();
        }
    }
}
