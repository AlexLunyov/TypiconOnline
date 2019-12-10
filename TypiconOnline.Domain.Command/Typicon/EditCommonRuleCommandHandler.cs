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
    public class EditCommonRuleCommandHandler : EditRuleCommandHandlerBase<CommonRule>, ICommandHandler<EditCommonRuleCommand>
    {
        public EditCommonRuleCommandHandler(TypiconDBContext dbContext, CollectorSerializerRoot serializerRoot) : base(dbContext, serializerRoot) { }

        public Task<Result> ExecuteAsync(EditCommonRuleCommand command)
        {
            return Task.FromResult(Execute(command));
        }

        protected override Result UpdateValues(CommonRule entity, EditRuleCommandBase<CommonRule> command)
        {
            var c = command as EditCommonRuleCommand;

            entity.Name = c.Name;

            //Синхронизируем Переменные Устава
            //if (entity.RuleDefinition != c.RuleDefinition)
            //{
                entity.RuleDefinition = c.RuleDefinition;
            //    entity.SyncRuleVariables(SerializerRoot);
            //}

            return Result.Ok();
        }
    }
}
