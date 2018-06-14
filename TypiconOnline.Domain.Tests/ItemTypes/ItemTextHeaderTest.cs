using NUnit.Framework;
using System.Linq;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Tests.ItemTypes
{
    [TestFixture]
    public class ItemTextHeaderTest
    {
        [Test]
        public void ItemTextHeader_Right()
        {
            string xmlString = @"<ItemTextHeader header=""h1"">
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
                                </ItemTextHeader>";

            //XmlDocument xmlDoc = new XmlDocument();

            //xmlDoc.LoadXml(xmlString);

            var element = new ItemTextHeader(xmlString, "ItemTextHeader");// (xmlDoc.FirstChild);

            Assert.IsFalse(element.IsEmpty);
            Assert.AreEqual(4, element.Items.Count());
            Assert.AreEqual(HeaderCaption.h1, element.Header);
        }
    }
}
