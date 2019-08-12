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
    public class DeleteMenologyRuleCommandHandler : DeleteRuleCommandHandlerBase<MenologyRule>, ICommandHandler<DeleteMenologyRuleCommand>
    {
        public DeleteMenologyRuleCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public async Task<Result> ExecuteAsync(DeleteMenologyRuleCommand command)
        {
            return await base.ExecuteAsync(command);
        }
    }
}
