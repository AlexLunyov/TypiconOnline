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
    public class EsperinosTest
    {
        [Test]
        public void Esperinos_Deserialization()
        {
            string xmlString = @"<Esperinos>
		                            <kekragaria>
				                        <group ihos=""1"">
					                        <prosomoion>
						                        <item language=""cs-ru"">Небе́сных чино́в</item>
					                        </prosomoion>
						                    <ymnos>
							                    <text>
								                    <item language=""cs-ru"">Благочести́вым всеору́жием оде́явся, / побо́рник яви́лся еси́ Христа́ Всецаря́, / Ники́тo страстоте́рпче, / я́коже дре́вле Гедео́н всекре́пкий, / иноплеме́нных полки́ низложи́в / до́блественным твои́м дерзнове́нием.</item>
							                    </text>
						                    </ymnos>
						                    <ymnos>
							                    <text>
								                    <item language=""cs-ru"">Иноплеме́ннаго, му́чениче, пора́товал еси́ / всеси́льною ве́рою Спа́совою, богому́дре, / претерпе́вый стра́сти. / Те́м му́дре ва́рвары обрати́л еси́ / ко Творцу́ и Влады́це все́х, / и сла́вят Его́ благочести́во.</item>
							                    </text>
						                    </ymnos>
						                    <ymnos>
							                    <text>
								                    <item language=""cs-ru"">Нача́тки, му́чениче, от ва́рвар Христу́ сла́ву принесы́й, / яви́лся еси́ страда́лец богоглаго́ливый, / у́мер за Тро́ицу, / те́м я́вственно и вене́ц восприя́л еси́, / и, я́ко свети́ло, сия́еши боголе́пно / в ми́ре, достосла́вне.</item>
							                    </text>
						                    </ymnos>
				                        </group>
			                        <doxastichon ihos=""6"">
				                        <annotation>
					                        <item language=""cs-ru"">Феофа́ново</item>
				                        </annotation>
					                        <ymnos>
						                        <text>
							                        <item language=""cs-ru"">Свети́льника му́чеников тя́ позна́хом, Ники́то, Христо́в страда́льче: ты́ бо, су́щаго на земли́ досто́инства сла́ву оста́вив и оте́ческое безбо́жие возненави́дев, бо́ги и́х сокруши́л еси́ и победи́тельно ва́рвары посрами́л еси́, за Христа́ му́ченичество испове́дания испо́лнил еси́ и во́ин небе́снаго Бо́га бы́л еси́. О на́с моли́ся Благоде́телю все́х, е́же уще́дрити и спасти́ ду́ши на́ша.</item>
						                        </text>
					                        </ymnos>
			                        </doxastichon>
		                        </kekragaria>
		                        <aposticha>
			                        <doxastichon ihos=""8"">
					                    <ymnos>
						                    <text>
							                    <item language=""cs-ru"">Побе́ды тезоимени́т показа́лся еси́, му́чениче всече́стне Ники́то, в по́двизе пропове́дав Христа́ Бо́га на́шего, и пред цари́ и мучи́тели того́ испове́дал еси́. Те́м не преста́й моля́ся о ми́ре ми́ра, и христолюби́вых царе́х, и о все́х, па́мять твою́ соверша́ющих ве́рно, Еди́ному Человеколю́бцу, изба́вити вся́каго гне́ва.</item>
						                    </text>
					                    </ymnos>
			                        </doxastichon>
		                        </aposticha>
		                        <troparion>
				                    <group ihos=""4"">
						                <ymnos>
							                <text>
								                <item language=""cs-ru"">Му́ченик Тво́й, Го́споди, Ники́та во страда́нии свое́м вене́ц прия́т нетле́нный от Тебе́, Бо́га на́шего: име́яй бо кре́пость Твою́, мучи́телей низложи́, сокруши́ и де́монов немощны́я де́рзости. Того́ моли́твами спаси́ ду́ши на́ша.</item>
							                </text>
						                </ymnos>
						                <ymnos>
							                <text>
								                <item language=""cs-ru"">Кре́ст Христо́в, я́ко не́кое ору́жие, усе́рдно восприи́м, и к боре́нию враго́в прите́кл еси́, и, за Христа́ пострада́в, последи́ огне́м свяще́нную твою́ ду́шу Го́споду пре́дал еси́, отону́дуже и даро́в исцеле́ния от Него́ сподо́бился еси́ прия́ти, великому́чениче Ники́то. Моли́ Христа́ Бо́га спасти́ся душа́м на́шим.</item>
							                </text>
						                </ymnos>
				                    </group>
		                        </troparion>
	                        </Esperinos>";

            TypiconSerializer ser = new TypiconSerializer();
            Esperinos element = ser.Deserialize<Esperinos>(xmlString);

            Assert.AreEqual(element.Troparion.Groups[0].Ihos, 4);

        }
    }
}
