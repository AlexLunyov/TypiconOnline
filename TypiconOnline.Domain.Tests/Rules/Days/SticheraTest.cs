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
    public class SticheraTest
    {
        [Test]
        public void Stichera_Creature()
        {
            string xmlString = @"<kekragaria>
				                    <group ihos=""5"">
					                    <prosomoion self=""true""/>
					                    <ymnos>
						                    <text>
							                    <cs-ru>Ра́дуйся, живоно́сный Кре́сте, благоче́стия непобеди́мая побе́да, две́рь ра́йская, ве́рных утвержде́ние, Це́ркве огражде́ние, и́мже тля́ разори́ся и упраздни́ся, и попра́ся сме́ртная держа́ва, и вознесо́хомся от земли́ к небе́сным; ору́жие непобеди́мое, бесо́в сопротивобо́рче, сла́ва му́чеников, преподо́бных я́ко вои́стину удобре́ние, приста́нище спасе́ния, да́руяй ми́ру ве́лию ми́лость.</cs-ru>
						                    </text>
					                    </ymnos>
					                    <ymnos>
						                    <text>
							                    <cs-ru>Ра́дуйся, Госпо́день Кре́сте, и́мже разреши́ся от кля́твы челове́чество, су́щия ра́дости зна́мение, прогоня́яй враги́ во твое́м воздви́жении, всече́стне. На́м помо́щниче, царе́й держа́во, кре́пость пра́ведных, свяще́нников благоле́пие, вообража́емый, и лю́тых избавля́яй, же́зл си́лы, и́мже пасе́мся, ору́жие ми́ра, его́же со стра́хом обстоя́т Ангели, Христа́ Боже́ственная сла́ва, подаю́щаго ми́ру ве́лию ми́лость.</cs-ru>
						                    </text>
					                    </ymnos>
					                    <ymnos>
						                    <text>
							                    <cs-ru>Ра́дуйся, слепы́х наста́вниче, немощны́х врачу́, воскресе́ние все́х уме́рших, воздви́гнувый ны́, во тлю́ па́дшия, Кре́сте честны́й, и́мже разруши́ся кля́тва, и процвете́ нетле́ние, и земни́и обожи́хомся, и диа́вол всеконе́чно низве́ржеся. Дне́сь воздвиза́ема тя́ ви́дяще рука́ми архиере́йскими, возно́сим Вознесе́ннаго посреде́ тебе́, и тебе́ покланя́емся, почерпа́юще бога́тно ве́лию ми́лость.</cs-ru>
						                    </text>
					                    </ymnos>
				                    </group>
				                    <theotokion ihos=""6"">
					                    <ymnos>
						                    <text>
							                    <cs-ru>Проро́ков гла́си Дре́во свято́е предвозвести́ша, и́мже дре́вния свободи́ся кля́твы сме́ртныя Ада́м, тва́рь же, дне́сь возноси́му тому́, совозвыша́ет гла́с, от Бо́га прося́щи бога́тыя ми́лости; но, еди́ный в благоутро́бии, безме́рный Влады́ко, очище́ние бу́ди на́м и спаси́ ду́ши на́ша.</cs-ru>
						                    </text>
					                    </ymnos>
				                    </theotokion>
			                    </kekragaria>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            XmlNode root = xmlDoc.DocumentElement;

            YmnosStructure element = new YmnosStructure(root);// (xmlDoc.FirstChild);

            Assert.AreEqual(element.Groups[0].Ihos, 5);
            //Assert.Pass(element.Text.Text["cs-ru"]);
        }
    }
}
