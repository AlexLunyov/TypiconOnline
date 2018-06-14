using NUnit.Framework;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Tests.Rules.Days
{
    [TestFixture]
    public class ProsomoionTest
    {
        [Test]
        public void Prosomoion_Self()
        {
            string xmlString = @"<prosomoion self=""true""/>";

            TypiconSerializer ser = new TypiconSerializer();
            Prosomoion element = ser.Deserialize<Prosomoion>(xmlString);

            Assert.IsTrue(element.Self);
        }

        [Test]
        public void Prosomoion_Self_Serialize()
        {
            string xmlString = @"<prosomoion self=""true""/>";

            TypiconSerializer ser = new TypiconSerializer();
            Prosomoion element = ser.Deserialize<Prosomoion>(xmlString);

            Assert.Pass(ser.Serialize(element));
        }

        [Test]
        public void Prosomoion_Deserialize()
        {
            string xmlString = @"<prosomoion>
                                    <item language=""cs-ru"">Все́ отло́жше</item>
                                  </prosomoion>";

            TypiconSerializer ser = new TypiconSerializer();
            Prosomoion element = ser.Deserialize<Prosomoion>(xmlString);

            Assert.IsFalse(element.Self);
            Assert.IsFalse(element.IsEmpty);
        }

        [Test]
        public void Prosomoion_Serialize()
        {
            string xmlString = @"<prosomoion>
                                    <item language=""cs-ru"">Все́ отло́жше</item>
                                  </prosomoion>";

            TypiconSerializer ser = new TypiconSerializer();
            Prosomoion element = ser.Deserialize<Prosomoion>(xmlString);
            element.Self = true;

            xmlString = ser.Serialize(element);

            element = ser.Deserialize<Prosomoion>(xmlString);

            Assert.IsTrue(element.Self);
        }
    }
}
