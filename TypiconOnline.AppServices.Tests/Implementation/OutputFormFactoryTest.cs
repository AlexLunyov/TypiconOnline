using NUnit.Framework;
using System;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
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

            var form = factory.Create(new CreateOutputDayRequest()
            {
                TypiconId = 1,
                TypiconVersionId = 1,
                Date = new DateTime(2019, 11, 2),
                HandlingMode = HandlingMode.AstronomicDay
            });

            Assert.NotNull(form);
        }

        private OutputDayFactory Build()
        {
            var dbContext = TypiconDbContextFactory.Create();

            var query = DataQueryProcessorFactory.Create(dbContext);

            var serializerRoot = TestRuleSerializer.Create(dbContext);

            var settingsFactory = new RuleHandlerSettingsFactory(serializerRoot);

            var dataCalculator = new ExplicitDataCalculator(query,
                new AsAdditionDataCalculator(query,
                    new TransparentDataCalculator(query,
                        new MajorDataCalculator(query, settingsFactory)
                        , settingsFactory)
                    , settingsFactory)
                , settingsFactory);

            return new OutputDayFactory(dataCalculator
                , new ScheduleDayNameComposer(query)
                , serializerRoot.TypiconSerializer
                , new ServiceSequenceHandler());
        }
    }
}
