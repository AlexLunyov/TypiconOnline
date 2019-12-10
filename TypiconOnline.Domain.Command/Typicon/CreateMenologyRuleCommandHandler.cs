using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class CreateMenologyRuleCommandHandler : CreateRuleCommandHandlerBase<MenologyRule>, ICommandHandler<CreateMenologyRuleCommand>
    {
        public CreateMenologyRuleCommandHandler(TypiconDBContext dbContext, CollectorSerializerRoot serializerRoot) : base(dbContext, serializerRoot) { }

        public async Task<Result> ExecuteAsync(CreateMenologyRuleCommand command)
        {
            return await base.ExecuteAsync(command);
        }

        protected override MenologyRule Create(CreateRuleCommandBase<MenologyRule> command, TypiconVersion typiconVersion)
        {
            var c = command as CreateMenologyRuleCommand;


            var entity = new MenologyRule
            {
                TypiconVersion = typiconVersion,
                TemplateId = c.TemplateId,
                IsAddition = c.IsAddition,
                RuleDefinition = c.RuleDefinition,
                ModRuleDefinition = c.ModRuleDefinition
            };

            //Синхронизируем Переменные Устава
            //entity.SyncRuleVariables(SerializerRoot);
            //entity.SyncModRuleVariables(SerializerRoot);

            foreach (var i in c.DayWorshipIds)
            {
                entity.DayRuleWorships.Add(new DayRuleWorship() { DayRuleId = entity.Id, DayWorshipId = i.Id, Order = i.Order });
            }

            return entity;
        }
    }
}
