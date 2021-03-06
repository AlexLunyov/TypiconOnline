﻿using System.Linq;
using NUnit.Framework;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class KAfterRuleTest
    {
        [Test]
        public void KAfterRule_Deserialize()
        {
            string xmlString = @"<kanonasrule>
                                    <k_after number=""3"">
							            <priest>
						                    <p>
							                    <item language=""cs-ru"">Благослове́н Бог наш всегда́, ны́не и при́сно, и во ве́ки веко́в</item>
						                    </p>
					                    </priest>
						            </k_after>
                                </kanonasrule>";

            var element = TestRuleSerializer.Deserialize<KanonasRule>(xmlString);

            Assert.IsTrue(element.ChildElements[0].IsValid);
        }

        //[Test]
        //public void KAfterRule_NoChildren()
        //{
        //    string xmlString = @"<k_after number=""3""></k_after>";

        //    var element = TestRuleSerializer.Deserialize<KAfterRule>(xmlString);

        //    Assert.IsFalse(element.IsValid);
        //}

        [Test]
        public void KAfterRule_InvalidNumber()
        {
            string xmlString = @"<kanonasrule><k_after number=""10""></k_after></kanonasrule>";

            var element = TestRuleSerializer.Deserialize<KanonasRule>(xmlString);

            Assert.IsFalse(element.ChildElements[0].IsValid);
        }

        [Test]
        public void KAfterRule_NoNumber()
        {
            string xmlString = @"<kanonasrule>
                                    <k_after>
							            <priest>
						                    <p>
							                    <item language=""cs-ru"">Благослове́н Бог наш всегда́, ны́не и при́сно, и во ве́ки веко́в</item>
						                    </p>
					                    </priest>
						            </k_after>
                                </kanonasrule>";

            var element = TestRuleSerializer.Deserialize<KanonasRule>(xmlString);

            Assert.IsFalse(element.ChildElements[0].IsValid);
        }

        [Test]
        public void KAfterRule_InKanonasRule()
        {
            string xmlString = @"<kanonasrule>
						            <k_after number=""3"">
							            <priest>
						                    <p>
							                    <item language=""cs-ru"">Благослове́н Бог наш всегда́, ны́не и при́сно, и во ве́ки веко́в</item>
						                    </p>
					                    </priest>
						            </k_after>
						            <k_after number=""6"">
							            <priest>
						                    <p>
							                    <item language=""cs-ru"">Благослове́н Бог наш всегда́, ны́не и при́сно, и во ве́ки веко́в</item>
						                    </p>
					                    </priest>
						            </k_after>
                                </kanonasrule>";

            var element = TestRuleSerializer.Deserialize<KanonasRule>(xmlString);

            element.Interpret(BypassHandler.Instance);

            Assert.AreEqual(2, element.AfterRules.Count());
        }
    }
}
