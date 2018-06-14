using NUnit.Framework;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Tests.Rules.Days
{
    [TestFixture]
    public class PsalmStihosTest
    {
        [Test]
        public void PsalmStihos_Serialize()
        {
            PsalmStihos stihos = new PsalmStihos() { Number = 1 };

            stihos.AddOrUpdate("cs-ru", "Стих 1");

            TypiconSerializer ser = new TypiconSerializer();

            string xmlString = ser.Serialize(stihos);

            PsalmStihos element = ser.Deserialize<PsalmStihos>(xmlString);

            Assert.IsNotNull(element);
            Assert.AreEqual(1, element.Number);
            Assert.Pass(xmlString);
        }

        [Test]
        public void PsalmStihos_Serialize_WithoutNumber()
        {
            PsalmStihos stihos = new PsalmStihos();

            stihos.AddOrUpdate("cs-ru", "Стих 1" );

            TypiconSerializer ser = new TypiconSerializer();

            string xmlString = ser.Serialize(stihos);

            PsalmStihos element = ser.Deserialize<PsalmStihos>(xmlString);

            Assert.IsNotNull(element);
            Assert.AreEqual(0, element.Number);
            Assert.Pass(xmlString);
        }
    }
}
