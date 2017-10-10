using NUnit.Framework;
using System.Linq;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Repository.EF;

namespace TypiconOnline.AppServices.Tests
{
    [TestFixture]
    public class TypiconEntityServiceTest
    {
        [Test]
        public void TypiconEntityService_ClearModifiedYears()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            BookStorage.Instance = BookStorageFactory.Create();

            TypiconEntityService service = new TypiconEntityService(_unitOfWork);

            GetTypiconEntityResponse response = service.GetTypiconEntity(1);

            service.ClearModifiedYears(1);

            Assert.AreEqual(response.TypiconEntity.ModifiedYears.Count, 0);
        }
    }
}
