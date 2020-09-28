using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class AddScheduleTriodionCommand : TypiconCommandBase
    {
        public AddScheduleTriodionCommand(int typiconId, int ruleId) : base(typiconId)
        {
            RuleId = ruleId;
        }

        public int RuleId { get; }
    }
}
