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
    public class EditExplicitAddRuleCommandHandler : EditRuleCommandHandlerBase<ExplicitAddRule>, ICommandHandler<EditExplicitAddRuleCommand>
    {
        public EditExplicitAddRuleCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(EditExplicitAddRuleCommand command)
        {
            return await base.ExecuteAsync(command);
        }

        protected override void UpdateValues(ExplicitAddRule entity, EditRuleCommandBase<ExplicitAddRule> command)
        {
            var c = command as EditExplicitAddRuleCommand;

            entity.Date = c.Date;
            entity.RuleDefinition = c.RuleDefinition;
        }
    }
}
