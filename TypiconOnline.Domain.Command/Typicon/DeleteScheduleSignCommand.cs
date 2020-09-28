using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class DeleteScheduleSignCommand : TypiconByRuleCommandBase<Sign>
    {
        public DeleteScheduleSignCommand(int id) : base(id)
        {
        }
    }
}
