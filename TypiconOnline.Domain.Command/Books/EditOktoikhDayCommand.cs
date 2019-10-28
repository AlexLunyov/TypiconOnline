using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Books
{
    public class EditOktoikhDayCommand : ICommand
    {
        public EditOktoikhDayCommand(int id
            //, int ihos
            //, DayOfWeek dayOfWeek
            , string definition)
        {
            Id = id;
            //Ihos = ihos;
            //DayOfWeek = dayOfWeek;
            Definition = definition;
        }
        public int Id { get; }
        //public int Ihos { get; }
        //public DayOfWeek DayOfWeek { get; }
        public string Definition { get; }
    }
}
