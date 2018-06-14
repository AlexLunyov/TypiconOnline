using NUnit.Framework;
using ScheduleHandling;
using System.Linq;
using TypiconOnline.Domain.Days;

namespace TypiconMigrationTool.Tests
{
    [TestFixture]
    public class MigrationDayServiceFactoryTest
    {
        [Test]
        public void MigrationDayServiceFactory_Sample()
        {
            ScheduleHandler sh = new ScheduleHandler("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\data\\ScheduleDB.mdb;");

            ScheduleDBDataSet.MineinikRow row = sh.DataSet.Mineinik.FirstOrDefault();

            MigrationDayWorshipFactory factory = new MigrationDayWorshipFactory(Properties.Settings.Default.FolderPath);

            factory.Initialize(row);

            DayWorship dayService = factory.Create();

            Assert.IsNotEmpty(dayService.Definition);
        }
    }
}
