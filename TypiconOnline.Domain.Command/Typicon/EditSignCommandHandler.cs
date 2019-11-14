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
    public class EditSignCommandHandler : EditRuleCommandHandlerBase<Sign>, ICommandHandler<EditSignCommand>
    {
        public EditSignCommandHandler(TypiconDBContext dbContext, CollectorSerializerRoot serializerRoot) : base(dbContext, serializerRoot) { }

        public async Task<Result> ExecuteAsync(EditSignCommand command)
        {
            return await base.ExecuteAsync(command);
        }

        protected override Result UpdateValues(Sign entity, EditRuleCommandBase<Sign> command)
        {
            var c = command as EditSignCommand;

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

            //не возможно просто присвоить значение, потому как ef core 
            //будет думать, что TypiconEntity удалена
            entity.SignName.ReplaceValues(c.Name);

            entity.TemplateId = (c.TemplateId > 0) ? c.TemplateId : null;
            entity.IsAddition = c.IsAddition;
            entity.Number = c.Number;
            entity.Priority = c.Priority;

            return Result.Ok();
        }
    }
}
