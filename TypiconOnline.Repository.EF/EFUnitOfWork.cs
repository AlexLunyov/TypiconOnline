using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EF.DataBase;

namespace TypiconOnline.Repository.EF
{
    /// <summary>
    /// за основу взят пример кода отсюда
    /// https://www.codeproject.com/Articles/770156/Understanding-Repository-and-Unit-of-Work-Pattern
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        TypiconDBContext _dbContext = null;
        //private bool disposed = false;

        public EFUnitOfWork()
        {
            _dbContext = new TypiconDBContext();
        }

        #region Repositories

        //private MenologyDayRepository _menologyDayRepository = null;

        //public MenologyDayRepository MenologyDayRepository
        //{
        //    get
        //    {
        //        if (_menologyDayRepository == null)
        //        {
        //            _menologyDayRepository = new MenologyDayRepository(_dbContext);
        //        }
        //        return _menologyDayRepository;
        //    }
        //}


        #endregion


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

        public void SaveChanges()
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

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
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
