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
    public class DeleteExplicitAddRuleCommandHandler : DeleteRuleCommandHandlerBase<ExplicitAddRule>, ICommandHandler<DeleteExplicitAddRuleCommand>
    {
        public DeleteExplicitAddRuleCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(DeleteExplicitAddRuleCommand command)
        {
            return await base.ExecuteAsync(command);
        }
    }
}
