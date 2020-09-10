using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.AuthorizeKeys;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditOutputWorshipCommand : ICommand, IHasAuthorizedAccess
    {
        public EditOutputWorshipCommand(int id, ItemTextStyled name, ItemTextStyled addName, string time)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            AdditionalName = addName ?? throw new ArgumentNullException(nameof(addName));
            Time = time ?? throw new ArgumentNullException(nameof(time));
        }

        public int Id { get; set; }
        public string Time { get; set; }
        public virtual ItemTextStyled Name { get; set; }
        public virtual ItemTextStyled AdditionalName { get; set; }

        public IAuthorizeKey Key => new OutputWorshipCanEditKey(Id);
    }
}
