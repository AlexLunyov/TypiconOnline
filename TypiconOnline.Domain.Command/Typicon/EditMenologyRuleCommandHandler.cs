using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditMenologyRuleCommandHandler : EditRuleCommandHandlerBase<MenologyRule>, ICommandHandler<EditMenologyRuleCommand>
    {
        public EditMenologyRuleCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(EditMenologyRuleCommand command)
        {
            return await base.ExecuteAsync(command);
        }

        protected override void UpdateValues(MenologyRule entity, EditRuleCommandBase<MenologyRule> command)
        {
            var c = command as EditMenologyRuleCommand;

            entity.TemplateId = c.TemplateId;
            entity.IsAddition = c.IsAddition;
            entity.Date = (c.Date != null) ? new ItemDate(c.Date.Value.Month, c.Date.Value.Day) : null;
            entity.LeapDate = (c.LeapDate != null) ? new ItemDate(c.LeapDate.Value.Month, c.LeapDate.Value.Day) : null;
            entity.RuleDefinition = c.RuleDefinition;
            entity.ModRuleDefinition = c.ModRuleDefinition;

            entity.DayRuleWorships.Clear();

            foreach ((int id, int order) in c.DayWorshipIds)
            {
                entity.DayRuleWorships.Add(new DayRuleWorship() { DayRuleId = entity.Id, DayWorshipId = id, Order = order });
            }
        }
    }
}
