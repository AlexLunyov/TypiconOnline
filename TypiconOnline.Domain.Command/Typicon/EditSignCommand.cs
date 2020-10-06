using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditSignCommand : EditRuleCommandBase<Sign>
    {
        public EditSignCommand(int id
            , string name
            , int? templateId
            , bool isAddition
            , int? printTemplateId
            , int priority
            , string ruleDefinition
            , string modRuleDefinition) : base(id)
        {
            Name = name;
            TemplateId = templateId;
            IsAddition = isAddition;
            PrintTemplateId = printTemplateId;
            Priority = priority;
            RuleDefinition = ruleDefinition;
            ModRuleDefinition = modRuleDefinition;
        }
        public string Name { get; }
        public int? TemplateId { get; }
        public bool IsAddition { get; }
        public int? PrintTemplateId { get; }
        public int Priority { get; }
        public string RuleDefinition { get; }
        public string ModRuleDefinition { get; }
    }
}
