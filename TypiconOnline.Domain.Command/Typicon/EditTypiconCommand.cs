using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditTypiconCommand : ICommand
    {
        public EditTypiconCommand(int id, string name, string description, bool isTemplate, string defaultLanguage)
        {
            Name = name;
            Description = description;
            IsTemplate = isTemplate;
            DefaultLanguage = defaultLanguage;
            Id = id;
        }
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public bool IsTemplate { get; }
        public string DefaultLanguage { get; }
    }
}
