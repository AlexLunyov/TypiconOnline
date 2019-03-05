using NUnit.Framework;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Rules.Schedule.Extensions;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule.Extensions
{
    [TestFixture]
    public class KanonasExtensionsTest
    {
        [Test]
        public void KanonasExtensions_IsKatavasia()
        {
            var bookStorage = BookStorageFactory.Create();

            var response = bookStorage.Katavasia.Get(new GetKatavasiaRequest() { Name = "отверзу_уста_моя", Serializer = new TypiconSerializer()});

            Assert.IsTrue(response.BookElement.IsKatavasia());
        }
    }
}
