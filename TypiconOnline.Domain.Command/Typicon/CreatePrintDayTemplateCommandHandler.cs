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
    public class CreatePrintDayTemplateCommandHandler : CreateRuleCommandHandlerBase<PrintDayTemplate>, ICommandHandler<CreatePrintDayTemplateCommand>
    {
        public CreatePrintDayTemplateCommandHandler(TypiconDBContext dbContext, CollectorSerializerRoot serializerRoot) : base(dbContext, serializerRoot) { }

        public async Task<Result> ExecuteAsync(CreatePrintDayTemplateCommand command)
        {
            return await base.ExecuteAsync(command);
        }

        protected override PrintDayTemplate Create(CreateRuleCommandBase<PrintDayTemplate> command, TypiconVersion typiconVersion)
        {
            var c = command as CreatePrintDayTemplateCommand;

            var entity = new PrintDayTemplate()
            {
                Name = c.Name,
                Number = c.Number,
                Icon = c.Icon,
                IsRed = c.IsRed,
                PrintFile = c.PrintFile,
                PrintFileName = c.PrintFileName,
                TypiconVersion = typiconVersion
            };

            return entity;
        }
    }
}
