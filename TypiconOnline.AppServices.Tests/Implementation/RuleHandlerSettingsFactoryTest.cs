using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Implementation
{
    [TestFixture]
    public class RuleHandlerSettingsFactoryTest
    {
        [Test]
        public void RuleHandlerSettingsFactory_Create()
        {
            var unitOfWork = UnitOfWorkFactory.Create();
            var serializer = TestRuleSerializer.GetRoot(unitOfWork);
            var modifiedRuleService = new ModifiedRuleService(unitOfWork, new ModifiedYearFactory(unitOfWork, serializer));
            RuleHandlerSettingsFactory factory = new RuleHandlerSettingsFactory(serializer, modifiedRuleService);

            var request = new GetRuleSettingsRequest()
            {
                TypiconId = 1,
                Date = new DateTime(2018, 06, 04)
            };

            var response = factory.Create(request);

            Assert.IsNotNull(response.RuleContainer);
        }
    }
}
