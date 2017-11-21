using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class KKatavasiaRuleTest
    {
        [Test]
        public void KKatavasiaRule_Source_Kanonas()
        {
            string xmlString = @"<k_katavasia source=""item1"" kanonas=""orthros1""/>";

            KKatavasiaRule element = RuleFactory.CreateElement(xmlString) as KKatavasiaRule;

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void KKatavasiaRule_Invalid_Source()
        {
            string xmlString = @"<k_katavasia source=""item1""/>";

            KKatavasiaRule element = RuleFactory.CreateElement(xmlString) as KKatavasiaRule;

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void KKatavasiaRule_Invalid_Kanonas()
        {
            string xmlString = @"<k_katavasia kanonas=""orthros1""/>";

            KKatavasiaRule element = RuleFactory.CreateElement(xmlString) as KKatavasiaRule;

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void KKatavasiaRule_Invalid_InvalidName()
        {
            BookStorage.Instance = BookStorageFactory.Create();

            string xmlString = @"<k_katavasia name=""invalid""/>";

            KKatavasiaRule element = RuleFactory.CreateElement(xmlString) as KKatavasiaRule;

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void KKatavasiaRule_ValidName_FromDB()
        {
            BookStorage.Instance = BookStorageFactory.Create();

            string xmlString = @"<k_katavasia name=""отверзу_уста_моя""/>";

            KKatavasiaRule element = RuleFactory.CreateElement(xmlString) as KKatavasiaRule;

            Assert.IsTrue(element.IsValid);
        }
    }
}
