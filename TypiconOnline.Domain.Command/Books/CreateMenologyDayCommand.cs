﻿using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Books
{
    public class CreateMenologyDayCommand : ICommand
    {
        public CreateMenologyDayCommand(DateTime? leapDate
            , string name
            , string shortName
            , bool isCelebrating
            , bool useFullName
            , string definition)
        {
            Name = name;
            LeapDate = leapDate;
            Name = name;
            ShortName = shortName;
            IsCelebrating = isCelebrating;
            UseFullName = useFullName;
            Definition = definition;
        }
        public DateTime? LeapDate { get; }
        public string Name { get; }
        public string ShortName { get; }
        public bool IsCelebrating { get; }
        public bool UseFullName { get; }
        public string Definition { get; }
    }
}
