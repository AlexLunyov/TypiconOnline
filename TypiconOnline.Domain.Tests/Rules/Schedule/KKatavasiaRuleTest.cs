using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Tests.Common;


namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class KKatavasiaRuleTest
    {
        [Test]
        public void KKatavasiaRule_Source_Kanonas()
        {
            string xmlString = @"<k_katavasia source=""item1"" kanonas=""orthros1""/>";

            var element = TestRuleSerializer.Deserialize<KKatavasiaRule>(xmlString);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void KKatavasiaRule_Invalid_Source()
        {
            string xmlString = @"<k_katavasia source=""item1""/>";

            var element = TestRuleSerializer.Deserialize<KKatavasiaRule>(xmlString);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void KKatavasiaRule_Invalid_Kanonas()
        {
            string xmlString = @"<k_katavasia kanonas=""orthros1""/>";

            var element = TestRuleSerializer.Deserialize<KKatavasiaRule>(xmlString);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void KKatavasiaRule_Invalid_InvalidName()
        {
            string xmlString = @"<k_katavasia name=""invalid""/>";

            var element = TestRuleSerializer.Deserialize<KKatavasiaRule>(xmlString);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void KKatavasiaRule_ValidName_FromDB()
        {
            string xmlString = @"<k_katavasia name=""отверзу_уста_моя""/>";

            var element = TestRuleSerializer.Deserialize<KKatavasiaRule>(xmlString);

            Assert.IsTrue(element.IsValid);
        }
    }
}
