using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Implementation
{
    [TestFixture]
    public class OutputFormFactoryTest
    {
        [Test]
        public void OutputFormFactory_Test()
        {
            var factory = Build();

            var form = factory.Create(new OutputFormCreateRequest()
            {
                TypiconId = 1,
                TypiconVersionId = 1,
                Date = new DateTime(2019, 1, 1),
                HandlingMode = HandlingMode.AstronomicDay
            });

            Assert.NotNull(form);
        }

        private OutputFormFactory Build()
        {
            var dbContext = TypiconDbContextFactory.Create();

            var query = DataQueryProcessorFactory.Create(dbContext);

            var serializerRoot = TestRuleSerializer.Create(dbContext);

            var settingsFactory = new RuleHandlerSettingsFactory(serializerRoot);

            return new OutputFormFactory(new ScheduleDataCalculator(query, settingsFactory)
                , new ScheduleDayNameComposer(query)
                , serializerRoot.TypiconSerializer);
        }
    }
}
