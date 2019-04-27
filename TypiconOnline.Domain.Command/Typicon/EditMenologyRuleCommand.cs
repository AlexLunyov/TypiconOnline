using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditMenologyRuleCommand : EditRuleCommandBase<MenologyRule>
    {
        public EditMenologyRuleCommand(int id
            , IEnumerable<(int Id, int Order)> dayWorshipIds
            , int templateId
            , bool isAddition
            , DateTime? date
            , DateTime? leapDate
            , string ruleDefinition
            , string modRuleDefinition) : base(id)
        {
            DayWorshipIds = dayWorshipIds;
            TemplateId = templateId;
            IsAddition = isAddition;
            Date = date;
            LeapDate = leapDate;
            RuleDefinition = ruleDefinition;
            ModRuleDefinition = modRuleDefinition;
        }
        public IEnumerable<(int Id, int Order)> DayWorshipIds { get; set; }
        public int TemplateId { get; set; }
        public bool IsAddition { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? LeapDate { get; set; }
        public string ModRuleDefinition { get; set; }
        public string RuleDefinition { get; set; }
    }
}
