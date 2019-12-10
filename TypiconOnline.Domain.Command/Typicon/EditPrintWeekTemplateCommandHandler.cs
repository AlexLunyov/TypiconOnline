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
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Domain.Typicon.Print;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditPrintWeekTemplateCommandHandler : EditRuleCommandHandlerBase<PrintWeekTemplate>, ICommandHandler<EditPrintWeekTemplateCommand>
    {
        public EditPrintWeekTemplateCommandHandler(TypiconDBContext dbContext, CollectorSerializerRoot serializerRoot) : base(dbContext, serializerRoot) { }

        public Task<Result> ExecuteAsync(EditPrintWeekTemplateCommand command)
        {
            return Task.FromResult(Execute(command));
        }

        protected override Result UpdateValues(PrintWeekTemplate entity, EditRuleCommandBase<PrintWeekTemplate> command)
        {
            var c = command as EditPrintWeekTemplateCommand;

            entity.DaysPerPage = c.DaysPerPage;

            if (c.PrintFile != null)
            {
                entity.PrintFile = c.PrintFile;
            }

            if (!string.IsNullOrEmpty(c.PrintFileName))
            {
                entity.PrintFileName = c.PrintFileName;
            }

            return Result.Ok();
        }
    }
}
