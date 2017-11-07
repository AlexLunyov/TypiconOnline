using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Books;

namespace TypiconOnline.Repository.EFSQLite.Tests
{
    [TestFixture]
    public class SequenceViewerTest
    {
        [Test]
        public void BypassSequenceViewer_Test()
        {
            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Data\SQLiteDB.db");
            EFSQLiteUnitOfWork unitOfWork = new EFSQLiteUnitOfWork(path);

            BookStorage.Instance = BookStorageFactory.Create(unitOfWork);

            ISequenceViewer viewer = new BypassSequenceViewer(unitOfWork);

            string result = viewer.GetSequence(new GetSequenceRequest() { TypiconId = 1, Date = DateTime.Today }).Sequence;

            Assert.IsNotEmpty(result);
            Assert.Pass(result);
        }

        [Test]
        public void BypassSequenceViewer_Test2()
        {
            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Data\SQLiteDB.db");
            EFSQLiteUnitOfWork unitOfWork = new EFSQLiteUnitOfWork(path);

            BookStorage.Instance = BookStorageFactory.Create(unitOfWork);

            ISequenceViewer viewer = new BypassSequenceViewer(unitOfWork);

            string result = viewer.GetSequence(new GetSequenceRequest() { TypiconId = 1, Date = new DateTime(2017, 11, 7) }).Sequence;

            result = viewer.GetSequence(new GetSequenceRequest() { TypiconId = 1, Date = new DateTime(2017, 12, 26) }).Sequence;

            Assert.IsNotEmpty(result);
            Assert.Pass(result);
        }
    }
}
