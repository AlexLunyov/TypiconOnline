using System;
using System.Collections.Generic;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFSQLite.DataBase;
using System.Linq.Expressions;
using System.Linq;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain;
using Microsoft.EntityFrameworkCore;
//using System.Data;

namespace TypiconOnline.Repository.EFSQLite
{
    public class Repository<DomainType> : IRepository<DomainType> where DomainType : class, IAggregateRoot
    {
        private SQLiteDBContext _typiconDBContext = null;
        DbSet<DomainType> _objectSet;

        public Repository(SQLiteDBContext typiconDBContext)
        {
            _typiconDBContext = typiconDBContext;
            _objectSet = _typiconDBContext.Set<DomainType>();
        }

        public IEnumerable<DomainType> GetAll(Expression<Func<DomainType, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return _objectSet.Where(predicate);
            }

            return _objectSet.AsEnumerable();
        }

        public DomainType Get(Expression<Func<DomainType, bool>> predicate)
        {
            return _objectSet.Where(predicate).FirstOrDefault();
        }

        public void Update(DomainType aggregate)
        {
            //_objectSet.Attach(aggregate);
            _typiconDBContext.Entry(aggregate).State = EntityState.Modified;
        }

        public void Insert(DomainType aggregate)
        {
            _objectSet.Add(aggregate);
        }

        public void Delete(DomainType aggregate)
        {
            _objectSet.Remove(aggregate);
        }
    }
}
