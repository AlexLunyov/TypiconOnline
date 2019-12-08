using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Extensions;
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
    public class CreatePrintWeekTemplateCommandHandler : CreateRuleCommandHandlerBase<PrintWeekTemplate>, ICommandHandler<CreatePrintWeekTemplateCommand>
    {
        public CreatePrintWeekTemplateCommandHandler(TypiconDBContext dbContext, CollectorSerializerRoot serializerRoot) : base(dbContext, serializerRoot) { }

        public async Task<Result> ExecuteAsync(CreatePrintWeekTemplateCommand command)
        {
            return await base.ExecuteAsync(command);
        }

        protected override PrintWeekTemplate Create(CreateRuleCommandBase<PrintWeekTemplate> command, TypiconVersion typiconVersion)
        {
            var c = command as CreatePrintWeekTemplateCommand;

            var entity = new PrintWeekTemplate()
            {
                DaysPerPage = c.DaysPerPage,
                PrintFile = c.PrintFile,
                PrintFileName = c.PrintFileName,
                TypiconVersion = typiconVersion
            };

            return entity;
        }
    }
}
