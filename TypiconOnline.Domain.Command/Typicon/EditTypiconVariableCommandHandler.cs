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
    public class EditTypiconVariableCommandHandler : EditRuleCommandHandlerBase<TypiconVariable>, ICommandHandler<EditTypiconVariableCommand>
    {
        public EditTypiconVariableCommandHandler(TypiconDBContext dbContext, CollectorSerializerRoot serializerRoot) : base(dbContext, serializerRoot) { }

        public async Task<Result> ExecuteAsync(EditTypiconVariableCommand command)
        {
            return await base.ExecuteAsync(command);
        }

        protected override Result UpdateValues(TypiconVariable entity, EditRuleCommandBase<TypiconVariable> command)
        {
            var c = command as EditTypiconVariableCommand;

            entity.Description = c.Description;

            return Result.Ok();
        }
    }
}
