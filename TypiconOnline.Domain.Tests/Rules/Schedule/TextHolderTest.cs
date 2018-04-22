using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class TextHolderTest
    {
        [Test]
        public void TextHolder_Creature()
        {
            #region xml
            string xmlString = @"<rule>
	                                <deacon>
		                                <p>
			                                <item language=""cs-ru"">Миром Господу помолимся.</item>
		                                </p>
	                                </deacon>
	                                <choir>
		                                <p>
			                                <item language=""cs-ru"">Господи, помилуй.</item>
		                                </p>
	                                </choir>
	                                <deacon>
		                                <p>
			                                <item language=""cs-ru"">Пресвятую, Пречистую, Преблагословенную, Славную Владычицу Нашу Богородицу, и Приснодеву Марию со всеми святыми помянувши сами себя, и друг друга, и весь живот наш Христу Богу предадим.</item>
		                                </p>
	                                </deacon>
	                                <choir>
		                                <p>
			                                <item language=""cs-ru"">Тебе Господи.</item>
		                                </p>
	                                </choir>
	                                <priest>
		                                <p>
			                                <item language=""cs-ru"">Яко подобает Тебе всякая слава, честь и поклонение, Отцу, и Сыну, и Святому Духу, всегда, ныне и присно, и во веки веков.</item>
		                                </p>
	                                </priest>
	                                <choir>
		                                <p>
			                                <item language=""cs-ru"">Аминь.</item>
		                                </p>
	                                </choir>
	                                <lector>
		                                <p>
			                                <item language=""cs-cs"">Положи́, Го́споди, хране́ние усто́м мои́м, и дверь огражде́ния о устна́х мои́х.</item>
		                                </p>
	                                </lector>
                                </rule>";
            #endregion

            var unitOfWork = new RuleSerializerRoot(BookStorageFactory.Create());

            var element = unitOfWork.Container<ExecContainer>()
                .Deserialize(new XmlDescriptor() { Description = xmlString }, null);
            //3
            int choirCount = element.ChildElements.Where(c => (c is TextHolder) && (c as TextHolder).Kind == TextHolderKind.Choir).Count();
            //2
            int deaconCount = element.ChildElements.Where(c => (c is TextHolder) && (c as TextHolder).Kind == TextHolderKind.Deacon).Count();
            //1
            int priestCount = element.ChildElements.Where(c => (c is TextHolder) && (c as TextHolder).Kind == TextHolderKind.Priest).Count();
            //1
            int lectorCount = element.ChildElements.Where(c => (c is TextHolder) && (c as TextHolder).Kind == TextHolderKind.Lector).Count();

            Assert.AreEqual(choirCount, 3);
            Assert.AreEqual(deaconCount, 2);
            Assert.AreEqual(priestCount, 1);
            Assert.AreEqual(lectorCount, 1);
            Assert.IsTrue(element.IsValid);
            Assert.Pass("Ok");
        }
    }
}
