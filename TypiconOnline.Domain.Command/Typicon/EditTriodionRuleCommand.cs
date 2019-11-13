using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditTriodionRuleCommand : EditRuleCommandBase<TriodionRule>
    {
        public EditTriodionRuleCommand(int id
            , IEnumerable<(int Id, int Order)> dayWorshipIds
            , int templateId
            , int daysFromEaster
            , bool isTransparent
            , string ruleDefinition
            , string modRuleDefinition) : base(id)
        {
            DayWorshipIds = dayWorshipIds;
            TemplateId = templateId;
            DaysFromEaster = daysFromEaster;
            IsTransparent = isTransparent;
            RuleDefinition = ruleDefinition;
            ModRuleDefinition = modRuleDefinition;
        }
        public IEnumerable<(int Id, int Order)> DayWorshipIds { get; set; }
        public int TemplateId { get; set; }
        public int DaysFromEaster { get; set; }
        public bool IsTransparent { get; set; }
        public string ModRuleDefinition { get; set; }
        public string RuleDefinition { get; set; }
    }
}
