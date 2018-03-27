using System;
using System.Collections.Generic;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;
using System.Linq.Expressions;
using System.Linq;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain;
using Microsoft.EntityFrameworkCore;
//using System.Data;

namespace TypiconOnline.Repository.EFCore
{
    public class Repository<DomainType> : IRepository<DomainType> where DomainType : class, IAggregateRoot
    {
        private DBContextBase _typiconDBContext = null;
        DbSet<DomainType> _objectSet;

        public Repository(DBContextBase typiconDBContext)
        {
            _typiconDBContext = typiconDBContext;
            _objectSet = _typiconDBContext.Set<DomainType>();
        }

        public IEnumerable<DomainType> GetAll(Expression<Func<DomainType, bool>> predicate = null)
        {
            IQueryable<DomainType> request = new ClassPropertiesIncluder<DomainType>(_objectSet).GetIncludes();

            if (predicate != null)
            {
                request = request.Where(predicate);
            }

            return request.AsNoTracking().AsEnumerable();
        }

        public DomainType Get(Expression<Func<DomainType, bool>> predicate)
        {
            return GetAll(predicate).FirstOrDefault();
        }

        public void Update(DomainType aggregate)
        {
            _typiconDBContext.Update(aggregate);
            //if (_typiconDBContext.Entry(aggregate).State != EntityState.Modified)
            //{
            //    _typiconDBContext.Entry(aggregate).State = EntityState.Modified;
            //}
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
