using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Serialization;
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

            var result = serializer.Serialize(viewModel);

            Assert.IsNotEmpty(result);
            Assert.Pass(result);
        }
    }
}
