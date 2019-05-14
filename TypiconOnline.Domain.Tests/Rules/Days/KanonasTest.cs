using NUnit.Framework;
using TypiconOnline.Domain.Serialization;
using System.IO;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.AppServices.Migration;

namespace TypiconOnline.Domain.Tests.Rules.Days
{
    [TestFixture]
    public class KanonasTest
    {
        [Test]
        public void KanonasTest_Deserialization()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            var reader = new FileReader(folderPath);
            string xml = reader.Read("KanonasTest.xml");

            TypiconSerializer ser = new TypiconSerializer();
            Kanonas element = ser.Deserialize<Kanonas>(xml);

            Assert.IsTrue(element.IsValid);
            Assert.NotNull(element.Acrostic.FirstOrDefault("cs-cs"));
            Assert.NotNull(element.Annotation.FirstOrDefault("cs-cs"));
            Assert.NotNull(element.Stihos.FirstOrDefault("cs-ru"));
            Assert.AreEqual(element.Odes.Count, 8);
            //Assert.NotNull(element.Odes[7].Irmos);
            //Assert.NotNull(element.Odes[7].Katavasia);

            //Assert.AreEqual(element.Sedalen.Theotokion.Count, 2);
            //Assert.NotNull(element.Exapostilarion.Ymnis[0].Text.FirstOrDefault("cs-ru"));
            //Assert.AreEqual(element.Evangelion.Count, 2);
        }
    }
}
