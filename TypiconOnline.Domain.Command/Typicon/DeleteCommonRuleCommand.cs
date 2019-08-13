using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class DeleteCommonRuleCommand : DeleteRuleCommandBase<CommonRule>
    {
        public DeleteCommonRuleCommand(int id) : base(id)
        {
        }
    }
}
