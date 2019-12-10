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
    public class EditPrintDayTemplateCommandHandler : EditRuleCommandHandlerBase<PrintDayTemplate>, ICommandHandler<EditPrintDayTemplateCommand>
    {
        public EditPrintDayTemplateCommandHandler(TypiconDBContext dbContext, CollectorSerializerRoot serializerRoot) : base(dbContext, serializerRoot) { }

        public Task<Result> ExecuteAsync(EditPrintDayTemplateCommand command)
        {
            return Task.FromResult(Execute(command));
        }

        protected override Result UpdateValues(PrintDayTemplate entity, EditRuleCommandBase<PrintDayTemplate> command)
        {
            var c = command as EditPrintDayTemplateCommand;

            entity.Name = c.Name;
            entity.Number = c.Number;
            entity.SignSymbol = c.Sign;

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
