using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Repository.EFCore
{
    /// <summary>
    /// за основу взят пример кода отсюда
    /// https://www.codeproject.com/Articles/770156/Understanding-Repository-and-Unit-of-Work-Pattern
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        protected TypiconDBContext dbContext = null;
        IRepositoryFactory repositoryFactory;

        //private bool disposed = false;

        public UnitOfWork(TypiconDBContext dbContext, IRepositoryFactory repositoryFactory)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException("dbContext in UnitOfWorkBase");
            this.repositoryFactory = repositoryFactory ?? throw new ArgumentNullException("repositoryFactory in UnitOfWorkBase");
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }

        protected Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public IRepository<AggregateType> Repository<AggregateType>() where AggregateType : class//, IAggregateRoot
        {
            if (repositories.Keys.Contains(typeof(AggregateType)) == true)
            {
                return repositories[typeof(AggregateType)] as IRepository<AggregateType>;
            }
            IRepository<AggregateType> repo = repositoryFactory.Create<AggregateType>(dbContext);
            repositories.Add(typeof(AggregateType), repo);
            return repo;
        }

        //public virtual void Dispose(bool disposing)
        //{
        //    if (!disposed)
        //    {
        //        if (disposing)
        //        {
        //            _dbContext.Dispose();
        //        }
        //    }
        //    disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}
