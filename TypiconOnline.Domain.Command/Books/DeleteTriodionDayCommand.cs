using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Books
{
    public class DeleteTriodionDayCommand : ICommand
    {
        public DeleteTriodionDayCommand(int worshipId)
        {
            WorshipId = worshipId;
        }
        public int WorshipId { get; }
    }
}
