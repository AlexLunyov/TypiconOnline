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
    public class CreateMenologyRuleCommandHandler : CreateRuleCommandHandlerBase<MenologyRule>, ICommandHandler<CreateMenologyRuleCommand>
    {
        public CreateMenologyRuleCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(CreateMenologyRuleCommand command)
        {
            return await base.ExecuteAsync(command);
        }

        protected override MenologyRule Create(CreateRuleCommandBase<MenologyRule> command, int typiconVersionId)
        {
            var c = command as CreateMenologyRuleCommand;

            var entity = new MenologyRule
            {
                TypiconVersionId = typiconVersionId,
                TemplateId = c.TemplateId,
                IsAddition = c.IsAddition,
                Date = (c.Date != null) ? new ItemDate(c.Date.Value.Month, c.Date.Value.Day) : new ItemDate(),
                LeapDate = (c.LeapDate != null) ? new ItemDate(c.LeapDate.Value.Month, c.LeapDate.Value.Day) : new ItemDate(),
                RuleDefinition = c.RuleDefinition,
                ModRuleDefinition = c.ModRuleDefinition
            };

            foreach (var i in c.DayWorshipIds)
            {
                entity.DayRuleWorships.Add(new DayRuleWorship() { DayRuleId = entity.Id, DayWorshipId = i.Id, Order = i.Order });
            }

            return entity;
        }
    }
}
