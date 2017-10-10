using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.AppServices.Implementations;
using System.IO;

namespace TypiconOnline.Domain.Tests.Days
{
    [TestFixture]
    public class EsperinosTest
    {
        [Test]
        public void EsperinosTest_Serialization()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.GetXml("Esperinos.xml");

            TypiconSerializer ser = new TypiconSerializer();

            Esperinos esperinos = ser.Deserialize<Esperinos>(xml);

            Assert.IsNotNull(esperinos);
            Assert.AreEqual(esperinos.Kekragaria.Groups[0].Ihos, 1);
            Assert.AreEqual(esperinos.Paroimies.Count, 2);
        }
    }
}
