using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Rules.Days;

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
            #region xml
            string xmlString = @"<orthros>
		                            <sed_1kathisma>
			                            <group ihos=""1"">
				                            <prosomoion>
					                            <item language=""cs-ru"">Ли́к Ангельский.</item>
				                            </prosomoion>
				                            <ymnos>
					                            <text>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Седален по 1-й кафизме. Тропарь 1</item>
					                            </text>
				                            </ymnos>
			                            </group>
			                            <doxastichon ihos=""3"">
				                            <ymnos>
					                            <text>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Седален по 1-й кафизме. Слава</item>
					                            </text>
				                            </ymnos>
			                            </doxastichon>
			                            <theotokion ihos=""5"">
				                            <ymnos>
					                            <text>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Седален по 1-й кафизме. Богородичен</item>
					                            </text>
				                            </ymnos>
			                            </theotokion>
		                            </sed_1kathisma>
		                            <sed_2kathisma>
			                            <group ihos=""3"">
				                            <prosomoion>
					                            <item language=""cs-ru"">Красоте́ де́вства.</item>
				                            </prosomoion>
				                            <ymnos>
					                            <text>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Седален по 2-й кафизме. Тропарь 1</item>
					                            </text>
				                            </ymnos>
			                            </group>
			                            <doxastichon ihos=""3"">
				                            <ymnos>
					                            <text>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Седален по 2-й кафизме. Слава</item>
					                            </text>
				                            </ymnos>
			                            </doxastichon>
			                            <theotokion ihos=""3"">
				                            <ymnos>
					                            <text>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Седален по 2-й кафизме. Богородичен</item>
					                            </text>
				                            </ymnos>
			                            </theotokion>
		                            </sed_2kathisma>
		                            <sed_polyeleos>
			                            <group ihos=""3"">
				                            <prosomoion>
					                            <item language=""cs-ru"">Красоте́ де́вства.</item>
				                            </prosomoion>
				                            <ymnos>
					                            <text>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Седален по полиелее. Тропарь 1</item>
					                            </text>
				                            </ymnos>
			                            </group>
			                            <doxastichon ihos=""3"">
				                            <ymnos>
					                            <text>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Седален по полиелее. Слава</item>
					                            </text>
				                            </ymnos>
			                            </doxastichon>
			                            <theotokion ihos=""3"">
				                            <ymnos>
					                            <text>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Седален по полиелее. Богородичен</item>
					                            </text>
				                            </ymnos>
			                            </theotokion>
		                            </sed_polyeleos>
		                            <megalynarion>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Величание.</item>
			                            </stihos>
		                            </megalynarion>
		                            <eclogarion>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Псалом избранный. Стих 1</item>
			                            </stihos>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Псалом избранный. Стих 2</item>
			                            </stihos>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Псалом избранный. Стих 3</item>
			                            </stihos>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Псалом избранный. Стих 4</item>
			                            </stihos>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Псалом избранный. Стих 5</item>
			                            </stihos>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Псалом избранный. Стих 6</item>
			                            </stihos>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Псалом избранный. Стих 7</item>
			                            </stihos>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Псалом избранный. Стих 8</item>
			                            </stihos>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Псалом избранный. Стих 9</item>
			                            </stihos>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Псалом избранный. Стих 10</item>
			                            </stihos>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Псалом избранный. Стих 11</item>
			                            </stihos>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Псалом избранный. Стих 12</item>
			                            </stihos>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Псалом избранный. Стих 13</item>
			                            </stihos>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Псалом избранный. Стих 14</item>
			                            </stihos>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Псалом избранный. Стих 15</item>
			                            </stihos>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Псалом избранный. Стих 16</item>
			                            </stihos>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Псалом избранный. Стих 17</item>
			                            </stihos>
		                            </eclogarion>
		                            <prokeimenon ihos=""3"">
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Утреня. Полиелей. Прокимен</item>
			                            </stihos>
			                            <stihos>
				                            <item language=""cs-ru"">[item] [sign] Утреня. Полиелей. Прокимен. Стих</item>
			                            </stihos>
		                            </prokeimenon>
		                            <evangelion>
			                            <part number=""43"" bookname=""Мф""/>
		                            </evangelion>
		                            <sticheron_50 ihos=""6"">
			                            <ymnos>
				                            <text>
					                            <item language=""cs-ru"">[item] [sign] Утреня. Стихира по 50-м псалме</item>
				                            </text>
			                            </ymnos>
		                            </sticheron_50>
		                            <kanones>
			                            <kanonas ihos=""8"">
				                            <acrostic>
					                            <item language=""cs-ru"">Цве́т тя пою́ па́стырей и му́чеников.</item>
				                            </acrostic>
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Припев.</item>
				                            </stihos>
				                            <odes>
					                            <odi number=""1"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Ирмос 1-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 1 1-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 2 1-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 3 1-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь Богородичен 1-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""3"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Ирмос 3-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 1 3-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 2 3-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 3 3-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь Богородичен 3-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""4"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Ирмос 4-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 1 4-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 2 4-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 3 4-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь Богородичен 4-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""5"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Ирмос 5-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 1 5-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 2 5-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 3 5-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь Богородичен 5-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""6"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Ирмос 6-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 1 6-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 2 6-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 3 6-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь Богородичен 6-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""7"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Ирмос 7-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 1 7-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 2 7-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 3 7-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь Богородичен 7-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""8"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Ирмос 8-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 1 8-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 2 8-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 3 8-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь Богородичен 8-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""9"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Ирмос 9-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 1 9-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 2 9-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь 3 9-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Тропарь Богородичен 9-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
				                            </odes>
				                            <sedalen>
					                            <group ihos=""1"">
						                            <prosomoion>
							                            <item language=""cs-ru"">Ли́к Ангельски</item>
						                            </prosomoion>
						                            <ymnos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Седален по 3-й песни.</item>
							                            </text>
						                            </ymnos>
					                            </group>
					                            <doxastichon ihos=""4"">
						                            <ymnos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Седален по 3-й песни. Слава</item>
							                            </text>
						                            </ymnos>
					                            </doxastichon>
					                            <theotokion ihos=""4"">
						                            <ymnos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Седален по 3-й песни. Богородичен</item>
							                            </text>
						                            </ymnos>
					                            </theotokion>
					                            <theotokion kind=""stavros"" ihos=""4"">
						                            <ymnos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Седален по 3-й песни. Крестобогородичен</item>
							                            </text>
						                            </ymnos>
					                            </theotokion>
				                            </sedalen>
				                            <kontakion ihos=""4"">
					                            <prosomoion>
						                            <item language=""cs-ru"">Вознесы́йся</item>
					                            </prosomoion>
					                            <ymnos>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Кондак.</item>
					                            </ymnos>
					                            <ikos>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Икос.</item>
					                            </ikos>
				                            </kontakion>
				                            <exapostilarion>
					                            <ymnos>
						                            <prosomoion>
							                            <item language=""cs-ru"">Не́бо звезда́ми</item>
						                            </prosomoion>
						                            <text>
							                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Эксапостиларий. Тропарь 1.</item>
						                            </text>
					                            </ymnos>
					                            <theotokion>
						                            <text>
							                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 1-й. Эксапостиларий. Богородичен.</item>
						                            </text>
					                            </theotokion>
				                            </exapostilarion>
			                            </kanonas>
			                            <kanonas ihos=""6"">
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Припев.</item>
				                            </stihos>
				                            <odes>
					                            <odi number=""1"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Ирмос 1-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 1 1-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 2 1-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 3 1-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь Богородичен 1-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""3"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Ирмос 3-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 1 3-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 2 3-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 3 3-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь Богородичен 3-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""4"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Ирмос 4-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 1 4-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 2 4-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 3 4-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь Богородичен 4-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""5"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Ирмос 5-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 1 5-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 2 5-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 3 5-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь Богородичен 5-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""6"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Ирмос 6-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 1 6-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 2 6-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 3 6-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь Богородичен 6-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""7"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Ирмос 7-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 1 7-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 2 7-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 3 7-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь Богородичен 7-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""8"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Ирмос 8-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 1 8-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 2 8-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 3 8-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь Богородичен 8-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""9"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Ирмос 9-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 1 9-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 2 9-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь 3 9-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Тропарь Богородичен 9-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
				                            </odes>
				                            <sedalen>
					                            <group ihos=""1"">
						                            <prosomoion>
							                            <item language=""cs-ru"">Ли́к Ангельски</item>
						                            </prosomoion>
						                            <ymnos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Седален по 3-й песни.</item>
							                            </text>
						                            </ymnos>
					                            </group>
					                            <theotokion ihos=""1"">
						                            <ymnos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Седален по 3-й песни. Богородичен</item>
							                            </text>
						                            </ymnos>
					                            </theotokion>
					                            <theotokion kind=""stavros"" ihos=""1"">
						                            <ymnos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Седален по 3-й песни. Крестобогородичен</item>
							                            </text>
						                            </ymnos>
					                            </theotokion>
				                            </sedalen>
				                            <kontakion ihos=""4"">
					                            <prosomoion>
						                            <item language=""cs-ru"">Вознесы́йся</item>
					                            </prosomoion>
					                            <ymnos>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Кондак.</item>
					                            </ymnos>
					                            <ikos>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Икос.</item>
					                            </ikos>
				                            </kontakion>
				                            <exapostilarion>
					                            <ymnos>
						                            <prosomoion>
							                            <item language=""cs-ru"">Не́бо звезда́ми</item>
						                            </prosomoion>
						                            <text>
							                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Эксапостиларий. Тропарь 1.</item>
						                            </text>
					                            </ymnos>
					                            <theotokion>
						                            <text>
							                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 2-й. Эксапостиларий. Богородичен.</item>
						                            </text>
					                            </theotokion>
				                            </exapostilarion>
			                            </kanonas>
			                            <kanonas ihos=""6"">
				                            <stihos>
					                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Припев.</item>
				                            </stihos>
				                            <odes>
					                            <odi number=""1"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Ирмос 1-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 1 1-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 2 1-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 3 1-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь Богородичен 1-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""3"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Ирмос 3-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 1 3-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 2 3-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 3 3-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь Богородичен 3-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""4"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Ирмос 4-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 1 4-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 2 4-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 3 4-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь Богородичен 4-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""5"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Ирмос 5-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 1 5-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 2 5-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 3 5-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь Богородичен 5-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""6"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Ирмос 6-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 1 6-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 2 6-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 3 6-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь Богородичен 6-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""7"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Ирмос 7-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 1 7-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 2 7-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 3 7-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь Богородичен 7-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""8"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Ирмос 8-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 1 8-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 2 8-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 3 8-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь Богородичен 8-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
					                            <odi number=""9"">
						                            <irmos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Ирмос 9-й песни.</item>
							                            </text>
						                            </irmos>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 1 9-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 2 9-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь 3 9-й песни.</item>
							                            </text>
						                            </troparion>
						                            <troparion kind=""theotokion"">
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Тропарь Богородичен 9-й песни.</item>
							                            </text>
						                            </troparion>
					                            </odi>
				                            </odes>
				                            <sedalen>
					                            <group ihos=""1"">
						                            <prosomoion>
							                            <item language=""cs-ru"">Ли́к Ангельски</item>
						                            </prosomoion>
						                            <ymnos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Седален по 3-й песни.</item>
							                            </text>
						                            </ymnos>
					                            </group>
					                            <theotokion ihos=""1"">
						                            <ymnos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Седален по 3-й песни. Богородичен</item>
							                            </text>
						                            </ymnos>
					                            </theotokion>
					                            <theotokion kind=""stavros"" ihos=""4"">
						                            <ymnos>
							                            <text>
								                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Седален по 3-й песни. Крестобогородичен</item>
							                            </text>
						                            </ymnos>
					                            </theotokion>
				                            </sedalen>
				                            <kontakion ihos=""4"">
					                            <prosomoion>
						                            <item language=""cs-ru"">Вознесы́йся</item>
					                            </prosomoion>
					                            <ymnos>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Кондак.</item>
					                            </ymnos>
					                            <ikos>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Икос.</item>
					                            </ikos>
				                            </kontakion>
				                            <exapostilarion>
					                            <ymnos>
						                            <prosomoion>
							                            <item language=""cs-ru"">Не́бо звезда́ми</item>
						                            </prosomoion>
						                            <text>
							                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Эксапостиларий. Тропарь 1.</item>
						                            </text>
					                            </ymnos>
					                            <theotokion>
						                            <text>
							                            <item language=""cs-ru"">[item] [sign] Утреня. Канон 3-й. Эксапостиларий. Богородичен.</item>
						                            </text>
					                            </theotokion>
				                            </exapostilarion>
			                            </kanonas>
		                            </kanones>
		                            <ainoi>
			                            <group ihos=""8"">
				                            <ymnos>
					                            <text>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Стихиры на Хвалитех. 1 стихира</item>
					                            </text>
				                            </ymnos>
				                            <ymnos>
					                            <text>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Стихиры на Хвалитех. 2 стихира</item>
					                            </text>
				                            </ymnos>
				                            <ymnos>
					                            <text>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Стихиры на Хвалитех. 3 стихира</item>
					                            </text>
				                            </ymnos>
				                            <ymnos>
					                            <text>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Стихиры на Хвалитех. 4 стихира</item>
					                            </text>
				                            </ymnos>
			                            </group>
			                            <doxastichon ihos=""2"">
				                            <ymnos>
					                            <text>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Стихиры на Хвалитех. Славник</item>
					                            </text>
				                            </ymnos>
			                            </doxastichon>
			                            <theotokion ihos=""2"">
				                            <ymnos>
					                            <text>
						                            <item language=""cs-ru"">[item] [sign] Утреня. Стихиры на Хвалитех. Богородичен</item>
					                            </text>
				                            </ymnos>
			                            </theotokion>
		                            </ainoi>
	                            </orthros>";

            #endregion
            TypiconSerializer ser = new TypiconSerializer();
            Orthros element = ser.Deserialize<Orthros>(xmlString);

            Assert.NotNull(element.SedalenKathisma1);
            Assert.NotNull(element.SedalenKathisma2);
            Assert.IsNull(element.SedalenKathisma3);
            Assert.NotNull(element.SedalenPolyeleos);
            Assert.AreEqual(element.Megalynarion.Count, 1);
            Assert.AreEqual(element.Eclogarion.Count, 17);
            Assert.AreEqual(element.Prokeimenon.Ihos, 3);

            Assert.NotNull(element.Evangelion);
            Assert.AreEqual(element.Evangelion.Count, 1);

            Assert.AreEqual(element.Sticheron50.Ymnis.Count, 1);
            Assert.AreEqual(element.Kanones.Count, 3);
            Assert.NotNull(element.Ainoi);
            Assert.IsNull(element.Aposticha);
        }
    }
}
