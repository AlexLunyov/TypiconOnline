using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Repository.EFCore
{
    public class RepositoryFactory : IRepositoryFactory
    {
        DBContextBase dbContext;

        public RepositoryFactory(DBContextBase dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentOutOfRangeException("dbContext in RepositoryFactory");
        }

        public IRepository<AggregateType> Create<AggregateType>() where AggregateType : class, IAggregateRoot
        {
            return new Repository<AggregateType>(dbContext);
        }
    }
}
