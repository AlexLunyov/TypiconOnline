using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Tests.ItemTypes
{
    [TestFixture]
    public class ItemTextNotedTest
    {
        [Test]
        public void ItemTextNotedTest_Note()
        {
            string xmlString = @"<ItemText>
	                                <item language=""cs-ru"">Господи помилуй.</item>
	                                <item language=""cs-cs"">Господи помилуй.</item>
	                                <item language=""ru-ru"">Господи помилуй.</item>
	                                <item language=""el-el"">Господи помилуй.</item>
	                                <note><item language=""cs-cs"">Трижды.</item><style><red>true</red><bold>true</bold></style></note>
                                </ItemText>";

            //XmlDocument xmlDoc = new XmlDocument();

            //xmlDoc.LoadXml(xmlString);

            ItemTextNoted element = new ItemTextNoted
            {
                StringExpression = xmlString
            };

            Assert.AreEqual("Господи помилуй. Трижды.", element.ToString());
            Assert.Pass(element.StringExpression);
        }

        [Test]
        public void ItemText_Serialize()
        {
            string xmlString = @"<ItemTextNoted>
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
	                                <note><item language=""cs-cs"">Трижды.</item><style><red>true</red><bold>true</bold></style></note>
                                </ItemTextNoted>";
            TypiconSerializer ser = new TypiconSerializer();
            ItemTextNoted element = ser.Deserialize<ItemTextNoted>(xmlString);

            element["cs-cs"] = "cs-cs Текст измененный";

            element.Style.Header = HeaderCaption.h1;
            element.Style.IsBold = true;

            string result = ser.Serialize(element);

            Assert.Pass(result);
        }
    }
}
