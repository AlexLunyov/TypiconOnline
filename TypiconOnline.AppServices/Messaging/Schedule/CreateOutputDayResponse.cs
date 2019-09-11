using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon.Output;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class CreateOutputDayResponse
    {
        public CreateOutputDayResponse(OutputDay day, IEnumerable<BusinessConstraint> constraints)
        {
            Day = day ?? throw new ArgumentNullException(nameof(day));

            if (constraints != null)
            {
                BrokenConstraints = constraints;
            }
        }
        public OutputDay Day { get; }
        public IEnumerable<BusinessConstraint> BrokenConstraints { get; }
    }
}
