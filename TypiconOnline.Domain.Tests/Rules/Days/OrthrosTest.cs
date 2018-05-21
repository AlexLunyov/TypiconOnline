using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Days
{
    [TestFixture]
    public class OrthrosTest
    {
        [Test]
        public void OrthrosTest_DeserializeEvangelion()
        {
            #region xml
            string xmlString = @"<orthros>
                                    <evangelion>
			                            <part number=""43"" bookname=""Мф""/>
                                        <part number=""36"" bookname=""Ин""/>
		                            </evangelion>
                                 </orthros>";

            #endregion
            TypiconSerializer ser = new TypiconSerializer();
            Orthros element = ser.Deserialize<Orthros>(xmlString);

            Assert.NotNull(element.Evangelion);
            Assert.AreEqual(element.Evangelion.Count, 2);
        }

        [Test]
        public void OrthrosTest_DeserializeFull()
        {
            string xml = TestDataXmlReader.GetXmlString("OrthrosTest_DeserializeFull.xml");

            TypiconSerializer ser = new TypiconSerializer();
            Orthros element = ser.Deserialize<Orthros>(xml);

            Assert.NotNull(element.SedalenKathisma1);
            Assert.NotNull(element.SedalenKathisma2);
            Assert.NotNull(element.SedalenKathisma3);
            Assert.NotNull(element.SedalenPolyeleos);
            
            Assert.AreEqual(element.Megalynarion.Count, 1);
            Assert.AreEqual(element.Eclogarion.Count, 17);
            Assert.AreEqual(element.Prokeimenon.Ihos, 3);

            Assert.NotNull(element.Evangelion);
            Assert.AreEqual(element.Evangelion.Count, 1);

            Assert.AreEqual(element.Sticheron50.Ymnis.Count, 1);
            Assert.AreEqual(element.Kanones.Count, 3);
            Assert.NotNull(element.SedalenKanonas);

            Assert.AreEqual(1, element.Kontakia.Count);
            Assert.AreEqual(1, element.Exapostilarion.Ymnis.Count);
            Assert.AreEqual(1, element.Exapostilarion.Theotokion.Count);

            Assert.NotNull(element.Ainoi);
            Assert.IsNull(element.Aposticha);
        }
    }
}
