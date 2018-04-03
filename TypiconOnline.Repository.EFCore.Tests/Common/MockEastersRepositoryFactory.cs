using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Repository.EFCore.Tests.Common
{
    public class MockEastersRepositoryFactory
    {
        /// <summary>
        /// Возвращает репозиторий, возвращающий фейковые значения для EasterItem
        /// </summary>
        /// <returns></returns>
        public static IRepositoryFactory Create()
        {
            var mockRepo = new Mock<IRepository<EasterItem>>();

            mockRepo.Setup(c => c.GetAll(null)).Returns(GetAllAsters());
            mockRepo.Setup(c => c.Get(It.IsAny<Expression<Func<EasterItem, bool>>>())).Returns(new EasterItem() { Date = new DateTime(2018, 1, 1) });

            var mockFactory = new Mock<IRepositoryFactory>();
            mockFactory.Setup(c => c.Create<EasterItem>(It.IsAny<DBContextBase>())).Returns(mockRepo.Object);

            return mockFactory.Object;
        }

        /// <summary>
        /// Возвращает 100 элементов с днем даты year-01-01
        /// </summary>
        /// <returns></returns>
        private static IQueryable<EasterItem> GetAllAsters()
        {
            var easters = new List<EasterItem>();

            var date = new DateTime(2010, 1, 1);

            for(int i = 0; i < 100; i++)
            {
                easters.Add(new EasterItem() { Date = date.AddYears(i) });
            }

            return easters.AsQueryable();
        }
    }
}
