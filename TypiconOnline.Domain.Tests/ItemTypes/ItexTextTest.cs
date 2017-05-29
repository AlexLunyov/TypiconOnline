using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Tests.ItemTypes
{
    [TestFixture]
    public class ItexTextTest
    {
        [Test]
        public void ItexText_Right()
        {
            string xmlString = @"<text>
	                                <cs-ru>Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</cs-ru>
	                                <cs-cs>Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</cs-cs>
	                                <ru-ru>Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</ru-ru>
	                                <el-el>Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</el-el>
	                                <style><bold/><red/><h1/></style>
                                </text>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            ItemText element = new ItemText(xmlDoc.FirstChild);

            Assert.AreEqual(element.Style.Header, HeaderCaption.H1);
        }

        [Test]
        public void ItexText_WrongLanguage()
        {
            string xmlString = @"<text>
	                                <cs-ru>Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</cs-ru>
	                                <cs-cs1>Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</cs-cs1>
	                                <ru-ru>Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</ru-ru>
	                                <el-el>Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</el-el>
	                                <style><bold/><red/><h1/></style>
                                </text>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            ItemText element = new ItemText(xmlDoc.FirstChild);

            Assert.AreEqual(element.IsValid, false);
        }
    }
}
