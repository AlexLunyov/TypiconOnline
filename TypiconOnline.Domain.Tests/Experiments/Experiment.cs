using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Domain.Tests.Experiments
{
    [TestFixture]
    public class Experiment
    {
        [Test]
        public void SimpleLinkingList()
        {
            List<DayService> list1 = new List<DayService>();

            list1.Add(new DayService());
            list1.Add(new DayService());
            list1.Add(new DayService());
            list1.Add(new DayService());

            List<DayService> list2 = list1;

            list2.Add(new DayService());

            Assert.AreEqual(5, list1.Count);
            Assert.AreEqual(5, list2.Count);
        }

        [Test]
        public void CopyList()
        {
            List<DayService> list1 = new List<DayService>();

            list1.Add(new DayService());
            list1.Add(new DayService());
            list1.Add(new DayService());
            list1.Add(new DayService());

            List<DayService> list2 = list1.ToList();

            list2.Add(new DayService());

            Assert.AreEqual(4, list1.Count);
            Assert.AreEqual(5, list2.Count);
        }
    }
}
