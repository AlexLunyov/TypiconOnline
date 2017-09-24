using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.Tests.Rules.Days
{
    [TestFixture]
    public class YmnosGroupTest
    {
        [Test]
        public void YmnosGroupTest_InvalidIhos()
        {
            string xmlString = @"<YmnosGroup ihos=""9"">
				                    <ymnos>
					                    <stihos>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, Стих 1-й стихиры.</cs-ru>
					                    </stihos>
					                    <text>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, 1 стихира.</cs-ru>
					                    </text>
				                    </ymnos>
				                    <ymnos>
					                    <stihos>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, Стих 2-й стихиры.</cs-ru>
					                    </stihos>
					                    <text>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, 2 стихира.</cs-ru>
					                    </text>
				                    </ymnos>
				                    <ymnos>
					                    <stihos>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, Стих 3-й стихиры.</cs-ru>
					                    </stihos>
					                    <text>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, 3 стихира.</cs-ru>
					                    </text>
				                    </ymnos>
			                    </YmnosGroup>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            XmlNode root = xmlDoc.DocumentElement;

            YmnosGroup element = new YmnosGroup(root);// (xmlDoc.FirstChild);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void YmnosGroupTest_ValidIhos()
        {
            string xmlString = @"<YmnosGroup ihos=""4"">
				                    <prosomoion>
					                    <cs-ru>Ки́ими похва́льными.</cs-ru>
				                    </prosomoion>
                                    <ymnos>
					                    <stihos>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, Стих 1-й стихиры.</cs-ru>
					                    </stihos>
					                    <text>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, 1 стихира.</cs-ru>
					                    </text>
				                    </ymnos>
				                    <ymnos>
					                    <stihos>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, Стих 2-й стихиры.</cs-ru>
					                    </stihos>
					                    <text>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, 2 стихира.</cs-ru>
					                    </text>
				                    </ymnos>
				                    <ymnos>
					                    <stihos>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, Стих 3-й стихиры.</cs-ru>
					                    </stihos>
					                    <text>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, 3 стихира.</cs-ru>
					                    </text>
				                    </ymnos>
			                    </YmnosGroup>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            XmlNode root = xmlDoc.DocumentElement;

            YmnosGroup element = new YmnosGroup(root);// (xmlDoc.FirstChild);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void YmnosGroupTest_Equals()
        {
            YmnosGroup element1 = new YmnosGroup()
            {
                Annotation = new ItemText(@"<annotation>
					                            <cs-ru>Феофа́ново</cs-ru>
				                            </annotation>"),
                Ihos = 1,
                Prosomoion = new Prosomoion()
            };

            YmnosGroup element2 = new YmnosGroup()
            {
                Annotation = new ItemText(@"<annotation>
					                            <cs-ru>Феофа́ново</cs-ru>
				                            </annotation>"),
                Ihos = 2,
                Prosomoion = new Prosomoion()
            };

            Assert.IsFalse(element1.Equals(element2));
        }

        [Test]
        public void YmnosGroupTest_Equals2()
        {
            YmnosGroup element1 = new YmnosGroup()
            {
                //Annotation = new ItemText(@"<annotation>
					           //                 <cs-ru>Феофа́ново</cs-ru>
				            //                </annotation>"),
                Ihos = 1,
                //Prosomoion = new Prosomoion()
            };

            YmnosGroup element2 = new YmnosGroup()
            {
                //Annotation = new ItemText(@"<annotation>
					           //                 <cs-ru>Феофа́ново</cs-ru>
				            //                </annotation>"),
                Ihos = 1,
                //Prosomoion = new Prosomoion()
            };

            Assert.IsTrue(element1.Equals(element2));
        }

        [Test]
        public void YmnosGroupTest_Equals3()
        {
            YmnosGroup element1 = new YmnosGroup()
            {
                Annotation = new ItemText(@"<annotation>
                                 <cs-ru>Феофа́ново</cs-ru>
                                </annotation>"),
                Ihos = 1,
                //Prosomoion = new Prosomoion()
            };

            YmnosGroup element2 = new YmnosGroup()
            {
                Annotation = new ItemText(@"<annotation>
                                 <cs-cs>Феофа́ново</cs-cs>
                                </annotation>"),
                Ihos = 1,
                //Prosomoion = new Prosomoion()
            };

            Assert.IsFalse(element1.Equals(element2));
        }

        [Test]
        public void YmnosGroupTest_Deserialization()
        {
            string xmlString = @"<YmnosGroup ihos=""4"">
				                    <prosomoion>
					                    <cs-ru>Ки́ими похва́льными.</cs-ru>
				                    </prosomoion>
                                    <ymnos>
					                    <stihos>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, Стих 1-й стихиры.</cs-ru>
					                    </stihos>
					                    <text>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, 1 стихира.</cs-ru>
					                    </text>
				                    </ymnos>
				                    <ymnos>
					                    <stihos>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, Стих 2-й стихиры.</cs-ru>
					                    </stihos>
					                    <text>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, 2 стихира.</cs-ru>
					                    </text>
				                    </ymnos>
				                    <ymnos>
					                    <stihos>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, Стих 3-й стихиры.</cs-ru>
					                    </stihos>
					                    <text>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, 3 стихира.</cs-ru>
					                    </text>
				                    </ymnos>
			                    </YmnosGroup>";

            TypiconSerializer ser = new TypiconSerializer();
            YmnosGroup element = ser.Deserialize<YmnosGroup>(xmlString);

            Assert.AreEqual(element.Ymnis.Count, 3);
            Assert.AreEqual(element.Ihos, 4);
            Assert.IsNull(element.Annotation);
        }

        [Test]
        public void YmnosGroupTest_Serialization()
        {
            string xmlString = @"<YmnosGroup ihos=""4"">
				                    <prosomoion>
					                    <cs-ru>Ки́ими похва́льными.</cs-ru>
				                    </prosomoion>
                                    <ymnos>
					                    <stihos>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, Стих 1-й стихиры.</cs-ru>
					                    </stihos>
					                    <text>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, 1 стихира.</cs-ru>
					                    </text>
				                    </ymnos>
				                    <ymnos>
					                    <stihos>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, Стих 2-й стихиры.</cs-ru>
					                    </stihos>
					                    <text>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, 2 стихира.</cs-ru>
					                    </text>
				                    </ymnos>
				                    <ymnos>
					                    <stihos>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, Стих 3-й стихиры.</cs-ru>
					                    </stihos>
					                    <text>
						                    <cs-ru>[item] [sign] Малая вечерня. На стиховне, 3 стихира.</cs-ru>
					                    </text>
				                    </ymnos>
			                    </YmnosGroup>";

            TypiconSerializer ser = new TypiconSerializer();
            YmnosGroup element = ser.Deserialize<YmnosGroup>(xmlString);

            string result = ser.Serialize(element);

            Assert.Pass(result);
        }
    }
}
