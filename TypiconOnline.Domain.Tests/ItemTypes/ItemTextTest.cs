using NUnit.Framework;
using System.Linq;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Serialization;

//using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Tests.ItemTypes
{
    [TestFixture]
    public class ItemTextTest
    {
        [Test]
        public void ItemText_Right()
        {
            string xmlString = @"<text>
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
                                </text>";

            var element = new TypiconSerializer().Deserialize<ItemText>(xmlString, "text");

            Assert.IsFalse(element.IsEmpty);
            Assert.AreEqual(4, element.Items.Count());
            //Assert.Pass($"count: {element.Items.Count()}");

            var item = element.FirstOrDefault("cs-ru");
            Assert.IsNotNull(item);
            Assert.Pass($"text: {item.Text}");
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

            var element = new TypiconSerializer().Deserialize<ItemText>(xmlString, "ItemText");

            Assert.AreEqual(4, element.Items.Count);
            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void ItemText_Deserialize()
        {
            string xmlString = @"<ItemText>
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
                                </ItemText>";

            TypiconSerializer ser = new TypiconSerializer();
            ItemText element = ser.Deserialize<ItemText>(xmlString);

            Assert.IsNotNull(element);
            Assert.AreEqual(4, element.Items.Count());
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

            //element["cs-cs"] = "cs-cs Текст измененный";
            element.AddOrUpdate(new ItemTextUnit() { Language = "cs-cs", Text = "cs-cs Текст измененный" });

            string result = ser.Serialize(element);

            Assert.Pass(result);
        }

        //[Test]
        //public void ItemText_Serialize_wrongstring()
        //{
        //    string xmlString = @"some string";
        //    TypiconSerializer ser = new TypiconSerializer();
        //    ItemText element = ser.Deserialize<ItemText>(xmlString);

        //    Assert.IsNull(element);
        //}

        //[Test]
        //public void ItemText_StringExpression_wrongstring()
        //{
        //    string xmlString = @"some string";
        //    ItemText element = new ItemText()
        //    {
        //        StringExpression = xmlString
        //    };

        //    Assert.IsNotNull(element);
        //}

        [Test]
        public void ItemText_StringExpression()
        {
            string xmlString = @"<text>
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
                                </text>";

            var element = new TypiconSerializer().Deserialize<ItemText>(xmlString, "text");

            Assert.IsFalse(element.IsEmpty);
        }

        
    }
}
