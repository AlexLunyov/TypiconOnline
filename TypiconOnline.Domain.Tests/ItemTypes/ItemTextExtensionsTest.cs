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
    public class ItemTextExtensionsTest
    {
        [Test]
        public void ItemText_Merge()
        {
            var item1 = new ItemText()
            {
                Items = new List<ItemTextUnit>()
                {
                    new ItemTextUnit("cs-ru", "1"),
                    new ItemTextUnit("cs-cs", "1"),
                }
            };

            var item2 = new ItemText()
            {
                Items = new List<ItemTextUnit>()
                {
                    new ItemTextUnit("cs-ru", "2"),
                    new ItemTextUnit("ru-ru", "1"),
                }
            };

            item1.Merge(item2);

            Assert.AreEqual("1 2", item1.FirstOrDefault("cs-ru").Text);
            Assert.AreEqual("1", item1.FirstOrDefault("cs-cs").Text);
        }

        [Test]
        public void ItemText_MergeEmpty()
        {
            var item1 = new ItemText()
            {
                Items = new List<ItemTextUnit>()
                {
                    new ItemTextUnit("cs-ru", "1"),
                    new ItemTextUnit("cs-cs", "1"),
                }
            };

            var item2 = new ItemText()
            {
                Items = new List<ItemTextUnit>()
                {
                    new ItemTextUnit("cs-ru", "2"),
                    new ItemTextUnit("ru-ru", "1"),
                }
            };

            var item3 = new ItemText();

            item3.Merge(item1, item2);

            Assert.AreEqual("1 2", item3.FirstOrDefault("cs-ru").Text);
            Assert.AreEqual("1", item3.FirstOrDefault("cs-cs").Text);
        }
    }
}
