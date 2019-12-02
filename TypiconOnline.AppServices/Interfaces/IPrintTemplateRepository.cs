using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IPrintTemplateRepository : IDisposable
    {
        IEnumerable<OpenXmlElement> GetDayTemplate(int typiconId, int number);
    }
}
