using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class KAfterRuleTest
    {
        [Test]
        public void KAfterRule_Deserialize()
        {
            string xmlString = @"<k_after odinumber=""3"">
							        <priest>
						                <p>
							                <item language=""cs-ru"">Благослове́н Бог наш всегда́, ны́не и при́сно, и во ве́ки веко́в</item>
						                </p>
					                </priest>
						        </k_after>";

            var element = TestRuleSerializer.Deserialize<KAfterRule>(xmlString);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void KAfterRule_NoChildren()
        {
            string xmlString = @"<k_after odinumber=""3""></k_after>";

            var element = TestRuleSerializer.Deserialize<KAfterRule>(xmlString);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void KAfterRule_InvalidNumber()
        {
            string xmlString = @"<k_after odinumber=""10""></k_after>";

            var element = TestRuleSerializer.Deserialize<KAfterRule>(xmlString);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void KAfterRule_NoNumber()
        {
            string xmlString = @"<k_after>
							        <priest>
						                <p>
							                <item language=""cs-ru"">Благослове́н Бог наш всегда́, ны́не и при́сно, и во ве́ки веко́в</item>
						                </p>
					                </priest>
						        </k_after>";

            var element = TestRuleSerializer.Deserialize<KAfterRule>(xmlString);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void KAfterRule_InKanonasRule()
        {
            string xmlString = @"<kanonasrule panagias=""Честнейшую"">
						            <k_after odinumber=""3"">
							            <commonrule name=""Ектения_малая3""/>
						            </k_after>
						            <k_after odinumber=""6"">
							            <commonrule name=""Ектения_малая6""/>
						            </k_after>
                                </kanonasrule>";

            var element = TestRuleSerializer.Deserialize<KanonasRule>(xmlString);

            element.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.AreEqual(2, element.AfterRules.Count());
        }
    }
}
