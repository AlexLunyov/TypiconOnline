using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class CreateOutputFormResponse
    {
        public CreateOutputFormResponse(OutputForm form, OutputDay day)
        {
            Form = form ?? throw new ArgumentNullException(nameof(form));
            Day = day ?? throw new ArgumentNullException(nameof(day));
        }
        public OutputForm Form { get; }
        public OutputDay Day { get; }
    }
}
