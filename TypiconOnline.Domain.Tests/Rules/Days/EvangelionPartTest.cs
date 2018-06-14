using NUnit.Framework;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Tests.Rules.Days
{
    [TestFixture]
    public class EvangelionPartTest
    {
        [Test]
        public void EvangelionPart_Deserialize()
        {
            string xmlString = @"<EvangelionPart number=""43"" bookname=""Мф""/>";
            TypiconSerializer ser = new TypiconSerializer();
            EvangelionPart element = ser.Deserialize<EvangelionPart>(xmlString);

            Assert.AreEqual(element.Number, 43);
            Assert.AreEqual(element.BookName, EvangelionBook.Мф);
        }
    }
}
