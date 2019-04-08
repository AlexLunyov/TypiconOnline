using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IOutputFormFactory
    {
        (OutputForm OutputForm, OutputDay Day) Create(OutputFormCreateRequest req);
    }
}
