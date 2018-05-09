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
    public class Repository<DomainType> : IRepository<DomainType> where DomainType : class//, IAggregateRoot
    {
        readonly DbSet<DomainType> objectSet;

        public Repository(TypiconDBContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException("TypiconDBContext in Repository");

            objectSet = dbContext.Set<DomainType>();
        }

        public IQueryable<DomainType> GetAll(Expression<Func<DomainType, bool>> predicate = null, IncludeOptions options = null)
        {
            var request = objectSet.GetIncludes(options);

            if (predicate != null)
            {
                request = request.Where(predicate);
            }

            //TODO: AsNoTracking??
            // ASNOTracking используется для read-only случаев, т.е. изменения не фиксируются при сохранении
            //return request.AsNoTracking().AsEnumerable();
            return request;
        }

        public DomainType Get(Expression<Func<DomainType, bool>> predicate, IncludeOptions options = null)
        {
            return GetAll(predicate, options).FirstOrDefault();
        }

        public void Update(DomainType aggregate)
        {
            objectSet.Update(aggregate);
            //if (_typiconDBContext.Entry(aggregate).State != EntityState.Modified)
            //{
            //    _typiconDBContext.Entry(aggregate).State = EntityState.Modified;
            //}
        }

        public void Add(DomainType aggregate)
        {
            objectSet.Add(aggregate);
        }

        public void Remove(DomainType aggregate)
        {
            objectSet.Remove(aggregate);
        }
    }
}
