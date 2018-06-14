using NUnit.Framework;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Factories
{
    [TestFixture]
    public class RuleSerializerRootTest
    {
        [Test]
        public void RuleSerializerRoot_ExecContainer()
        {
            var unitOfWork = TestRuleSerializer.Create();

            var factoryContainer = unitOfWork.Container<ExecContainer>();

            Assert.IsNotNull(factoryContainer);
            Assert.Pass(factoryContainer.ToString());
        }

        [Test]
        public void RuleSerializerRoot_RuleElement()
        {
            var unitOfWork = TestRuleSerializer.Create();

            var factoryContainer = unitOfWork.Container<RuleElement>();

            Assert.IsNotNull(factoryContainer);
            Assert.Pass(factoryContainer.ToString());
        }

        [Test]
        public void RuleSerializerRoot_If()
        {
            var unitOfWork = TestRuleSerializer.Create();

            var factoryContainer = unitOfWork.Container<If>();

            Assert.IsNotNull(factoryContainer);
            Assert.Pass(factoryContainer.ToString());
        }

        [Test]
        public void RuleSerializerRoot_TypeTesting()
        {
            Assert.IsTrue(typeof(ExecContainer).IsSubclassOf((typeof(RuleElement))));
        }

        [Test]
        public void RuleSerializerRoot_Additional()
        {
            var unitOfWork = TestRuleSerializer.Create();

            var factoryContainer = unitOfWork.Container<RuleExecutable, ICalcStructureElement>();

            Assert.IsNotNull(factoryContainer);
            Assert.Pass(factoryContainer.ToString());
        }
    }
}
