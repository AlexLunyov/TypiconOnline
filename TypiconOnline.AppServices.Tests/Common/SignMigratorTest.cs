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
            Assert.AreEqual("Бдение с литией", SignMigrator.Instance(20).MajorTemplateName);
            Assert.AreEqual("Без знака", SignMigrator.Instance(11).MajorTemplateName);
            Assert.AreEqual("Воскресный день", SignMigrator.Instance(10).MajorTemplateName);
            Assert.AreEqual("Без знака", SignMigrator.Instance(14).MajorTemplateName);
            Assert.AreEqual("Без знака", SignMigrator.Instance(16).MajorTemplateName);
        }
    }
}
