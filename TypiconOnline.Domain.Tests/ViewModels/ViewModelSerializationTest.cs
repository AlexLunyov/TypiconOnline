using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.ViewModels
{
    [TestFixture]
    public class ViewModelSerializationTest
    {
        [Test]
        public void ViewModelSerialization_Deserialize()
        {
            var factory = new RuleHandlerSettingsTestFactory();

            string xml = TestDataXmlReader.GetXmlString("ViewModel_Deserialize.xml");

            var settings = factory.CreateSettings(new DateTime(2017, 11, 13), xml);

            var handler = new ServiceSequenceHandler() { Settings = settings };

            settings.RuleContainer.Interpret(handler);

            var viewModel = handler.GetResult();

            var serializer = new TypiconSerializer();

            var result = serializer.Serialize(viewModel.First());

            Assert.IsNotEmpty(result);
            Assert.Pass(result);
        }

        [Test]
        public void ViewModelSerialization_DeserializeFull()
        {
            var unitOfWork = UnitOfWorkFactory.Create();

            var typiconEntity = unitOfWork.Repository<TypiconEntity>().Get(c => c.Id == 1);

            var service = ScheduleServiceFactory.Create(unitOfWork);

            var scheduleDay = service.GetScheduleDay(new GetScheduleDayRequest()
            {
                Handler = new ServiceSequenceHandler(),
                Date = new DateTime(2017, 11, 13),
                Typicon = typiconEntity
            });

            var serializer = new TypiconSerializer();

            var result = serializer.Serialize(scheduleDay.Day);

            Assert.IsNotEmpty(result);
            Assert.Pass(result);
        }
    }
}
