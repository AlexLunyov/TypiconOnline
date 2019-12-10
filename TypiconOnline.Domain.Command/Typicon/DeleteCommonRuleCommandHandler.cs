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
    public class DeleteCommonRuleCommandHandler : DeleteRuleCommandHandlerBase<CommonRule>, ICommandHandler<DeleteCommonRuleCommand>
    {
        public DeleteCommonRuleCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public Task<Result> ExecuteAsync(DeleteCommonRuleCommand command)
        {
            return Task.FromResult(Execute(command));
        }
    }
}
