﻿using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Books
{
    public class CreateTriodionDayCommand : ICommand
    {
        public CreateTriodionDayCommand(int daysFromEaster
            , ItemTextStyled name
            , ItemText shortName
            , bool isCelebrating
            , bool useFullName
            , string definition)
        {
            Name = name;
            DaysFromEaster = daysFromEaster;
            Name = name;
            ShortName = shortName;
            IsCelebrating = isCelebrating;
            UseFullName = useFullName;
            Definition = definition;
        }
        public int DaysFromEaster { get; }
        public ItemTextStyled Name { get; }
        public ItemText ShortName { get; }
        public bool IsCelebrating { get; }
        public bool UseFullName { get; }
        public string Definition { get; }
    }
}
