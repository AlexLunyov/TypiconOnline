using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Common;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.AppServices.Tests.Common
{
    [TestFixture]
    class TypeEqualsOrSubclassOfTest
    {
        [Test]
        public void Check()
        {
            Assert.IsTrue(TypeEqualsOrSubclassOf<string>.Is(""));
        }

        [Test]
        public void CheckMenology()
        {
            DayRule rule = new MenologyRule();
            var modifiedRule = new ModifiedRule() { DayRule = rule };

            Assert.IsTrue(TypeEqualsOrSubclassOf<MenologyRule>.Is(modifiedRule.DayRule));
        }

        [Test]
        public void CheckMenology_False()
        {
            DayRule rule = new TriodionRule();
            var modifiedRule = new ModifiedRule() { DayRule = rule };

            Assert.IsFalse(TypeEqualsOrSubclassOf<MenologyRule>.Is(modifiedRule.DayRule));
        }
    }
}
