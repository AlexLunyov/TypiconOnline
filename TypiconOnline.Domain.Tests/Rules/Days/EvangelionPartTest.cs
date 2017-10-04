using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Rules.Days;

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
