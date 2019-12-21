using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class RejectTypiconClaimCommand : ICommand
    {
        public RejectTypiconClaimCommand(int claimId, string message)
        {
            ClaimId = claimId;
            ResultMessage = message;
        }
        public int ClaimId { get; }
        public string ResultMessage { get; set; }
    }
}
