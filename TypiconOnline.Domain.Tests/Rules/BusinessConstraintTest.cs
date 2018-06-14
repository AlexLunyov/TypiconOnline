using System.Linq;
using NUnit.Framework;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules
{
    [TestFixture]
    public class BusinessConstraintTest
    {
        [Test]
        public void RuleElement_Validate()
        {
            string xmlString = TestDataXmlReader.GetXmlString("BusinessConstraintTest.xml");

            var element = TestRuleSerializer.Deserialize<ExecContainer>(xmlString);

            Assert.IsFalse(element.IsValid);
            Assert.Pass(element.GetBrokenConstraints().FirstOrDefault().ConstraintFullDescription);
        }
    }
}
