using NUnit.Framework;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.AppServices.Implementations;
using System.IO;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.AppServices.Migration;

namespace TypiconOnline.Domain.Tests.Days
{
    [TestFixture]
    public class EsperinosTest
    {
        [Test]
        public void EsperinosTest_Serialization()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            var reader = new FileReader(folderPath);
            string xml = reader.Read("Esperinos.xml");

            TypiconSerializer ser = new TypiconSerializer();

            Esperinos esperinos = ser.Deserialize<Esperinos>(xml);

            Assert.IsNotNull(esperinos);
            Assert.AreEqual(esperinos.Kekragaria.Groups[0].Ihos, 1);
            Assert.AreEqual(esperinos.Paroimies.Count, 2);
        }
    }
}
