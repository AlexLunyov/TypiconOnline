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
    public class KanonasTest
    {
        [Test]
        public void KanonasTest_Deserialization()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.Read("KanonasTest.xml");

            TypiconSerializer ser = new TypiconSerializer();
            Kanonas element = ser.Deserialize<Kanonas>(xml);

            Assert.IsTrue(element.IsValid);
            Assert.NotNull(element.Acrostic["cs-cs"]);
            Assert.NotNull(element.Annotation["cs-cs"]);
            Assert.NotNull(element.Stihos["cs-ru"]);
            Assert.AreEqual(element.Odes.Count, 8);
            Assert.NotNull(element.Odes[7].Katavasia["cs-ru"]);

            Assert.AreEqual(element.Sedalen.Theotokion.Count, 2);
            Assert.NotNull(element.Exapostilarion.Ymnis[0].Text["cs-ru"]);
            //Assert.AreEqual(element.Evangelion.Count, 2);
        }
    }
}
