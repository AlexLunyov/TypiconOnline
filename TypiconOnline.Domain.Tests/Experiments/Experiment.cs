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
            List<DayWorship> list1 = new List<DayWorship>();

            list1.Add(new DayWorship());
            list1.Add(new DayWorship());
            list1.Add(new DayWorship());
            list1.Add(new DayWorship());

            List<DayWorship> list2 = list1;

            list2.Add(new DayWorship());

            Assert.AreEqual(5, list1.Count);
            Assert.AreEqual(5, list2.Count);
        }

        [Test]
        public void CopyList()
        {
            List<DayWorship> list1 = new List<DayWorship>();

            list1.Add(new DayWorship());
            list1.Add(new DayWorship());
            list1.Add(new DayWorship());
            list1.Add(new DayWorship());

            List<DayWorship> list2 = list1.ToList();

            list2.Add(new DayWorship());

            Assert.AreEqual(4, list1.Count);
            Assert.AreEqual(5, list2.Count);
        }

        [Test]
        public void LinkPropertiesTest()
        {
            DayWorship dayWorship = new DayWorship();

            SetNullObject(dayWorship);

            Assert.IsNotNull(dayWorship);

            void SetNullObject(DayWorship w)
            {
                w = null;
            }
        }

        [Test]
        public void IntTryParseTest()
        {
            var numberString = "Псалом 144.";
            numberString = numberString.Replace("Псалом", string.Empty).Replace(".", string.Empty);

            int.TryParse(numberString, out int number);

            Assert.AreEqual(144, number);
        }
    }
}
