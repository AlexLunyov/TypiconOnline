using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditCommonRuleCommand : EditRuleCommandBase<CommonRule>
    {
        public EditCommonRuleCommand(int id
            , string name
            , string ruleDefinition) : base(id)
        {
            Name = name;
            RuleDefinition = ruleDefinition;
        }
        public string Name { get; }
        public string RuleDefinition { get; }
    }
}
