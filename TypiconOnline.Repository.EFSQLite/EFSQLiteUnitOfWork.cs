using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFSQLite.DataBase;

namespace TypiconOnline.Repository.EFSQLite
{
    /// <summary>
    /// за основу взят пример кода отсюда
    /// https://www.codeproject.com/Articles/770156/Understanding-Repository-and-Unit-of-Work-Pattern
    /// </summary>
    public class EFSQLiteUnitOfWork : IUnitOfWork
    {
        SQLiteDBContext _dbContext = null;
        private bool disposed = false;

        public EFSQLiteUnitOfWork()
        {
            _dbContext = new SQLiteDBContext(@"Data\SQLiteDB.db");
        }

        public EFSQLiteUnitOfWork(string connection)
        {
            _dbContext = new SQLiteDBContext(connection);
        }

        #region Register...
        //public void RegisterDeletion(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository)
        //{
        //    repository.PersistDeletion(aggregateRoot);
        //}


        //public void RegisterInsertion(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository)
        //{
        //    repository.PersistInsertion(aggregateRoot);
        //}


        //public void RegisterUpdate(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository)
        //{
        //    repository.PersistUpdate(aggregateRoot);
        //}

        #endregion

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        protected Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public IRepository<AggregateType> Repository<AggregateType>() where AggregateType : class, IAggregateRoot
        {
            if (repositories.Keys.Contains(typeof(AggregateType)) == true)
            {
                return repositories[typeof(AggregateType)] as IRepository<AggregateType>;
            }
            IRepository<AggregateType> repo = new Repository<AggregateType>(_dbContext);
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
