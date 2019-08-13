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
    public class CreateCommonRuleCommandHandler : CreateRuleCommandHandlerBase<CommonRule>, ICommandHandler<CreateCommonRuleCommand>
    {
        public CreateCommonRuleCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(CreateCommonRuleCommand command)
        {
            return await base.ExecuteAsync(command);
        }

        protected override CommonRule Create(CreateRuleCommandBase<CommonRule> command, int typiconVersionId)
        {
            var c = command as CreateCommonRuleCommand;

            return new CommonRule()
            {
                Name = c.Name,
                RuleDefinition = c.RuleDefinition,
                TypiconVersionId = typiconVersionId
            };
        }
    }
}
