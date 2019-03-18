using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IOutputFormFactory
    {
        (OutputForm OutputForm, ScheduleDay Day) Create(OutputFormCreateRequest req);
    }
}
