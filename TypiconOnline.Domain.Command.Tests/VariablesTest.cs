using NUnit.Framework;
using System.Linq;
using TypiconOnline.Domain.Command.Utilities;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Command.Tests
{
    public class VariablesTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VariableSynchronizer_Test()
        {
            string xml = TestDataXmlReader.GetXmlString("VariablesTest.xml");

            var dbContext = TypiconDbContextFactory.Create();

            var entity = dbContext.Set<Sign>().FirstOrDefault(c => c.TypiconVersionId == 1);

            entity.RuleDefinition = xml;

            entity.SyncRuleVariables(TestRuleSerializer.CreateCollectorSerializerRoot());

            Assert.IsTrue(entity.TypiconVersion.TypiconVariables.Count == 2);
        }
    }
}