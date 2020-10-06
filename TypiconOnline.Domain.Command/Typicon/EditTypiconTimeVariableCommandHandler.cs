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
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditTypiconTimeVariableCommandHandler : EditRuleCommandHandlerBase<TypiconVariable>, ICommandHandler<EditTypiconTimeVariableCommand>
    {
        public EditTypiconTimeVariableCommandHandler(TypiconDBContext dbContext, CollectorSerializerRoot serializerRoot) : base(dbContext, serializerRoot) { }

        public Task<Result> ExecuteAsync(EditTypiconTimeVariableCommand command)
        {
            return Task.FromResult(Execute(command));
        }

        protected override Result UpdateValues(TypiconVariable entity, EditRuleCommandBase<TypiconVariable> command)
        {
            var c = command as EditTypiconTimeVariableCommand;

            //редактируем только если Устав является Шаблоном
            if (entity.TypiconVersion.IsTemplate)
            {
                entity.Header = c.Header;
                entity.Description = c.Description;
            }

            entity.Value = c.Value;

            return Result.Ok();
        }
    }
}
