using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var result = stihos.AddElement("cs-ru", "Стих 1");

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

            var result = stihos.AddElement("cs-ru", "Стих 1");

            TypiconSerializer ser = new TypiconSerializer();

            string xmlString = ser.Serialize(stihos);

            PsalmStihos element = ser.Deserialize<PsalmStihos>(xmlString);

            Assert.IsNotNull(element);
            Assert.AreEqual(null, element.Number);
            Assert.Pass(xmlString);
        }
    }
}
