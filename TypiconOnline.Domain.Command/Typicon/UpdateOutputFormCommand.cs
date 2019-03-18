using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class UpdateOutputFormCommand : ICommand
    {
        public UpdateOutputFormCommand(OutputForm outputForm)
        {
            OutputForm = outputForm ?? throw new ArgumentNullException(nameof(outputForm));
        }

        public OutputForm OutputForm { get; }
    }
}
