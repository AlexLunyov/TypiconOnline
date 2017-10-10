using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Services;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Repository.EF;

namespace TypiconOnline.Domain.Tests.Rules.Executables
{
    [TestFixture]
    public class ModifyReplacedDayTest
    {
        [Test]
        public void ModifyReplacedDay_Xml()
        {
            string xmlString = @"<modifyreplacedday daymove=""0"" kind=""menology""/>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            ModifyReplacedDay element = RuleFactory.CreateElement(xmlDoc.FirstChild) as ModifyReplacedDay;

            Assert.AreEqual(RuleConstants.KindOfReplacedDay.menology, element.Kind);
        }
    }
}
