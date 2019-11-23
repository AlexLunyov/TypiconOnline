using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class DeleteTypiconCommand : ICommand
    {
        public DeleteTypiconCommand(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }
}
