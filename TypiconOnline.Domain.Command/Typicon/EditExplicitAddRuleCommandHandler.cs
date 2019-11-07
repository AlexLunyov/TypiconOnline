using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Command.Utilities;
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
    public class EditExplicitAddRuleCommandHandler : EditRuleCommandHandlerBase<ExplicitAddRule>, ICommandHandler<EditExplicitAddRuleCommand>
    {
        public EditExplicitAddRuleCommandHandler(TypiconDBContext dbContext, CollectorSerializerRoot serializerRoot) : base(dbContext, serializerRoot) { }

        public async Task<Result> ExecuteAsync(EditExplicitAddRuleCommand command)
        {
            return await base.ExecuteAsync(command);
        }

        protected override Result UpdateValues(ExplicitAddRule entity, EditRuleCommandBase<ExplicitAddRule> command)
        {
            var c = command as EditExplicitAddRuleCommand;

            entity.Date = c.Date;

            //Синхронизируем Переменные Устава
            if (entity.RuleDefinition != c.RuleDefinition)
            {
                entity.RuleDefinition = c.RuleDefinition;
                entity.SyncRuleVariables(SerializerRoot);
            }

            return Result.Ok();
        }
    }
}
