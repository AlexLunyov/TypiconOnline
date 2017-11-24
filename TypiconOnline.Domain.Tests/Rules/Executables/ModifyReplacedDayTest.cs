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
using TypiconOnline.Domain.Serialization;
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

            var unitOfWork = new RuleSerializerRoot();

            var element = unitOfWork.Factory<ModifyReplacedDay>()
                .CreateElement(new XmlDescriptor() { Description = xmlString });

            Assert.AreEqual(KindOfReplacedDay.Menology, element.Kind);
        }
    }
}
