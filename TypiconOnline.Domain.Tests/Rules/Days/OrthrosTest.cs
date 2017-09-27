using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.Tests.Rules.Days
{
    [TestFixture]
    public class OrthrosTest
    {
        [Test]
        public void OrthrosTest_Creature()
        {
            string xmlString = @"<orthros>
			                        <kanonas ihos=""4"">
				                        <acrostic>
					                        <cs-ru>Побе́ды тя́ пою́ тезоимени́таго, Ники́ту.</cs-ru>
				                        </acrostic>
				                        <odi number=""1"">
					                        <irmos>
						                        <cs-ru>Мо́ря чермну́ю пучи́ну невла́жными стопа́ми дре́вний пешеше́ствовав Изра́иль, крестообра́зными Моисе́овыма рука́ма Амали́кову си́лу в пусты́ни победи́л е́сть</cs-ru>
					                        </irmos>
					                        <troparion>
						                        <cs-ru>Умертви́в пло́ти моея́ мудрова́ние, у́м озари́ просвети́тельною све́тлостию, твое́ воспе́ти торжество́, я́ко победи́тель всеизя́щнейш, умоля́я, о Ники́то, Христа́.</cs-ru>
					                        </troparion>
					                        <troparion>
						                        <cs-ru>Яд от страсте́й бы́вший отложи́в, ра́ны претерпе́л еси́ кре́пце и в ри́зу че́стну страда́льчески оде́ялся еси́, от твои́х крове́й истка́нную.</cs-ru>
					                        </troparion>
					                        <troparion>
						                        <cs-ru>Но́в же и чу́ден яви́л еси́ пу́ть страда́ния, за ны́ на Дре́ве пригвозди́вся: нача́ло бо бы́л еси́ му́чеников, Влады́ко, испове́дания и кре́пости.</cs-ru>
					                        </troparion>
					                        <troparion kind=""theotokion"">
						                        <cs-ru>Две́рь я́же к на́м Твоего́, Го́споди, снизше́ствия, Восто́к, и Со́лнце, и Све́т та́инственно Тя́ нарица́ем, преесте́ственно просия́, сугу́ба естество́м, Человеколю́бца.</cs-ru>
					                        </troparion>
				                        </odi>
				                        <odi number=""3"">
					                        <irmos>
						                        <cs-ru>Весели́тся о Тебе́ Це́рковь Твоя́, Христе́, зову́щи: Ты́ моя́ кре́пость, Го́споди, и прибе́жище, и утвержде́ние.</cs-ru>
					                        </irmos>
					                        <troparion>
						                        <cs-ru>Тве́рдостию души́ мучи́тельская преще́ния претерпе́л еси́, му́чениче Христо́в сла́вне, Боже́ственною си́лою возмога́емь.</cs-ru>
					                        </troparion>
					                        <troparion>
						                        <cs-ru>Пло́ти твоея́ расточе́ния и уде́с отсече́ния претерпе́в, му́чениче Христо́в всехва́льне, твою́ ду́шу приобре́л еси́.</cs-ru>
					                        </troparion>
					                        <troparion>
						                        <cs-ru>Весели́тся о тебе́ му́чеников Христо́вых собра́ние, па́мять твою́ дне́сь соверша́ему ви́дяще.</cs-ru>
					                        </troparion>
					                        <troparion kind=""theotokion"">
						                        <cs-ru>Мари́е, чи́стое де́вства и честно́е селе́ние, лю́тыя стру́пы моего́ се́рдца исцели́.</cs-ru>
					                        </troparion>
				                        </odi>
				                        <sedalen ihos=""4"">
					                        <prosomoion>
						                        <cs-ru>Вознесы́йся.</cs-ru>
					                        </prosomoion>
					                        <ymnos>
						                        <text>
							                        <cs-ru>Непобеди́мь яви́вся во́ин, ва́рварския ко́зни победи́л еси́ и, пострада́в, сла́вне, крепча́йше по́лк пора́товал eси́ неви́димых враго́в, те́м и диади́му прия́л еси́ побе́ды, о на́с моля́ся не преста́й, блаже́нне Ники́то, ве́рою пою́щих тя́.</cs-ru>
						                        </text>
					                        </ymnos>
				                        </sedalen>
				                        <odi number=""4"">
					                        <irmos>
						                        <cs-ru>Вознесе́на Тя́ ви́девши Це́рковь на Кресте́, Со́лнце пра́ведное, ста́ в чи́не свое́м, досто́йно взыва́ющи: сла́ва си́ле Твое́й, Го́споди.</cs-ru>
					                        </irmos>
					                        <troparion>
						                        <cs-ru>Иску́плен кро́вию Влады́чнею, страда́лец Ники́та свою́ кро́вь, ра́дуяся, прино́сит Христу́, те́мже, закала́емь, — сла́ва Тебе́, Бо́же мо́й, — вопия́ше.</cs-ru>
					                        </troparion>
					                        <troparion>
						                        <cs-ru>Разжига́емь раче́нием Влады́чним, и́дольский, му́чениче, попра́л еси́ пла́мень и всю́ де́монскую кре́пость, зовы́й Созда́телю: сла́ва си́ле Твое́й, Го́споди.</cs-ru>
					                        </troparion>
					                        <troparion>
						                        <cs-ru>Наслажда́яся Госпо́дним виде́нием и Того́ све́тлостию насыща́яся, му́ченик, стражда́, не ощуща́ше и, ра́дуяся, зва́ше: сла́ва си́ле Твое́й, Го́споди.</cs-ru>
					                        </troparion>
					                        <troparion kind=""theotokion"">
						                        <cs-ru>Украша́емо разли́чным светоли́тием не́бо одушевле́нное Твое́, Царя́ ца́рствующих, Христе́, Де́ва Пречи́стая, ны́не я́ко Богоро́дица сла́вится.</cs-ru>
					                        </troparion>
				                        </odi>
				                        <odi number=""5"">
					                        <irmos>
						                        <cs-ru>Ты́, Го́споди, мо́й Све́т, в ми́р прише́л еси́, Све́т Святы́й, обраща́яй из мра́чна неве́дения ве́рою воспева́ющия Тя́.</cs-ru>
					                        </irmos>
					                        <troparion>
						                        <cs-ru>Кто́ мо́жет изрещи́ твои́х, Ники́то, натрижне́ний, всехва́льне, венцы́, я́же дарова́ тебе́ Христо́с, за Него́ пострада́вшу?</cs-ru>
					                        </troparion>
					                        <troparion>
						                        <cs-ru>Му́ченицы, на земли́ по́двиги сконча́вше, небе́сное в высоча́йших от Тебе́, Жизнода́вче, воздая́ние прия́ша.</cs-ru>
					                        </troparion>
					                        <troparion>
						                        <cs-ru>Ны́не сия́еши, я́ко свети́ло, о Ники́то всехва́льне, cве́т вторы́й быва́еши, Cве́ту вели́кому сла́вно предстоя́.</cs-ru>
					                        </troparion>
					                        <troparion kind=""theotokion"">
						                        <cs-ru>Све́т безле́тный на́м, подле́тен бы́вшь, родила́ еси́, Богома́ти, су́щим во тме́ жития́, и ми́р ве́сь просвети́ла еси́.</cs-ru>
					                        </troparion>
				                        </odi>
				                        <odi number=""6"">
					                        <irmos>
						                        <cs-ru>Пожру́ Ти со гла́сом хвале́ния, Го́споди, Це́рковь вопие́т Ти́, от бесо́вския кро́ве очи́щшися, ра́ди ми́лости от ре́бр Твои́х исте́кшею кро́вию.</cs-ru>
					                        </irmos>
					                        <troparion>
						                        <cs-ru>Вожделе́в добро́ты Созда́вшаго, сла́вне, и отту́ду сия́ния прие́мь, му́чениче, впери́лся еси́ и, о сме́рти небре́г, Тому́ приступи́л еси́.</cs-ru>
					                        </troparion>
					                        <troparion>
						                        <cs-ru>Теку́щих по́мыслом прие́м целому́дренным, неистощи́мая, му́чениче, предче́ствовав, те́ло твое́ му́кам, о Ники́то, c весе́лием, ра́дуяся, преда́л еси́.</cs-ru>
					                        </troparion>
					                        <troparion kind=""theotokion"">
						                        <cs-ru>Яко я́блоко в дубра́ве еди́ну тя́ обре́т, и чисте́йший кри́н, и удо́льный цве́т, о Богома́ти, Жени́х мы́сленный в Тя́ всели́ся.</cs-ru>
					                        </troparion>
				                        </odi>
				                        <kontakion ihos=""2"">
					                        <prosomoion>
						                        <cs-ru>Вы́шних ища́.</cs-ru>
					                        </prosomoion>
					                        <ymnos>
						                        <text>
							                        <cs-ru>Пре́лести посе́к держа́ву стоя́нием твои́м и побе́ды прии́м вене́ц во страда́льчествех твои́х, со Ангелы, сла́вне, ра́дуешися, Ники́то тезоимени́тe, с ни́ми Христа́ Бо́га моля́ непреста́нно о все́х на́с.</cs-ru>
						                        </text>
					                        </ymnos>
					                        <ymnos>
						                        <text>
							                        <cs-ru>Зна́ние Твое́ вложи́в души́ мое́й, очи́сти мо́й по́мысл и Твои́х за́поведей де́лателя, Спа́се, покажи́, да возмогу́ победи́ти страсте́й мои́х ра́зная привоста́ния, победи́тельную по́честь прие́м безстра́стия, моли́твами Твоего́ до́бляго страстоте́рпца Ники́ты, Человеколю́бче: и́бо са́м на́с воспомина́ти того́ в па́мяти его́ созва́, моля́ся непреста́нно о все́х на́с.</cs-ru>
						                        </text>
					                        </ymnos>
				                        </kontakion>
				                        <odi number=""7"">
					                        <irmos>
						                        <cs-ru>В пещи́ авраа́мстии о́троцы перси́дстей, любо́вию благоче́стия па́че, не́жели пла́менем, опаля́еми, взыва́ху: благослове́н еси́ в хра́ме сла́вы Твоея́, Го́споди.</cs-ru>
					                        </irmos>
					                        <troparion>
						                        <cs-ru>Ны́не непристу́пною заре́ю осия́емь, му́чениче, пою́щия ны́не тво́й пра́здник све́том твои́м просвети́, — благослове́н еси́, Бо́же мо́й, — зовы́й, — и Го́споди.</cs-ru>
					                        </troparion>
					                        <troparion>
						                        <cs-ru>Преудиви́шася твоему́ му́жеству во́инства Ангельская, блаже́нне, зря́ще терпели́вно стра́ждуща тя́ и глаго́люща: благослове́н еси́, Бо́же все́х и Го́споди.</cs-ru>
					                        </troparion>
					                        <troparion kind=""theotokion"">
						                        <cs-ru>С вы́шним ликостоя́нием безпло́тный, — ра́дуйся, Богоро́дице Чи́стая, — Гаврии́л, веселя́ся, зовя́ше Ти́, — благослове́на Ты́ в жена́х еси́, Всенепоро́чная Влады́чице.</cs-ru>
					                        </troparion>
				                        </odi>
				                        <odi number=""8"">
					                        <irmos>
						                        <cs-ru>Ру́це распросте́р, Дании́л, льво́в зия́ния в ро́ве затче́; о́гненную же си́лу угаси́ша, доброде́телию препоя́савшеся, благоче́стия рачи́тели, о́троцы, взыва́юще: благослови́те, вся́ дела́ Госпо́дня, Го́спода.</cs-ru>
					                        </irmos>
					                        <troparion>
						                        <cs-ru>Всеце́лу же́ртву и прия́тну тебе́ сама́го прине́сл еси́, му́чениче непобеди́ме, и всепло́дие бы́л еси́ благово́нно Влады́це твоему́, распе́ншемуся на́с ра́ди, с весе́лием вопия́: благослови́те, вся́ дела́ Госпо́дня, Го́спода.</cs-ru>
					                        </troparion>
					                        <troparion>
						                        <cs-ru>Зако́нне, я́ко боже́ственный хра́брник, вра́жия полки́ низврати́в, досто́йне прия́л еси́ вене́ц побе́ды неувяда́ющий от жизноно́сныя Десни́цы, Ейже ны́не предстои́ши, воспева́я: благослови́те, вся́ дела́ Госпо́дня, Го́спода.</cs-ru>
					                        </troparion>
					                        <troparion>
						                        <cs-ru>Возлюби́л еси́ безме́рно Христа́ и Того́ Кро́ви твою́ кро́вь примеси́л еси́, и раздробля́емь ра́нами, и разли́чно убива́емь. Ему́же и ны́не, я́ко подо́бник, сца́рствуеши, вопия́: благослови́те, вся́ дела́ Госпо́дня, Го́спода.</cs-ru>
					                        </troparion>
					                        <troparion kind=""theotokion"">
						                        <cs-ru>Скве́рну естества́ на́шего, Христа́ ро́ждши Еди́наго Пречи́стаго, я́ве отмы́ла еси́, Чи́стая, Богоро́дице Всенепоро́чная, и херуви́м и серафи́м была́ еси́ превы́шше, вопию́щих: благослови́те, вся́ дела́ Госпо́дня, Го́спода.</cs-ru>
					                        </troparion>
				                        </odi>
				                        <odi number=""9"">
					                        <irmos>
						                        <cs-ru>Ка́мень нерукосе́чный от несеко́мыя горы́, Тебе́, Де́во, краеуго́льный отсече́ся — Христо́с, совокупи́вый разстоя́щаяся естества́. Те́м, веселя́щеся, Тя́, Богоро́дице, велича́ем.</cs-ru>
					                        </irmos>
					                        <troparion>
						                        <cs-ru>Всего́ мене́ сама́го тебе́ приношу́, богоблаже́нне Ники́то, да обря́щу тя́ предста́теля ко Влады́це, спаса́ти могу́ща от вся́каго обстоя́ния, и спасе́ния Боже́ственнаго хода́тая.</cs-ru>
					                        </troparion>
					                        <troparion>
						                        <cs-ru>Свиде́тель и́стинно бы́л еси́ и́стины, страстоте́рпче, Ипоста́сней и честне́й ны́не Истине, ра́дуяся, предстои́ши, по́двигов натрижне́ния со дерзнове́нием прие́мля.</cs-ru>
					                        </troparion>
					                        <troparion>
						                        <cs-ru>Ко приста́нищу ти́хому приста́в, почи́ от боле́зней и венцено́сец лику́еши в раи́ с му́ченики Христо́выми. Те́м тя́ ны́не, я́ко богопросла́вленна, вси́ досто́йно сла́вим.</cs-ru>
					                        </troparion>
					                        <troparion kind=""theotokion"">
						                        <cs-ru>Зако́нов есте́ственных кроме́ Законода́вца рaжда́еши, пло́ть бы́вша невра́щно за благоутро́бие неизрече́нное, Благослове́нная Чи́стая, во дву́х существа́х познава́ема.</cs-ru>
					                        </troparion>
				                        </odi>
				                        <exapostilarion>
					                        <prosomoion>
						                        <cs-ru>Жены́, услы́шите.</cs-ru>
					                        </prosomoion>
					                        <ymnos>
						                        <text>
							                        <cs-ru>Ору́жием честна́го Твоего́ Креста́ огради́вся, страстоте́рпец Тво́й, Сло́ве, проти́вныя си́лы кре́пко препобеди́, и мучи́тели посрами́, и за Тя́ пострада́, и с Тобо́ю, Христе́ мо́й, Всецарю́, Ники́та ца́рствует.</cs-ru>
						                        </text>
					                        </ymnos>
				                        </exapostilarion>
			                        </kanonas>
			                        <aposticha>
				                        <doxastichon ihos=""6"">
					                        <ymnos>
						                        <text>
							                        <cs-ru>Дне́сь вселе́нная вся́ страстоте́рпца ра́дуется страда́нием, и Христо́ва Це́рковь, цве́ты украша́ема, му́чениче Христо́в, вопие́т ти́: уго́дниче Христо́в и предста́телю тепле́йший, не преста́й моля́ся о рабе́х твои́х.</cs-ru>
						                        </text>
					                        </ymnos>
				                        </doxastichon>
			                        </aposticha>
		                        </orthros>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            XmlNode root = xmlDoc.DocumentElement;

            Orthros element = new Orthros(root);// (xmlDoc.FirstChild);

            Assert.NotNull(element);
        }

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
