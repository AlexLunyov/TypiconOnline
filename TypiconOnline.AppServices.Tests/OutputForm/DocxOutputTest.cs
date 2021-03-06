﻿using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TypiconOnline.AppServices.Viewers;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.AppServices.OutputFiltering;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.OutputForm
{
    [TestFixture]
    public class DocxOutputTest
    {
        [Test]
        public void Docx_Serialize()
        {
            var date = new DateTime(2019, 2, 1);
            var queryProcessor = QueryProcessorFactory.Create();

            var week = queryProcessor.Process(new OutputWeekQuery(1, date, new OutputFilter() { Language = "cs-ru" }));

            var ser = new TypiconSerializer();

            var xml = ser.Serialize(week.Value);

            Assert.IsNotNull(xml);
        }

        [Test]
        public void Docx_FromFile()
        {
            var xml = TestDataXmlReader.GetXmlString("DocxViewerWeek.xml");

            var queryProcessor = new Mock<IQueryProcessor>();

            queryProcessor.Setup(c => c.Process(It.IsAny<PrintWeekTemplateQuery>()))
                .Returns(GetPrintWeekTemplate());

            queryProcessor.Setup(c => c.Process(It.IsAny<PrintDayTemplateQuery>()))
                .Returns<PrintDayTemplateQuery>(q => GetDayWeekTemplate(q.Number));

            var ser = new TypiconSerializer();
            var week = ser.Deserialize<FilteredOutputWeek>(xml);

            var viewer = new DocxFromOutputTemplatesWeekViewer(queryProcessor.Object);

            var result = viewer.Execute(1, week);

            File.WriteAllBytes(GetPath($"PrintWeekTemplate.docx"), result.Value.Content);

            Assert.IsTrue(result.Success);
        }

        private PrintDayTemplate GetDayWeekTemplate(int? number)
        {
            if (!number.HasValue)
            {
                number = 0;
            }
            byte[] arr = File.ReadAllBytes(GetPath($"{number}.docx"));

            return new PrintDayTemplate()
            {
                Number = number.Value,
                PrintFile = arr
            };
        }

        private PrintWeekTemplate GetPrintWeekTemplate()
        {
            byte[] arr = File.ReadAllBytes(GetPath("week.docx"));

            return new PrintWeekTemplate()
            {
                DaysPerPage = 4,
                PrintFile = arr
            };
        }

        private string GetPath(string fileName) => Path.Combine(TestFileCommander.ExecFolder, "Data", "OutputForm", fileName);
    }
}
