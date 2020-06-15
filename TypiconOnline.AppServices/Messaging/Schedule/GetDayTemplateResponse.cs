using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Extensions;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class GetDayTemplateResponse
    {
        public GetDayTemplateResponse(IEnumerable<OpenXmlElement> elements, MainDocumentPart docPart)
        {
            XmlElements = elements ?? throw new ArgumentNullException(nameof(elements));
            DocumentPart = docPart ?? throw new ArgumentNullException(nameof(docPart));
        }

        public IEnumerable<OpenXmlElement> XmlElements { get; }
        public MainDocumentPart DocumentPart { get; }

        public GetDayTemplateResponse Clone() => new GetDayTemplateResponse(XmlElements.DeepClone(), DocumentPart);
    }
}
