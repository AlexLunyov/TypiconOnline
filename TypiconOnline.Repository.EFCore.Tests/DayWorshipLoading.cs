using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Repository.EFCore.Tests
{
    [TestFixture]
    public class DayWorshipLoading
    {
        [Test]
        public void DayWorshipLoading_ItemTexts()
        {
            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Data\SQLiteDB.db");
            SQLiteUnitOfWork unitOfWork = new SQLiteUnitOfWork(path);

            DayWorship entity = unitOfWork.Repository<DayWorship>().Get(c => c.Id == 155);

            Assert.IsFalse(entity.WorshipName.IsEmpty);
            Assert.Pass(entity.WorshipName.StringExpression);
        }
    }
}
