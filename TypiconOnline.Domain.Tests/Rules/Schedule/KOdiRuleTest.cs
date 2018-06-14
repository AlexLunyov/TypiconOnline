using NUnit.Framework;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class KOdiRuleTest
    {
        [Test]
        public void KOdiRule_Deserialize()
        {
            string xmlString = @"<kanonasrule>
                                    <k_odi number=""5"">
			                            <k_kanonas source=""item2"" kanonas=""orthros1"" irmoscount=""1"" count=""5""/>
		                            </k_odi>
                                 </kanonasrule>";

            var element = TestRuleSerializer.Deserialize<KanonasRule>(xmlString);

            Assert.IsTrue(element.IsValid);
        }
    }
}
