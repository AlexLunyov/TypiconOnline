using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Repository.EFCore.Tests.Common
{
    [TestClass]
    public class IncludeTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestMethod()
        {
            Type type = typeof(TypiconEntity);

            foreach (var property in type.GetProperties())
            {
                //property.
            }
        }

        [TestMethod]
        public void Include_LoadRelated()
        {
            var unitOfWork = UnitOfWorkFactory.Create();

            var typicon = unitOfWork.Repository<TypiconEntity>().Get(c => c.Id == 1, new IncludeOptions());

            Assert.AreEqual(0, typicon.MenologyRules.Count);

            typicon = unitOfWork.Repository<TypiconEntity>().Get(c => c.Id == 1);

            Assert.AreNotEqual(0, typicon.MenologyRules.Count);
        }

        [TestMethod]
        public void Include_LoadCachedRelated()
        {
            var unitOfWork = EFCacheUOFFactory.Create();

            var typicon = unitOfWork.Repository<TypiconEntity>().Get(c => c.Id == 1, new IncludeOptions());

            Assert.AreEqual(0, typicon.MenologyRules.Count);

            typicon = unitOfWork.Repository<TypiconEntity>().Get(c => c.Id == 1);

            Assert.AreNotEqual(0, typicon.MenologyRules.Count);
        }
    }
}
