using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class DeleteTypiconClaimCommand : ICommand
    {
        public DeleteTypiconClaimCommand(ClaimsPrincipal user, int id)
        {
            User = user;
            Id = id;
        }

        public ClaimsPrincipal User { get; set; }
        public int Id { get; set; }
    }
}
