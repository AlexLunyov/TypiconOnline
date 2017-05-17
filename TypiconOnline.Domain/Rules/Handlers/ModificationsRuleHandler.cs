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
        public ModificationsRuleHandler(RuleHandlerRequest request)// : base(request)
        {
            Initialize(request);

            AuthorizedTypes = new List<Type>()
            {
                typeof(DayModification)
            };
        }

        public override void Execute(ICustomInterpreted element)
        {
            if (element is DayModification)
            {
                int priority = (element as DayModification).Priority.Value;

                if (priority == 0)
                {
                    priority = SeniorTypiconRule.Template.Priority;
                }

                ModificationsRuleRequest request = new ModificationsRuleRequest()
                {
                    Caller = SeniorTypiconRule,
                    Date = (element as DayModification).MoveDateCalculated,
                    Priority = priority,
                    ShortName = (element as DayModification).ShortName,
                    AsAddition = (element as DayModification).AsAddition.Value,
                    IsLastName = (element as DayModification).IsLastName.Value
                };

                TypiconEntity typiconEntity = SeniorTypiconRule.Owner;//Folder.GetOwner();

                typiconEntity.AddModifiedRule(request);
            }
        }

        public override RuleContainer GetResult()
        {
            throw new NotImplementedException();
        }
    }
}
