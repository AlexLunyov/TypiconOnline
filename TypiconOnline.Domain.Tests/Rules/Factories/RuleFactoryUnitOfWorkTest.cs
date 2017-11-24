using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Tests.Rules.Factories
{
    [TestFixture]
    public class RuleFactoryUnitOfWorkTest
    {
        [Test]
        public void RuleFactoryUnitOfWork_ExecContainer()
        {
            var unitOfWork = new RuleSerializerUnitOfWork();

            var factoryContainer = unitOfWork.Factory<ExecContainer>();

            Assert.IsNotNull(factoryContainer);
            Assert.Pass(factoryContainer.ToString());
        }

        [Test]
        public void RuleFactoryUnitOfWork_RuleElement()
        {
            var unitOfWork = new RuleSerializerUnitOfWork();

            var factoryContainer = unitOfWork.Factory<RuleElement>();

            Assert.IsNotNull(factoryContainer);
            Assert.Pass(factoryContainer.ToString());
        }

        [Test]
        public void RuleFactoryUnitOfWork_If()
        {
            var unitOfWork = new RuleSerializerUnitOfWork();

            var factoryContainer = unitOfWork.Factory<If>();

            Assert.IsNotNull(factoryContainer);
            Assert.Pass(factoryContainer.ToString());
        }

        [Test]
        public void RuleFactoryUnitOfWork_TypeTesting()
        {
            Assert.IsTrue(typeof(ExecContainer).IsSubclassOf((typeof(RuleElement))));
        }

        [Test]
        public void RuleFactoryUnitOfWork_Additional()
        {
            var unitOfWork = new RuleSerializerUnitOfWork();

            var factoryContainer = unitOfWork.Factory<RuleExecutable, ICalcStructureElement>();

            Assert.IsNotNull(factoryContainer);
            Assert.Pass(factoryContainer.ToString());
        }
    }
}
