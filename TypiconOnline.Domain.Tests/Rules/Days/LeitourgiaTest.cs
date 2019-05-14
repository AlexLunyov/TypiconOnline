using NUnit.Framework;
using TypiconOnline.Domain.Serialization;
using System.IO;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.AppServices.Migration;

namespace TypiconOnline.Domain.Tests.Rules.Days
{
    [TestFixture]
    public class LeitourgiaTest
    {
        [Test]
        public void Leitourgia_Deserialization()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            var reader = new FileReader(folderPath);
            string xml = reader.Read("LeitourgiaTest.xml");

            TypiconSerializer ser = new TypiconSerializer();
            Leitourgia element = ser.Deserialize<Leitourgia>(xml);

            Assert.AreEqual(element.Makarismi.Links.Count, 2);
            Assert.AreEqual(element.Makarismi.Ymnis.Ymnis.Count, 6);
            Assert.AreEqual(element.Prokeimeni[0].Ihos, 7);
            Assert.AreEqual(element.Apostles[0].Number, 213);
            Assert.AreEqual(element.Alleluias[0].Stihoi.Count, 2);
            Assert.AreEqual(element.Evangelion[0].BookName, EvangelionBook.Мф);
            Assert.AreEqual(2, element.Kinoniki.Count);
        }
    }
}
