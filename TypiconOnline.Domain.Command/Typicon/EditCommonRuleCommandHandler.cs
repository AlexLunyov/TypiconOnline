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
    public class EditCommonRuleCommandHandler : EditRuleCommandHandlerBase<CommonRule>, ICommandHandler<EditCommonRuleCommand>
    {
        public EditCommonRuleCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(EditCommonRuleCommand command)
        {
            return await base.ExecuteAsync(command);
        }

        protected override void UpdateValues(CommonRule entity, EditRuleCommandBase<CommonRule> command)
        {
            var c = command as EditCommonRuleCommand;

            entity.Name = c.Name;
            entity.RuleDefinition = c.RuleDefinition;
        }
    }
}
