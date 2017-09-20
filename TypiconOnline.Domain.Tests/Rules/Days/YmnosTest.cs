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
    public class YmnosTest
    {
        [Test]
        public void Ymnos_Creature()
        {
            string xmlString = @"<ymnos>
						            <text>
							            <cs-ru>Зна́ние Твое́ вложи́в души́ мое́й, очи́сти мо́й по́мысл и Твои́х за́поведей де́лателя, Спа́се, покажи́, да возмогу́ победи́ти страсте́й мои́х ра́зная привоста́ния, победи́тельную по́честь прие́м безстра́стия, моли́твами Твоего́ до́бляго страстоте́рпца Ники́ты, Человеколю́бче: и́бо са́м на́с воспомина́ти того́ в па́мяти его́ созва́, моля́ся непреста́нно о все́х на́с.</cs-ru>
						            </text>
					            </ymnos>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            XmlNode root = xmlDoc.DocumentElement;

            Ymnos element = new Ymnos(root);// (xmlDoc.FirstChild);

            Assert.NotNull(element.Text.Text["cs-ru"]);
            Assert.Pass(element.Text.Text["cs-ru"]);
        }

        [Test]
        public void Ymnos_Serialization()
        {
            string xmlString = @"<Ymnos>
                                    <text>
						                <cs-ru>[item] [sign] Господи воззвах, Славник</cs-ru>
					                </text>
				                </Ymnos>";
            TypiconSerializer ser = new TypiconSerializer();
            Ymnos element = ser.Deserialize<Ymnos>(xmlString);

            Assert.NotNull(element.Text.Text["cs-ru"]);
            Assert.Pass(element.Text.Text["cs-ru"]);
        }

        [Test]
        public void Ymnos_SerializationFull()
        {
            string xmlString = @"<Ymnos>
                                    <annotation>
					                    <cs-ru>Аннотация</cs-ru>
				                    </annotation>
                                    <stihos>
							            <cs-ru>Стих 1</cs-ru>
						            </stihos>
                                    <stihos>
							            <cs-ru>Стих 2</cs-ru>
						            </stihos>
                                    <text>
						                <cs-ru>Текст</cs-ru>
					                </text>
				                </Ymnos>";
            TypiconSerializer ser = new TypiconSerializer();
            Ymnos element = ser.Deserialize<Ymnos>(xmlString);

            Assert.NotNull(element.Text.Text["cs-ru"]);
            Assert.Pass(element.Text.Text["cs-ru"]);
        }

        [Test]
        public void Ymnos_SerializationTextRequired()
        {
            string xmlString = @"<Ymnos>
                                    <annotation>
					                    <cs-ru>Творе́ние господи́на Пахо́мия мона́ха.</cs-ru>
				                    </annotation>
                                    <stihos>
							            <cs-ru>[item] [sign] Вечерня. На стиховне, Стих 1-й стихиры.</cs-ru>
						            </stihos>
                                    <stihos>
							            <cs-ru>[item] [sign] Вечерня. На стиховне, Стих 2-й стихиры.</cs-ru>
						            </stihos>
				                </Ymnos>";
            TypiconSerializer ser = new TypiconSerializer();
            Ymnos element = ser.Deserialize<Ymnos>(xmlString);

            Assert.IsFalse(element.IsValid);
        }
    }
}
