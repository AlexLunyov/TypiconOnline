using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class TextHolderTest
    {
        [Test]
        public void TextHolder_Creature()
        {
            string xmlString = @"<rule>
	                                <deacon>
		                                <p>
			                                <cs-ru>Миром Господу помолимся.</cs-ru>
		                                </p>
	                                </deacon>
	                                <choir>
		                                <p>
			                                <cs-ru>Господи, помилуй.</cs-ru>
		                                </p>
	                                </choir>
	                                <deacon>
		                                <p>
			                                <cs-ru>Пресвятую, Пречистую, Преблагословенную, Славную Владычицу Нашу Богородицу, и Приснодеву Марию со всеми святыми помянувши сами себя, и друг друга, и весь живот наш Христу Богу предадим.</cs-ru>
		                                </p>
	                                </deacon>
	                                <choir>
		                                <p>
			                                <cs-ru>Тебе Господи.</cs-ru>
		                                </p>
	                                </choir>
	                                <priest>
		                                <p>
			                                <cs-ru>Яко подобает Тебе всякая слава, честь и поклонение, Отцу, и Сыну, и Святому Духу, всегда, ныне и присно, и во веки веков.</cs-ru>
		                                </p>
	                                </priest>
	                                <choir>
		                                <p>
			                                <cs-ru>Аминь.</cs-ru>
		                                </p>
	                                </choir>
	                                <lector>
		                                <p>
			                                <cs-cs>Положи́, Го́споди, хране́ние усто́м мои́м, и дверь огражде́ния о устна́х мои́х.</cs-cs>
		                                </p>
	                                </lector>
                                </rule>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            ExecContainer element = (ExecContainer) RuleFactory.CreateElement(xmlDoc.FirstChild);
            //3
            int choirCount = element.ChildElements.Where(c => (c is TextHolder) && (c as TextHolder).Kind.Value == TextHolderKind.Choir).Count();
            //2
            int deaconCount = element.ChildElements.Where(c => (c is TextHolder) && (c as TextHolder).Kind.Value == TextHolderKind.Deacon).Count();
            //1
            int priestCount = element.ChildElements.Where(c => (c is TextHolder) && (c as TextHolder).Kind.Value == TextHolderKind.Priest).Count();
            //1
            int lectorCount = element.ChildElements.Where(c => (c is TextHolder) && (c as TextHolder).Kind.Value == TextHolderKind.Lector).Count();

            Assert.AreEqual(choirCount, 3);
            Assert.AreEqual(deaconCount, 2);
            Assert.AreEqual(priestCount, 1);
            Assert.AreEqual(lectorCount, 1);
            Assert.IsTrue(element.IsValid);
            Assert.Pass("Ok");
        }
    }
}
