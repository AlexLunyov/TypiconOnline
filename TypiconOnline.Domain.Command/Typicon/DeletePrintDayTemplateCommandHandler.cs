using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class DeletePrintDayTemplateCommandHandler : DeleteRuleCommandHandlerBase<PrintDayTemplate>, ICommandHandler<DeletePrintDayTemplateCommand>
    {
        public DeletePrintDayTemplateCommandHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public Task<Result> ExecuteAsync(DeletePrintDayTemplateCommand command)
        {
            return Task.FromResult(Execute(command));
        }
    }
}
