using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Books
{
    public class DeleteOktoikhDayCommand : ICommand
    {
        public DeleteOktoikhDayCommand(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }
}
