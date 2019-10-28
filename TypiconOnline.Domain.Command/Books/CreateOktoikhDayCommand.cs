using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Books
{
    public class CreateOktoikhDayCommand : ICommand
    {
        public CreateOktoikhDayCommand(int ihos
            , DayOfWeek dayOfWeek
            , string definition)
        {
            Ihos = ihos;
            DayOfWeek = dayOfWeek;
            Definition = definition;
        }
        public int Ihos { get; }
        public DayOfWeek DayOfWeek { get; }
        public string Definition { get; }
    }
}
