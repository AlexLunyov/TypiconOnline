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
    public class LeitourgiaTest
    {
        [Test]
        public void Leitourgia_Deserialization()
        {
            string xmlString = @"<Leitourgia>
                                    <makarismi>
			                            <odi number=""3"" count=""4""></odi>
			                            <odi number=""6"" count=""4""></odi>
		                            </makarismi>
		                            <prokeimenon ihos=""7"">
			                            <stihos>
				                            <cs-ru>[item] [sign] Честна́ пред Го́сподем / сме́рть преподо́бных Его́.</cs-ru>
			                            </stihos>
			                            <stihos>
				                            <cs-ru>[item] [sign] Что́ возда́м Го́сподеви о все́х, я́же воздаде́ ми?</cs-ru>
			                            </stihos>
		                            </prokeimenon>
		                            <apostles>
			                            <part number=""213""/>
		                            </apostles>
		                            <alleluia ihos=""6"">
			                            <stihos>
				                            <cs-ru>[item] [sign] Блаже́н му́ж, боя́йся Го́спода, в за́поведех Его́ восхо́щет зело́.</cs-ru>
			                            </stihos>
			                            <stihos>
				                            <cs-ru>[item] [sign] Си́льно на земли́ бу́дет се́мя его́.</cs-ru>
			                            </stihos>
		                            </alleluia>
		                            <evangelion>
			                            <part number=""10"" bookname=""Мф""/>
		                            </evangelion>
		                            <kinonik>
			                            <cs-ru>[item] [sign] Ра́дуйтеся пра́веднии о Го́споде, [пра́вым подоба́ет похвала́].</cs-ru>
		                            </kinonik>
	                            </Leitourgia>";

            TypiconSerializer ser = new TypiconSerializer();
            Leitourgia element = ser.Deserialize<Leitourgia>(xmlString);

            Assert.AreEqual(element.Makarismi.Count, 2);
            Assert.AreEqual(element.Prokeimenon.Ihos, 7);
            Assert.AreEqual(element.Apostles[0].Number, 213);
            Assert.AreEqual(element.Alleluia.Stihoi.Count, 2);
            Assert.AreEqual(element.Evangelion[0].BookName, EvangelionBook.Мф);
            Assert.NotNull(element.Kinonik);
        }
    }
}
