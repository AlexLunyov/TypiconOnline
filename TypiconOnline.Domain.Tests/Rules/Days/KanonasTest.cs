using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.Tests.Rules.Days
{
    [TestFixture]
    public class KanonasTest
    {
        [Test]
        public void KanonasTest_Deserialization()
        {
            #region xml
            string xmlString = @"<Kanonas ihos=""6"">
			                        <stihos>
				                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Припев.</cs-ru>
			                        </stihos>
			                        <odes>
				                        <odi number=""1"">
					                        <irmos>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Ирмос 1-й песни.</cs-ru>
						                        </text>
					                        </irmos>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 1 1-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 2 1-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 3 1-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion kind=""theotokion"">
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь Богородичен 1-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <katavasia>
						                        <cs-ru>[item] [sign] Утреня. Канон 2-й. катавасия 1-й песни.</cs-ru>
					                        </katavasia>
				                        </odi>
				                        <odi number=""3"">
					                        <irmos>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Ирмос 3-й песни.</cs-ru>
						                        </text>
					                        </irmos>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 1 3-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 2 3-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 3 3-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion kind=""theotokion"">
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь Богородичен 3-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <katavasia>
						                        <cs-ru>[item] [sign] Утреня. Канон 2-й. катавасия 3-й песни.</cs-ru>
					                        </katavasia>
				                        </odi>
				                        <odi number=""4"">
					                        <irmos>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Ирмос 4-й песни.</cs-ru>
						                        </text>
					                        </irmos>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 1 4-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 2 4-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 3 4-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion kind=""theotokion"">
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь Богородичен 4-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <katavasia>
						                        <cs-ru>[item] [sign] Утреня. Канон 2-й. катавасия 4-й песни.</cs-ru>
					                        </katavasia>
				                        </odi>
				                        <odi number=""5"">
					                        <irmos>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Ирмос 5-й песни.</cs-ru>
						                        </text>
					                        </irmos>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 1 5-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 2 5-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 3 5-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion kind=""theotokion"">
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь Богородичен 5-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <katavasia>
						                        <cs-ru>[item] [sign] Утреня. Канон 2-й. катавасия 5-й песни.</cs-ru>
					                        </katavasia>
				                        </odi>
				                        <odi number=""6"">
					                        <irmos>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Ирмос 6-й песни.</cs-ru>
						                        </text>
					                        </irmos>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 1 6-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 2 6-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 3 6-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion kind=""theotokion"">
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь Богородичен 6-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <katavasia>
						                        <cs-ru>[item] [sign] Утреня. Канон 2-й. катавасия 6-й песни.</cs-ru>
					                        </katavasia>
				                        </odi>
				                        <odi number=""7"">
					                        <irmos>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Ирмос 7-й песни.</cs-ru>
						                        </text>
					                        </irmos>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 1 7-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 2 7-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 3 7-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion kind=""theotokion"">
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь Богородичен 7-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <katavasia>
						                        <cs-ru>[item] [sign] Утреня. Канон 2-й. катавасия 7-й песни.</cs-ru>
					                        </katavasia>
				                        </odi>
				                        <odi number=""8"">
					                        <irmos>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Ирмос 8-й песни.</cs-ru>
						                        </text>
					                        </irmos>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 1 8-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 2 8-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 3 8-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion kind=""theotokion"">
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь Богородичен 8-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <katavasia>
						                        <cs-ru>[item] [sign] Утреня. Канон 2-й. катавасия 8-й песни.</cs-ru>
					                        </katavasia>
				                        </odi>
				                        <odi number=""9"">
					                        <irmos>
						                        <stihos>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Припев 9-й песни.</cs-ru>
						                        </stihos>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Ирмос 9-й песни.</cs-ru>
						                        </text>
					                        </irmos>
					                        <troparion>
						                        <stihos>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Припев 2 9-й песни.</cs-ru>
						                        </stihos>
						                        <stihos>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Припев 3 9-й песни.</cs-ru>
						                        </stihos>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 1 9-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion>
						                        <stihos>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Припев 4 9-й песни.</cs-ru>
						                        </stihos>
						                        <stihos>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Припев 5 9-й песни.</cs-ru>
						                        </stihos>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 2 9-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion>
						                        <stihos>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Припев 6 9-й песни.</cs-ru>
						                        </stihos>
						                        <stihos>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Припев 7 9-й песни.</cs-ru>
						                        </stihos>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 3 9-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <troparion>
						                        <stihos>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Припев 8 9-й песни.</cs-ru>
						                        </stihos>
						                        <stihos>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Припев 9 9-й песни.</cs-ru>
						                        </stihos>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Тропарь 4 9-й песни.</cs-ru>
						                        </text>
					                        </troparion>
					                        <katavasia>
						                        <cs-ru>[item] [sign] Утреня. Канон 2-й. катавасия 9-й песни.</cs-ru>
					                        </katavasia>
				                        </odi>
			                        </odes>
			                        <sedalen>
				                        <group ihos=""1"">
					                        <prosomoion>
						                        <cs-ru>Ли́к Ангельски</cs-ru>
					                        </prosomoion>
					                        <ymnos>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Седален по 3-й песни.</cs-ru>
						                        </text>
					                        </ymnos>
				                        </group>
				                        <theotokion ihos=""1"">
					                        <ymnos>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Седален по 3-й песни. Богородичен</cs-ru>
						                        </text>
					                        </ymnos>
				                        </theotokion>
				                        <theotokion ihos=""1"" kind=""stavros"">
					                        <ymnos>
						                        <text>
							                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Седален по 3-й песни. Крестобогородичен</cs-ru>
						                        </text>
					                        </ymnos>
				                        </theotokion>
			                        </sedalen>
			                        <kontakion ihos=""4"">
				                        <prosomoion>
					                        <cs-ru>Вознесы́йся</cs-ru>
				                        </prosomoion>
				                        <ymnos>
					                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Икос.</cs-ru>
				                        </ymnos>
				                        <ikos>
					                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Кондак.</cs-ru>
				                        </ikos>
			                        </kontakion>
			                        <exapostilarion>
				                        <ymnos>
					                        <prosomoion>
						                        <cs-ru>Не́бо звезда́ми</cs-ru>
					                        </prosomoion>
					                        <text>
						                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Эксапостиларий. Тропарь 1.</cs-ru>
					                        </text>
				                        </ymnos>
				                        <theotokion>
					                        <text>
						                        <cs-ru>[item] [sign] Утреня. Канон 2-й. Эксапостиларий. Богородичен.</cs-ru>
					                        </text>
				                        </theotokion>
			                        </exapostilarion>
		                        </Kanonas>";
            #endregion

            TypiconSerializer ser = new TypiconSerializer();
            Kanonas element = ser.Deserialize<Kanonas>(xmlString);

            Assert.IsTrue(element.IsValid);
            Assert.NotNull(element.Stihos["cs-ru"]);
            Assert.AreEqual(element.Odes.Count, 8);
            Assert.NotNull(element.Odes[7].Katavasia["cs-ru"]);

            Assert.AreEqual(element.Sedalen.Theotokion.Count, 2);
            Assert.NotNull(element.Exapostilarion.Ymnis[0].Text["cs-ru"]);
            //Assert.AreEqual(element.Evangelion.Count, 2);
        }
    }
}
