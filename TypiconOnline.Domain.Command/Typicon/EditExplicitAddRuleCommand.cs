using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditExplicitAddRuleCommand : EditRuleCommandBase<ExplicitAddRule>
    {
        public EditExplicitAddRuleCommand(int id
            , DateTime date
            , string ruleDefinition) : base(id)
        {
            Date = date;
            RuleDefinition = ruleDefinition;
        }
        public DateTime Date { get; }
        public string RuleDefinition { get; }
    }
}
