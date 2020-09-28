using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.AuthorizeKeys;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class TypiconCommandBase : ICommand, IHasAuthorizedAccess
    {
        public TypiconCommandBase(int typiconId)
        {
            TypiconId = typiconId;
            Key = new TypiconEntityCanEditKey(TypiconId);
        }

        public int TypiconId { get; }

        public IAuthorizeKey Key { get; }
    }
}
