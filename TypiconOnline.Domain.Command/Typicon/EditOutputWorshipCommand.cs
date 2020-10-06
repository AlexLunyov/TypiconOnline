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
        public EditOutputWorshipCommand(int id
            , string name
            , string addName
            , string time
            , TextStyle nameStyle
            , TextStyle additionalNameStyle
            )
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            AdditionalName = addName;
            NameStyle = nameStyle;
            AdditionalNameStyle = additionalNameStyle;
            Time = time;
        }

        public int Id { get; set; }
        public string Time { get; set; }
        public virtual string Name { get; set; }
        public virtual TextStyle NameStyle { get; set; }
        public virtual string AdditionalName { get; set; }
        public virtual TextStyle AdditionalNameStyle { get; set; }

        public IAuthorizeKey Key => new OutputWorshipCanEditKey(Id);
    }
}
