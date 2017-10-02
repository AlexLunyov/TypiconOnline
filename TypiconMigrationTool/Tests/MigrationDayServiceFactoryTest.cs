using NUnit.Framework;
using ScheduleHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            MigrationDayServiceFactory factory = new MigrationDayServiceFactory(Properties.Settings.Default.FolderPath);

            factory.Initialize(row);

            DayService dayService = factory.Create();

            Assert.IsNotEmpty(dayService.DayDefinition);
        }
    }
}
