﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.AppServices.OutputFiltering;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.AppServices.Implementations
{
    public class HtmlScheduleDayViewer : IScheduleDayViewer<string>
    {
        const string XSLT_CONFIG = "ScheduleDayViewer_XsltFile";
        readonly ITypiconSerializer xmlSerializer;
        readonly IConfigurationRepository configRepo;

        public HtmlScheduleDayViewer(ITypiconSerializer xmlSerializer, IConfigurationRepository configRepo)
        {
            this.xmlSerializer = xmlSerializer ?? throw new ArgumentNullException("xmlSerializer in HtmlScheduleDayViewer");
            this.configRepo = configRepo ?? throw new ArgumentNullException("configRepo in HtmlScheduleDayViewer");
        }

        public string Execute(FilteredOutputDay day)
        {
            //находим путь к файлу xslt
            var xsltFilePath = configRepo.GetConfigurationValue(XSLT_CONFIG);

            var xslt = new XslCompiledTransform();
            xslt.Load(xsltFilePath);

            //получаем xml-строку
            var xml = xmlSerializer.Serialize(day);

            var xmlreader = XmlReader.Create(new StringReader(xml));

            var stringWriter = new StringWriter();
            var xmlWriter = XmlWriter.Create(stringWriter);

            //выполняем xslt-трансформацию
            xslt.Transform(xmlreader, xmlWriter);

            return stringWriter.ToString();
        }

        public string Execute(FilteredOutputWorship viewModel)
        {
            var day = new FilteredOutputDay();
            day.Worships.Add(viewModel);

            return Execute(day);
        }
    }
}
