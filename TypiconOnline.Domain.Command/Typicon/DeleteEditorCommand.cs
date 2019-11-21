using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class DeleteEditorCommand : ICommand
    {
        public DeleteEditorCommand(int typiconId, int userId)
        {
            TypiconId = typiconId;
            UserId = userId;
        }
        public int TypiconId { get; }
        public int UserId { get; }
    }
}
