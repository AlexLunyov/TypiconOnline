using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Executables
{
    [TestFixture]
    public class RootContainerTest
    {
        [Test]
        public void RootContainer_Deserialize()
        {
            var xml = TestDataXmlReader.GetXmlString("RootContainerTest.xml");
            var element = TestRuleSerializer.Deserialize<RootContainer>(xml);
            // TODO: Add your test code here
            Assert.IsTrue(element is RootContainer);
        }
    }
}
