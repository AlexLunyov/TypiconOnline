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
    public class EditTriodionRuleCommandHandler : EditRuleCommandHandlerBase<TriodionRule>, ICommandHandler<EditTriodionRuleCommand>
    {
        public EditTriodionRuleCommandHandler(TypiconDBContext dbContext, CollectorSerializerRoot serializerRoot) : base(dbContext, serializerRoot) { }

        public Task<Result> ExecuteAsync(EditTriodionRuleCommand command)
        {
            return Task.FromResult(Execute(command));
        }

        protected override Result UpdateValues(TriodionRule entity, EditRuleCommandBase<TriodionRule> command)
        {
            var c = command as EditTriodionRuleCommand;

            //Синхронизируем Переменные Устава
            //if (entity.RuleDefinition != c.RuleDefinition)
            //{
                entity.RuleDefinition = c.RuleDefinition;
            //    entity.SyncRuleVariables(SerializerRoot);
            //}
            //if (entity.ModRuleDefinition != c.ModRuleDefinition)
            //{
                entity.ModRuleDefinition = c.ModRuleDefinition;
            //    entity.SyncModRuleVariables(SerializerRoot);
            //}

            entity.TemplateId = c.TemplateId;
            entity.DaysFromEaster = c.DaysFromEaster;
            entity.IsTransparent = c.IsTransparent;

            entity.DayRuleWorships.Clear();

            foreach ((int id, int order) in c.DayWorshipIds)
            {
                entity.DayRuleWorships.Add(new DayRuleWorship() { DayRuleId = entity.Id, DayWorshipId = id, Order = order });
            }

            return Result.Ok();
        }
    }
}
