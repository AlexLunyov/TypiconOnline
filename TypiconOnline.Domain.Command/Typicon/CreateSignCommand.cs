using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class CreateSignCommand : EditRuleCommandBase<Sign>
    {
        public CreateSignCommand(int id
            , ItemText name
            , int? templateId
            , bool isAddition
            , int? number
            , int priority
            , string ruleDefinition
            , string modRuleDefinition) : base(id)
        {
            Name = name;
            TemplateId = templateId;
            IsAddition = isAddition;
            Number = number;
            Priority = priority;
            RuleDefinition = ruleDefinition;
            ModRuleDefinition = modRuleDefinition;
        }
        public ItemText Name { get; }
        public int? TemplateId { get; }
        public bool IsAddition { get; }
        public int? Number { get; }
        public int Priority { get; }
        public string RuleDefinition { get; }
        public string ModRuleDefinition { get; }
    }
}
