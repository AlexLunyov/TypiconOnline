using NUnit.Framework;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Serialization;

//using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Tests.ItemTypes
{
    [TestFixture]
    public class ItemTextStyledTest
    {
        [Test]
        public void ItemTextStyled_Right()
        {
            string xmlString = @"<ItemTextStyled bold=""true"" red=""true"" italic=""true"">
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
                                </ItemTextStyled>";

            var element = new TypiconSerializer().Deserialize<ItemTextStyled>(xmlString);

            Assert.IsTrue(element.IsBold);
            Assert.IsTrue(element.IsRed);
            Assert.IsTrue(element.IsItalic);
        }

        [Test]
        public void ItemTextStyled_StringExpression_Get()
        {
            string xmlString = @"<ItemTextStyled bold=""true"">
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
                                </ItemTextStyled>";

            var serializer = new TypiconSerializer();

            var element = serializer.Deserialize<ItemTextStyled>(xmlString);

            element.IsBold = false;

            var newXmlString = serializer.Serialize(element);

            Assert.IsTrue(newXmlString.Contains(@"bold=""false"""));
        }

        //[Test]
        //public void ItemTextStyled_Deserialize()
        //{
        //    string xmlString = @"<ItemTextStyled>
	       //                         <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	       //                         <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	       //                         <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	       //                         <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
	       //                         <style>
		      //                          <bold>true</bold>
        //                                <header>h1</header>
	       //                         </style>
        //                        </ItemTextStyled>";

        //    TypiconSerializer ser = new TypiconSerializer();
        //    ItemTextStyled element = ser.Deserialize<ItemTextStyled>(xmlString);


        //    Assert.IsNotNull(element);
        //    Assert.AreEqual(element.Style.Header, HeaderCaption.h1);
        //}

        [Test]
        public void ItemTextStyled_Serialize()
        {
            string xmlString = @"<ItemTextStyled bold=""true"">
	                                <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	                                <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	                                <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	                                <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
                                </ItemTextStyled>";
            TypiconSerializer ser = new TypiconSerializer();
            ItemTextStyled element = ser.Deserialize<ItemTextStyled>(xmlString);

            element.AddOrUpdate(new ItemTextUnit() { Language = "cs-cs", Text = "cs-cs Текст измененный" });

            //element.Style.Header = HeaderCaption.h1;
            element.IsBold = true;

            string result = ser.Serialize(element);

            Assert.Pass(result);
        }

        //[Test]
        //public void ItemTextStyled_StringExpression()
        //{
        //    string xmlString = @"<ItemText>
	       //                         <item language=""cs-ru"">Блажен муж, иже не иде на совет нечестивых и на пути грешных не ста, и на седалищи губителей не седе,</item>
	       //                         <item language=""cs-cs"">Бlжeнъ мyжъ, и4же не и4де на совётъ нечести1выхъ, и3 на пути2 грёшныхъ не стA, и3 на сэдaлищи губи1телей не сёде:</item>
	       //                         <item language=""ru-ru"">Блажен муж, который не пошел на совет нечестивых, и на путь грешных не вступил, и не сидел в сборище губителей;</item>
	       //                         <item language=""el-el"">Μακάριος ἀνήρ, ὃς οὐκ ἐπορεύθη ἐν βουλῇ ἀσεβῶν καὶ ἐν ὁδῷ ἁμαρτωλῶν οὐκ ἔστη καὶ ἐπὶ καθέδραν λοιμῶν οὐκ ἐκάθισεν,</item>
	       //                         <style>
		      //                          <bold>true</bold>
        //                                <red>true</red>
        //                                <header>h1</header>
	       //                         </style>
        //                        </ItemText>";

        //    ItemTextStyled element = new ItemTextStyled
        //    {
        //        StringExpression = xmlString
        //    };

        //    Assert.AreEqual(element.Style.Header, HeaderCaption.h1);
        //    Assert.IsFalse(element.IsEmpty);
        //}

        
    }
}
