using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.Tests.Rules.Days
{
    [TestFixture]
    public class MikrosEsperinosTest
    {
        [Test]
        public void MikrosEsperinos_Creature()
        {
            string xmlString = @"<mikrosesperinos>
			                        <kekragaria>
				                        <group ihos=""1"">
					                        <prosomoion>
						                        <cs-ru>Небе́сных чино́в.</cs-ru>
					                        </prosomoion>
					                        <ymnos>
						                        <text>
							                        <cs-ru>Кре́ст возно́сится, / и де́мони прогоня́ются, / разбо́йник Еде́ма у́бо врата́ отверза́ет, / сме́рть умерщвля́ется и ны́не пуста́ яви́ся, / Христо́с велича́ется. / Те́м весели́теся, вси́ земноро́днии, / кля́тва разруши́ся.</cs-ru>
						                        </text>
					                        </ymnos>
					                        <ymnos>
						                        <text>
							                        <cs-ru>Прииди́те, боголюби́вии вси́, / Кре́ст честны́й возноси́мый ви́дяще, / возвели́чим ку́пно и сла́ву дади́м / еди́ному Изба́вителю и Бо́гу, взыва́юще: / Распны́йся на дре́ве кре́стнем, / не пре́зри моля́щихся на́с.</cs-ru>
						                        </text>
					                        </ymnos>
					                        <ymnos>
						                        <text>
							                        <cs-ru>Го́ресть дре́вле ослажда́я, Моисе́й / изба́ви Изра́иля, о́бразом Кре́ст пропису́я; / мы́ же, сего́ вси́ боже́ственно, ве́рнии, / та́инственно вообража́юще в сердца́х на́ших всегда́, / спаса́емся держа́вою его́.</cs-ru>
						                        </text>
					                        </ymnos>
				                        </group>
				                        <theotokion ihos=""6"">
					                        <prosomoion self=""true""/>
					                        <ymnos>
						                        <text>
							                        <cs-ru>Дне́сь са́д живо́тный от земны́х незаходи́мых не́др происхо́дит, на не́м пригвожде́ннаго Христа́ известву́ет Воскресе́ние, и воздвиза́емый рука́ми свяще́нническими, Того́ на небеса́ возвеща́ет возноше́ние, и́мже на́ше смеше́ние от е́же на зе́млю паде́ния на небесе́х жи́тельствует. Те́мже благода́рственне возопии́м: Го́споди, вознесы́йся на не́м и те́м совозне́с на́с, небе́сныя Твоея́ ра́дости сподо́би, я́ко Человеколю́бец.</cs-ru>
						                        </text>
					                        </ymnos>
				                        </theotokion>
			                        </kekragaria>
			                        <aposticha>
				                        <group ihos=""2"">
					                        <prosomoion>
						                        <cs-ru>До́ме Евфра́фов.</cs-ru>
					                        </prosomoion>
					                        <ymnos>
						                        <text>
							                        <cs-ru>Водо́ю боготво́рною / и Кро́вию Твое́ю, Сло́ве, / све́тло Це́рковь украша́ется, / я́ко неве́ста, / Креста́ сла́ву пою́щи.</cs-ru>
						                        </text>
					                        </ymnos>
					                        <ymnos>
						                        <stihos>
							                        <cs-ru>Возноси́те Го́спода Бо́га на́шего / и покланя́йтеся подно́жию но́гу Его́, я́ко свя́то е́сть.</cs-ru>
						                        </stihos>
						                        <text>
							                        <cs-ru>Копие́ со кресто́м, / гво́зди и ина́я, / и́миже живоно́сное / Христо́во пригвозди́ся Те́ло, / вознося́ще, поклони́мся.</cs-ru>
						                        </text>
					                        </ymnos>
					                        <ymnos>
						                        <stihos>
							                        <cs-ru>Бо́г же, Ца́рь на́ш пре́жде ве́ка, / соде́ла спасе́ние посреде́ земли́.</cs-ru>
						                        </stihos>
						                        <text>
							                        <cs-ru>Егда́ Амали́ка / Моисе́й побежда́ше, / на высоту́ ру́це име́я / крестоявле́нно, / образова́ше Христо́ву Стра́сть пречи́стую.</cs-ru>
						                        </text>
					                        </ymnos>
				                        </group>
				                        <theotokion ihos=""6"">
					                        <ymnos>
						                        <text>
							                        <cs-ru>Дне́сь Дре́во яви́ся, дне́сь ро́д евре́йский поги́бе, дне́сь ве́рными цари́ ве́ра явля́ется, и Ада́м дре́ва ра́ди испаде́, и па́ки Дре́вом де́мони вострепета́ша; Всеси́льне Го́споди, сла́ва Тебе́.</cs-ru>
						                        </text>
					                        </ymnos>
				                        </theotokion>
			                        </aposticha>
			                        <troparion ihos=""1"">
                                        <ymnos>
					                        <text>
						                        <cs-ru>Спаси́, Го́споди, лю́ди Твоя́ и благослови́ достоя́ние Твое́ на сопроти́вныя да́руя и Твое́ сохраня́я Кресто́м Твои́м жи́тельство.</cs-ru>
					                        </text>
				                        </ymnos>
			                        </troparion>
		                        </mikrosesperinos>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            XmlNode root = xmlDoc.DocumentElement;

            MikrosEsperinos element = new MikrosEsperinos(root);// (xmlDoc.FirstChild);

            Assert.AreEqual(element.Kekragaria.Groups[0].Ymnis.Count, 3);
        }
    }
}
