using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Rules.Days;
using System.IO;
using TypiconOnline.AppServices.Implementations;

namespace TypiconOnline.Domain.Tests.Rules.Days
{
    [TestFixture]
    public class LeitourgiaTest
    {
        [Test]
        public void Leitourgia_Deserialization()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.GetXml("LeitourgiaTest.xml");

            TypiconSerializer ser = new TypiconSerializer();
            Leitourgia element = ser.Deserialize<Leitourgia>(xml);

            Assert.AreEqual(element.Makarismi.Count, 2);
            Assert.AreEqual(element.Prokeimenon.Ihos, 7);
            Assert.AreEqual(element.Apostles[0].Number, 213);
            Assert.AreEqual(element.Alleluia.Stihoi.Count, 2);
            Assert.AreEqual(element.Evangelion[0].BookName, EvangelionBook.Мф);
            Assert.NotNull(element.Kinonik);
        }
    }
}
