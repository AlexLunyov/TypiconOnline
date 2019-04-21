using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditTypiconCommand : ICommand
    {
        public EditTypiconCommand(int id, ItemText name, string defaultLanguage)
        {
            Name = name;
            DefaultLanguage = defaultLanguage;
            Id = id;
        }
        public int Id { get; }
        public ItemText Name { get; }
        public string DefaultLanguage { get; }
    }
}
