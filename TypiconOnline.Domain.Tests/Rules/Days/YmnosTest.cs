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
    }
}
