using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class DeletePrintDayTemplateCommand : DeleteRuleCommandBase<PrintDayTemplate>
    {
        public DeletePrintDayTemplateCommand(int id) : base(id)
        {
        }
    }
}
