using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Common;

namespace TypiconOnline.AppServices.Tests.Common
{
    [TestFixture]
    public class SignMigratorTest
    {
        [Test]
        public void SignMigrator_MajorTemplateName()
        {
            Assert.AreEqual("Бдение с литией", SignMigrator.Instance(5).MajorTemplateName);
            Assert.AreEqual("Без знака", SignMigrator.Instance(0).MajorTemplateName);
            Assert.AreEqual("Воскресный день", SignMigrator.Instance(8).MajorTemplateName);
            Assert.AreEqual("Без знака", SignMigrator.Instance(2).MajorTemplateName);
            Assert.AreEqual("Без знака", SignMigrator.Instance(1).MajorTemplateName);
        }
    }
}
