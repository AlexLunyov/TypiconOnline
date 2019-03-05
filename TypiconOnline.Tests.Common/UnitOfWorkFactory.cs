using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Repository.EFCore;

namespace TypiconOnline.Tests.Common
{
    public class UnitOfWorkFactory
    {
        public static IUnitOfWork Create()
        {
            return Create(TypiconDbContextFactory.Create());
        }

        public static IUnitOfWork Create(TypiconDBContext dbContext)
        {
            return new UnitOfWork(dbContext, new RepositoryFactory());
        }
    }
}
