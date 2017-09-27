using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Tests.ItemTypes
{
    [TestFixture]
    public class ItemTextTest
    {
        [Test]
        public void ItemText_Right()
        {
            string xmlString = @"<ItemText>
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
	                                <style>
		                                <bold/>
		                                <red/>
		                                <h1/>
	                                </style>
                                </ItemText>";

            //XmlDocument xmlDoc = new XmlDocument();

            //xmlDoc.LoadXml(xmlString);

            ItemText element = new ItemText(xmlString);// (xmlDoc.FirstChild);

            Assert.AreEqual(element.Style.Header, HeaderCaption.h1);
        }

        [Test]
        public void ItemText_WrongLanguage()
        {
            string xmlString = @"<ItemText>
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-c1s"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
                                </ItemText>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            ItemText element = new ItemText(xmlString);// (xmlDoc.FirstChild);

            Assert.AreEqual(element.IsValid, false);
        }

        [Test]
        public void ItemxText_ComposeXml()
        {
            string xmlString = @"<ItemText>
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
	                                <style>
		                                <bold>true</bold>
	                                </style>
                                </ItemText>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            ItemText element = new ItemText(xmlString);

            element.Style.IsBold = false;

            string newXmlStrng = element.StringExpression;

            Assert.Pass(newXmlStrng);

            Assert.AreEqual(element.IsValid, false);
        }

        [Test]
        public void ItemText_Deserialize()
        {
            string xmlString = @"<ItemText>
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
	                                <style>
		                                <bold>true</bold>
                                        <header>h1</header>
	                                </style>
                                </ItemText>";

            TypiconSerializer ser = new TypiconSerializer();
            ItemText element = ser.Deserialize<ItemText>(xmlString);


            Assert.IsNotNull(element);
            Assert.AreEqual(element.Style.Header, HeaderCaption.h1);
        }

        [Test]
        public void ItemText_Serialize()
        {
            string xmlString = @"<ItemText>
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
	                                
                                </ItemText>";
            TypiconSerializer ser = new TypiconSerializer();
            ItemText element = ser.Deserialize<ItemText>(xmlString);

            element.Text["cs-cs"] = "cs-cs Текст измененный";

            element.Style.Header = HeaderCaption.h1;
            element.Style.IsBold = true;

            string result = ser.Serialize(element);

            Assert.Pass(result);
        }
    }
}
