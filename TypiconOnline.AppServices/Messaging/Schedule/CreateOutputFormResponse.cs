using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class CreateOutputFormResponse
    {
        public CreateOutputFormResponse(OutputForm form, OutputDay day, IEnumerable<BusinessConstraint> constraints)
        {
            Form = form ?? throw new ArgumentNullException(nameof(form));
            Day = day ?? throw new ArgumentNullException(nameof(day));

            if (constraints != null)
            {
                BrokenConstraints = constraints;
            }
        }
        public OutputForm Form { get; }
        public OutputDay Day { get; }
        public IEnumerable<BusinessConstraint> BrokenConstraints { get; }
    }
}
