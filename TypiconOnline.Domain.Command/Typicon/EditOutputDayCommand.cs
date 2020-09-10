using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.AuthorizeKeys;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditOutputDayCommand: ICommand, IHasAuthorizedAccess
    {
        public EditOutputDayCommand(int id, ItemTextStyled name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public int Id { get; set; }
        public ItemTextStyled Name { get; set; }

        public IAuthorizeKey Key => new OutputDayCanEditKey(Id);
    }
}
