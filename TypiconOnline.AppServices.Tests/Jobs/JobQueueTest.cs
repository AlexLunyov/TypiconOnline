using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Jobs;

namespace TypiconOnline.AppServices.Tests.Jobs
{
    [TestFixture]
    public class JobQueueTest
    {
        [Test]
        public void JobQueue_MoreThan()
        {
            var queue = new JobQueue();

            queue.Send(new CalculateModifiedYearJob(1, 2019));
            queue.Send(new CalculateModifiedYearJob(1, 2020));
            queue.Send(new CalculateModifiedYearJob(1, 2021));

            var c = queue.Extract<CalculateModifiedYearJob>(6);

            Assert.AreEqual(3, c.Count());
        }
    }
}
