using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations.Books;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Migration;

namespace TypiconOnline.AppServices.Tests.Migration
{
    [TestFixture]
    public class IrmTheotokionMigrationTest
    {
        [Test]
        public void IrmTheotokionMigration_Test()
        {
            string folder = Path.Combine(Properties.Settings.Default.FolderPath, @"Books\Irmologion\Theotokion");

            IrmologionTheotokionFileReader reader = new IrmologionTheotokionFileReader(new FileReader(folder));

            IrmologionTheotokionService service = new IrmologionTheotokionService(_unitOfWork);

            IMigrationManager manager = new IrmologionTheotokionMigrationManager(reader, service);

            manager.Migrate();
        }
    }
}
