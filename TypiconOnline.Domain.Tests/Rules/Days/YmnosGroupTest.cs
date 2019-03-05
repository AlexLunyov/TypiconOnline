using NUnit.Framework;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.ItemTypes;
using System.IO;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Books.Elements;

namespace TypiconOnline.Domain.Tests.Rules.Days
{
    [TestFixture]
    public class YmnosGroupTest
    {
        [Test]
        public void YmnosGroupTest_ValidIhos()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.Read("YmnosGroupTest.xml");

            TypiconSerializer ser = new TypiconSerializer();
            YmnosGroup element = ser.Deserialize<YmnosGroup>(xml);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void YmnosGroupTest_Equals()
        {
            var element1 = new YmnosGroup()
            {
                Ihos = 1,
                Prosomoion = new Prosomoion(),
                Annotation = new ItemText()
            };

            element1.Annotation.AddOrUpdate("cs-ru", "Феофа́ново");

            var element2 = new YmnosGroup()
            {
                Ihos = 2,
                Prosomoion = new Prosomoion(),
                Annotation = new ItemText()
            };

            element2.Annotation.AddOrUpdate("cs-ru", "Феофа́ново");

            Assert.IsFalse(element1.Equals(element2));
        }

        [Test]
        public void YmnosGroupTest_Equals2()
        {
            YmnosGroup element1 = new YmnosGroup()
            {
                //Annotation = new ItemText(@"<annotation>
					           //                 <item language=""cs-ru"">Феофа́ново</item>
				            //                </annotation>"),
                Ihos = 1,
                //Prosomoion = new Prosomoion()
            };

            YmnosGroup element2 = new YmnosGroup()
            {
                //Annotation = new ItemText(@"<annotation>
					           //                 <item language=""cs-ru"">Феофа́ново</item>
				            //                </annotation>"),
                Ihos = 1,
                //Prosomoion = new Prosomoion()
            };

            Assert.IsTrue(element1.Equals(element2));
        }

        [Test]
        public void YmnosGroupTest_Equals3()
        {
            var element1 = new YmnosGroup()
            {
                Ihos = 1,
                Annotation = new ItemText()
            };
            element1.Annotation.AddOrUpdate("cs-ru", "Феофа́ново");

            var element2 = new YmnosGroup()
            {
                Ihos = 1,
                Annotation = new ItemText()
            };
            element2.Annotation.AddOrUpdate("cs-cs", "Феофа́ново");

            Assert.IsFalse(element1.Equals(element2));
        }

        [Test]
        public void YmnosGroupTest_Deserialization()
        {
            string xmlString = @"<YmnosGroup ihos=""4"">
				                    <prosomoion>
					                    <item language=""cs-ru"">Ки́ими похва́льными.</item>
				                    </prosomoion>
                                    <ymnos>
					                    <stihos>
						                    <item language=""cs-ru"">[item] [sign] Малая вечерня. На стиховне, Стих 1-й стихиры.</item>
					                    </stihos>
					                    <text>
						                    <item language=""cs-ru"">[item] [sign] Малая вечерня. На стиховне, 1 стихира.</item>
					                    </text>
				                    </ymnos>
				                    <ymnos>
					                    <stihos>
						                    <item language=""cs-ru"">[item] [sign] Малая вечерня. На стиховне, Стих 2-й стихиры.</item>
					                    </stihos>
					                    <text>
						                    <item language=""cs-ru"">[item] [sign] Малая вечерня. На стиховне, 2 стихира.</item>
					                    </text>
				                    </ymnos>
				                    <ymnos>
					                    <stihos>
						                    <item language=""cs-ru"">[item] [sign] Малая вечерня. На стиховне, Стих 3-й стихиры.</item>
					                    </stihos>
					                    <text>
						                    <item language=""cs-ru"">[item] [sign] Малая вечерня. На стиховне, 3 стихира.</item>
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
					                    <item language=""cs-ru"">Ки́ими похва́льными.</item>
				                    </prosomoion>
                                    <ymnos>
					                    <stihos>
						                    <item language=""cs-ru"">[item] [sign] Малая вечерня. На стиховне, Стих 1-й стихиры.</item>
					                    </stihos>
					                    <text>
						                    <item language=""cs-ru"">[item] [sign] Малая вечерня. На стиховне, 1 стихира.</item>
					                    </text>
				                    </ymnos>
				                    <ymnos>
					                    <stihos>
						                    <item language=""cs-ru"">[item] [sign] Малая вечерня. На стиховне, Стих 2-й стихиры.</item>
					                    </stihos>
					                    <text>
						                    <item language=""cs-ru"">[item] [sign] Малая вечерня. На стиховне, 2 стихира.</item>
					                    </text>
				                    </ymnos>
				                    <ymnos>
					                    <stihos>
						                    <item language=""cs-ru"">[item] [sign] Малая вечерня. На стиховне, Стих 3-й стихиры.</item>
					                    </stihos>
					                    <text>
						                    <item language=""cs-ru"">[item] [sign] Малая вечерня. На стиховне, 3 стихира.</item>
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
