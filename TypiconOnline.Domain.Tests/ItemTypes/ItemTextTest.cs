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
            string xmlString = @"<text>
	                                <cs-ru>Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</cs-ru>
	                                <cs-cs>Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</cs-cs>
	                                <ru-ru>Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</ru-ru>
	                                <el-el>Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</el-el>
	                                <style><bold/><red/><h1/></style>
                                </text>";

            //XmlDocument xmlDoc = new XmlDocument();

            //xmlDoc.LoadXml(xmlString);

            ItemText element = new ItemText(xmlString);// (xmlDoc.FirstChild);

            Assert.AreEqual(element.Style.Header, HeaderCaption.h1);
        }

        [Test]
        public void ItemxText_WrongLanguage()
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

            ItemText element = new ItemText(xmlString);// (xmlDoc.FirstChild);

            Assert.AreEqual(element.IsValid, false);
        }

        [Test]
        public void ItemxText_ComposeXml()
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

            ItemText element = new ItemText(xmlString);

            element.Style.IsBold = false;

            string newXmlStrng = element.StringExpression;

            Assert.Pass(newXmlStrng);

            Assert.AreEqual(element.IsValid, false);
        }

        [Test]
        public void ItemText_Deserialize()
        {
            string xmlString = @"<?xml version=""1.0"" encoding=""UTF - 8""?>
                                <ItemText xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" >
                                    <cs-ru>Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</cs-ru>
	                                <cs-cs>Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</cs-cs>
	                                <ru-ru>Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</ru-ru>
	                                <el-el>Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</el-el>
	                                <style><bold/><red/><h1/></style>
                                </ItemText>";

            XmlSerializer formatter = new XmlSerializer(typeof(ItemText));

            ItemText element = null;

            using (StringReader fs = new StringReader(xmlString))
            {
                element = (ItemText)formatter.Deserialize(fs);
            }


            Assert.IsNotNull(element);
            Assert.AreEqual(element.Style.Header, HeaderCaption.h1);
        }
    }
}
