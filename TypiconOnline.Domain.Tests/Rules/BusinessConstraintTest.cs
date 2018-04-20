using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Executables;

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
