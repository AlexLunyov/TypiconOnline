using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class AddModifiedYearCommand : ICommand
    {
        public AddModifiedYearCommand(ModifiedYear modifiedYear)
        {
            ModifiedYear = modifiedYear ?? throw new ArgumentNullException(nameof(modifiedYear));
        }

        public ModifiedYear ModifiedYear { get; }
    }
}
