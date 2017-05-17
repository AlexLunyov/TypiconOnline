using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Tests.ItemTypes
{
    [TestFixture]
    public class ItemTimeTest
    {
        [Test]
        public void ItemTime_FromIntegers()
        {
            ItemTime item = new ItemTime(12, 15);
            Assert.AreEqual("12.15", item.ToString());
        }

        [Test]
        public void ItemTime_FromString()
        {
            ItemTime item = new ItemTime("18.00");

            Assert.AreEqual(18, item.Hour);
            Assert.AreEqual(0, item.Minute);
        }
    }
}
