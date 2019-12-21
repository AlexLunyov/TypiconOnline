﻿using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditTypiconCommand : ICommand
    {
        public EditTypiconCommand(int id, ItemText name, ItemText description, bool isTemplate, string defaultLanguage)
        {
            Name = name;
            Description = description;
            IsTemplate = isTemplate;
            DefaultLanguage = defaultLanguage;
            Id = id;
        }
        public int Id { get; }
        public ItemText Name { get; }
        public ItemText Description { get; }
        public bool IsTemplate { get; }
        public string DefaultLanguage { get; }
    }
}
