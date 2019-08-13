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
    public class CreateExplicitAddRuleCommandHandler : CreateRuleCommandHandlerBase<ExplicitAddRule>, ICommandHandler<CreateExplicitAddRuleCommand>
    {
        public CreateExplicitAddRuleCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(CreateExplicitAddRuleCommand command)
        {
            return await base.ExecuteAsync(command);
        }

        protected override ExplicitAddRule Create(CreateRuleCommandBase<ExplicitAddRule> command, int typiconVersionId)
        {
            var c = command as CreateExplicitAddRuleCommand;

            return new ExplicitAddRule()
            {
                Date = c.Date,
                RuleDefinition = c.RuleDefinition,
                TypiconVersionId = typiconVersionId,
            };
        }
    }
}
