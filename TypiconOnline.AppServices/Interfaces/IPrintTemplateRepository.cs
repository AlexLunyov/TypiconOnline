using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Messaging.Schedule;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IPrintTemplateRepository : IDisposable
    {
        GetDayTemplateResponse GetDayTemplate(int typiconId, int number);
    }
}
