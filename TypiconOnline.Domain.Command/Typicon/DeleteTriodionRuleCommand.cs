using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class DeleteTriodionRuleCommand : DeleteRuleCommandBase<TriodionRule>
    {
        public DeleteTriodionRuleCommand(int id) : base(id)
        {
        }
    }
}
