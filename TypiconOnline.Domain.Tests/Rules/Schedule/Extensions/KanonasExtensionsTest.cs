using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Rules.Schedule.Extensions;

namespace TypiconOnline.Domain.Tests.Rules.Schedule.Extensions
{
    [TestFixture]
    public class KanonasExtensionsTest
    {
        [Test]
        public void KanonasExtensions_IsKatavasia()
        {
            var bookStorage = BookStorageFactory.Create();

            var response = bookStorage.Katavasia.Get(new GetKatavasiaRequest() { Name = "отверзу_уста_моя" });

            Assert.IsTrue(response.BookElement.IsKatavasia());
        }
    }
}
