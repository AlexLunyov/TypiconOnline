using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;

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
						                        <cs-ru>[item] [sign] Господи воззвах, 1 стихира </cs-ru>
					                        </text>
				                        </ymnos>
				                        <ymnos>
					                        <text>
						                        <cs-ru>[item] [sign] Господи воззвах, 2 стихира </cs-ru>
					                        </text>
				                        </ymnos>
				                        <ymnos>
					                        <text>
						                        <cs-ru>[item] [sign] Господи воззвах, 3 стихира </cs-ru>
					                        </text>
				                        </ymnos>
			                        </group>
			                        <doxastichon ihos=""8"">
				                        <ymnos>
					                        <text>
						                        <cs-ru>[item] [sign] Господи воззвах, Славник</cs-ru>
					                        </text>
				                        </ymnos>
			                        </doxastichon>
			                        <theotokion ihos=""8"" kind=""stavros"">
				                        <ymnos>
					                        <text>
						                        <cs-ru>[item] [sign] Господи воззвах, Крестобогородичен</cs-ru>
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
						                        <cs-ru>[item] [sign] Господи воззвах, 1 стихира </cs-ru>
					                        </text>
				                        </ymnos>
				                        <ymnos>
					                        <text>
						                        <cs-ru>[item] [sign] Господи воззвах, 2 стихира </cs-ru>
					                        </text>
				                        </ymnos>
				                        <ymnos>
					                        <text>
						                        <cs-ru>[item] [sign] Господи воззвах, 3 стихира </cs-ru>
					                        </text>
				                        </ymnos>
			                        </group>
			                        <doxastichon ihos=""8"">
				                        <ymnos>
					                        <text>
						                        <cs-ru>[item] [sign] Господи воззвах, Славник</cs-ru>
					                        </text>
				                        </ymnos>
			                        </doxastichon>
			                        <theotokion ihos=""8"" kind=""stavros"">
				                        <ymnos>
					                        <text>
						                        <cs-ru>[item] [sign] Господи воззвах, Крестобогородичен</cs-ru>
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
