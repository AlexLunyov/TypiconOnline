using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Repository.EFSQLite.Tests
{
    [TestFixture]
    public class MenologyDayLoading
    {
        [Test]
        public void MenologyDayLoading_ItemText_and_Dates()
        {
            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Data\SQLiteDB.db");
            EFSQLiteUnitOfWork unitOfWork = new EFSQLiteUnitOfWork(path);

            MenologyDay entity = unitOfWork.Repository<MenologyDay>().GetAll().FirstOrDefault();

            Assert.IsFalse(entity.Date.IsEmpty);
            Assert.IsFalse(entity.DateB.IsEmpty);
        }
    }
}
