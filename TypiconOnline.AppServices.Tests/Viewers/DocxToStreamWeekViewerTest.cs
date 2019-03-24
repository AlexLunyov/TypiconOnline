using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Viewers;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Viewers
{
    [TestFixture]
    class DocxToStreamWeekViewerTest
    {
        [Test]
        public void DocxToStreamWeekViewer_Test()
        {

            var query = DataQueryProcessorFactory.Create();

            var mock = new Mock<IConfigurationRepository>();
            mock.Setup(c => c.GetConfigurationValue(It.IsAny<string>()))
                .Returns($@"{TestContext.CurrentContext.TestDirectory}\Data\BigTemplate.docx");

            var viewer = new DocxToStreamWeekViewer(query, mock.Object);

            var week = new ScheduleWeek()
            {
                Name = new ItemTextUnit("cs-ru", "Первая седмица Великого поста"),
                Days = new List<ScheduleDay>()
                {
                    new ScheduleDay()
                    {
                        Date = DateTime.Today,
                        Name = new ItemTextUnit("cs-ru", "Торжество Православия"),
                        Worships = new List<WorshipRuleViewModel>()
                        {
                            new WorshipRuleViewModel()
                            {
                                Time = "06.00",
                                Name = new ItemTextUnit("cs-ru", "Полунощница")
                            }
                        }
                    }
                }
            };

            var result = viewer.Execute(week);

            //using (Stream file = File.Create($@"{TestContext.CurrentContext.TestDirectory}\Data\Output.docx"))
            //{
            //    file.Write(result.Value.Content, 0, result.Value.Content.Length);
            //}

            Assert.True(result.Value.Content.Length > 0, "Пустой файл");
        }

    }
}
