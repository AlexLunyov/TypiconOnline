using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Tests.Rules.Serialization
{
    [TestFixture]
    class OutputSectionModelCollectionTest
    {
        [Test]
        public void OutputSectionModelCollection_Serialize()
        {
            var serializer = new TypiconSerializer();

            var collection = new OutputSectionModelCollection()
            {
                new OutputSectionModel()
                {
                    Kind = ElementViewModelKind.Choir,
                    KindText = new ItemText(new ItemTextUnit("cs-ru", "Заголовок"), new ItemTextUnit("ru-ru", "Заголовок")),
                    Paragraphs = new List<ItemTextNoted>()
                    {
                        new ItemTextNoted(new ItemText(new ItemTextUnit("cs-ru", "Параграф1"), new ItemTextUnit("ru-ru", "Параграф1"))),
                        new ItemTextNoted(new ItemText(new ItemTextUnit("cs-ru", "Параграф2"), new ItemTextUnit("ru-ru", "Параграф2")))
                    }
                }
            };
            var str = serializer.Serialize(collection);

            var deserialized = serializer.Deserialize<OutputSectionModelCollection>(str);

            Assert.NotNull(deserialized);
        }
    }
}
