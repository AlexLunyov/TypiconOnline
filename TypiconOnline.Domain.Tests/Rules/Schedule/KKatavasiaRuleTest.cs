using NUnit.Framework;
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
            string xmlString = @"<k_katavasia source=""menology1"" kanonas=""orthros1""/>";

            var element = TestRuleSerializer.Deserialize<KKatavasiaRule>(xmlString);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void KKatavasiaRule_Invalid_Source()
        {
            string xmlString = @"<k_katavasia source=""menology1""/>";

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
