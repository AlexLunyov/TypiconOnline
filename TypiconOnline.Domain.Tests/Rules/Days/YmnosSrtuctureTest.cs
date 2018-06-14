using NUnit.Framework;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Tests.Rules.Days
{
    [TestFixture]
    public class YmnosSrtuctureTest
    {
        [Test]
        public void YmnosSrtucture_Deserialization()
        {
            string xmlString = @"<YmnosStructure>
			                        <group ihos=""1"">
				                        <ymnos>
					                        <text>
						                        <item language=""cs-ru"">[item] [sign] Господи воззвах, 1 стихира </item>
					                        </text>
				                        </ymnos>
				                        <ymnos>
					                        <text>
						                        <item language=""cs-ru"">[item] [sign] Господи воззвах, 2 стихира </item>
					                        </text>
				                        </ymnos>
				                        <ymnos>
					                        <text>
						                        <item language=""cs-ru"">[item] [sign] Господи воззвах, 3 стихира </item>
					                        </text>
				                        </ymnos>
			                        </group>
			                        <doxastichon ihos=""8"">
				                        <ymnos>
					                        <text>
						                        <item language=""cs-ru"">[item] [sign] Господи воззвах, Славник</item>
					                        </text>
				                        </ymnos>
			                        </doxastichon>
			                        <theotokion ihos=""8"" kind=""stavros"">
				                        <ymnos>
					                        <text>
						                        <item language=""cs-ru"">[item] [sign] Господи воззвах, Крестобогородичен</item>
					                        </text>
				                        </ymnos>
			                        </theotokion>
		                        </YmnosStructure>";

            TypiconSerializer ser = new TypiconSerializer();
            YmnosStructure element = ser.Deserialize<YmnosStructure>(xmlString);

            Assert.AreEqual(element.Groups.Count, 1);
            Assert.AreEqual(element.Groups[0].Ymnis.Count, 3);
            Assert.IsNotNull(element.Doxastichon);
            Assert.AreEqual(element.Doxastichon.Ihos, 8);
            Assert.AreEqual(element.Theotokion[0].Kind, YmnosGroupKind.Stavros);
        }

        [Test]
        public void YmnosSrtucture_Serialization()
        {
            string xmlString = @"<YmnosStructure>
			                        <group ihos=""1"">
				                        <ymnos>
					                        <text>
						                        <item language=""cs-ru"">[item] [sign] Господи воззвах, 1 стихира </item>
					                        </text>
				                        </ymnos>
				                        <ymnos>
					                        <text>
						                        <item language=""cs-ru"">[item] [sign] Господи воззвах, 2 стихира </item>
					                        </text>
				                        </ymnos>
				                        <ymnos>
					                        <text>
						                        <item language=""cs-ru"">[item] [sign] Господи воззвах, 3 стихира </item>
					                        </text>
				                        </ymnos>
			                        </group>
			                        <doxastichon ihos=""8"">
				                        <ymnos>
					                        <text>
						                        <item language=""cs-ru"">[item] [sign] Господи воззвах, Славник</item>
					                        </text>
				                        </ymnos>
			                        </doxastichon>
			                        <theotokion ihos=""8"" kind=""stavros"">
				                        <ymnos>
					                        <text>
						                        <item language=""cs-ru"">[item] [sign] Господи воззвах, Крестобогородичен</item>
					                        </text>
				                        </ymnos>
			                        </theotokion>
		                        </YmnosStructure>";

            TypiconSerializer ser = new TypiconSerializer();
            YmnosStructure element = ser.Deserialize<YmnosStructure>(xmlString);

            string result = ser.Serialize(element);

            Assert.Pass(result);
        }
    }
}
