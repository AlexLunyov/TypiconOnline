using NUnit.Framework;
using System.Linq;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests
{
    [TestFixture]
    public class TypiconEntityServiceTest
    {
        [Test]
        public void TypiconEntityService_ClearModifiedYears()
        {
            var _unitOfWork = UnitOfWorkFactory.Create();

            //BookStorage.Instance = BookStorageFactory.Create();

            TypiconEntityService service = new TypiconEntityService(_unitOfWork);

            GetTypiconEntityResponse response = service.GetTypiconEntity(1);

            service.ClearModifiedYears(1);

            Assert.AreEqual(response.TypiconEntity.ModifiedYears.Count, 0);
        }

        [Test]
        public void TypiconEntity_RemoveExlicitlyModifiedRule()
        {
            var unitOfWork = UnitOfWorkFactory.Create();
            var modifiedRule = unitOfWork.Repository<ModifiedRule>().Get(c => c.Parent.Year == 2017);

            Assert.IsNotNull(modifiedRule);

            var date = modifiedRule.Date;

            unitOfWork.Repository<ModifiedRule>().Remove(modifiedRule);
            unitOfWork.SaveChanges();

            modifiedRule = unitOfWork.Repository<ModifiedRule>().Get(c => c.Date == date);

            Assert.IsNull(modifiedRule);
        }

        [Test]
        public void TypiconEntityService_GetAllTypiconEntities()
        {
            var _unitOfWork = UnitOfWorkFactory.Create();

            //BookStorage.Instance = BookStorageFactory.Create();

            TypiconEntityService service = new TypiconEntityService(_unitOfWork);

            GetTypiconEntitiesResponse response = service.GetAllTypiconEntities();

            Assert.GreaterOrEqual(response.TypiconEntities.Count(), 1);
        }

        [Test]
        public void TypiconEntityService_GetTypiconEntity()
        {
            var _unitOfWork = UnitOfWorkFactory.Create();

            //BookStorage.Instance = BookStorageFactory.Create();

            TypiconEntityService service = new TypiconEntityService(_unitOfWork);

            GetTypiconEntityResponse response = service.GetTypiconEntity(1);

            Assert.NotNull(response.TypiconEntity);
        }
    }
}
