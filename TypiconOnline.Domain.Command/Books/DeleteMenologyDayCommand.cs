using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Books
{
    public class DeleteMenologyDayCommand : ICommand
    {
        public DeleteMenologyDayCommand(int worshipId)
        {
            WorshipId = worshipId;
        }
        public int WorshipId { get; }
    }
}
