using System;
using System.Collections.Generic;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using System.Data.Entity;
using TypiconOnline.Repository.EF.DataBase;
using System.Linq.Expressions;
using System.Linq;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain;
//using System.Data;

namespace TypiconOnline.Repository.EF
{
    public class Repository<DomainType> : IRepository<DomainType> where DomainType : class, IAggregateRoot
    {
        private TypiconDBContext _typiconDBContext = null;
        IDbSet<DomainType> _objectSet;

        public Repository(TypiconDBContext typiconDBContext)
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
            //return _objectSet.Where(predicate).FirstOrDefault();
            
            if (typeof(DomainType).Equals(typeof(TypiconEntity)))
            {
                //_typiconDBContext.TypiconEntitySet.Include(c => c.RulesFolder)
                //    .Include(c => c.CommonRules)
                //    .Include(c => c.MenologyRules)
                //    .Include(c => c.MenologyRules.Select(m => m.Day))
                //    .Include(c => c.TriodionRules)
                //    .Include(c => c.TriodionRules.Select(m => m.Day))
                //    .Include(c => c.ModifiedYears)
                //    .Include(c => c.Signs)
                //    .Where(predicate).FirstOrDefault();
                return _objectSet.
                    Include("RulesFolder").
                    Include("CommonRules").
                    Include("MenologyRules").
                    Include("MenologyRules.Day").
                    Include("TriodionRules").
                    Include("TriodionRules.Day").
                    Include("ModifiedYears").
                    Include("ModifiedYears.ModifiedRules").
                    Include("Signs").
                    Where(predicate).FirstOrDefault();
            }

            return _objectSet.Where(predicate).FirstOrDefault();
        }

        public DomainType Get(int id)
        {
            return _objectSet.Find(id);
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
