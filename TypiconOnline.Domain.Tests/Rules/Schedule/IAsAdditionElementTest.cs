using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class IAsAdditionElementTest
    {
        /// <summary>
        /// Создаем ситуацию с наличием Addition в настройках handler.
        /// В Addition-правиле должен быть элемент, перезаписывающий свойства в Шаблоне-правиле.
        /// </summary>
        [Test]
        public void IAsAdditionElement_Rewrite()
        {
            var factory = new RuleHandlerSettingsTestFactory();

            string xml = TestDataXmlReader.GetXmlString("AsAdditionRewrite1.xml");

            var additionalSettings = factory.CreateSettings(new DateTime(2017, 11, 09), xml);

            xml = TestDataXmlReader.GetXmlString("AsAdditionRewrite2.xml");

            var mainSettings = factory.CreateSettings(new DateTime(2017, 11, 09), xml, additionalSettings);

            var handler = new IsAdditionTestHandler() { Settings = mainSettings };

            mainSettings.RuleContainer.Interpret(handler);

            var result = handler.GetResult();

            Assert.IsNotNull(result.FirstOrDefault( c => c is EktenisRule));
        }

        [Test]
        public void IAsAdditionElement_Append()
        {
            var factory = new RuleHandlerSettingsTestFactory();

            string xml = TestDataXmlReader.GetXmlString("AsAdditionAppend2.xml");

            var additionalSettings = factory.CreateSettings(new DateTime(2017, 11, 09), xml);

            xml = TestDataXmlReader.GetXmlString("AsAdditionAppend1.xml");

            var mainSettings = factory.CreateSettings(new DateTime(2017, 11, 09), xml, additionalSettings);

            var handler = new IsAdditionTestHandler() { Settings = mainSettings };

            mainSettings.RuleContainer.Interpret(handler);

            var result = handler.GetResult();

            Assert.AreEqual(3, result.Count(c => c is KOdiRule));
        }

        [Test]
        public void IAsAdditionElement_Remove()
        {
            var factory = new RuleHandlerSettingsTestFactory();

            string xml = TestDataXmlReader.GetXmlString("AsAdditionRemove2.xml");

            var additionalSettings = factory.CreateSettings(new DateTime(2017, 11, 09), xml);

            xml = TestDataXmlReader.GetXmlString("AsAdditionRemove1.xml");

            var mainSettings = factory.CreateSettings(new DateTime(2017, 11, 09), xml, additionalSettings);

            var handler = new IsAdditionTestHandler() { Settings = mainSettings };

            mainSettings.RuleContainer.Interpret(handler);

            var result = handler.GetResult();

            Assert.AreEqual(4, result.Count(c => c is KOdiRule));
            Assert.IsNull(result.FirstOrDefault(c => c is KOdiRule && (c as KOdiRule).Number == 1));
        }
    }
}
