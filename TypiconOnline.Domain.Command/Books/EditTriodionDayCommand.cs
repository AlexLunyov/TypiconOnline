using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Books
{
    public class EditTriodionDayCommand : ICommand
    {
        public EditTriodionDayCommand(int id
            , int daysFromEaster
            , string name
            , string shortName
            , bool isCelebrating
            , bool useFullName
            , string definition) 
        {
            Id = id;
            Name = name;
            DaysFromEaster = daysFromEaster;
            Name = name;
            ShortName = shortName;
            IsCelebrating = isCelebrating;
            UseFullName = useFullName;
            Definition = definition;
        }
        public int Id { get; }
        public int DaysFromEaster { get; }
        public string Name { get; }
        public string ShortName { get; }
        public bool IsCelebrating { get; }
        public bool UseFullName { get; }
        public string Definition { get; }
    }
}
