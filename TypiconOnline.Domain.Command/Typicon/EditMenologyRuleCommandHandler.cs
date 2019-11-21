using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditMenologyRuleCommandHandler : EditRuleCommandHandlerBase<MenologyRule>, ICommandHandler<EditMenologyRuleCommand>
    {
        public EditMenologyRuleCommandHandler(TypiconDBContext dbContext, CollectorSerializerRoot serializerRoot) : base(dbContext, serializerRoot) { }

        public async Task<Result> ExecuteAsync(EditMenologyRuleCommand command)
        {
            return await base.ExecuteAsync(command);
        }

        protected override Result UpdateValues(MenologyRule entity, EditRuleCommandBase<MenologyRule> command)
        {
            var c = command as EditMenologyRuleCommand;

            //Синхронизируем Переменные Устава
            if (entity.RuleDefinition != c.RuleDefinition)
            {
                entity.RuleDefinition = c.RuleDefinition;
                entity.SyncRuleVariables(SerializerRoot);
            }
            if (entity.ModRuleDefinition != c.ModRuleDefinition)
            {
                entity.ModRuleDefinition = c.ModRuleDefinition;
                entity.SyncModRuleVariables(SerializerRoot);
            }

            entity.TemplateId = c.TemplateId;
            entity.IsAddition = c.IsAddition;

            entity.DayRuleWorships.Clear();

            foreach ((int id, int order) in c.DayWorshipIds)
            {
                entity.DayRuleWorships.Add(new DayRuleWorship() { DayRuleId = entity.Id, DayWorshipId = id, Order = order });
            }

            return Result.Ok();
        }
    }
}
